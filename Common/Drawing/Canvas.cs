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

using System;
using System.Collections.Generic;

namespace Epsitec.Common.Drawing
{
    /// <summary>
    /// La classe Canvas permet de représenter une image vectorielle.
    /// </summary>
    public class Canvas : Image
    {
        internal Canvas(byte[] data, string debugTag)
        {
            this.engine = Canvas.engineFactory(data);
            System.Diagnostics.Debug.Assert(this.engine != null);
            IconKey[] iks = this.engine.IconKeys;
            System.Diagnostics.Debug.Assert(iks != null && iks.Length > 0);

            this.debugDeep = 0;
            this.DebugTag = $"{debugTag}[{data.Length}]";
            this.firstPageKey = iks[0]; // clé pour la première page

            this.keys = new KeyTable();
            foreach (IconKey ik in iks)
            {
                this.keys[ik] = new Canvas(this, ik);
            }

            this.size = Size.Empty;
            this.origin = new Point(0, 0);
        }

        protected Canvas(Canvas original, IconKey key)
        {
            //	Version selon IconKey du même dessin.
            //	On partage les données avec le modèle original.
            this.DebugTag = original.DebugTag;
            this.engine = original.engine;
            this.debugDeep = 1;

            this.key = key;
            this.keys = original.keys;

            this.effects = new EffectTable();
            this.effects[GlyphPaintStyle.Normal] = new Canvas(this, GlyphPaintStyle.Normal);
            this.effects[GlyphPaintStyle.Disabled] = new Canvas(this, GlyphPaintStyle.Disabled);
            this.effects[GlyphPaintStyle.Selected] = new Canvas(this, GlyphPaintStyle.Selected);
            this.effects[GlyphPaintStyle.Entered] = new Canvas(this, GlyphPaintStyle.Entered);
            this.effects[GlyphPaintStyle.Shadow] = new Canvas(this, GlyphPaintStyle.Shadow);
            this.size = original.size;
            this.origin = original.origin;
        }

        protected Canvas(Canvas original, GlyphPaintStyle style)
        {
            //	Version "normal", "disabled" ou "selected" du même dessin.
            //	On partage les données avec le modèle original.
            this.DebugTag = original.DebugTag;
            this.engine = original.engine;
            this.debugDeep = 2;

            this.key = original.key;
            this.keys = original.keys;

            this.paintStyle = style;
            this.effects = original.effects;
            this.size = original.size;
            this.origin = original.origin;
        }

        ~Canvas()
        {
            this.Dispose();
        }

        public static Canvas FromData(byte[] buffer, string debugTag = null)
        {
            return new Canvas(buffer, debugTag);
        }

        public int DebugDeep
        {
            //	Profondeur 0..2 pour la mise au point.
            get { return this.debugDeep; }
        }

        public override void DefineZoom(double zoom)
        {
            if (this.zoom != zoom)
            {
                this.zoom = zoom;
                this.DestroyCache();
            }
        }

        public override void DefineColor(Drawing.Color color)
        {
            if (this.color != color)
            {
                this.color = color;
                this.DestroyCache();
            }
        }

        public override void DefineAdorner(object adorner)
        {
            if (this.adorner != adorner)
            {
                this.adorner = adorner;
                this.DestroyCache();
            }
        }

        public IEnumerable<IconKey> IconKeys
        {
            get
            {
                var list = new List<IconKey>();

                foreach (IconKey key in this.keys.Keys)
                {
                    list.Add(key);
                }

                return list;
            }
        }

        public Image GetImageForIconKey(IconKey key)
        {
            //	Cherche l'image correspondant le mieux possible à une clé.
            System.Diagnostics.Debug.Assert(this.keys != null);

            //	Cherche une image correspondant le mieux possible à la langue et à la taille demandée.
            if (
                (key.Size.Width != 0 && key.Size.Height != 0)
                || key.Language != null
                || key.Style != null
            )
            {
                double min = 1000000;
                Canvas best = null;
                bool first = true;
                bool diff = false;
                double last = 0;
                foreach (System.Collections.DictionaryEntry dict in this.keys)
                {
                    // ATTENTION:
                    // L'ordre dans lequel ces clés est retourné par le dictionnaire est non défini
                    // et il se trouve qu'en exécutant avec le debugger attaché, il est différent de
                    // quand on lance l'application avec F5.

                    IconKey candidate = dict.Key as IconKey;
                    double delta = Canvas.Delta(key, candidate);

                    if (delta < min)
                    {
                        min = delta;
                        best = dict.Value as Canvas;
                    }

                    if (first)
                    {
                        last = delta;
                        first = false;
                    }
                    else
                    {
                        if (last != delta)
                            diff = true;
                    }
                }
                if (diff && best != null)
                    return best;
            }

            //	Cherche l'image correspondant à la clé de la première page.
            if (this.firstPageKey != null)
            {
                return this.keys[this.firstPageKey];
            }

            //	En désespoir de cause, retourne n'importe quelle image.
            foreach (System.Collections.DictionaryEntry dict in this.keys)
            {
                return dict.Value as Canvas;
            }

            throw new System.ArgumentException("GetImageForIconKey");
        }

