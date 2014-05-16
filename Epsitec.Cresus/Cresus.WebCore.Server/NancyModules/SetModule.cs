﻿using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;

using Epsitec.Cresus.Core.Business;

using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Controllers.SetControllers;

using Epsitec.Cresus.Core.Data;

using Epsitec.Cresus.WebCore.Server.Core;
using Epsitec.Cresus.WebCore.Server.Core.Extraction;
using Epsitec.Cresus.WebCore.Server.Core.IO;
using Epsitec.Cresus.WebCore.Server.Layout;
using Epsitec.Cresus.WebCore.Server.NancyHosting;

using Nancy;

using System;

using System.Collections.Generic;

using System.Linq;


namespace Epsitec.Cresus.WebCore.Server.NancyModules
{


	using Database = Core.Databases.Database;
	using Epsitec.Cresus.Core.Library;


	/// <summary>
	/// This modules is used to retrieve and manipulate the entities contained in set DataSets. Set
	/// DataSets are the data sets provided by instances of SetViewController.
	/// </summary>
	public class SetModule : AbstractAuthenticatedModule
	{


		public SetModule(CoreServer coreServer)
			: base (coreServer, "/set")
		{
			// Gets the data of the entities in a set DataSet.
			// URL arguments:
			// - viewId:    The view id of the SetViewController to use, as used by the DataIO
			//              class.
			// - entityId:  The entity key of the entity on which the SetViewController will be
			//              used, in the format used by the EntityIO class.
			// - dataset:   The type of dataset to use:
			//              - display:  for the dataset that displays its elements
			//              - pick:     for the dataset that displays the elements that can be
			//                          picked to be added to the dataset.
			// GET arguments:
			// - start:   The index of the first entity to return, as an integer.
			// - limit:   The maximum number of entities to return, as an integer.
			// - columns: The id of the columns whose data to return, in the format used by the
			//            ColumnIO class.
			// - sort:    The sort clauses, in the format used by SorterIO class.
			// - filter:  The filters, in the format used by FilterIO class.
			Get["/{viewId}/{entityId}/get/{dataset}"] = p =>
				this.Execute (context => this.GetEntities (context, p));

			// Exports the data of the entities that are in a set DataSet.
			// URL arguments:
			// - viewId:    The view id of the SetViewController to use, as used by the DataIO
			//              class.
			// - entityId:  The entity key of the entity on which the SetViewController will be
			//              used, in the format used by the EntityIO class.
			// - dataset:   The type of dataset to use:
			//              - display:  for the dataset that displays its elements
			//              - pick:     for the dataset that displays the elements that can be
			//                          picked to be added to the dataset.
			// GET arguments:
			// - sort:    The sort clauses, in the format used by SorterIO class.
			// - filter:  The filters, in the format used by FilterIO class.
			// - type:    The type of export to do.
			//            - array for entity daty as a csv file.
			//            - label for labels as a pdf file.
			// If the type is array, then:
			// - columns: The id of the columns whose data to return, in the format used by the
			//            ColumnIO class.
			// If the type is label, then:
			// - layout:  The kind of layout desired. This is the value of the LabelLayout type, as
			//            used by the Enum.Parse(...) method.
			// - text:    The id of the LabelTextFactory used to generate the label text, as an
			//            integer value.
			Get["/{viewId}/{entityId}/export/{dataset}"] = (p =>
			{
				var type		= this.Request.Query.type == "label" ? "PDF" : "CSV";
				var exportTask = this.CreateJob ("Export "+ type);
				this.Execute (wa => this.NotifyUIForExportWaiting (wa, exportTask));
				this.Enqueue (exportTask, context => this.LongRunningExport (context, exportTask, p));

				return new Response ()
				{
					StatusCode = HttpStatusCode.Accepted
				};
			});

			// Adds entities to the set DataSet
			// URL arguments:
			// - viewId:    The view id of the SetViewController to use, as used by the DataIO
			//              class.
			// - entityId:  The entity key of the entity on which the SetViewController will be
			//              used, in the format used by the EntityIO class.
			// POST arguments:
			// - entityIds: The entity keys of the entities to add to the DataSet, in the format
			//              used by the EntityIO class.
			Post["/{viewId}/{entityId}/add"] = p =>
				this.Execute (b => this.Add (b, p));
			
			// Removes entities from the set DataSet
			// URL arguments:
			// - viewId:    The view id of the SetViewController to use, as used by the DataIO
			//              class.
			// - entityId:  The entity key of the entity on which the SetViewController will be
			//              used, in the format used by the EntityIO class.
			// POST arguments:
			// - entityIds: The entity keys of the entities to removre from the DataSet, in the
			//              format used by the EntityIO class.
			Post["/{viewId}/{entityId}/remove"] = p =>
				this.Execute (b => this.Remove (b, p));
		}

		private Response GetEntities(BusinessContext businessContext, dynamic parameters)
		{
			var caches = this.CoreServer.Caches;

			string rawColumns = Request.Query.columns;
			int start = Request.Query.start;
			int limit = Request.Query.limit;

			using (ISetViewController controller = this.GetController (businessContext, parameters))
			using (EntityExtractor extractor = this.GetEntityExtractor (businessContext, controller, parameters))
			{
				return DatabaseModule.GetEntities (caches, extractor, rawColumns, start, limit);
			}
		}


