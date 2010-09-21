//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support.Extensions;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.DataLayer.Infrastructure;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core
{
	/// <summary>
	/// The <c>CoreDataConnectionManager</c> class maintains an active connection with
	/// the underlying database. Typically, it updates the connection state periodically.
	/// </summary>
	public sealed class CoreDataConnectionManager : System.IDisposable
	{
		public CoreDataConnectionManager(CoreData data)
		{
			this.data = data;
			this.dataInfrastructure = data.DataInfrastructure;
			
			this.keepAliveTimer = new Timer ()
			{
				AutoRepeat = CoreDataConnectionManager.KeepAlivePeriodInSeconds,
				Delay = CoreDataConnectionManager.KeepAlivePeriodInSeconds,
			};

			this.keepAliveTimer.TimeElapsed += this.HandleKeepAliveTimerTimeElapsed;
		}


		public bool IsActive
		{
			get
			{
				this.dataInfrastructure.RefreshConnectionInformation ();
				
				var info = this.dataInfrastructure.ConnectionInformation;
				
				return info != null && info.Status == ConnectionStatus.Open;
			}
		}

		public bool IsReady
		{
			get
			{
				return this.isReady;
			}
		}


		/// <summary>
		/// Validates the connection once the database infrastructure is ready to be
		/// used. This will start the keep alive timer which pulses with a 10 second
		/// period.
		/// </summary>
		public void Validate()
		{
			if (!this.isReady)
			{
				this.OpenConnection ();
				this.StartTimerIfNotRunning ();
				this.KeepAliveConnection ();
			}
		}

		public void ReopenConnection()
		{
			if (this.isReady)
			{
				this.CloseConnection ();
				this.OpenConnection ();
			}
		}

		private void OpenConnection()
		{
			string identity = this.GetIdentity ();
			this.dataInfrastructure.OpenConnection (identity);
			this.isReady = true;
		}

		private void CloseConnection()
		{
			this.isReady = false;
			this.dataInfrastructure.CloseConnection ();
		}


		private string GetIdentity()
		{
			var userCode    = this.GetActiveUserCode ();
			var userName    = System.Environment.UserName;
			var machineName = System.Environment.MachineName;
			var osVersion   = System.Environment.OSVersion.VersionString;
			var clrVersion  = System.Environment.Version.ToString ();
			var coreVersion = typeof (CoreData).Assembly.GetVersionString ();

			return string.Concat (userCode, ":", userName, "@", machineName, "/OS={", osVersion, "}/CLR={", clrVersion, "}/Core={", coreVersion, "}");
		}

		private string GetActiveUserCode()
		{
			var userManager = CoreProgram.Application.UserManager;
			var activeUser  = userManager.AuthenticatedUser;

			if (activeUser == null)
			{
				return "<none>";
			}
			else
			{
				return activeUser.Code;
			}
		}


		#region IDisposable Members

		public void Dispose()
		{
			this.CloseConnection ();

			if (this.keepAliveTimer.State != TimerState.Disposed)
			{
				this.keepAliveTimer.TimeElapsed -= this.HandleKeepAliveTimerTimeElapsed;
				this.keepAliveTimer.Dispose ();
			}
		}

		#endregion

		private void KeepAliveConnection()
		{
			this.dataInfrastructure.KeepConnectionAlive ();
		}

		private void StartTimerIfNotRunning()
		{
			if (this.keepAliveTimer.State == TimerState.Invalid)
			{
				this.keepAliveTimer.Start ();
			}
		}
		
		private void HandleKeepAliveTimerTimeElapsed(object sender)
		{
			this.KeepAliveConnection ();
		}


		private static readonly double KeepAlivePeriodInSeconds = 10.0;

		private readonly CoreData data;
		private readonly DataInfrastructure dataInfrastructure;
		private readonly Timer keepAliveTimer;
		
		private bool isReady;
	}
}