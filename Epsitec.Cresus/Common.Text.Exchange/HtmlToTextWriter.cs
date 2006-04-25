//	Copyright � 2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Michael WALZ

/// Probl�mes non r�solus:
/// 1)
///		Pour les tags html sup, sub et u les param�tres suppl�mentaires sont hard cod�es de la m�me fa�on
///		que dans la classe Epsitec.Common.Document.Wappers du fichier Common.Text/Wrappers.cs,
///		chercher commentaires [HARDCODE]
///		D'ailleurs les m�thodes Fill*Definition(...) ont �t� copi�es depuis Common.Text/Wrappers.cs
///		
///	2)
///		Il faut voir si on ne peux pas simplifier l'usage des multiples SuspendSynchronizations()/ResumeSynchronizations()
///		
///	3)
///		Dans la fonction ProcessIt () il faut voir si les divers Clear...() sont appel�es correctement
///		
	


using System;
using System.Collections.Generic;
using System.Text;
using Epsitec.Common.Text.Exchange.HtmlParser;

namespace Epsitec.Common.Text.Exchange
{
	/// <summary>
	/// La classe HtmlToTextWriter s'occupe d'�crire un texte html sous forme de HtmlDocument (arbre html pars�)
	/// dans un pav� de text par son Wrapper.TextWrapper
	/// </summary>
	/// 
	class HtmlToTextWriter
	{
		public HtmlToTextWriter (HtmlDocument thehtmldoc, Wrappers.TextWrapper textWrapper, Wrappers.ParagraphWrapper paraWrapper, TextNavigator navigator)
		{
			this.htmlDoc = thehtmldoc;
			this.navigator = navigator;
			this.paraWrapper = paraWrapper;
			this.textWrapper = textWrapper;
		}

		public void ProcessIt()
		{
			//this.navigator.Insert ("Hello you world !");

			this.textWrapper.Defined.ClearInvertItalic ();
			this.textWrapper.Defined.ClearColor ();
			//this.textWrapper.Defined.ClearConditions ();
			this.textWrapper.Defined.ClearFontFace ();
			//this.textWrapper.Defined.ClearFontFeatures ();
			//this.textWrapper.Defined.ClearFontGlue ();
			this.textWrapper.Defined.ClearFontSize ();
			//this.textWrapper.Defined.ClearFontStyle ();
			this.textWrapper.Defined.ClearInvertBold ();
			this.textWrapper.Defined.ClearInvertItalic ();
			this.textWrapper.Defined.ClearStrikeout() ;
			//this.textWrapper.Defined.ClearTextBox ();
			//this.textWrapper.Defined.ClearTextMarker ();
			this.textWrapper.Defined.ClearUnderline ();
			//this.textWrapper.Defined.ClearUnits ();
			//this.textWrapper.Defined.ClearUserTags ();
			this.textWrapper.Defined.ClearXscript ();

			ProcessNodes (this.htmlDoc.Nodes);
		}

		private void ProcessSpan(HtmlElement htmlelement)
		{
			string style = string.Empty;

			foreach (HtmlAttribute attr in htmlelement.Attributes)
			{
				switch (attr.Name)
				{
					case "style":
						style = attr.Value;
						break;
				}
			}

			string fontface = string.Empty;
			string fontsize = string.Empty;
			string fontcolor = string.Empty;

			if (style.Length > 0)
			{
				SpanStyleElements spanstyleelements = new SpanStyleElements (style);

				foreach (string element in spanstyleelements)
				{
					string value = spanstyleelements[element];

					if (element == "font-size")
					{
						fontsize = value;
						fontsize = fontsize.Trim (HtmlToTextWriter.quotesForTrim);
					}
					if (element == "font-family")
					{
						fontface = spanstyleelements[element];
					}
					if (element == "color")
					{
						fontcolor = spanstyleelements[element];
					}
				}

				HtmlFontProperties oldfontprops = SaveFontProps ();

				this.textWrapper.SuspendSynchronizations ();

				if (fontface.Length > 0)
				{
					this.textWrapper.Defined.FontFace = fontface;
				}

				if (fontsize.Length > 0)
				{
					int ptindex = fontsize.LastIndexOf ("pt");

					if (ptindex != -1)
					{
						fontsize = fontsize.Substring (0, ptindex);
						this.textWrapper.Defined.FontSize = double.Parse (fontsize) * HtmlTextOut.FontSizeFactor;
						this.textWrapper.Defined.Units = Common.Text.Properties.SizeUnits.Points;
					}
					else
					{
						fontsize = string.Empty;
					}
				}

				if (fontcolor.Length > 0)
				{
					// this.textWrapper.Defined.Color = ... ;
					Epsitec.Common.Drawing.RichColor richcolor = Epsitec.Common.Drawing.RichColor.FromHexa (fontcolor.Substring (1));
					this.textWrapper.Defined.Color = Epsitec.Common.Drawing.RichColor.ToString (richcolor);
				}

				this.textWrapper.ResumeSynchronizations ();

				ProcessNodes (htmlelement.Nodes);

				this.textWrapper.SuspendSynchronizations ();
				this.RestoreFontProps (oldfontprops);
				this.textWrapper.ResumeSynchronizations ();
			}
			else
			{
				ProcessNodes (htmlelement.Nodes);
			}
		}

