//	Copyright � 2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;
using Epsitec.Common.Support.EntityEngine;

using Epsitec.Cresus.Core.Business;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Entities
{
	public partial class PriceDiscountEntity : ICopyableEntity<PriceDiscountEntity>
	{
		public bool HasDiscountRate
		{
			get
			{
				return this.DiscountRate.GetValueOrDefault () != 0;
			}
		}

		public bool HasValue
		{
			get
			{
				return this.Value.GetValueOrDefault () != 0;
			}
		}


		public override FormattedText GetSummary()
		{
			return this.GetCompactSummary ();
		}

		public override FormattedText GetCompactSummary()
		{
			if (this.DiscountRate.HasValue)
			{
				return TextFormatter.FormatText (this.Text, "~,", this.DiscountRate*100, "%");
			}
			else if (this.Value.HasValue)
			{
				return TextFormatter.FormatText (this.Text, "~,", this.Value);
			}
			else
			{
				return TextFormatter.FormatText (this.Text);
			}
		}

		public override EntityStatus GetEntityStatus()
		{
			return EntityStatus.Valid;
		}

		#region ICloneable<PriceDiscountEntity> Members

		void ICopyableEntity<PriceDiscountEntity>.CopyTo(IBusinessContext businessContext, PriceDiscountEntity copy)
		{
			copy.Text           = this.Text;
			copy.DiscountRate   = this.DiscountRate;
			copy.Value          = this.Value;
			copy.DiscountPolicy = this.DiscountPolicy;
			copy.RoundingMode   = this.RoundingMode;
		}

		#endregion
	}
}
