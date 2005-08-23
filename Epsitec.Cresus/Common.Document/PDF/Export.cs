using Epsitec.Common.Drawing;

namespace Epsitec.Common.Document.PDF
{
	public enum TypeComplexSurface
	{
		None            = -1,
		ExtGState       = 0,
		ExtGStateP1     = 1,
		ExtGStateP2     = 2,
		ExtGStateSmooth = 3,
		ShadingColor    = 4,
		ShadingGray     = 5,
		XObject         = 6,
		XObjectSmooth   = 7,
		XObjectMask     = 7,
	}

	public enum TypeFont
	{
		Base      = 0,
		Encoding  = 1,
		CharProcs = 2,
		Widths    = 3,
		ToUnicode = 4,
	}

	public enum ColorConversion
	{
		None   = 0,
		ToRGB  = 1,
		ToCMYK = 2,
		ToGray = 3,
	}

	/// <summary>
	/// La classe Export impl�mente la publication d'un document PDF.
	/// [*] = documentation PDF Reference, version 1.6, fifth edition, 1236 pages
	/// </summary>
	/// 
	public class Export
	{
		protected enum TypeFunction
		{
			SampledColor = 0,
			SampledAlpha = 1,
			Color1       = 2,	// red or cyan or gray
			Color2       = 3,	// green or magenta
			Color3       = 4,	// blue or yellow
			Color4       = 5,	// black
			Alpha        = 6,
		}

		public Export(Document document)
		{
			this.document = document;
		}

		// Exporte le document dans un fichier.
		public string FileExport(string filename)
		{
			Settings.ExportPDFInfo info = this.document.Settings.ExportPDFInfo;
			this.colorConversion = info.ColorConversion;

			this.complexSurfaces = new System.Collections.ArrayList();
			this.imageSurfaces   = new System.Collections.ArrayList();
			this.characterList   = new System.Collections.Hashtable();
			this.fontList        = new System.Collections.Hashtable();

			Port port = new Port(this.complexSurfaces, this.imageSurfaces, info.TextCurve ? null : this.fontList);
			port.PushColorModifier(new ColorModifier(this.FinalOutputColorModifier));

			// Cr�e le DrawingContext utilis� pour l'exportation.
			DrawingContext drawingContext;
			drawingContext = new DrawingContext(this.document, null);
			drawingContext.ContainerSize = this.document.Size;
			drawingContext.PreviewActive = true;

			int max = this.document.Modifier.PrintableTotalPages();

			Settings.PrintRange range = info.PageRange;
			int from = 1;
			int to   = max;

			if ( range == Settings.PrintRange.FromTo )
			{
				from = info.PageFrom;
				to   = info.PageTo;
			}
			else if ( range == Settings.PrintRange.Current )
			{
				int cp = this.document.Modifier.ActiveViewer.DrawingContext.CurrentPage;
				Objects.Page page = this.document.GetObjects[cp] as Objects.Page;
				if ( page.MasterType == Objects.MasterType.Slave )
				{
					from = page.Rank+1;
					to   = from;
				}
				else
				{
					from = cp;
					to   = cp;
				}
			}
			from = System.Math.Min(from, max);
			to   = System.Math.Min(to,   max);

			if ( from > to )
			{
				Misc.Swap(ref from, ref to);
			}
			int total = to-from+1;

			this.FoundComplexSurfaces(port, drawingContext, from, to);

			// Cr�e et ouvre le fichier.
			if ( System.IO.File.Exists(filename) )
			{
				System.IO.File.Delete(filename);
			}

			Writer writer = new Writer(filename);

			// Objet racine du document.
			writer.WriteObjectDef("Root");
			writer.WriteString("<< /Type /Catalog /Outlines ");
			writer.WriteObjectRef("HeaderOutlines");
			writer.WriteString("/Pages ");
			writer.WriteObjectRef("HeaderPages");
			writer.WriteLine(">> endobj");

			// Objet outlines.
			writer.WriteObjectDef("HeaderOutlines");
			writer.WriteLine("<< /Type /Outlines /Count 0 >> endobj");

			// Objet d�crivant le format de la page.
			double pageWidth  = this.document.Size.Width;
			double pageHeight = this.document.Size.Height;
			Point pageOffset = new Point(0.0, 0.0);

			if ( info.Debord > 0.0 )
			{
				pageWidth  += info.Debord*2.0;
				pageHeight += info.Debord*2.0;
				pageOffset.X += info.Debord;
				pageOffset.Y += info.Debord;
			}

			if ( info.Target )  // traits de coupe ?
			{
				pageWidth  += info.TargetLength*2.0;
				pageHeight += info.TargetLength*2.0;
				pageOffset.X += info.TargetLength;
				pageOffset.Y += info.TargetLength;
			}

			writer.WriteObjectDef("HeaderFormat");
			writer.WriteString("[0 0 ");
			writer.WriteString(Port.StringValue(pageWidth*Export.mm2in));
			writer.WriteString(" ");
			writer.WriteString(Port.StringValue(pageHeight*Export.mm2in));
			writer.WriteLine("] endobj");

			// Objet donnant la liste des pages.
			writer.WriteObjectDef("HeaderPages");
			writer.WriteString("<< /Type /Pages /Kids [ ");
			for ( int page=from ; page<=to ; page++ )
			{
				writer.WriteObjectRef(Export.NamePage(page));
			}
			writer.WriteLine(string.Format("] /Count {0} >> endobj", total));

			// Un objet pour chaque page.
			for ( int page=from ; page<=to ; page++ )
			{
				writer.WriteObjectDef(Export.NamePage(page));
				writer.WriteString("<< /Type /Page /Parent ");
				writer.WriteObjectRef("HeaderPages");
				writer.WriteString("/MediaBox ");
				writer.WriteObjectRef("HeaderFormat");
				writer.WriteString("/Resources ");
				writer.WriteObjectRef(Export.NameResources(page));
				writer.WriteString("/Contents ");
				writer.WriteObjectRef(Export.NameContent(page));
				writer.WriteLine(">> endobj");
			}

			// Un objet pour les ressources de chaque page.
			for ( int page=from ; page<=to ; page++ )
			{
				writer.WriteObjectDef(Export.NameResources(page));
				writer.WriteString("<< /ProcSet [/PDF /Text /ImageB /ImageC] ");

				int tcs = this.TotalComplexSurface(page);
				if ( tcs > 0 || this.imageSurfaces.Count > 0 )
				{
					writer.WriteString("/ExtGState << ");
					writer.WriteString(Export.ShortNameComplexSurface(0, TypeComplexSurface.ExtGState));
					writer.WriteObjectRef(Export.NameComplexSurface(0, TypeComplexSurface.ExtGState));
					for ( int index=0 ; index<tcs ; index++ )
					{
						ComplexSurface cs = this.GetComplexSurface(page, index);
						System.Diagnostics.Debug.Assert(cs != null);
						if ( cs.Type == Type.TransparencyRegular  ||
							 cs.Type == Type.TransparencyGradient ||
							 cs.IsSmooth                          )
						{
							writer.WriteString(Export.ShortNameComplexSurface(cs.Id, TypeComplexSurface.ExtGState));
							writer.WriteObjectRef(Export.NameComplexSurface(cs.Id, TypeComplexSurface.ExtGState));
						}
						if ( cs.Type == Type.TransparencyPattern  )
						{
							writer.WriteString(Export.ShortNameComplexSurface(cs.Id, TypeComplexSurface.ExtGStateP1));
							writer.WriteObjectRef(Export.NameComplexSurface(cs.Id, TypeComplexSurface.ExtGStateP1));

							writer.WriteString(Export.ShortNameComplexSurface(cs.Id, TypeComplexSurface.ExtGStateP2));
							writer.WriteObjectRef(Export.NameComplexSurface(cs.Id, TypeComplexSurface.ExtGStateP2));
						}
					}
					writer.WriteString(">> ");

					writer.WriteString("/Shading << ");
					for ( int index=0 ; index<tcs ; index++ )
					{
						ComplexSurface cs = this.GetComplexSurface(page, index);
						System.Diagnostics.Debug.Assert(cs != null);
						if ( cs.Type == Type.OpaqueGradient       ||
							 cs.Type == Type.TransparencyGradient )
						{
							writer.WriteString(Export.ShortNameComplexSurface(cs.Id, TypeComplexSurface.ShadingColor));
							writer.WriteObjectRef(Export.NameComplexSurface(cs.Id, TypeComplexSurface.ShadingColor));
						}
					}
					writer.WriteString(">> ");

					writer.WriteString("/XObject << ");
					for ( int index=0 ; index<tcs ; index++ )
					{
						ComplexSurface cs = this.GetComplexSurface(page, index);
						System.Diagnostics.Debug.Assert(cs != null);
						if ( cs.Type == Type.OpaquePattern       ||
							 cs.Type == Type.TransparencyPattern )
						{
							writer.WriteString(Export.ShortNameComplexSurface(cs.Id, TypeComplexSurface.XObject));
							writer.WriteObjectRef(Export.NameComplexSurface(cs.Id, TypeComplexSurface.XObject));
						}
					}
					foreach ( ImageSurface image in this.imageSurfaces )
					{
						if ( image.DrawingImage == null )  continue;
						writer.WriteString(Export.ShortNameComplexSurface(image.Id, TypeComplexSurface.XObject));
						writer.WriteObjectRef(Export.NameComplexSurface(image.Id, TypeComplexSurface.XObject));
					}
					writer.WriteString(">> ");
				}

				if ( !info.TextCurve )
				{
					writer.WriteString("/Font << ");
					foreach ( System.Collections.DictionaryEntry dict in this.fontList )
					{
						FontList font = dict.Key as FontList;
						int totalPages = (font.CharacterCount+Export.charPerFont-1)/Export.charPerFont;
						for ( int fontPage=0 ; fontPage<totalPages ; fontPage++ )
						{
							writer.WriteString(Export.ShortNameFont(font.Id, fontPage));
							writer.WriteObjectRef(Export.NameFont(font.Id, fontPage, TypeFont.Base));
						}
					}
					writer.WriteString(">> ");
				}

				//?writer.WriteLine("/ColorSpace << /Cs [/Pattern /DeviceRGB] >> ");
				writer.WriteLine(">> endobj");
			}

			// Cr�e les objets de d�finition.
			this.CreateComplexSurface(writer, port, drawingContext);
			this.CreateImageSurface(writer, port);

			if ( !info.TextCurve )
			{
				this.CreateFont(writer);
			}

			// Un objet pour le contenu de chaque page.
			for ( int page=from ; page<=to ; page++ )
			{
				port.Reset();

				// Matrice de transformation globale:
				Transform gt = port.Transform;
				gt.Translate(pageOffset);  // translation si d�bord
				gt.Scale(Export.mm2in);  // unit� = 0.1mm
				port.Transform = gt;

				System.Collections.ArrayList layers = this.ComputeLayers(page-1);
				foreach ( Objects.Layer layer in layers )
				{
					Properties.ModColor modColor = layer.PropertyModColor;
					port.PushColorModifier(new ColorModifier(modColor.ModifyColor));
					drawingContext.IsDimmed = (layer.Print == Objects.LayerPrint.Dimmed);
					port.PushColorModifier(new ColorModifier(drawingContext.DimmedColor));

					foreach ( Objects.Abstract obj in this.document.Deep(layer) )
					{
						if ( obj.IsHide )  continue;  // objet cach� ?

						obj.DrawGeometry(port, drawingContext);
					}

					port.PopColorModifier();
					port.PopColorModifier();
				}

				this.DrawTarget(port, info);  // traits de coupe

				string pdf = port.GetPDF();
				writer.WriteObjectDef(Export.NameContent(page));
				writer.WriteLine(string.Format("<< {0} >>", Port.StringLength(pdf.Length)));
				writer.WriteLine("stream");
				writer.WriteString(pdf);
				writer.WriteLine("endstream endobj");
			}

			writer.Flush();
			this.FlushComplexSurface();
			this.FlushImageSurface();
			this.FlushFont();

			return "";  // ok
		}

