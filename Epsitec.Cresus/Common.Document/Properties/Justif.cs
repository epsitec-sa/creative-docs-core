using Epsitec.Common.Widgets;
using Epsitec.Common.Drawing;
using System.Runtime.Serialization;

namespace Epsitec.Common.Document.Properties
{
	// ATTENTION: Ne jamais modifier les valeurs existantes de cette liste,
	// sous peine de plant�e lors de la d�s�rialisation.
	public enum JustifHorizontal
	{
		None    = 0,
		Left    = 1,			// |abc  |
		Center  = 2,			// | abc |
		Right   = 3,			// |  abc|
		Justif  = 4,			// |a b c| sauf la derni�re ligne
		All     = 5,			// |a b c| avec la derni�re ligne
		Stretch = 6,			// |abc| �tendu pour ObjectTextLine
	}

	public enum JustifVertical
	{
		None   = 0,
		Top    = 1,				// en haut
		Center = 2,				// au milieu
		Bottom = 3,				// en bas
	}

	public enum JustifOrientation
	{
		None        = 0,
		LeftToRight = 1,		// -> (normal)
		BottomToTop = 2,		// ^
		RightToLeft = 3,		// <-
		TopToBottom = 4,		// v
	}

	/// <summary>
	/// La classe Justif repr�sente une propri�t� d'un objet graphique.
	/// </summary>
	[System.Serializable()]
	public class Justif : Abstract
	{
		public Justif(Document document, Type type) : base(document, type)
		{
		}

		protected override void Initialise()
		{
			this.horizontal  = JustifHorizontal.Left;
			this.vertical    = JustifVertical.Top;
			this.orientation = JustifOrientation.LeftToRight;
			
			if ( this.document.Type == DocumentType.Pictogram )
			{
				this.marginH = 0.2;
				this.marginV = 0.1;
				this.offsetV = 0.0;
			}
			else
			{
				if ( System.Globalization.RegionInfo.CurrentRegion.IsMetric )
				{
					this.marginH = 10.0;  // 1mm
					this.marginV = 10.0;  // 1mm
					this.offsetV = 0.0;
				}
				else
				{
					this.marginH = 12.7;  // 0.05in
					this.marginV = 12.7;  // 0.05in
					this.offsetV = 0.0;
				}
			}
		}

		public JustifHorizontal Horizontal
		{
			get
			{
				return this.horizontal;
			}
			
			set
			{
				if ( this.horizontal != value )
				{
					this.NotifyBefore();
					this.horizontal = value;
					this.NotifyAfter();
				}
			}
		}

		public JustifVertical Vertical
		{
			get
			{
				return this.vertical;
			}
			
			set
			{
				if ( this.vertical != value )
				{
					this.NotifyBefore();
					this.vertical = value;
					this.NotifyAfter();
				}
			}
		}

		public JustifOrientation Orientation
		{
			get
			{
				return this.orientation;
			}
			
			set
			{
				if ( this.orientation != value )
				{
					this.NotifyBefore();
					this.orientation = value;
					this.NotifyAfter();
				}
			}
		}

		public double MarginH
		{
			get
			{
				return this.marginH;
			}
			
			set
			{
				if ( this.marginH != value )
				{
					this.NotifyBefore();
					this.marginH = value;
					this.NotifyAfter();
				}
			}
		}

		public double MarginV
		{
			get
			{
				return this.marginV;
			}
			
			set
			{
				if ( this.marginV != value )
				{
					this.NotifyBefore();
					this.marginV = value;
					this.NotifyAfter();
				}
			}
		}

		public double OffsetV
		{
			get
			{
				return this.offsetV;
			}
			
			set
			{
				if ( this.offsetV != value )
				{
					this.NotifyBefore();
					this.offsetV = value;
					this.NotifyAfter();
				}
			}
		}