        protected static double Delta(IconKey search, IconKey candidate)
        {
            //	Calcule une valeur 'delta' aussi petite que possible lorsque le candidat
            //	est bon, et même nulle si le candidat est parfait. A l'inverse, la valeur
            //	est grande si le candidat est mauvais.
            double delta = 0;

            if (string.IsNullOrEmpty(search.Style))
            {
                if (!string.IsNullOrEmpty(candidate.Style))
                {
                    delta += 100000;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(candidate.Style))
                {
                    delta += 100000;
                }
                else
                {
                    if (search.Style != candidate.Style)
                    {
                        delta += 100000;
                    }
                }
            }

            if (!string.IsNullOrEmpty(search.Language)) // cherche une langue précise ?
            {
                if (string.IsNullOrEmpty(candidate.Language))
                {
                    delta += 5000;
                }
                else
                {
                    if (search.Language != candidate.Language)
                    {
                        delta += 10000;
                    }
                }
            }

            if (search.Size.Width != 0 && search.Size.Height != 0) // cherche une taille précise ?
            {
                if (candidate.Size.Width != 0 && candidate.Size.Height != 0)
                {
                    delta += Canvas.Delta(search.Size, candidate.Size);
                }
                else
                {
                    delta += 100;
                }
            }

            return delta;
        }

        protected static double Delta(Size search, Size candidate)
        {
            //	Calcule la valeur 'delta' entre une taille cherchée et une tailla candidate.
            //	Si la taille candidate est plus grande que la taille cherchée, on retourne le
            //	plus grand delta (donc la valeur la plus défavorable), car on ne doit jamais
            //	essayer de caser une icône dans une surface trop petite (même d'un seul pixel).
            if (candidate.Width > search.Width || candidate.Height > search.Height)
            {
                return 100;
            }
            else
            {
                return (search.Width - candidate.Width) + (search.Height - candidate.Height);
            }
        }

        public override Image GetImageForPaintStyle(GlyphPaintStyle style)
        {
            //	Cherche l'image correspondant à un style.
            System.Diagnostics.Debug.Assert(this.effects != null);
            return this.effects[style];
        }

        public static void RegisterEngineFactory(Func<byte[], ICanvasEngine> factory)
        {
            Canvas.engineFactory = factory;
        }

        public override Bitmap BitmapImage
        {
            get
            {
                this.ValidateCache();
                return this.cache;
            }
        }

        public override Size Size
        {
            //	Taille de l'image, selon les préférences.
            get
            {
                this.ValidateGeometry();

                if (this.key != null && this.key.Size.Width != 0 && this.key.Size.Height != 0)
                {
                    return this.key.Size;
                }

                return this.size;
            }
        }

        public Point Origin
        {
            get
            {
                this.ValidateGeometry();
                return this.origin;
            }
        }

        public GlyphPaintStyle PaintStyle
        {
            get { return this.paintStyle; }
        }

        protected void ValidateCache()
        {
            //	Met le bitmap de l'image en cache, à la taille selon les préférences.
            if (this.cache != null)
            {
                return;
            }
            Size size = this.Size;
            uint width = (uint)(size.Width * this.zoom);
            uint height = (uint)(size.Height * this.zoom);
            size = new Size(width, height);
            this.cache = new DrawingBitmap(width, height);
            this.cache.DebugTag = this.DebugTag ?? "* Canvas cache";
            using (Graphics graphics = new Graphics(this.cache.GraphicContext))
            {
                int page = 0;
                if (this.key != null)
                {
                    page = this.key.PageRank;
                }
                this.engine.Paint(graphics, size, this.paintStyle, this.color, page, this.adorner);
            }
        }

        protected void ValidateGeometry()
        {
            if (this.isGeomOk != false)
            {
                return;
            }
            this.size = this.engine.Size;
            this.origin = this.engine.Origin;
            this.isGeomOk = true;
        }

        protected void DestroyCache()
        {
            if (this.cache == null)
            {
                return;
            }
            this.cache.Dispose();
            this.cache = null;
        }

        public override void Dispose()
        {
            if (this.isDisposed)
            {
                return;
            }
            this.isDisposed = true;

            this.DestroyCache();
            GC.SuppressFinalize(this);
        }

