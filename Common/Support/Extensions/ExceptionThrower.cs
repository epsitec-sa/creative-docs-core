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

namespace Epsitec.Common.Support.Extensions
{
    /// <summary>
    /// The <c>ExceptionThrower</c> class contains extension methods that can be used to check
    /// variables and throw <see cref="Exception"/> if some condition is not met.
    /// </summary>
    public static class ExceptionThrower
    {
        /// <summary>
        /// Checks that <paramref name="element"/> is not null.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="element"/>.</typeparam>
        /// <param name="element">The element to ensure that it is not null.</param>
        /// <param name="elementName">The name of <paramref name="element"/>.</param>
        /// <exception cref="System.ArgumentNullException">If <paramref name="element"/> is null.</exception>
        public static void ThrowIfNull<T>(this T element, string elementName)
            where T : class
        {
            if (element == null)
            {
                throw new System.ArgumentNullException(
                    elementName,
                    string.Format("Element of type {0} is null", typeof(T).FullName)
                );
            }
        }

        /// <summary>
        /// Checks that <paramref name="element"/> is not null.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="element"/>.</typeparam>
        /// <param name="element">The element to ensure that it is not null.</param>
        /// <param name="elementName">The name of <paramref name="element"/>.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentNullException">If <paramref name="element"/> is null.</exception>
        public static void ThrowIfNull<T>(this T element, string elementName, string message)
            where T : class
        {
            if (element == null)
            {
                throw new System.ArgumentNullException(elementName, message);
            }
        }

        /// <summary>
        /// Checks that <paramref name="element"/> has a value.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="element"/>.</typeparam>
        /// <param name="element">The element to ensure that it has a value.</param>
        /// <param name="elementName">The name of <paramref name="element"/>.</param>
        /// <exception cref="System.ArgumentException">If <paramref name="element"/> has no value.</exception>
        public static void ThrowIfWithoutValue<T>(this T? element, string elementName)
            where T : struct
        {
            if (!element.HasValue)
            {
                throw new System.ArgumentException("Argument has no value", elementName);
            }
        }

        /// <summary>
        /// Checks that <paramref name="element"/> is not null and not empty.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="element"/>.</typeparam>
        /// <param name="element">The element to ensure that it is not null and not empty.</param>
        /// <param name="elementName">The name of <paramref name="element"/>.</param>
        /// <exception cref="System.ArgumentException">If <paramref name="element"/> is null or empty.</exception>
        public static void ThrowIfNullOrEmpty(this string element, string elementName)
        {
            if (string.IsNullOrEmpty(element))
            {
                throw new System.ArgumentException("Argument is null or empty", elementName);
            }
        }

        /// <summary>
        /// Checks that <paramref name="element"/> satisfies <paramref name="condition"/>.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="element"/>.</typeparam>
        /// <param name="element">The element on which to ensure the condition.</param>
        /// <param name="condition">The condition to check on <paramref name="element"/>.</param>
        /// <param name="message">The message that will be put in the <see cref="System.ArgumentException"/>.</param>
        /// <exception cref="System.ArgumentNullException">If <paramref name="condition"/> is null.</exception>
        /// <exception cref="System.ArgumentException">If <paramref name="condition"/> is not met by <paramref name="element"/>.</exception>
        public static void ThrowIf<T>(this T element, System.Predicate<T> condition, string message)
        {
            condition.ThrowIfNull("condition");

            if (condition(element))
            {
                throw new System.ArgumentException(message);
            }
        }

        public static void ThrowIfAnyNull<T>(this IEnumerable<T> collection, string elementName)
            where T : class
        {
            if (collection.Any(x => x == null))
            {
                throw new System.ArgumentNullException(
                    elementName,
                    "Collection contains a null item"
                );
            }
        }

        public static void WhenTrueThrow<T>(this bool condition, params string[] args)
            where T : System.Exception, new()
        {
            if (condition)
            {
                throw System.Activator.CreateInstance(typeof(T), args) as T;
            }
        }

        public static void WhenFalseThrow<T>(this bool condition, params string[] args)
            where T : System.Exception, new()
        {
            if (!condition)
            {
                throw System.Activator.CreateInstance(typeof(T), args) as T;
            }
        }
    }
}