		protected static string NamePage(int page)
		{
			return string.Format("HeaderPage{0}", page);
		}

		protected static string NameResources(int page)
		{
			return string.Format("HeaderResources{0}", page);
		}

		protected static string NameContent(int page)
		{
			return string.Format("HeaderContent{0}", page);
		}

		protected static string NameComplexSurface(int id, TypeComplexSurface type)
		{
			return string.Format("HeaderComplexSurface{0}", id*10+(int)type);
		}

		public static string ShortNameComplexSurface(int id, TypeComplexSurface type)
		{
			return string.Format("/S{0} ", id*10+(int)type);
		}

		protected static string NameFont(int id, int fontPage, TypeFont type)
		{
			return string.Format("HeaderFont{0}", id*100 + fontPage*10 + (int)type);
		}

		public static string ShortNameFont(int id, int fontPage)
		{
			return string.Format("/F{0} ", id*10 + fontPage);
		}

		protected static string NameCharacter(int id, int fontPage, int character)
		{
			return string.Format("HeaderCharacter{0}_{1}_{2}", id, fontPage, character);
		}

		protected static string ShortNameCharacter(int id, int fontPage, int character)
		{
			return string.Format("/C{0}_{1}_{2} ", id, fontPage, character);
		}

		protected static string NameFunction(int id, TypeFunction type)
		{
			return string.Format("HeaderFunction{0}", id*10+(int)type);
		}


		// Modification finale d'une couleur en fonction du mode de sortie.
		protected void FinalOutputColorModifier(ref RichColor color)
		{
			if ( this.colorConversion == PDF.ColorConversion.ToRGB )
			{
				color.ColorSpace = ColorSpace.RGB;
			}
			else if ( this.colorConversion == PDF.ColorConversion.ToCMYK )
			{
				color.ColorSpace = ColorSpace.CMYK;
			}
			else if ( this.colorConversion == PDF.ColorConversion.ToGray )
			{
				color.ColorSpace = ColorSpace.Gray;
			}
		}


		// Dessine les traits de coupe.
		protected void DrawTarget(Port port, Settings.ExportPDFInfo info)
		{
			if ( !info.Target )  return;

			double width  = this.document.Size.Width;
			double height = this.document.Size.Height;
			double debord = info.Debord;
			double length = info.TargetLength;

			Path path = new Path();

			// Traits horizontaux.
			path.MoveTo(0.0-debord, 0.0);
			path.LineTo(0.0-debord-length, 0.0);

			path.MoveTo(0.0-debord, height);
			path.LineTo(0.0-debord-length, height);

			path.MoveTo(width+debord, 0.0);
			path.LineTo(width+debord+length, 0.0);

			path.MoveTo(width+debord, height);
			path.LineTo(width+debord+length, height);

			// Traits verticaux.
			path.MoveTo(0.0, 0.0-debord);
			path.LineTo(0.0, 0.0-debord-length);

			path.MoveTo(width, 0.0-debord);
			path.LineTo(width, 0.0-debord-length);

			path.MoveTo(0.0, height+debord);
			path.LineTo(0.0, height+debord+length);

			path.MoveTo(width, height+debord);
			path.LineTo(width, height+debord+length);

			port.LineWidth = info.TargetWidth;
			port.RichColor = RichColor.FromCMYK(1.0, 1.0, 1.0, 1.0);  // noir de rep�rage
			port.PaintOutline(path);
		}


		#region ComplexSurface
		// Trouve toutes les surfaces complexes dans toutes les pages.
		protected void FoundComplexSurfaces(Port port, DrawingContext drawingContext, int from, int to)
		{
			int id = 1;
			for ( int page=from ; page<=to ; page++ )
			{
				System.Collections.ArrayList layers = this.ComputeLayers(page-1);
				foreach ( Objects.Layer layer in layers )
				{
					Properties.ModColor modColor = layer.PropertyModColor;
					port.PushColorModifier(new ColorModifier(modColor.ModifyColor));
					drawingContext.IsDimmed = (layer.Print == Objects.LayerPrint.Dimmed);
					port.PushColorModifier(new ColorModifier(drawingContext.DimmedColor));

					foreach ( Objects.Abstract obj in this.document.Deep(layer) )
					{
						if ( obj.IsHide )  continue;  // objet cach� ?

						System.Collections.ArrayList list = obj.GetComplexSurfacesPDF(port);
						int total = list.Count;
						for ( int rank=0 ; rank<total ; rank++ )
						{
							if ( list[rank] is Properties.Gradient )
							{
								Properties.Gradient gradient = list[rank] as Properties.Gradient;
								Properties.Line stroke = null;

								if ( gradient.Type == Properties.Type.LineColor )
								{
									stroke = obj.PropertyLineMode;
								}

								ComplexSurface cs = new ComplexSurface(page, port, layer, obj, gradient, stroke, rank, id++);
								this.complexSurfaces.Add(cs);
							}

							if ( list[rank] is Properties.Font )
							{
								Properties.Font font = list[rank] as Properties.Font;

								ComplexSurface cs = new ComplexSurface(page, port, layer, obj, font, null, rank, id++);
								this.complexSurfaces.Add(cs);
							}
						}

						if ( obj is Objects.Image )
						{
							Objects.Image objImage = obj as Objects.Image;
							Properties.Image propImage = obj.PropertyImage;
							System.Diagnostics.Debug.Assert(propImage != null);
							string filename = propImage.Filename;

							ImageSurface image = ImageSurface.Search(this.imageSurfaces, filename);
							if ( image == null )
							{
								image = new ImageSurface(filename, id++);
								this.imageSurfaces.Add(image);
							}
						}

						obj.FillOneCharList(this.characterList);
					}

					port.PopColorModifier();
					port.PopColorModifier();
				}
			}

			FontList.CreateFonts(this.fontList, this.characterList);
		}

