using Epsitec.Common.Widgets;
using Epsitec.Common.Drawing;
using System.Runtime.Serialization;

namespace Epsitec.Common.Document.Properties
{
	// ATTENTION: Ne jamais modifier les valeurs existantes de cette liste,
	// sous peine de plant�e lors de la d�s�rialisation.
	public enum GradientFillType
	{
		None    = 0,
		Linear  = 1,
		Circle  = 2,
		Diamond = 3,
		Conic   = 4,
		Hatch   = 5,
		Dots    = 6,
		Squares = 7,
	}

	/// <summary>
	/// La classe Gradient repr�sente une propri�t� d'un objet graphique.
	/// </summary>
	[System.Serializable()]
	public class Gradient : Abstract
	{
		public Gradient(Document document, Type type) : base(document, type)
		{
		}

		protected override void Initialise()
		{
			this.fillType = GradientFillType.None;

			if ( this.type == Type.LineColor )
			{
				this.color1 = RichColor.FromBrightness(0.0);
				this.color2 = RichColor.FromBrightness(0.5);
			}
			else if ( this.type == Type.FillGradientVT )
			{
				this.color1 = RichColor.FromARGB(0.95, 0.8, 0.7, 0.7);
				this.color2 = RichColor.FromARGB(0.95, 0.6, 0.5, 0.5);
			}
			else if ( this.type == Type.FillGradientVL )
			{
				this.color1 = RichColor.FromARGB(0.95, 0.7, 0.8, 0.7);
				this.color2 = RichColor.FromARGB(0.95, 0.5, 0.6, 0.5);
			}
			else if ( this.type == Type.FillGradientVR )
			{
				this.color1 = RichColor.FromARGB(0.95, 0.7, 0.7, 0.8);
				this.color2 = RichColor.FromARGB(0.95, 0.5, 0.5, 0.6);
			}
			else
			{
				this.color1 = RichColor.FromBrightness(0.8);
				this.color2 = RichColor.FromBrightness(0.5);
			}
			this.angle  = 0.0;
			this.cx     = 0.5;
			this.cy     = 0.5;
			this.sx     = 0.5;
			this.sy     = 0.5;
			this.repeat = 1;
			this.middle = 0.0;
			this.smooth = 0.0;

			this.hatchAngle    = new double[Gradient.HatchMax];
			this.hatchWidth    = new double[Gradient.HatchMax];
			this.hatchDistance = new double[Gradient.HatchMax];

			this.hatchAngle[0] = 45.0;
			this.hatchAngle[1] =  0.0;

			if ( this.document.Type == DocumentType.Pictogram )
			{
				this.hatchWidth[0]    = 1.0;
				this.hatchDistance[0] = 5.0;
				this.hatchDistance[1] = 5.0;
			}
			else
			{
				this.hatchWidth[0]    =  5.0;  // 0.5mm
				this.hatchDistance[0] = 50.0;  // 5.0mm
				this.hatchDistance[1] = 50.0;  // 5.0mm
			}

			this.hatchWidth[1] = 0.0;

			this.initialHatchAngle    = new double[Gradient.HatchMax];
			this.initialHatchWidth    = new double[Gradient.HatchMax];
			this.initialHatchDistance = new double[Gradient.HatchMax];

		}

		// Mode de remplissage du d�grad�.
		public GradientFillType FillType
		{
			get
			{
				return this.fillType;
			}
			
			set
			{
				if ( this.fillType != value )
				{
					this.NotifyBefore();
					this.fillType = value;
					this.NotifyAfter();
				}
			}
		}

		// Couleur 1 du d�grad�.
		public Drawing.RichColor Color1
		{
			get
			{
				return this.color1;
			}
			
			set
			{
				if ( this.color1 != value )
				{
					this.NotifyBefore();
					this.color1 = value;
					this.NotifyAfter();
				}
			}
		}

		// Couleur 2 du d�grad�.
		public Drawing.RichColor Color2
		{
			get
			{
				return this.color2;
			}
			
			set
			{
				if ( this.color2 != value )
				{
					this.NotifyBefore();
					this.color2 = value;
					this.NotifyAfter();
				}
			}
		}

		// Angle du d�grad�.
		public double Angle
		{
			get
			{
				return this.angle;
			}

			set
			{
				value = System.Math.Max(value, -360);
				value = System.Math.Min(value,  360);

				if ( this.angle != value )
				{
					this.NotifyBefore();
					this.angle = value;
					this.NotifyAfter();
				}
			}
		}

