﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.Server.BusinessLogic
{
	public static class AssetsLogic
	{
		public static IEnumerable<UserField> GetUserFields(DataAccessor accessor)
		{
			//	Retourne les champs d'un objet d'immobilisation.
			bool mainValue = false;

			foreach (var userField in accessor.Settings.GetUserFields (BaseType.Assets))
			{
				if (!mainValue && userField.Type == FieldType.ComputedAmount)
				{
					//	Juste avant la première valeur utilisateur, on injecte la valeur comptable.
					//	Le Guid créé à la volée n'est pas utilisé !
					yield return new UserField (DataDescriptions.GetObjectFieldDescription (ObjectField.MainValue), ObjectField.MainValue, FieldType.AmortizedAmount, userField.ColumnWidth, null, null, null, 0);
					mainValue = true;
				}

				yield return userField;
			}
		}


		public static string GetSummary(DataAccessor accessor, Guid guid)
		{
			//	Retourne le nom court d'un objet, du genre:
			//	"Toyota Yaris Verso"
			var obj = accessor.GetObject (BaseType.Assets, guid);
			if (obj == null)
			{
				return null;
			}
			else
			{
				//	On prend les champs de type texte ayant un SummaryOrder.
				var list = new List<string> ();

				foreach (var field in accessor.Settings.GetUserFields (BaseType.Assets)
					.Where (x => x.Type == FieldType.String && x.SummaryOrder.HasValue)
					.OrderBy (x => x.SummaryOrder)
					.Select (x => x.Field))
				{
					var text = ObjectProperties.GetObjectPropertyString (obj, null, field);

					if (!string.IsNullOrEmpty (text))
					{
						list.Add (text);
					}
				}

				return string.Join (" ", list).Trim ();
			}
		}
	}
}
