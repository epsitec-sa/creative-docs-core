using System.Collections.Generic;

using Epsitec.Common.Drawing;
using Epsitec.Common.Support;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;

namespace Epsitec.Common.FormEngine
{
	/// <summary>
	/// Proc�dures de manipulation et d'arrangement de listes et d'arbres.
	/// </summary>
	public class Arrange
	{
		static public List<FieldDescription> Merge(List<FieldDescription> reference, List<FieldDescription> patch)
		{
			//	Retourne la liste fusionn�e.
			return reference;  // TODO:
		}


		static public List<FieldDescription> Develop(List<FieldDescription> fields)
		{
			//	Retourne une liste d�velopp�e qui ne contient plus de noeuds.
			List<FieldDescription> dst = new List<FieldDescription>();

			Arrange.Develop(dst, fields);
			
			return dst;
		}

		static private void Develop(List<FieldDescription> dst, List<FieldDescription> fields)
		{
			foreach (FieldDescription field in fields)
			{
				if (field.Type == FieldDescription.FieldType.Node)
				{
					Arrange.Develop(dst, field.NodeDescription);
				}
				else if (field.Type == FieldDescription.FieldType.InsertionPoint)
				{
				}
				else if (field.Type == FieldDescription.FieldType.Hide)
				{
				}
				else
				{
					dst.Add(field);
				}
			}
		}
	}
}
