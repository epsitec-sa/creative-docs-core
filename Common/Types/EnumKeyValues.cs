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


using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Common.Types
{
    /// <summary>
    /// The <c>EnumKeyValues</c> class is used to store an <c>enum</c> key
    /// and associated texts which represent its value (it can also be used
    /// with a <see cref="Druid"/> in place of an <c>enum</c>).
    /// See the generic/ type <c>EnumKeyValues{T}</c> for a concrete implementation.
    /// </summary>
    public abstract class EnumKeyValues : ITextFormatter
    {
        protected EnumKeyValues() { }

        /// <summary>
        /// Gets the values which describe this <c>enum</c> value.
        /// </summary>
        public abstract FormattedText[] Values { get; }

        /// <summary>
        /// Gets the enum value description (if any) for this value.
        /// </summary>
        public abstract EnumValue EnumValue { get; }

        public static EnumKeyValues<T> Create<T>(T key, EnumValue item, params string[] values)
        {
            return new EnumKeyValues<T>(key, item, values);
        }

        public static EnumKeyValues<T> Create<T>(
            T key,
            EnumValue item,
            params FormattedText[] values
        )
        {
            return new EnumKeyValues<T>(key, item, values);
        }

        public static IEnumerable<EnumKeyValues<T>> FromEnum<T>()
            where T : struct
        {
            var typeObject = TypeRosetta.GetTypeObject(typeof(T));
            var enumType = typeObject as EnumType;

            if (enumType == null)
            {
                yield break;
            }

            foreach (
                var item in enumType.Values.Where(x => !x.IsHidden) /*.OrderBy (x => x.Rank) */
            )
            {
                var enumKey = (T)(object)item.Value;
                var caption = TextFormatter.GetCurrentCultureCaption(item.Caption);
                var labels =
                    caption == null ? EmptyArray<string>.Instance : caption.Labels.ToArray();

                if (labels.Length == 0)
                {
                    var red = Epsitec.Common.Drawing.Color.FromName("Red");
                    labels = new string[]
                    {
                        FormattedText
                            .FromSimpleText(enumKey.ToString())
                            .ApplyFontColor(red)
                            .ToString()
                    };
                }

                yield return EnumKeyValues.Create<T>(enumKey, item, labels);
            }
        }

        public static IEnumerable<EnumKeyValues<Druid>> FromEntityIds(IEnumerable<Druid> entityIds)
        {
            foreach (var entityId in entityIds)
            {
                var info = EntityInfo.GetStructuredType(entityId);
                var caption = info == null ? null : info.Caption;

                caption = TextFormatter.GetCurrentCultureCaption(caption);

                System.Diagnostics.Debug.Assert(caption != null);

                var label = new FormattedText(caption.DefaultLabel ?? caption.Description ?? "");
                var name = FormattedText.FromSimpleText(caption.Name);

                yield return EnumKeyValues.Create(caption.Id, null, name, label);
            }
        }

        public static EnumKeyValues GetEnumKeyValue(object value)
        {
            return Helper.Resolve(value);
        }

        public static EnumKeyValues GetEnumKeyValue<T>(T value)
            where T : struct
        {
            return EnumKeyValues.FromEnum<T>().FirstOrDefault(x => x.Key.Equals(value));
        }

        #region ITextFormatter Members

        public FormattedText ToFormattedText(
            System.Globalization.CultureInfo culture,
            TextFormatterDetailLevel detailLevel
        )
        {
            var value = this.EnumValue;

            if ((value != null) && (value.Caption != null))
            {
                return value.Caption.DefaultLabelOrName;
            }

            return FormattedText.Join(FormattedText.FromSimpleText(" - "), this.Values);
        }

        #endregion

        #region Helper Class

        private abstract class Helper
        {
            protected abstract EnumKeyValues GetEnumKeyValues(object value);

            public static EnumKeyValues Resolve(object value)
            {
                var type = typeof(Helper<>).MakeGenericType(value.GetType());
                var helper = System.Activator.CreateInstance(type) as Helper;

                return helper.GetEnumKeyValues(value);
            }
        }

        #endregion

        #region Generic Helper Class

        private class Helper<T> : Helper
            where T : struct
        {
            protected override EnumKeyValues GetEnumKeyValues(object value)
            {
                return EnumKeyValues.GetEnumKeyValue<T>((T)value);
            }
        }

        #endregion
    }
}
