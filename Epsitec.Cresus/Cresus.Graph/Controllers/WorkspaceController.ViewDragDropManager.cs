﻿//	Copyright © 2009, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Drawing;
using Epsitec.Common.Graph;
using Epsitec.Common.Graph.Data;
using Epsitec.Common.Graph.Renderers;
using Epsitec.Common.Graph.Styles;
using Epsitec.Common.Graph.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Support.Extensions;
using Epsitec.Common.Types;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Graph.Widgets;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Graph.Controllers
{
	internal partial class WorkspaceController
	{
		class ViewDragDropManager
		{
			public ViewDragDropManager(GraphDataSeries item, MiniChartView view, WorkspaceController host, Point mouse)
			{
				this.item = item;
				this.view = view;
				this.host = host;
				this.viewMouseCursor = this.view.MouseCursor;
				
				this.host.CloseBalloonTip ();
				
				this.viewOrigin  = view.MapClientToScreen (new Point (0, 0));
				this.mouseOrigin = mouse;

				this.groupIndex = -1;
				this.outputIndex = -1;
				
				this.outputInsertionMark = new Separator ()
				{
					Parent = this.host.outputItemsController.Container,
					Visibility = false,
					Anchor = AnchorStyles.TopAndBottom | AnchorStyles.Left,
					PreferredWidth = 2,
				};

				this.groupInsertionMark = new Separator ()
				{
					Parent = this.host.groupItemsController.Container,
					Visibility = false,
					Anchor = AnchorStyles.LeftAndRight | AnchorStyles.Top,
					PreferredHeight = 2,
				};
			}

			public bool LockX
			{
				get;
				set;
			}

			public bool LockY
			{
				get;
				set;
			}

			public DragWindow DragWindow
			{
				get;
				private set;
			}

			public bool ProcessMouseMove(Point mouse, System.Action dragStartAction)
			{
				mouse = this.view.MapClientToScreen (mouse);
				
				if (this.DragWindow == null)
				{
					if (Point.Distance (mouse, this.mouseOrigin) > 2.0)
					{
						this.CreateDragWindow ();
					}

					dragStartAction ();
				}

				if (this.DragWindow != null)
				{
					double x = this.viewOrigin.X + (this.LockX ? 0 : mouse.X - this.mouseOrigin.X);
					double y = this.viewOrigin.Y + (this.LockY ? 0 : mouse.Y - this.mouseOrigin.Y);

					this.DragWindow.WindowLocation = new Point (x, y);
					
					if ((this.DetectOutput (mouse)) ||
						(this.DetectGroup (mouse)))
					{
						return true;
					}

					this.host.RefreshHints ();
				}
				
				return false;
			}

			public bool ProcessDragEnd()
			{
				bool ok = this.DragWindow != null;

				if (this.DragWindow != null)
				{
					this.DragWindow.Dispose ();
					this.DragWindow = null;
				}

				if (this.outputInsertionMark != null)
				{
					this.outputInsertionMark.Dispose ();
					this.outputInsertionMark = null;
				}

				if (this.groupInsertionMark != null)
				{
					this.groupInsertionMark.Dispose ();
					this.groupInsertionMark = null;
				}

				if (this.outputIndex >= 0)
				{
					if (!this.host.Document.SetOutputIndex (this.item, this.outputIndex))
					{
						this.host.AddOutputToDocument (this.item);
						this.host.Document.SetOutputIndex (this.item, this.outputIndex);
					}

					this.host.Refresh ();
				}
				else if (this.groupIndex >= 0)
				{
					var groups = this.host.Document.Groups;

					if ((this.groupIndex < groups.Count) &&
						(this.groupUpdate))
					{
						this.host.UpdateGroup (groups[this.groupIndex], Collection.Single (this.item));
					}
					else
					{
						this.host.CreateGroup (Collection.Single (this.item));
					}
					
					this.host.Refresh ();
				}

				this.view.Enable = true;
				this.view.MouseCursor = this.viewMouseCursor;
				this.view.ClearUserEventHandlers (Widget.EventNames.MouseMoveEvent);

				return ok;
			}

			private bool DetectOutput(Point screenMouse)
			{
				var container = this.host.outputItemsController.Container;
				var mouse     = container.MapScreenToClient (screenMouse);

				if (container.Client.Bounds.Contains (mouse))
				{
					//	If the target is empty, accept to drop independently of the position :

					if (this.host.outputItemsController.Count == 0)
					{
						this.SetOutputDropTarget (2, 0);
						return true;
					}

					//	Otherwise, find the best possible match, but discard dropping on the item
					//	itself, or between it and its immediate neighbours :

					var dist = this.host.outputItemsController.Select (x => new
					{
						Distance = mouse.X - x.ActualBounds.Center.X,
						View = x
					});

					System.Diagnostics.Debug.WriteLine (string.Join (", ", dist.Select (x => string.Format ("{0}:{1}", x.View.Index, x.Distance)).ToArray ()));

					var best = dist.OrderBy (x => System.Math.Abs (x.Distance)).FirstOrDefault ();

					if ((best != null) &&
						(best.View != this.view))
					{
						if ((best.Distance < 0) &&
							((best.View.Index != this.view.Index+1) || !this.host.Document.OutputSeries.Contains (this.item)))
						{
							this.SetOutputDropTarget (best.View.ActualBounds.Left - this.outputInsertionMark.PreferredWidth + 3, best.View.Index);
							return true;
						}
						if ((best.Distance >= 0) &&
						    ((best.View.Index != this.view.Index-1) || !this.host.Document.OutputSeries.Contains (this.item)))
						{
							this.SetOutputDropTarget (best.View.ActualBounds.Right - 3, best.View.Index+1);
							return true;
						}
					}
				}
				else if (this.host.outputPageFrame.Parent.Client.Bounds.Contains (this.host.outputPageFrame.MapScreenToParent (screenMouse)))
				{
					int n = this.host.outputItemsController.Count;

					this.SetOutputDropTarget (n > 0 ? this.host.outputItemsController.Last ().ActualBounds.Right - 3 : 2, n);
					
					return true;
				}
				
				this.outputInsertionMark.Visibility = false;
				this.outputIndex = -1;
				
				return false;
			}

			private void SetOutputDropTarget(double x, int index)
			{
				this.outputInsertionMark.Margins = new Margins (x, 0, 0, 0);
				this.outputInsertionMark.Visibility = true;
				
				this.outputIndex = index;
				
				this.host.outputItemsHint.Visibility = false;
			}

			private void SetGroupDropTarget(int index, bool update, double y, double dy)
			{
				this.groupInsertionMark.Margins = new Margins (0, 0, y, 0);
				this.groupInsertionMark.Visibility = dy > 0;
				this.groupInsertionMark.PreferredHeight = dy;
				this.groupInsertionMark.ZOrder = this.groupInsertionMark.Parent.Children.Count - 1;
				
				this.groupIndex = index;
				this.groupUpdate = update;

				this.host.groupItemsHint.Visibility = false;
			}


			private bool DetectGroup(Point screenMouse)
			{
				var container = this.host.groupItemsController.Container;
				var mouse     = container.MapScreenToClient (screenMouse);

				if (container.Client.Bounds.Contains (mouse))
				{
					if (this.host.groupItemsController.Count == 0)
					{
						this.SetGroupDropTarget (0, false, 0.0, 2.0);
						return true;
					}

					var drop = this.host.groupItemsController.Where (x => x.HitTest (mouse)).FirstOrDefault ();

					if (drop != null)
					{
						double y = drop.Parent.ActualHeight - drop.ActualBounds.Top;
						this.SetGroupDropTarget (drop.Index, true, y, drop.ActualBounds.Height);
						return true;
					}
					
					var dist = this.host.groupItemsController.Select (x => new
					{
						Distance = mouse.Y - x.ActualBounds.Center.Y,
						View = x
					});

					System.Diagnostics.Debug.WriteLine (string.Join (", ", dist.Select (x => string.Format ("{0}:{1}", x.View.Index, x.Distance)).ToArray ()));

					var best = dist.Where (x => x.Distance >= 0).OrderBy (x => System.Math.Abs (x.Distance)).FirstOrDefault ();

					if (best != null)
					{
						double y = best.View.Parent.ActualHeight - best.View.ActualBounds.Top - 2; 
						this.SetGroupDropTarget (best.View.Index, false, y, 2.0);
					}
					else
					{
						double y = this.host.groupItemsController.Container.ActualHeight - this.host.groupItemsController.Last ().ActualBounds.Bottom - 2; 
						this.SetGroupDropTarget (this.host.groupItemsController.Count, false, y, 2.0);
					}

					return true;
				}

				this.groupIndex = -1;

				return false;
			}
			
			private void CreateDragWindow()
			{
				this.DragWindow = new DragWindow ()
				{
					Alpha = 0.6,
					WindowLocation = this.viewOrigin,
					Owner = this.view.Window,
				};

				this.DragWindow.DefineWidget (this.host.CreateView (this.item), this.view.PreferredSize, Margins.Zero);
				this.DragWindow.Show ();
			}

			
			private readonly GraphDataSeries item;
			private readonly MiniChartView view;
			private readonly WorkspaceController host;
			private readonly Point viewOrigin;
			private readonly Point mouseOrigin;
			private readonly MouseCursor viewMouseCursor;
			
			private Separator outputInsertionMark;
			private Separator groupInsertionMark;

			private int outputIndex;
			private int groupIndex;
			private bool groupUpdate;
		}
	}
}
