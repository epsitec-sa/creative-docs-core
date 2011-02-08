//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Dialogs;

using Epsitec.Cresus.Core.Entities;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.DocumentOptionsEditor
{
	public class ValuesController
	{
		public ValuesController(Core.Business.BusinessContext businessContext, DocumentOptionsEntity documentOptionsEntity, Dictionary<string, string> options)
		{
			this.businessContext       = businessContext;
			this.documentOptionsEntity = documentOptionsEntity;
			this.options               = options;

			this.allOptions   = DocumentOption.GetAllDocumentOptions ().Where (x => !x.IsTitle).ToList ();
			this.titleOptions = DocumentOption.GetAllDocumentOptions ().Where (x => x.IsTitle).ToList ();
		}


		public void CreateUI(Widget parent)
		{
			this.optionsFrame = new Scrollable
			{
				Parent = parent,
				PreferredWidth = 300,
				Dock = DockStyle.Left,
				HorizontalScrollerMode = ScrollableScrollerMode.HideAlways,
				VerticalScrollerMode = ScrollableScrollerMode.Auto,
				PaintViewportFrame = false,
			};

			this.optionsFrame.Viewport.IsAutoFitting = true;

			this.UpdateOptionButtons ();
		}


		public void Update()
		{
			this.UpdateOptionButtons ();
		}


		private void UpdateOptionButtons()
		{
			this.optionsFrame.Viewport.Children.Clear ();

			string lastGroup = null;
			int tabIndex = 0;

			foreach (var option in this.allOptions)
			{
				if (this.options.ContainsKey (option.Name))
				{
					if (lastGroup != option.Group)
					{
						lastGroup = option.Group;

						if (!string.IsNullOrEmpty (option.Group))
						{
							var t = this.titleOptions.Where (x => x.Group == lastGroup).FirstOrDefault ();

							if (t != null)
							{
								var title = new StaticText
								{
									Parent = this.optionsFrame.Viewport,
									Text = t.Title,
									Dock = DockStyle.Top,
									Margins = new Margins (0, 0, 10, 5),
								};
							}
						}
					}

					string value = this.options[option.Name];

					if (option.Widget == DocumentOptionWidget.CheckButton)
					{
						var check = new CheckButton
						{
							Parent = this.optionsFrame.Viewport,
							Name = option.Name,
							Text = option.Description,
							ActiveState = (value == "true") ? ActiveState.Yes : ActiveState.No,
							Dock = DockStyle.Top,
							AutoToggle = false,
							TabIndex = ++tabIndex,
						};
					}

					if (option.Widget == DocumentOptionWidget.RadioButton)
					{
						var check = new RadioButton
						{
							Parent = this.optionsFrame.Viewport,
							Name = option.Name,
							Text = option.Description,
							ActiveState = (value == "true") ? ActiveState.Yes : ActiveState.No,
							Dock = DockStyle.Top,
							AutoToggle = false,
							TabIndex = ++tabIndex,
						};
					}
				}
			}
		}


		private void SetDirty()
		{
			this.businessContext.NotifyExternalChanges ();
		}


		private readonly Core.Business.BusinessContext		businessContext;
		private readonly DocumentOptionsEntity				documentOptionsEntity;
		private readonly Dictionary<string, string>			options;
		private readonly List<DocumentOption>				allOptions;
		private readonly List<DocumentOption>				titleOptions;

		private Scrollable									optionsFrame;
	}
}
