//	Copyright © 2003, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Statut : en chantier/PA

using Epsitec.Common.Support;

namespace Epsitec.Cresus.DataLayer
{
	/// <summary>
	/// La classe DataAttributes représente des attributs qui peuvent exister
	/// en diverses variantes (localisation).
	/// </summary>
	public class DataAttributes
	{
		public DataAttributes()
		{
		}
		
		public string[]							Names
		{
			get
			{
				if ((this.attributes == null) || (this.attributes.Count == 0))
				{
					return new string[0];
				}
				
				string[] names = new string[this.attributes.Count];
				
				this.attributes.Keys.CopyTo (names, 0);
				System.Array.Sort (names);
				
				return names;
			}
		}
		
		
		public string GetAttribute(string name, ResourceLevel level)
		{
			if (this.attributes == null)
			{
				return null;
			}
			
			string find;
			
			switch (level)
			{
				case ResourceLevel.Default:		find = name; break;
				case ResourceLevel.Customised:	find = DbTools.BuildCompositeName (name, Resources.CustomisedSuffix);	break;
				case ResourceLevel.Localised:	find = DbTools.BuildCompositeName (name, Resources.LocalisedSuffix);	break;
				
				case ResourceLevel.Merged:
					
					//	Cas spécial: on veut trouver automatiquement l'attribut le meilleur dans
					//	ce contexte; commence par chercher la variante personnalisée, puis la
					//	variante localisée, pour enfin chercher la variante de base.
					
					find = DbTools.BuildCompositeName (name, Resources.CustomisedSuffix);
					if (this.attributes.Contains (find)) return this.attributes[find] as string;
					
					find = DbTools.BuildCompositeName (name, Resources.LocalisedSuffix);
					if (this.attributes.Contains (find)) return this.attributes[find] as string;
					
					find = name;
					break;
				
				default:
					throw new ResourceException ("Invalid ResourceLevel");
			}
			
			return (this.attributes.Contains (find)) ? this.attributes[find] as string : null;
		}
		
		
		public void SetAttribute(string name, string value)
		{
			this.SetAttribute (name, value, "");
		}
		
		public void SetAttribute(string name, string value, ResourceLevel level)
		{
			switch (level)
			{
				case ResourceLevel.Default:		this.SetAttribute (name, value, "");							break;
				case ResourceLevel.Customised:	this.SetAttribute (name, value, Resources.CustomisedSuffix);	break;
				case ResourceLevel.Localised:	this.SetAttribute (name, value, Resources.LocalisedSuffix);		break;
				
				default:
					throw new System.ArgumentException ("Unsupported ResourceLevel");
			}
		}
		
		public void SetAttribute(string name, string value, string localisation_suffix)
		{
			if (this.attributes == null)
			{
				this.attributes = new System.Collections.Hashtable ();
			}
			
			name = DbTools.BuildCompositeName (name, localisation_suffix);
			
			this.attributes[name] = value;
		}
		
		
		
		protected System.Collections.Hashtable	attributes;
	}
}
