//	Copyright � 2012-2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Aider.Entities;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Business.UserManagement;

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Data;

namespace Epsitec.Aider.Override
{
	public sealed class AiderUserManager : UserManager
	{
		public AiderUserManager(CoreData data, bool enableReload)
			: base (data, enableReload)
		{
		}


		public new AiderUserEntity				AuthenticatedUser
		{
			get
			{
				return base.AuthenticatedUser as AiderUserEntity;
			}
		}

		public new AiderUserSession				ActiveSession
		{
			get
			{
				return base.ActiveSession as AiderUserSession;
			}
		}

		public static new AiderUserManager		Current
		{
			get
			{
				return UserManager.Current as AiderUserManager;
			}
		}


		public override void NotifySusccessfulLogin(SoftwareUserEntity user)
		{
			this.UpdateUser (user.Code, u => u.LastLoginDate = System.DateTime.UtcNow);

			var notif = Epsitec.Cresus.Core.Library.NotificationManager.GetCurrentNotificationManager ();

			notif.Notify (user.LoginName, new Cresus.Core.Library.NotificationMessage ()
			{
				Title = "Information AIDER",
				Body = "Bienvenue!"

			}, true);

			notif.NotifyAll (new Cresus.Core.Library.NotificationMessage ()
			{
				Title = "Information AIDER",
				Body = user.DisplayName + " viens de ce connecter."

			}, false);
			
			base.NotifySusccessfulLogin (user);
		}

		public override void NotifyChangePassword(SoftwareUserEntity user)
		{
			var notif = Epsitec.Cresus.Core.Library.NotificationManager.GetCurrentNotificationManager ();

			notif.WarnUser (user.LoginName, new Cresus.Core.Library.NotificationMessage ()
			{
				Title = "Attention AIDER",
				Body = "Merci de changer rapidement votre mot de passe! (cliquez sur ce message pour acc�der � votre profil)",
				Dataset = Res.CommandIds.Base.ShowAiderUser,
				EntityKey = this.BusinessContext.DataContext.GetNormalizedEntityKey(user).Value

			}, true);

			base.NotifyChangePassword (user);
		}

		protected override void ChangeAuthenticatedUser(SoftwareUserEntity user)
		{
			var aiderUser = user as AiderUserEntity;

			if (aiderUser != null)
			{
				AiderActivityLogger.Current.RecordAccess (aiderUser);
				
				var notif = Epsitec.Cresus.Core.Library.NotificationManager.GetCurrentNotificationManager ();

				var now  = System.DateTime.UtcNow;
				var last = aiderUser.LastActivityDate;

				if ((last == null) ||
					((now-last.Value).Seconds > 10))
				{
					this.UpdateUser (user.Code, u => u.LastActivityDate = now);
				}
			}

			base.ChangeAuthenticatedUser (user);
		}


		private void UpdateUser(string userCode, System.Action<AiderUserEntity> update)
		{
#if false
			var notif = Epsitec.Cresus.Core.Library.NotificationManager.GetCurrentNotificationManager ();
			
			notif.NotifyAll (new Cresus.Core.Library.NotificationMessage ()
			{
				Title = "Welcome back",
				Body = "C'est un plaisir de vous revoir parmi nous..."
			});
#endif

			using (var context = new BusinessContext (this.Host, false))
			{
				context.GlobalLock = GlobalLocks.UserManagement;
				
				var example = new AiderUserEntity ()
				{
					Code = userCode,
					Disabled = false,
				};

				var repo = context.GetRepository<AiderUserEntity> ();
				var user = repo.GetByExample (example).FirstOrDefault ();

				if (user != null)
				{
					update (user);
					context.SaveChanges (LockingPolicy.ReleaseLock);
				}
			}
		}
	}
}
