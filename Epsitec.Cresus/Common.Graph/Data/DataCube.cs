﻿//	Copyright © 2009, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Common.Graph.Data
{
	public class DataCube
	{
		public DataCube()
		{
			this.values = new Dictionary<string, double> ();
			this.dimensions = new Dictionary<string, DimensionValues> ();
			this.dimensionNames = new List<string> ();
		}


		public IList<string> DimensionNames
		{
			get
			{
				return this.dimensionNames.AsReadOnly ();
			}
		}


		public void AddTable(DataTable table)
		{
			string colKey = table.ColumnDimensionKey;
			string rowKey = table.RowDimensionKey;

			if (string.IsNullOrEmpty (colKey))
			{
				throw new System.ArgumentException ("Table has no ColumnDimensionKey");
			}
			if (string.IsNullOrEmpty (rowKey))
			{
				throw new System.ArgumentException ("Table has no RowDimensionKey");
			}

			foreach (var row in table.RowSeries)
			{
				string rowLabel = row.Label;

				foreach (var cell in row.Values)
				{
					DimensionVector vector = new DimensionVector (table.DimensionVector);

					vector.Add (new Dimension (rowKey, rowLabel));
					vector.Add (new Dimension (colKey, cell.Label));

					this.values.Add (vector.Compile (), cell.Value);

					foreach (Dimension dimension in vector)
					{
						DimensionValues values;

						if (this.dimensions.TryGetValue (dimension.Key, out values))
						{
							values.Add (dimension.Value);
						}
						else
						{
							values = new DimensionValues ();
							values.Add (dimension.Value);
							
							this.dimensions.Add (dimension.Key, values);
							this.dimensionNames.Add (dimension.Key);
						}
					}
				}
			}

			this.dimensionNames.Sort ((a, b) => string.CompareOrdinal (a, b));
		}


		public IList<string> GetDimensionValues(string key)
		{
			DimensionValues values;

			if (this.dimensions.TryGetValue (key, out values))
			{
				List<string> list = values.ToList ();
				list.Sort ();
				return list.AsReadOnly ();
			}
			else
			{
				return null;
			}
		}

		public ChartSeries ExtractChartSeries(params string[] dimensions)
		{
			return new ChartSeries (this.Accumulate (dimensions).Values);
		}

		public DataTable ExtractDataTable(params string[] dimensions)
		{
			List<string> axes;

			var accumulator = this.Accumulate (dimensions, out axes);

			if (axes.Count != 2)
			{
				throw new System.ArgumentException ("Invalid number of axes for table extraction");
			}

			DataTable table = new DataTable ();

			table.RowDimensionKey    = axes[0];
			table.ColumnDimensionKey = axes[1];

			HashSet<string> rowLabels = new HashSet<string> ();
			HashSet<string> colLabels = new HashSet<string> ();

			foreach (var item in accumulator.Values)
			{
				string[] keys = item.Label.Split ('+');

				rowLabels.Add (keys[0]);
				colLabels.Add (keys[1]);
			}

			table.DefineRowLabels (rowLabels.OrderBy (x => x));
			table.DefineColumnLabels (colLabels.OrderBy (x => x));

			foreach (var item in accumulator.Values)
			{
				string[] keys = item.Label.Split ('+');

				table[keys[0], keys[1]] = item.Value;
			}

			return table;
		}

		
		private Accumulator Accumulate(string[] dimensions)
		{
			List<string> axes;
			return this.Accumulate (dimensions, out axes);
		}
		
		private Accumulator Accumulate(string[] dimensions, out List<string> axes)
		{
			List<string> dims;
			
			System.Text.RegularExpressions.Regex regex = this.GetExtractionRegex (dimensions, out dims, out axes);

			int[] groupIndexMap = DataCube.GetGroupIndexMapAndUpdateAxes (dims, axes);

			Accumulator accumulator = new Accumulator ();
			System.Text.StringBuilder key = new System.Text.StringBuilder ();

			int expectedGroupsInRegexMatch = groupIndexMap.Length + 1;

			foreach (var item in this.values)
			{
				var match = regex.Match (item.Key);

				if (match.Success)
				{
					int num = match.Groups.Count;

					System.Diagnostics.Debug.Assert (num == expectedGroupsInRegexMatch);

					if (num == expectedGroupsInRegexMatch)
					{
						key.Length = 0;

						foreach (int groupIndex in groupIndexMap)
						{
							if (key.Length > 0)
							{
								key.Append ('+');
							}

							key.Append (match.Groups[groupIndex].Value);
						}
						
						accumulator.Accumulate (key.ToString (), item.Value);
					}
				}
			}

			return accumulator;
		}

		private System.Text.RegularExpressions.Regex GetExtractionRegex(string[] dimensions, out List<string> dims, out List<string> axes)
		{
			dims = new List<string> (dimensions);
			axes = new List<string> ();

			System.Text.StringBuilder buffer = new System.Text.StringBuilder ("^");
			
			foreach (string name in this.dimensionNames)
			{
				string prefix = name + "=";

				int pos = dims.FindIndex (x => x == name);

				if (pos >= 0)
				{
					//	Found an exact match, which means that we have found the dimension
					//	which we will enumerate to produce our sequence.

					if (buffer.Length > 1)
					{
						buffer.Append (':');
					}

					buffer.Append (prefix);
					buffer.Append ("([^:]*)");

					axes.Add (name);

					continue;
				}

				pos = dims.FindIndex (x => x.StartsWith (prefix));

				if (pos >= 0)
				{
					//	Found an extraction match; just use it as is.

					if (buffer.Length > 1)
					{
						buffer.Append (':');
					}

					buffer.Append (dims[pos]);
					dims.RemoveAt (pos);

					continue;
				}

				if (buffer.Length > 1)
				{
					buffer.Append (':');
				}

				buffer.Append (prefix);
				buffer.Append ("[^:]*");
			}

			buffer.Append ('$');

			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex (buffer.ToString (), System.Text.RegularExpressions.RegexOptions.Compiled);
			return regex;
		}

		private static int[] GetGroupIndexMapAndUpdateAxes(List<string> dims, List<string> axes)
		{
			int[] groupIndexes = new int[axes.Count];

			for (int i = 0; i < groupIndexes.Length; i++)
			{
				groupIndexes[i] = dims.IndexOf (axes[i]) + 1;
			}

			axes.Clear ();
			axes.AddRange (dims);
			
			return groupIndexes;
		}


		#region DimensionValues Class

		private sealed class DimensionValues : HashSet<string>
		{
		}

		#endregion


		readonly Dictionary<string, double> values;
		readonly Dictionary<string, DimensionValues> dimensions;
		readonly List<string> dimensionNames;
	}
}
