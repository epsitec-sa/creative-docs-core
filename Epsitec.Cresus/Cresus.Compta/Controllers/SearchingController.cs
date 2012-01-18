﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Converters;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Widgets.Tiles;
using Epsitec.Cresus.Core.Library;
using Epsitec.Cresus.Core.Business;

using Epsitec.Cresus.Compta.Entities;

using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta
{
	public class SearchingController
	{
		public SearchingController(SearchingData data, List<ColumnMapper> columnMappers)
		{
			this.data          = data;
			this.columnMappers = columnMappers;

			if (this.data.TabsData.Count == 0)
			{
				this.data.TabsData.Add (new SearchingTabData ());
			}
		}


		public FrameBox CreateUI(FrameBox parent, System.Action searchStartAction, System.Action<int> searchNextAction)
		{
			this.searchStartAction = searchStartAction;
			this.searchNextAction  = searchNextAction;

			var frame = new FrameBox
			{
				Parent          = parent,
				PreferredHeight = 20,
				Dock            = DockStyle.Fill,
			};

			var leftFrame = new FrameBox
			{
				Parent          = frame,
				PreferredHeight = 20,
				Dock            = DockStyle.Left,
			};

			this.middleFrame = new FrameBox
			{
				Parent          = frame,
				PreferredHeight = 20,
				Dock            = DockStyle.Fill,
				Margins         = new Margins (0, 20, 0, 0),
			};

			var rightFrame = new FrameBox
			{
				Parent          = frame,
				PreferredHeight = 20,
				Dock            = DockStyle.Right,
			};

			this.CreateLeftUI   (leftFrame);
			this.CreateRightUI  (rightFrame);
			this.CreateMiddleUI ();

			return frame;
		}

		private void CreateLeftUI(FrameBox parent)
		{
			var header = new FrameBox
			{
				Parent          = parent,
				PreferredHeight = 20,
				Dock            = DockStyle.Top,
			};

			this.searchButtonClear = new GlyphButton
			{
				Parent          = header,
				GlyphShape      = GlyphShape.Close,
				PreferredSize   = new Size (20, 20),
				Dock            = DockStyle.Left,
				Enable          = false,
				Margins         = new Margins (0, 10, 0, 0),
			};

			new StaticText
			{
				Parent          = header,
				Text            = "Rechercher",
				PreferredWidth  = 64,
				PreferredHeight = 20,
				Dock            = DockStyle.Left,
			};

			this.searchButtonClear.Clicked += delegate
			{
				while (this.data.TabsData.Count > 1)
				{
					this.data.TabsData.RemoveAt (1);
				}

				this.data.TabsData[0].Clear ();

				this.CreateMiddleUI ();
				this.searchStartAction ();
			};

			ToolTip.Default.SetToolTip (this.searchButtonClear, "Termine la recherche");
		}

		private void CreateMiddleUI()
		{
			this.middleFrame.Children.Clear ();

			int count = this.data.TabsData.Count;
			for (int i = 0; i < count; i++)
			{
				var controller = new SearchingTabController (this.data.TabsData[i], this.columnMappers);

				var frame = controller.CreateUI (this.middleFrame, this.bigDataInterface, this.searchStartAction, this.AddRemoveAction);
				controller.Index = i;
				controller.AddAction = (i == 0);

				frame.TabIndex = i+1;
				frame.Margins = new Margins (0, 0, 0, (count > 1 && i < count-1) ? 1 : 0);
			}

			this.modeFrame.Visibility = (this.data.TabsData.Count > 1);
		}

		private void CreateRightUI(FrameBox parent)
		{
			this.modeFrame = new FrameBox
			{
				Parent          = parent,
				PreferredHeight = 20,
				Dock            = DockStyle.Fill,
			};

			var footer = new FrameBox
			{
				Parent          = parent,
				PreferredHeight = 20,
				Dock            = DockStyle.Bottom,
			};

			{
				var andButton = new RadioButton
				{
					Parent          = this.modeFrame,
					Text            = "Tous",
					PreferredWidth  = 60,
					ActiveState     = this.data.OrMode ? ActiveState.No : ActiveState.Yes,
					Dock            = DockStyle.Left,
				};

				var orButton = new RadioButton
				{
					Parent          = this.modeFrame,
					Text            = "Au moins un",
					PreferredWidth  = 90,
					ActiveState     = this.data.OrMode ? ActiveState.Yes : ActiveState.No,
					Dock            = DockStyle.Left,
				};

				orButton.ActiveStateChanged += delegate
				{
					this.data.OrMode = orButton.ActiveState == ActiveState.Yes;
					this.searchStartAction ();
				};

				ToolTip.Default.SetToolTip (andButton, "Cherche les données qui répondent à tous les critères");
				ToolTip.Default.SetToolTip (orButton,  "Cherche les données qui répondent à au moins un critère");
			}

			{
				this.searchButtonPrev = new GlyphButton
				{
					Parent          = footer,
					GlyphShape      = Common.Widgets.GlyphShape.TriangleLeft,
					PreferredWidth  = 30,
					PreferredHeight = 20,
					Dock            = DockStyle.Left,
					Enable          = false,
					Margins         = new Margins (0, 0, 0, 0),
				};

				this.searchButtonNext = new GlyphButton
				{
					Parent          = footer,
					GlyphShape      = Common.Widgets.GlyphShape.TriangleRight,
					PreferredWidth  = 30,
					PreferredHeight = 20,
					Dock            = DockStyle.Left,
					Enable          = false,
					Margins         = new Margins (-1, 10, 0, 0),
				};

				this.searchResult = new StaticText
				{
					Parent          = footer,
					PreferredWidth  = 120,
					PreferredHeight = 20,
					Dock            = DockStyle.Left,
					Margins         = new Margins (0, 0, 0, 0),
				};

				this.searchButtonPrev.Clicked += delegate
				{
					this.searchNextAction (-1);
				};

				this.searchButtonNext.Clicked += delegate
				{
					this.searchNextAction (1);
				};

				ToolTip.Default.SetToolTip (this.searchButtonPrev, "Cherche en arrière");
				ToolTip.Default.SetToolTip (this.searchButtonNext, "Cherche en avant");
			}
		}


		public void SetSearchingCount(int dataCount, int? count)
		{
			this.BigDataInterface = (dataCount >= 1000);  // limite arbitraire au-delà de laquelle les recherches deviennent trop lentes !

			this.UpdateButtons ();

			this.searchButtonNext.Enable = (count > 1);
			this.searchButtonPrev.Enable = (count > 1);

			if (!count.HasValue)
			{
				this.searchResult.Text = null;
			}
			else if (count == 0)
			{
				this.searchResult.Text = "Aucun résultat trouvé";
			}
			else
			{
				int c = count.Value;
				this.searchResult.Text = string.Format ("{0} resultat{1}", c.ToString (), (c == 1) ? "" : "s");
			}
		}


		private void AddRemoveAction(int index)
		{
			if (index == 0)
			{
				this.data.TabsData.Add (new SearchingTabData ());
			}
			else
			{
				this.data.TabsData.RemoveAt (index);
			}

			this.CreateMiddleUI ();
			this.UpdateButtons ();
			this.searchStartAction ();
		}

		private void UpdateButtons()
		{
			this.searchButtonClear.Enable = !this.data.IsEmpty || this.data.TabsData.Count > 1;
		}


		private bool BigDataInterface
		{
			get
			{
				return this.bigDataInterface;
			}
			set
			{
				if (this.bigDataInterface != value)
				{
					this.bigDataInterface = value;

					this.CreateMiddleUI ();
					this.UpdateButtons ();
				}
			}
		}


		private readonly SearchingData			data;
		private readonly List<ColumnMapper>		columnMappers;

		private bool							bigDataInterface;
		private System.Action					searchStartAction;
		private System.Action<int>				searchNextAction;

		private FrameBox						middleFrame;
		private GlyphButton						searchButtonClear;
		private GlyphButton						searchButtonNext;
		private GlyphButton						searchButtonPrev;
		private StaticText						searchResult;
		private FrameBox						modeFrame;
	}
}
