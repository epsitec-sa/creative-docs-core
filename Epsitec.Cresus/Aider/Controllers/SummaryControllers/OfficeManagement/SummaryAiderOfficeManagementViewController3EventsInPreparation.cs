//	Copyright © 2012-2014, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Samuel LOUP, Maintainer: Samuel LOUP

using Epsitec.Common.Support;
using Epsitec.Common.Types;

using Epsitec.Aider.Controllers.ActionControllers;
using Epsitec.Aider.Controllers.EditionControllers;
using Epsitec.Aider.Entities;
using Epsitec.Aider.Override;


using Epsitec.Cresus.Core.Business.UserManagement;
using Epsitec.Cresus.Core.Bricks;
using Epsitec.Cresus.Core.Controllers.SummaryControllers;
using Epsitec.Cresus.Core.Library;
using Epsitec.Cresus.Core.Entities;

using System.Linq;
using Epsitec.Cresus.Bricks;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Aider.Controllers.SetControllers;

namespace Epsitec.Aider.Controllers.SummaryControllers
{
	[ControllerSubType (3)]
	public sealed class SummaryAiderOfficeManagementViewController3EventsInPreparation : SummaryViewController<AiderOfficeManagementEntity>
	{
		protected override void CreateBricks(BrickWall<AiderOfficeManagementEntity> wall)
		{
			wall.AddBrick (p => p.EventsInPreparation)
								.Attribute (BrickMode.DefaultToSummarySubView)
								.Attribute (BrickMode.AutoGroup)
								.Attribute (BrickMode.HideAddButton)
								.Attribute (BrickMode.HideRemoveButton)
								.EnableActionMenu<ActionAiderOfficeManagementViewController7DuplicateEvent> ()
								.EnableActionMenu<ActionAiderOfficeManagementViewController8DeleteEvent> ()
								.Template ()
									.Title ("Actes en préparations")
									.Text (x => x.GetSummary ())								
								.End ();
								
			
		}
	}
}