		private void ProcessItalic(HtmlElement element)
		{
			this.textWrapper.SuspendSynchronizations ();
			this.textWrapper.Defined.InvertItalic = true;
			this.textWrapper.ResumeSynchronizations ();
			ProcessNodes (element.Nodes);
			this.textWrapper.SuspendSynchronizations ();
			this.textWrapper.Defined.InvertItalic = false;
			this.textWrapper.ResumeSynchronizations ();
		}

		private void ProcessBold(HtmlElement element)
		{
			this.textWrapper.SuspendSynchronizations ();
			this.textWrapper.Defined.InvertBold = true;
			this.textWrapper.ResumeSynchronizations ();
			ProcessNodes (element.Nodes);
			this.textWrapper.SuspendSynchronizations ();
			this.textWrapper.Defined.InvertBold = false;
			this.textWrapper.ResumeSynchronizations ();
		}



		private void ProcessSup(HtmlElement element)
		{
			this.textWrapper.SuspendSynchronizations ();
			FillSuperscriptDefinition (this.textWrapper.Defined.Xscript);
			this.textWrapper.ResumeSynchronizations ();
			ProcessNodes (element.Nodes);
			this.textWrapper.SuspendSynchronizations ();
			this.textWrapper.Defined.ClearXscript ();
			this.textWrapper.ResumeSynchronizations ();

		}

		private void ProcessSub(HtmlElement element)
		{
			this.textWrapper.SuspendSynchronizations ();
			FillSubscriptDefinition (this.textWrapper.Defined.Xscript);
			this.textWrapper.ResumeSynchronizations ();
			ProcessNodes (element.Nodes);
			this.textWrapper.SuspendSynchronizations ();
			this.textWrapper.Defined.ClearXscript ();
			this.textWrapper.ResumeSynchronizations ();
		}

		private void ProcessUnderline(HtmlElement element)
		{
			this.textWrapper.SuspendSynchronizations ();

			this.FillUnderlineDefinition (this.textWrapper.Defined.Underline);

			this.textWrapper.ResumeSynchronizations ();
			ProcessNodes (element.Nodes);
			this.textWrapper.SuspendSynchronizations ();
			this.textWrapper.Defined.ClearUnderline ();
			this.textWrapper.ResumeSynchronizations ();

		}

		private void ProcessFont(HtmlElement element)
		{
			string fontface = string.Empty;
			string fontsize = string.Empty;
			string fontcolor = string.Empty ;

			

			foreach (HtmlAttribute attr in element.Attributes)
			{
				switch (attr.Name)
				{
					case "face":
						fontface = attr.Value;
						break;
					case "size":
						fontsize = attr.Value;
						break;
					case "color":
						fontcolor = attr.Value;
						break;
				}
			}

			HtmlFontProperties oldfontprops = SaveFontProps();
			
			this.textWrapper.SuspendSynchronizations ();

			if (fontface.Length > 0)
			{
				this.textWrapper.Defined.FontFace = fontface;
			}

			if (fontsize.Length > 0)
			{
				this.textWrapper.Defined.FontSize = HtmlTextOut.HtmlFontSizeTopointFontSize (Int32.Parse (fontsize)) * HtmlTextOut.FontSizeFactor;
				this.textWrapper.Defined.Units = Common.Text.Properties.SizeUnits.Points;
			}

			if (fontcolor.Length > 0)
			{
				// this.textWrapper.Defined.Color = ... ;
				Epsitec.Common.Drawing.RichColor richcolor = Epsitec.Common.Drawing.RichColor.FromHexa (fontcolor.Substring (1));
				this.textWrapper.Defined.Color = Epsitec.Common.Drawing.RichColor.ToString(richcolor);
			}

			this.textWrapper.ResumeSynchronizations ();

			ProcessNodes (element.Nodes);

			this.textWrapper.SuspendSynchronizations ();
			this.RestoreFontProps (oldfontprops);
			this.textWrapper.ResumeSynchronizations ();

		}

