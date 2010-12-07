//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Cresus.Core.Data;
using Epsitec.Cresus.Core.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Business.Rules
{
	[BusinessRule]
	internal class FreeTextValueArticleParameterDefinitionBusinessRules : GenericBusinessRule<FreeTextValueArticleParameterDefinitionEntity>
	{
		public override void ApplySetupRule(FreeTextValueArticleParameterDefinitionEntity entity)
		{
			entity.Code = (string) ItemCodeGenerator.NewCode ();
		}
	}
}
