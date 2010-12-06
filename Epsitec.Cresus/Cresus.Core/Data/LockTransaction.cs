//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Data
{
	using DataInfrastructure=Epsitec.Cresus.DataLayer.Infrastructure.DataInfrastructure;
	using LowLevelLockTransaction=Epsitec.Cresus.DataLayer.Infrastructure.LockTransaction;
	using LowLevelLockOwner=Epsitec.Cresus.DataLayer.Infrastructure.LockOwner;
	using LockState=Epsitec.Cresus.DataLayer.Infrastructure.LockState;
	
	public sealed class LockTransaction : System.IDisposable
	{
		public LockTransaction(DataInfrastructure dataInfrastructure, IEnumerable<string> lockNames)
		{
			this.dataInfrastructure = dataInfrastructure;
			this.lockNames = new List<string> (lockNames);
			this.lockTransaction = this.dataInfrastructure.CreateLockTransaction (this.lockNames);
		}

		/// <summary>
		/// Gets the current state of this instance.
		/// </summary>
		public LockState						LockSate
		{
			get
			{
				if (this.isDisposed)
				{
					return LockState.Disposed;
				}
				else
				{
					return this.lockTransaction.State;
				}
			}
		}

		/// <summary>
		/// Gets the identification of the connections who owned the locks of the current instance
		/// when the last call to <see cref="LockTransaction.Poll"/> or <see cref="LockTransaction.Lock"/>
		/// was made. The data is returned as a mapping from the lock names to the connection identifications.
		/// </summary>
		/// <value>The lock owners or <c>null</c> if this information is not available.</value>
		/// <remarks>This data is only available after a call to <see cref="LockTransaction.Poll"/>
		/// or <see cref="LockTransaction.Lock"/> has been made, and the data might be outdated if
		/// the real state of the locks has changed in the database since that call.</remarks>
		public IList<LockOwner>					ForeignLockOwners
		{
			get
			{
				if (this.foreignLockOwners == null)
				{
					return null;
				}
				else
				{
					return this.foreignLockOwners.AsReadOnly ();
				}
			}
		}

		/// <summary>
		/// Tries to acquire the locks of the current instance.
		/// </summary>
		/// <returns><c>true</c> if the locks have been acquired, <c>false</c> if they don't.</returns>
		/// <exception cref="System.InvalidOperationException">If the locks have already been acquired by this instance or if this instance has been disposed.</exception>
		internal bool Acquire()
		{
			if (this.LockSate != LockState.Idle)
			{
				throw new System.InvalidOperationException ("Cannot execute this operation in the current state.");
			}

			var lockOwners = new List<LowLevelLockOwner> ();
			var result     = this.lockTransaction.Lock (lockOwners);
			
			this.foreignLockOwners = lockOwners.Select (x => new LockOwner (x)).ToList ();

			return result;
		}

		/// <summary>
		/// Checks if the locks of the current instance can be acquired by the connection of the
		/// current instance and populates the fields with the data about the lock owners and
		/// creation time
		/// </summary>
		/// <returns><c>true</c> if the locks can be acquired (or have been acquired), <c>false</c> if they cannot.</returns>
		/// <exception cref="System.InvalidOperationException">If this instance has been disposed.</exception>
		internal bool Poll()
		{
			if (this.LockSate == LockState.Disposed)
			{
				throw new System.InvalidOperationException ("Cannot execute this operation in the current state.");
			}
			
			string id = this.dataInfrastructure.ConnectionInformation.ConnectionIdentity;

			this.foreignLockOwners = this.lockTransaction
				.GetLockOwners ()
				.Where (x => x.ConnectionIdentity != id)
				.Select (x => new LockOwner (x))
				.ToList ();

			return this.foreignLockOwners.Count == 0;
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (this.lockTransaction != null)
			{
				this.lockTransaction.Dispose ();
				this.lockTransaction = null;
			}

			this.isDisposed = true;
		}

		#endregion

		private readonly DataInfrastructure		dataInfrastructure;
		private readonly List<string>			lockNames;
		private LowLevelLockTransaction			lockTransaction;
		private List<LockOwner>					foreignLockOwners;
		
		private bool							isDisposed;
	}
}
