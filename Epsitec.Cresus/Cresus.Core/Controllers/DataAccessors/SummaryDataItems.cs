﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.DataAccessors
{
	public class SummaryDataItems : IEnumerable<SummaryData>
	{
		public SummaryDataItems()
		{
			this.staticItems = new List<SummaryData> ();
			this.emptyItems  = new List<SummaryData> ();
			this.collectionItems = new List<SummaryData> ();
			this.collectionAccessors = new List<CollectionAccessor> ();
		}


		public IList<SummaryData> StaticItems
		{
			get
			{
				return this.staticItems;
			}
		}

		public IList<SummaryData> EmptyItems
		{
			get
			{
				return this.emptyItems;
			}
		}

		public IList<CollectionAccessor> CollectionAccessors
		{
			get
			{
				return this.collectionAccessors;
			}
		}


		public object SyncObject
		{
			get
			{
				return this.exclusion;
			}
		}


		public void Add(SummaryData data)
		{
			int rank = this.EmptyItems.Count + this.StaticItems.Count;

			if (data.Rank == 0)
			{
				data.Rank = SummaryData.CreateRank (rank+1, 0);
			}

			if (data.EntityAccessor == null)
			{
				this.EmptyItems.Add (data);
			}
			else
			{
				this.StaticItems.Add (data);
			}
		}

		public void Add(CollectionAccessor collectionAccessor)
		{
			this.CollectionAccessors.Add (collectionAccessor);
		}

		
		public void RefreshCollectionItems()
		{
			var items = new List<SummaryData> ();

			foreach (var accessor in this.collectionAccessors)
			{
				items.AddRange (accessor.Resolve (this.GetTemplate));
			}

			this.collectionItems.Clear ();
			this.collectionItems.AddRange (items);

			this.RefreshEmptyItems ();
		}

		private void RefreshEmptyItems()
		{
			foreach (var item in this.emptyItems)
			{
				item.DataType = SummaryDataType.EmptyItem;

				if (item.AddNewItem == null)
                {
					var accessor = this.collectionAccessors.First (x => x.Template.NamePrefix == item.Name);

					accessor.Template.BindCreateItem (item, accessor);
                }
			}
		}


		private SummaryData GetTemplate(string name, int index)
		{
			//	Look for templates in the existing collection items first, then
			//	in the empty items. This will enforce reuse of existing items.

			var items = this.collectionItems.Concat (this.emptyItems);
			return CollectionAccessor.GetTemplate (items, name, index);
		}

		private IEnumerable<SummaryData> GetItems()
		{
			lock (this.SyncObject)
			{
				var itemNames = new HashSet<string> ();
				return new List<SummaryData> (this.staticItems.Concat (this.collectionItems.Where (x => itemNames.Add (x.Name))).Concat (this.emptyItems.Where (x => itemNames.Add (x.Name + ".0"))));
			}
		}


		#region IEnumerable<SummaryData> Members

		public IEnumerator<SummaryData> GetEnumerator()
		{
			return this.GetItems ().GetEnumerator ();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetItems ().GetEnumerator ();
		}

		#endregion

		private readonly object exclusion = new object ();

		private readonly List<SummaryData> staticItems;
		private readonly List<SummaryData> emptyItems;
		private readonly List<SummaryData> collectionItems;
		private readonly List<CollectionAccessor> collectionAccessors;
	}
}
