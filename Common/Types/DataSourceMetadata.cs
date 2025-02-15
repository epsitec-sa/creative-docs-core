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


using Epsitec.Common.Types;
using System.Collections.Generic;

[assembly: DependencyClass(typeof(DataSourceMetadata))]

namespace Epsitec.Common.Types
{
    /// <summary>
    /// The <c>DataSourceMetadata</c> class describes the fields found in the data source
    /// attached to a panel, for instance.
    /// </summary>
    public class DataSourceMetadata : DependencyObject, IStructuredType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataSourceMetadata"/> class.
        /// </summary>
        public DataSourceMetadata() { }

        /// <summary>
        /// Gets the structured type field collection; every field has a one-to-one
        /// mapping with the fields found in the panel's <see cref="DataSource"/>.
        /// </summary>
        /// <value>The structured type field collection.</value>
        public IList<StructuredTypeField> Fields
        {
            get
            {
                if (this.fields == null)
                {
                    this.fields = new List<StructuredTypeField>();
                }

                return this.fields;
            }
        }

        /// <summary>
        /// Gets or sets the default data type for the data source.
        /// </summary>
        /// <value>The default data type.</value>
        public IStructuredType DefaultDataType
        {
            get
            {
                StructuredTypeField field = this.GetField("*");

                if (field == null)
                {
                    return null;
                }
                else
                {
                    return field.Type as IStructuredType;
                }
            }
            set
            {
                INamedType namedType = value as INamedType;

                if ((value != null) && (namedType == null))
                {
                    throw new System.ArgumentException(
                        "Specified default data type does not implement INamedType"
                    );
                }

                if (this.fields != null)
                {
                    for (int i = 0; i < this.fields.Count; i++)
                    {
                        if (this.fields[i].Id == "*")
                        {
                            this.fields[i] = new StructuredTypeField("*", namedType);
                            return;
                        }
                    }
                }

                this.Fields.Add(new StructuredTypeField("*", namedType));
            }
        }

        #region IStructuredType Members

        /// <summary>
        /// Gets the field descriptor for the specified field identifier.
        /// </summary>
        /// <param name="fieldId">The field identifier.</param>
        /// <returns>
        /// The matching field descriptor; otherwise, <c>null</c>.
        /// </returns>
        public StructuredTypeField GetField(string fieldId)
        {
            if (this.fields != null)
            {
                foreach (StructuredTypeField field in this.fields)
                {
                    if (field.Id == fieldId)
                    {
                        return field;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a collection of field identifiers.
        /// </summary>
        /// <returns>A collection of field identifiers.</returns>
        public IEnumerable<string> GetFieldIds()
        {
            if (this.fields != null)
            {
                foreach (StructuredTypeField field in this.fields)
                {
                    yield return field.Id;
                }
            }
        }

        StructuredTypeClass IStructuredType.GetClass()
        {
            return StructuredTypeClass.None;
        }

        #endregion

        private static object GetFieldsValue(DependencyObject obj)
        {
            DataSourceMetadata metadata = (DataSourceMetadata)obj;
            return metadata.Fields;
        }

        public static DependencyProperty FieldsProperty = DependencyProperty.RegisterReadOnly(
            "Fields",
            typeof(List<StructuredTypeField>),
            typeof(DataSourceMetadata),
            new DependencyPropertyMetadata(
                DataSourceMetadata.GetFieldsValue
            ).MakeReadOnlySerializable()
        );

        private List<StructuredTypeField> fields;
    }
}