		// Calcule le nombre de surfaces complexes dans une page.
		protected int TotalComplexSurface(int page)
		{
			int total = 0;
			for ( int i=0 ; i<this.complexSurfaces.Count ; i++ )
			{
				ComplexSurface cs = this.complexSurfaces[i] as ComplexSurface;
				if ( cs.Page == page )  total++;
			}
			return total;
		}

		// Donne une surface complexe.
		protected ComplexSurface GetComplexSurface(int page, int index)
		{
			int ip = 0;
			for ( int i=0 ; i<this.complexSurfaces.Count ; i++ )
			{
				ComplexSurface cs = this.complexSurfaces[i] as ComplexSurface;
				if ( cs.Page == page )
				{
					if ( ip == index )  return cs;
					ip ++;
				}
			}
			return null;
		}

		// Cr�e toutes les surfaces complexes.
		protected void CreateComplexSurface(Writer writer, Port port, DrawingContext drawingContext)
		{
			// Cr�e le ExtGState num�ro 0, pour annuler une transparence.
			writer.WriteObjectDef(Export.NameComplexSurface(0, TypeComplexSurface.ExtGState));
			writer.WriteLine("<< /CA 1 /ca 1 >> endobj");

			for ( int i=0 ; i<this.complexSurfaces.Count ; i++ )
			{
				ComplexSurface cs = this.complexSurfaces[i] as ComplexSurface;

				Objects.Layer layer = cs.Layer;
				Properties.ModColor modColor = layer.PropertyModColor;
				port.PushColorModifier(new ColorModifier(modColor.ModifyColor));
				drawingContext.IsDimmed = (layer.Print == Objects.LayerPrint.Dimmed);
				port.PushColorModifier(new ColorModifier(drawingContext.DimmedColor));

				cs.Object.SurfaceAnchor.LineUse = cs.Fill.IsStrokingGradient;
				this.MatrixComplexSurfaceGradient(cs);

				switch ( cs.Type )
				{
					case PDF.Type.OpaqueRegular:
						this.CreateComplexSurfaceTransparencyGradientOrSmooth(writer, port, drawingContext, cs);
						break;

					case PDF.Type.TransparencyRegular:
						this.CreateComplexSurfaceTransparencyRegular(writer, port, drawingContext, cs);
						break;

					case PDF.Type.OpaqueGradient:
						this.CreateComplexSurfaceOpaqueGradient(writer, port, drawingContext, cs);
						break;

					case PDF.Type.TransparencyGradient:
						this.CreateComplexSurfaceTransparencyGradientOrSmooth(writer, port, drawingContext, cs);
						break;

					case PDF.Type.OpaquePattern:
						this.CreateComplexSurfaceOpaquePattern(writer, port, drawingContext, cs);
						break;

					case PDF.Type.TransparencyPattern:
						this.CreateComplexSurfaceTransparencyPattern(writer, port, drawingContext, cs);
						this.CreateComplexSurfaceOpaquePattern(writer, port, drawingContext, cs);
						break;
				}

				port.PopColorModifier();
				port.PopColorModifier();
			}
		}

		// Cr�e une surface transparente unie.
		protected void CreateComplexSurfaceTransparencyRegular(Writer writer, Port port, DrawingContext drawingContext, ComplexSurface cs)
		{
			if ( cs.IsSmooth )
			{
				this.CreateComplexSurfaceTransparencyGradientOrSmooth(writer, port, drawingContext, cs);
			}
			else
			{
				double a = 1.0;
				if ( cs.Fill is Properties.Gradient )
				{
					Properties.Gradient gradient = cs.Fill as Properties.Gradient;
					a = port.GetFinalColor(gradient.Color1.Basic).A;
				}
				if ( cs.Fill is Properties.Font )
				{
					Properties.Font font = cs.Fill as Properties.Font;
					a = port.GetFinalColor(font.FontColor.Basic).A;
				}

				this.CreateComplexSurfaceAlpha(writer, a, cs.Id, TypeComplexSurface.ExtGState);
			}
		}

		// Cr�e deux surfaces transparentes unies pour un motif.
		protected void CreateComplexSurfaceTransparencyPattern(Writer writer, Port port, DrawingContext drawingContext, ComplexSurface cs)
		{
			double a1 = 1.0;
			double a2 = 1.0;
			if ( cs.Fill is Properties.Gradient )
			{
				Properties.Gradient gradient = cs.Fill as Properties.Gradient;
				a1 = port.GetFinalColor(gradient.Color1.Basic).A;
				a2 = port.GetFinalColor(gradient.Color2.Basic).A;
			}
			if ( cs.Fill is Properties.Font )
			{
				Properties.Font font = cs.Fill as Properties.Font;
				a1 = port.GetFinalColor(font.FontColor.Basic).A;
				a2 = a1;
			}

			this.CreateComplexSurfaceAlpha(writer, a1, cs.Id, TypeComplexSurface.ExtGStateP1);
			this.CreateComplexSurfaceAlpha(writer, a2, cs.Id, TypeComplexSurface.ExtGStateP2);
		}

		// Cr�e un ExtGState pour une transparence unie.
		protected void CreateComplexSurfaceAlpha(Writer writer, double alpha, int id, TypeComplexSurface type)
		{
			writer.WriteObjectDef(Export.NameComplexSurface(id, type));
			Port port = new Port();
			port.PutCommand("<< /CA ");  // voir [*] page 192
			port.PutValue(alpha, 3);
			port.PutCommand("/ca ");
			port.PutValue(alpha, 3);
			port.PutCommand(">> endobj");
			writer.WriteLine(port.GetPDF());
		}

		// Engraisse une bbox en fonction d'une surface floue.
		public static void SurfaceInflate(ComplexSurface cs, ref Rectangle bbox)
		{
			if ( cs.Fill is Properties.Gradient )
			{
				Properties.Gradient surface = cs.Fill as Properties.Gradient;
				Properties.Line     stroke  = cs.Stroke;

				double width = 0.0;

				if ( stroke != null && surface != null )
				{
					width += surface.InflateBoundingBoxWidth();
					width += stroke.InflateBoundingBoxWidth();
					width *= stroke.InflateBoundingBoxFactor();
				}
				else if ( surface != null )
				{
					width += surface.InflateBoundingBoxWidth();
				}

				bbox.Inflate(width);
			}
		}

		// Cr�e une surface d�grad�e transparente et/ou floue.
		protected void CreateComplexSurfaceTransparencyGradientOrSmooth(Writer writer, Port port, DrawingContext drawingContext, ComplexSurface cs)
		{
			Objects.Abstract obj = cs.Object;
			SurfaceAnchor sa = obj.SurfaceAnchor;

			// ExtGState.
			writer.WriteObjectDef(Export.NameComplexSurface(cs.Id, TypeComplexSurface.ExtGState));
			writer.WriteString("<< /SMask << /S /Luminosity /G ");  // voir [*] page 521
			writer.WriteObjectRef(Export.NameComplexSurface(cs.Id, TypeComplexSurface.XObject));
			writer.WriteLine(">> >> endobj");

			// XObject.
			Rectangle bbox = sa.BoundingBox;
			Export.SurfaceInflate(cs, ref bbox);

			writer.WriteObjectDef(Export.NameComplexSurface(cs.Id, TypeComplexSurface.XObject));
			writer.WriteString("<< /Subtype /Form /FormType 1 ");    // voir [*] page 328
			writer.WriteString(Port.StringBBox(bbox));

			writer.WriteString("/Resources << ");

			if ( cs.IsSmooth )
			{
				writer.WriteString("/ExtGState << ");
				writer.WriteString(Export.ShortNameComplexSurface(cs.Id, TypeComplexSurface.ExtGStateSmooth));
				writer.WriteObjectRef(Export.NameComplexSurface(cs.Id, TypeComplexSurface.ExtGStateSmooth));
				writer.WriteString(">> ");
			}

			writer.WriteString("/Shading << ");
			writer.WriteString(Export.ShortNameComplexSurface(cs.Id, TypeComplexSurface.ShadingGray));
			writer.WriteObjectRef(Export.NameComplexSurface(cs.Id, TypeComplexSurface.ShadingGray));
			writer.WriteString(">> ");

			writer.WriteString(">> ");
			writer.WriteString("/Group << /S /Transparency /CS /DeviceGray >> ");  // voir [*] page 525

			Path path = new Path();
			path.AppendRectangle(bbox);
			port.Reset();
			if ( cs.IsSmooth )
			{
				port.PutCommand("q ");
				port.PutCommand(Export.ShortNameComplexSurface(cs.Id, TypeComplexSurface.ExtGStateSmooth));
				port.PutCommand("gs ");
				port.PutPath(path);
				port.PutCommand("W n ");
				port.PutTransform(cs.Matrix);
				port.PutCommand(Export.ShortNameComplexSurface(cs.Id, TypeComplexSurface.ShadingGray));
				port.PutCommand("sh Q ");  // shading, voir [*] page 273
			}
			else
			{
				port.PutPath(path);
				port.PutCommand("q W n ");
				port.PutTransform(cs.Matrix);
				port.PutCommand(Export.ShortNameComplexSurface(cs.Id, TypeComplexSurface.ShadingGray));
				port.PutCommand("sh Q ");  // shading, voir [*] page 273
			}
			port.PutEOL();

			string pdf = port.GetPDF();
			writer.WriteLine(string.Format("{0} >>", Port.StringLength(pdf.Length)));
			writer.WriteLine("stream");
			writer.WriteString(pdf);
			writer.WriteLine("endstream endobj");

			this.CreateComplexSurfaceGradient(writer, port, drawingContext, cs, 0);

			if ( cs.Type == PDF.Type.OpaqueGradient       ||
				 cs.Type == PDF.Type.TransparencyGradient )
			{
				int nbColors = this.ComplexSurfaceGradientNbColors(port, drawingContext, cs);
				this.CreateComplexSurfaceGradient(writer, port, drawingContext, cs, nbColors);
			}

			if ( cs.IsSmooth )
			{
				this.CreateComplexSurfaceSmooth(writer, port, drawingContext, cs);
			}
		}

