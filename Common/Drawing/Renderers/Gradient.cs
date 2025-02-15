/*
This file is part of CreativeDocs.

Copyright © 2003-2024, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland

CreativeDocs is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
any later version.

CreativeDocs is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/


namespace Epsitec.Common.Drawing.Renderers
{
    /// <summary>
    /// The <c>Gradient</c> class implements a renderer which can paint various styles
    /// of gradients, as defined by the <see cref="GradientFill"/> enumeration.
    /// </summary>
    public sealed class Gradient : IRenderer, System.IDisposable
    {
        public Gradient(Graphics graphics, AntigrainSharp.Renderer.Gradient gradientRenderer)
        {
            this.graphics = graphics;
            this.gradientRenderer = gradientRenderer;
            this.AlphaMutiplier = 1.0;
        }

        public double AlphaMutiplier { get; set; }

        public Transform Transform
        {
            get { return this.transform; }
            set
            {
                //	Note: on recalcule la transformation à tous les coups, parce que l'appelant peut être
                //	Graphics.UpdateTransform...

                /*                if (this.handle.IsInvalid)
                                {
                                    return;
                                }
                */
                this.transform = value;
                this.internalTransform = value.MultiplyBy(this.graphics.Transform);

                Transform inverse = Transform.Inverse(this.internalTransform);
                this.gradientRenderer.Matrix(
                    inverse.XX,
                    inverse.XY,
                    inverse.YX,
                    inverse.YY,
                    inverse.TX,
                    inverse.TY
                );
            }
        }

        public DrawingBitmap Pixmap
        {
            set
            {
                if (this.pixmap != value)
                {
                    if (value == null)
                    {
                        this.Detach();
                    }
                    else
                    {
                        this.Attach(value);
                    }
                }
            }
        }

        public GradientFill Fill
        {
            get { return this.fill; }
            set
            {
                if (this.fill != value)
                {
                    this.fill = value;
                    this.gradientRenderer.Select((int)this.fill);
                }
            }
        }

        public void SetAlphaMask(DrawingBitmap pixmap, MaskComponent component)
        {
            /*
            this.gradientRenderer.SetAlphaMask(
                (pixmap == null) ? System.IntPtr.Zero : pixmap.Handle,
                (AntigrainSharp.Renderer.MaskComponent)component
            );
            */
            throw new System.NotImplementedException();
        }

        public void SetColors(Color a, Color b)
        {
            this.SetColors(a.R, a.G, a.B, a.A, b.R, b.G, b.B, b.A);
        }

        public void SetColors(double ar, double ag, double ab, double br, double bg, double bb)
        {
            this.SetColors(ar, ag, ab, 1.0, br, bg, bb, 1.0);
        }

        public void SetColors(
            double ar,
            double ag,
            double ab,
            double aa,
            double br,
            double bg,
            double bb,
            double ba
        )
        {
            double[] r = new double[256];
            double[] g = new double[256];
            double[] b = new double[256];
            double[] a = new double[256];

            double deltaR = (br - ar) / 255.0;
            double deltaG = (bg - ag) / 255.0;
            double deltaB = (bb - ab) / 255.0;
            double deltaA = (ba - aa) / 255.0;

            for (int i = 0; i < 256; i++)
            {
                r[i] = ar + i * deltaR;
                g[i] = ag + i * deltaG;
                b[i] = ab + i * deltaB;
                a[i] = aa + i * deltaA;
            }

            this.SetColors(r, g, b, a);
        }

        public void SetColors(double[] r, double[] g, double[] b, double[] a)
        {
            if (r.Length != 256 || g.Length != 256 || b.Length != 256 || a.Length != 256)
            {
                throw new System.ArgumentOutOfRangeException("Color arrays missized");
            }

            if (this.AlphaMutiplier == 1.0)
            {
                this.gradientRenderer.Color1(r, g, b, a);
            }
            else
            {
                double[] rr = new double[r.Length];
                double[] gg = new double[r.Length];
                double[] bb = new double[r.Length];
                double[] aa = new double[r.Length];

                for (int i = 0; i < r.Length; i++)
                {
                    rr[i] = r[i] * this.AlphaMutiplier;
                    gg[i] = g[i] * this.AlphaMutiplier;
                    bb[i] = b[i] * this.AlphaMutiplier;
                    aa[i] = a[i] * this.AlphaMutiplier;
                }

                this.gradientRenderer.Color1(rr, gg, bb, aa);
            }
        }

        public void SetParameters(double r1, double r2)
        {
            this.gradientRenderer.Range(r1, r2);
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Detach();
        }

        #endregion


        private void Attach(DrawingBitmap pixmap)
        {
            this.Detach();
            this.pixmap = pixmap;
        }

        private void Detach()
        {
            if (this.pixmap != null)
            {
                this.pixmap = null;
                this.fill = GradientFill.None;
                this.transform = Transform.Identity;
                this.internalTransform = Transform.Identity;
            }
        }

        readonly Graphics graphics;
        internal readonly AntigrainSharp.Renderer.Gradient gradientRenderer;
        private DrawingBitmap pixmap;
        private GradientFill fill;
        private Transform transform = Transform.Identity;
        private Transform internalTransform = Transform.Identity;
    }
}
