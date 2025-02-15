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


using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Xml.Linq;

namespace Epsitec.Common.OpenType
{
    /// <summary>
    /// The <c>FontName</c> structure stores the face and style name as a
    /// single object.
    /// </summary>
    [System.Serializable]
    public struct FontName
        : System.IComparable<FontName>,
            System.IEquatable<FontName>,
            Common.Support.IXMLSerializable<FontName>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontName"/> structure.
        /// </summary>
        /// <param name="face">The font face.</param>
        /// <param name="style">The font style.</param>
        public FontName(string face, FontStyle style)
        {
            this.face = face;
            this.style = style;
        }

        public FontName(string face, string style)
            : this(face, FontStyle.Normal)
        {
            foreach (string styleElement in style.ToLower().Split(' '))
            {
                switch (styleElement)
                {
                    case "bold":
                        this.style |= FontStyle.Bold;
                        break;
                    case "italic":
                        this.style |= FontStyle.Italic;
                        break;
                }
            }
        }

        public FontName(string face)
            : this(face, "") { }

#if false
        /// <summary>
        /// Initializes a new instance of the <see cref="FontName"/> structure.
        /// </summary>
        /// <param name="fontIdentity">The font identity.</param>
        public FontName(FontIdentity fontIdentity)
        {
            this.face = fontIdentity.InvariantFaceName;
            this.style = fontIdentity.InvariantStyleName;
        }
#endif

        /// <summary>
        /// Gets the name of the font face.
        /// </summary>
        /// <value>The name of the font face.</value>
        public string FaceName
        {
            get { return this.face; }
        }

        public FontStyle Style
        {
            get { return this.style; }
        }

        /// <summary>
        /// Gets the name of the font style.
        /// </summary>
        /// <value>The name of the font style.</value>
        public string StyleName
        {
            get
            {
                switch (this.style)
                {
                    case FontStyle.Normal:
                    case FontStyle.Bold:
                    case FontStyle.Italic:
                        return this.style.ToString();
                    case FontStyle.BoldItalic:
                        return "Bold Italic";
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Gets the full name of the font (face and style).
        /// </summary>
        /// <value>The full name of the font (face and style).</value>
        public string FullName
        {
            get
            {
                if (this.style == FontStyle.Normal)
                {
                    return this.face;
                }
                return FontName.GetFullName(this.face, this.StyleName);
            }
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return this.FullName.GetHashCode();
        }

        public IEnumerable<FontName> EnumerateWithFallbackStyles()
        {
            var fallbackStyles = this.EnumerateFallbackStyles();
            foreach (FontStyle style in fallbackStyles)
            {
                yield return new FontName(this.face, style);
            }
        }

        private IEnumerable<FontStyle> EnumerateFallbackStyles()
        {
            // The order of preference for the fallback is as follows:
            // - first the style that contains our style (e.g. BoldItalic when looking for Bold)
            // - then the Normal type
            // - at last, other styles

            // As a tie-breaker rule, we prefer the Bold style over the Italic

            switch (this.style)
            {
                case FontStyle.Normal:
                    yield return FontStyle.Bold;
                    yield return FontStyle.Italic;
                    yield return FontStyle.BoldItalic;
                    yield break;
                case FontStyle.Bold:
                    yield return FontStyle.BoldItalic;
                    yield return FontStyle.Normal;
                    yield return FontStyle.Italic;
                    yield break;
                case FontStyle.Italic:
                    yield return FontStyle.BoldItalic;
                    yield return FontStyle.Normal;
                    yield return FontStyle.Bold;
                    yield break;
                case FontStyle.BoldItalic:
                    yield return FontStyle.Bold;
                    yield return FontStyle.Italic;
                    yield return FontStyle.Normal;
                    yield break;
            }
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        /// <c>true</c> if obj and this instance are the same type and represent
        /// the same value; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            FontName that = (FontName)obj;

            return this.Equals(that);
        }

        /// <summary>
        /// Gets the full font name, based on the concatenation of the font face
        /// name and font style name.
        /// </summary>
        /// <param name="face">The font face name.</param>
        /// <param name="style">The font style name.</param>
        /// <returns>The full font name.</returns>
        public static string GetFullName(string face, string style)
        {
            if (string.IsNullOrEmpty(style))
            {
                return face;
            }
            else
            {
                return string.Concat(face, " ", style);
            }
        }

#if false
        /// <summary>
        /// Gets the full hash of the specified full font name. This will sort
        /// all elements in alphabetic order and remove any <c>"Regular"</c>,
        /// <c>"Normal"</c> or <c>"Roman"</c> from the name; duplicates are
        /// also removed.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <returns>The full name hash.</returns>
        public static string GetFullHash(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return null;
            }

            HashSet<string> names = new HashSet<string>();

            string clean = fullName.Replace("(", "").Replace(")", "").ToLowerInvariant();
            string[] split = clean.Split(' ');

            //	TODO: remove the xbkl --> extra black hack

            foreach (string element in split)
            {
                switch (element)
                {
                    case "regular":
                    case "normal":
                    case "roman":
                        break;

                    case "bk":
                        names.Add("book");
                        break;

                    case "hv":
                        names.Add("heavy");
                        break;

                    case "cn":
                        names.Add("condensed");
                        break;

                    case "mdcn":
                        names.Add("medium");
                        names.Add("condensed");
                        break;

                    case "xblk":
                        names.Add("extra");
                        names.Add("black");
                        break;

                    case "xblkcn":
                        names.Add("extra");
                        names.Add("black");
                        names.Add("condensed");
                        break;

                    case "xblkit":
                        names.Add("extra");
                        names.Add("black");
                        names.Add("italic");
                        break;

                    default:
                        names.Add(element);
                        break;
                }
            }

            return string.Join(" ", names.OrderBy(x => x));
        }

#endif
        #region IComparable Members

        public int CompareTo(object obj)
        {
            FontName that = (FontName)obj;

            return this.CompareTo(that);
        }

        #endregion

        #region IComparable<FontName> Members

        public int CompareTo(FontName other)
        {
            return this.FullName.CompareTo(other.FullName);
        }

        #endregion

        #region IEquatable<FontName> Members

        public bool Equals(FontName other)
        {
            return this.FullName == other.FullName;
        }

        #endregion

        public bool HasEquivalentData(Common.Support.IXMLWritable other)
        {
            FontName otherFontName = (FontName)other;
            return this.face == otherFontName.face && this.style == otherFontName.style;
        }

        public XElement ToXML()
        {
            return new XElement(
                "FontName",
                new XAttribute("Face", this.face),
                new XAttribute("Style", this.style)
            );
        }

        public override string ToString() => this.FullName;

        public static FontName FromXML(XElement xml)
        {
            return new FontName(xml);
        }

        private FontName(XElement xml)
        {
            this.face = xml.Attribute("Face").Value;
            FontStyle.TryParse(xml.Attribute("Style").Value, out this.style);
        }

        private string face;
        private FontStyle style;
    }
}
