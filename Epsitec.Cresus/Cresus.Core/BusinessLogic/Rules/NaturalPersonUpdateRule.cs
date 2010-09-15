//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support.Extensions;
using Epsitec.Cresus.Core.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.BusinessLogic.Rules
{
	[BusinessRule (RuleType=RuleType.Update)]
	internal class NaturalPersonUpdateRule : GenericBusinessRule<NaturalPersonEntity>
	{
		protected override void Apply(NaturalPersonEntity person)
		{
			person.Contacts.ForEach (x => x.NaturalPerson = person);
		}
	}
}