		// Cr�e une surface floue.
		protected void CreateComplexSurfaceSmooth(Writer writer, Port port, DrawingContext drawingContext, ComplexSurface cs)
		{
			Objects.Abstract obj = cs.Object;
			SurfaceAnchor sa = obj.SurfaceAnchor;

			Properties.Gradient surface = cs.Fill as Properties.Gradient;
			Properties.Line     stroke  = cs.Stroke;

			// ExtGStateSmooth.
			writer.WriteObjectDef(Export.NameComplexSurface(cs.Id, TypeComplexSurface.ExtGStateSmooth));
			writer.WriteString("<< /SMask << /S /Luminosity /G ");  // voir [*] page 521
			writer.WriteObjectRef(Export.NameComplexSurface(cs.Id, TypeComplexSurface.XObjectSmooth));
			writer.WriteLine(">> >> endobj");

			// XObjectSmooth.
			Rectangle bbox = sa.BoundingBox;
			Export.SurfaceInflate(cs, ref bbox);

			writer.WriteObjectDef(Export.NameComplexSurface(cs.Id, TypeComplexSurface.XObjectSmooth));
			writer.WriteString("<< /Subtype /Form /FormType 1 ");    // voir [*] page 328
			writer.WriteString(Port.StringBBox(bbox));
			writer.WriteString("/Group << /S /Transparency /CS /DeviceGray >> ");  // voir [*] page 525

			double    width = 0.0;
			CapStyle  cap   = CapStyle.Round;
			JoinStyle join  = JoinStyle.Round;
			double    limit = 5.0;

			if ( stroke != null )
			{
				width = stroke.Width;
				cap   = stroke.Cap;
				join  = stroke.Join;
				limit = stroke.Limit;
			}

			port.Reset();
			port.ColorForce = ColorForce.Gray;
			port.LineCap = cap;
			port.LineJoin = join;
			port.LineMiterLimit = limit;

			Shape[] shapes = obj.ShapesBuild(port, drawingContext, false);

			int step = (int)(surface.Smooth*drawingContext.ScaleX);
			if ( step > 20 )  step = 20;
			if ( step <  2 )  step =  2;
			foreach ( Shape shape in shapes )
			{
				if ( shape.PropertySurface != surface )  continue;

				for ( int i=0 ; i<step ; i++ )
				{
					double intensity = (i+1.0)/step;
					double w = surface.Smooth-i*surface.Smooth/step;
					port.RichColor = RichColor.FromGray(intensity);
					port.LineWidth = width + w*2.0;
					port.PaintOutline(shape.Path);
				}

				if ( stroke == null )
				{
					port.RichColor = RichColor.FromGray(1.0);
					port.PaintSurface(shape.Path);
				}
			}

			string pdf = port.GetPDF();
			writer.WriteLine(string.Format("{0} >>", Port.StringLength(pdf.Length)));
			writer.WriteLine("stream");
			writer.WriteString(pdf);
			writer.WriteLine("endstream endobj");
		}

		// Cr�e une surface d�grad�e transparente.
		protected void CreateComplexSurfaceOpaqueGradient(Writer writer, Port port, DrawingContext drawingContext, ComplexSurface cs)
		{
			if ( cs.IsSmooth )
			{
				this.CreateComplexSurfaceTransparencyGradientOrSmooth(writer, port, drawingContext, cs);
			}
			else
			{
				int nbColors = this.ComplexSurfaceGradientNbColors(port, drawingContext, cs);
				this.CreateComplexSurfaceGradient(writer, port, drawingContext, cs, nbColors);
			}
		}

		// Calcule la matrice de transformation pour une surface d�grad�e.
		protected void MatrixComplexSurfaceGradient(ComplexSurface cs)
		{
			Properties.Gradient gradient = cs.Fill as Properties.Gradient;
			if ( gradient == null )  return;

			Objects.Abstract obj = cs.Object;
			SurfaceAnchor sa = obj.SurfaceAnchor;
			Rectangle t = cs.Object.BoundingBox;  // update bounding box !
			Point center = sa.ToAbs(new Point(gradient.Cx, gradient.Cy));

			if ( gradient.FillType == Properties.GradientFillType.Linear )
			{
				double sx = sa.Width *gradient.Sx;
				double sy = sa.Height*gradient.Sy;
				double angle = Point.ComputeAngleDeg(sx, sy)-90.0;
				double length = System.Math.Sqrt(sx*sx + sy*sy);

				cs.Matrix.RotateDeg(obj.Direction+angle);
				cs.Matrix.Scale(length, length);
				cs.Matrix.Translate(center);
			}

			if ( gradient.FillType == Properties.GradientFillType.Circle )
			{
				double dx = System.Math.Abs(sa.Width *gradient.Sx);
				double dy = System.Math.Abs(sa.Height*gradient.Sy);

				cs.Matrix.RotateDeg(obj.Direction);
				cs.Matrix.Scale(dx, dy);
				cs.Matrix.Translate(center);
			}

			if ( gradient.FillType == Properties.GradientFillType.Diamond ||
				 gradient.FillType == Properties.GradientFillType.Conic   )
			{
				double dx = System.Math.Abs(sa.Width *gradient.Sx);
				double dy = System.Math.Abs(sa.Height*gradient.Sy);

				cs.Matrix.Scale(dx, dy);
				cs.Matrix.Translate(center);
				cs.Matrix.RotateDeg(sa.Direction+gradient.Angle, center);
			}
		}

