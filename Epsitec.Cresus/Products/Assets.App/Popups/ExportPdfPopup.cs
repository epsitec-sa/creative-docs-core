//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Export;
using Epsitec.Cresus.Assets.App.Popups.StackedControllers;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.Export;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Popups
{
	/// <summary>
	/// Popup permettant de choisir les paramètres pour l'exportation au format pdf.
	/// </summary>
	public class ExportPdfPopup : AbstractStackedPopup
	{
		private ExportPdfPopup(DataAccessor accessor)
			: base (accessor)
		{
			this.title = Res.Strings.Popup.ExportPdf.Title.ToString ();

			var list = new List<StackedControllerDescription> ();

			list.Add (new StackedControllerDescription  // 0
			{
				StackedControllerType = StackedControllerType.PdfStyle,
				Label                 = Res.Strings.Popup.ExportPdf.Style.ToString (),
				BottomMargin          = 10,
			});

			list.Add (new StackedControllerDescription  // 1
			{
				StackedControllerType = StackedControllerType.PageSize,
				DecimalFormat         = DecimalFormat.Millimeters,
				Label                 = Res.Strings.Popup.ExportPdf.PageFormat.ToString (),
			});

			list.Add (new StackedControllerDescription  // 2
			{
				StackedControllerType = StackedControllerType.Margins,
				DecimalFormat         = DecimalFormat.Millimeters,
				Label                 = Res.Strings.Popup.ExportPdf.PageMargins.ToString (),
			});

			list.Add (new StackedControllerDescription  // 3
			{
				StackedControllerType = StackedControllerType.Combo,
				Label                 = Res.Strings.Popup.ExportPdf.FontName.ToString (),
				MultiLabels           = ExportFontHelpers.Labels,
				Width                 = 240,
			});

			list.Add (new StackedControllerDescription  // 4
			{
				StackedControllerType = StackedControllerType.Decimal,
				DecimalFormat         = DecimalFormat.Real,
				Label                 = Res.Strings.Popup.ExportPdf.FontSize.ToString (),
			});

			list.Add (new StackedControllerDescription  // 5
			{
				StackedControllerType = StackedControllerType.Margins,
				DecimalFormat         = DecimalFormat.Millimeters,
				Label                 = Res.Strings.Popup.ExportPdf.CellMargins.ToString (),
				BottomMargin          = 10,
			});

			list.Add (new StackedControllerDescription  // 6
			{
				StackedControllerType = StackedControllerType.Bool,
				Label                 = Res.Strings.Popup.ExportPdf.AutomaticColumnWidths.ToString (),
				BottomMargin          = 10,
			});

			list.Add (new StackedControllerDescription  // 7
			{
				StackedControllerType = StackedControllerType.Text,
				Label                 = Res.Strings.Popup.ExportPdf.Header.ToString (),
				Width                 = 240,
			});

			list.Add (new StackedControllerDescription  // 8
			{
				StackedControllerType = StackedControllerType.Text,
				Label                 = Res.Strings.Popup.ExportPdf.Footer.ToString (),
				Width                 = 240,
			});

			list.Add (new StackedControllerDescription  // 9
			{
				StackedControllerType = StackedControllerType.Text,
				Label                 = Res.Strings.Popup.ExportPdf.Indent.ToString (),
				Width                 = 240,
			});

			list.Add (new StackedControllerDescription  // 10
			{
				StackedControllerType = StackedControllerType.Text,
				Label                 = Res.Strings.Popup.ExportPdf.Watermark.ToString (),
				Width                 = 240,
			});

			this.SetDescriptions (list);

			this.defaultAcceptButtonName = Res.Strings.Popup.Button.Export.ToString ();
			this.defaultControllerRankFocus = 0;
		}


		private PdfExportProfile				Profile
		{
			get
			{
				PdfStyle	style;
				Size		pageSize;
				Margins		pageMargins;
				ExportFont	font;
				double		fontSize;
				Margins		cellMargins;
				bool		automaticColumnWidths;
				string		header;
				string		footer;
				string		indent;
				string		watermark;

				{
					var controller = this.GetController (0) as PdfStyleStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					style = controller.Value;
				}

				{
					var controller = this.GetController (1) as PageSizeStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					pageSize = controller.Value;
				}

				{
					var controller = this.GetController (2) as MarginsStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					pageMargins = controller.Value;
				}

				{
					var controller = this.GetController (3) as ComboStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					font = ExportFontHelpers.IntToFont (controller.Value.GetValueOrDefault (-1));
				}

				{
					var controller = this.GetController (4) as DecimalStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					fontSize = (double) controller.Value.GetValueOrDefault ();
				}

				{
					var controller = this.GetController (5) as MarginsStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					cellMargins = controller.Value;
				}

				{
					var controller = this.GetController (6) as BoolStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					automaticColumnWidths = controller.Value;
				}

				{
					var controller = this.GetController (7) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					header = controller.Value;
				}

				{
					var controller = this.GetController (8) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					footer = controller.Value;
				}

				{
					var controller = this.GetController (9) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					indent = controller.Value;
				}

				{
					var controller = this.GetController (10) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					watermark = controller.Value;
				}

				return new PdfExportProfile (style, pageSize, pageMargins, cellMargins, font, fontSize, automaticColumnWidths, header, footer, indent, watermark);
			}
			set
			{
				{
					var controller = this.GetController (0) as PdfStyleStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.Style;
				}

				{
					var controller = this.GetController (1) as PageSizeStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.PageSize;
				}

				{
					var controller = this.GetController (2) as MarginsStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.PageMargins;
				}

				{
					var controller = this.GetController (3) as ComboStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = ExportFontHelpers.FontToInt (value.Font);
				}

				{
					var controller = this.GetController (4) as DecimalStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = (decimal) value.FontSize;
				}

				{
					var controller = this.GetController (5) as MarginsStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.CellMargins;
				}

				{
					var controller = this.GetController (6) as BoolStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.AutomaticColumnWidths;
				}

				{
					var controller = this.GetController (7) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.Header;
				}

				{
					var controller = this.GetController (8) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.Footer;
				}

				{
					var controller = this.GetController (9) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.Indent;
				}

				{
					var controller = this.GetController (10) as TextStackedController;
					System.Diagnostics.Debug.Assert (controller != null);
					controller.Value = value.Watermark;
				}
			}
		}


		#region Helpers
		public static void Show(Widget target, DataAccessor accessor, PdfExportProfile profile, System.Action<PdfExportProfile> action)
		{
			//	Affiche le Popup.
			var popup = new ExportPdfPopup (accessor)
			{
				Profile = profile,
			};

			popup.Create (target, leftOrRight: true);

			popup.ButtonClicked += delegate (object sender, string name)
			{
				if (name == "ok")
				{
					action (popup.Profile);
				}
			};
		}
		#endregion
	}
}