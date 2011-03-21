//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Dialogs;

using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Documents;
using Epsitec.Cresus.Core.Documents.Verbose;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Print.Controllers
{
	public class SelectController
	{
		public SelectController(IBusinessContext businessContext, DocumentPrintingUnitsEntity documentPrintingUnitsEntity, PrintingUnitDictionary printingUnits)
		{
			this.businessContext             = businessContext;
			this.documentPrintingUnitsEntity = documentPrintingUnitsEntity;
			this.printingUnits               = printingUnits;

			this.allPageTypes = VerbosePageType.GetAll ().ToList ();
		}


		public void CreateUI(Widget parent)
		{
			this.table = new CellTable
			{
				Parent = parent,
				PreferredWidth = 310,
				Dock = DockStyle.Left,
				Margins = new Margins (0, 10, 0, 0),
				StyleH = CellArrayStyles.Separator | CellArrayStyles.Header,
				StyleV = CellArrayStyles.ScrollNorm | CellArrayStyles.Separator | CellArrayStyles.SelectLine | CellArrayStyles.SelectMulti,
			};

			var rightFrame = new FrameBox
			{
				Parent = parent,
				PreferredWidth = 200,
				Dock = DockStyle.Left,
				Margins = new Margins (0, 10, 0, 0),
			};

			{
				var title = new StaticText
				{
					Parent = rightFrame,
					Text = "Types de documents :",
					Dock = DockStyle.Top,
					Margins = new Margins (0, 0, 0, 2),
				};

				var box = new FrameBox
				{
					Parent = rightFrame,
					PreferredHeight = 300,
					DrawFullFrame = true,
					Dock = DockStyle.Top,
					Padding = new Margins (10, 10, 1, 1),
				};

				this.documentTypesText = new StaticText
				{
					Parent = box,
					ContentAlignment = ContentAlignment.TopLeft,
					TextBreakMode = TextBreakMode.Hyphenate,
					Dock = DockStyle.Fill,
				};
			}

			{
				var title = new StaticText
				{
					Parent = rightFrame,
					Text = "Job d'impression :",
					Dock = DockStyle.Top,
					Margins = new Margins (0, 0, 5, 2),
				};

				var box = new FrameBox
				{
					Parent = rightFrame,
					PreferredHeight = 18,
					DrawFullFrame = true,
					Dock = DockStyle.Top,
					Padding = new Margins (10, 10, 1, 1),
				};

				this.jobTypeText = new StaticText
				{
					Parent = box,
					ContentAlignment = ContentAlignment.TopLeft,
					TextBreakMode = TextBreakMode.Hyphenate,
					Dock = DockStyle.Fill,
				};
			}

			{
				var buttonsBox = new FrameBox
				{
					Parent = rightFrame,
					DrawFullFrame = true,
					Dock = DockStyle.Fill,
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

			//	Conexion des événements.
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


		public void Update()
		{
		}


		private void ActionUse(bool value)
		{
			for (int row = 0; row < this.allPageTypes.Count; row++)
			{
				if (table.IsCellSelected (row, 0))
				{
					this.SetUsedPrintingUnit (row, value);
				}
			}

			this.UpdateTable ();
			this.UpdateWidgets ();
		}

		private void ActionAll(bool value)
		{
			for (int sel = 0; sel < this.allPageTypes.Count; sel++)
			{
				this.SetUsedPrintingUnit (sel, value);
			}

			this.UpdateTable ();
			this.UpdateWidgets ();
		}


		private void UpdateTable()
		{
			int rows = this.allPageTypes.Count;
			this.table.SetArraySize (2, rows);

			this.table.SetWidthColumn (0, 240);
			this.table.SetWidthColumn (1, 50);

			this.table.SetHeaderTextH (0, "Types de pages");
			this.table.SetHeaderTextH (1, "Utilisé");

			ContentAlignment[] alignments =
			{
				ContentAlignment.MiddleLeft,
				ContentAlignment.MiddleCenter,
			};

			for (int row=0; row<rows; row++)
			{
				this.table.FillRow (row, alignments);
				this.table.UpdateRow (row, this.GetRowTexts (row));
			}
		}

		private string[] GetRowTexts(int row)
		{
			var result = new string[2];

			result[0] = this.allPageTypes[row].ShortDescription;
			result[1] = this.GetUsedPrintingUnit (row) ? "Oui" : "";

			return result;
		}

		private void UpdateWidgets()
		{
			int count = 0;
			int usedCount = 0;
			int selCount = 0;
			int sel = -1;

			for (int row = 0; row < this.allPageTypes.Count; row++)
			{
				bool state = this.GetUsedPrintingUnit (row);

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
				this.jobTypeText.Text       = null;
			}
			else if (count > 1)
			{
				this.documentTypesText.FormattedText = TextFormatter.FormatText ("Sélection multiple").ApplyItalic ();
				this.jobTypeText.FormattedText       = TextFormatter.FormatText ("Sélection multiple").ApplyItalic ();
			}
			else
			{
				this.documentTypesText.Text = this.allPageTypes[sel].DocumentTypeDescription;
				this.jobTypeText.Text       = this.allPageTypes[sel].JobNiceDescription;
			}

			if (count == 0)
			{
				this.noButton.Enable  = false;
				this.yesButton.Enable = false;
			}
			else
			{
				this.noButton.Enable  = true;
				this.yesButton.Enable = true;
			}

			if (sel == -1 || diff)
			{
				this.noButton.ActiveState  = ActiveState.No;
				this.yesButton.ActiveState = ActiveState.No;
			}
			else
			{
				bool state = this.GetUsedPrintingUnit (sel);
				this.noButton.ActiveState  = !state ? ActiveState.Yes : ActiveState.No;
				this.yesButton.ActiveState =  state ? ActiveState.Yes : ActiveState.No;
			}

			this.noneButton.Enable = (selCount != 0);
			this.allButton.Enable  = (selCount != this.allPageTypes.Count);
		}


		private void SetUsedPrintingUnit(int row, bool value)
		{
			var type = this.allPageTypes[row].Type;

			if (value)  // utilise l'unité ?
			{
				if (!this.GetUsedPrintingUnit (row))
				{
					this.printingUnits[type] = "";
					this.SetDirty ();
				}
			}
			else  // n'utilise pas l'unité ?
			{
				if (this.GetUsedPrintingUnit (row))
				{
					this.printingUnits[type] = null;
					this.SetDirty ();
				}
			}
		}

		private bool GetUsedPrintingUnit(int row)
		{
			var type = this.allPageTypes[row].Type;
			return this.printingUnits.ContainsPageType (type);
		}


		private void SetDirty()
		{
			this.businessContext.NotifyExternalChanges ();
		}


		private readonly IBusinessContext					businessContext;
		private readonly DocumentPrintingUnitsEntity		documentPrintingUnitsEntity;
		private readonly PrintingUnitDictionary			printingUnits;
		private readonly List<VerbosePageType>				allPageTypes;

		private CellTable									table;
		private StaticText									documentTypesText;
		private StaticText									jobTypeText;
		private RadioButton									noButton;
		private RadioButton									yesButton;
		private Button										noneButton;
		private Button										allButton;
	}
}
