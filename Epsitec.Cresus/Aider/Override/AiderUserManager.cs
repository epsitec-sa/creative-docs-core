//	Copyright � 2012, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Aider.Entities;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Business.UserManagement;

using System.Collections.Generic;
using System.Linq;

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
	}
}
