using Epsitec.Common.Support;
using System.Xml.Serialization;

namespace Epsitec.Common.Pictogram.Data
{
	/// <summary>
	/// La classe ObjectRegular est la classe de l'objet graphique "polygone r�gulier".
	/// </summary>
	public class ObjectRegular : AbstractObject
	{
		public ObjectRegular()
		{
		}

		public override void CreateProperties()
		{
			PropertyLine lineMode = new PropertyLine();
			lineMode.Type = PropertyType.LineMode;
			this.AddProperty(lineMode);

			PropertyColor lineColor = new PropertyColor();
			lineColor.Type = PropertyType.LineColor;
			this.AddProperty(lineColor);

			PropertyGradient fillGradient = new PropertyGradient();
			fillGradient.Type = PropertyType.FillGradient;
			this.AddProperty(fillGradient);

			PropertyRegular regular = new PropertyRegular();
			regular.Type = PropertyType.Regular;
			regular.Changed += new EventHandler(this.HandleRegularChanged);
			this.AddProperty(regular);

			PropertyCorner corner = new PropertyCorner();
			corner.Type = PropertyType.Corner;
			this.AddProperty(corner);
		}

		protected override AbstractObject CreateNewObject()
		{
			return new ObjectRegular();
		}

		public override void Dispose()
		{
			if ( this.ExistProperty(3) )  this.PropertyRegular(3).Changed -= new EventHandler(this.HandleRegularChanged);
			base.Dispose();
		}


		// Nom de l'ic�ne.
		public override string IconName
		{
			get { return @"file:images/regular1.icon"; }
		}


		// D�tecte si la souris est sur l'objet.
		public override bool Detect(Drawing.Point pos)
		{
			Drawing.Rectangle bbox = this.BoundingBox;
			if ( !bbox.Contains(pos) )  return false;

			Drawing.Path path = this.PathBuild();

			double width = System.Math.Max(this.PropertyLine(0).Width/2, this.minimalWidth);
			if ( base.DetectOutline(path, width, pos) )  return true;
			
			if ( this.PropertyGradient(2).IsVisible() )
			{
				path.Close();
				if ( base.DetectFill(path, pos) )  return true;
			}
			return false;
		}

		// D�tecte si l'objet est dans un rectangle.
		public override bool Detect(Drawing.Rectangle rect, bool all)
		{
			Drawing.Rectangle fullBbox = this.BoundingBox;
			double width = System.Math.Max(this.PropertyLine(0).Width/2, this.minimalWidth);
			fullBbox.Inflate(width, width);
			return rect.Contains(fullBbox);
		}


		// D�place une poign�e.
		public override void MoveHandleProcess(int rank, Drawing.Point pos, IconContext iconContext)
		{
			if ( rank >= this.handles.Count )  // poign�e d'une propri�t� ?
			{
				base.MoveHandleProcess(rank, pos, iconContext);
				return;
			}

			iconContext.ConstrainSnapPos(ref pos);

			if ( rank == 0 )  // centre ?
			{
				Drawing.Point move = pos-this.Handle(rank).Position;
				this.Handle(0).Position = pos;
				this.Handle(1).Position += move;
			}
			else if ( rank == 1 )  // extr�mit� ?
			{
				this.Handle(1).Position = pos;
			}
			else if ( rank == 2 )  // renfoncement de l'�toile ?
			{
				double d1 = Drawing.Point.Distance(this.Handle(1).Position, this.Handle(0).Position);
				double d2 = Drawing.Point.Distance(this.Handle(1).Position, pos);
				this.PropertyRegular(3).Deep = d2/d1;
			}
			this.UpdateRegularHandle();
			this.dirtyBbox = true;
		}

		// Indique si le d�placement d'une poign�e doit se r�percuter sur les propri�t�s.
		public override bool IsMoveHandlePropertyChanged(int rank)
		{
			if ( rank >= this.handles.Count )  // poign�e d'une propri�t� ?
			{
				return base.IsMoveHandlePropertyChanged(rank);
			}
			return ( rank >= 2 );
		}


		// D�but de la cr�ation d'un objet.
		public override void CreateMouseDown(Drawing.Point pos, IconContext iconContext)
		{
			iconContext.ConstrainFixStarting(pos);
			this.HandleAdd(pos, HandleType.Primary);
			this.HandleAdd(pos, HandleType.Primary);
		}

		// D�placement pendant la cr�ation d'un objet.
		public override void CreateMouseMove(Drawing.Point pos, IconContext iconContext)
		{
			iconContext.ConstrainSnapPos(ref pos);
			this.Handle(1).Position = pos;
			this.dirtyBbox = true;
		}

		// Fin de la cr�ation d'un objet.
		public override void CreateMouseUp(Drawing.Point pos, IconContext iconContext)
		{
			iconContext.ConstrainSnapPos(ref pos);
			this.Handle(1).Position = pos;
			iconContext.ConstrainDelStarting();
			this.UpdateRegularHandle();
		}

		// Indique si l'objet doit exister. Retourne false si l'objet ne peut
		// pas exister et doit �tre d�truit.
		public override bool CreateIsExist(IconContext iconContext)
		{
			double len = Drawing.Point.Distance(this.Handle(0).Position, this.Handle(1).Position);
			return ( len > this.minimalSize );
		}


		private void HandleRegularChanged(object sender)
		{
			this.UpdateRegularHandle();
		}

