//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Dialogs;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Print2;
using Epsitec.Cresus.Core.Print2.Verbose;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.DocumentOptionsEditor
{
	public class ValuesController
	{
		public ValuesController(Core.Business.BusinessContext businessContext, DocumentOptionsEntity documentOptionsEntity, Print2.OptionsDictionary options)
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
			this.editableWidgets.Clear ();

			string lastGroup = null;
			int tabIndex = 0;
			bool firstWidget = true;
			FrameBox box = null;

			foreach (var option in this.allOptions)
			{
				if (this.options.ContainsOption (option.Option))
				{
					if (firstWidget || lastGroup != option.Group)
					{
						lastGroup = option.Group;
						firstWidget = false;

						box = new FrameBox
						{
							DrawFullFrame = true,
							Parent = this.optionsFrame.Viewport,
							Dock = DockStyle.Top,
							Margins = new Margins (0, 0, 0, -1),
							Padding = new Margins (10),
						};

						if (!string.IsNullOrEmpty (option.Group))
						{
							var t = this.titleOptions.Where (x => x.Group == lastGroup).FirstOrDefault ();

							if (t != null)
							{
								var title = new StaticText
								{
									Parent = box,
									Text = string.Concat (t.Title, " :"),
									Dock = DockStyle.Top,
									Margins = new Margins (0, 0, 0, 10),
								};
							}
						}
					}

					string value = this.options.GetValue (option.Option);

					if (option.Type == DocumentOptionValueType.Boolean)
					{
						if (option.Widget == DocumentOptionWidgetType.RadioButton)
						{
							var button = new RadioButton
							{
								Parent = box,
								Name = Print2.Common.DocumentOptionToString (option.Option),
								Group = option.Group,
								Text = option.Description,
								ActiveState = (value == "true") ? ActiveState.Yes : ActiveState.No,
								Dock = DockStyle.Top,
								AutoToggle = false,
								TabIndex = ++tabIndex,
							};

							button.Clicked += delegate
							{
								var op = Print2.Common.StringToDocumentOption (button.Name);
								this.RadioButtonAction (op);
							};

							this.editableWidgets.Add (button);
							firstWidget = false;
						}
						else
						{
							var button = new CheckButton
							{
								Parent = box,
								Name = Print2.Common.DocumentOptionToString (option.Option),
								Text = option.Description,
								ActiveState = (value == "true") ? ActiveState.Yes : ActiveState.No,
								Dock = DockStyle.Top,
								AutoToggle = false,
								TabIndex = ++tabIndex,
							};

							button.Clicked += delegate
							{
								var op = Print2.Common.StringToDocumentOption (button.Name);
								this.CheckButtonAction (op);
							};

							this.editableWidgets.Add (button);
							firstWidget = false;
						}
					}

					if (option.Type == DocumentOptionValueType.Distance)
					{
						var frame = new FrameBox
						{
							Parent = box,
							Dock = DockStyle.Top,
							Margins = new Margins (0, 0, 1, 0),
							TabIndex = ++tabIndex,
						};

						var label = new StaticText
						{
							Parent = frame,
							Text = option.Description,
							Dock = DockStyle.Fill,
						};

						var unit = new StaticText
						{
							Parent = frame,
							Text = "mm",
							PreferredWidth = 20,
							Dock = DockStyle.Right,
							Margins = new Margins (5, 0, 0, 0),
						};

						var field = new TextFieldEx
						{
							Parent = frame,
							Name = Print2.Common.DocumentOptionToString (option.Option),
							Text = value,
							PreferredWidth = 80,
							Dock = DockStyle.Right,
							DefocusAction = DefocusAction.AutoAcceptOrRejectEdition,
							SwallowEscapeOnRejectEdition = true,
							SwallowReturnOnAcceptEdition = true,
							TabIndex = ++tabIndex,
						};

						field.EditionAccepted += delegate
						{
							var op = Print2.Common.StringToDocumentOption (field.Name);
							this.EditionAccepted (op, field.Text);
						};

						this.editableWidgets.Add (field);
						firstWidget = false;
					}
				}
			}

			var defaultButton = new Button
			{
				Parent = this.optionsFrame.Viewport,
				Text = "Valeurs par défaut",
				Dock = DockStyle.Top,
				Margins = new Margins (0, 0, 10, 1),
			};

			defaultButton.Clicked += delegate
			{
				this.DefaultValues ();
			};
		}


		private void RadioButtonAction(DocumentOption option)
		{
			string group = this.allOptions.Where (x => x.Option == option).Select (x => x.Group).FirstOrDefault ();

			if (!string.IsNullOrEmpty (group))
			{
				var groups = this.allOptions.Where (x => x.Group == group && x.Widget == DocumentOptionWidgetType.RadioButton).Select (x => x.Option);

				foreach (var key in this.options.Options.ToArray ())
				{
					if (groups.Contains (key))
					{
						this.options.Add (key, "false");
					}
				}
			}

			this.CheckButtonAction (option);
		}

		private void CheckButtonAction(DocumentOption option)
		{
			if (this.options.GetValue (option) == "false")
			{
				this.options.Add (option, "true");
			}
			else
			{
				this.options.Add (option, "false");
			}

			this.UpdateEditablesWidgetsValues ();
			this.SetDirty ();
		}

		private void EditionAccepted(DocumentOption option, string value)
		{
			double d;
			if (double.TryParse (value, out d))
			{
				this.options.Add (option, value);
			}

			this.UpdateEditablesWidgetsValues ();
			this.SetDirty ();
		}

		private void DefaultValues()
		{
			foreach (var key in this.options.Options.ToArray ())
			{
				string value = this.allOptions.Where (x => x.Option == key).Select (x => x.DefaultValue).FirstOrDefault ();
				this.options.Add (key, value);
			}

			this.UpdateEditablesWidgetsValues ();
			this.SetDirty ();
		}

		private void UpdateEditablesWidgetsValues()
		{
			foreach (var widget in this.editableWidgets)
			{
				var option = Print2.Common.StringToDocumentOption (widget.Name);

				if (widget is AbstractButton)
				{
					if (this.options.ContainsOption (option))
					{
						widget.ActiveState = (this.options.GetValue (option) == "true") ? ActiveState.Yes : ActiveState.No;
					}
				}

				if (widget is AbstractTextField)
				{
					widget.Text = this.options.GetValue (option);
				}
			}
		}


		private void SetDirty()
		{
			this.businessContext.NotifyExternalChanges ();
		}


		private readonly Core.Business.BusinessContext		businessContext;
		private readonly DocumentOptionsEntity				documentOptionsEntity;
		private readonly Print2.OptionsDictionary			options;
		private readonly List<VerboseDocumentOption>		allOptions;
		private readonly List<VerboseDocumentOption>		titleOptions;
		private readonly List<Widget>						editableWidgets;

		private Scrollable									optionsFrame;
	}
}
