﻿//	Copyright © 2012-2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Marc BETTEX, Maintainer: Marc BETTEX

using Epsitec.Common.Support.EntityEngine;

using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Metadata;

using Epsitec.Cresus.DataLayer.Expressions;

using Epsitec.Cresus.WebCore.Server.Core.Databases;
using Epsitec.Cresus.WebCore.Server.Core.PropertyAccessor;

using Nancy;
using Nancy.Json;

using System;

using System.Collections.Generic;

using System.Linq;


namespace Epsitec.Cresus.WebCore.Server.Core.IO
{


	using Database = Core.Databases.Database;
	using System.Globalization;


	/// <summary>
	/// This class provides the methods required to parse filters sent by the javascript client.
	/// </summary>
	internal static class FilterIO
	{


		public static EntityFilter ParseFilter(BusinessContext businessContext, Caches caches, Database database, string filterParameter)
		{
			var entityType = database.DataSetMetadata.EntityTableMetadata.EntityType;
			var entityId = EntityInfo.GetTypeId (entityType);
			var entityFilter = new EntityFilter (entityId, FilterCombineMode.And);

			if (string.IsNullOrEmpty (filterParameter))
			{
				return entityFilter;
			}

			var deserializer = new JavaScriptSerializer ();
			var filters = (object[]) deserializer.DeserializeObject (filterParameter);

			foreach (var filter in filters.Cast<Dictionary<string, object>> ())
			{
				var column = ColumnIO.ParseColumn (caches, database, (string) filter["field"]);

				var columnId = column.MetaData.Id;
				var columnFilter = FilterIO.ParseColumnFilter (businessContext, caches, column, filter);
				var columnRef = new ColumnRef<EntityColumnFilter> (columnId, columnFilter);

				entityFilter.Columns.Add (columnRef);
			}

			return entityFilter;
		}

		public static Filter ParseQuery(BusinessContext businessContext, Caches caches, Database database, string queryParameter)
		{
			var entityType = database.DataSetMetadata.EntityTableMetadata.EntityType;
			var entityId = EntityInfo.GetTypeId (entityType);
			var queryFilter = new Filter ();
			
			if (string.IsNullOrEmpty (queryParameter))
			{
				return queryFilter;
			}

			var deserializer = new JavaScriptSerializer ();
			var query = (object[]) deserializer.DeserializeObject (queryParameter);

			var filtersDef = new Dictionary<string, object> ();
			var queryNode = new FilterNode (queryFilter, FilterIncludeMode.Inclusive, FilterActiveMode.Enabled);
			FilterIO.ParseQueryGroup (businessContext, caches, database, entityId, (Dictionary<string, object>) query[0],ref queryNode, ref queryFilter);
			
			return queryFilter;
		}

		private static void ParseQueryGroup (BusinessContext businessContext, Caches caches, Database database, Common.Support.Druid entityId, Dictionary<string, object> group, ref FilterNode node, ref Filter rootFilter)
		{
			var filterOp = (string) group["operator"];
			var rules    = (object[]) group["rules"];

			var richFilter = new Filter ();
			
			var currentGroupNode = new FilterNode (richFilter, FilterIncludeMode.Inclusive, FilterActiveMode.Enabled);
			
		
			if(filterOp == "and")
			{
				richFilter.CombineMode = FilterCombineMode.And;
			}
			else
			{
				richFilter.CombineMode = FilterCombineMode.Or;
			}

			node.RichFilter.Nodes.Add (currentGroupNode);

			foreach (var rule in rules)
			{
				var type = (Dictionary<string, object>) rule;

				object subGroup = null;
				if (type.TryGetValue ("group", out subGroup))
				{
					//find subgroup
					FilterIO.ParseQueryGroup (businessContext, caches, database, entityId, (Dictionary<string, object>) subGroup,ref currentGroupNode, ref rootFilter);
				}
				else
				{
					//Create simple filter and add it to existing filter node
					var field = (string) type["field"];
					var entityFilter = new EntityFilter (entityId, FilterCombineMode.And);
					var column = database.Columns.First (c => c.Name == field);
					var columnId = column.MetaData.Id;
					var columnFilter = FilterIO.ParseColumnFilter (businessContext, caches, column, type);
					var columnRef = new ColumnRef<EntityColumnFilter> (columnId, columnFilter);
					entityFilter.Columns.Add (columnRef);

					var simpleFilterNode = new FilterNode (entityFilter, FilterIncludeMode.Inclusive, FilterActiveMode.Enabled);
					richFilter.Nodes.Add (simpleFilterNode);				
				}
			}

			return;
		}