		// Donne le petit texte pour les �chantillons.
		public override string SampleText
		{
			get
			{
				if ( this.horizontal == JustifHorizontal.Left    )  return "|ab |";
				if ( this.horizontal == JustifHorizontal.Center  )  return "| ab |";
				if ( this.horizontal == JustifHorizontal.Right   )  return "| ab|";
				if ( this.horizontal == JustifHorizontal.Justif  )  return "|ab|";
				if ( this.horizontal == JustifHorizontal.All     )  return "|ab.|";
				if ( this.horizontal == JustifHorizontal.Stretch )  return "<ab>";
				return "|ab|";
			}
		}

		// Retourne le nom d'un type donn�.
		public static string GetName(JustifHorizontal type)
		{
			string name = "";
			switch ( type )
			{
				case JustifHorizontal.Left:     name = Res.Strings.Property.Justif.JustifHLeft;     break;
				case JustifHorizontal.Center:   name = Res.Strings.Property.Justif.JustifHCenter;   break;
				case JustifHorizontal.Right:    name = Res.Strings.Property.Justif.JustifHRight;    break;
				case JustifHorizontal.Justif:   name = Res.Strings.Property.Justif.JustifHJustif;   break;
				case JustifHorizontal.All:      name = Res.Strings.Property.Justif.JustifHAll;      break;
				case JustifHorizontal.Stretch:  name = Res.Strings.Property.Justif.JustifHStretch;  break;
			}
			return name;
		}

		// Retourne l'ic�ne pour un type donn�.
		public static string GetIconText(JustifHorizontal type)
		{
			switch ( type )
			{
				case JustifHorizontal.Left:      return "JustifHLeft";
				case JustifHorizontal.Center:    return "JustifHCenter";
				case JustifHorizontal.Right:     return "JustifHRight";
				case JustifHorizontal.Justif:    return "JustifHJustif";
				case JustifHorizontal.All:       return "JustifHAll";
				case JustifHorizontal.Stretch:   return "JustifHStretch";
			}
			return "";
		}

		// Retourne le nom d'un type donn�.
		public static string GetName(JustifVertical type)
		{
			string name = "";
			switch ( type )
			{
				case JustifVertical.Top:     name = Res.Strings.Property.Justif.JustifVTop;     break;
				case JustifVertical.Center:  name = Res.Strings.Property.Justif.JustifVCenter;  break;
				case JustifVertical.Bottom:  name = Res.Strings.Property.Justif.JustifVBottom;  break;
			}
			return name;
		}

		// Retourne l'ic�ne pour un type donn�.
		public static string GetIconText(JustifVertical type)
		{
			switch ( type )
			{
				case JustifVertical.Top:     return "JustifVTop";
				case JustifVertical.Center:  return "JustifVCenter";
				case JustifVertical.Bottom:  return "JustifVBottom";
			}
			return "";
		}

		// Retourne le nom d'un type donn�.
		public static string GetName(JustifOrientation type)
		{
			string name = "";
			switch ( type )
			{
				case JustifOrientation.LeftToRight:  name = Res.Strings.Property.Justif.JustifOLR;  break;
				case JustifOrientation.BottomToTop:  name = Res.Strings.Property.Justif.JustifOBT;  break;
				case JustifOrientation.RightToLeft:  name = Res.Strings.Property.Justif.JustifORL;  break;
				case JustifOrientation.TopToBottom:  name = Res.Strings.Property.Justif.JustifOTB;  break;
			}
			return name;
		}

		// Retourne l'ic�ne pour un type donn�.
		public static string GetIconText(JustifOrientation type)
		{
			switch ( type )
			{
				case JustifOrientation.LeftToRight:  return "JustifOLR";
				case JustifOrientation.BottomToTop:  return "JustifOBT";
				case JustifOrientation.RightToLeft:  return "JustifORL";
				case JustifOrientation.TopToBottom:  return "JustifOTB";
			}
			return "";
		}


		// Indique si un changement de cette propri�t� modifie la bbox de l'objet.
		public override bool AlterBoundingBox
		{
			get { return true; }
		}

		// Effectue une copie de la propri�t�.
		public override void CopyTo(Abstract property)
		{
			base.CopyTo(property);
			Justif p = property as Justif;
			p.horizontal  = this.horizontal;
			p.vertical    = this.vertical;
			p.orientation = this.orientation;
			p.marginH     = this.marginH;
			p.marginV     = this.marginV;
			p.offsetV     = this.offsetV;
		}

