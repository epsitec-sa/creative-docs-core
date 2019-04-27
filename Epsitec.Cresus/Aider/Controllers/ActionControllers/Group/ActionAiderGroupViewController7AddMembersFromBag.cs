//	Copyright © 2012-2014, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Marc BETTEX, Maintainer: Pierre ARNAUD

using Epsitec.Aider.Controllers.SpecialFieldControllers;
using Epsitec.Aider.Entities;

using Epsitec.Common.Types;

using Epsitec.Cresus.Bricks;

using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Controllers.ActionControllers;
using Epsitec.Cresus.Core.Entities;

using System.Linq;
using Epsitec.Aider.Override;
using Epsitec.Cresus.Core.Library;
using System.Collections.Generic;
using Epsitec.Common.Support.EntityEngine;

namespace Epsitec.Aider.Controllers.ActionControllers
{
	[ControllerSubType (7)]
	public sealed class ActionAiderGroupViewController7AddMembersFromBag : ActionViewController<AiderGroupEntity>
	{
		public override FormattedText GetTitle()
		{
			return "Reprendre depuis le panier";
		}

		public override ActionExecutor GetExecutor()
		{
			return ActionExecutor.Create<Date?, FormattedText> (this.Execute);
		}

		private void Execute(Date? startDate, FormattedText comment)
		{
			if (!this.Entity.CanBeEditedByCurrentUser ())
			{
				var message = "Vous n'avez pas le droit d'éditer ce groupe";

				Logic.BusinessRuleException (message);
				
				return;
			}

			var contacts = new List<AiderContactEntity> ();
			var bagEntities = EntityBag.GetEntities (this.BusinessContext.DataContext);

			foreach (var entity in bagEntities)
			{
				EntityBag.Process (entity as AiderContactEntity, x => contacts.Add (x));
			}

			if (contacts.Count > 0)
			{
				this.Entity.ImportContactsMembers (this.BusinessContext, contacts, startDate, comment);
			}
		}

		protected override void GetForm(ActionBrick<AiderGroupEntity, SimpleBrick<AiderGroupEntity>> form)
		{
			form
				.Title (this.GetTitle ())
				.Field<Date> ()
					.Title ("Date d'entrée dans le groupe")
					.InitialValue (Date.Today)
				.End ()
				.Field<FormattedText> ()
					.Title ("Commentaire")
					.Multiline ()
				.End ()
			.End ();
		}
	}
}