		private static EntityColumnFilter ParseColumnFilter(BusinessContext businessContext, Caches caches, Column column, Dictionary<string, object> filter)
		{
			var lambda = column.LambdaExpression;
			var propertyAccessorCache = caches.PropertyAccessorCache;
			var propertyAccessor = propertyAccessorCache.Get (lambda);

			var fieldType = propertyAccessor.FieldType;
			var valueType = propertyAccessor.Type;

			var type = (string) filter["type"];
			var value = filter["value"];

			if (type == "date")
			{
				DateTime dt;
				// Check for a valid "wire format"
				var isValidWireFormat = DateTime.TryParseExact(
											(string) value,
											"yyyy-MM-dd",
											CultureInfo.InvariantCulture,
											DateTimeStyles.None,
											out dt);
				if (isValidWireFormat)
				{
					//Convert back to Constant filter date format
					value = dt.Date.ToString ("dd.MM.yyyy");
				}
			}

			if (type == "list")
			{
				return FilterIO.ParseColumnSetFilter (businessContext, fieldType, valueType, value);
			}
			else
			{
				object comparison;
				if (!filter.TryGetValue ("comparison", out comparison))
				{
					comparison = "eq";
				}

				return FilterIO.ParseColumnComparisonFilter (businessContext, fieldType, valueType, type, comparison, value);
			}
		}


		private static EntityColumnFilter ParseColumnSetFilter(BusinessContext businessContext, FieldType fieldType, Type valueType, object value)
		{
			var valueArray = (object[]) value;
			var constants = valueArray.Select (v => FilterIO.ParseConstant (businessContext, fieldType, valueType, v, ColumnFilterComparisonCode.Undefined));

			var filterExpression = new ColumnFilterSetExpression ()
			{
				Predicate = ColumnFilterSetCode.In,
			};

			foreach (var constant in constants)
			{
				filterExpression.Values.Add (constant);
			}

			return new EntityColumnFilter (filterExpression);
		}


		private static EntityColumnFilter ParseColumnComparisonFilter(BusinessContext businessContext, FieldType fieldType, Type valueType, string type, object comparator, object value)
		{
			var comparison = FilterIO.ParseComparison (type, comparator, ref value);
			var constant = FilterIO.ParseConstant (businessContext, fieldType, valueType, value, comparison);

			var filterExpression = new ColumnFilterComparisonExpression ()
			{
				Comparison = comparison,
				Constant = constant,
			};

			return new EntityColumnFilter (filterExpression);
		}


