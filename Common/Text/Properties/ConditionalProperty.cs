//	Copyright © 2005-2008, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland
//	Responsable: Pierre ARNAUD
using System.Xml.Linq;

namespace Epsitec.Common.Text.Properties
{
    /// <summary>
    /// La classe ConditionalProperty définit la condition qui doit être remplie
    /// pour que le texte soit considéré comme visible.
    /// </summary>
    public class ConditionalProperty
        : Property,
            Common.Support.IXMLSerializable<ConditionalProperty>
    {
        public ConditionalProperty() { }

        public ConditionalProperty(string condition)
            : this(condition, true) { }

        public ConditionalProperty(string condition, bool showIfTrue)
        {
            this.condition = condition;
            this.showIfTrue = showIfTrue;
        }

        public override WellKnownType WellKnownType
        {
            get { return WellKnownType.Conditional; }
        }

        public override PropertyType PropertyType
        {
            get { return PropertyType.CoreSetting; }
        }

        public override CombinationMode CombinationMode
        {
            get { return CombinationMode.Accumulate; }
        }

        public string Condition
        {
            get { return this.condition; }
        }

        public bool ShowIfTrue
        {
            get { return this.showIfTrue; }
        }

        public override Property EmptyClone()
        {
            return new ConditionalProperty();
        }

        public override void SerializeToText(System.Text.StringBuilder buffer)
        {
            SerializerSupport.Join(
                buffer,
                /**/SerializerSupport.SerializeString(this.condition),
                /**/SerializerSupport.SerializeBoolean(this.showIfTrue)
            );
        }

        public override bool HasEquivalentData(Common.Support.IXMLWritable otherWritable)
        {
            ConditionalProperty other = (ConditionalProperty)otherWritable;
            return this.condition == other.condition && this.showIfTrue == other.showIfTrue;
        }

        public override XElement ToXML()
        {
            return new XElement(
                "ConditionalProperty",
                new XAttribute("Condition", this.condition),
                new XAttribute("ShowIfTrue", this.showIfTrue)
            );
        }

        public static ConditionalProperty FromXML(XElement xml)
        {
            return new ConditionalProperty(xml);
        }

        private ConditionalProperty(XElement xml)
        {
            this.condition = xml.Attribute("Condition").Value;
            this.showIfTrue = (bool)xml.Attribute("ShowIfTrue");
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

            string condition = SerializerSupport.DeserializeString(args[0]);
            bool showIfTrue = SerializerSupport.DeserializeBoolean(args[1]);

            this.condition = condition;
            this.showIfTrue = showIfTrue;
        }

        public override Property GetCombination(Property property)
        {
            throw new System.InvalidOperationException();
        }

        public override void UpdateContentsSignature(IO.IChecksum checksum)
        {
            checksum.UpdateValue(this.condition);
            checksum.UpdateValue(this.showIfTrue);
        }

        public override bool CompareEqualContents(object value)
        {
            return ConditionalProperty.CompareEqualContents(this, value as ConditionalProperty);
        }

        private static bool CompareEqualContents(ConditionalProperty a, ConditionalProperty b)
        {
            return a.condition == b.condition && a.showIfTrue == b.showIfTrue;
        }

        private string condition;
        private bool showIfTrue;
    }
}
