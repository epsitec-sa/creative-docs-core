﻿//	Copyright © 2008, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Dialogs;
using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.UI;
using Epsitec.Common.Widgets;

using System.Collections.Generic;

[assembly: DependencyClass (typeof (HintListController))]

namespace Epsitec.Common.Dialogs
{
	/// <summary>
	/// The <c>HintListWidget</c> class represents a hint list using a dedicated
	/// item panel.
	/// </summary>
	public sealed class HintListWidget : FrameBox
	{
		public HintListWidget()
		{
			this.itemTable = new ItemTable ()
			{
				Embedder = this,
				Dock = DockStyle.Fill,
				HeaderVisibility = false,
				HorizontalScrollMode = ItemTableScrollMode.None,
				VerticalScrollMode = ItemTableScrollMode.ItemBased
			};
 
			this.BackColor = Drawing.Color.FromName ("White");
		}

		
		public ICollectionView Items
		{
			get
			{
				return this.items;
			}
			set
			{
				if (this.items != value)
				{
					if (this.items != null)
					{
						this.items.CurrentChanged -= this.HandleItemsCurrentChanged;
					}

					this.items = value;
					this.itemTable.Items = value;

					if (this.items != null)
					{
						this.items.CurrentChanged += this.HandleItemsCurrentChanged;
					}
				}
			}
		}

		private void HandleItemsCurrentChanged(object sender)
		{
			this.OnCurrentItemChanged ();
		}

		private void OnCurrentItemChanged()
		{
			if (this.CurrentItemChanged != null)
			{
				this.CurrentItemChanged (this);
			}
		}


		public event EventHandler CurrentItemChanged;

		private readonly ItemTable itemTable;
		private ICollectionView items;
	}
}
