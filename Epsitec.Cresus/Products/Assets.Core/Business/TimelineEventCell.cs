//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Assets.Core.Business
{
	public class TimelineEventCell
	{
		public TimelineEventCell(Date date)
		{
			this.Date = date;
		}


		public Date Date
		{
			get;
			set;
		}
	}
}
