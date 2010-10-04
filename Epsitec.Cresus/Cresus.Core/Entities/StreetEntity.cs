//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;
using Epsitec.Common.Support.EntityEngine;

using Epsitec.Cresus.Core.Helpers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Entities
{
	public partial class StreetEntity
	{
		public override EntityStatus GetEntityStatus()
		{
			var s1 = this.StreetName.GetEntityStatus ();
			var s2 = EntityStatusHelper.TreatAsOptional (this.Complement.GetEntityStatus ());

			return EntityStatusHelper.CombineStatus (StatusHelperCardinality.All, s1, s2);
		}
	}
}
