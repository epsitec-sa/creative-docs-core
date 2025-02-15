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


namespace Epsitec.Common.Support
{
    /// <summary>
    /// The <c>IResourceProvider</c> interface provides low level access to the
    /// resource data.
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// Gets the prefix of this resource provider.
        /// </summary>
        /// <value>The prefix of this resource provider (this is <c>"file"</c> for
        /// the file-based resource provider).</value>
        string Prefix { get; }

        /// <summary>
        /// Selects the specified module by using either the module name, the
        /// module identifier or both.
        /// </summary>
        /// <param name="module">The module to select; the module information
        /// is updated on success.</param>
        /// <returns><c>true</c> if the module could be selected; otherwise,
        /// <c>false</c>.</returns>
        bool SelectModule(ref ResourceModuleId module);

        /// <summary>
        /// Validates the resource bundle identifier.
        /// </summary>
        /// <param name="id">The resource bundle identifier.</param>
        /// <returns><c>true</c> if the resource bundle identifier is valid; otherwise,
        /// <c>false</c>.</returns>
        bool ValidateId(string id);

        /// <summary>
        /// Determines whether the resource provider can locate the bundle with the
        /// specified identifier.
        /// </summary>
        /// <param name="id">The resource bundle identifier.</param>
        /// <returns><c>true</c> the resource provider can locate the specified resource
        /// bundle identifier; otherwise, <c>false</c>.</returns>
        bool Contains(string id);

        /// <summary>
        /// Gets the modules which can be accessed by this resource provider.
        /// </summary>
        /// <returns>An array of modules which can be accessed by this
        /// resource provider.</returns>
        ResourceModuleId[] GetModules();

        /// <summary>
        /// Gets the resource bundle identifiers matching several criteria.
        /// </summary>
        /// <param name="nameFilter">The name filter (may include wildcards).</param>
        /// <param name="typeFilter">The type filter (may include wildcards).</param>
        /// <param name="level">The localization level (all, default, localized, customized).</param>
        /// <param name="culture">The culture for the locale.</param>
        /// <returns>The resource bundle identifiers matching the criteria.</returns>
        string[] GetIds(
            string nameFilter,
            string typeFilter,
            ResourceLevel level,
            System.Globalization.CultureInfo culture
        );

        byte[] GetData(string id, ResourceLevel level, System.Globalization.CultureInfo culture);
        bool SetData(
            string id,
            ResourceLevel level,
            System.Globalization.CultureInfo culture,
            byte[] data,
            ResourceSetMode mode
        );
        bool Remove(string id, ResourceLevel level, System.Globalization.CultureInfo culture);
    }
}
