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
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Library;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Core.Resolvers;

namespace Epsitec.Cresus.Core.DocumentOptionsController
{
	public class SelectController
	{
		public SelectController(IBusinessContext businessContext, DocumentOptionsEntity documentOptionsEntity, PrintingOptionDictionary options)
		{
			this.businessContext       = businessContext;
			this.documentOptionsEntity = documentOptionsEntity;
			this.options               = options;

			this.allOptions = VerboseDocumentOption.GetAll ().ToList ();
			this.visibleOptions = new List<VerboseDocumentOption> ();

			this.documentTypeFilter = DocumentType.Unknown;
		}


		public void CreateUI(Widget parent)
		{
			var leftFrame = new FrameBox
			{
				Parent = parent,
				PreferredWidth = 330,
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

			//	Connexion des événements.
			this.table.SelectionChanged += delegate
			{
				this.UpdateWidgets ();
			};

			this.noButton.Clicked += delegate
			{
				this.ActionUse (false);
			};

			this.yesButton.Clicked += delegate
			{
				this.ActionUse (true);
			};

			this.noneButton.Clicked += delegate
			{
				this.ActionAll (false);
			};

			this.allButton.Clicked += delegate
			{
				this.ActionAll (true);
			};

			this.UpdateTable ();
			this.UpdateWidgets ();
		}

		private void CreateLeftUI(Widget parent)
		{
			{
				var top = new FrameBox
				{
					Parent = parent,
					Dock = DockStyle.Top,
					Margins = new Margins (0, 0, 0, 10),
				};

				new StaticText
				{
					Parent = top,
					Text = "Filtre",
					PreferredWidth = 35,
					Dock = DockStyle.Left,
				};

				var combo = new TextFieldCombo
				{
					Parent = top,
					Dock = DockStyle.Fill,
					IsReadOnly = true,
				};

				var types = EnumKeyValues.FromEnum<DocumentType> ();
				foreach (var type in types)
				{
					if (type.Key == DocumentType.None)
					{
						combo.Items.Add (DocumentType.None.ToString (), "Montrer les options pour tous les documents");
					}
					else if (type.Key == DocumentType.Unknown)
					{
						continue;
					}
					else
					{
						string description = string.Format ("Seulement les options pour \"{0}\"", type.Values[0]);
						combo.Items.Add (type.Key.ToString(), description);
					}
				}

				this.DocumentTypeFilter = DocumentType.None;
				combo.SelectedItemIndex = 0;  // sélectionne "Montrer tous les documents"

				combo.SelectedItemChanged += delegate
				{
					string key = combo.Items.GetKey (combo.SelectedItemIndex);
					this.DocumentTypeFilter = (DocumentType) System.Enum.Parse (typeof (DocumentType), key);
					this.UpdateTable ();
					this.UpdateWidgets ();
				};
			}

			this.table = new CellTable
			{
				Parent = parent,
				Dock = DockStyle.Fill,
				StyleH = CellArrayStyles.Separator | CellArrayStyles.Header,
				StyleV = CellArrayStyles.ScrollNorm | CellArrayStyles.Separator | CellArrayStyles.SelectLine | CellArrayStyles.SelectMulti,
			};
		}

		private void CreateRightUI(Widget parent)
		{
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

			{
				var buttonsBox = new FrameBox
				{
					Parent = parent,
					DrawFullFrame = true,
					Dock = DockStyle.Bottom,
					Margins = new Margins (0, 0, 10, 0),
					Padding = new Margins (10),
				};

				this.noButton = new RadioButton
				{
					Parent = buttonsBox,
					Text = "N'utilise pas",
					AutoToggle = false,
					Dock = DockStyle.Top,
				};

				this.yesButton = new RadioButton
				{
					Parent = buttonsBox,
					Text = "Utilise",
					AutoToggle = false,
					Dock = DockStyle.Top,
				};

				this.noneButton = new Button
				{
					Parent = buttonsBox,
					Text = "N'utilise aucun",
					Dock = DockStyle.Top,
					Margins = new Margins (0, 0, 5, 0),
				};

				this.allButton = new Button
				{
					Parent = buttonsBox,
					Text = "Utilise tout",
					Dock = DockStyle.Top,
					Margins = new Margins (0, 0, 1, 0),
				};
			}

			{
				var box = new FrameBox
				{
					Parent = parent,
					PreferredHeight = 4+14*6,
					DrawFullFrame = true,
					Dock = DockStyle.Bottom,
					Padding = new Margins (10, 10, 1, 1),
				};

				var title = new StaticText
				{
					Parent = parent,
					Text = "Type de la valeur :",
					Dock = DockStyle.Bottom,
					Margins = new Margins (0, 0, 5, 2),
				};

				this.valueTypeText = new StaticText
				{
					Parent = box,
					ContentAlignment = Common.Drawing.ContentAlignment.TopLeft,
					TextBreakMode = Common.Drawing.TextBreakMode.Hyphenate,
					Dock = DockStyle.Fill,
				};
			}

			{
				var box = new FrameBox
				{
					Parent = parent,
					PreferredHeight = 4+14*3,
					DrawFullFrame = true,
					Dock = DockStyle.Bottom,
					Padding = new Margins (10, 10, 1, 1),
				};

				var title = new StaticText
				{
					Parent = parent,
					Text = "Description de l'option :",
					Dock = DockStyle.Bottom,
					Margins = new Margins (0, 0, 5, 2),
				};

				this.valueDescription = new StaticText
				{
					Parent = box,
					ContentAlignment = Common.Drawing.ContentAlignment.TopLeft,
					TextBreakMode = Common.Drawing.TextBreakMode.Hyphenate,
					Dock = DockStyle.Fill,
				};
			}
		}


		public void Update()
		{
		}


		private void ActionUse(bool value)
		{
			for (int row = 0; row < this.visibleOptions.Count; row++)
			{
				if (table.IsCellSelected (row, 0))
				{
					this.SetUsedOption (row, value);
				}
			}

			this.UpdateTable ();
			this.UpdateWidgets ();
		}

		private void ActionAll(bool value)
		{
			for (int sel = 0; sel < this.visibleOptions.Count; sel++)
			{
				this.SetUsedOption (sel, value);
			}

			this.UpdateTable ();
			this.UpdateWidgets ();
		}


		private void UpdateTable()
		{
			int rows = this.visibleOptions.Count;
			this.table.SetArraySize (3, rows);

			this.table.SetWidthColumn (0, 220);
			this.table.SetWidthColumn (1, 40);
			this.table.SetWidthColumn (2, 50);

			this.table.SetHeaderTextH (0, "Description");
			this.table.SetHeaderTextH (1, "Type");
			this.table.SetHeaderTextH (2, "Utilisé");

			ContentAlignment[] alignments =
			{
				ContentAlignment.MiddleLeft,
				ContentAlignment.MiddleCenter,
				ContentAlignment.MiddleCenter,
			};

			for (int row=0; row<rows; row++)
			{
				this.table.FillRow (row, alignments);
				this.table.UpdateRow (row, this.GetRowTexts (row));
				this.table.UpdateRow (row, this.GetRowColors (row));
			}
		}

		private string[] GetRowTexts(int row)
		{
			var result = new string[3];

			if (this.IsTitle (row))
			{
				result[0] = TextFormatter.FormatText (string.Concat (this.GetDescription (row), " :")).ApplyItalic ().ToString ();
				result[1] = null;
				result[2] = null;
			}
			else
			{
				result[0] = this.GetDescription (row);
				result[1] = this.GetType (row);
				result[2] = this.GetUsedOption (row) ? "oui" : "non";
			}

			return result;
		}

		private Color[] GetRowColors(int row)
		{
			var result = new Color[3];

			if (this.IsTitle (row))
			{
				result[0] = Color.FromBrightness (0.9);
				result[1] = Color.FromBrightness (0.9);
				result[2] = Color.FromBrightness (0.9);
			}
			else
			{
				result[0] = Color.Empty;
				result[1] = Color.Empty;
				result[2] = Color.Empty;
			}

			return result;
		}

		private void UpdateWidgets()
		{
			int count = 0;
			int usedCount = 0;
			int selCount = 0;
			int sel = -1;

			for (int row = 0; row < this.visibleOptions.Count; row++)
			{
				if (this.IsTitle (row))
				{
					continue;
				}

				bool state = this.GetUsedOption (row);

				if (state)
				{
					selCount++;
				}

				if (table.IsCellSelected (row, 0))
				{
					if (state)
					{
						usedCount++;
					}

					count++;

					if (sel == -1)
					{
						sel = row;
					}
				}
			}

			bool diff = (count > 1 && usedCount != 0 && count != usedCount);

			if (sel == -1 || count == 0)
			{
				this.documentTypesText.Text = null;
				this.valueDescription.Text  = null;
				this.valueTypeText.Text     = null;
			}
			else if (count > 1)
			{
				this.documentTypesText.FormattedText = TextFormatter.FormatText ("Sélection multiple").ApplyItalic ();
				this.valueDescription.FormattedText  = TextFormatter.FormatText ("Sélection multiple").ApplyItalic ();
				this.valueTypeText.FormattedText     = TextFormatter.FormatText ("Sélection multiple").ApplyItalic ();
			}
			else
			{
				this.documentTypesText.Text = this.visibleOptions[sel].DocumentTypeDescription;
				this.valueDescription.Text  = this.GetLongDescription (sel);
				this.valueTypeText.Text     = this.GetValueTypeDescription (sel);
			}

			if (count == 0 || this.IsTitle (sel))
			{
				this.noButton.Enable  = false;
				this.yesButton.Enable = false;
			}
			else
			{
				this.noButton.Enable  = true;
				this.yesButton.Enable = true;
			}

			if (sel == -1 || diff || this.IsTitle (sel))
			{
				this.noButton.ActiveState  = ActiveState.No;
				this.yesButton.ActiveState = ActiveState.No;
			}
			else
			{
				bool state = this.GetUsedOption (sel);
				this.noButton.ActiveState  = !state ? ActiveState.Yes : ActiveState.No;
				this.yesButton.ActiveState =  state ? ActiveState.Yes : ActiveState.No;
			}

			this.noneButton.Enable = (selCount != 0);
			this.allButton.Enable  = (selCount != this.visibleOptions.Count);
		}

		private string GetDescription(int row)
		{
			if (!string.IsNullOrEmpty (this.visibleOptions[row].ShortDescription))
			{
				return this.visibleOptions[row].ShortDescription;
			}

			if (!string.IsNullOrEmpty (this.visibleOptions[row].Title))
			{
				return this.visibleOptions[row].Title;
			}

			string group = this.visibleOptions[row].Group;
			return this.visibleOptions.Where (x => x.Group == group).Select (x => x.Title).FirstOrDefault ();
		}

		private string GetLongDescription(int row)
		{
			if (!string.IsNullOrEmpty (this.visibleOptions[row].LongDescription))
			{
				return this.visibleOptions[row].LongDescription;
			}

			if (!string.IsNullOrEmpty (this.visibleOptions[row].ShortDescription))
			{
				return this.visibleOptions[row].ShortDescription;
			}

			if (!string.IsNullOrEmpty (this.visibleOptions[row].Title))
			{
				return this.visibleOptions[row].Title;
			}

			string group = this.visibleOptions[row].Group;
			return this.visibleOptions.Where (x => x.Group == group).Select (x => x.Title).FirstOrDefault ();
		}

		private string GetType(int row)
		{
			switch (this.visibleOptions[row].Type)
			{
				case DocumentOptionValueType.Boolean:
					return "bool";

				case DocumentOptionValueType.Enumeration:
					return "enum";

				case DocumentOptionValueType.Distance:
				case DocumentOptionValueType.Size:
					return "mm";

				case DocumentOptionValueType.Text:
				case DocumentOptionValueType.TextMultiline:
					return "texte";

				default:
					return null;
			}
		}

		private string GetValueTypeDescription(int row)
		{
			switch (this.visibleOptions[row].Type)
			{
				case DocumentOptionValueType.Boolean:
					return "Vrai ou faux";

				case DocumentOptionValueType.Enumeration:
					return string.Concat ("Enumération :<br/>● ", string.Join ("<br/>● ", this.visibleOptions[row].EnumerationDescription));

				case DocumentOptionValueType.Distance:
					return "Distance en mm";

				case DocumentOptionValueType.Size:
					return "Dimension en mm";
			}

			return null;
		}


		private bool IsTitle(int row)
		{
			if (row == -1)
			{
				return true;
			}
			else
			{
				return this.visibleOptions[row].IsTitle;
			}
		}

		private void SetUsedOption(int row, bool value)
		{
			if (this.visibleOptions[row].IsTitle)
			{
				return;
			}

			var option = this.visibleOptions[row].Option;

			if (value)  // utilise l'option ?
			{
				if (!this.GetUsedOption (row))
				{
					this.options[option] = this.visibleOptions[row].DefaultValue;
					this.SetDirty ();
				}
			}
			else  // n'utilise pas l'option ?
			{
				if (this.GetUsedOption (row))
				{
					this.options[option] = null;
					this.SetDirty ();
				}
			}
		}

		private bool GetUsedOption(int row)
		{
			var option = this.visibleOptions[row].Option;
			return this.options.ContainsOption (option);
		}


		private DocumentType DocumentTypeFilter
		{
			get
			{
				return this.documentTypeFilter;
			}
			set
			{
				if (this.documentTypeFilter != value)
				{
					this.documentTypeFilter = value;
					this.UpdateVisibleOptions ();
				}
			}
		}

		private void UpdateVisibleOptions()
		{
			this.visibleOptions.Clear ();
			this.visibleOptions.AddRange (this.allOptions);

			if (this.documentTypeFilter != DocumentType.None)
			{
				var options = EntityPrinterFactoryResolver.FindRequiredDocumentOptions (this.documentTypeFilter);

				//	On part de la liste complète, à laquelle on enlève les options inutiles.
				//	Il restera donc au minimun tous les titres.
				int i = 0;
				while (i < this.visibleOptions.Count)
				{
					if (!this.visibleOptions[i].IsTitle &&
						(options == null || !options.Contains (this.visibleOptions[i].Option)))
					{
						this.visibleOptions.RemoveAt (i);
					}
					else
					{
						i++;
					}
				}

				//	On enlève les titres inutiles, c'est-à-dire suivis d'un autre titre
				//	ou placés à la fin.
				i = 0;
				while (i < this.visibleOptions.Count)
				{
					if (this.visibleOptions[i].IsTitle &&
						(i == this.visibleOptions.Count-1 || this.visibleOptions[i+1].IsTitle))
					{
						this.visibleOptions.RemoveAt (i);
					}
					else
					{
						i++;
					}
				}
			}
		}


		private void SetDirty()
		{
			this.businessContext.NotifyExternalChanges ();
		}


		private readonly IBusinessContext					businessContext;
		private readonly DocumentOptionsEntity				documentOptionsEntity;
		private readonly PrintingOptionDictionary			options;
		private readonly List<VerboseDocumentOption>		allOptions;
		private readonly List<VerboseDocumentOption>		visibleOptions;

		private CellTable									table;
		private StaticText									documentTypesText;
		private StaticText									valueDescription;
		private StaticText									valueTypeText;
		private RadioButton									noButton;
		private RadioButton									yesButton;
		private Button										noneButton;
		private Button										allButton;
		private DocumentType								documentTypeFilter;
	}
}
