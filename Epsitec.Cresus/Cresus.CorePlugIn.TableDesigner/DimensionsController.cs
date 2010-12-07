//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Business.Finance.PriceCalculators;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.CorePlugIn.TableDesigner
{
	public class DimensionsController
	{
		public DimensionsController(DimensionTable table)
		{
			this.table = table;
		}

		public void CreateUI(Widget parent)
		{
			var frame = new FrameBox
			{
				Parent = parent,
				Dock = DockStyle.Fill,
				Margins = new Margins (10),
			};

			var leftPane = new FrameBox
			{
				Parent = frame,
				PreferredWidth = 200,
				Dock = DockStyle.Left,
				Margins = new Margins (0, 4, 0, 0),
			};

			var rightPane = new FrameBox
			{
				Parent = frame,
				Dock = DockStyle.Fill,
				Margins = new Margins (0, 0, 0, 0),
			};

			//	Crée le panneau de gauche.
			{
				// TODO: Utiliser un Epsitec.Cresus.Core.Controllers.ListController dès que le plugin est de retour dans Cresus.Core !
				//	Crée la toolbar.
				double buttonSize = 19;

				this.toolbar = new FrameBox
				{
					Parent = leftPane,
					DrawFullFrame = true,
					PreferredHeight = buttonSize,
					//?BackColor = ArrowedFrame.SurfaceColors.First (),
					Padding = new Margins (2),
					Dock = DockStyle.Top,
					Margins = new Margins (0, 0, 0, -1),
				};

				this.addButton = new GlyphButton
				{
					Parent = toolbar,
					PreferredSize = new Size (buttonSize*2+1, buttonSize),
					GlyphShape = GlyphShape.Plus,
					Margins = new Margins (0, 0, 0, 0),
					Dock = DockStyle.Left,
				};

				this.removeButton = new GlyphButton
				{
					Parent = toolbar,
					PreferredSize = new Size (buttonSize, buttonSize),
					GlyphShape = GlyphShape.Minus,
					Margins = new Margins (1, 0, 0, 0),
					Dock = DockStyle.Left,
				};

				this.upButton = new GlyphButton
				{
					Parent = toolbar,
					PreferredSize = new Size (buttonSize, buttonSize),
					GlyphShape = GlyphShape.ArrowUp,
					Margins = new Margins (10, 0, 0, 0),
					Dock = DockStyle.Left,
				};

				this.downButton = new GlyphButton
				{
					Parent = toolbar,
					PreferredSize = new Size (buttonSize, buttonSize),
					GlyphShape = GlyphShape.ArrowDown,
					Margins = new Margins (1, 0, 0, 0),
					Dock = DockStyle.Left,
				};

				ToolTip.Default.SetToolTip (this.addButton,    "Crée une nouvelle dimension");
				ToolTip.Default.SetToolTip (this.removeButton, "Supprime la dimension sélectionnée");
				ToolTip.Default.SetToolTip (this.upButton,     "Monte la dimension");
				ToolTip.Default.SetToolTip (this.downButton,   "Desacend la dimension");
			}

			{
				//	Crée la liste.
				this.list = new ScrollList
				{
					Parent = leftPane,
					Dock = DockStyle.Fill,
				};
			}

			// Crée le panneau de droite.
			var right = new FrameBox
			{
				Parent = rightPane,
				DrawFullFrame = true,
				Dock = DockStyle.Fill,
				Padding = new Margins (10),
			};

			this.UpdateList ();
		}

		private void UpdateList()
		{
			this.list.Items.Clear ();

			foreach (var d in this.table.Dimensions)
			{
				this.list.Items.Add (d.Name);
			}
		}


		private readonly DimensionTable					table;
		private FrameBox								toolbar;
		private GlyphButton								addButton;
		private GlyphButton								removeButton;
		private GlyphButton								upButton;
		private GlyphButton								downButton;
		private ScrollList								list;
	}
}
