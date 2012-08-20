//	Copyright � 2011-2012, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;

using Epsitec.Cresus.DataLayer.Expressions;

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Epsitec.Cresus.Core.Data.Extraction
{
	/// <summary>
	/// The <c>EntityDataColumn</c> class defines a column (i.e. a direct or indirect field
	/// of an entity).
	/// </summary>
	public sealed class EntityDataColumn : EntityColumn
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EntityDataColumn"/> class. This should
		/// not be called directly. Use <see cref="EntityDataMetadataRecorder.Column"/> instead.
		/// </summary>
		/// <param name="expression">The lambda expression (as an expression, not as compiled code).</param>
		/// <param name="sortOrder">The sort order.</param>
		/// <param name="name">The name associated with the column.</param>
		public EntityDataColumn(LambdaExpression expression, SortOrder sortOrder, FormattedText name)
			: base (expression, name)
		{
			this.sortOrder  = sortOrder;
		}

		public EntityDataColumn(IDictionary<string, string> data)
			: base (data)
		{
			this.sortOrder = data["sort"].ToEnum<SortOrder> ();
		}


		public SortOrder						SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		
		public SortClause ToSortClause(AbstractEntity example)
		{
			var fieldEntity = this.GetLeafEntity (example, NullNodeAction.CreateMissing);
			var fieldId     = this.GetLeafFieldId ();

			var fieldNode = new ValueField (fieldEntity, fieldId);

			return new SortClause (fieldNode, sortOrder);
		}

		protected override void Serialize(List<XAttribute> attributes)
		{
			base.Serialize (attributes);

			attributes.Add (new XAttribute ("sort", this.sortOrder.ToString ()));
		}


		private readonly SortOrder				sortOrder;
	}
}
