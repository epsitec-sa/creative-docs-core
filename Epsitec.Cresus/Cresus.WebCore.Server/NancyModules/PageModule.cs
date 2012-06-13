﻿using Epsitec.Cresus.WebCore.Server.CoreServer;
using Epsitec.Cresus.WebCore.Server.NancyHosting;

using Nancy;


namespace Epsitec.Cresus.WebCore.Server.NancyModules
{


	/// <summary>
	/// Proxy to retrieve a page stored in the Views folder
	/// </summary>
	public class PageModule : AbstractCoreSessionModule
	{


		public PageModule(ServerContext serverContext)
			: base (serverContext, "/page/")
		{
			Get["/{name}"] = p => this.ExecuteWithCoreSession (cs => this.GetPageView (p));
		}


		private Response GetPageView(dynamic parameters)
		{
			string pageName = parameters.name;

			Dumper.Instance.Dump ("Data for page: " + pageName);

			return View[pageName];
		}


	}


}
