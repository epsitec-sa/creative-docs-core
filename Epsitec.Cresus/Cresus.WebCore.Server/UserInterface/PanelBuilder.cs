﻿using Epsitec.Common.Support.EntityEngine;

using Epsitec.Cresus.Bricks;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Controllers;

using Epsitec.Cresus.WebCore.Server.Core;
using Epsitec.Cresus.WebCore.Server.Core.PropertyAccessor;
using Epsitec.Cresus.WebCore.Server.Core.PropertyAutoCreator;

using Epsitec.Cresus.WebCore.Server.UserInterface.Tile;
using Epsitec.Cresus.WebCore.Server.UserInterface.TileData;

using System;

using System.Collections.Generic;

using System.Linq;


namespace Epsitec.Cresus.WebCore.Server.UserInterface
{


	/// <summary>
	/// Allow to create an ExtJS 4 panel by inferring the layout using AbstractEntities and their
	/// ViewControllers.
	/// </summary>
	internal sealed class PanelBuilder
	{


		public PanelBuilder(BusinessContext businessContext, PropertyAccessorCache propertyAccessorCache, AutoCreatorCache autoCreatorCache)
		{
			this.businessContext = businessContext;
			this.propertyAccessorCache = propertyAccessorCache;
			this.autoCreatorCache = autoCreatorCache;
		}


		public Dictionary<string, object> Build(AbstractEntity entity, ViewControllerMode controllerMode, int? controllerSubTypeId)
		{
			var panelData = new PanelData ()
			{
				ViewControllerMode = controllerMode,
				ControllerSubTypeId = controllerSubTypeId,
				Tiles = this.GetTileData (entity, controllerMode, controllerSubTypeId).ToList (),
			};

			return panelData
				.ToPanel (this, entity)
				.ToDictionary ();
		}

		private IEnumerable<ITileData> GetTileData(AbstractEntity entity, ViewControllerMode controllerMode, int? controllerSubTypeId)
		{
			var brickWall = Mason.BuildBrickWall (entity, controllerMode, controllerSubTypeId);
			
			return Carpenter.BuildTileData (brickWall, this.propertyAccessorCache, this.autoCreatorCache);
		}


		public IEnumerable<AbstractTile> GetTiles(IEnumerable<ITileData> tileData, AbstractEntity entity)
		{
			return tileData.SelectMany (td => td.ToTiles (this, entity));
		}


		public string GetEntityId(AbstractEntity entity)
		{
			return Tools.GetEntityId (this.businessContext, entity);
		}


		public string GetIconClass(Type entityType, string uri)
		{
			return IconManager.GetCssClassName (entityType, uri, IconSize.Sixteen);
		}


		public string GetTypeName(Type type)
		{
			return type.AssemblyQualifiedName;
		}


		public IEnumerable<AbstractTile> BuildEditionTiles(AbstractEntity entity)
		{
			var tileData = this.GetTileData (entity, ViewControllerMode.Edition, null);

			return this.GetTiles (tileData, entity);
		}


		public IEnumerable<AbstractEntity> GetEntities(Type entityType)
		{
			var mode = DataExtractionMode.Sorted;
			var dataContext = this.businessContext.DataContext;

			return this.businessContext.Data.GetAllEntities (entityType, mode, dataContext);
		}
		
		
		private readonly BusinessContext businessContext;


		private readonly PropertyAccessorCache propertyAccessorCache;


		private readonly AutoCreatorCache autoCreatorCache;


	}


}
