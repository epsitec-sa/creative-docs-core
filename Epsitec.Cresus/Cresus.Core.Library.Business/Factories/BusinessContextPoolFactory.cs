//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Data;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Factories
{
	public sealed class BusinessContextPoolFactory : ICoreDataComponentFactory
	{
		#region ICoreDataComponentFactory Members

		public bool CanCreate(CoreData data)
		{
			return true;
		}

		public CoreDataComponent Create(CoreData data)
		{
			return new BusinessContextPool (data);
		}

		public System.Type GetComponentType()
		{
			return typeof (BusinessContextPool);
		}

		#endregion
	}
}
