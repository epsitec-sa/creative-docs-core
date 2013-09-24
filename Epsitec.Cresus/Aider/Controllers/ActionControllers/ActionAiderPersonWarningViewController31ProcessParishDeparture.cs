//	Copyright � 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Samuel LOUP, Maintainer: Samuel LOUP

using Epsitec.Aider.Entities;

using Epsitec.Common.Support;
using Epsitec.Common.Types;

using Epsitec.Cresus.Bricks;

using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Controllers.ActionControllers;
using Epsitec.Cresus.Core.Entities;

using System.Collections.Generic;
using System.Linq;
using Epsitec.Aider.Enumerations;
using Epsitec.Cresus.DataLayer.Loader;

namespace Epsitec.Aider.Controllers.ActionControllers
{
	[ControllerSubType (31)]
	public sealed class ActionAiderPersonWarningViewController31ProcessParishDeparture : ActionAiderPersonWarningViewControllerPassive
	{
		public override FormattedText GetTitle()
		{
			return Resources.FormattedText ("Marquer comme lu (tout le m�nage)");
		}

		protected override void Execute()
		{
			this.ClearWarningAndRefreshCaches ();

			var warning = this.Entity;
			var person  = warning.Person;
			var members = person.GetAllHouseholdMembers ();
			var warnings = members.SelectMany (x => x.Warnings.Where (w => w.WarningType == WarningType.ParishDeparture)).ToList ();

			warnings.ForEach (x => this.ClearWarningAndRefreshCaches (x));
		}
	}
}