		// Compare deux propri�t�s.
		public override bool Compare(Abstract property)
		{
			if ( !base.Compare(property) )  return false;

			Justif p = property as Justif;
			if ( p.horizontal  != this.horizontal  )  return false;
			if ( p.vertical    != this.vertical    )  return false;
			if ( p.orientation != this.orientation )  return false;
			if ( p.marginH     != this.marginH     )  return false;
			if ( p.marginV     != this.marginV     )  return false;
			if ( p.offsetV     != this.offsetV     )  return false;

			return true;
		}

		// Cr�e le panneau permettant d'�diter la propri�t�.
		public override Panels.Abstract CreatePanel(Document document)
		{
			Panels.Abstract.StaticDocument = document;
			return new Panels.Justif(document);
		}


		// Diminue la bo�te qui contient le texte en fonction des marges.
		// Retourne false si elle est trop petite.
		public bool DeflateBox(ref Point pbl, ref Point pbr, ref Point ptl, ref Point ptr)
		{
			double mh = this.marginH;
			double mv = this.marginV;

			double offset = 0;
			if ( this.vertical == JustifVertical.Center )
			{
				offset = this.offsetV * Point.Distance(pbl,ptl);
				mv = 0;
			}

			if ( Point.Distance(pbl,pbr) <= mh*2 )  return false;
			if ( Point.Distance(pbl,ptl) <= mv*2 )  return false;

			pbl = Point.Move(pbl, pbr, mh);
			ptl = Point.Move(ptl, ptr, mh);
			pbr = Point.Move(pbr, pbl, mh);
			ptr = Point.Move(ptr, ptl, mh);
			pbl = Point.Move(pbl, ptl, mv+offset);
			pbr = Point.Move(pbr, ptr, mv+offset);
			ptl = Point.Move(ptl, pbl, mv-offset);
			ptr = Point.Move(ptr, pbr, mv-offset);

			return true;
		}


		// D�but du d�placement global de la propri�t�.
		public override void MoveGlobalStarting()
		{
			if ( !this.document.Modifier.ActiveViewer.SelectorAdaptText )  return;

			this.InsertOpletProperty();

			this.initialMarginH = this.marginH;
			this.initialMarginV = this.marginV;
		}
		
		// Effectue le d�placement global de la propri�t�.
		public override void MoveGlobalProcess(Selector selector)
		{
			if ( !this.document.Modifier.ActiveViewer.SelectorAdaptText )  return;

			double zoom = selector.GetTransformZoom;
			this.marginH = this.initialMarginH*zoom;
			this.marginV = this.initialMarginV*zoom;

			this.document.Notifier.NotifyPropertyChanged(this);
		}

		
		#region Serialization
		// S�rialise la propri�t�.
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);

			info.AddValue("Horizontal", this.horizontal);
			info.AddValue("Vertical", this.vertical);
			info.AddValue("Orientation", this.orientation);
			info.AddValue("MarginH", this.marginH);
			info.AddValue("MarginV", this.marginV);
			info.AddValue("OffsetV", this.offsetV);
		}

		// Constructeur qui d�s�rialise la propri�t�.
		protected Justif(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.horizontal = (JustifHorizontal) info.GetValue("Horizontal", typeof(JustifHorizontal));
			this.vertical = (JustifVertical) info.GetValue("Vertical", typeof(JustifVertical));
			this.orientation = (JustifOrientation) info.GetValue("Orientation", typeof(JustifOrientation));
			this.marginH = info.GetDouble("MarginH");
			this.marginV = info.GetDouble("MarginV");
			this.offsetV = info.GetDouble("OffsetV");
		}
		#endregion

	
		protected JustifHorizontal		horizontal;
		protected JustifVertical		vertical;
		protected JustifOrientation		orientation;
		protected double				marginH;
		protected double				marginV;
		protected double				offsetV;
		protected double				initialMarginH;
		protected double				initialMarginV;
	}
}
