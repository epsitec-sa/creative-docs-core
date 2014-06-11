﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Cresus.Assets.App.Export;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.Export;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Popups
{
	/// <summary>
	/// Popup permettant de choisir les paramètres pour l'exportation au format pdf.
	/// </summary>
	public class ExportPdfPopup : StackedPopup
	{
		public ExportPdfPopup(DataAccessor accessor)
			: base (accessor)
		{
			this.title = "Exportation des données au format PDF";

			var list = new List<StackedControllerDescription> ();

			list.Add (new StackedControllerDescription  // 0
			{
				StackedControllerType = StackedControllerType.PageSize,
				DecimalFormat         = DecimalFormat.Millimeters,
				Label                 = "Format des pages",
			});

			list.Add (new StackedControllerDescription  // 1
			{
				StackedControllerType = StackedControllerType.Margins,
				DecimalFormat         = DecimalFormat.Millimeters,
				Label                 = "Marges",
			});

			list.Add (new StackedControllerDescription  // 2
			{
				StackedControllerType = StackedControllerType.Decimal,
				DecimalFormat         = DecimalFormat.Real,
				Label                 = "Taille de police",
			});

			list.Add (new StackedControllerDescription  // 3
			{
				StackedControllerType = StackedControllerType.Decimal,
				DecimalFormat         = DecimalFormat.Millimeters,
				Label                 = "Marges des cellules",
				BottomMargin          = 10,
			});

			list.Add (new StackedControllerDescription  // 4
			{
				StackedControllerType = StackedControllerType.Bool,
				Label                 = "Largeurs des colonnes selon le contenu",
			});

			list.Add (new StackedControllerDescription  // 5
			{
				StackedControllerType = StackedControllerType.Bool,
				Label                 = "Une ligne sur deux en gris",
			});

			list.Add (new StackedControllerDescription  // 6
			{
				StackedControllerType = StackedControllerType.Text,
				Label                 = "En-tête",
				Width                 = 240,
			});

			list.Add (new StackedControllerDescription  // 7
			{
				StackedControllerType = StackedControllerType.Text,
				Label                 = "Pied de page",
				Width                 = 240,
			});

			list.Add (new StackedControllerDescription  // 8
			{
				StackedControllerType = StackedControllerType.Text,
				Label                 = "Indentation",
				Width                 = 240,
			});

			list.Add (new StackedControllerDescription  // 9
			{
				StackedControllerType = StackedControllerType.Text,
				Label                 = "Filigrane",
				Width                 = 240,
			});

			this.SetDescriptions (list);
		}


		public PdfExportProfile					Profile
		{
			get
			{
				Size		pageSize;
				Margins		pageMargins;
				double		fontSize;
				double		cellMargins;
				bool		automaticColumnWidths;
				bool		evenOddGrey;
				string		header;
				string		footer;
				string		indent;
				string		watermark;

				{
					var controller = this.GetController (0) as PageSizeStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					pageSize = controller.Value;
				}

				{
					var controller = this.GetController (1) as MarginsStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					pageMargins = controller.Value;
				}

				{
					var controller = this.GetController (2) as DecimalStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					fontSize = (double) controller.Value.GetValueOrDefault ();
				}

				{
					var controller = this.GetController (3) as DecimalStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					cellMargins = (double) controller.Value.GetValueOrDefault ();
				}

				{
					var controller = this.GetController (4) as BoolStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					automaticColumnWidths = controller.Value;
				}

				{
					var controller = this.GetController (5) as BoolStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					evenOddGrey = controller.Value;
				}

				{
					var controller = this.GetController (6) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					header = controller.Value;
				}

				{
					var controller = this.GetController (7) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					footer = controller.Value;
				}

				{
					var controller = this.GetController (8) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					indent = Converters.EditableToInternal (controller.Value);
				}

				{
					var controller = this.GetController (9) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					watermark = controller.Value;
				}

				return new PdfExportProfile (pageSize, pageMargins, new Margins (cellMargins), fontSize, automaticColumnWidths, evenOddGrey, header, footer, indent, watermark);
			}
			set
			{
				{
					var controller = this.GetController (0) as PageSizeStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.PageSize;
				}

				{
					var controller = this.GetController (1) as MarginsStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.PageMargins;
				}

				{
					var controller = this.GetController (2) as DecimalStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = (decimal) value.FontSize;
				}

				{
					var controller = this.GetController (3) as DecimalStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = (decimal) value.CellMargins.Left;
				}

				{
					var controller = this.GetController (4) as BoolStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.AutomaticColumnWidths;
				}

				{
					var controller = this.GetController (5) as BoolStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.EvenOddGrey;
				}

				{
					var controller = this.GetController (6) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.Header;
				}

				{
					var controller = this.GetController (7) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.Footer;
				}

				{
					var controller = this.GetController (8) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = Converters.InternalToEditable (value.Indent);
				}

				{
					var controller = this.GetController (9) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.Watermark;
				}
			}
		}


		protected override void UpdateWidgets(StackedControllerDescription description)
		{
			this.okButton.Text = "Exporter";
		}
	}
}