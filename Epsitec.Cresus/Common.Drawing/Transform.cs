namespace Epsitec.Common.Drawing
{
	/// <summary>
	/// La classe Transform représente une transformation 2D (matrice 2x2 et vecteur de
	/// translation). Elle supporte les opérations telles que la translation simple, la
	/// rotation, le changement d'échelle, ainsi que leur combinaison.
	/// </summary>
	public class Transform : System.IComparable
	{
		public Transform() : this (1, 0, 0, 1, 0, 0)
		{
		}
		
		public Transform(double xx, double xy, double yx, double yy, double tx, double ty)
		{
			this.xx = xx;
			this.xy = xy;
			this.yx = yx;
			this.yy = yy;
			this.tx = tx;
			this.ty = ty;
		}
		
		public Transform(Transform transform)
		{
			this.xx = transform.xx;
			this.xy = transform.xy;
			this.yx = transform.yx;
			this.yy = transform.yy;
			this.tx = transform.tx;
			this.ty = transform.ty;
		}
		
		
		public double				XX
		{
			get { return this.xx; }
			set
			{
				if (this.xx != value)
				{
					this.xx = value;
					this.OnChanged (System.EventArgs.Empty);
				}
			}
		}
		
		public double				XY
		{
			get { return this.xy; }
			set
			{
				if (this.xy != value)
				{
					this.xy = value;
					this.OnChanged (System.EventArgs.Empty);
				}
			}
		}
		
		public double				YX
		{
			get { return this.yx; }
			set
			{
				if (this.yx != value)
				{
					this.yx = value;
					this.OnChanged (System.EventArgs.Empty);
				}
			}
		}
		
		public double				YY
		{
			get { return this.yy; }
			set
			{
				if (this.yy != value)
				{
					this.yy = value;
					this.OnChanged (System.EventArgs.Empty);
				}
			}
		}
		
		public double				TX
		{
			get { return this.tx; }
			set
			{
				if (this.tx != value)
				{
					this.tx = value;
					this.OnChanged (System.EventArgs.Empty);
				}
			}
		}
		
		public double				TY
		{
			get { return this.ty; }
			set
			{
				if (this.ty != value)
				{
					this.ty = value;
					this.OnChanged (System.EventArgs.Empty);
				}
			}
		}
		
		
		public Point TransformDirect(Point pt)
		{
			double x = pt.X;
			double y = pt.Y;
			
			pt.X = this.xx * x + this.xy * y + this.tx;
			pt.Y = this.yx * x + this.yy * y + this.ty;
			
			return pt;
		}
		
		public Point TransformInverse(Point pt)
		{
			double det = this.xx * this.yy - this.xy * this.yx;
			
			System.Diagnostics.Debug.Assert (det != 0.0f);
			
			double x = pt.X - this.tx;
			double y = pt.Y - this.ty;
			
			pt.X = (  this.yy * x - this.xy * y) / det;
			pt.Y = (- this.yx * x + this.xx * y) / det;
			
			return pt;
		}
		
		
		public void Reset()
		{
			this.xx = 1;
			this.xy = 0;
			this.tx = 0;
			this.yx = 0;
			this.yy = 1;
			this.ty = 0;
			this.OnChanged (System.EventArgs.Empty);
		}
		
		public void Reset(Transform model)
		{
			this.xx = model.xx;
			this.xy = model.xy;
			this.tx = model.tx;
			this.yx = model.yx;
			this.yy = model.yy;
			this.ty = model.ty;
			this.OnChanged (System.EventArgs.Empty);
		}
		
		public void MultiplyBy(Transform t)
		{
			Transform c = Transform.Multiply (t, this);
			
			this.xx = c.xx;
			this.xy = c.xy;
			this.tx = c.tx;
			this.yx = c.yx;
			this.yy = c.yy;
			this.ty = c.ty;
			
			this.OnChanged (System.EventArgs.Empty);
		}
		
		public void MultiplyByPostfix(Transform t)
		{
			Transform c = Transform.Multiply (this, t);
			
			this.xx = c.xx;
			this.xy = c.xy;
			this.tx = c.tx;
			this.yx = c.yx;
			this.yy = c.yy;
			this.ty = c.ty;
			
			this.OnChanged (System.EventArgs.Empty);
		}
		
		public void Round()
		{
			Round (ref this.xx);
			Round (ref this.xy);
			Round (ref this.yx);
			Round (ref this.yy);
			Round (ref this.tx);
			Round (ref this.ty);
			
			this.OnChanged (System.EventArgs.Empty);
		}
		
		public void Translate(double tx, double ty)
		{
			this.tx += tx;
			this.ty += ty;
			
			this.OnChanged (System.EventArgs.Empty);
		}
		
		public void Translate(Point offset)
		{
			this.tx += offset.X;
			this.ty += offset.Y;
			
			this.OnChanged (System.EventArgs.Empty);
		}
		
		public void Rotate(double angle)
		{
			if (angle != 0)
			{
				this.MultiplyBy (Transform.FromRotation (angle));
			}
		}
		
		public void Rotate(double angle, Point center)
		{
			if (angle != 0)
			{
				this.MultiplyBy (Transform.FromRotation (angle, center.X, center.Y));
			}
		}
		
		public void Rotate(double angle, double x, double y)
		{
			if (angle != 0)
			{
				this.MultiplyBy (Transform.FromRotation (angle, x, y));
			}
		}
		
		public void Scale(double s)
		{
			if (s != 1.0f)
			{
				this.Scale (s, s);
			}
		}
		
		public void Scale(double sx, double sy)
		{
			this.xx *= sx;
			this.xy *= sy;
			this.yx *= sx;
			this.yy *= sy;
			this.tx *= sx;
			this.ty *= sy;
			
			this.OnChanged (System.EventArgs.Empty);
		}
		
		public void Scale(double sx, double sy, double cx, double cy)
		{
			this.Translate (-cx, -cy);
			this.Scale (sx, sy);
			this.Translate (cx, cy);
			
			this.OnChanged (System.EventArgs.Empty);
		}
		
		
		public static Transform FromScale(double sx, double sy)
		{
			return new Transform (sx, 0, 0, sy, 0, 0);
		}
		
		public static Transform FromScale(double sx, double sy, double cx, double cy)
		{
			return new Transform (sx, 0, 0, sy, cx-sx*cx, cy-sy*cy);
		}
		
		public static Transform FromTranslation(double tx, double ty)
		{
			return new Transform (1, 0, 0, 1, tx, ty);
		}
		
		public static Transform FromTranslation(Point offset)
		{
			return new Transform (1, 0, 0, 1, offset.X, offset.Y);
		}
		
		public static Transform FromRotation(double angle)
		{
			double alpha = angle * System.Math.PI / 180;
			double sin   = System.Math.Sin (alpha);
			double cos   = System.Math.Cos (alpha);
			
			return new Transform (cos, -sin, sin, cos, 0, 0);
		}
		
		public static Transform FromRotation(double angle, Point center)
		{
			return Transform.FromRotation (angle, center.X, center.Y);
		}
		
		public static Transform FromRotation(double angle, double cx, double cy)
		{
			Transform m = FromRotation (angle);
			
			m.tx = cx - m.xx * cx - m.xy * cy;
			m.ty = cy - m.yx * cx - m.yy * cy;
			
			return m;
		}
		
		
		public static Transform Inverse(Transform m)
		{
			double det   = m.xx * m.yy - m.xy * m.yx;
			Transform c = new Transform ();
			
			System.Diagnostics.Debug.Assert (det != 0.0f);
			
			double det_1 = 1.0f / det;
			
			c.xx =   m.yy * det_1;
			c.xy = - m.xy * det_1;
			c.yx = - m.yx * det_1;
			c.yy =   m.xx * det_1;
			
			c.tx = - c.xx * m.tx - c.xy * m.ty;
			c.ty = - c.yx * m.tx - c.yy * m.ty;
			
			return c;
		}
		
		public static Transform Multiply(Transform a, Transform b)
		{
			Transform c = new Transform ();
			
			c.xx = a.xx * b.xx + a.xy * b.yx;
			c.xy = a.xx * b.xy + a.xy * b.yy;
			c.tx = a.xx * b.tx + a.xy * b.ty + a.tx;
			c.yx = a.yx * b.xx + a.yy * b.yx;
			c.yy = a.yx * b.xy + a.yy * b.yy;
			c.ty = a.yx * b.tx + a.yy * b.ty + a.ty;
			
			return c;
		}
		
		public static Point Multiply(Transform a, Point b)
		{
			Point c = new Point ();
			
			c.X = a.xx * b.X + a.xy * b.Y + a.tx;
			c.Y = a.yx * b.X + a.yy * b.Y + a.ty;
			
			return c;
		}
		
		
		protected static readonly double epsilon = 0.00001;
		
		public static bool Equal(double a, double b)
		{
			double delta = a - b;
			return (delta < epsilon) && (delta > -epsilon);
		}
		
		public static bool Equal(Point a, Point b)
		{
			return Equal (a.X, b.X) && Equal (a.Y, b.Y);
		}
		
		public static bool Equal(Size a, Size b)
		{
			return Equal (a.Width, b.Width) && Equal (a.Height, b.Height);
		}
		
		public static bool Equal(Rectangle a, Rectangle b)
		{
			return Equal (a.Location, b.Location) && Equal (a.Size, b.Size);
		}
		
		public static bool IsZero(double a)
		{
			return (a < epsilon) && (a > -epsilon);
		}
		
		public static bool IsOne(double a)
		{
			return (a < 1+epsilon) && (a > 1-epsilon);
		}
		
		public static void Round(ref double a)
		{
			if (IsZero (a))
				a = 0;
			else if (IsOne (a))
				a = 1;
		}
		
		
		#region IComparable Members
		
		public int CompareTo(object obj)
		{
			if (obj is Transform)
			{
				Transform t = obj as Transform;
				
				if (t == null)
				{
					return 1;
				}
				if (Equal (this.XX, t.XX) &&
					Equal (this.XY, t.XY) &&
					Equal (this.YX, t.YX) &&
					Equal (this.YY, t.YY) &&
					Equal (this.TX, t.TX) &&
					Equal (this.TY, t.TY))
				{
					return 0;
				}
				if ((this.XX < t.XX) ||
					((this.XX == t.XX) &&
					 ((this.XY < t.XY) ||
					  ((this.XY == t.XY) &&
					   ((this.YX < t.YX) ||
					    ((this.YX == t.YX) &&
					     ((this.YY < t.YY) ||
					      ((this.YY == t.YY) &&
					       ((this.TX < t.TX) ||
					        ((this.TX == t.TX) &&
					         (this.TY < t.TY)))))))))))
				{
					return -1;
				}
				return 1;
			}
			
			throw new System.ArgumentException ("object is not a Transform");
		}
		
		#endregion
		
		public static bool operator ==(Transform a, Transform b)
		{
			object oa = a;
			object ob = b;
			
			if (oa == ob)
			{
				return true;
			}
			
			if ((oa != null) && (ob != null))
			{
				if (Equal (a.XX, b.XX) &&
					Equal (a.XY, b.XY) &&
					Equal (a.YX, b.YX) &&
					Equal (a.YY, b.YY) &&
					Equal (a.TX, b.TX) &&
					Equal (a.TY, b.TY))
				{
					return true;
				}
			}
			
			return false;
		}
		
		public static bool operator !=(Transform a, Transform b)
		{
			object oa = a;
			object ob = b;
			
			if (oa == ob)
			{
				return false;
			}
			
			if ((oa != null) && (ob != null))
			{
				if (!Equal (a.XX, b.XX) ||
					!Equal (a.XY, b.XY) ||
					!Equal (a.YX, b.YX) ||
					!Equal (a.YY, b.YY) ||
					!Equal (a.TX, b.TX) ||
					!Equal (a.TY, b.TY))
				{
					return true;
				}
			}
			
			return true;
		}
		
		
		public override bool Equals(object obj)
		{
			if (obj is Transform)
			{
				Transform t = obj as Transform;
				
				if (t == null)
				{
					return false;
				}
				if (Equal (this.XX, t.XX) &&
					Equal (this.XY, t.XY) &&
					Equal (this.YX, t.YX) &&
					Equal (this.YY, t.YY) &&
					Equal (this.TX, t.TX) &&
					Equal (this.TY, t.TY))
				{
					return true;
				}
			}
			
			return false;
		}
		
		public bool EqualsStrictly(object obj)
		{
			if (obj is Transform)
			{
				Transform t = obj as Transform;
				
				if (t == null)
				{
					return false;
				}
				if ((this.XX == t.XX) &&
					(this.XY == t.XY) &&
					(this.YX == t.YX) &&
					(this.YY == t.YY) &&
					(this.TX == t.TX) &&
					(this.TY == t.TY))
				{
					return true;
				}
			}
			
			return false;
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}

		public override string ToString()
		{
			System.Text.StringBuilder buffer = new System.Text.StringBuilder ();
			buffer.Append ("[ ");
			buffer.Append (this.XX.ToString ());
			buffer.Append (" ");
			buffer.Append (this.XY.ToString ());
			buffer.Append (" ");
			buffer.Append (this.YX.ToString ());
			buffer.Append (" ");
			buffer.Append (this.YY.ToString ());
			buffer.Append (" ");
			buffer.Append (this.TX.ToString ());
			buffer.Append (" ");
			buffer.Append (this.TY.ToString ());
			buffer.Append (" ]");
			
			return buffer.ToString ();
		}
					
		protected virtual void OnChanged(System.EventArgs e)
		{
			if (this.Changed != null)
			{
				this.Changed (this, e);
			}
		}

		
		public event System.EventHandler	Changed;
		
		private double				xx, xy, yx, yy;
		private double				tx, ty;
	}
}
