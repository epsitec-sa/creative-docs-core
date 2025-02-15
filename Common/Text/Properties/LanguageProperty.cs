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

using System.Xml.Linq;

namespace Epsitec.Common.Text.Properties
{
    /// <summary>
    /// La classe LanguageProperty définit la langue utilisée pour un fragment
    /// de texte.
    /// </summary>
    public class LanguageProperty : Property, Common.Support.IXMLSerializable<LanguageProperty>
    {
        public LanguageProperty() { }

        public LanguageProperty(string locale, double hyphenation)
        {
            this.locale = locale;
            this.hyphenation = hyphenation;
        }

        public override WellKnownType WellKnownType
        {
            get { return WellKnownType.Language; }
        }

        public override PropertyType PropertyType
        {
            get { return PropertyType.ExtraSetting; }
        }

        public string Locale
        {
            get { return this.locale; }
        }

        public double Hyphenation
        {
            get { return this.hyphenation; }
        }

        public override Property EmptyClone()
        {
            return new LanguageProperty();
        }

        public override void SerializeToText(System.Text.StringBuilder buffer)
        {
            SerializerSupport.Join(
                buffer,
                /**/SerializerSupport.SerializeString(this.locale),
                /**/SerializerSupport.SerializeDouble(this.hyphenation)
            );
        }

        public override bool HasEquivalentData(Common.Support.IXMLWritable otherWritable)
        {
            LanguageProperty other = (LanguageProperty)otherWritable;
            return this.locale == other.locale
                && (
                    this.hyphenation == other.hyphenation
                    || this.hyphenation.IsSafeNaN() && other.hyphenation.IsSafeNaN()
                );
        }

        public override XElement ToXML()
        {
            var root = new XElement(
                "LanguageProperty",
                new XAttribute("Hyphenation", this.hyphenation)
            );
            if (this.locale != null)
            {
                root.Add(new XAttribute("Locale", this.locale));
            }
            return root;
        }

        public static LanguageProperty FromXML(XElement xml)
        {
            return new LanguageProperty(xml);
        }

        private LanguageProperty(XElement xml)
        {
            this.locale = xml.Attribute("Locale")?.Value;
            this.hyphenation = (double)xml.Attribute("Hyphenation");
        }

        public override void DeserializeFromText(
            TextContext context,
            string text,
            int pos,
            int length
        )
        {
            string[] args = SerializerSupport.Split(text, pos, length);

            Debug.Assert.IsTrue(args.Length == 2);

            string locale = SerializerSupport.DeserializeString(args[0]);
            double hyphenation = SerializerSupport.DeserializeDouble(args[1]);

            this.locale = locale;
            this.hyphenation = hyphenation;
        }

        public override Property GetCombination(Property property)
        {
            Debug.Assert.IsTrue(property is Properties.LanguageProperty);

            LanguageProperty a = this;
            LanguageProperty b = property as LanguageProperty;
            LanguageProperty c = new LanguageProperty();

            c.hyphenation = NumberSupport.Combine(a.hyphenation, b.hyphenation);
            c.locale = b.locale == null ? a.locale : b.locale;

            return c;
        }

        public override void UpdateContentsSignature(IO.IChecksum checksum)
        {
            checksum.UpdateValue(this.locale);
            checksum.UpdateValue(this.hyphenation);
        }

        public override bool CompareEqualContents(object value)
        {
            return LanguageProperty.CompareEqualContents(this, value as LanguageProperty);
        }

        private static bool CompareEqualContents(LanguageProperty a, LanguageProperty b)
        {
            return a.locale == b.locale && NumberSupport.Equal(a.hyphenation, b.hyphenation);
        }

        private string locale;
        private double hyphenation;
    }
}