		private HtmlFontProperties SaveFontProps()
		{
			HtmlFontProperties props = new HtmlFontProperties ();
			props.FontFace  = this.textWrapper.Defined.FontFace;
			props.FontColor = this.textWrapper.Defined.Color;
			props.FontSize  = this.textWrapper.Defined.FontSize;
			return props;
		}

		private void RestoreFontProps(HtmlFontProperties oldprops)
		{
			this.textWrapper.Defined.FontFace = oldprops.FontFace;
			this.textWrapper.Defined.Color = oldprops.FontColor;
			this.textWrapper.Defined.FontSize = oldprops.FontSize;
		}


		private void ProcessNodes(HtmlNodeCollection nodes)
		{
			foreach (HtmlNode node in nodes)
			{
				string s = node.ToString ();
				if (node is HtmlElement)
				{
					HtmlElement element = node as HtmlElement;

					if (element.Name == "span")
					{
						this.ProcessSpan (element);
					}
					else if (element.Name == "i" || element.Name == "em")
					{
						this.ProcessItalic (element);
					}
					else if (element.Name == "b" || element.Name == "strong")
					{
						this.ProcessBold (element);
					}
					else if (element.Name == "sup")
					{
						this.ProcessSup (element);
					}
					else if (element.Name == "sub")
					{
						this.ProcessSub (element);
					}
					else if (element.Name == "u")
					{
						this.ProcessUnderline (element);
					}
					else if (element.Name == "font")
					{
						this.ProcessFont (element);
					}
					else
					{
						// element html inconnu, on traite l'int�rieur sans s'occuper de l'�l�ment lui m�me
						ProcessNodes (element.Nodes);
					}
				}
				else
				{
					HtmlText text = node as HtmlText;
					this.navigator.Insert (text.Text);
				}
			}
		}


		private void FillSubscriptDefinition(Common.Text.Wrappers.TextWrapper.XscriptDefinition xscript)
		{
			xscript.IsDisabled = false;

			xscript.Scale  =  0.6;			// [HARDCODE]
			xscript.Offset = -0.15;			// [HARDCODE]
		}

		private void FillSuperscriptDefinition(Common.Text.Wrappers.TextWrapper.XscriptDefinition xscript)
		{
			xscript.IsDisabled = false;

			xscript.Scale  = 0.6;			// [HARDCODE]
			xscript.Offset = 0.25;			// [HARDCODE]
		}


		private void FillUnderlineDefinition(Common.Text.Wrappers.TextWrapper.XlineDefinition xline)
		{
			xline.IsDisabled = false;

			double thickness;
			double position;
			if (System.Globalization.RegionInfo.CurrentRegion.IsMetric)
			{
				thickness = 1.0;  // 0.1mm			// [HARDCODE]
				position  = 5.0;  // 0.5mm			// [HARDCODE]
			}
			else
			{
				thickness = 1.27;  // 0.005in			// [HARDCODE]
				position  = 5.08;  // 0.02in			// [HARDCODE]
			}

			xline.Thickness      = thickness;
			xline.ThicknessUnits = Common.Text.Properties.SizeUnits.Points;
			xline.Position       = position;
			xline.PositionUnits  = Common.Text.Properties.SizeUnits.Points;
			xline.DrawClass      = "";
			xline.DrawStyle      = null;
		}


		private HtmlDocument htmlDoc;
		private Wrappers.TextWrapper textWrapper;
		private Wrappers.ParagraphWrapper paraWrapper;
		private TextNavigator navigator;

		private static char[] quotesForTrim = "\"".ToCharArray ();

	}


	struct HtmlFontProperties
	{

		string fontFace;
		double fontSize;
		string fontColor;


		public HtmlFontProperties(string fontface, double fontsize, string fontcolor)
		{
			this.fontFace = fontface;
			this.fontColor = fontcolor;
			this.fontSize = fontsize;			
		}

		public string FontFace
		{
			get
			{
				return fontFace;
			}
			set
			{
				fontFace = value;
			}
		}

		public string FontColor
		{
			get
			{
				return fontColor;
			}
			set
			{
				fontColor = value;
			}
		}

		public double FontSize
		{
			get
			{
				return fontSize;
			}
			set
			{
				fontSize = value;
			}
		}
	}

}
