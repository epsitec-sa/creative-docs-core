//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Data
{
	/// <summary>
	/// The <c>LockOwner</c> class provides information about the owner
	/// (defined by a <see cref="ConnectionUserIdentity"/>) for a named
	/// lock.
	/// </summary>
	public sealed class LockOwner
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LockOwner"/> class.
		/// </summary>
		/// <param name="owner">The low level lock owner information.</param>
		internal LockOwner(DataLayer.Infrastructure.LockOwner owner)
		{
			this.User         = ConnectionUserIdentity.Parse (owner.ConnectionIdentity);
			this.LockName     = owner.LockName;
			this.LockTime = new DatabaseTime (owner.LockDateTime);
		}


		/// <summary>
		/// Gets the connection user identity.
		/// </summary>
		/// <value>The connection user identity.</value>
		public ConnectionUserIdentity			User
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the name of the lock.
		/// </summary>
		/// <value>The name of the lock.</value>
		public string							LockName
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the time when the lock was acquired, expressed as a database
		/// date and time.
		/// </summary>
		/// <value>The lock date.</value>
		public DatabaseTime						LockTime
		{
			get;
			set;
		}
	}
}