﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

namespace Epsitec.Cresus.Assets.App.Widgets
{
	/// <summary>
	/// Surcouche interactive pour TreeTable s'occupant du déplacement d'un élément quelconque.
	/// </summary>
	public abstract class AbstractInteractiveLayer
	{
		public AbstractInteractiveLayer(TreeTable treeTable)
		{
			this.treeTable = treeTable;
		}


		public void CreateUI()
		{
			this.foreground = new Foreground
			{
				Parent  = this.treeTable,
				Anchor  = AnchorStyles.All,
				Margins = new Margins (0, 0, 0, AbstractScroller.DefaultBreadth),
			};
		}


		public bool IsDragging
		{
			get
			{
				return this.isDragging;
			}
		}

		public virtual void BeginDrag(Point pos)
		{
		}

		public virtual void ProcessDrag(Point pos)
		{
		}

		public virtual void EndDrag(Point pos)
		{
		}


		protected void ClearForeground()
		{
			this.foreground.ClearZones ();
			this.foreground.Invalidate ();
		}


		protected FrameBox LeftContainer
		{
			get
			{
				return this.treeTable.LeftContainer;
			}
		}

		protected Scrollable ColumnsContainer
		{
			get
			{
				return this.treeTable.ColumnsContainer;
			}
		}

		protected int HeaderHeight
		{
			get
			{
				return this.treeTable.HeaderHeight;
			}
		}


		protected Rectangle GetSeparatorRect(double x, int thickness)
		{
			x = System.Math.Floor (x);
			return new Rectangle (x-thickness, 0, thickness*2+1, this.foreground.ActualHeight);
		}

		protected double? GetSeparatorX(int rank)
		{
			//	Retourne la position d'un frontière. S'il existe n colonnes, on peut
			//	obtenir les positions 0..n (0 = tout à gauche, n = tout à droite).
			if (rank != -1)
			{
				if (rank == 0)  // tout à gauche ?
				{
					var column = this.GetColumn (0);
					return column.ActualBounds.Left;
				}
				else  // cherche une frontière droite ?
				{
					rank--;  // 0..n-1
					var column = this.GetColumn (rank);

					if (column.DockToLeft)
					{
						return column.ActualBounds.Right;
					}
					else
					{
						double offset = this.ColumnsContainer.ViewportOffsetX;
						double position = column.ActualBounds.Right;

						if (position > offset)
						{
							var x = this.ColumnsContainer.ActualBounds.Left - offset + position;

							if (rank == this.ColumnCount-1)  // dernière colonne ?
							{
								x -= 2;  // pour ne pas être sous l'ascenseur vertical
							}

							return x;
						}
					}
				}
			}

			return null;
		}

		protected int ColumnCount
		{
			get
			{
				return this.treeTable.TreeTableColumns.Count;
			}
		}

		protected AbstractTreeTableColumn GetColumn(int rank)
		{
			return this.treeTable.TreeTableColumns[rank];
		}


		protected readonly TreeTable			treeTable;

		protected Foreground					foreground;
		protected bool							isDragging;
	}
}