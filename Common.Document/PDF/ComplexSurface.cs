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


using Epsitec.Common.Drawing;

namespace Epsitec.Common.Document.PDF
{
    public enum Type
    {
        None,
        OpaqueRegular,
        TransparencyRegular,
        OpaqueGradient,
        TransparencyGradient,
        OpaquePattern,
        TransparencyPattern,
    }

    /// <summary>
    /// La classe ComplexSurface enregistre les informations d'une surface complexe.
    /// </summary>
    public class ComplexSurface
    {
        public ComplexSurface(
            int page,
            IPaintPort port,
            Objects.Layer layer,
            Objects.Abstract obj,
            Properties.Abstract fill,
            Properties.Line stroke,
            int rank,
            int id
        )
        {
            this.page = page;
            this.layer = layer;
            this.obj = obj;
            this.fill = fill;
            this.stroke = stroke;
            this.type = fill.TypeComplexSurfacePDF(port);
            this.isSmooth = fill.IsSmoothSurfacePDF(port);
            this.rank = rank;
            this.id = id;
            this.matrix = Transform.Identity;
        }

        public void Dispose()
        {
            //	Libère la surface.
            this.layer = null;
            this.obj = null;
            this.fill = null;
            this.stroke = null;
        }

        public int Page
        {
            //	Numéro de la page (1..n).
            get { return this.page; }
        }

        public Objects.Layer Layer
        {
            //	Calque contenant cette surface.
            get { return this.layer; }
        }

        public Objects.Abstract Object
        {
            //	Objet utilisant cette surface.
            get { return this.obj; }
        }

        public Properties.Abstract Fill
        {
            //	Propriété ayant généré cette surface (Gradient ou Font).
            get { return this.fill; }
        }

        public Properties.Line Stroke
        {
            //	Propriété ayant généré cette surface.
            get { return this.stroke; }
        }

        public Type Type
        {
            //	Type de la surface.
            get { return this.type; }
        }

        public bool IsSmooth
        {
            //	Surface floue ?
            get { return this.isSmooth; }
        }

        public int Rank
        {
            //	Rang dans l'objet (0..n).
            get { return this.rank; }
        }

        public int Id
        {
            //	Identificateur unique.
            get { return this.id; }
        }

        public Transform Matrix
        {
            //	Matrice de transformation.
            get { return this.matrix; }
            set { this.matrix = value; }
        }

        protected int page; // 1..n
        protected Objects.Layer layer;
        protected Objects.Abstract obj;
        protected Properties.Abstract fill;
        protected Properties.Line stroke;
        protected Type type;
        protected bool isSmooth;
        protected int rank;
        protected int id;
        protected Transform matrix;
    }
}
