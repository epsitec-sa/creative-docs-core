//	Copyright � 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System;
using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Types;

namespace Epsitec.Cresus.Assets.App.Widgets
{
	public struct TimelineCell
	{
		public Date Date;
		public TimelineCellType Type;
		public bool IsSelected;
		public bool IsError;

		public bool IsInvalid
		{
			get
			{
				return this.Date.IsNull;
			}
		}

		public bool IsValid
		{
			get
			{
				return !this.Date.IsNull;
			}
		}
	}
}
