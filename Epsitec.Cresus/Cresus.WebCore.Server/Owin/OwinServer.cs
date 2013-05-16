﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Owin.Hosting;
using Epsitec.Cresus.WebCore.Server.Owin.Hubs;

namespace Epsitec.Cresus.WebCore.Server.Owin
{
	class OwinServer : IDisposable
	{

		public void Dispose()
		{
			if (this.owin!=null)
			{
				this.owin.Dispose ();
			}
		}

		public OwinServer()
		{

			this.owin = WebApplication.Start<Startup> ("http://localhost:9002/");
			
			Console.WriteLine ("Owin Server running at http://localhost:9002/");
			
            this.hubClient = NotificationClient.Instance;
		}

		private IDisposable owin;
        private NotificationClient hubClient;
	}
}
