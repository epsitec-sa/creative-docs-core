//	Copyright � 2012, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support.Extensions;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Common.BigList
{
	public abstract class ItemList
	{
		protected ItemList()
		{
			this.visibleRows = new List<ItemListRow> ();
			this.SelectionMode = ItemSelectionMode.ExactlyOne;
		}


		public ItemSelectionMode SelectionMode
		{
			get;
			set;
		}

		public abstract int Count
		{
			get;
		}

		public int ActiveIndex
		{
			get
			{
				return this.activeIndex;
			}
			set
			{
				if (this.activeIndex != value)
				{
					this.SetActiveIndex (value);
				}
			}
		}

		public int VisibleIndex
		{
			get
			{
				return this.visibleIndex;
			}
			set
			{
				var count = this.Count;

				if ((value >= count) &&
					(value > 0))
				{
					value = count-1;
				}

				if (this.visibleIndex != value)
				{
					this.SetVisibleIndex (value);
				}
			}
		}

		public int VisibleOffset
		{
			get
			{
				return this.visibleOffset;
			}
		}

		public int VisibleHeight
		{
			get
			{
				return this.visibleHeight;
			}
			set
			{
				if (this.visibleHeight != value)
				{
					this.visibleHeight = value;
					this.MoveVisibleContent (0);
				}
			}
		}

		public int VisibleCount
		{
			get
			{
				return this.visibleRows.Count;
			}
		}

		public abstract ItemCache Cache
		{
			get;
		}

		public IList<ItemListRow> VisibleRows
		{
			get
			{
				return this.visibleRows.AsReadOnly ();
			}
		}


		public void Reset()
		{
			this.ResetCache ();
			this.SetVisibleIndex (0);
		}

		public void Select(int index, ItemSelection selection)
		{
			if ((index < 0) ||
				(index >= this.Count))
			{
				throw new System.IndexOutOfRangeException (string.Format ("Index {0} out of range", index));
			}

			switch (this.SelectionMode)
			{
				case ItemSelectionMode.ExactlyOne:
					if (selection == ItemSelection.Deselect)
					{
						return;
					}
					selection = ItemSelection.Select;
					break;

				case ItemSelectionMode.None:
					return;
			}

			if (this.ChangeFlagState (index, x => x.Select (selection)))
			{
				//	Process state change. We might need to deselect/reselect others.

				this.ChangeSelection (index);
			}
		}

		public bool IsSelected(int index)
		{
			return this.Cache.GetItemState (index, ItemStateDetails.Flags).Selected;
		}

		public void MoveVisibleContent(int distance)
		{
			var rows = this.GetVisibleRowsStartingWith (this.visibleIndex, this.visibleOffset + distance);
			var row  = rows.FirstOrDefault ();

			if (row == null)
			{
				this.visibleIndex  = 0;
				this.visibleOffset = 0;
				this.visibleRows   = rows;
			}
			else
			{
				this.visibleIndex  = row.Index;
				this.visibleOffset = row.Offset;
				this.visibleRows   = rows;
			}
		}


		public ItemState GetItemState(int index)
		{
			return this.Cache.GetItemState (index, ItemStateDetails.Full);
		}

		public int GetItemHeight(int index)
		{
			return this.Cache.GetItemHeight (index);
		}

		public void SetItemState(int index, ItemState state)
		{
			this.Cache.SetItemState (index, state, ItemStateDetails.Full);
		}

		public void SetItemHeight(int index, int height)
		{
			var state = this.GetItemState (index);
			state.Height = height;
			this.SetItemState (index, state);
		}


		protected void ResetCache()
		{
			this.Cache.Reset ();
		}



		private void ChangeSelection(int index)
		{
			switch (this.SelectionMode)
			{
				case ItemSelectionMode.ExactlyOne:
					this.DeselectAll ();
					this.SelectOne (index);
					break;
				
				case ItemSelectionMode.Multiple:
					break;
				
				case ItemSelectionMode.OneOrMore:
					if (this.HasSelectedItems () == false)
					{
						this.SelectOne (index);
					}
					break;
				
				case ItemSelectionMode.ZeroOrOne:
					if (this.IsSelected (index))
					{
						this.DeselectAll ();
						this.SelectOne (index);
					}
					break;

				case ItemSelectionMode.None:
					break;
			}
		}


		private void DeselectAll()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this.ChangeFlagState (i, x => x.Select (ItemSelection.Deselect));
			}
		}

		private void SelectOne(int index)
		{
			this.ChangeFlagState (index, x => x.Select (ItemSelection.Select));
		}

		private bool HasSelectedItems()
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this.IsSelected (i))
				{
					return true;
				}
			}
			
			return false;
		}

		private IEnumerable<int> EnumerateIndexes()
		{
			for (int i = 0; i < this.Count; i++)
			{
				yield return i;
			}
		}

		private void SetActiveIndex(int index)
		{
			this.activeIndex = index;
		}

		private void SetVisibleIndex(int index)
		{
			if (this.visibleHeight == 0)
			{
				this.visibleIndex  = 0;
				this.visibleOffset = 0;
				this.visibleRows   = new List<ItemListRow> ();

				return;
			}


			var rows = this.visibleRows;
			var row  = rows.Find (x => x.Index == index);

			if (row == null)
			{
				if (index > this.visibleIndex)
				{
					rows = this.GetVisibleRowsEndingWith (index);
					row  = rows.Find (x => x.Index == index);
				}
				else
				{
					rows = this.GetVisibleRowsStartingWith (index);
					row  = rows.Find (x => x.Index == index);
				}
			}

			int offset = 0;

			if (row != null)
			{
				if (row.Offset < 0)
				{
					rows = this.GetVisibleRowsStartingWith (index);
					row  = rows.Find (x => x.Index == index);
				}
				else if (row.Offset + row.Height > this.visibleHeight)
				{
					rows = this.GetVisibleRowsEndingWith (index);
					row  = rows.Find (x => x.Index == index);
				}

				offset = row.Offset;
			}

			this.visibleRows   = rows;
			this.visibleIndex  = index;
			this.visibleOffset = offset;
		}

		private bool GetFlagState(int index, System.Predicate<ItemState> predicate)
		{
			return predicate (this.Cache.GetItemState (index, ItemStateDetails.Flags));
		}

		private bool ChangeFlagState(int index, System.Action<ItemState> action)
		{
			var state = this.Cache.GetItemState (index, ItemStateDetails.Flags);
			var copy  = state.Clone ();

			action (copy);

			this.Cache.SetItemState (index, copy, ItemStateDetails.Flags);

			return !state.Equals (copy);
		}

		private List<ItemListRow> GetVisibleRowsStartingWith(int index, int startOffset = 0)
		{
			var rows  = new List<ItemListRow> ();
			int count = this.Count;

			if (count == 0)
			{
				return rows;
			}

			while ((startOffset > 0) && (index > 0))
			{
				startOffset -= this.GetItemHeight (--index);
			}

			if (index < 0)
			{
				index = 0;
				startOffset = 0;
			}
			if (index >= count)
			{
				index = count-1;
			}

			int start  = index;
			int offset = startOffset < 0 ? startOffset : 0;

			//	Assign each row an offset and a height, until we fill all the available space.

			while (offset < this.visibleHeight)
			{
				if (index >= count)
				{
					//	We could not fill the available space.

					if (start == 0)
					{
						//	We already started at the beginning of the collection, stop here;
						//	this will result in an incomplete list of rows. We can't do better.

						break;
					}

					//	Since we did not start at the beginning of the collection, try to
					//	generate a list with the last item of the collection aligned with the
					//	end of the available space:

					return this.GetVisibleRowsEndingWith (count-1);
				}

				int height = this.GetItemHeight (index);

				if ((height > 0) && (offset + height > 0))
				{
					rows.Add (new ItemListRow (index, offset, height));
				}

				offset += height;
				index  += 1;
			}

			return rows;
		}

		private List<ItemListRow> GetVisibleRowsEndingWith(int index)
		{
			var rows  = new List<ItemListRow> ();
			int count = this.Count;

			if (count == 0)
			{
				return rows;
			}
			if (index < 0)
			{
				index = 0;
			}
			if (index >= count)
			{
				index = count-1;
			}

			int offset = this.visibleHeight;
			int total  = 0;

			//	Assign each row an offset and a height, until we fill all the available space.

			while ((index >= 0) && (offset > 0))
			{
				int height = this.GetItemHeight (index);

				offset -= height;

				if ((offset <= 0) &&
					(rows.Count == 0))
				{
					//	Special case: there is just one item, which does not fit into the
					//	available space. Position it so that the end of the item will be
					//	visible.

					rows.Add (new ItemListRow (index, 0, height));
					return rows;
				}

				if (height > 0)
				{
					rows.Insert (0, new ItemListRow (index, offset, height));
				}

				total += height;
				index -= 1;
			}

			if ((total < this.visibleHeight) &&
				(rows.Count > 0))
			{
				//	There were not enough items to fill the available space. Shift the rows
				//	so that the first is aligned with the starting offset...

				rows = new List<ItemListRow> (ItemList.ShiftRows (rows, total - this.visibleHeight));

				var last = rows.Last ();

				offset = total;
				index  = last.Index + 1;

				//	...and fill the list with as many items as possible.

				while ((total < this.visibleHeight) && (index < count))
				{
					int height = this.GetItemHeight (index);

					if (height > 0)
					{
						rows.Add (new ItemListRow (index, offset, height));
					}

					offset += height;
					index  += 1;
				}
			}

			return rows;
		}


		private static IEnumerable<ItemListRow> ShiftRows(IEnumerable<ItemListRow> rows, int offset)
		{
			return rows.Select (x => new ItemListRow (x.Index, x.Offset + offset, x.Height));
		}


		private List<ItemListRow>				visibleRows;
		private int								activeIndex;
		private int								visibleIndex;
		private int								visibleOffset;
		private int								visibleHeight;
	}
}