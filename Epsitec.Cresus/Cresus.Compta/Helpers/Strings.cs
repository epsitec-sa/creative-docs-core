//	Copyright � 2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Types;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Compta.Entities;
using Epsitec.Cresus.Compta.Accessors;
using Epsitec.Cresus.Compta.Controllers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Helpers
{
	public static class Strings
	{
		public static string AddS�pMilliers(string text, string s�p)
		{
			if (!string.IsNullOrEmpty (text) && text.Length > 3)
			{
				var list = new List<string> ();

				while (text.Length != 0)
				{
					int length = System.Math.Min (text.Length, 3);
					list.Add (text.Substring (text.Length-length, length));
					text = text.Substring (0, text.Length-length);
				}

				list.Reverse ();
				return string.Join (s�p, list);
			}

			return text;
		}
	}
}
