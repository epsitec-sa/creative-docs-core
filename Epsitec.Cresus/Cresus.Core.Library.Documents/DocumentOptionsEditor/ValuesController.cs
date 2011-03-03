//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Dialogs;

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
		public ValuesController(IBusinessContext businessContext, DocumentOptionsEntity documentOptionsEntity, OptionsDictionary options)
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
						var button = new CheckButton
						{
							Parent = box,
							Name = OptionsDictionary.DocumentOptionToString (option.Option),
							Text = option.Description,
							ActiveState = (value == "true") ? ActiveState.Yes : ActiveState.No,
							Dock = DockStyle.Top,
							AutoToggle = false,
							TabIndex = ++tabIndex,
						};

						button.Clicked += delegate
						{
							var op = OptionsDictionary.StringToDocumentOption (button.Name);
							this.CheckButtonAction (op);
						};

						this.editableWidgets.Add (button);
						firstWidget = false;
					}

					if (option.Type == DocumentOptionValueType.Enumeration)
					{
						for (int e = 0; e < option.Enumeration.Count (); e++)
						{
							var enumValue   = option.Enumeration.ElementAt (e);
							var description = option.EnumerationDescription.ElementAt (e);

							var button = new RadioButton
							{
								Parent = box,
								Name = string.Concat (OptionsDictionary.DocumentOptionToString (option.Option), ".", enumValue),
								Group = option.Group,
								Text = description,
								ActiveState = (value == enumValue) ? ActiveState.Yes : ActiveState.No,
								Dock = DockStyle.Top,
								TabIndex = ++tabIndex,
							};

							button.Clicked += delegate
							{
								var p = button.Name.Split ('.');
								var op = OptionsDictionary.StringToDocumentOption (p[0]);
								this.RadioButtonAction (op, p[1]);
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
							Name = OptionsDictionary.DocumentOptionToString (option.Option),
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
							var op = OptionsDictionary.StringToDocumentOption (field.Name);
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


		private void RadioButtonAction(DocumentOption option, string value)
		{
			this.options.Add (option, value);

			this.UpdateEditablesWidgetsValues ();
			this.SetDirty ();
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
				var option = OptionsDictionary.StringToDocumentOption (widget.Name);

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


		private readonly IBusinessContext					businessContext;
		private readonly DocumentOptionsEntity				documentOptionsEntity;
		private readonly OptionsDictionary					options;
		private readonly List<VerboseDocumentOption>		allOptions;
		private readonly List<VerboseDocumentOption>		titleOptions;
		private readonly List<Widget>						editableWidgets;

		private Scrollable									optionsFrame;
	}
}
