﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.Server.BusinessLogic
{
	public static class GroupsLogic
	{
		public static string GetFullName(DataAccessor accessor, Guid guid)
		{
			//	Retourne le nom complet d'un groupe, du genre:
			//	"Immobilisations > Bâtiments > Usines > Etrangères"
			var list = new List<string> ();

			while (!guid.IsEmpty)
			{
				var obj = accessor.GetObject (BaseType.Groups, guid);
				if (obj == null)
				{
					break;
				}

				list.Insert (0, ObjectCalculator.GetObjectPropertyString (obj, null, ObjectField.Nom));
				guid = ObjectCalculator.GetObjectPropertyGuid (obj, null, ObjectField.Parent);
			}

			if (list.Count > 1)
			{
				list.RemoveAt (0);  // supprime le premier nom "Groupes"
			}

			//-return string.Join (" ˃ ", list);  // 02C3
			//-return string.Join (" → ", list);  // 2192
			return string.Join ("  ►  ", list);  // 25BA
		}
	}
}
