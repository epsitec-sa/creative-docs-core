//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Dialogs;

using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Documents;
using Epsitec.Cresus.Core.Documents.Verbose;
using Epsitec.Cresus.Core.Entities;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.DocumentOptionsEditor
{
	public class ValuesController
	{
		public ValuesController(IBusinessContext businessContext, DocumentOptionsEntity documentOptionsEntity, PrintingOptionDictionary options)
		{
			this.businessContext       = businessContext;
			this.documentOptionsEntity = documentOptionsEntity;
			this.options               = options;

			this.allOptions   = VerboseDocumentOption.GetAll ().Where (x => !x.IsTitle).ToList ();
			this.titleOptions = VerboseDocumentOption.GetAll ().Where (x => x.IsTitle).ToList ();

			this.editableWidgets = new List<Widget> ();
		}


		public void CreateUI(Widget parent)
		{
			var leftFrame = new FrameBox
			{
				Parent = parent,
				PreferredWidth = 300,
				Dock = DockStyle.Left,
				Margins = new Margins (0, 10, 0, 0),
			};

			var rightFrame = new FrameBox
			{
				Parent = parent,
				Dock = DockStyle.Fill,
			};

			this.CreateLeftUI (leftFrame);
			this.CreateRightUI (rightFrame);

			this.Update ();
		}

		private void CreateLeftUI(Widget parent)
		{
			this.optionsFrame = new Scrollable
			{
				Parent = parent,
				Dock = DockStyle.Fill,
				HorizontalScrollerMode = ScrollableScrollerMode.HideAlways,
				VerticalScrollerMode = ScrollableScrollerMode.Auto,
				PaintViewportFrame = false,
			};

			this.optionsFrame.Viewport.IsAutoFitting = true;
		}

		private void CreateRightUI(Widget parent)
		{
			var title = new StaticText
			{
				Parent = parent,
				Text = "Adapté aux documents suivants :",
				Dock = DockStyle.Top,
				Margins = new Margins (0, 0, 0, 2),
			};

			var box = new FrameBox
			{
				Parent = parent,
				PreferredHeight = 200,
				DrawFullFrame = true,
				Dock = DockStyle.Fill,
				Padding = new Margins (10, 10, 1, 1),
			};

			this.documentTypesText = new StaticText
			{
				Parent = box,
				ContentAlignment = Common.Drawing.ContentAlignment.TopLeft,
				TextBreakMode = Common.Drawing.TextBreakMode.Hyphenate,
				Dock = DockStyle.Fill,
			};
		}


		public void Update()
		{
			this.UpdateOptionButtons ();
			this.UpdateDocumentTypesText ();
		}

		private void UpdateOptionButtons()
		{
			var controller = new OptionsController (null, this.options);
			controller.CreateUI (this.optionsFrame.Viewport, this.SetDirty);
		}

		private void UpdateDocumentTypesText()
		{
			this.documentTypesText.FormattedText = this.options.GetDocumentTypesSummary ();
		}


		private void SetDirty()
		{
			this.businessContext.NotifyExternalChanges ();
		}


		private readonly IBusinessContext					businessContext;
		private readonly DocumentOptionsEntity				documentOptionsEntity;
		private readonly PrintingOptionDictionary			options;
		private readonly List<VerboseDocumentOption>		allOptions;
		private readonly List<VerboseDocumentOption>		titleOptions;
		private readonly List<Widget>						editableWidgets;

		private Scrollable									optionsFrame;
		private StaticText									documentTypesText;
	}
}