		private Response Export(BusinessContext businessContext, dynamic parameters)
		{
			var caches = this.CoreServer.Caches;

			using (ISetViewController controller = this.GetController (businessContext, parameters))
			using (EntityExtractor extractor = this.GetEntityExtractor (businessContext, controller, parameters))
			{
				return DatabaseModule.Export (caches, extractor, this.Request.Query);
			}
		}

		private void LongRunningExport(BusinessContext businessContext, CoreJob job, dynamic parameters)
		{
			job.Start ();
			this.UpdateTaskStatusInBag (job);

			var user		= LoginModule.GetUserName (this);
			var fileExt		= this.Request.Query.type == "label" ? ".pdf" : ".csv";
			var filename	= job.Id + fileExt;
			var caches = this.CoreServer.Caches;

			using (ISetViewController controller = this.GetController (businessContext, parameters))
			using (EntityExtractor extractor = this.GetEntityExtractor (businessContext, controller, parameters))
			{
				DatabaseModule.ExportToDisk (filename, caches, extractor, this.Request.Query);
			}


			job.Metadata = "<br><input type='button' onclick='Epsitec.Cresus.Core.app.downloadFile(\"" + filename + "\");' value='Télécharger' />";
			job.Finish ();
			this.UpdateTaskStatusInBag (job);
		}

		private Response NotifyUIForExportWaiting(WorkerApp workerApp, CoreJob task)
		{
			var entityBag = EntityBagManager.GetCurrentEntityBagManager ();
			entityBag.AddToBag (task.Username, task.Title, task.HtmlView, task.Id, When.Now);

			return new Response ()
			{
				StatusCode = HttpStatusCode.Accepted
			};
		}

		private void UpdateTaskStatusInBag(CoreJob task)
		{
			var user = LoginModule.GetUserName (this);
			var entityBag = EntityBagManager.GetCurrentEntityBagManager ();
			entityBag.RemoveFromBag (user, task.Id, When.Now);
			entityBag.AddToBag (user, task.Title, task.HtmlView, task.Id, When.Now);
		}

		private EntityExtractor GetEntityExtractor(BusinessContext businessContext, ISetViewController controller, dynamic parameters)
		{
			var workerApp = WorkerApp.Current;
			var caches = this.CoreServer.Caches;
			var userManager = workerApp.UserManager;
			var databaseManager = this.CoreServer.DatabaseManager;

			string rawSorters = Tools.GetOptionalParameter (Request.Query.sort);
			string rawFilters = Tools.GetOptionalParameter (Request.Query.filter);

			string dataSetName = parameters.dataset;

			var dataSetGetter = workerApp.DataSetGetter;
			var dataStore = workerApp.DataStoreMetaData;

			Druid databaseId;
			Func<Database, DataSetAccessor> dataSetAccessorGetter;

			if (dataSetName == "display")
			{
				databaseId = controller.GetDisplayDataSetId ();
				dataSetAccessorGetter = _ => controller.GetDisplayDataSetAccessor (dataSetGetter, dataStore);
			}
			else if (dataSetName == "pick")
			{
				databaseId = controller.GetPickDataSetId ();
				dataSetAccessorGetter = _ => controller.GetPickDataSetAccessor (dataSetGetter, dataStore);
			}
			else
			{
				throw new ArgumentException ("Invalid data set name.");
			}

			return EntityExtractor.Create
			(
				businessContext, caches, userManager, databaseManager, dataSetAccessorGetter,
				databaseId, rawSorters, rawFilters
			);
		}


		private ISetViewController GetController(BusinessContext businessContext, dynamic parameters)
		{
			string rawEntityId = parameters.entityId;
			string rawViewId = parameters.viewId;

			var entity = EntityIO.ResolveEntity (businessContext, rawEntityId);
			var viewId = DataIO.ParseViewId (rawViewId);
			var viewMode = ViewControllerMode.Set;

			return Mason.BuildController<ISetViewController>
			(
				businessContext, entity, null, viewMode, viewId
			);
		}


		private Response Add(BusinessContext businessContext, dynamic parameters)
		{
			Action<ISetViewController, IEnumerable<AbstractEntity>> action = (c, e) => c.AddItems (e);

			return this.ExecuteAction (businessContext, action, parameters);
		}


		private Response Remove(BusinessContext businessContext, dynamic parameters)
		{
			Action<ISetViewController, IEnumerable<AbstractEntity>> action = (c, e) => c.RemoveItems (e);

			return this.ExecuteAction (businessContext, action, parameters);
		}


		private Response ExecuteAction(BusinessContext businessContext, Action<ISetViewController, IEnumerable<AbstractEntity>> action, dynamic parameters)
		{
			string rawEntityIds = this.Request.Form.entityIds;
			var entities = EntityIO.ResolveEntities (businessContext, rawEntityIds).ToList ();

			using (ISetViewController controller = this.GetController (businessContext, parameters))
			{
				using (businessContext.Bind (controller.GetEntity ()))
				using (businessContext.Bind (entities))
				{
					action (controller, entities);

					businessContext.SaveChanges (LockingPolicy.KeepLock, EntitySaveMode.IncludeEmpty);

					return CoreResponse.Success ();
				}
			}
		}


	}


}