		// Centre x du d�grad�.
		public double Cx
		{
			get
			{
				return this.cx;
			}
			
			set
			{
				if ( this.cx != value )
				{
					this.NotifyBefore();
					this.cx = value;
					this.NotifyAfter();
				}
			}
		}

		// Centre y du d�grad�.
		public double Cy
		{
			get
			{
				return this.cy;
			}
			
			set
			{
				if ( this.cy != value )
				{
					this.NotifyBefore();
					this.cy = value;
					this.NotifyAfter();
				}
			}
		}

		// Echelle x du d�grad�.
		public double Sx
		{
			get
			{
				return this.sx;
			}
			
			set
			{
				if ( this.sx != value )
				{
					this.NotifyBefore();
					this.sx = value;
					this.NotifyAfter();
				}
			}
		}

		// Echelle y du d�grad�.
		public double Sy
		{
			get
			{
				return this.sy;
			}
			
			set
			{
				if ( this.sy != value )
				{
					this.NotifyBefore();
					this.sy = value;
					this.NotifyAfter();
				}
			}
		}

		// Nombre de r�p�titions.
		public int Repeat
		{
			get
			{
				return this.repeat;
			}

			set
			{
				value = System.Math.Max(value, 1);
				value = System.Math.Min(value, 8);
				
				if ( this.repeat != value )
				{
					this.NotifyBefore();
					this.repeat = value;
					this.NotifyAfter();
				}
			}
		}

		// Point milieu des couleurs.
		public double Middle
		{
			get
			{
				return this.middle;
			}

			set
			{
				value = System.Math.Max(value, -5.0);
				value = System.Math.Min(value,  5.0);
				
				if ( this.middle != value )
				{
					this.NotifyBefore();
					this.middle = value;
					this.NotifyAfter();
				}
			}
		}

		// Rayon du flou.
		public double Smooth
		{
			get
			{
				return this.smooth;
			}

			set
			{
				value = System.Math.Max(value,  0.0);
				
				if ( this.smooth != value )
				{
					this.NotifyBefore();
					this.smooth = value;
					this.NotifyAfter();
				}
			}
		}

		// Angle des hachures.
		public double HatchAngle1
		{
			get
			{
				return this.hatchAngle[0];
			}

			set
			{
				if ( this.hatchAngle[0] != value )
				{
					this.NotifyBefore();
					this.hatchAngle[0] = value;
					this.NotifyAfter();
				}
			}
		}

		public double HatchAngle2
		{
			get
			{
				return this.hatchAngle[1];
			}

			set
			{
				if ( this.hatchAngle[1] != value )
				{
					this.NotifyBefore();
					this.hatchAngle[1] = value;
					this.NotifyAfter();
				}
			}
		}

		// Epaisseur des hachures.
		public double HatchWidth1
		{
			get
			{
				return this.hatchWidth[0];
			}

			set
			{
				if ( this.hatchWidth[0] != value )
				{
					this.NotifyBefore();
					this.hatchWidth[0] = value;
					this.NotifyAfter();
				}
			}
		}

		public double HatchWidth2
		{
			get
			{
				return this.hatchWidth[1];
			}

			set
			{
				if ( this.hatchWidth[1] != value )
				{
					this.NotifyBefore();
					this.hatchWidth[1] = value;
					this.NotifyAfter();
				}
			}
		}

		// Distance des hachures.
		public double HatchDistance1
		{
			get
			{
				return this.hatchDistance[0];
			}

			set
			{
				if ( this.hatchDistance[0] != value )
				{
					this.NotifyBefore();
					this.hatchDistance[0] = value;
					this.NotifyAfter();
				}
			}
		}

		public double HatchDistance2
		{
			get
			{
				return this.hatchDistance[1];
			}

			set
			{
				if ( this.hatchDistance[1] != value )
				{
					this.NotifyBefore();
					this.hatchDistance[1] = value;
					this.NotifyAfter();
				}
			}
		}

		public double GetHatchAngle(int rank)
		{
			return this.hatchAngle[rank];
		}

		public void SetHatchAngle(int rank, double value)
		{
			if ( this.hatchAngle[rank] != value )
			{
				this.NotifyBefore();
				this.hatchAngle[rank] = value;
				this.NotifyAfter();
			}
		}