        public class IconKey
        {
            //	Informations sur l'icône contenue dans une page du document.
            public Size Size = new Size(0, 0);
            public string Language = null;
            public string Style = null;
            public int PageRank = -1;
        }

        protected class KeyTable : System.Collections.IDictionary
        {
            //	Collection de Canvas, accessibles par une clé IconKey.
            //	Les Canvas de cette collection on un debugDeep = 1.
            public KeyTable()
            {
                this.hash = new System.Collections.Hashtable();
            }

            public Canvas this[IconKey key]
            {
                get { return this.hash[key] as Canvas; }
                set { this.hash[key] = value; }
            }

            #region IDictionary Members
            public bool IsReadOnly
            {
                get { return this.hash.IsReadOnly; }
            }

            object System.Collections.IDictionary.this[object key]
            {
                get { return this.hash[key]; }
                set { this.hash[key] = value; }
            }

            public System.Collections.ICollection Keys
            {
                get { return this.hash.Keys; }
            }

            public System.Collections.ICollection Values
            {
                get { return this.hash.Values; }
            }

            public bool IsFixedSize
            {
                get { return this.hash.IsFixedSize; }
            }

            public System.Collections.IDictionaryEnumerator GetEnumerator()
            {
                return this.hash.GetEnumerator();
            }

            public bool Contains(object key)
            {
                return this.hash.Contains(key);
            }

            public void Add(object key, object value)
            {
                this.hash.Add(key, value);
            }

            public void Remove(object key)
            {
                this.hash.Remove(key);
            }

            public void Clear()
            {
                this.hash.Clear();
            }
            #endregion

            #region ICollection Members
            public bool IsSynchronized
            {
                get { return this.hash.IsSynchronized; }
            }

            public int Count
            {
                get { return this.hash.Count; }
            }

            public object SyncRoot
            {
                get { return this.hash.SyncRoot; }
            }

            public void CopyTo(System.Array array, int index)
            {
                this.CopyTo(array, index);
            }
            #endregion

            #region IEnumerable Members
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.hash.GetEnumerator();
            }
            #endregion

            protected System.Collections.Hashtable hash;
        }

        protected class EffectTable : System.Collections.IDictionary
        {
            //	Collection de Canvas, accessibles par une clé GlyphPaintStyle.
            //	Les Canvas de cette collection on un debugDeep = 2.
            public EffectTable()
            {
                this.hash = new System.Collections.Hashtable();
            }

            public Canvas this[GlyphPaintStyle key]
            {
                get { return this.hash[key] as Canvas; }
                set { this.hash[key] = value; }
            }

            #region IDictionary Members
            public bool IsReadOnly
            {
                get { return this.hash.IsReadOnly; }
            }

            object System.Collections.IDictionary.this[object key]
            {
                get { return this.hash[key]; }
                set { this.hash[key] = value; }
            }

            public System.Collections.ICollection Keys
            {
                get { return this.hash.Keys; }
            }

            public System.Collections.ICollection Values
            {
                get { return this.hash.Values; }
            }

            public bool IsFixedSize
            {
                get { return this.hash.IsFixedSize; }
            }

            public System.Collections.IDictionaryEnumerator GetEnumerator()
            {
                return this.hash.GetEnumerator();
            }

            public bool Contains(object key)
            {
                return this.hash.Contains(key);
            }

            public void Add(object key, object value)
            {
                this.hash.Add(key, value);
            }

            public void Remove(object key)
            {
                this.hash.Remove(key);
            }

            public void Clear()
            {
                this.hash.Clear();
            }
            #endregion

            #region ICollection Members
            public bool IsSynchronized
            {
                get { return this.hash.IsSynchronized; }
            }

            public int Count
            {
                get { return this.hash.Count; }
            }

            public object SyncRoot
            {
                get { return this.hash.SyncRoot; }
            }

            public void CopyTo(System.Array array, int index)
            {
                this.CopyTo(array, index);
            }
            #endregion

            #region IEnumerable Members
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.hash.GetEnumerator();
            }
            #endregion

            protected System.Collections.Hashtable hash;
        }

        protected bool isDisposed;
        protected bool isGeomOk;

        protected IconKey firstPageKey = null;
        protected IconKey key = null;
        protected KeyTable keys;

        protected GlyphPaintStyle paintStyle = GlyphPaintStyle.Invalid;
        protected EffectTable effects;

        protected readonly ICanvasEngine engine;
        protected Point origin;
        protected Size size;
        protected double zoom = 1.0;
        protected Drawing.Color color = Drawing.Color.Empty;
        protected object adorner = null;

        protected DrawingBitmap cache;
        protected int debugDeep = -1;

        static Func<byte[], ICanvasEngine> engineFactory;
    }
}
