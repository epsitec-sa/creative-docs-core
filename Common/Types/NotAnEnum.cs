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


namespace Epsitec.Common.Types
{
    /// <summary>
    /// The <c>NotAnEnum</c> enumeration is used when an <see cref="T:IEnumValue"/>
    /// needs to return a <see cref="T:System.Enum"/> value, but the underlying type
    /// does not define a C# enum value for it.
    /// </summary>
    public enum NotAnEnum
    {
        /// <summary>
        /// Use <c>NotAnEnum.Instance</c> whenever you need to pass a <see cref="T:System.Enum"/>
        /// value, but none is valid in that context.
        /// </summary>
        Instance
    }
}
