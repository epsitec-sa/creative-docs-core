﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Assets.Server.BusinessLogic
{
	public struct HistoryDetails
	{
		public HistoryDetails(System.DateTime inputDate, decimal inputAmount,
			System.DateTime baseDate, decimal baseAmount, decimal initialAmout)
		{
			this.InputDate     = inputDate;
			this.InputAmount   = inputAmount;
			this.BaseDate      = baseDate;
			this.BaseAmount    = baseAmount;
			this.InitialAmount = initialAmout;
		}

		public bool								IsEmpty
		{
			get
			{
				return this.InputAmount   == 0.0m
					&& this.BaseAmount    == 0.0m
					&& this.InitialAmount == 0.0m;
			}
		}

		public static HistoryDetails Empty = new HistoryDetails (System.DateTime.MinValue, 0.0m, System.DateTime.MinValue, 0.0m, 0.0m);

		public readonly System.DateTime			InputDate;
		public readonly decimal					InputAmount;
		public readonly System.DateTime			BaseDate;
		public readonly decimal					BaseAmount;
		public readonly decimal					InitialAmount;
	}
}
