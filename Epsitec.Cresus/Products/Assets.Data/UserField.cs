﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Assets.Data
{
	public class UserField : IGuid
	{
		public UserField(Guid guid, string name, ObjectField field, FieldType type, bool required, int columnWidth, int? lineWidth, int? lineCount, int? summaryOrder, int topMargin)
		{
			if (type != FieldType.String)
			{
				lineWidth = null;
				lineCount = null;
			}

			this.guid         = guid;
			this.Name         = name;
			this.Field        = field;
			this.Type         = type;
			this.Required     = required;
			this.ColumnWidth  = columnWidth;
			this.LineWidth    = lineWidth;
			this.LineCount    = lineCount;
			this.SummaryOrder = summaryOrder;
			this.TopMargin    = topMargin;
		}

		public UserField(string name, ObjectField field, FieldType type, bool required, int columnWidth, int? lineWidth, int? lineCount, int? summaryOrder, int topMargin)
		{
			if (type != FieldType.String)
			{
				lineWidth = null;
				lineCount = null;
			}

			this.guid         = Guid.NewGuid ();
			this.Name         = name;
			this.Field        = field;
			this.Type         = type;
			this.Required     = required;
			this.ColumnWidth  = columnWidth;
			this.LineWidth    = lineWidth;
			this.LineCount    = lineCount;
			this.SummaryOrder = summaryOrder;
			this.TopMargin    = topMargin;
		}

		public UserField(UserField model)
		{
			this.guid         = model.Guid;
			this.Name         = model.Name;
			this.Field        = model.Field;
			this.Type         = model.Type;
			this.Required     = model.Required;
			this.ColumnWidth  = model.ColumnWidth;
			this.LineWidth    = model.LineWidth;
			this.LineCount    = model.LineCount;
			this.SummaryOrder = model.SummaryOrder;
			this.TopMargin    = model.TopMargin;
		}

		public UserField(UserField model, ObjectField field, string name)
		{
			this.guid         = Guid.NewGuid ();
			this.Name         = name;
			this.Field        = field;
			this.Type         = model.Type;
			this.Required     = model.Required;
			this.ColumnWidth  = model.ColumnWidth;
			this.LineWidth    = model.LineWidth;
			this.LineCount    = model.LineCount;
			this.SummaryOrder = model.SummaryOrder;
			this.TopMargin    = model.TopMargin;
		}


		#region IGuid Members
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}
		#endregion


		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty (this.Name)
					&& this.Field == ObjectField.Unknown
					&& this.Type  == FieldType.Unknown;
			}
		}


		public static UserField Empty = new UserField (null, ObjectField.Unknown, FieldType.Unknown, false, 0, null, null, null, 0);


		private readonly Guid					guid;

		public readonly string					Name;
		public readonly ObjectField				Field;
		public readonly FieldType				Type;
		public readonly bool					Required;
		public readonly int						ColumnWidth;
		public readonly int?					LineWidth;
		public readonly int?					LineCount;
		public readonly int?					SummaryOrder;
		public readonly int						TopMargin;
	}
}