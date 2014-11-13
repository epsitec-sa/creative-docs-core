﻿//	Copyright © 2012-2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Samuel LOUP, Maintainer: Samuel LOUP

using Epsitec.Aider.Controllers.SpecialFieldControllers;
using Epsitec.Aider.Entities;
using Epsitec.Aider.Enumerations;
using Epsitec.Common.Types;
using Epsitec.Cresus.Bricks;
using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Controllers.ActionControllers;

namespace Epsitec.Aider.Controllers.ActionControllers
{
	[ControllerSubType (4)]
	public sealed class ActionAiderGroupDefViewController4CreateFunction : ActionViewController<AiderGroupDefEntity>
	{
		public override FormattedText GetTitle()
		{
			if (this.Entity.Level == 0)
			{
				return "Créer une fonction générale";
			}
			else
			{
				return "Ajouter une fonction";
			}
			
		}

		public override ActionExecutor GetExecutor()
		{
			return ActionExecutor.Create<string> (this.Execute);
		}


		protected override void GetForm(ActionBrick<AiderGroupDefEntity, SimpleBrick<AiderGroupDefEntity>> form)
		{
			form
				.Title (this.GetTitle ())
				.Field<string> ()
					.Title ("Nom de la fonction")
				.End ()
			.End ();
		}

		private void Execute(string name)
		{
			if (string.IsNullOrWhiteSpace (name))
			{
				throw new BusinessRuleException ("Le nom ne peut pas être vide");
			}

			var groupDef = AiderGroupDefEntity.CreateFunctionSubGroup (
																			this.BusinessContext,
																			this.Entity,
																			name);
			if (this.Entity.Level > 0)
			{
				AiderGroupDefEntity.InstantiateFunctionSubGroup (this.BusinessContext, this.Entity, groupDef);
			}
			
			
			
		}
	}
}