		public double GetHatchWidth(int rank)
		{
			return this.hatchWidth[rank];
		}

		public void SetHatchWidth(int rank, double value)
		{
			if ( this.hatchWidth[rank] != value )
			{
				this.NotifyBefore();
				this.hatchWidth[rank] = value;
				this.NotifyAfter();
			}
		}

		public double GetHatchDistance(int rank)
		{
			return this.hatchDistance[rank];
		}

		public void SetHatchDistance(int rank, double value)
		{
			if ( this.hatchDistance[rank] != value )
			{
				this.NotifyBefore();
				this.hatchDistance[rank] = value;
				this.NotifyAfter();
			}
		}


		// Retourne le nom d'un type donn�.
		public static string GetName(GradientFillType type)
		{
			string name = "";
			switch ( type )
			{
				case GradientFillType.None:     name = Res.Strings.Property.Gradient.None;     break;
				case GradientFillType.Linear:   name = Res.Strings.Property.Gradient.Linear;   break;
				case GradientFillType.Circle:   name = Res.Strings.Property.Gradient.Circle;   break;
				case GradientFillType.Diamond:  name = Res.Strings.Property.Gradient.Diamond;  break;
				case GradientFillType.Conic:    name = Res.Strings.Property.Gradient.Conic;    break;
				case GradientFillType.Hatch:    name = Res.Strings.Property.Gradient.Hatch;    break;
				case GradientFillType.Dots:     name = Res.Strings.Property.Gradient.Dots;     break;
				case GradientFillType.Squares:  name = Res.Strings.Property.Gradient.Squares;  break;
			}
			return name;
		}

		// Retourne l'ic�ne pour un type donn�.
		public static string GetIconText(GradientFillType type)
		{
			switch ( type )
			{
				case GradientFillType.None:     return "GradientNone";
				case GradientFillType.Linear:   return "GradientLinear";
				case GradientFillType.Circle:   return "GradientCircle";
				case GradientFillType.Diamond:  return "GradientDiamond";
				case GradientFillType.Conic:    return "GradientConic";
				case GradientFillType.Hatch:    return "GradientHatch";
				case GradientFillType.Dots:     return "GradientDots";
				case GradientFillType.Squares:  return "GradientSquares";
			}
			return "";
		}

		
		// Indique si une impression complexe est n�cessaire.
		public override bool IsComplexPrinting
		{
			get
			{
				if ( this.fillType != GradientFillType.None )  return true;
				if ( this.color1.A > 0.0 && this.color1.A < 1.0 )  return true;
				if ( this.smooth > 0.0 )  return true;
				return false;
			}
		}

		// Indique si un changement de cette propri�t� modifie la bbox de l'objet.
		public override bool AlterBoundingBox
		{
			get { return true; }  // (*)
		}
		// (*)	Doit rendre "true" � cause de BoundingBox, lorsque this.fillType passe
		//		de GradientFillType.None � autre chose, et invers�ment.

		// Indique si le d�grad� est visible.
		public override bool IsVisible(IPaintPort port)
		{
			Drawing.Color c1 = this.color1.Basic;
			Drawing.Color c2 = this.color2.Basic;

			if ( this.fillType == GradientFillType.None )
			{
				if ( port != null )
				{
					c1 = port.GetFinalColor(c1);
				}

				return ( c1.A > 0 );
			}
			else
			{
				if ( port != null )
				{
					c1 = port.GetFinalColor(c1);
					c2 = port.GetFinalColor(c2);
				}

				return ( c1.A > 0 || c2.A > 0 );
			}
		}

		// Effectue une copie de la propri�t�.
		public override void CopyTo(Abstract property)
		{
			base.CopyTo(property);
			Gradient p = property as Gradient;
			p.fillType = this.fillType;
			p.color1   = this.color1;
			p.color2   = this.color2;
			p.angle    = this.angle;
			p.cx       = this.cx;
			p.cy       = this.cy;
			p.sx       = this.sx;
			p.sy       = this.sy;
			p.repeat   = this.repeat;
			p.middle   = this.middle;
			p.smooth   = this.smooth;

			for ( int i=0 ; i<Gradient.HatchMax ; i++ )
			{
				p.hatchAngle[i]    = this.hatchAngle[i];
				p.hatchWidth[i]    = this.hatchWidth[i];
				p.hatchDistance[i] = this.hatchDistance[i];
			}
		}

