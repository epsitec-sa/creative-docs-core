//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Dialogs;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Printers;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.DocumentPrintingUnitsEditor
{
	public class ValuesController
	{
		public ValuesController(Core.Business.BusinessContext businessContext, DocumentPrintingUnitsEntity documentPrintingUnitsEntity, Dictionary<string, string> printingUnits)
		{
			this.businessContext             = businessContext;
			this.documentPrintingUnitsEntity = documentPrintingUnitsEntity;
			this.printingUnits               = printingUnits;

			this.printerUnitList = Printers.PrinterApplicationSettings.GetPrinterUnitList ();
			this.allPageTypes = DocumentPageType.GetAllDocumentPageTypes ().ToList ();

			this.editableWidgets = new List<Widget> ();
		}


		public void CreateUI(Widget parent)
		{
			this.printingUnitsFrame = new Scrollable
			{
				Parent = parent,
				PreferredWidth = 300,
				Dock = DockStyle.Left,
				HorizontalScrollerMode = ScrollableScrollerMode.HideAlways,
				VerticalScrollerMode = ScrollableScrollerMode.Auto,
				PaintViewportFrame = false,
			};

			this.printingUnitsFrame.Viewport.IsAutoFitting = true;

			this.UpdatePrinterUnitsButtons ();
		}


		public void Update()
		{
			this.UpdatePrinterUnitsButtons ();
		}


		private void UpdatePrinterUnitsButtons()
		{
			this.printingUnitsFrame.Viewport.Children.Clear ();
			this.editableWidgets.Clear ();

			string lastJob = null;
			int tabIndex = 0;
			FrameBox box = null;

			foreach (var pageType in this.allPageTypes)
			{
				if (this.printingUnits.ContainsKey (pageType.Name))
				{
					if (pageType.Job != lastJob)
					{
						box = new FrameBox
						{
							DrawFullFrame = true,
							Parent = this.printingUnitsFrame.Viewport,
							Dock = DockStyle.Top,
							Margins = new Margins (0, 0, 0, -1),
							Padding = new Margins (10),
						};
					}

					var label = new StaticText
					{
						Parent = box,
						Text = string.Concat (pageType.LongDescription, " :"),
						Dock = DockStyle.Top,
						Margins = new Margins (0, 0, 0, UIBuilder.MarginUnderLabel),
					};

					var field = new TextFieldCombo
					{
						IsReadOnly = true,
						Name = pageType.Name,
						Parent = box,
						Dock = DockStyle.Top,
						Margins = new Margins (0, 0, 0, UIBuilder.MarginUnderTextField),
						TabIndex = ++tabIndex,
					};

					this.InitializeCombo (field, this.printingUnits[pageType.Name]);
					this.editableWidgets.Add (field);

					lastJob = pageType.Job;
				}
			}
		}

		private void InitializeCombo(TextFieldCombo field, string printerUnitName)
		{
			//	Met le texte initial dans le champ.
			if (!string.IsNullOrEmpty (printerUnitName))
			{
				PrinterUnit printerUnit = this.printerUnitList.Where (x => x.LogicalName == printerUnitName).FirstOrDefault ();

				if (printerUnit != null)
				{
					field.Text = printerUnit.NiceDescription;
				}
			}

			//	Initialise le combo-menu.
			field.Items.Add ("", "");  // une première case vide

			foreach (var printerUnit in this.printerUnitList)
			{
				field.Items.Add (printerUnit.LogicalName, printerUnit.NiceDescription);  // key, value
			}

			//	Connexion des événements.
			field.SelectedItemChanged += delegate
			{
				this.printingUnits[field.Name] = field.SelectedKey;
				this.SetDirty ();
			};
		}


		private void SetDirty()
		{
			this.businessContext.NotifyExternalChanges ();
		}


		private readonly Core.Business.BusinessContext		businessContext;
		private readonly DocumentPrintingUnitsEntity		documentPrintingUnitsEntity;
		private readonly Dictionary<string, string>			printingUnits;  // "type de pages" / "unité d'impression"
		private readonly List<PrinterUnit>					printerUnitList;
		private readonly List<DocumentPageType>				allPageTypes;
		private readonly List<Widget>						editableWidgets;

		private Scrollable									printingUnitsFrame;
	}
}
