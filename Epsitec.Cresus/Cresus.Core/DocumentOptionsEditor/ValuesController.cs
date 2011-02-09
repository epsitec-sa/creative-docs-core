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
				if (this.options.ContainsKey (option.Name))
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

					string value = this.options[option.Name];

					if (option.Type == DocumentOptionValueType.Boolean)
					{
						if (option.Widget == DocumentOptionWidgetType.RadioButton)
						{
							var button = new RadioButton
							{
								Parent = box,
								Name = option.Name,
								Group = option.Group,
								Text = option.Description,
								ActiveState = (value == "true") ? ActiveState.Yes : ActiveState.No,
								Dock = DockStyle.Top,
								AutoToggle = false,
								TabIndex = ++tabIndex,
							};

							button.Clicked += delegate
							{
								this.RadioButtonAction (button.Name);
							};

							this.editableWidgets.Add (button);
							firstWidget = false;
						}
						else
						{
							var button = new CheckButton
							{
								Parent = box,
								Name = option.Name,
								Text = option.Description,
								ActiveState = (value == "true") ? ActiveState.Yes : ActiveState.No,
								Dock = DockStyle.Top,
								AutoToggle = false,
								TabIndex = ++tabIndex,
							};

							button.Clicked += delegate
							{
								this.CheckButtonAction (button.Name);
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
							Name = option.Name,
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
							this.EditionAccepted (field.Name, field.Text);
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


		private void RadioButtonAction(string name)
		{
			string group = this.allOptions.Where (x => x.Name == name).Select (x => x.Group).FirstOrDefault ();

			if (!string.IsNullOrEmpty (group))
			{
				var groups = this.allOptions.Where (x => x.Group == group && x.Widget == DocumentOptionWidgetType.RadioButton).Select (x => x.Name);

				foreach (var key in this.options.Keys.ToArray ())
				{
					if (groups.Contains (key))
					{
						this.options[key] = "false";
					}
				}
			}

			this.CheckButtonAction (name);
		}

		private void CheckButtonAction(string name)
		{
			if (this.options[name] == "false")
			{
				this.options[name] = "true";
			}
			else
			{
				this.options[name] = "false";
			}

			this.UpdateEditablesWidgetsValues ();
			this.SetDirty ();
		}

		private void EditionAccepted(string name, string value)
		{
			double d;
			if (double.TryParse (value, out d))
			{
				this.options[name] = value;
			}

			this.UpdateEditablesWidgetsValues ();
			this.SetDirty ();
		}

		private void DefaultValues()
		{
			foreach (var key in this.options.Keys.ToArray ())
			{
				string value = this.allOptions.Where (x => x.Name == key).Select (x => x.DefaultValue).FirstOrDefault ();
				this.options[key] = value;
			}

			this.UpdateEditablesWidgetsValues ();
			this.SetDirty ();
		}

		private void UpdateEditablesWidgetsValues()
		{
			foreach (var widget in this.editableWidgets)
			{
				if (widget is AbstractButton)
				{
					if (this.options.ContainsKey (widget.Name))
					{
						widget.ActiveState = (this.options[widget.Name] == "true") ? ActiveState.Yes : ActiveState.No;
					}
				}

				if (widget is AbstractTextField)
				{
					widget.Text = this.options[widget.Name];
				}
			}
		}


		private void SetDirty()
		{
			this.businessContext.NotifyExternalChanges ();
		}


		private readonly Core.Business.BusinessContext		businessContext;
		private readonly DocumentOptionsEntity				documentOptionsEntity;
		private readonly Dictionary<string, string>			options;  // "option d'impression" / "valeur"
		private readonly List<DocumentOption>				allOptions;
		private readonly List<DocumentOption>				titleOptions;
		private readonly List<Widget>						editableWidgets;

		private Scrollable									optionsFrame;
	}
}
