//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Dialogs;
using Epsitec.Common.Support.EntityEngine;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Documents;
using Epsitec.Cresus.Core.Documents.Verbose;
using Epsitec.Cresus.Core.Business;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.DocumentOptionsController
{
	/// <summary>
	/// Ce contrôleur permet de saisir les options d'impression. Un panneau d'environ 300 pixels de large et le plus haut
	/// possible contient des boutons à cocher et des boutons radio. Le mode 'threeState' permet de définir une option
	/// comme étant "non imposée". C'est avec ce mode qu'il est nécessaire de définir deux OptionsDictionary.
	/// </summary>
	public class OptionsController
	{
		public OptionsController(AbstractEntity entity, PrintingOptionDictionary optionsKeys, PrintingOptionDictionary optionsValues = null, bool threeState = false)
		{
			System.Diagnostics.Debug.Assert (optionsKeys != null);

			if (entity == null)
			{
				this.documentType = DocumentType.None;
			}
			else
			{
				this.documentType = DocumentType.None;  // TODO: à trouver d'après AbstractEntity !!!
			}

			this.optionsKeys = optionsKeys;

			if (optionsValues == null)
			{
				this.optionsValues = optionsKeys;
			}
			else
			{
				this.optionsValues = optionsValues;
			}

			this.threeState = threeState;

			this.allOptions   = VerboseDocumentOption.GetAll ().Where (x => !x.IsTitle).ToList ();
			this.titleOptions = VerboseDocumentOption.GetAll ().Where (x => x.IsTitle).ToList ();

			this.editableWidgets = new List<Widget> ();
		}


		public void CreateUI(Widget parent, System.Action onChanged)
		{
			this.onChanged = onChanged;

			parent.Children.Clear ();
			this.editableWidgets.Clear ();

			string lastGroup = null;
			int tabIndex = 0;
			bool firstWidget = true;
			FrameBox box = null;
			var currentType = DocumentOptionValueType.Undefined;
			var lastType    = DocumentOptionValueType.Undefined;
			string currentSeparatorGroup = null;
			string lastSeparatorGroup    = null;

			foreach (var verboseOption in this.allOptions)
			{
				if (this.optionsKeys.ContainsOption (verboseOption.Option))
				{
					lastType = currentType;
					currentType = verboseOption.Type;

					lastSeparatorGroup = currentSeparatorGroup;
					currentSeparatorGroup = verboseOption.Group;

					string group;

					if (string.IsNullOrEmpty (verboseOption.Group))
					{
						group = null;
					}
					else
					{
						int i = verboseOption.Group.IndexOf ('.');
						if (i == -1)
						{
							group = verboseOption.Group;
						}
						else
						{
							group = verboseOption.Group.Substring (0, i);  // transforme "Paper.1" en "Paper"
						}
					}

					//	Regarde s'il faut mettre le titre du groupe.
					bool titleGenerated = false;

					if (firstWidget || lastGroup != group)
					{
						lastGroup = group;
						firstWidget = false;
						lastType = DocumentOptionValueType.Undefined;

						box = new FrameBox
						{
							DrawFullFrame = true,
							Parent = parent,
							Dock = DockStyle.Top,
							Margins = new Margins (0, 0, 0, -1),
							Padding = new Margins (10),
						};

						if (!string.IsNullOrEmpty (verboseOption.Group))
						{
							var t = this.titleOptions.Where (x => x.Group == lastGroup).FirstOrDefault ();

							if (t != null)
							{
								var title = new StaticText
								{
									Parent = box,
									Text = string.Concat (t.Title, " :"),
									TextBreakMode = TextBreakMode.Ellipsis | TextBreakMode.Split | TextBreakMode.SingleLine,
									Dock = DockStyle.Top,
									Margins = new Margins (0, 0, 0, 4),
								};

								titleGenerated = true;
							}
						}
					}

					//	Regarde s'il faut mettre un espace de séparation.
					if (lastType != currentType || lastType == DocumentOptionValueType.Enumeration || (!titleGenerated && lastSeparatorGroup != currentSeparatorGroup))
					{
						new FrameBox
						{
							Parent = box,
							PreferredHeight = 6,
							Dock = DockStyle.Top,
						};
					}

					//	Regarde s'il faut mettre un bouton à cocher.
					if (currentType == DocumentOptionValueType.Boolean)
					{
						var button = new CheckButton
						{
							Parent = box,
							Name = DocumentOptionConverter.ToString (verboseOption.Option),
							Text = verboseOption.Description,
							AcceptThreeState = this.threeState,
							Dock = DockStyle.Top,
							AutoToggle = false,
							TabIndex = ++tabIndex,
						};

						button.Clicked += delegate
						{
							var op = DocumentOptionConverter.Parse (button.Name);
							this.CheckButtonAction (op);
						};

						this.editableWidgets.Add (button);
						firstWidget = false;
					}

					//	Regarde s'il faut mettre les liste de boutons radio.
					if (currentType == DocumentOptionValueType.Enumeration)
					{
						for (int e = -1; e < verboseOption.Enumeration.Count (); e++)
						{
							string enumValue, description;

							if (e == -1)
							{
								if (this.threeState)
								{
									enumValue   = "_unused_";
									description = "Pas imposé";
								}
								else
								{
									continue;
								}
							}
							else
							{
								enumValue   = verboseOption.Enumeration.ElementAt (e);
								description = verboseOption.EnumerationDescription.ElementAt (e);
							}

							var button = new RadioButton
							{
								Parent = box,
								Name = string.Concat (DocumentOptionConverter.ToString (verboseOption.Option), ".", enumValue),
								Group = verboseOption.Group,
								Text = description,
								Dock = DockStyle.Top,
								AutoToggle = false,
								TabIndex = ++tabIndex,
							};

							button.Clicked += delegate
							{
								var p = button.Name.Split ('.');
								var op = DocumentOptionConverter.Parse (p[0]);
								this.RadioButtonAction (op, p[1]);
							};

							this.editableWidgets.Add (button);
							firstWidget = false;
						}
					}

					//	Regarde s'il faut mettre un texte éditable.
					if (currentType == DocumentOptionValueType.Distance ||
						currentType == DocumentOptionValueType.Size)
					{
						var frame = new FrameBox
						{
							Parent = box,
							Dock = DockStyle.Top,
							Margins = new Margins (0, 0, -1, 0),
							TabIndex = ++tabIndex,
						};

						var label = new StaticText
						{
							Parent = frame,
							Text = verboseOption.Description,
							TextBreakMode = TextBreakMode.Ellipsis | TextBreakMode.Split | TextBreakMode.SingleLine,
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
							Name = DocumentOptionConverter.ToString (verboseOption.Option),
							PreferredWidth = 70,
							Dock = DockStyle.Right,
							DefocusAction = DefocusAction.AutoAcceptOrRejectEdition,
							SwallowEscapeOnRejectEdition = true,
							SwallowReturnOnAcceptEdition = true,
							TabIndex = ++tabIndex,
						};

						field.EditionAccepted += delegate
						{
							var op = DocumentOptionConverter.Parse (field.Name);
							this.EditionAccepted (op, field.Text);
						};

						this.editableWidgets.Add (field);
						firstWidget = false;
					}
				}
			}

			var defaultButton = new Button
			{
				Parent = parent,
				Text = this.threeState ? "N'impose aucune option" : "Valeurs par défaut",
				Dock = DockStyle.Top,
				Margins = new Margins (10, 10, 10, 1),
			};

			defaultButton.Clicked += delegate
			{
				this.DefaultValues ();
			};

			this.UpdateEditablesWidgetsValues ();
		}


		private void RadioButtonAction(DocumentOption option, string value)
		{
			if (this.threeState && value == "_unused_")
			{
				this.optionsValues[option] = null;
			}
			else
			{
				this.optionsValues[option] = value;
			}

			this.UpdateEditablesWidgetsValues ();
			this.SetDirty ();
		}

		private void CheckButtonAction(DocumentOption option)
		{
			if (this.threeState)
			{
				//	Cycle "maybe" -> "yes" -> "no"
				if (this.optionsValues.ContainsOption (option))
				{
					if (this.optionsValues[option] == "false")
					{
						this.optionsValues[option] = null;
					}
					else
					{
						this.optionsValues[option] = "false";
					}
				}
				else
				{
					this.optionsValues[option] = "true";
				}
			}
			else
			{
				//	Cycle "yes" -> "no"
				if (this.optionsValues[option] == "false")
				{
					this.optionsValues[option] = "true";
				}
				else
				{
					this.optionsValues[option] = "false";
				}
			}

			this.UpdateEditablesWidgetsValues ();
			this.SetDirty ();
		}

		private void EditionAccepted(DocumentOption option, string value)
		{
			if (this.threeState && string.IsNullOrEmpty (value))
			{
				this.optionsValues[option] = null;
			}
			else
			{
				double d;
				if (double.TryParse (value, out d))
				{
					this.optionsValues[option] = value;
				}
			}

			this.UpdateEditablesWidgetsValues ();
			this.SetDirty ();
		}

		private void DefaultValues()
		{
			if (this.threeState)
			{
				this.optionsValues.Clear ();
			}
			else
			{
				foreach (var key in this.optionsKeys.Options.ToArray ())
				{
					string value = this.allOptions.Where (x => x.Option == key).Select (x => x.DefaultValue).FirstOrDefault ();
					this.optionsValues[key] = value;
				}
			}

			this.UpdateEditablesWidgetsValues ();
			this.SetDirty ();
		}

		private void UpdateEditablesWidgetsValues()
		{
			foreach (var widget in this.editableWidgets)
			{
				if (widget is CheckButton)
				{
					var option = DocumentOptionConverter.Parse (widget.Name);

					if (this.optionsValues.ContainsOption (option))
					{
						widget.ActiveState = (this.optionsValues[option] == "true") ? ActiveState.Yes : ActiveState.No;
					}
					else
					{
						widget.ActiveState = ActiveState.Maybe;
					}
				}

				if (widget is RadioButton)
				{
					var n = widget.Name.Split ('.');
					var option = DocumentOptionConverter.Parse (n[0]);
					var value = n[1];

					if (value == "_unused_")
					{
						widget.ActiveState = (!this.optionsValues.ContainsOption (option)) ? ActiveState.Yes : ActiveState.No;
					}
					else
					{
						widget.ActiveState = (this.optionsValues[option] == value) ? ActiveState.Yes : ActiveState.No;
					}
				}

				if (widget is AbstractTextField)
				{
					var option = DocumentOptionConverter.Parse (widget.Name);
					widget.Text = this.optionsValues[option];
				}
			}
		}


		private void SetDirty()
		{
			if (this.onChanged != null)
			{
				this.onChanged ();
			}
		}


		private readonly DocumentType						documentType;
		private readonly PrintingOptionDictionary			optionsKeys;
		private readonly PrintingOptionDictionary			optionsValues;
		private readonly bool								threeState;
		private readonly List<VerboseDocumentOption>		allOptions;
		private readonly List<VerboseDocumentOption>		titleOptions;
		private readonly List<Widget>						editableWidgets;

		private System.Action								onChanged;
	}
}
