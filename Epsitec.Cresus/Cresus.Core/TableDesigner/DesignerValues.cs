//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Business.Finance.PriceCalculators;
using Epsitec.Cresus.Core.Widgets;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.TableDesigner
{
	public class DesignerValues
	{
		public DesignerValues()
		{
			this.values = new Dictionary<string, decimal> ();
		}


		public void Clear()
		{
			this.values.Clear ();
		}


		public decimal? GetValue(int[] indexes)
		{
			string key = DesignerValues.GetKey (indexes);

			if (this.values.ContainsKey (key))
			{
				return this.values[key];
			}

			return null;
		}

		public void SetValue(int[] indexes, decimal? value)
		{
			string key = DesignerValues.GetKey (indexes);

			if (value == null)
			{
				if (this.values.ContainsKey (key))
				{
					this.values.Remove (key);
				}
			}
			else
			{
				this.values[key] = value.Value;
			}
		}


		private static string GetKey(int[] indexes)
		{
			string[] list = new string[indexes.Length];

			for (int i = 0; i < list.Length; i++)
			{
				list[i] = indexes[i].ToString (System.Globalization.CultureInfo.InvariantCulture);
			}

			return string.Join (".", list);
		}


		private readonly Dictionary<string, decimal>			values;
	}
}