		// Cr�e une surface d�grad�e.
		// Si nbColors == 0  ->  canal alpha
		// Si nbColors == 1  ->  espace Gray
		// Si nbColors == 3  ->  espace RGB
		// Si nbColors == 4  ->  espace CMYK
		protected void CreateComplexSurfaceGradient(Writer writer,
													Port port,
													DrawingContext drawingContext,
													ComplexSurface cs,
													int nbColors)
		{
			Properties.Gradient gradient = cs.Fill as Properties.Gradient;
			Objects.Abstract obj = cs.Object;
			SurfaceAnchor sa = obj.SurfaceAnchor;
			Rectangle t = cs.Object.BoundingBox;  // update bounding box !
			port.Reset();

			int totalColors = System.Math.Max(nbColors, 1);

			TypeComplexSurface tcs = (nbColors == 0) ? TypeComplexSurface.ShadingGray : TypeComplexSurface.ShadingColor;

			if ( gradient.FillType == Properties.GradientFillType.None )
			{
				writer.WriteObjectDef(Export.NameComplexSurface(cs.Id, tcs));

				// Axial shading, voir [*] page 279
				port.PutCommand("<< /ShadingType 2 /Coords [0 -1 0 1] ");

				if ( nbColors <= 1 )  port.PutCommand("/ColorSpace /DeviceGray ");
				if ( nbColors == 3 )  port.PutCommand("/ColorSpace /DeviceRGB ");
				if ( nbColors == 4 )  port.PutCommand("/ColorSpace /DeviceCMYK ");

				port.PutCommand("/Function << /FunctionType 2 /Domain [0 1] /C0 [");
				RichColor c1 = port.GetFinalColor(gradient.Color1);
				RichColor c2 = port.GetFinalColor(gradient.Color2);
				if ( nbColors == 1 )  // gray ?
				{
					c1.ColorSpace = ColorSpace.Gray;
					c2.ColorSpace = ColorSpace.Gray;
				}
				if ( nbColors == 3 )  // rgb ?
				{
					c1.ColorSpace = ColorSpace.RGB;
					c2.ColorSpace = ColorSpace.RGB;
				}
				if ( nbColors == 4 )  // cmyk ?
				{
					c1.ColorSpace = ColorSpace.CMYK;
					c2.ColorSpace = ColorSpace.CMYK;
				}
				if ( nbColors == 0 )  port.PutValue(c1.A, 3);
				else                  port.PutColor(c1);
				port.PutCommand("] /C1 [");
				if ( nbColors == 0 )  port.PutValue(c1.A, 3);  // c1 to c1 (flat) !
				else                  port.PutColor(c1);
				port.PutCommand("] /N 1 >> ");

				port.PutCommand("/Extend [true true] >> endobj");
				writer.WriteLine(port.GetPDF());
			}

			if ( gradient.FillType == Properties.GradientFillType.Linear ||
				 gradient.FillType == Properties.GradientFillType.Circle )
			{
				if ( gradient.Middle != 0.0 || gradient.Repeat != 1 )
				{
					Export.FunctionColorSampled(writer, port, drawingContext, cs, nbColors);
				}

				writer.WriteObjectDef(Export.NameComplexSurface(cs.Id, tcs));

				if ( gradient.FillType == Properties.GradientFillType.Linear )
				{
					// Axial shading, voir [*] page 279
					port.PutCommand("<< /ShadingType 2 /Coords [0 -1 0 1] ");
				}

				if ( gradient.FillType == Properties.GradientFillType.Circle )
				{
					// Radial shading, voir [*] page 280
					port.PutCommand("<< /ShadingType 3 /Coords [0 0 0 0 0 1] ");
				}

				if ( nbColors <= 1 )  port.PutCommand("/ColorSpace /DeviceGray ");
				if ( nbColors == 3 )  port.PutCommand("/ColorSpace /DeviceRGB ");
				if ( nbColors == 4 )  port.PutCommand("/ColorSpace /DeviceCMYK ");

				if ( gradient.Middle == 0.0 && gradient.Repeat == 1 )
				{
					// Exponential interpolation function, voir [*] page 146
					port.PutCommand("/Function << /FunctionType 2 /Domain [0 1] /C0 [");
					RichColor c1 = port.GetFinalColor(gradient.Color1);
					RichColor c2 = port.GetFinalColor(gradient.Color2);
					if ( nbColors == 1 )  // gray ?
					{
						c1.ColorSpace = ColorSpace.Gray;
						c2.ColorSpace = ColorSpace.Gray;
					}
					if ( nbColors == 3 )  // rgb ?
					{
						c1.ColorSpace = ColorSpace.RGB;
						c2.ColorSpace = ColorSpace.RGB;
					}
					if ( nbColors == 4 )  // cmyk ?
					{
						c1.ColorSpace = ColorSpace.CMYK;
						c2.ColorSpace = ColorSpace.CMYK;
					}
					if ( nbColors == 0 )  port.PutValue(c1.A, 3);
					else                  port.PutColor(c1);
					port.PutCommand("] /C1 [");
					if ( nbColors == 0 )  port.PutValue(c2.A, 3);
					else                  port.PutColor(c2);
					port.PutCommand("] /N 1 >> ");
				}
				else
				{
					port.PutCommand("/Function ");
					writer.WriteString(port.GetPDF());
					writer.WriteObjectRef(Export.NameFunction(cs.Id, (nbColors == 0) ? TypeFunction.SampledAlpha : TypeFunction.SampledColor));
					port.Reset();
				}

				port.PutCommand("/Extend [true true] >> endobj");
				writer.WriteLine(port.GetPDF());
			}

			if ( gradient.FillType == Properties.GradientFillType.Diamond ||
				 gradient.FillType == Properties.GradientFillType.Conic   )
			{
				// Calcule le domaine (pfff...).
				double w = sa.Width;
				double h = sa.Height;
				Point p1 = new Point((0.0-gradient.Cx)*w, (0.0-gradient.Cy)*h);
				Point p2 = new Point((1.0-gradient.Cx)*w, (0.0-gradient.Cy)*h);
				Point p3 = new Point((0.0-gradient.Cx)*w, (1.0-gradient.Cy)*h);
				Point p4 = new Point((1.0-gradient.Cx)*w, (1.0-gradient.Cy)*h);
				p1 = Transform.RotatePointDeg(-gradient.Angle, p1);
				p2 = Transform.RotatePointDeg(-gradient.Angle, p2);
				p3 = Transform.RotatePointDeg(-gradient.Angle, p3);
				p4 = Transform.RotatePointDeg(-gradient.Angle, p4);
				double domainMinX = System.Math.Min(System.Math.Min(p1.X, p2.X), System.Math.Min(p3.X, p4.X));
				double domainMaxX = System.Math.Max(System.Math.Max(p1.X, p2.X), System.Math.Max(p3.X, p4.X));
				double domainMinY = System.Math.Min(System.Math.Min(p1.Y, p2.Y), System.Math.Min(p3.Y, p4.Y));
				double domainMaxY = System.Math.Max(System.Math.Max(p1.Y, p2.Y), System.Math.Max(p3.Y, p4.Y));
				domainMinX /= w*System.Math.Abs(gradient.Sx);
				domainMaxX /= w*System.Math.Abs(gradient.Sx);
				domainMinY /= h*System.Math.Abs(gradient.Sy);
				domainMaxY /= h*System.Math.Abs(gradient.Sy);
				Rectangle bbox = new Rectangle(0.0, 0.0, w, h);
				Export.SurfaceInflate(cs, ref bbox);
				double fx = bbox.Width /w;
				double fy = bbox.Height/h;
				double f = System.Math.Max(fx, fy);
				domainMinX *= f;
				domainMaxX *= f;
				domainMinY *= f;
				domainMaxY *= f;

				port.PutCommand("/Domain [");
				port.PutValue(domainMinX, 3);
				port.PutValue(domainMaxX, 3);
				port.PutValue(domainMinY, 3);
				port.PutValue(domainMaxY, 3);
				port.PutCommand("]");
				string domain = port.GetPDF();
				port.Reset();

				writer.WriteObjectDef(Export.NameComplexSurface(cs.Id, tcs));

				// Function-based shading, voir [*] page 178
				port.PutCommand("<< /ShadingType 1 ");
				if ( nbColors <= 1 )  port.PutCommand("/ColorSpace /DeviceGray ");
				if ( nbColors == 3 )  port.PutCommand("/ColorSpace /DeviceRGB ");
				if ( nbColors == 4 )  port.PutCommand("/ColorSpace /DeviceCMYK ");
				port.PutCommand(domain);
				port.PutCommand(" /Function [");
				writer.WriteString(port.GetPDF());
				if ( nbColors == 0 )  // alpha ?
				{
					writer.WriteObjectRef(Export.NameFunction(cs.Id, TypeFunction.Alpha));
				}
				if ( nbColors == 1 )  // gray ?
				{
					writer.WriteObjectRef(Export.NameFunction(cs.Id, TypeFunction.Color1));
				}
				if ( nbColors == 3 )  // rgb ?
				{
					writer.WriteObjectRef(Export.NameFunction(cs.Id, TypeFunction.Color1));
					writer.WriteObjectRef(Export.NameFunction(cs.Id, TypeFunction.Color2));
					writer.WriteObjectRef(Export.NameFunction(cs.Id, TypeFunction.Color3));
				}
				if ( nbColors == 4 )  // cmyk ?
				{
					writer.WriteObjectRef(Export.NameFunction(cs.Id, TypeFunction.Color1));
					writer.WriteObjectRef(Export.NameFunction(cs.Id, TypeFunction.Color2));
					writer.WriteObjectRef(Export.NameFunction(cs.Id, TypeFunction.Color3));
					writer.WriteObjectRef(Export.NameFunction(cs.Id, TypeFunction.Color4));
				}
				writer.WriteLine("] >> endobj");

				// G�n�re les fonctions pour les couleurs.
				string[] fonctions = new string[totalColors];
				RichColor c1 = port.GetFinalColor(gradient.Color1);
				RichColor c2 = port.GetFinalColor(gradient.Color2);
				if ( gradient.FillType == Properties.GradientFillType.Diamond )
				{
					if ( nbColors == 0 )  // alpha ?
					{
						fonctions[0] = Export.FunctionGradientDiamond(gradient, c1.A, c2.A);
					}
					if ( nbColors == 1 )  // gray ?
					{
						fonctions[0] = Export.FunctionGradientDiamond(gradient, c1.Gray, c2.Gray);
					}
					if ( nbColors == 3 )  // rgb ?
					{
						fonctions[0] = Export.FunctionGradientDiamond(gradient, c1.R, c2.R);
						fonctions[1] = Export.FunctionGradientDiamond(gradient, c1.G, c2.G);
						fonctions[2] = Export.FunctionGradientDiamond(gradient, c1.B, c2.B);
					}
					if ( nbColors == 4 )  // cmyk ?
					{
						fonctions[0] = Export.FunctionGradientDiamond(gradient, c1.C, c2.C);
						fonctions[1] = Export.FunctionGradientDiamond(gradient, c1.M, c2.M);
						fonctions[2] = Export.FunctionGradientDiamond(gradient, c1.Y, c2.Y);
						fonctions[3] = Export.FunctionGradientDiamond(gradient, c1.K, c2.K);
					}
				}
				if ( gradient.FillType == Properties.GradientFillType.Conic )
				{
					if ( nbColors == 0 )  // alpha ?
					{
						fonctions[0] = Export.FunctionGradientConic(gradient, c1.A, c2.A);
					}
					if ( nbColors == 1 )  // gray ?
					{
						fonctions[0] = Export.FunctionGradientConic(gradient, c1.Gray, c2.Gray);
					}
					if ( nbColors == 3 )  // rgb ?
					{
						fonctions[0] = Export.FunctionGradientConic(gradient, c1.R, c2.R);
						fonctions[1] = Export.FunctionGradientConic(gradient, c1.G, c2.G);
						fonctions[2] = Export.FunctionGradientConic(gradient, c1.B, c2.B);
					}
					if ( nbColors == 4 )  // cmyk ?
					{
						fonctions[0] = Export.FunctionGradientConic(gradient, c1.C, c2.C);
						fonctions[1] = Export.FunctionGradientConic(gradient, c1.M, c2.M);
						fonctions[2] = Export.FunctionGradientConic(gradient, c1.Y, c2.Y);
						fonctions[3] = Export.FunctionGradientConic(gradient, c1.K, c2.K);
					}
				}

				for ( int i=0 ; i<totalColors ; i++ )
				{
					TypeFunction ft = TypeFunction.Alpha;
					if ( nbColors != 0 )  // gray, rgb ou cmyk ?
					{
						switch ( i )
						{
							case 0:  ft = TypeFunction.Color1;  break;
							case 1:  ft = TypeFunction.Color2;  break;
							case 2:  ft = TypeFunction.Color3;  break;
							case 3:  ft = TypeFunction.Color4;  break;
						}
					}
					writer.WriteObjectDef(Export.NameFunction(cs.Id, ft));
					// PostScript calculator function, voir [*] page 148
					writer.WriteLine(string.Format("<< /FunctionType 4 /Range [0 1] {0} {1} >>", domain, Port.StringLength(fonctions[i].Length)));
					writer.WriteLine("stream");
					writer.WriteLine(fonctions[i]);
					writer.WriteLine("endstream endobj");
				}
			}
		}

