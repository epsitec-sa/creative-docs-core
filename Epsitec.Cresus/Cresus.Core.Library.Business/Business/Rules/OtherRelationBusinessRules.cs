//	Copyright � 2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support.Extensions;
using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Data;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Workflows;

namespace Epsitec.Cresus.Core.Business.Rules
{
	[BusinessRule]
	internal class OtherRelationBusinessRules : GenericBusinessRule<OtherRelationEntity>
	{
		public override void ApplyBindRule(OtherRelationEntity otherRelation)
		{
			var businessContext = Logic.Current.GetComponent<BusinessContext> ();
			businessContext.Register (otherRelation.Person);
		}
	}
}
