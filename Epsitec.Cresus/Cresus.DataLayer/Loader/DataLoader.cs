﻿using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Support.Extensions;

using Epsitec.Common.Types;

using Epsitec.Cresus.Database;

using Epsitec.Cresus.DataLayer.Context;
using Epsitec.Cresus.DataLayer.Proxies;
using Epsitec.Cresus.DataLayer.Serialization;

using System.Collections;
using System.Collections.Generic;

using System.Linq;


namespace Epsitec.Cresus.DataLayer.Loader
{

	
	// TODO Comment this class
	// Marc


	internal sealed class DataLoader
	{


		public DataLoader(DataContext dataContext)
		{
			this.DataContext = dataContext;
			this.LoaderQueryGenerator = new LoaderQueryGenerator (dataContext);
		}


		private DataContext DataContext
		{
			get;
			set;
		}


		private EntityContext EntityContext
		{
			get
			{
				return this.DataContext.EntityContext;
			}
		}


		private LoaderQueryGenerator LoaderQueryGenerator
		{
			get;
			set;
		}


		public IEnumerable<T> GetByExample<T>(T example) where T : AbstractEntity
		{
			Request request = new Request ()
			{
				RootEntity = example,
				RequestedEntity = example,
			};

			return this.GetByRequest<T> (request);
		}


		public IEnumerable<T> GetByRequest<T>(Request request) where T : AbstractEntity
		{
			request.ThrowIfNull ("request");
			request.RootEntity.ThrowIfNull ("request.RootEntity");
			request.RequestedEntity.ThrowIfNull ("request.RequestedEntity");
			
			foreach (EntityData entityData in this.LoaderQueryGenerator.GetEntitiesData (request))
			{
				T entity = this.ResolveEntity (entityData) as T;

				if (entity != null)
				{
					yield return entity;
				}
			}
		}


		public AbstractEntity ResolveEntity(Druid entityId, DbKey rowKey)
		{
			AbstractEntity entity = EntityClassFactory.CreateEmptyEntity (entityId);

			Request request = new Request ()
			{
				RootEntity = entity,
				RootEntityKey = rowKey,
			};

			return this.GetByRequest<AbstractEntity> (request).FirstOrDefault ();
		}


		public AbstractEntity ResolveEntity(EntityData entityData)
		{
			Druid leafEntityId = entityData.LeafEntityId;
			DbKey rowKey = entityData.RowKey;

			EntityKey entityKey = EntityKey.CreateNormalizedEntityKey (this.EntityContext, leafEntityId, rowKey);

			AbstractEntity entity = this.DataContext.GetEntity (entityKey);

			if (entity == null)
			{
				entity = this.DataContext.SerializationManager.Deserialize (entityData);
			}

			return entity;
		}


		public object ResolveValueField(AbstractEntity entity, Druid fieldId)
		{
			return this.LoaderQueryGenerator.GetFieldValue (entity, fieldId);
		}


		public AbstractEntity ResolveReferenceField(AbstractEntity entity, Druid fieldId)
		{
			string fieldName = fieldId.ToResourceId ();
			
			Druid leafEntityId = entity.GetEntityStructuredTypeId ();
			StructuredTypeField field = this.EntityContext.GetEntityFieldDefinition (leafEntityId, fieldName);

			AbstractEntity rootExample = EntityClassFactory.CreateEmptyEntity (leafEntityId);
			AbstractEntity targetExample = EntityClassFactory.CreateEmptyEntity (field.TypeId);

			rootExample.SetField<AbstractEntity> (fieldName, targetExample);

			Request request = new Request ()
			{
				RootEntity = rootExample,
				RootEntityKey = this.DataContext.GetEntityKey (entity).Value.RowKey,
				RequestedEntity = targetExample,
			};

			return this.DataContext.DataLoader.GetByRequest<AbstractEntity> (request).FirstOrDefault ();
		}


		public IEnumerable<AbstractEntity> ResolveCollectionField(AbstractEntity entity, Druid fieldId)
		{
			string fieldName = fieldId.ToResourceId ();

			Druid leafEntityId = entity.GetEntityStructuredTypeId ();
			StructuredTypeField field = this.EntityContext.GetEntityFieldDefinition (leafEntityId, fieldName);

			AbstractEntity rootExample = EntityClassFactory.CreateEmptyEntity (leafEntityId);
			AbstractEntity targetExample = EntityClassFactory.CreateEmptyEntity (field.TypeId);

			rootExample.GetFieldCollection<AbstractEntity> (fieldName).Add (targetExample);

			Request request = new Request ()
			{
				RootEntity = rootExample,
				RootEntityKey = this.DataContext.GetEntityKey (entity).Value.RowKey,
				RequestedEntity = targetExample,
			};

			return this.DataContext.DataLoader.GetByRequest<AbstractEntity> (request);
		}
		

	}


}
