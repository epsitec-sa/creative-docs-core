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
	public class DesignerDimension
	{
		public DesignerDimension(AbstractDimension dimension)
		{
			this.HasDecimal = dimension is NumericDimension;

			this.Code = dimension.Code;

			if (this.HasDecimal)
			{
				var n = dimension as NumericDimension;
				this.RoundingMode = n.RoundingMode;
			}

			this.values = new List<string> ();

			foreach (var value in dimension.Values)
			{
				this.values.Add (value);
			}
		}

		public DesignerDimension(AbstractArticleParameterDefinitionEntity articleParameter)
		{
			this.Code = articleParameter.Code;

			this.values = new List<string> ();

			if (articleParameter is NumericValueArticleParameterDefinitionEntity)
			{
				this.HasDecimal = true;

				var numericArticleParameter = articleParameter as NumericValueArticleParameterDefinitionEntity;

				string[] values = (numericArticleParameter.PreferredValues ?? "").Split (new string[] { AbstractArticleParameterDefinitionEntity.Separator }, System.StringSplitOptions.None);

				foreach (var value in values)
				{
					if (!string.IsNullOrWhiteSpace (value))
					{
						decimal d;

						if (decimal.TryParse (value, out d))
						{
							this.values.Add (value);
						}
					}
				}
			}

			if (articleParameter is EnumValueArticleParameterDefinitionEntity)
			{
				this.HasDecimal = false;

				var enumArticleParameter = articleParameter as EnumValueArticleParameterDefinitionEntity;

				string[] values = (enumArticleParameter.Values ?? "").Split (new string[] { AbstractArticleParameterDefinitionEntity.Separator }, System.StringSplitOptions.None);
				var list = new List<string> ();

				foreach (var value in values)
				{
					if (!string.IsNullOrEmpty (value))
					{
						this.values.Add (value);
					}
				}
			}
		}


		public bool HasDecimal
		{
			get;
			set;
		}

		public string Code
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public RoundingMode RoundingMode
		{
			get;
			set;
		}

		public List<string> Values
		{
			get
			{
				return this.values;
			}
		}


		public int Sort(int index)
		{
			if (this.HasDecimal)
			{
				string value = this.values[index];

				this.values.Sort ((a, b) => DesignerDimension.Compare (a, b));

				return this.values.IndexOf (value);
			}
			else
			{
				return index;
			}
		}



		private static int Compare(string a, string b)
		{
			decimal da = DesignerDimension.GetDecimal (a as string);
			decimal db = DesignerDimension.GetDecimal (b as string);

			return da.CompareTo (db);
		}

		private static decimal GetDecimal(string value)
		{
			decimal d;

			if (decimal.TryParse (value, out d))
			{
				return d;
			}

			return 0;
		}

	
		private readonly List<string>			values;
	}
}
