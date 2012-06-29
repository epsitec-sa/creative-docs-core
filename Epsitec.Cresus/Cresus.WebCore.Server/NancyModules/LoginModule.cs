﻿using Epsitec.Cresus.WebCore.Server.Core;

using Epsitec.Cresus.WebCore.Server.NancyHosting;

using Nancy;

using Nancy.Extensions;

using System.Collections.Generic;


namespace Epsitec.Cresus.WebCore.Server.NancyModules
{
	
	
	/// <summary>
	/// Called from the login page to check if the user can access the application
	/// </summary>
	public class LoginModule : AbstractCoreModule
	{


		public LoginModule(CoreServer coreServer)
			: base (coreServer, "/log")
		{
			Post["/in"] = p => this.Login ();
			Post["/out"] = p => this.Logout ();
		}


		public Response Login()
		{
			bool loggedIn = this.CheckCredentials (this.Request.Form);

			this.Session[LoginModule.LoggedInName] = loggedIn;

			if (loggedIn)
			{
				return CoreResponse.AsSuccess ();
			}
			else
			{
				var dic = new Dictionary<string, object> ()
				{
					{ "username" , "Incorrect username" },
				};
				
				return CoreResponse.AsError (dic);
			}
		}


		public Response Logout()
		{
			this.Session[LoginModule.LoggedInName] = false;

			var response = "logout";

			Dumper.Instance.Dump (response);

			return response;
		}


		private bool CheckCredentials(dynamic form)
		{
			string username = form.username;
			string password = form.password;

			return this.CoreServer.AuthenticationManager.CheckCredentials (username, password);
		}


		public static void CheckIsLoggedIn(NancyModule module)
		{
			module.Before.AddItemToEndOfPipeline (nc => LoginModule.RequiresAuthentication (nc));
		}


		private static Response RequiresAuthentication(NancyContext context)
		{
			var session = context.Request.Session;

			var loggedIn = session[LoginModule.LoggedInName];

			if (loggedIn != null && loggedIn.GetType () == typeof (bool) && (bool) loggedIn)
			{
				// He is logged in, we don't want to do anything
				return null;
			}
			else
			{
				// Not logged in, break to usual path and redirect the user to the main page where he
				// can log in.
				return context.GetRedirect ("~/");
			}
		}


		public static readonly string LoggedInName = "LoggedIn";


	}


}
