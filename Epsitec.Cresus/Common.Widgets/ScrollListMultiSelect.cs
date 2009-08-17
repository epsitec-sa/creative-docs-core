//	Copyright � 2003-2009, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Drawing;
using Epsitec.Common.Support;
using Epsitec.Common.Types;

using System.Collections.Generic;

namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// The <c>ScrollListMultiselect</c> class implements a <see cref="ScrollList"/>
	/// with support for multiple line selection.
	/// </summary>
	public class ScrollListMultiSelect : ScrollList
	{
		public ScrollListMultiSelect()
		{
			this.dragSelectionStartIndex = NoSelection;
			this.selection = new HashSet<int> ();
		}

		public ScrollListMultiSelect(Widget embedder)
			: this ()
		{
			this.SetEmbedder (embedder);
		}


		public void AddSelection(IEnumerable<int> selection)
		{
			bool dirty = false;

			foreach (var index in selection)
			{
				if (this.selection.Add (index))
				{
					dirty = true;
				}
			}

			if (dirty)
			{
				this.OnMultiSelectionChanged ();
			}
		}

		public void RemoveSelection(IEnumerable<int> selection)
		{
			bool dirty = false;

			foreach (var index in selection)
			{
				if (this.selection.Remove (index))
				{
					dirty = true;
				}
			}

			if (dirty)
			{
				this.OnMultiSelectionChanged ();
			}
		}

		public void ClearSelection()
		{
			if (this.selection.Count > 0)
			{
				this.selection.Clear ();
				this.OnMultiSelectionChanged ();
			}
		}


		public override bool IsItemSelected(int index)
		{
			if (this.dragSelectionStartIndex >= 0)
			{
				int begin = System.Math.Min (this.dragSelectionStartIndex, this.SelectedIndex);
				int end   = System.Math.Max (this.dragSelectionStartIndex, this.SelectedIndex);

				if ((index >= begin) &&
					(index <= end))
				{
					return true;
				}
			}

			if (this.selection.Contains (index))
			{
				return true;
			}

			return base.IsItemSelected (index);
		}

		protected override void MouseSelectBegin()
		{
			base.MouseSelectBegin ();
			this.dragSelectionStartIndex = EmptySelection;

			this.OnDragMultiSelectionStarted (new MultiSelectEventArgs ());
		}

		protected override void MouseSelectEnd()
		{
			base.MouseSelectEnd ();
			
			if (this.dragSelectionStartIndex != this.SelectedIndex)
			{
				//	The user dragged a multiline selection, which will trigger an appropriate
				//	notification event.

				int begin = System.Math.Min (this.SelectedIndex, this.dragSelectionStartIndex);
				int end   = System.Math.Max (this.SelectedIndex, this.dragSelectionStartIndex);

				this.OnDragMultiSelectionEnded (new MultiSelectEventArgs (begin, end));
			}

			this.dragSelectionStartIndex = NoSelection;
		}

		protected override void MouseSelectLine(int index)
		{
			if (this.dragSelectionStartIndex == EmptySelection)
			{
				this.dragSelectionStartIndex = index;
			}

			base.MouseSelectLine (index);
		}


		protected void OnMultiSelectionChanged()
		{
			this.Invalidate ();

			var handler = this.GetUserEventHandler<DependencyPropertyChangedEventArgs> (ScrollListMultiSelect.MultiSelectionChangedEvent);
			var e = new DependencyPropertyChangedEventArgs ("MultiSelection");

			if (handler != null)
			{
				handler (this, e);
			}
		}

		protected void OnDragMultiSelectionStarted(MultiSelectEventArgs e)
		{
			var handler = this.GetUserEventHandler<MultiSelectEventArgs> (ScrollListMultiSelect.DragMultiSelectionStartedEvent);

			if (handler != null)
			{
				handler (this, e);
			}
		}

		protected void OnDragMultiSelectionEnded(MultiSelectEventArgs e)
		{
			var handler = this.GetUserEventHandler<MultiSelectEventArgs> (ScrollListMultiSelect.DragMultiSelectionEndedEvent);

			if (handler != null)
			{
				handler (this, e);
			}
		}


		public event Support.EventHandler<DependencyPropertyChangedEventArgs> MultiSelectionChanged
		{
			add
			{
				this.AddUserEventHandler (ScrollListMultiSelect.MultiSelectionChangedEvent, value);
			}
			remove
			{
				this.RemoveUserEventHandler (ScrollListMultiSelect.MultiSelectionChangedEvent, value);
			}
		}

		public event Support.EventHandler<MultiSelectEventArgs> DragMultiSelectionStarted
		{
			add
			{
				this.AddUserEventHandler (ScrollListMultiSelect.DragMultiSelectionStartedEvent, value);
			}
			remove
			{
				this.RemoveUserEventHandler (ScrollListMultiSelect.DragMultiSelectionStartedEvent, value);
			}
		}

		public event Support.EventHandler<MultiSelectEventArgs> DragMultiSelectionEnded
		{
			add
			{
				this.AddUserEventHandler (ScrollListMultiSelect.DragMultiSelectionEndedEvent, value);
			}
			remove
			{
				this.RemoveUserEventHandler (ScrollListMultiSelect.DragMultiSelectionEndedEvent, value);
			}
		}


		private const string DragMultiSelectionEndedEvent	= "DragMultiSelectionEnded";
		private const string DragMultiSelectionStartedEvent = "DragMultiSelectionStarted";
		private const string MultiSelectionChangedEvent		= "MultiSelectionChanged";
		
		private const int NoSelection    = -2;
		private const int EmptySelection = -1;

		private int dragSelectionStartIndex;
		private readonly HashSet<int> selection;
	}
}
