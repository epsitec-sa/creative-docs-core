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
using System.IO;
using System.Linq;

namespace Epsitec.Common.OpenType
{
    public delegate void FontIdentityCallback(FontIdentity fid);

    /// <summary>
    /// The <c>FontCollection</c> class manages the collection of available
    /// fonts.
    /// </summary>
    public sealed class FontCollection : IEnumerable<FontIdentity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontCollection"/> class.
        /// </summary>
        public FontCollection()
        {
            this.fontDict = new Dictionary<string, FontIdentity>();
            this.fallbackFontIdentity = null;
        }

        /// <summary>
        /// Gets the <see cref="FontIdentity"/> with the specified name.
        /// </summary>
        /// <value>The <see cref="FontIdentity"/> or <c>null</c> if it does
        /// not exist in the collection.</value>
        public FontIdentity this[string name]
        {
            get
            {
                if (this.fontDict.ContainsKey(name))
                {
                    return this.fontDict[name];
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the <see cref="FontIdentity"/> with the specified name.
        /// </summary>
        /// <value>The <see cref="FontIdentity"/> or <c>null</c> if it does
        /// not exist in the collection.</value>
        public FontIdentity this[FontName name]
        {
            get { return this[name.FullName]; }
        }

        /// <summary>
        /// Gets the default font collection object.
        /// </summary>
        /// <value>The default font collection object.</value>
        public static FontCollection Default
        {
            get
            {
                FontCollection.defaultCollection ??= new FontCollection();
                return FontCollection.defaultCollection;
            }
        }

        /// <summary>
        /// Gets or sets the filter used when listing the system fonts.
        /// </summary>
        public static System.Predicate<string> FontListFilter
        {
            get { return FontCollection.fontListFilter; }
            set { FontCollection.fontListFilter = value; }
        }

        public void Initialize(IEnumerable<FontIdentity> fontIdentities)
        {
            foreach (FontIdentity fontIdentity in fontIdentities)
            {
                this.fontDict[fontIdentity.Name] = fontIdentity;
                this.fallbackFontIdentity ??= fontIdentity;
            }
        }

        /// <summary>
        /// Refreshes the cache.
        /// </summary>
        /// <returns><c>true</c> if the contents of the cache changed; otherwise, <c>false</c>.</returns>
        public bool RefreshCache()
        {
            return this.RefreshCache(null);
        }

        /// <summary>
        /// Refreshes the cache.
        /// </summary>
        /// <param name="callback">A callback called on every saved font identity.</param>
        /// <returns><c>true</c> if the contents of the cache changed; otherwise, <c>false</c>.</returns>
        public bool RefreshCache(FontIdentityCallback callback)
        {
            /*
            lock (this.localExclusion)
            {
                this.LockedLoadFromCache();

                if (this.LockedInitialize(callback))
                {
                    return this.LockedSaveToCache(callback);
                }
                else
                {
                    return false;
                }
            }
            */
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Creates the font object based on the font face and font style.
        /// </summary>
        /// <param name="face">The font face.</param>
        /// <param name="style">The font style.</param>
        /// <returns>The font object or <c>null</c> if no font can be found.</returns>
        public Font CreateFont(string face, string style)
        {
            FontName fontName = new FontName(face, style);
            FontIdentity fid = this[fontName];

            var fallbacks = fontName.EnumerateWithFallbackStyles();
            foreach (FontName fallback in fallbacks)
            {
                fid ??= this[fallback];
            }
            fid ??= this.fallbackFontIdentity;
            if (fid == null)
            {
                throw new NoMatchingFontException($"No matching font found for {fontName} (fallbacks: {string.Join(", ", fallbacks.Select(f => f.ToString()))}");
            }
            return this.CreateFont(fid);
        }

        /// <summary>
        /// Creates the font object based on the full font name.
        /// </summary>
        /// <param name="font">The full font name.</param>
        /// <returns>The font object or <c>null</c> if no font can be found.</returns>
        public Font CreateFont(string font)
        {
            return this.CreateFont(font, "");
        }

        /// <summary>
        /// Creates the font object based on the font identity.
        /// </summary>
        /// <param name="font">The font identity.</param>
        /// <returns>The font object or <c>null</c> if no font can be found.</returns>
        public Font CreateFont(FontIdentity fid)
        {
            try
            {
                return new Font(fid);
            }
            catch (FileNotFoundException)
            {
                throw new FontFileNotFoundException();
            }
        }

        /// <summary>
        /// Registers the font as a dynamic font.
        /// </summary>
        /// <param name="data">The font data.</param>
        /// <returns>the font identity of the newly registered font; otherwise, <c>null</c>.</returns>
        public FontIdentity RegisterDynamicFont(byte[] data)
        {
            /*
            FontIdentity fid = FontIdentity.CreateDynamicFont(data);

            int nameTOffset = fid.FontData["name"].Offset;
            int nameTLength = fid.FontData["name"].Length;

            fid.DefineTableName(new TableName(data, nameTOffset), nameTLength);

            if (this.fullDict.ContainsKey(fid.FullName))
            {
                return null;
            }

            this.Add(fid);
            this.RefreshFullList();

            return fid;
            */
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the style hash, which is a simplified version of the style
        /// name.
        /// </summary>
        /// <param name="style">The raw style name.</param>
        /// <returns>The hashed style name.</returns>
        public static string GetStyleHash(string style)
        {
            //	Le "hash" d'un style de fonte correspond à une forme simplifiée
            //	et triée des éléments constituant un nom de style. On évite des
            //	problèmes de comparaison liés à des permutations, etc.

            //	En plus, le nom de style peut contenir des éléments "*Xyz" où "x"
            //	peut être "+", "-" ou "!" pour ajouter, supprimer ou inverser un
            //	style tel que "Bold" ou "Italic".

            if (string.IsNullOrEmpty(style))
            {
                return style;
            }

            string[] parts = style.Split(' ');

            int bold = 0;
            int italic = 0;

            List<string> list = new List<string>();

            foreach (string part in parts)
            {
                if (part.Length > 0)
                {
                    switch (part)
                    {
                        case "Regular":
                            break;
                        case "Normal":
                            break;
                        case "Roman":
                            break;

                        case "Bold":
                            bold = 1;
                            break;
                        case "+Bold":
                            bold += 1;
                            break;
                        case "-Bold":
                            bold -= 1;
                            break;

                        case "Italic":
                            italic = 1;
                            break;
                        case "+Italic":
                            italic += 1;
                            break;
                        case "-Italic":
                            italic -= 1;
                            break;

                        case "!Bold":
                            bold = (bold > 0) ? bold - 1 : bold + 1;
                            break;
                        case "!Italic":
                            italic = (italic > 0) ? italic - 1 : italic + 1;
                            break;

                        default:
                            if (list.Contains(part) == false)
                            {
                                list.Add(part);
                            }
                            break;
                    }
                }
            }

            if (bold > 0)
            {
                list.Add("Bold");
            }
            if (italic > 0)
            {
                list.Add("Italic");
            }

            list.Sort();

            return string.Join(" ", list.ToArray());
        }

        public string DebugGetFullFontList()
        {
            /*
            List<string> lines = new List<string>();

            foreach (FontIdentity id in this)
            {
                TableName name = id.OpenTypeTableName;
                TableName.NameEncoding[] encodings = name.GetAvailableNameEncodings();

                lines.Add("-----------------------------");
                lines.AddRange(
                    string.Format(
                            "{0,-32}Style={1} Weight={2} Count={3}\n"
                                + "                                Invariant={4} / {5}\n"
                                + "                                Invariant Preferred={6} / {7}\n"
                                + "                                Invariant Hash={8}\n"
                                + "                                Locale={9} / {10}\n"
                                + "                                Locale Preferred={11} / {12}\n"
                                + "                                UniqueFontId={13}, entries={14}",
                            id.FullName,
                            id.FontStyle,
                            id.FontWeight,
                            id.FontStyleCount,
                            id.InvariantSimpleFaceName,
                            id.InvariantSimpleStyleName,
                            id.InvariantPreferredFaceName,
                            id.InvariantPreferredStyleName,
                            id.InvariantStyleHash,
                            id.LocaleSimpleFaceName,
                            id.LocaleSimpleStyleName,
                            id.LocalePreferredFaceName,
                            id.LocalePreferredStyleName,
                            id.UniqueFontId,
                            encodings.Length
                        )
                        .Split('\n')
                );

                lines.Add(
                    string.Concat(
                        ">>> ",
                        id.FullName,
                        "/",
                        id.InvariantFaceName,
                        "+",
                        id.InvariantStyleName,
                        "/",
                        id.LocaleFaceName,
                        "+",
                        id.LocaleStyleName
                    )
                );

                for (int i = 0; i < encodings.Length; i++)
                {
                    string unicode =
                        encodings[i].Platform == PlatformId.Microsoft
                            ? name.GetUnicodeName(
                                encodings[i].Language,
                                encodings[i].Name,
                                encodings[i].Platform
                            )
                            : null;
                    string latin =
                        encodings[i].Platform == PlatformId.Macintosh
                            ? name.GetLatinName(
                                encodings[i].Language,
                                encodings[i].Name,
                                encodings[i].Platform
                            )
                            : null;

                    lines.Add(
                        string.Format(
                            "{0,-4} {1,-24} {2,-10} : {3}",
                            encodings[i].Language,
                            encodings[i].Name,
                            encodings[i].Platform,
                            (unicode ?? latin ?? "").Split('\n')[0]
                        )
                    );
                }
            }

            return string.Join("\n", lines.ToArray());
            */
            throw new System.NotImplementedException();
        }

        #region IEnumerable Members

        public IEnumerator<FontIdentity> GetEnumerator()
        {
            return this.fontDict.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.fontDict.Values.GetEnumerator();
        }

        #endregion

        //public event FontIdentityCallback FontIdentityDefined;

        private Dictionary<string, FontIdentity> fontDict;
        private FontIdentity fallbackFontIdentity;
        private static FontCollection defaultCollection;
        private static System.Predicate<string> fontListFilter;
    }
}
