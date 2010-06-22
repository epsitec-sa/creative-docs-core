//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Widgets.Tiles;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.DataAccessors
{
	/// <summary>
	/// The <c>CollectionTemplate</c> class provides the basic functionality
	/// needed to create and delete items, related to a <see cref="CollectionAccessor"/>.
	/// </summary>
	public abstract class CollectionTemplate
	{
		protected CollectionTemplate(string name)
		{
			this.name = name;
		}

		public string NamePrefix
		{
			get
			{
				return this.name;
			}
		}

		public abstract bool IsCompatible(AbstractEntity entity);

		public abstract void BindSummaryData(SummaryData data, AbstractEntity entity, ICollectionAccessor collectionAccessor);

		public abstract AbstractEntity CreateItem();
		
		public abstract void DeleteItem(AbstractEntity item);

		public abstract void BindCreateItem(SummaryData data, ICollectionAccessor collectionAccessor);

		private readonly string name;
	}

	public class CollectionTemplate<T> : CollectionTemplate
			where T : AbstractEntity, new ()
	{
		public CollectionTemplate(string name)
			: base (name)
		{
			this.DefineCreateItem (CollectionTemplate<T>.CreateEmptyItem);
			this.DefineDeleteItem (item => { });
		}

		public CollectionTemplate(string name, System.Predicate<T> filter)
			: this (name)
		{
			this.Filter = filter;
		}

		public bool HasCreateItem
		{
			get
			{
				return this.createItem != null;
			}
		}

		public bool HasDeleteItem
		{
			get
			{
				return this.deleteItem != null;
			}
		}

		public CollectionTemplate<T> DefineCreateItem(System.Func<T> action)
		{
			this.createItem = action;
			return this;
		}

		public CollectionTemplate<T> DefineDeleteItem(System.Action<T> action)
		{
			this.deleteItem = action;
			return this;
		}

		public CollectionTemplate<T> DefineSetupItem(System.Action<T> action)
		{
			this.setupItem = action;
			return this;
		}

		public CollectionTemplate<T> DefineTitle(System.Func<T, FormattedText> action)
		{
			this.TitleAccessor = IndirectAccessor<T>.Create (action);
			return this;
		}

		public CollectionTemplate<T> DefineText(System.Func<T, FormattedText> action)
		{
			this.TextAccessor = IndirectAccessor<T>.Create (action);
			return this;
		}

		public CollectionTemplate<T> DefineCompactTitle(System.Func<T, FormattedText> action)
		{
			this.CompactTitleAccessor = IndirectAccessor<T>.Create (action);
			return this;
		}

		public CollectionTemplate<T> DefineCompactText(System.Func<T, FormattedText> action)
		{
			this.CompactTextAccessor = IndirectAccessor<T>.Create (action);
			return this;
		}

		public IndirectAccessor<T, FormattedText> TitleAccessor
		{
			get;
			set;
		}

		public IndirectAccessor<T, FormattedText> TextAccessor
		{
			get;
			set;
		}

		public IndirectAccessor<T, FormattedText> CompactTitleAccessor
		{
			get;
			set;
		}

		public IndirectAccessor<T, FormattedText> CompactTextAccessor
		{
			get;
			set;
		}

		public System.Predicate<T> Filter
		{
			get;
			set;
		}

		public override bool IsCompatible(AbstractEntity entity)
		{
			T source = entity as T;

			if (source == null)
			{
				return false;
			}
			else if (this.Filter == null)
			{
				return true;
			}
			else
			{
				return this.Filter (source);
			}
		}

		public override void BindSummaryData(SummaryData data, AbstractEntity entity, ICollectionAccessor collectionAccessor)
		{
			T source = entity as T;

			var context = Epsitec.Cresus.DataLayer.DataContextPool.Instance.FindDataContext (entity);

			if ((context != null) &&
				(context.IsRegisteredAsEmptyEntity (entity)))
			{
				this.BindEmptyEntitySummaryData (data, source);
			}
			else
			{
				this.BindRealEntitySummaryData (data, source, collectionAccessor);
			}
		}

		public override void BindCreateItem(SummaryData data, ICollectionAccessor collectionAccessor)
		{
			if (this.HasCreateItem && collectionAccessor != null)
			{
				data.AddNewItem = () => collectionAccessor.AddItem (this.GenericCreateItem ());
			}
		}

		
		public override AbstractEntity CreateItem()
		{
			return this.GenericCreateItem ();
		}

		public override void DeleteItem(AbstractEntity item)
		{
			this.GenericDeleteItem (item as T);
		}

		private static T CreateEmptyItem()
		{
			T entity = EntityContext.Current.CreateEmptyEntity<T> ();
			var context = Epsitec.Cresus.DataLayer.DataContextPool.Instance.FindDataContext (entity);

			context.RegisterEmptyEntity (entity);

			return entity;
		}

		private void BindEmptyEntitySummaryData(SummaryData data, T source)
		{
			data.EntityAccessor = () => source;
			data.DataType		= SummaryDataType.CollectionItem;
			data.Text           = UIBuilder.FormatText ("<i>Définition en cours</i>");
		}

		private void BindRealEntitySummaryData(SummaryData data, T source, ICollectionAccessor collectionAccessor)
		{
			data.EntityAccessor		  = () => source;
			data.TitleAccessor        = IndirectAccessor<T, FormattedText>.GetAccessor (this.TitleAccessor, source);
			data.TextAccessor         = IndirectAccessor<T, FormattedText>.GetAccessor (this.TextAccessor, source);
			data.CompactTitleAccessor = IndirectAccessor<T, FormattedText>.GetAccessor (this.CompactTitleAccessor, source);
			data.CompactTextAccessor  = IndirectAccessor<T, FormattedText>.GetAccessor (this.CompactTextAccessor, source);
			data.DataType			  = SummaryDataType.CollectionItem;

			if (this.HasCreateItem && this.HasDeleteItem && collectionAccessor != null)
			{
				data.AddNewItem = () => collectionAccessor.AddItem (this.GenericCreateItem ());
				data.DeleteItem = () => collectionAccessor.RemoveItem (source);
			}

			data.GroupController = new GroupedItemController (collectionAccessor.GetItemCollection (), source, x => this.IsCompatible (x));
		}
		private T GenericCreateItem()
		{
			var item = this.createItem ();

			if (item != null)
			{
				EntityNullReferenceVirtualizer.PatchNullReferences (item);

				if (this.setupItem != null)
				{
					this.setupItem (item);
				}
			}

			return item;
		}

		private void GenericDeleteItem(T item)
		{
			this.deleteItem (item);
		}


		private System.Func<T> createItem;
		private System.Action<T> deleteItem;
		private System.Action<T> setupItem;
	}
}