		private static ColumnFilterComparisonCode ParseComparison(string type, object comparator, ref object value)
		{
			if (type == "string")
			{
				var text = value as string;

				if (text != null)
				{
					if (text.StartsWith ("*") && !text.EndsWith ("*"))
					{
						value = text.Substring (1);
						return ColumnFilterComparisonCode.EndsWithEscaped;
					}

					if (text.EndsWith ("*") && !text.StartsWith ("*"))
					{
						value = text.Substring (0, text.Length -1);
						return ColumnFilterComparisonCode.StartsWithEscaped;
					}

					if (text.EndsWith ("*") && text.StartsWith ("*"))
					{
						var preValue = text.Substring (0, text.Length -1);
						value = preValue.Substring (1);
						return ColumnFilterComparisonCode.ContainsEscaped;
					}

					if (text.EndsWith ("\"") && text.StartsWith ("\""))
					{
						var preValue = text.Substring (0, text.Length -1);
						value = preValue.Substring (1);
						return ColumnFilterComparisonCode.Equal;
					}

					return ColumnFilterComparisonCode.StartsWithEscaped;
				}
				else // By default we are not srict
				{
					return ColumnFilterComparisonCode.StartsWithEscaped;
				}
			}

			switch ((string) comparator)
			{
				case "eq":
					return ColumnFilterComparisonCode.Equal;
				case "nq":
					return ColumnFilterComparisonCode.NotEqual;
				case "gt":
					return ColumnFilterComparisonCode.GreaterThan;
				case "lt":
					return ColumnFilterComparisonCode.LessThan;
				case "even":
					return ColumnFilterComparisonCode.Even;
				case "odd":
					return ColumnFilterComparisonCode.Odd;
				default:
					throw new NotImplementedException ();
			}
		}


		private static ColumnFilterConstant ParseConstant(BusinessContext businessContext, FieldType fieldType, Type valueType, object value, ColumnFilterComparisonCode comparison)
		{
			var nancyValue = new DynamicDictionaryValue (value);
			var entityValue = FieldIO.ConvertFromClient (businessContext, nancyValue, valueType, fieldType);

			switch (fieldType)
			{
				case FieldType.Boolean:
					return ColumnFilterConstant.From ((bool?) entityValue);

				case FieldType.Date:
					return ColumnFilterConstant.From ((Date?) entityValue);

				case FieldType.DateTime:
					return ColumnFilterConstant.From ((DateTime?) entityValue);

				case FieldType.Decimal:
					return ColumnFilterConstant.From ((decimal?) entityValue);

				case FieldType.Enumeration:
					return ColumnFilterConstant.From ((Enum) entityValue);

				case FieldType.Integer:
					if (entityValue is short? || entityValue is short)
					{
						return ColumnFilterConstant.From ((short?) entityValue);
					}
					else if (entityValue is int? || entityValue is int)
					{
						return ColumnFilterConstant.From ((int?) entityValue);
					}
					else if (entityValue is long? || entityValue is long)
					{
						return ColumnFilterConstant.From ((long?) entityValue);
					}
					else
					{
						// Odd or Even comparison need a dummy constant value
						if (comparison == ColumnFilterComparisonCode.Odd || comparison == ColumnFilterComparisonCode.Even)
						{
							return ColumnFilterConstant.From (1);
						}
						throw new NotImplementedException ();
					}

				case FieldType.Text:
					var pattern = Constant.Escape (FormattedText.CastToString (entityValue));

					pattern = pattern.Replace ('*', '%');

					switch (comparison)
					{
						case ColumnFilterComparisonCode.EndsWith:
						case ColumnFilterComparisonCode.EndsWithEscaped:
							return ColumnFilterConstant.From ("%" + pattern);

						case ColumnFilterComparisonCode.Contains:
						case ColumnFilterComparisonCode.ContainsEscaped:
						case ColumnFilterComparisonCode.NotContains:
						case ColumnFilterComparisonCode.NotContainsEscaped:
							return ColumnFilterConstant.From ("%" + pattern + "%");

						case ColumnFilterComparisonCode.StartsWith:
						case ColumnFilterComparisonCode.StartsWithEscaped:
						case ColumnFilterComparisonCode.NotStartsWith:
						case ColumnFilterComparisonCode.NotStartsWithEscaped:
							return ColumnFilterConstant.From (pattern + "%");

						default:
							return ColumnFilterConstant.From (pattern);
					}

				case FieldType.Time:
					return ColumnFilterConstant.From ((Time?) entityValue);

				default:
					throw new NotImplementedException ();
			}
		}



	}


}
