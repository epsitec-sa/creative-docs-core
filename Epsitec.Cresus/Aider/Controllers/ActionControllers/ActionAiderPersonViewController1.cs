using Epsitec.Aider.Entities;

using Epsitec.Common.Types;

using Epsitec.Cresus.Bricks;

using Epsitec.Cresus.Core.Business;

using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Controllers.ActionControllers;

using Epsitec.Cresus.Core.Entities;

using System.Collections.Generic;


namespace Epsitec.Aider.Controllers.ActionControllers
{


	[ControllerSubType (1)]
	public sealed class ActionAiderPersonViewController1 : ActionViewController<AiderPersonEntity>
	{


		public override FormattedText GetTitle()
		{
			return "G�rer les m�nages";
		}


		public override ActionExecutor GetExecutor()
		{
			return ActionExecutor.Create<AiderHouseholdEntity, AiderHouseholdEntity> (this.Execute);
		}


		private void Execute(AiderHouseholdEntity household1, AiderHouseholdEntity household2)
		{
			var person = this.Entity;

			if (household1.IsNull ())
			{
				throw new BusinessRuleException (person, "Le m�nage principal est obligatoire.");
			}

			if (household1 == household2)
			{
				throw new BusinessRuleException (person, "Le m�nage secondaire doit �tre diff�rent du m�nage principal.");
			}

			person.SetHousehold1 (this.BusinessContext, household1);
			person.SetHousehold2 (this.BusinessContext, household2);
		}


		protected override void GetForm(ActionBrick<AiderPersonEntity, SimpleBrick<AiderPersonEntity>> form)
		{
			form
				.Title ("G�rer les m�nages")
				.Field<AiderHouseholdEntity> ()
					.Title ("M�nage principal")
					.InitialValue (x => x.Household1)
				.End ()
				.Field<AiderHouseholdEntity> ()
					.Title ("M�nage secondaire")
					.InitialValue (x => x.Household2)
				.End ()
			.End ();
		}
	}


}
