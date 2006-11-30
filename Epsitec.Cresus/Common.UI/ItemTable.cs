//	Copyright � 2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;
using Epsitec.Common.Types.Collections;
using Epsitec.Common.UI;
using Epsitec.Common.Widgets;

using System.Collections.Generic;

[assembly: DependencyClass (typeof (ItemTable))]

namespace Epsitec.Common.UI
{
	/// <summary>
	/// The <c>ItemTable</c> class is used to represent items in a table, with
	/// column headers and scroll bars.
	/// </summary>
	public class ItemTable : Widgets.FrameBox, IListHost<ItemTableColumn>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ItemTable"/> class.
		/// </summary>
		public ItemTable()
		{
			this.vScroller = new VScroller (this);
			this.hScroller = new HScroller (this);
			this.surface = new Widget (this);
			this.headerStripe = new Widget (this);
			this.columnHeader = new ItemPanelColumnHeader (this.headerStripe);
			this.itemPanel = new ItemPanel (this.surface);

			this.vScroller.IsInverted = false;
			this.vScroller.Value = 1;
			
			this.vScroller.Anchor = AnchorStyles.TopAndBottom | AnchorStyles.Right;
			this.hScroller.Anchor = AnchorStyles.LeftAndRight | AnchorStyles.Bottom;
			this.headerStripe.Anchor = AnchorStyles.LeftAndRight | AnchorStyles.Top;
			this.surface.Anchor = AnchorStyles.All;
			
			this.vScroller.Margins = new Drawing.Margins (0, 0, 0, this.hScroller.PreferredHeight);
			this.hScroller.Margins = new Drawing.Margins (0, this.vScroller.PreferredWidth, 0, 0);
			this.headerStripe.Margins = new Drawing.Margins (0, this.vScroller.PreferredWidth, 0, 0);
			this.headerStripe.PreferredHeight = this.columnHeader.PreferredHeight;
			this.surface.Margins = new Drawing.Margins (0, this.vScroller.PreferredWidth, this.headerStripe.PreferredHeight, this.hScroller.PreferredHeight);

			this.hScroller.ValueChanged += this.HandleScrollerValueChanged;
			this.vScroller.ValueChanged += this.HandleScrollerValueChanged;
			this.surface.SizeChanged += this.HandleSurfaceSizeChanged;
			
			this.itemPanel.AddEventHandler (Visual.PreferredHeightProperty, this.HandleItemPanelSizeChanged);
			this.itemPanel.AddEventHandler (Visual.PreferredWidthProperty, this.HandleItemPanelSizeChanged);

			this.itemPanel.Layout = ItemPanelLayout.VerticalList;
			this.itemPanel.ItemSelection = ItemPanelSelectionMode.ExactlyOne;
			this.itemPanel.GroupSelection = ItemPanelSelectionMode.None;

			this.columnHeader.ItemPanel = this.itemPanel;

			//	Link the item panel with its table, so that an ItemViewFactory
			//	can find the table and the column templates, if it needs to do
			//	so :
			
			ItemTable.SetItemTable (this.itemPanel, this);
		}

		public ItemTable(Widget embedder)
			: this ()
		{
			this.SetEmbedder (embedder);
		}

		public ICollectionView					Items
		{
			get
			{
				return this.ItemPanel.Items;
			}
			set
			{
				this.ItemPanel.Items = value;
			}
		}

		public ItemPanel						ItemPanel
		{
			get
			{
				return this.itemPanel;
			}
		}

		public ItemPanelColumnHeader			ColumnHeader
		{
			get
			{
				return this.columnHeader;
			}
		}

		public StructuredType					SourceType
		{
			get
			{
				return this.sourceType;
			}
			set
			{
				if (this.sourceType != value)
				{
					this.sourceType = value;
					this.UpdateColumnHeader ();
				}
			}
		}

		public Collections.ItemTableColumnCollection Columns
		{
			get
			{
				if (this.columns == null)
				{
					this.columns = new Collections.ItemTableColumnCollection (this);
				}
				
				return this.columns;
			}
		}

		public void DefineDefaultColumns(StructuredType sourceType, double width)
		{
			try
			{
				this.suspendColumnUpdates++;
				this.SourceType = sourceType;
				this.Columns.Clear ();
				
				foreach (string fieldId in sourceType.GetFieldIds ())
				{
					this.Columns.Add (new ItemTableColumn (fieldId, width));
				}
			}
			finally
			{
				this.suspendColumnUpdates--;
			}

			this.UpdateColumnHeader ();
		}
		