		// Compare deux propri�t�s.
		public override bool Compare(Abstract property)
		{
			if ( !base.Compare(property) )  return false;

			Gradient p = property as Gradient;
			if ( p.fillType != this.fillType )  return false;
			if ( p.color1   != this.color1   )  return false;
			if ( p.color2   != this.color2   )  return false;
			if ( p.angle    != this.angle    )  return false;
			if ( p.cx       != this.cx       )  return false;
			if ( p.cy       != this.cy       )  return false;
			if ( p.sx       != this.sx       )  return false;
			if ( p.sy       != this.sy       )  return false;
			if ( p.repeat   != this.repeat   )  return false;
			if ( p.middle   != this.middle   )  return false;
			if ( p.smooth   != this.smooth   )  return false;

			for ( int i=0 ; i<Gradient.HatchMax ; i++ )
			{
				if ( p.hatchAngle[i]    != this.hatchAngle[i]    )  return false;
				if ( p.hatchWidth[i]    != this.hatchWidth[i]    )  return false;
				if ( p.hatchDistance[i] != this.hatchDistance[i] )  return false;
			}

			return true;
		}

		// Cr�e le panneau permettant d'�diter la propri�t�.
		public override Panels.Abstract CreatePanel(Document document)
		{
			Panels.Abstract.StaticDocument = document;
			return new Panels.Gradient(document);
		}


		// Calcule la bbox pour la repr�sentation du d�grad�.
		public Rectangle BoundingBoxGeom(Rectangle bbox)
		{
			bbox.Inflate(this.smooth);
			return bbox;
		}

		// Retourne la valeur d'engraissement pour la bbox.
		public static double InflateBoundingBoxWidth(Shape shape)
		{
			Gradient surface = shape.PropertySurface as Gradient;
			if ( surface == null )  return 0.0;
			return surface.InflateBoundingBoxWidth();
		}

		// Retourne la valeur d'engraissement pour la bbox.
		public double InflateBoundingBoxWidth()
		{
			return this.smooth;
		}

		// Engraisse la bbox en fonction de la propri�t�.
		public override void InflateBoundingBox(SurfaceAnchor sa, ref Rectangle bboxFull)
		{
			if ( this.fillType == GradientFillType.None )  return;

			Point center = sa.ToAbs(new Point(this.cx, this.cy));
			Point p1 = sa.ToAbs(new Point(this.cx+this.sx, this.cy+this.sy));
			Point p2 = sa.ToAbs(new Point(this.cx-this.sx, this.cy+this.sy));
			Point p3 = sa.ToAbs(new Point(this.cx-this.sx, this.cy-this.sy));
			Point p4 = sa.ToAbs(new Point(this.cx+this.sx, this.cy-this.sy));

			if ( this.fillType == GradientFillType.Linear )
			{
				bboxFull.MergeWith(p1);
				bboxFull.MergeWith(p3);
				bboxFull.MergeWith(this.ComputeExtremity(p3, p1, 0.0, 0.2, 0));
				bboxFull.MergeWith(this.ComputeExtremity(p3, p1, 0.0, 0.2, 1));
				bboxFull.MergeWith(this.ComputeExtremity(p1, p3, 0.0, 0.2, 0));
				bboxFull.MergeWith(this.ComputeExtremity(p1, p3, 0.0, 0.2, 1));
			}
			else
			{
				bboxFull.MergeWith(p1);
				bboxFull.MergeWith(p2);
				bboxFull.MergeWith(p3);
				bboxFull.MergeWith(p4);
			}
		}


		// D�but du d�placement d'une poign�e.
		public override void MoveHandleStarting(Objects.Abstract obj, int rank, Point pos, DrawingContext drawingContext)
		{
			if ( this.fillType == GradientFillType.Hatch   ||
				 this.fillType == GradientFillType.Dots    ||
				 this.fillType == GradientFillType.Squares )
			{
				if ( rank == 0 )  // angle ?
				{
					SurfaceAnchor sa = this.SurfaceAnchor(obj);
					drawingContext.ConstrainAddCenter(sa.Center);
				}

				if ( rank == 1 )  // �paisseur ?
				{
				}

				if ( rank == 2 )  // distance ?
				{
				}
			}
			else
			{
				if ( rank == 0 )  // centre ?
				{
					drawingContext.ConstrainAddHV(this.GetHandlePosition(obj, 0));
				}

				if ( rank == 1 )  // coin ?
				{
					Point center = this.GetHandlePosition(obj, 0);
					drawingContext.ConstrainAddHV(center);
					drawingContext.ConstrainAddLine(center, this.GetHandlePosition(obj, 1));
				}

				if ( rank == 2 )  // angle ?
				{
					drawingContext.ConstrainAddCenter(this.GetHandlePosition(obj, 0));
				}
			}
		}

