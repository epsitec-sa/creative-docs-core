﻿using Epsitec.Common.Support.EntityEngine;

using Epsitec.Cresus.Core.Data;
using Epsitec.Cresus.Core.Metadata;

using Epsitec.Cresus.WebCore.Server.Core.Databases;
using Epsitec.Cresus.WebCore.Server.Core.PropertyAccessor;

using System;

using System.Collections.Generic;

using System.IO;

using System.Linq;


namespace Epsitec.Cresus.WebCore.Server.Core.Extraction
{


	internal sealed class ArrayWriter : EntityWriter
	{


		internal ArrayWriter(PropertyAccessorCache properties, DataSetMetadata metadata, IEnumerable<Column> columns, DataSetAccessor accessor, ArrayFormat format)
			: base (metadata, accessor)
		{
			this.columns = columns.ToList ();
			this.properties = properties;
			this.format = format;
		}


		protected override string GetExtension()
		{
			return this.format.Extension;
		}


		protected override void WriteStream(Stream stream)
		{
			var headers = this.GetHeaders ();
			var rows = this.GetRows ();

			this.format.Write (stream, headers, rows);
		}


		private IList<string> GetHeaders()
		{
			return this.columns
				.Select (c => c.MetaData.GetColumnTitle ().ToSimpleText ())
				.ToList ();
		}


		private IList<IList<string>> GetRows()
		{
			var columnAccessors = this.GetColumnAccessors ();
			var items = this.Accessor.GetAllItems ();

			var dataContext = this.Accessor.IsolatedDataContext;
			var expressions = this.columns.Select (c => c.LambdaExpression);
			dataContext.LoadRelatedData (items, expressions);

			return items
				.Select (e => this.GetCells (columnAccessors, e))
				.ToList ();
		}


		private IList<AbstractPropertyAccessor> GetColumnAccessors()
		{
			return this.columns
				.Select (c => this.properties.Get (c.LambdaExpression))
				.ToList ();
		}


		private IList<string> GetCells(IList<AbstractPropertyAccessor> columnAccessors, AbstractEntity entity)
		{
			return columnAccessors
				.Select (c => this.GetCell (c, entity))
				.ToList ();
		}


		private string GetCell(AbstractPropertyAccessor columnAccessors, AbstractEntity entity)
		{
			var value = columnAccessors.GetValue (entity);

			if (value != null && !(value is string))
			{
				throw new InvalidOperationException ();
			}

			return (string) value;
		}


		private readonly IEnumerable<Column> columns;


		private readonly PropertyAccessorCache properties;


		private readonly ArrayFormat format;


	}


}
