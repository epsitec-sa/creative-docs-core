//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Business.Finance.PriceCalculators;

using System.Collections.Generic;

namespace Epsitec.Cresus.Core.TableDesigner
{
	public class DesignerDimension
	{
		public DesignerDimension(DesignerDimension dimension)
		{
			this.HasDecimal   = dimension.HasDecimal;
			this.Code         = dimension.Code;
			this.Name         = dimension.Name;
			this.Description  = dimension.Description;
			this.RoundingMode = dimension.RoundingMode;

			this.points = new List<string> ();

			foreach (var point in dimension.Points)
			{
				this.points.Add (point);
			}
		}

		public DesignerDimension(AbstractDimension dimension)
		{
			this.HasDecimal = dimension is NumericDimension;
			this.Code       = dimension.Code;

			if (this.HasDecimal)
			{
				var n = dimension as NumericDimension;
				this.RoundingMode = n.RoundingMode;
			}

			this.points = new List<string> ();

			foreach (var value in dimension.Values)
			{
				this.points.Add (value);
			}
		}

		public DesignerDimension(AbstractArticleParameterDefinitionEntity articleParameter)
		{
			this.Code        = articleParameter.Code;
			this.Name        = articleParameter.Name;
			this.Description = articleParameter.Description;

			this.points = new List<string> ();

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
							this.points.Add (value);
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
						this.points.Add (value);
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

		public FormattedText Name
		{
			get;
			set;
		}

		public FormattedText Description
		{
			get;
			set;
		}

		public FormattedText NiceDescription
		{
			get
			{
				var desc = TextFormatter.FormatText ("(~", this.Description, "~)");
				return TextFormatter.FormatText (this.Name, desc);
			}
		}

		public RoundingMode RoundingMode
		{
			get;
			set;
		}

		public List<string> Points
		{
			get
			{
				return this.points;
			}
		}

		public List<decimal> DecimalPoints
		{
			get
			{
				if (this.HasDecimal)
				{
					var list = new List<decimal> ();

					foreach (var point in this.points)
					{
						list.Add (decimal.Parse (point));
					}

					return list;
				}
				else
				{
					return null;
				}
			}
		}


		public int Sort(int index)
		{
			//	Si la dimension est de type 'decimal', trie les points dans l'ordre croissant.
			//	Retourne le nouvel index d'un point.
			if (this.HasDecimal)
			{
				string value = this.points[index];

				this.points.Sort ((a, b) => DesignerDimension.Compare (a, b));

				return this.points.IndexOf (value);
			}
			else
			{
				return index;
			}
		}

		public void CleanUp()
		{
			//	Supprime les points doublons.
			int i = 0;
			while (i < this.points.Count)
			{
				if ((this.GetTotalPoint (this.points[i])) > 1)
				{
					this.points.RemoveAt (i);
				}
				else
				{
					i++;
				}
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

		private int GetTotalPoint(string point)
		{
			int count = 0;

			foreach (var p in points)
			{
				if (point == p)
				{
					count++;
				}
			}

			return count;
		}

	
		private readonly List<string>			points;
	}
}