		// Nombre de poign�es.
		public override int TotalHandle(Objects.Abstract obj)
		{
			return 3;
		}

		// Indique si une poign�e est visible.
		public override bool IsHandleVisible(Objects.Abstract obj, int rank)
		{
			if ( !this.document.Modifier.IsPropertiesExtended(this.type) )
			{
				return false;
			}

			if ( this.fillType == GradientFillType.None )
			{
				return false;
			}
			else if ( this.fillType == GradientFillType.Conic )
			{
				return (rank < 3);
			}
			else if ( this.fillType == GradientFillType.Hatch   ||
					  this.fillType == GradientFillType.Dots    ||
					  this.fillType == GradientFillType.Squares )
			{
				return (rank < 3);
			}
			else
			{
				return (rank < 2);
			}
		}

		// Retourne la surface � utiliser pour les poign�es du d�grad�.
		protected SurfaceAnchor SurfaceAnchor(Objects.Abstract obj)
		{
			obj.SurfaceAnchor.LineUse = (this.type == Properties.Type.LineColor);
			return obj.SurfaceAnchor;
		}

		// Retourne la position d'une poign�e.
		public override Point GetHandlePosition(Objects.Abstract obj, int rank)
		{
			Point pos = new Point();
			SurfaceAnchor sa = this.SurfaceAnchor(obj);

			if ( this.fillType == GradientFillType.Hatch   ||
				 this.fillType == GradientFillType.Dots    ||
				 this.fillType == GradientFillType.Squares )
			{
				if ( rank == 0 )  // angle ?
				{
					pos = sa.Center+Transform.RotatePointDeg(sa.Direction+this.hatchAngle[0], new Point(0, sa.RotateRadius));
				}

				if ( rank == 1 )  // �paisseur ?
				{
					pos = sa.Center+Transform.RotatePointDeg(sa.Direction+this.hatchAngle[0]-90, new Point(0, this.hatchWidth[0]));
				}

				if ( rank == 2 )  // distance ?
				{
					pos = sa.Center+Transform.RotatePointDeg(sa.Direction+this.hatchAngle[0]-90, new Point(0, this.hatchDistance[0]));
				}
			}
			else
			{
				Point center = sa.ToAbs(new Point(this.cx, this.cy));
				Point corner = sa.ToAbs(new Point(this.cx+this.sx, this.cy+this.sy));

				if ( rank == 0 )  // centre ?
				{
					pos = center;
				}

				if ( rank == 1 )  // coin ?
				{
					pos = corner;
				}

				if ( rank == 2 )  // angle ?
				{
					double radius = System.Math.Min(System.Math.Abs(this.sx*sa.Width), System.Math.Abs(this.sy*sa.Height));
					pos = center+Transform.RotatePointDeg(sa.Direction+this.angle, new Point(0, radius));
				}
			}

			return pos;
		}

		// Modifie la position d'une poign�e.
		public override void SetHandlePosition(Objects.Abstract obj, int rank, Point pos)
		{
			SurfaceAnchor sa = this.SurfaceAnchor(obj);

			if ( this.fillType == GradientFillType.Hatch   ||
				 this.fillType == GradientFillType.Dots    ||
				 this.fillType == GradientFillType.Squares )
			{
				if ( rank == 0 )  // angle ?
				{
					this.HatchAngle1 = Point.ComputeAngleDeg(sa.Center, pos)-90-sa.Direction;
				}

				if ( rank == 1 )  // �paisseur ?
				{
					this.HatchWidth1 = Point.Distance(sa.Center, pos);
				}

				if ( rank == 2 )  // distance ?
				{
					this.HatchDistance1 = Point.Distance(sa.Center, pos);
				}
			}
			else
			{
				if ( rank == 0 )  // centre ?
				{
					Point rel = sa.ToRel(pos);
					this.Cx = rel.X;
					this.Cy = rel.Y;
				}

				if ( rank == 1 )  // coin ?
				{
					Point rel = sa.ToRel(pos);
					this.Sx = rel.X-this.cx;
					this.Sy = rel.Y-this.cy;
				}

				if ( rank == 2 )  // angle ?
				{
					Point center = sa.ToAbs(new Point(this.cx, this.cy));
					this.Angle = Point.ComputeAngleDeg(center, pos)-90-sa.Direction;
				}
			}

			base.SetHandlePosition(obj, rank, pos);
		}


