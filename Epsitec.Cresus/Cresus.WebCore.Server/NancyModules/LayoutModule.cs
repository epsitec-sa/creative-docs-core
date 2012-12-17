﻿using Epsitec.Cresus.Core.Business;

using Epsitec.Cresus.WebCore.Server.Core;
using Epsitec.Cresus.WebCore.Server.Core.IO;
using Epsitec.Cresus.WebCore.Server.Layout;
using Epsitec.Cresus.WebCore.Server.NancyHosting;

using Nancy;


namespace Epsitec.Cresus.WebCore.Server.NancyModules
{


	public class LayoutModule : AbstractAuthenticatedModule
	{


		/// <summary>
		/// Call the <see cref="PanelBuilder"/> to create the ExtJS interface.
		/// It is called to show the summary of the edition interface.
		/// </summary>
		public LayoutModule(CoreServer coreServer)
			: base (coreServer, "/layout")
		{
			Get["/{viewMode}/{viewId}/{entityId}"] = p => this.Execute (b => this.GetLayout (b, p));
		}


		private Response GetLayout(BusinessContext businessContext, dynamic parameters)
		{
			var caches = this.CoreServer.Caches;
			var databaseManager = this.CoreServer.DatabaseManager;

			var entity = EntityIO.ResolveEntity (businessContext, (string) parameters.entityId);
			var viewMode = DataIO.ParseViewMode ((string) parameters.viewMode);
			var viewId = DataIO.ParseViewId ((string) parameters.viewId);

			var entityColumn = Carpenter.BuildEntityColumn (businessContext, caches, databaseManager, entity, viewMode, viewId);
			var content = entityColumn.ToDictionary (caches);

			return CoreResponse.Success (content);
		}
		

	}


}
