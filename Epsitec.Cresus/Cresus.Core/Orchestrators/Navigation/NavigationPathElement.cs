﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Orchestrators.Navigation
{
	public abstract class NavigationPathElement
	{
		protected internal NavigationPathElement ()
		{
		}

		public virtual bool Navigate(NavigationOrchestrator navigator)
		{
			return false;
		}
	}
}
