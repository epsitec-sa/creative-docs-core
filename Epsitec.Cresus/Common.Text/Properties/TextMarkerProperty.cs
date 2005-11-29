//	Copyright � 2005, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Text.Properties
{
	/// <summary>
	/// La classe TextMarkerProperty permet de r�gler les d�tails relatifs �
	/// la mise en �vidence (au marquer "Stabylo") du texte.
	/// </summary>
	public class TextMarkerProperty : AbstractXlineProperty
	{
		public TextMarkerProperty()
		{
		}
		
		public TextMarkerProperty(double position, SizeUnits position_units, double thickness, SizeUnits thickness_units, string line_class, string line_style) : base (position, position_units, thickness, thickness_units, line_class, line_style)
		{
		}
		
		
		public override WellKnownType			WellKnownType
		{
			get
			{
				return WellKnownType.TextMarker;
			}
		}
		
		public override Property EmptyClone()
		{
			return new TextMarkerProperty ();
		}
	}
}