		// Met � jour la poign�e pour la profondeur de l'�toile.
		protected void UpdateRegularHandle()
		{
			if ( this.handles.Count < 2 )  return;
			PropertyRegular reg = this.PropertyRegular(3);
			if ( !reg.Star )  // polygone ?
			{
				if ( this.handles.Count > 2 )
				{
					this.HandleDelete(2);
				}
			}
			else	// �toile ?
			{
				Drawing.Point pos = Drawing.Point.Scale(this.Handle(1).Position, this.Handle(0).Position, reg.Deep);

				if ( this.handles.Count == 2 )
				{
					this.HandleAdd(pos, HandleType.Secondary);
				}
				else
				{
					this.Handle(2).Position = pos;
				}
				this.Handle(2).IsSelected = this.Handle(1).IsSelected;
			}
		}

		
		// Met � jour le rectangle englobant l'objet.
		public override void UpdateBoundingBox()
		{
			Drawing.Path path = this.PathBuild();
			this.bbox = path.ComputeBounds();
			double width = this.PropertyLine(0).Width/2.0;
			this.bbox.Inflate(width, width);
		}

		// Calcule une droite de l'objet.
		protected bool ComputeLine(int i, out Drawing.Point a, out Drawing.Point b)
		{
			int total = this.PropertyRegular(3).NbFaces;
			Drawing.Point center = this.Handle(0).Position;
			Drawing.Point corner = this.Handle(1).Position;

			a = new Drawing.Point(0, 0);
			b = new Drawing.Point(0, 0);

			if ( this.PropertyRegular(3).Star )  // �toile ?
			{
				if ( i >= total*2 )  return false;

				Drawing.Point star = center + (corner-center)*(1-this.PropertyRegular(3).Deep);
				a = Drawing.Transform.RotatePoint(center, System.Math.PI*2*(i+0)/(total*2), (i%2==0) ? corner : star);
				b = Drawing.Transform.RotatePoint(center, System.Math.PI*2*(i+1)/(total*2), (i%2==0) ? star : corner);
				return true;
			}
			else	// polygone ?
			{
				if ( i >= total )  return false;

				a = Drawing.Transform.RotatePoint(center, System.Math.PI*2*(i+0)/total, corner);
				b = Drawing.Transform.RotatePoint(center, System.Math.PI*2*(i+1)/total, corner);
				return true;
			}
		}

		// Cr�e le chemin d'un polygone r�gulier.
		protected Drawing.Path PathBuild()
		{
			Drawing.Path path = new Drawing.Path();
			int total = this.PropertyRegular(3).NbFaces;
			PropertyCorner corner = this.PropertyCorner(4);

			if ( corner.CornerType == CornerType.Right )  // coins droits ?
			{
				Drawing.Point a;
				Drawing.Point b;

				if ( this.PropertyRegular(3).Star )  total *= 2;  // �toile ?

				for ( int i=0 ; i<total ; i++ )
				{
					this.ComputeLine(i, out a, out b);
					if ( i == 0 )  path.MoveTo(a);
					else           path.LineTo(a);
				}
				path.Close();
			}
			else	// coins quelconques ?
			{
				Drawing.Point p1;
				Drawing.Point s;
				Drawing.Point p2;

				if ( this.PropertyRegular(3).Star )  total *= 2;  // �toile ?

				for ( int i=0 ; i<total ; i++ )
				{
					int prev = i-1;  if ( prev < 0 )  prev = total-1;
					this.ComputeLine(prev, out p1, out s);
					this.ComputeLine(i, out s, out p2);
					this.PathCorner(path, p1,s,p2, corner);
				}
				path.Close();
			}

			return path;
		}

		// Cr�e le chemin d'un coin.
		protected void PathCorner(Drawing.Path path, Drawing.Point p1, Drawing.Point s, Drawing.Point p2, PropertyCorner corner)
		{
			double l1 = Drawing.Point.Distance(p1, s);
			double l2 = Drawing.Point.Distance(p2, s);
			double radius = System.Math.Min(corner.Radius, System.Math.Min(l1,l2)/2);
			Drawing.Point c1 = Drawing.Point.Move(s, p1, radius);
			Drawing.Point c2 = Drawing.Point.Move(s, p2, radius);
			if ( path.IsEmpty )  path.MoveTo(c1);
			else                 path.LineTo(c1);
			corner.PathCorner(path, c1,s,c2, radius);
		}

		// Dessine l'objet.
		public override void DrawGeometry(Drawing.Graphics graphics, IconContext iconContext)
		{
			base.DrawGeometry(graphics, iconContext);

			if ( this.TotalHandle < 2 )  return;

			Drawing.Path path = this.PathBuild();
			this.PropertyGradient(2).Render(graphics, iconContext, path, this.BoundingBox);

			graphics.Rasterizer.AddOutline(path, this.PropertyLine(0).Width, this.PropertyLine(0).Cap, this.PropertyLine(0).Join);
			graphics.RenderSolid(iconContext.AdaptColor(this.PropertyColor(1).Color));

			if ( this.IsHilite && iconContext.IsEditable )
			{
				if ( this.PropertyGradient(2).IsVisible() )
				{
					graphics.Rasterizer.AddSurface(path);
					graphics.RenderSolid(iconContext.HiliteSurfaceColor);
				}

				graphics.Rasterizer.AddOutline(path, this.PropertyLine(0).Width+iconContext.HiliteSize, this.PropertyLine(0).Cap, this.PropertyLine(0).Join);
				graphics.RenderSolid(iconContext.HiliteOutlineColor);
			}
		}
	}
}
