//	Copyright � 2005-2008, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Text
{
	/// <summary>
	/// L'interface IContentsSignature donne acc�s � un m�canisme de v�rification
	/// du contenu d'un objet au moyen d'une signature (dans les faits, c'est une
	/// somme de contr�le ou CRC).
	/// </summary>
	public interface IContentsSignature
	{
		int GetContentsSignature();
	}
}