		// Calcule l'extr�mit� gauche ou droite de la fl�che.
		protected Point ComputeExtremity(Point p1, Point p2, double para, double perp, int rank)
		{
			double distPara = Point.Distance(p1, p2)*para;
			double distPerp = Point.Distance(p1, p2)*perp;
			Point c = Point.Move(p2, p1, distPara);
			Point p = Point.Move(c, Point.Symmetry(p2, p1), distPerp);
			double angle = (rank==0) ? 90 : -90;
			return Transform.RotatePointDeg(c, angle, p);
		}

		// Dessine les traits de construction avant les poign�es.
		public override void DrawEdit(Graphics graphics, DrawingContext drawingContext, Objects.Abstract obj)
		{
			if ( !obj.IsSelected ||
				 obj.IsGlobalSelected ||
				 obj.IsEdited ||
				 !this.IsHandleVisible(obj, 0) )  return;

			SurfaceAnchor sa = this.SurfaceAnchor(obj);
			Point center = sa.ToAbs(new Point(this.cx, this.cy));
			Point p1 = sa.ToAbs(new Point(this.cx+this.sx, this.cy+this.sy));
			Point p2 = sa.ToAbs(new Point(this.cx-this.sx, this.cy+this.sy));
			Point p3 = sa.ToAbs(new Point(this.cx-this.sx, this.cy-this.sy));
			Point p4 = sa.ToAbs(new Point(this.cx+this.sx, this.cy-this.sy));

			double initialWidth = graphics.LineWidth;
			graphics.LineWidth = 1.0/drawingContext.ScaleX;

			if ( this.fillType == GradientFillType.Linear )
			{
				graphics.AddLine(p3, p1);
				graphics.AddLine(p1, this.ComputeExtremity(p3, p1, 0.2, 0.1, 0));
				graphics.AddLine(p1, this.ComputeExtremity(p3, p1, 0.2, 0.1, 1));  // fl�che

				graphics.AddLine(this.ComputeExtremity(p3, p1, 0.0, 0.2, 0), this.ComputeExtremity(p3, p1, 0.0, 0.2, 1));
				graphics.AddLine(this.ComputeExtremity(p1, p3, 0.0, 0.2, 0), this.ComputeExtremity(p1, p3, 0.0, 0.2, 1));
			}
			else if ( this.fillType == GradientFillType.Hatch   ||
					  this.fillType == GradientFillType.Dots    ||
					  this.fillType == GradientFillType.Squares )
			{
				center = sa.Center;
				double radius = System.Math.Min(sa.Width, sa.Height)*0.5;
				Point pa = center+Transform.RotatePointDeg(sa.Direction+this.hatchAngle[0], new Point(0, radius*0.9));
				graphics.AddLine(pa, this.ComputeExtremity(center, pa, 0.4, 0.2, 0));
				graphics.AddLine(pa, this.ComputeExtremity(center, pa, 0.4, 0.2, 1));  // fl�che
				graphics.AddLine(center, pa);

				Point pb = center+Transform.RotatePointDeg(sa.Direction+this.hatchAngle[0]+90, new Point(0, radius));
				Point pc = center+Transform.RotatePointDeg(sa.Direction+this.hatchAngle[0]-90, new Point(0, radius));
				graphics.AddLine(pb, pc);
			}
			else
			{
				graphics.AddLine(p1, p2);
				graphics.AddLine(p2, p3);
				graphics.AddLine(p3, p4);
				graphics.AddLine(p4, p1);

				if ( this.fillType == GradientFillType.Circle )
				{
					Transform ot = graphics.Transform;
					Transform t = new Transform();
					t.RotateDeg(sa.Direction);
					t.Translate(center);
					t.MultiplyBy(graphics.Transform);
					graphics.Transform = t;
					graphics.AddCircle(0.0, 0.0, System.Math.Abs(this.sx*sa.Width), System.Math.Abs(this.sy*sa.Height));
					graphics.Transform = ot;
				}

				if ( this.fillType == GradientFillType.Diamond )
				{
					graphics.AddLine(p1, p3);
					graphics.AddLine(p2, p4);
				}

				if ( this.fillType == GradientFillType.Conic )
				{
					double radius = System.Math.Min(System.Math.Abs(this.sx*sa.Width), System.Math.Abs(this.sy*sa.Height));
					Point pa = center+Transform.RotatePointDeg(sa.Direction+this.angle, new Point(0, radius));
					graphics.AddLine(pa, this.ComputeExtremity(center, pa, 0.4, 0.2, 0));
					graphics.AddLine(pa, this.ComputeExtremity(center, pa, 0.4, 0.2, 1));  // fl�che
					graphics.AddLine(center, pa);
				}
			}

			graphics.RenderSolid(Drawing.Color.FromBrightness(0.6));
			graphics.LineWidth = initialWidth;
		}


