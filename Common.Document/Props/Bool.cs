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

using System.Runtime.Serialization;
using System.Xml.Linq;

namespace Epsitec.Common.Document.Properties
{
    /// <summary>
    /// La classe Bool représente une propriété d'un objet graphique.
    /// </summary>
    [System.Serializable()]
    public class Bool : Abstract, Support.IXMLSerializable<Bool>
    {
        public Bool(Document document, Type type)
            : base(document, type) { }

        protected override void Initialize()
        {
            base.Initialize();
            this.boolValue = false;
        }

        public bool BoolValue
        {
            //	Valeur de la propriété.
            get { return this.boolValue; }
            set
            {
                if (this.boolValue != value)
                {
                    this.NotifyBefore();
                    this.boolValue = value;
                    this.NotifyAfter();
                }
            }
        }

        public static string GetName(bool type)
        {
            //	Retourne le nom d'un type donné.
            if (type)
                return Res.Strings.Property.Close.Yes;
            else
                return Res.Strings.Property.Close.No;
        }

        public static string GetIconText(bool type)
        {
            //	Retourne l'icône pour un type donné.
            if (type)
                return "CloseYes";
            else
                return "CloseNo";
        }

        public override bool AlterBoundingBox
        {
            //	Indique si un changement de cette propriété modifie la bbox de l'objet.
            get { return (this.type == Type.PolyClose); }
        }

        public override void CopyTo(Abstract property)
        {
            //	Effectue une copie de la propriété.
            base.CopyTo(property);
            Bool p = property as Bool;
            p.boolValue = this.boolValue;
        }

        public override bool Compare(Abstract property)
        {
            //	Compare deux propriétés.
            if (!base.Compare(property))
                return false;

            Bool p = property as Bool;
            if (p.boolValue != this.boolValue)
                return false;

            return true;
        }

        public override Panels.Abstract CreatePanel(Document document)
        {
            //	Crée le panneau permettant d'éditer la propriété.
            Panels.Abstract.StaticDocument = document;
            return new Panels.Bool(document);
        }

        #region Serialization
        public new bool HasEquivalentData(Support.IXMLWritable other)
        {
            Bool otherBool = (Bool)other;
            return base.HasEquivalentData(other) && this.boolValue == otherBool.boolValue;
        }

        public override XElement ToXML()
        {
            return new XElement(
                "Bool",
                base.IterXMLParts(),
                new XAttribute("Value", this.boolValue)
            );
        }

        public static Bool FromXML(XElement xml)
        {
            return new Bool(xml);
        }

        private Bool(XElement xml)
            : base(xml)
        {
            this.boolValue = (bool)xml.Attribute("Value");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //	Sérialise la propriété.
            base.GetObjectData(info, context);

            info.AddValue("BoolValue", this.boolValue);
        }

        protected Bool(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            //	Constructeur qui désérialise la propriété.
            this.boolValue = info.GetBoolean("BoolValue");
        }
        #endregion


        protected bool boolValue;
    }
}
