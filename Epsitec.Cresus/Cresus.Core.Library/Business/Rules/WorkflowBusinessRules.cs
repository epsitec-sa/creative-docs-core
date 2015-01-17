//	Copyright � 2010-2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Cresus.Core.Data;
using Epsitec.Cresus.Core.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Business.Rules
{
	[BusinessRule]
	internal class WorkflowBusinessRules : GenericBusinessRule<WorkflowEntity>
	{
		public override void ApplySetupRule(WorkflowEntity workflow)
		{
			workflow.Code = (string) ItemCodeGenerator.NewCode ();
		}
	}
}