﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Cresus.Assets.Data.Helpers;
using Epsitec.Cresus.Assets.Data.Serialization;
using Epsitec.Cresus.Assets.Export.Helpers;

namespace Epsitec.Cresus.Assets.Server.Export
{
	/// <summary>
	/// Paramètres pour PdfExport.
	/// </summary>
	public class PdfExportProfile : AbstractExportProfile
	{
		public PdfExportProfile(PdfStyle style, Size pageSize, Margins pageMargins, Margins cellMargins,
			ExportFont font, double fontSize, bool automaticColumnWidths,
			string header, string footer, string indent, string watermark)
		{
			this.Style                 = style;
			this.PageSize              = pageSize;
			this.PageMargins           = pageMargins;
			this.CellMargins           = cellMargins;
			this.Font                  = font;
			this.FontSize              = fontSize;
			this.AutomaticColumnWidths = automaticColumnWidths;
			this.Header                = header;
			this.Footer                = footer;
			this.Indent                = indent;
			this.Watermark             = watermark;
		}

		public PdfExportProfile(System.Xml.XmlReader reader)
		{
			while (reader.Read ())
			{
				if (reader.NodeType == System.Xml.XmlNodeType.Element)
				{
					if (reader.Name == X.Style)
					{
						this.Style = new PdfStyle (reader);
					}
					else if (reader.Name == X.Params)
					{
						this.PageSize.Width        = (double) IOHelpers.ReadDecimalAttribute (reader, X.Attr.PageSize_Width);
						this.PageSize.Height       = (double) IOHelpers.ReadDecimalAttribute (reader, X.Attr.PageSize_Height);

						this.PageMargins.Left      = (double) IOHelpers.ReadDecimalAttribute (reader, X.Attr.PageMargins_Left);
						this.PageMargins.Right     = (double) IOHelpers.ReadDecimalAttribute (reader, X.Attr.PageMargins_Right);
						this.PageMargins.Top       = (double) IOHelpers.ReadDecimalAttribute (reader, X.Attr.PageMargins_Top);
						this.PageMargins.Bottom    = (double) IOHelpers.ReadDecimalAttribute (reader, X.Attr.PageMargins_Bottom);

						this.CellMargins.Left      = (double) IOHelpers.ReadDecimalAttribute (reader, X.Attr.CellMargins_Left);
						this.CellMargins.Right     = (double) IOHelpers.ReadDecimalAttribute (reader, X.Attr.CellMargins_Right);
						this.CellMargins.Top       = (double) IOHelpers.ReadDecimalAttribute (reader, X.Attr.CellMargins_Top);
						this.CellMargins.Bottom    = (double) IOHelpers.ReadDecimalAttribute (reader, X.Attr.CellMargins_Bottom);

						this.Font                  = (ExportFont) IOHelpers.ReadTypeAttribute (reader, X.Attr.Font, typeof (ExportFont));
						this.FontSize              = (double) IOHelpers.ReadDecimalAttribute (reader, X.Attr.FontSize);
						this.AutomaticColumnWidths = IOHelpers.ReadBoolAttribute   (reader, X.Attr.AutomaticColumnWidths);
						this.Header                = IOHelpers.ReadStringAttribute (reader, X.Attr.Header);
						this.Footer                = IOHelpers.ReadStringAttribute (reader, X.Attr.Footer);
						this.Indent                = IOHelpers.ReadStringAttribute (reader, X.Attr.Indent);
						this.Watermark             = IOHelpers.ReadStringAttribute (reader, X.Attr.Watermark);

						reader.Read ();
					}
				}
				else if (reader.NodeType == System.Xml.XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

	
		public static PdfExportProfile Default =
			new PdfExportProfile (PdfStyle.Factory (PdfPredefinedStyle.Frameless), new Size (297.0, 210.0),
				new Margins (10.0), new Margins (1.0), ExportFont.Arial, 10.0, false,
				TagConverters.Compile ("Crésus <TIRET> <TITLE>"),
				TagConverters.Compile ("Epsitec SA"),
				TagConverters.Compile ("<BULLET><SPACE><SPACE><SPACE>"),
				TagConverters.Compile ("SPECIMEN"));

		public string							FinalHeader
		{
			get
			{
				return TagConverters.GetFinalText (this.Header);
			}
		}

		public string							FinalFooter
		{
			get
			{
				return TagConverters.GetFinalText (this.Footer);
			}
		}

		public string							FinalIndent
		{
			get
			{
				return TagConverters.GetFinalText (this.Indent);
			}
		}

		public string							FinalWatermark
		{
			get
			{
				return TagConverters.GetFinalText (this.Watermark);
			}
		}


		public override void Serialize(System.Xml.XmlWriter writer, string name)
		{
			writer.WriteStartElement (name);

			this.Style.Serialize (writer, X.Style);

			writer.WriteStartElement (X.Params);
			IOHelpers.WriteDecimalAttribute (writer, X.Attr.PageSize_Width,        (decimal) this.PageSize.Width);
			IOHelpers.WriteDecimalAttribute (writer, X.Attr.PageSize_Height,       (decimal) this.PageSize.Height);

			IOHelpers.WriteDecimalAttribute (writer, X.Attr.PageMargins_Left,      (decimal) this.PageMargins.Left);
			IOHelpers.WriteDecimalAttribute (writer, X.Attr.PageMargins_Right,     (decimal) this.PageMargins.Right);
			IOHelpers.WriteDecimalAttribute (writer, X.Attr.PageMargins_Top,       (decimal) this.PageMargins.Top);
			IOHelpers.WriteDecimalAttribute (writer, X.Attr.PageMargins_Bottom,    (decimal) this.PageMargins.Bottom);

			IOHelpers.WriteDecimalAttribute (writer, X.Attr.CellMargins_Left,      (decimal) this.CellMargins.Left);
			IOHelpers.WriteDecimalAttribute (writer, X.Attr.CellMargins_Right,     (decimal) this.CellMargins.Right);
			IOHelpers.WriteDecimalAttribute (writer, X.Attr.CellMargins_Top,       (decimal) this.CellMargins.Top);
			IOHelpers.WriteDecimalAttribute (writer, X.Attr.CellMargins_Bottom,    (decimal) this.CellMargins.Bottom);

			IOHelpers.WriteTypeAttribute    (writer, X.Attr.Font,                  this.Font);
			IOHelpers.WriteDecimalAttribute (writer, X.Attr.FontSize,              (decimal) this.FontSize);
			IOHelpers.WriteBoolAttribute    (writer, X.Attr.AutomaticColumnWidths, this.AutomaticColumnWidths);
			IOHelpers.WriteStringAttribute  (writer, X.Attr.Header,                this.Header);
			IOHelpers.WriteStringAttribute  (writer, X.Attr.Footer,                this.Footer);
			IOHelpers.WriteStringAttribute  (writer, X.Attr.Indent,                this.Indent);
			IOHelpers.WriteStringAttribute  (writer, X.Attr.Watermark,             this.Watermark);
			writer.WriteEndElement ();

			writer.WriteEndElement ();
		}


		public readonly PdfStyle				Style;
		public readonly Size					PageSize;
		public readonly Margins					PageMargins;
		public readonly Margins					CellMargins;
		public readonly ExportFont				Font;
		public readonly double					FontSize;
		public readonly bool					AutomaticColumnWidths;
		public readonly string					Header;
		public readonly string					Footer;
		public readonly string					Indent;
		public readonly string					Watermark;
	}
}