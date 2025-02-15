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
using Epsitec.Common.Support;
using System.Collections.Generic;
using System.Xml;

namespace Epsitec.Common.FormEngine
{
    /// <summary>
    /// Description complète d'un masque de saisie.
    /// </summary>
    public sealed class FormDescription : System.IEquatable<FormDescription>
    {
        public FormDescription()
            : this(Druid.Empty, Druid.Empty) { }

        public FormDescription(Druid entityId, Druid deltaBaseFormId)
        {
            this.entityId = entityId;
            this.deltaBaseFormId = deltaBaseFormId;
            this.defaultSize = new Size(double.NaN, double.NaN);
            this.fields = new List<FieldDescription>();
        }

        public FormDescription(FormDescription model)
            : this(model.EntityId, model.DeltaBaseFormId)
        {
            this.fields.AddRange(model.Fields);
        }

        public Druid EntityId
        {
            //	Druid de l'entité de base du masque de saisie, utilisé pour un masque normal/delta.
            get { return this.entityId; }
        }

        public Druid DeltaBaseFormId
        {
            //	Druid du masque de base à fusionner. Il s'agit normalement d'un masque de base complet.
            //	Mais il peut s'agit d'un masque delta. Dans ce cas, il faudra remonter jusqu'au masque
            //	de base (IsDelta = false) pour construire le masque final.
            //	Par exemple:
            //	- Form1 est un masque de base
            //	- Form2 est un masque delta basé sur Form1
            //	- Form3 est un masque delta basé sur Form2
            //	Si on cherche à construire Form3, il faut utiliser FormEngine.Arrange.Merge ainsi:
            //	final = Merge(Merge(Form1, Form2), Form3);
            get { return this.deltaBaseFormId; }
        }

        public Size DefaultSize
        {
            //	Taille par défaut du masque. La largeur et/ou la hauteur peuvent être définis avec NaN
            //	s'il n'y a pas de dimension par défaut.
            get { return this.defaultSize; }
            set { this.defaultSize = value; }
        }

        public List<FieldDescription> Fields
        {
            //	Liste des champs, séparateurs, etc.
            //	S'il s'agit d'un masque de base, il s'agit directement de la liste des éléments du masque.
            //	S'il s'agit d'un masque delta, il s'agit de la liste des modifications à appliquer au
            //	masque de base défini dans DeltaBaseFormId.
            get { return this.fields; }
        }

        public bool IsDelta
        {
            //	Indique s'il s'agit d'un masque delta (Delta form).
            get { return this.isForceDelta || this.deltaBaseFormId.IsValid; }
        }

        public bool IsForceDelta
        {
            //	Force ce masque a apparaître comme un masque delta. C'est utile pour un masque dans un
            //	module de patch, qui est vu comme un masque normal, mais qui agit bien comme un delta.
            //	Cette information n'est pas sérialisée.
            get { return this.isForceDelta; }
            set { this.isForceDelta = value; }
        }

        #region IEquatable<FormDescription> Members

        public bool Equals(FormDescription other)
        {
            return FormDescription.Equals(this, other);
        }

        #endregion

        public override bool Equals(object obj)
        {
            return (obj is FormDescription) && (FormDescription.Equals(this, (FormDescription)obj));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool Equals(FormDescription a, FormDescription b)
        {
            //	Retourne true si les deux objets sont égaux.
            if ((a == null) != (b == null))
            {
                return false;
            }

            if (a == null && b == null)
            {
                return true;
            }

            if (a.entityId != b.entityId)
            {
                return false;
            }

            if (a.deltaBaseFormId != b.deltaBaseFormId)
            {
                return false;
            }

            if (a.defaultSize != b.defaultSize)
            {
                return false;
            }

            if (a.fields.Count != b.fields.Count)
            {
                return false;
            }

            for (int i = 0; i < a.fields.Count; i++)
            {
                FieldDescription f1 = a.fields[i];
                FieldDescription f2 = b.fields[i];
                if (!f1.Equals(f2))
                {
                    return false;
                }
            }

            return true;
        }

        #region Serialization
        public string Serialize()
        {
            //	Sérialise le masque et retourne le résultat dans un string.
            System.Text.StringBuilder buffer = new System.Text.StringBuilder();
            System.IO.StringWriter stringWriter = new System.IO.StringWriter(buffer);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = false;
            XmlWriter writer = XmlWriter.Create(stringWriter, settings);

            this.WriteXml(writer);

            writer.Flush();
            writer.Close();
            return buffer.ToString();
        }

        public void Deserialize(string data)
        {
            //	Désérialise le masque à partir d'un string de données.
            System.IO.StringReader stringReader = new System.IO.StringReader(data);
            XmlTextReader reader = new XmlTextReader(stringReader);

            this.ReadXml(reader);

            reader.Close();
        }

        private void WriteXml(XmlWriter writer)
        {
            //	Sérialise tout le masque.
            writer.WriteStartDocument();

            writer.WriteStartElement(Xml.Form);
            writer.WriteElementString(Xml.EntityId, this.entityId.ToString());
            writer.WriteElementString(Xml.DeltaBaseFormId, this.deltaBaseFormId.ToString());
            writer.WriteElementString(Xml.DefaultSize, this.defaultSize.ToString());
            foreach (FieldDescription field in this.fields)
            {
                field.WriteXml(writer);
            }
            writer.WriteEndElement();

            writer.WriteEndDocument();
        }

        private void ReadXml(XmlReader reader)
        {
            //	Désérialise tout le masque.
            this.fields.Clear();

            reader.Read();

            //	TODO: attention, la logique de désérialisation est fausse, mais ça marche provisoirement !
            while (true)
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    string name = reader.LocalName;

                    if (name == Xml.Form)
                    {
                        reader.Read();
                    }
                    else if (name == Xml.FieldDescription)
                    {
                        FieldDescription field = new FieldDescription(reader);
                        this.fields.Add(field);
                    }
                    else
                    {
                        string element = reader.ReadElementString();

                        if (name == Xml.EntityId)
                        {
                            this.entityId = Druid.Parse(element);
                        }
                        else if (name == Xml.DeltaBaseFormId)
                        {
                            this.deltaBaseFormId = Druid.Parse(element);
                        }
                        else if (name == Xml.DefaultSize)
                        {
                            this.defaultSize = Size.Parse(element);
                        }
                        else
                        {
                            throw new System.NotSupportedException(
                                string.Format(
                                    "Unexpected XML node {0} found in FieldDescription",
                                    name
                                )
                            );
                        }
                    }
                }
                else if (reader.NodeType == XmlNodeType.None)
                {
                    break;
                }
                else
                {
                    reader.Read();
                }
            }

#if false
			while (reader.ReadToFollowing(Xml.FieldDescription))
			{
				FieldDescription field = new FieldDescription(FieldDescription.FieldType.Field);
				field.ReadXml(reader);
				this.fields.Add(field);
			}

			// TODO: désérialiser this.entityId !
#endif
        }
        #endregion


        private Druid entityId;
        private Druid deltaBaseFormId;
        private Size defaultSize;
        private readonly List<FieldDescription> fields;
        private bool isForceDelta;
    }
}