		// Calcule le facteur de progression dans la couleur [0..1].
		// Si M>0:  P=1-(1-P)^(1+M)
		// Si M<0:  P=P^(1-M)
		public double GetProgressColorFactor(double progress)
		{
			if ( this.repeat > 1 )
			{
				int i = (int)(progress*this.repeat);
				progress = (progress*this.repeat)%1.0;
				if ( i%2 != 0 )  progress = 1.0-progress;
			}
			if ( this.middle != 0.0 )
			{
				if ( this.middle > 0.0 )
				{
					progress = 1.0-System.Math.Pow(1.0-progress, 1.0+this.middle);
				}
				else
				{
					progress = System.Math.Pow(progress, 1.0-this.middle);
				}
			}
			return progress;
		}

		// Indique si la surface PDF est floue.
		public override bool IsSmoothSurfacePDF(IPaintPort port)
		{
			Drawing.Color c1 = port.GetFinalColor(this.color1.Basic);
			Drawing.Color c2 = port.GetFinalColor(this.color2.Basic);

			if ( this.fillType == GradientFillType.None )
			{
				if ( this.smooth > 0.0 && c1.A > 0.0 )
				{
					return true;
				}
			}

			if ( this.fillType == GradientFillType.Linear  ||
				 this.fillType == GradientFillType.Circle  ||
				 this.fillType == GradientFillType.Diamond ||
				 this.fillType == GradientFillType.Conic   )
			{
				if ( this.smooth > 0.0 && (c1.A > 0.0 || c2.A > 0.0) )
				{
					return true;
				}
			}

			return false;
		}

		// Donne le type PDF de la surface complexe.
		public override PDF.Type TypeComplexSurfacePDF(IPaintPort port)
		{
			Drawing.Color c1 = port.GetFinalColor(this.color1.Basic);
			Drawing.Color c2 = port.GetFinalColor(this.color2.Basic);

			if ( this.fillType == GradientFillType.None )
			{
				if ( c1.A == 0.0 )
				{
					return PDF.Type.None;
				}

				if ( c1.A < 1.0 )
				{
					return PDF.Type.TransparencyRegular;
				}

				return PDF.Type.OpaqueRegular;
			}

			if ( c1.A == 0.0 && c2.A == 0.0 )
			{
				return PDF.Type.None;
			}

			if ( this.fillType == GradientFillType.Linear  ||
				 this.fillType == GradientFillType.Circle  ||
				 this.fillType == GradientFillType.Diamond ||
				 this.fillType == GradientFillType.Conic   )
			{
				if ( c1.A < 1.0 || c2.A < 1.0 )
				{
					return PDF.Type.TransparencyGradient;
				}
				else
				{
					return PDF.Type.OpaqueGradient;
				}
			}

			if ( this.fillType == GradientFillType.Hatch   ||
				 this.fillType == GradientFillType.Dots    ||
				 this.fillType == GradientFillType.Squares )
			{
				if ( c1.A < 1.0 || c2.A < 1.0 )
				{
					return PDF.Type.TransparencyPattern;
				}
				else
				{
					return PDF.Type.OpaquePattern;
				}
			}

			return PDF.Type.None;
		}

		
		// Modifie l'espace des couleurs.
		public override bool ChangeColorSpace(ColorSpace cs)
		{
			this.NotifyBefore();
			this.color1.ColorSpace = cs;
			this.color2.ColorSpace = cs;
			this.NotifyAfter();
			this.document.Notifier.NotifyPropertyChanged(this);
			return true;
		}

