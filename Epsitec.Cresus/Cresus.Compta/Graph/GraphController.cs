//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

using Epsitec.Cresus.Compta.Controllers;
using Epsitec.Cresus.Compta.Helpers;
using Epsitec.Cresus.Compta.Widgets;
using Epsitec.Cresus.Compta;

using System.Collections.Generic;

namespace Epsitec.Cresus.Compta.Graph
{
	public class GraphController
	{
		public GraphController(AbstractController controller)
		{
			this.controller = controller;
		}


		public void CreateUI(Widget parent)
		{
			this.mainFrame = new FrameBox
			{
				Parent     = parent,
				Dock       = DockStyle.Fill,
				Visibility = false,
			};

			var toolbar = new FrameBox
			{
				Parent          = this.mainFrame,
				PreferredHeight = 20,
				Dock            = DockStyle.Top,
				Margins         = new Margins (10),
			};

			new Separator
			{
				Parent           = this.mainFrame,
				PreferredHeight  = 1,
				IsHorizontalLine = true,
				Dock             = DockStyle.Top,
			};

			var frame = new FrameBox
			{
				Parent          = this.mainFrame,
				Dock            = DockStyle.Fill,
			};

			var options = new GraphOptions ();
			var controller = new OptionsController (options);

			this.graphWidget = new GraphWidget
			{
				Parent  = frame,
				Cube    = this.controller.DataAccessor.Cube,
				Options = options,
				Dock    = DockStyle.Fill,
			};

			controller.CreateUI (toolbar, () => this.graphWidget.Invalidate ());
		}

		public bool Show
		{
			get
			{
				return this.mainFrame.Visibility;
			}
			set
			{
				this.mainFrame.Visibility = value;
			}
		}


		private readonly AbstractController		controller;

		private FrameBox						mainFrame;
		private GraphWidget						graphWidget;
	}
}
