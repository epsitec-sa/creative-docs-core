//	Copyright � 2003-2004, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using Epsitec.Common.Support;

namespace Epsitec.Cresus.Database
{
	using Converter = Epsitec.Common.Types.Converter;
	
	/// <summary>
	/// La classe DbTools fournit quelques fonctions utilitaires qui n'ont pas
	/// de rapport direct avec SQL, mais sont utiles aux classes DbXyz.
	/// </summary>
	public class DbTools
	{
		private DbTools()
		{
		}
		
		
		public static string BuildLocalisedName(string name, Common.Support.ResourceLevel level)
		{
			//	TODO: g�rer les suffixes en fonction de la culture active, non en fonction de
			//	la culture par d�faut du gestionnaire de ressources par d�faut !
			
			switch (level)
			{
				case ResourceLevel.Default:		return name;
				case ResourceLevel.Customised:	return DbTools.BuildCompositeName (name, Resources.CustomisedSuffix);
				case ResourceLevel.Localised:	return DbTools.BuildCompositeName (name, Resources.LocalisedSuffix);
			}
			
			throw new System.ArgumentException (string.Format ("Level {0} not valid here (name='{1}').", level, name), "level");
		}
		
		public static string BuildCompositeName(params string[] list)
		{
			int num = list.Length;
			
			//	Retire de la liste toutes les cha�nes vides qui pourraient s'�tre
			//	accumul�es � la fin; �a �vite que la composition de "a" avec "" ne
			//	donne "a_" en fin de compte (le r�sultat sera "a", simplement).
			
			while ((num > 0) && (list[num-1].Length == 0))
			{
				num--;
			}
			
			return (num == 0) ? "" : string.Join ("_", list, 0, num);
		}
		
		public static DbElementCat ParseElementCategory(string text)
		{
			if ((text == null) ||
				(text.Length == 0))
			{
				return DbElementCat.Unknown;
			}
			
			int cat;
			Converter.Convert (text, out cat);
			return (DbElementCat) cat;
		}
		
		public static string ElementCategoryToString(DbElementCat cat)
		{
			if (cat == DbElementCat.Unknown)
			{
				return null;
			}
			
			return Converter.ToString ((int) cat);
		}
	}
}