		private void UpdateAperture(Drawing.Size aperture)
		{
			double hRatio = System.Math.Max (0, System.Math.Min (1, (aperture.Width+1) / (this.itemPanel.PreferredWidth+1)));
			double vRatio = System.Math.Max (0, System.Math.Min (1, (aperture.Height+1) / (this.itemPanel.PreferredHeight+1)));

			this.hScroller.VisibleRangeRatio = (decimal) hRatio;
			this.vScroller.VisibleRangeRatio = (decimal) vRatio;

			double ox = (double) this.hScroller.Value * System.Math.Max (0, this.itemPanel.PreferredWidth - aperture.Width);
			double oy = (double) this.vScroller.Value * System.Math.Max (0, this.itemPanel.PreferredHeight - aperture.Height);
			
			this.itemPanel.Aperture = new Drawing.Rectangle (ox, oy, aperture.Width, aperture.Height);

			if (this.itemPanel.PreferredHeight < aperture.Height)
			{
				oy = this.itemPanel.PreferredHeight - aperture.Height;
			}
			
			this.itemPanel.SetManualBounds (new Drawing.Rectangle (-ox, -oy, this.itemPanel.PreferredWidth, this.itemPanel.PreferredHeight));
			this.columnHeader.SetManualBounds (new Drawing.Rectangle (-ox, 0, this.columnHeader.GetTotalWidth (), this.columnHeader.PreferredHeight));
		}

		private void UpdateColumnHeader()
		{
			if ((this.suspendColumnUpdates == 0) &&
				(this.columns != null) &&
				(this.columns.Count > 0) &&
				(this.sourceType != null))
			{
				this.columnHeader.ClearColumns ();

				for (int i = 0; i < this.columns.Count; i++)
				{
					ItemTableColumn column = this.columns[i];
					
					if (string.IsNullOrEmpty (column.FieldId))
					{
						//	There is no field defined for this column. This means
						//	that the contents of the column will default to the
						//	full item itself.
						
						Support.Druid captionId = column.CaptionId;

						if (captionId.IsValid)
						{
							this.columnHeader.AddColumn (i, null, captionId);
						}
					}
					else
					{
						StructuredTypeField field = this.sourceType.GetField (column.FieldId);
						
						if (field.IsEmpty)
						{
							//	Ignore unknown columns...
						}
						else
						{
							Support.Druid captionId = column.CaptionId;

							if (captionId.IsEmpty)
							{
								this.columnHeader.AddColumn (i, field);
							}
							else
							{
								this.columnHeader.AddColumn (i, field.Id, captionId);
							}
						}
					}
				}
			}
		}

		private void HandleSurfaceSizeChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.UpdateAperture ((Drawing.Size) e.NewValue);
		}

		private void HandleScrollerValueChanged(object sender)
		{
			if (this.surface.IsActualGeometryValid)
			{
				this.UpdateAperture (this.surface.ActualSize);
			}
		}

		private void HandleItemPanelSizeChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (this.surface.IsActualGeometryValid)
			{
				this.UpdateAperture (this.surface.ActualSize);
			}
		}

		#region IListHost<ItemTableColumn> Members

		HostedList<ItemTableColumn> IListHost<ItemTableColumn>.Items
		{
			get
			{
				return this.Columns;
			}
		}

		void IListHost<ItemTableColumn>.NotifyListInsertion(ItemTableColumn item)
		{
			this.UpdateColumnHeader ();
		}

		void IListHost<ItemTableColumn>.NotifyListRemoval(ItemTableColumn item)
		{
			this.UpdateColumnHeader ();
		}

		#endregion


		public static void SetItemTable(DependencyObject obj, ItemTable value)
		{
			if (value == null)
			{
				obj.ClearValue (ItemTable.ItemTableProperty);
			}
			else
			{
				obj.SetValue (ItemTable.ItemTableProperty, value);
			}
		}

		public static ItemTable GetItemTable(DependencyObject obj)
		{
			return (ItemTable) obj.GetValue (ItemTable.ItemTableProperty);
		}
		
		public static readonly DependencyProperty ItemTableProperty = DependencyProperty.RegisterAttached ("ItemTable", typeof (ItemTable), typeof (ItemTable));

		private VScroller vScroller;
		private HScroller hScroller;
		private ItemPanelColumnHeader columnHeader;
		private Collections.ItemTableColumnCollection columns;
		private Widget surface;
		private Widget headerStripe;
		private ItemPanel itemPanel;
		private StructuredType sourceType;
		private int suspendColumnUpdates;
	}
}
