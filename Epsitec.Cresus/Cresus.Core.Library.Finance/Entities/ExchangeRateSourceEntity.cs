//	Copyright © 2011-2012, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Library;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Entities
{
	public partial class ExchangeRateSourceEntity
	{
		public override IEnumerable<FormattedText> GetFormattedEntityKeywords()
		{
			if (string.IsNullOrWhiteSpace (this.Originator))
			{
				yield return "—";
			}
			else
			{
				yield return TextFormatter.FormatText (this.Originator);
			}

			foreach (var value in EnumKeyValues.GetEnumKeyValue (this.Type).Values)
			{
				yield return value;
			}
		}
		
		public override FormattedText GetCompactSummary()
		{
			return TextFormatter.FormatText (this.Name, this.Type, TextFormatter.Command.IfEmpty);
		}
		
		public override FormattedText GetSummary()
		{
			return TextFormatter.FormatText (this.Name, this.Type, TextFormatter.Command.IfEmpty, "\n", this.Description, "\n", this.Originator);
		}
	}
}
