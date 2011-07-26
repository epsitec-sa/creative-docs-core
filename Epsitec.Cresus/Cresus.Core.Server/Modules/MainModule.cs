﻿using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Support.Extensions;
using Epsitec.Cresus.Core.Entities;
using Nancy;

namespace Epsitec.Cresus.Core.Server
{
	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = parameters =>
			{
				return "Hello World";
			};

			Get["/login"] = parameters =>
			{
				GetCoreSession ();

				return "logged in";
			};

			Get["/persons.json"] = parameters =>
			{
				var coreSession = GetCoreSession();

				var context = coreSession.GetBusinessContext ();

				var customers = from x in context.GetAllEntities<CustomerEntity> ()
								select x;

				var obj = new List<object> ();

				customers.ForEach (c => obj.Add (new
				{
					firstName = c.IdA,
					lastName = c.IdB
				}));

				obj.Add (new
				{
					LastUpdate = DebugSession.Session["last"] as string
				});

				DebugSession.Session["last"] = System.DateTime.Now.ToString ();

				var res = Response.AsJson (obj);
				res.Headers["Access-Control-Allow-Origin"] = "*";

				return res;

			};
		}

		private CoreSession GetCoreSession()
		{
			var sessionId = DebugSession.Session["CoreSession"] as string;
			var session = CoreServer.Instance.GetCoreSession (sessionId);

			if (session == null)
			{
				var server = CoreServer.Instance;
				session = server.CreateSession ();
				PanelBuilder.CoreSession = session;

				DebugSession.Session["CoreSession"] = session.Id;
			}

			return session;
		}
	}
}