		// G�n�re la fonction de calcul des couleurs interm�diaires, sur la base
		// d'une table de 3x256 valeurs 8 bits.
		// Si nbColors == 0  ->  canal alpha
		// Si nbColors == 1  ->  espace Gray
		// Si nbColors == 3  ->  espace RGB
		// Si nbColors == 4  ->  espace CMYK
		// Sampled function, voir [*] page 142
		protected static void FunctionColorSampled(Writer writer,
												   Port port,
												   DrawingContext drawingContext,
												   ComplexSurface cs,
												   int nbColors)
		{
			Properties.Gradient gradient = cs.Fill as Properties.Gradient;
			RichColor c1 = port.GetFinalColor(gradient.Color1);
			RichColor c2 = port.GetFinalColor(gradient.Color2);

			int totalColors = System.Math.Max(nbColors, 1);

			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			for ( int i=0 ; i<256 ; i++ )
			{
				double factor = gradient.GetProgressColorFactor(i/255.0);

				if ( nbColors == 0 )  // alpha ?
				{
					int a = (int) ((c1.A + (c2.A-c1.A)*factor) * 255.0);
					builder.Append(a.ToString("X2"));
				}

				if ( nbColors == 1 )  // gray ?
				{
					int a = (int) ((c1.Gray + (c2.Gray-c1.Gray)*factor) * 255.0);
					builder.Append(a.ToString("X2"));
				}

				if ( nbColors == 3 )  // rgb ?
				{
					int r = (int) ((c1.R + (c2.R-c1.R)*factor) * 255.0);
					int g = (int) ((c1.G + (c2.G-c1.G)*factor) * 255.0);
					int b = (int) ((c1.B + (c2.B-c1.B)*factor) * 255.0);
					builder.Append(r.ToString("X2"));
					builder.Append(g.ToString("X2"));
					builder.Append(b.ToString("X2"));
				}

				if ( nbColors == 4 )  // cmyk ?
				{
					int c = (int) ((c1.C + (c2.C-c1.C)*factor) * 255.0);
					int m = (int) ((c1.M + (c2.M-c1.M)*factor) * 255.0);
					int y = (int) ((c1.Y + (c2.Y-c1.Y)*factor) * 255.0);
					int k = (int) ((c1.K + (c2.K-c1.K)*factor) * 255.0);
					builder.Append(c.ToString("X2"));
					builder.Append(m.ToString("X2"));
					builder.Append(y.ToString("X2"));
					builder.Append(k.ToString("X2"));
				}
			}
			string table = builder.ToString();

			builder = new System.Text.StringBuilder();
			for ( int i=0 ; i<totalColors ; i++ )
			{
				builder.Append("0 1 ");
			}
			string range = builder.ToString();

			writer.WriteObjectDef(Export.NameFunction(cs.Id, (nbColors == 0) ? TypeFunction.SampledAlpha : TypeFunction.SampledColor));
			writer.WriteLine(string.Format("<< /FunctionType 0 /Domain [0 1] /Range [{0}] /Size [256] /BitsPerSample 8 /Filter /ASCIIHexDecode {1} >>", range, Port.StringLength(table.Length)));
			writer.WriteLine("stream");
			writer.WriteLine(table);
			writer.WriteLine("endstream endobj");
		}

		// Retourne la fonction permettant de calculer une couleur fondamentale
		// d'un d�grad� en diamant. En entr�s, la pile contient x et y [-1..1].
		// En sortie, elle contient la couleur [0..1]. Avec une r�p�tition de 1,
		// la fonction est la suivante:
		//   f(c) = max(|x|,|y|)
		// PostScript calculator function, voir [*] page 148
		protected static string FunctionGradientDiamond(Properties.Gradient gradient,
														double colorStart,
														double colorEnd)
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			builder.Append("{ ");

			if ( colorStart == colorEnd )
			{
				builder.Append("pop pop ");
				builder.Append(Port.StringValue(colorStart, 3));
			}
			else
			{
				builder.Append("abs exch abs 2 copy lt { exch } if pop ");
				if ( gradient.Repeat > 1 )
				{
					builder.Append("dup 1 gt { pop 1 } if ");
					builder.Append(Port.StringValue(gradient.Repeat, 3));
					builder.Append(" mul dup dup truncate sub exch cvi 2 mod 1 eq { 1 exch sub } if ");
				}

				Export.FunctionMiddleAdjust(builder, gradient);
				Export.FunctionColorAdjust(builder, colorStart, colorEnd);
			}

			builder.Append("}");
			return builder.ToString();
		}

		// Retourne la fonction permettant de calculer une couleur fondamentale
		// d'un d�grad� conique. En entr�s, la pile contient x et y [-1..1].
		// En sortie, elle contient la couleur [0..1]. Avec une r�p�tition de 1,
		// la fonction est la suivante:
		//   f(c) = (360-atan(x,y))/360
		// PostScript calculator function, voir [*] page 148
		protected static string FunctionGradientConic(Properties.Gradient gradient,
													  double colorStart,
													  double colorEnd)
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			builder.Append("{ ");

			if ( colorStart == colorEnd )
			{
				builder.Append("pop pop ");
				builder.Append(Port.StringValue(colorStart, 3));
			}
			else
			{
				if ( gradient.Sx < 0.0 )  // miroir X ?
				{
					builder.Append("exch neg exch ");  // [-1..1] -> [1..-1]
				}

				if ( gradient.Sy < 0.0 )  // miroir Y ?
				{
					builder.Append("neg ");  // [-1..1] -> [1..-1]
				}

				builder.Append("atan 360 exch sub ");

				if ( gradient.Repeat == 1 )
				{
					builder.Append("360 div ");
				}
				else
				{
					builder.Append(Port.StringValue(360.0/gradient.Repeat, 3));
					builder.Append(" div dup exch dup truncate sub exch cvi 2 mod 1 eq { 1 exch sub } if ");
				}

				Export.FunctionMiddleAdjust(builder, gradient);
				Export.FunctionColorAdjust(builder, colorStart, colorEnd);
			}

