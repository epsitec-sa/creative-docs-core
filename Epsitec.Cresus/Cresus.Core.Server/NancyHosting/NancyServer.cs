﻿//	Copyright © 2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Jonas Schmid, Maintainer: -


using Epsitec.Cresus.Core.Server.CoreServer;

using Nancy;

using Nancy.Bootstrapper;

using Nancy.IO;

using Nancy.Cookies;

using Nancy.Extensions;

using System;

using System.Collections.Generic;

using System.Globalization;

using System.Linq;

using System.Net;



namespace Epsitec.Cresus.Core.Server.NancyHosting
{


	internal sealed class NancyServer : IDisposable
	{


		// This class has been largely inspired by the source code found here :
		// https://github.com/NancyFx/Nancy/blob/09a5c3f8f79d5986a04973b0371e52f4f596a600/src/Nancy.Hosting.Self/NancyHost.cs


		public NancyServer(ServerContext serverContext, Uri uri, int nbThreads)
		{
			this.baseUri = uri;
			this.nbThreads = nbThreads;
			this.httpServer = new HttpServer (uri);

			var bootStrapper = new CoreServerBootstrapper (serverContext);

			bootStrapper.Initialise ();
			this.engine = bootStrapper.GetEngine ();
		}


		public void Start()
		{
			this.httpServer.Start (r => this.ProcessRequest (r), this.nbThreads);
		}


		public void Stop()
		{
			this.httpServer.Stop ();
		}


		public void Dispose()
		{
			this.httpServer.Dispose ();
		}


		private void ProcessRequest(HttpListenerContext requestContext)
		{
			HttpListenerRequest httpRequest = requestContext.Request;

			using (HttpListenerResponse httpResponse = requestContext.Response)
			{
				var nancyRequest = ConvertHttpRequestToNancyRequest (httpRequest);

				using (var nancyContext = this.engine.HandleRequest (nancyRequest))
				{
					var nancyResponse = nancyContext.Response;

					NancyServer.ConvertNancyResponseToHttpResponse (nancyResponse, httpResponse);
				}
			}
		}


		private static Uri GetUrlAndPathComponents(Uri uri)
		{
			return new Uri (uri.GetComponents (UriComponents.SchemeAndServer | UriComponents.Path, UriFormat.Unescaped));
		}


		private Request ConvertHttpRequestToNancyRequest(HttpListenerRequest request)
		{
			if (this.baseUri == null)
			{
				throw new InvalidOperationException (String.Format ("Unable to locate base URI for request: {0}", request.Url));
			}

			var expectedRequestLength = NancyServer.GetExpectedRequestLength (request.Headers.ToDictionary ());

			var relativeUrl = NancyServer.GetUrlAndPathComponents (this.baseUri).MakeRelativeUri (GetUrlAndPathComponents (request.Url));

			var nancyUrl = new Url
			{
				Scheme = request.Url.Scheme,
				HostName = request.Url.Host,
				Port = request.Url.IsDefaultPort ? null : (int?) request.Url.Port,
				BasePath = this.baseUri.AbsolutePath.TrimEnd ('/'),
				Path = string.Concat ("/", relativeUrl),
				Query = request.Url.Query,
				Fragment = request.Url.Fragment,
			};

			return new Request
			(
				request.HttpMethod,
				nancyUrl,
				RequestStream.FromStream (request.InputStream, expectedRequestLength, true),
				request.Headers.ToDictionary ()
			);
		}


		private static long GetExpectedRequestLength(IDictionary<string, IEnumerable<string>> incomingHeaders)
		{
			if (incomingHeaders == null)
			{
				return 0;
			}

			if (!incomingHeaders.ContainsKey ("Content-Length"))
			{
				return 0;
			}

			var headerValue = incomingHeaders["Content-Length"].SingleOrDefault ();

			if (headerValue == null)
			{
				return 0;
			}

			long contentLength;
			if (!long.TryParse (headerValue, NumberStyles.Any, CultureInfo.InvariantCulture, out contentLength))
			{
				return 0;
			}

			return contentLength;
		}


		private static void ConvertNancyResponseToHttpResponse(Response nancyResponse, HttpListenerResponse response)
		{
			foreach (var header in nancyResponse.Headers)
			{
				response.AddHeader (header.Key, header.Value);
			}

			foreach (var nancyCookie in nancyResponse.Cookies)
			{
				response.Cookies.Add (NancyServer.ConvertCookie (nancyCookie));
			}

			response.ContentType = nancyResponse.ContentType;
			response.StatusCode = (int) nancyResponse.StatusCode;

			using (var output = response.OutputStream)
			{
				nancyResponse.Contents.Invoke (output);
			}
		}


		private static Cookie ConvertCookie(INancyCookie nancyCookie)
		{
			var cookie = new Cookie (nancyCookie.EncodedName, nancyCookie.EncodedValue, nancyCookie.Path, nancyCookie.Domain);

			if (nancyCookie.Expires.HasValue)
			{
				cookie.Expires = nancyCookie.Expires.Value;
			}

			return cookie;
		}


		private readonly HttpServer httpServer;


		private readonly INancyEngine engine;


		private readonly int nbThreads;


		private readonly Uri baseUri;


	}


}