		// Modifie les couleurs.
		public override bool ChangeColor(double adjust, bool stroke)
		{
			if ( this.type == Type.LineColor )
			{
				if ( !stroke )  return false;
			}
			else
			{
				if ( stroke )  return false;
			}

			this.NotifyBefore();
			this.color1.ChangeBrightness(adjust);
			this.color2.ChangeBrightness(adjust);
			this.NotifyAfter();
			this.document.Notifier.NotifyPropertyChanged(this);
			return true;
		}

		
		#region Serialization
		// S�rialise la propri�t�.
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);

			info.AddValue("FillType", this.fillType);
			info.AddValue("Color1", this.color1);
			if ( this.fillType != GradientFillType.None )
			{
				info.AddValue("Color2", this.color2);
				info.AddValue("Angle", this.angle);
				info.AddValue("Cx", this.cx);
				info.AddValue("Cy", this.cy);
				info.AddValue("Sx", this.sx);
				info.AddValue("Sy", this.sy);
				info.AddValue("Repeat", this.repeat);
				info.AddValue("Middle", this.middle);
			}
			if ( this.fillType == GradientFillType.Hatch   ||
				 this.fillType == GradientFillType.Dots    ||
				 this.fillType == GradientFillType.Squares )
			{
				info.AddValue("HatchAngle", this.hatchAngle);
				info.AddValue("HatchWidth", this.hatchWidth);
				info.AddValue("HatchDistance", this.hatchDistance);
			}
			info.AddValue("Smooth", this.smooth);
		}

		// Constructeur qui d�s�rialise la propri�t�.
		protected Gradient(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fillType = (GradientFillType) info.GetValue("FillType", typeof(GradientFillType));

			if ( this.document.IsRevisionGreaterOrEqual(1,0,22) )
			{
				this.color1 = (Drawing.RichColor) info.GetValue("Color1", typeof(Drawing.RichColor));
			}
			else
			{
				Drawing.Color c1 = (Drawing.Color) info.GetValue("Color1", typeof(Drawing.Color));
				this.color1 = new RichColor(c1);
			}

			if ( this.fillType != GradientFillType.None )
			{
				if ( this.document.IsRevisionGreaterOrEqual(1,0,22) )
				{
					this.color2 = (Drawing.RichColor) info.GetValue("Color2", typeof(Drawing.RichColor));
				}
				else
				{
					Drawing.Color c2 = (Drawing.Color) info.GetValue("Color2", typeof(Drawing.Color));
					this.color2 = new RichColor(c2);
				}

				this.angle  = info.GetDouble("Angle");
				this.cx     = info.GetDouble("Cx");
				this.cy     = info.GetDouble("Cy");
				this.sx     = info.GetDouble("Sx");
				this.sy     = info.GetDouble("Sy");
				this.repeat = info.GetInt32("Repeat");
				this.middle = info.GetDouble("Middle");
			}

			bool hatch = false;
			if ( this.fillType == GradientFillType.Hatch )
			{
				hatch = true;
			}
			else if ( this.fillType == GradientFillType.Dots    ||
					  this.fillType == GradientFillType.Squares )
			{
				if ( this.document.IsRevisionGreaterOrEqual(1,0,6) )
				{
					hatch = true;
				}
			}
			if ( hatch )
			{
				this.hatchAngle    = (double[]) info.GetValue("HatchAngle",    typeof(double[]));
				this.hatchWidth    = (double[]) info.GetValue("HatchWidth",    typeof(double[]));
				this.hatchDistance = (double[]) info.GetValue("HatchDistance", typeof(double[]));
			}
			this.smooth = info.GetDouble("Smooth");
		}
		#endregion

	
		protected GradientFillType		fillType;
		protected RichColor				color1;
		protected RichColor				color2;
		protected double				angle;
		protected double				cx;
		protected double				cy;
		protected double				sx;
		protected double				sy;
		protected int					repeat;
		protected double				middle;
		protected double				smooth;
		protected double[]				hatchAngle;
		protected double[]				hatchWidth;
		protected double[]				hatchDistance;
		protected double				initialAngle;
		protected double				initialCx;
		protected double				initialCy;
		protected double				initialSx;
		protected double				initialSy;
		protected double				initialSmooth;
		protected double[]				initialHatchAngle;
		protected double[]				initialHatchWidth;
		protected double[]				initialHatchDistance;
		public static readonly int		HatchMax = 2;
	}
}
