//	Copyright � 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Aider.Enumerations;
using Epsitec.Aider.Entities;

using Epsitec.Common.Support;
using Epsitec.Common.Types;

using Epsitec.Cresus.Bricks;
using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Controllers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Aider.Controllers.ActionControllers
{
	[ControllerSubType (64)]
	public sealed class ActionAiderPersonWarningViewController64ProcessDeparture : ActionAiderPersonWarningViewControllerInteractive
	{
		public override FormattedText GetTitle()
		{
			return Resources.FormattedText ("Conserver la personne");
		}

		public override ActionExecutor GetExecutor()
		{
			return ActionExecutor.Create (this.Execute);
		}

		private void Execute()
		{
			this.ClearWarningAndRefreshCaches ();
			this.ClearWarningAndRefreshCachesForAll (this.Entity.WarningType);
		}
	}
}