			builder.Append("}");
			return builder.ToString();
		}

		// Ajoute une modification du nombre [0..1] sur la pile pour tenir compte
		// du point milieu (voir Properties.Gradient.GetProgressColorFactor).
		// Si M>0:  P=1-(1-P)^(1+M)
		// Si M<0:  P=P^(1-M)
		protected static void FunctionMiddleAdjust(System.Text.StringBuilder builder, Properties.Gradient gradient)
		{
			if ( gradient.Middle > 0.0 )
			{
				builder.Append("1 exch sub ");
				builder.Append(Port.StringValue(1.0+gradient.Middle, 3));
				builder.Append(" exp 1 exch sub ");
			}

			if ( gradient.Middle < 0.0 )
			{
				builder.Append(Port.StringValue(1.0-gradient.Middle, 3));
				builder.Append(" exp ");
			}
		}

		// Ajoute une modification du nombre [0..1] sur la pile pour tenir compte
		// de la couleur.
		protected static void FunctionColorAdjust(System.Text.StringBuilder builder, double colorStart, double colorEnd)
		{
			if ( colorEnd-colorStart != 1.0 )
			{
				builder.Append(Port.StringValue(colorEnd-colorStart, 3));
				builder.Append(" mul ");
			}
			if ( colorStart != 0.0 )
			{
				builder.Append(Port.StringValue(colorStart, 3));
				builder.Append(" add ");
			}
		}

		// Cr�e une surface opaque contenant un motif.
		protected void CreateComplexSurfaceOpaquePattern(Writer writer, Port port, DrawingContext drawingContext, ComplexSurface cs)
		{
			Properties.Gradient surface = cs.Fill as Properties.Gradient;
			Objects.Abstract obj = cs.Object;
			SurfaceAnchor sa = obj.SurfaceAnchor;
			Rectangle bbox = cs.Object.BoundingBox;  // update bounding box !

			port.Reset();

			Path path = new Path();
			path.AppendRectangle(bbox);

			Drawer.RenderMotif(port, drawingContext, obj, path, sa, surface);

			writer.WriteObjectDef(Export.NameComplexSurface(cs.Id, TypeComplexSurface.XObject));
			writer.WriteString("<< /Subtype /Form ");  // voir [*] page 328
			writer.WriteString(Port.StringBBox(bbox));

			string pdf = port.GetPDF();
			writer.WriteLine(string.Format("{0} >>", Port.StringLength(pdf.Length)));
			writer.WriteLine("stream");
			writer.WriteString(pdf);
			writer.WriteLine("endstream endobj");
		}

		// D�termine le nombre de couleurs (1, 3 ou 4) � utiliser pour une surface
		// complexe. C'est la premi�re couleur qui fait foi. La deuxi�me sera
		// convertie dans l'espace de couleur de la premi�re, au besoin.
		protected int ComplexSurfaceGradientNbColors(Port port, DrawingContext drawingContext, ComplexSurface cs)
		{
			Properties.Gradient gradient = cs.Fill as Properties.Gradient;
			RichColor color = port.GetFinalColor(gradient.Color1);

			if ( color.ColorSpace == ColorSpace.Gray )
			{
				color = port.GetFinalColor(gradient.Color2);
			}

			if ( color.ColorSpace == ColorSpace.CMYK )  return 4;
			if ( color.ColorSpace == ColorSpace.Gray )  return 1;
			return 3;
		}

		// Lib�re toutes les surfaces complexes.
		protected void FlushComplexSurface()
		{
			foreach ( ComplexSurface cs in this.complexSurfaces )
			{
				cs.Dispose();
			}
			this.complexSurfaces.Clear();
			this.complexSurfaces = null;
		}
		#endregion


		#region Images
		// Cr�e toutes les images.
		protected void CreateImageSurface(Writer writer, Port port)
		{
			foreach ( ImageSurface image in this.imageSurfaces )
			{
				if ( image.DrawingImage == null )  continue;

				if ( this.CreateImageSurface(writer, port, image, TypeComplexSurface.XObject, TypeComplexSurface.XObjectMask) )
				{
					this.CreateImageSurface(writer, port, image, TypeComplexSurface.XObjectMask, TypeComplexSurface.None);
				}
			}
		}

		// Cr�e une image.
		protected bool CreateImageSurface(Writer writer, Port port, ImageSurface image,
										  TypeComplexSurface baseType, TypeComplexSurface maskType)
		{
			Pixmap.RawData data = new Pixmap.RawData(image.DrawingImage);

			writer.WriteObjectDef(Export.NameComplexSurface(image.Id, baseType));
			writer.WriteString("<< /Subtype /Image ");

			if ( baseType == TypeComplexSurface.XObject )
			{
				if ( this.colorConversion == PDF.ColorConversion.ToGray )
				{
					writer.WriteString("/ColorSpace /DeviceGray ");
				}
				else if ( this.colorConversion == PDF.ColorConversion.ToCMYK )
				{
					writer.WriteString("/ColorSpace /DeviceCMYK ");
				}
				else
				{
					writer.WriteString("/ColorSpace /DeviceRGB ");
				}
			}
			if ( baseType == TypeComplexSurface.XObjectMask )
			{
				writer.WriteString("/ColorSpace /DeviceGray ");
			}

			writer.WriteString("/BitsPerComponent 8 /Filter /ASCIIHexDecode /Width ");
			writer.WriteString(Port.StringValue(data.Width, 0));
			writer.WriteString(" /Height ");
			writer.WriteString(Port.StringValue(data.Height, 0));

			port.Reset();

			bool useMask = false;
			using ( data )
			{
				for ( int y=0 ; y<data.Height ; y++ )
				{
					for ( int x=0 ; x<data.Width ; x++ )
					{
						Color color = data[x,y];
						if ( baseType == TypeComplexSurface.XObject )
						{
							if ( this.colorConversion == PDF.ColorConversion.ToGray )
							{
								double gray = Color.GetBrightness(color.R, color.G, color.B);
								int g = (int) (gray * 255.0);

								port.PutCommand(g.ToString("X2"));
							}
							else if ( this.colorConversion == PDF.ColorConversion.ToCMYK )
							{
								double cyan, magenta, yellow, black;
								RichColor.RGB2CMYK(color.R, color.G, color.B, out cyan, out magenta, out yellow, out black);
								int cc = (int) (cyan    * 255.0);
								int mm = (int) (magenta * 255.0);
								int yy = (int) (yellow  * 255.0);
								int kk = (int) (black   * 255.0);

								port.PutCommand(cc.ToString("X2"));
								port.PutCommand(mm.ToString("X2"));
								port.PutCommand(yy.ToString("X2"));
								port.PutCommand(kk.ToString("X2"));
							}
							else
							{
								int r = (int) (color.R * 255.0);
								int g = (int) (color.G * 255.0);
								int b = (int) (color.B * 255.0);

								port.PutCommand(r.ToString("X2"));
								port.PutCommand(g.ToString("X2"));
								port.PutCommand(b.ToString("X2"));
							}

							if ( color.A < 1.0 )  useMask = true;
						}
						if ( baseType == TypeComplexSurface.XObjectMask )
						{
							int a = (int) (color.A * 255.0);
							port.PutCommand(a.ToString("X2"));
						}
					}
					port.PutEOL();
				}
			}

			if ( maskType == TypeComplexSurface.XObjectMask && useMask )
			{
				writer.WriteString(" /SMask ");
				writer.WriteObjectRef(Export.NameComplexSurface(image.Id, maskType));
			}

			string pdf = port.GetPDF();
			writer.WriteLine(string.Format(" {0} >>", Port.StringLength(pdf.Length)));
			writer.WriteLine("stream");
			writer.WriteString(pdf);
			writer.WriteLine("endstream endobj");

			return useMask;
		}
		
		// Lib�re toutes les images.
		protected void FlushImageSurface()
		{
			foreach ( ImageSurface image in this.imageSurfaces )
			{
				image.Dispose();
			}
			this.imageSurfaces.Clear();
			this.imageSurfaces = null;
		}
		#endregion


		#region Fonts
		// Cr�e toutes les fontes.
		protected void CreateFont(Writer writer)
		{
			foreach ( System.Collections.DictionaryEntry dict in this.fontList )
			{
				FontList font = dict.Key as FontList;

				int totalPages = (font.CharacterCount+Export.charPerFont-1)/Export.charPerFont;
				System.Diagnostics.Debug.Assert(totalPages <= 10);  // voir Export.NameFont
				for ( int fontPage=0 ; fontPage<totalPages ; fontPage++ )
				{
					int count = Export.charPerFont;
					if ( fontPage == totalPages-1 )
					{
						count = font.CharacterCount%Export.charPerFont;
						if ( count == 0 )  count = Export.charPerFont;
					}

					this.CreateFontBase(writer, font, fontPage, count);
					this.CreateFontEncoding(writer, font, fontPage, count);
					this.CreateFontCharProcs(writer, font, fontPage, count);
					this.CreateFontWidths(writer, font, fontPage, count);
					this.CreateFontToUnicode(writer, font, fontPage, count);
					this.CreateFontCharacters(writer, font, fontPage, count);
				}
			}
		}

		// Cr�e l'objet de base d'une fonte.
		protected void CreateFontBase(Writer writer, FontList font, int fontPage, int count)
		{
			Rectangle bbox = font.CharacterBBox;

			writer.WriteObjectDef(Export.NameFont(font.Id, fontPage, TypeFont.Base));
			writer.WriteString("<< /Type /Font /Subtype /Type3 ");  // voir [*] page 394
			writer.WriteString(string.Format("/FirstChar 0 /LastChar {0} ", count-1));
			writer.WriteString(Port.StringBBox("/FontBBox", bbox));
			writer.WriteString("/FontMatrix [1 0 0 1 0 0] ");

			writer.WriteString("/Encoding ");
			writer.WriteObjectRef(Export.NameFont(font.Id, fontPage, TypeFont.Encoding));

			writer.WriteString("/CharProcs ");
			writer.WriteObjectRef(Export.NameFont(font.Id, fontPage, TypeFont.CharProcs));

			writer.WriteString("/Widths ");
			writer.WriteObjectRef(Export.NameFont(font.Id, fontPage, TypeFont.Widths));

			writer.WriteString("/ToUnicode ");
			writer.WriteObjectRef(Export.NameFont(font.Id, fontPage, TypeFont.ToUnicode));

			writer.WriteLine(">> endobj");
		}
		
		// Cr�e l'objet Encoding d'une fonte.
		protected void CreateFontEncoding(Writer writer, FontList font, int fontPage, int count)
		{
			writer.WriteObjectDef(Export.NameFont(font.Id, fontPage, TypeFont.Encoding));

			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			builder.Append("<< /Differences [ 0 ");
			
			int firstChar = fontPage*Export.charPerFont;
			for ( int i=0 ; i<count ; i++ )
			{
				CharacterList cl = font.GetCharacter(firstChar+i);
				builder.Append(Export.ShortNameCharacter(font.Id, fontPage, cl.Unicode));
			}

			builder.Append("] >> endobj");
			writer.WriteLine(builder.ToString());
		}
		
		// Cr�e l'objet CharProcs d'une fonte.
		protected void CreateFontCharProcs(Writer writer, FontList font, int fontPage, int count)
		{
			writer.WriteObjectDef(Export.NameFont(font.Id, fontPage, TypeFont.CharProcs));
			writer.WriteString("<< ");

			int firstChar = fontPage*Export.charPerFont;
			for ( int i=0 ; i<count ; i++ )
			{
				CharacterList cl = font.GetCharacter(firstChar+i);
				writer.WriteString(Export.ShortNameCharacter(font.Id, fontPage, cl.Unicode));
				writer.WriteObjectRef(Export.NameCharacter(font.Id, fontPage, cl.Unicode));
			}

			writer.WriteLine(">> endobj");
		}
		
		// Cr�e l'objet "table des chasses" d'une fonte.
		protected void CreateFontWidths(Writer writer, FontList font, int fontPage, int count)
		{
			writer.WriteObjectDef(Export.NameFont(font.Id, fontPage, TypeFont.Widths));

			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			builder.Append("[");

			int firstChar = fontPage*Export.charPerFont;
			for ( int i=0 ; i<count ; i++ )
			{
				CharacterList cl = font.GetCharacter(firstChar+i);
				double advance = font.Font.GetCharAdvance(cl.Unicode);
				builder.Append(Port.StringValue(advance, 4));
				builder.Append(" ");
			}
			builder.Append("] endobj");
			writer.WriteLine(builder.ToString());
		}
		
		// Cr�e l'objet ToUnicode d'une fonte, qui permet de retrouver les codes
		// des caract�res lors d'une copie depuis Acrobat dans le clipboard.
		// Voir [*] pages 420 � 446.
		protected void CreateFontToUnicode(Writer writer, FontList font, int fontPage, int count)
		{
			writer.WriteObjectDef(Export.NameFont(font.Id, fontPage, TypeFont.ToUnicode));

			string fontName = font.Font.FaceName;
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			builder.Append("/CIDInit /ProcSet findresource begin ");
			builder.Append("12 dict begin begincmap ");
			builder.Append("/CIDSystemInfo << /Registry (Epsitec) /Ordering (Identity-H) /Supplement 0 >> def ");
			builder.Append("/CMapName /Epsitec def ");
			builder.Append("1 begincodespacerange <00> <FF> endcodespacerange ");

			int firstChar = fontPage*Export.charPerFont;
			builder.Append(string.Format("{0} beginbfrange ", count));
			for ( int i=0 ; i<count ; i++ )
			{
				CharacterList cl = font.GetCharacter(firstChar+i);
				string hex1 = i.ToString("X2");
				string hex2 = (cl.Unicode).ToString("X4");
				builder.Append(string.Format("<{0}> <{0}> <{1}> ", hex1, hex2));
			}
			builder.Append("endbfrange ");

			builder.Append("endcmap CMapName currentdict /CMap defineresource pop end end\r\n");

			writer.WriteLine(string.Format("<< {0} >>", Port.StringLength(builder.Length)));
			writer.WriteLine("stream");
			writer.WriteString(builder.ToString());
			writer.WriteLine("endstream endobj");
		}
		
		// Cr�e tous les objets des caract�res d'une fonte.
		protected void CreateFontCharacters(Writer writer, FontList font, int fontPage, int count)
		{
			int firstChar = fontPage*Export.charPerFont;
			for ( int i=0 ; i<count ; i++ )
			{
				CharacterList cl = font.GetCharacter(firstChar+i);
				this.CreateFontCharacter(writer, font, fontPage, cl.Unicode);
			}
		}
		
		// Cr�e l'objet d'un caract�re d'une fonte.
		protected void CreateFontCharacter(Writer writer, FontList font, int fontPage, int unicode)
		{
			writer.WriteObjectDef(Export.NameCharacter(font.Id, fontPage, unicode));

			Font drawingFont = font.Font;
			Drawing.Transform ft = drawingFont.SyntheticTransform;
			int glyph = drawingFont.GetGlyphIndex(unicode);
			Path path = new Path();
			path.Append(drawingFont, glyph, ft.XX, ft.XY, ft.YX, ft.YY, ft.TX, ft.TY);

			Port port = new Port();
			port.ColorForce = ColorForce.Nothing;  // pas de commande de couleur !
			port.DefaultDecimals = 4;
			port.PaintSurface(path);

			string pdf = port.GetPDF();
			writer.WriteLine(string.Format("<< {0} >>", Port.StringLength(pdf.Length)));
			writer.WriteLine("stream");
			writer.WriteString(pdf);
			writer.WriteLine("endstream endobj");
		}
		
		// Lib�re toutes les fontes.
		protected void FlushFont()
		{
			foreach ( System.Collections.DictionaryEntry dict in this.characterList )
			{
				CharacterList ch = dict.Key as CharacterList;
				ch.Dispose();
			}
			this.characterList.Clear();
			this.characterList = null;

			foreach ( System.Collections.DictionaryEntry dict in this.fontList )
			{
				FontList font = dict.Key as FontList;
				font.Dispose();
			}
			this.fontList.Clear();
			this.fontList = null;
		}
		#endregion

		
		// Calcule la liste des calques, y compris ceux des pages ma�tres.
		// Les calques cach�s � l'impression ne sont pas mis dans la liste.
		protected System.Collections.ArrayList ComputeLayers(int pageNumber)
		{
			System.Collections.ArrayList layers = new System.Collections.ArrayList();
			System.Collections.ArrayList masterList = new System.Collections.ArrayList();
			this.document.Modifier.ComputeMasterPageList(masterList, pageNumber);

			// Mets d'abord les premiers calques de toutes les pages ma�tres.
			foreach ( Objects.Page master in masterList )
			{
				int frontier = master.MasterFirstFrontLayer;
				for ( int i=0 ; i<frontier ; i++ )
				{
					Objects.Layer layer = master.Objects[i] as Objects.Layer;
					if ( layer.Print == Objects.LayerPrint.Hide )  continue;
					layers.Add(layer);
				}
			}

			// Mets ensuite tous les calques de la page.
			Objects.Abstract page = this.document.GetObjects[pageNumber] as Objects.Abstract;
			foreach ( Objects.Layer layer in this.document.Flat(page) )
			{
				if ( layer.Print == Objects.LayerPrint.Hide )  continue;
				layers.Add(layer);
			}

			// Mets finalement les derniers calques de toutes les pages ma�tres.
			foreach ( Objects.Page master in masterList )
			{
				int frontier = master.MasterFirstFrontLayer;
				int total = master.Objects.Count;
				for ( int i=frontier ; i<total ; i++ )
				{
					Objects.Layer layer = master.Objects[i] as Objects.Layer;
					if ( layer.Print == Objects.LayerPrint.Hide )  continue;
					layers.Add(layer);
				}
			}

			return layers;
		}


		// Constante pour conversion dixi�mes de millim�tres -> 72�me de pouce
		protected static readonly double		mm2in = 7.2/25.4;
		public static readonly int				charPerFont = 256;

		protected Document						document;
		protected System.Collections.ArrayList	complexSurfaces;
		protected System.Collections.ArrayList	imageSurfaces;
		protected System.Collections.Hashtable	characterList;
		protected System.Collections.Hashtable	fontList;
		protected ColorConversion				colorConversion;
	}
}
