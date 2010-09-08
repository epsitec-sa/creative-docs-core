//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;

using Epsitec.Cresus.DataLayer;
using Epsitec.Cresus.DataLayer.Infrastructure;
using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core
{
	public sealed class CoreDataLocker : System.IDisposable
	{
		public CoreDataLocker(DataInfrastructure dataInfrastructure)
		{
			this.dataInfrastructure = dataInfrastructure;
		}

		
		public bool IsReady
		{
			get
			{
				return this.isReady;
			}
		}

		
		public void Validate()
		{
			if (!this.isReady)
			{
				this.isReady = true;
			}
		}

		public CoreDataLockTransaction RequestLock(params string[] lockNames)
		{
			return this.RequestLock ((IEnumerable<string>) lockNames);
		}

		public CoreDataLockTransaction RequestLock(IEnumerable<string> lockNames)
		{
			var lockTransaction = new CoreDataLockTransaction (lockNames);

			if (lockTransaction.Acquire (this.dataInfrastructure))
			{
				return lockTransaction;
			}
			else
			{
				return null;
			}
		}

		public bool AreAllLocksAvailable(IEnumerable<string> lockNames)
		{
			return this.dataInfrastructure.AreAllLocksAvailable (lockNames);
		}


		public static string GetLockName(DataContext context, AbstractEntity entity)
		{
			var key = context.GetNormalizedEntityKey (entity);

			if (key.HasValue)
			{
				return key.Value.ToString ();
			}
			else
			{
				return null;
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
		
		private readonly DataInfrastructure dataInfrastructure;

		private bool isReady;
	}
}