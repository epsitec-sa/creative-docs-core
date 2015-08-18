﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Data;

namespace Epsitec.Cresus.Assets.Server.NodeGetters
{
	/// <summary>
	/// Les instructions d'extraction permettent de définir des montants calculés
	/// par CumulNodeGetter, en vue de la production de rapports.
	/// Ces montants sont inclus dans une période temporelle et peuvent concerner un
	/// ou plusieurs types d'événements.
	/// </summary>
	public struct ExtractionInstructions
	{
		public ExtractionInstructions(ExtractionAmount extractionAmount, DateRange range, bool inverted, params EventType[] filteredEventTypes)
		{
			switch (extractionAmount)
			{
				case ExtractionAmount.StateAt:
				case ExtractionAmount.UserColumn:
					System.Diagnostics.Debug.Assert (filteredEventTypes == null || filteredEventTypes.Length == 0);
					break;

				case ExtractionAmount.LastFiltered:
				case ExtractionAmount.DeltaSum:
					System.Diagnostics.Debug.Assert (filteredEventTypes != null && filteredEventTypes.Length != 0);
					break;

				default:
					throw new System.InvalidOperationException (string.Format ("Unknown ExtractionAmount {0}", extractionAmount));
			}

			this.ExtractionAmount   = extractionAmount;
			this.Range              = range;
			this.FilteredEventTypes = filteredEventTypes;
			this.Inverted           = inverted;
		}

		public static ExtractionInstructions Empty = new ExtractionInstructions (ExtractionAmount.StateAt, DateRange.Empty, false);

		public readonly ExtractionAmount		ExtractionAmount;
		public readonly DateRange				Range;
		public readonly bool					Inverted;
		public readonly EventType[]				FilteredEventTypes;
	}
}