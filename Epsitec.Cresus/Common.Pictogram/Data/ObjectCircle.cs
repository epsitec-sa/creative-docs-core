using System.Xml.Serialization;

namespace Epsitec.Common.Pictogram.Data
{
	/// <summary>
	/// La classe ObjectCircle est la classe de l'objet graphique "cercle".
	/// </summary>
	public class ObjectCircle : AbstractObject
	{
		public ObjectCircle()
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
		}

		protected override AbstractObject CreateNewObject()
		{
			return new ObjectCircle();
		}


		// Nom de l'ic�ne.
		public override string IconName
		{
			get { return @"file:images/circle.icon"; }
		}


		// D�tecte si la souris est sur l'objet.
		public override bool Detect(Drawing.Point pos)
		{
			if ( this.isHide )  return false;

			Drawing.Point p1 = this.Handle(0).Position;
			Drawing.Point p2 = this.Handle(1).Position;
			double width = System.Math.Max(this.PropertyLine(0).Width/2, this.minimalWidth);
			double radius = Drawing.Point.Distance(p1, p2)+width;
			double dist = Drawing.Point.Distance(p1, pos);

			if ( this.PropertyGradient(2).IsVisible() )
			{
				return ( dist <= radius );
			}
			else
			{
				return ( dist <= radius && dist >= radius-width*2 );
			}
		}

		// D�tecte si l'objet est dans un rectangle.
		public override bool Detect(Drawing.Rectangle rect, bool all)
		{
			if ( this.isHide )  return false;
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
			this.dirtyBbox = true;
		}


		// D�but de la cr�ation d'un objet.
		public override void CreateMouseDown(Drawing.Point pos, IconContext iconContext)
		{
			iconContext.ConstrainFixStarting(pos, ConstrainType.Line);
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
		}

		// Indique si l'objet doit exister. Retourne false si l'objet ne peut
		// pas exister et doit �tre d�truit.
		public override bool CreateIsExist(IconContext iconContext)
		{
			double len = Drawing.Point.Distance(this.Handle(0).Position, this.Handle(1).Position);
			return ( len > this.minimalSize );
		}

		
		// Met � jour le rectangle englobant l'objet.
		protected override void UpdateBoundingBox()
		{
			Drawing.Path path = this.PathBuild();
			this.bboxThin = path.ComputeBounds();

			this.bboxGeom = this.bboxThin;
			this.PropertyLine(0).InflateBoundingBox(ref this.bboxGeom);

			this.bboxFull = this.bboxGeom;
			this.bboxGeom.MergeWith(this.PropertyGradient(2).BoundingBoxGeom(this.bboxThin));
			this.bboxFull.MergeWith(this.PropertyGradient(2).BoundingBoxFull(this.bboxThin));
			this.bboxFull.MergeWith(this.bboxGeom);
		}

		// Cr�e le chemin d'un cercle.
		protected Drawing.Path PathCircle(Drawing.Point c, double rx, double ry)
		{
			Drawing.Path path = new Drawing.Path();
			path.MoveTo(c.X-rx, c.Y);
			path.CurveTo(c.X-rx, c.Y+ry*0.56, c.X-rx*0.56, c.Y+ry, c.X, c.Y+ry);
			path.CurveTo(c.X+rx*0.56, c.Y+ry, c.X+rx, c.Y+ry*0.56, c.X+rx, c.Y);
			path.CurveTo(c.X+rx, c.Y-ry*0.56, c.X+rx*0.56, c.Y-ry, c.X, c.Y-ry);
			path.CurveTo(c.X-rx*0.56, c.Y-ry, c.X-rx, c.Y-ry*0.56, c.X-rx, c.Y);
			path.Close();
			return path;
		}

		// Cr�e le chemin de l'objet.
		protected Drawing.Path PathBuild()
		{
			Drawing.Point center = this.Handle(0).Position;
			double radius = Drawing.Point.Distance(center, this.Handle(1).Position);
			return this.PathCircle(center, radius, radius);
		}

		// Dessine l'objet.
		public override void DrawGeometry(Drawing.Graphics graphics, IconContext iconContext)
		{
			if ( this.isHide )  return;
			base.DrawGeometry(graphics, iconContext);

			if ( this.TotalHandle != 2 )  return;

			Drawing.Path path = this.PathBuild();
			this.PropertyGradient(2).Render(graphics, iconContext, path, this.BoundingBoxThin);

			graphics.Rasterizer.AddOutline(path, this.PropertyLine(0).Width, this.PropertyLine(0).Cap, this.PropertyLine(0).Join, this.PropertyLine(0).Limit);
			graphics.RenderSolid(iconContext.AdaptColor(this.PropertyColor(1).Color));

			if ( this.IsHilite && iconContext.IsEditable )
			{
				if ( this.PropertyGradient(2).IsVisible() )
				{
					graphics.Rasterizer.AddSurface(path);
					graphics.RenderSolid(iconContext.HiliteSurfaceColor);
				}

				graphics.Rasterizer.AddOutline(path, this.PropertyLine(0).Width+iconContext.HiliteSize, this.PropertyLine(0).Cap, this.PropertyLine(0).Join, this.PropertyLine(0).Limit);
				graphics.RenderSolid(iconContext.HiliteOutlineColor);
			}
		}
	}
}
