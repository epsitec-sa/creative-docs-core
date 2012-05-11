//	Copyright � 2006-2008, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// L'�num�ration <c>WidgetPaintState</c> d�termine quels effets graphiques
	/// utiliser quand un <c>Widget</c> est peint.
	/// </summary>
	[System.Flags]
	public enum WidgetPaintState : uint
	{
		None				= 0x00000000,				//	=> neutre
		
		ActiveYes			= 0x00000001,				//	=> mode ActiveState.Yes
		ActiveMaybe			= 0x00000002,				//	=> mode ActiveState.Maybe
		
		Enabled				= 0x00010000,				//	=> re�oit des �v�nements
		Focused				= 0x00020000,				//	=> re�oit les �v�nements clavier
		Entered				= 0x00040000,				//	=> contient la souris
		Selected			= 0x00080000,				//	=> s�lectionn�
		Engaged				= 0x00100000,				//	=> pression en cours
		Error				= 0x00200000,				//	=> signale une erreur
		ThreeState			= 0x00400000,				//	=> accepte 3 �tats
		UndefinedLanguage	= 0x00800000,				//	=> langue ind�finie

		InheritedFocus		= 0x01000000,				//	=> le parent est Focused
		InheritedEnter		= 0x02000000,				//	=> l'enfant est Entered

		Furtive				= 0x04000000,				//	=> widget furtif, sans bords lorsque la souris ne le survole pas
	}
}