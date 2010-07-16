﻿using Epsitec.Common.Support;

using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;

using Epsitec.Cresus.Database;

using Epsitec.Cresus.DataLayer.Context;
using Epsitec.Cresus.DataLayer.Schema;

using System.Collections;
using System.Collections.Generic;

using System.Linq;
using Epsitec.Cresus.DataLayer.Proxies;


namespace Epsitec.Cresus.DataLayer.Loader
{

	
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


		private DbInfrastructure DbInfrastructure
		{
			get
			{
				return this.DataContext.DbInfrastructure;
			}
		}


		private LoaderQueryGenerator LoaderQueryGenerator
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


		private SchemaEngine SchemaEngine
		{
			get
			{
				return this.DataContext.SchemaEngine;
			}
		}


		public IEnumerable<T> GetByExample<T>(T example) where T : AbstractEntity
		{
			Request request = new Request ()
			{
				RootEntity = example,
				RequestedEntity = example,
				ResolutionMode = ResolutionMode.Database,
			};

			return this.GetByRequest<T> (request);
		}


		public IEnumerable<T> GetByRequest<T>(Request request) where T : AbstractEntity
		{
			if (request.RootEntity == null)
			{
				throw new System.Exception ("No root entity in request.");
			}

			if (request.RequestedEntity == null)
			{
				throw new System.Exception ("No requested entity in request.");
			}

			foreach (EntityData entityData in this.LoaderQueryGenerator.GetEntitiesData (request))
			{
				T entity = this.ResolveEntity (entityData, request.ResolutionMode) as T;

				if (entity != null)
				{
					yield return entity;
				}
			}
		}


		public IEnumerable<System.Tuple<AbstractEntity, EntityFieldPath>> GetReferencers(AbstractEntity target, ResolutionMode resolutionMode = ResolutionMode.Database)
		{
			if (this.DataContext.GetEntityKey (target) != null)
			{
				foreach (Druid targetEntityId in this.EntityContext.GetInheritedEntityIds (target.GetEntityStructuredTypeId ()))
				{
					foreach (EntityFieldPath sourceFieldPath in this.DbInfrastructure.GetSourceReferences (targetEntityId))
					{
						foreach (System.Tuple<AbstractEntity, EntityFieldPath> item in this.GetReferencers (sourceFieldPath, target, resolutionMode))
						{
							yield return item;
						}
					}
				}
			}
		}


		private IEnumerable<System.Tuple<AbstractEntity, EntityFieldPath>> GetReferencers(EntityFieldPath sourceFieldPath, AbstractEntity target, ResolutionMode resolutionMode)
		{
			Druid sourceEntityId = sourceFieldPath.EntityId;
			string sourceFieldId = sourceFieldPath.Fields.First ();

			AbstractEntity example = this.EntityContext.CreateEmptyEntity (sourceEntityId);
			StructuredTypeField field = this.EntityContext.GetStructuredTypeField (example, sourceFieldId);

			using (example.DefineOriginalValues ())
			{
				if (field.Relation == FieldRelation.Reference)
				{
					example.SetField<object> (field.Id, target);
				}
				else if (field.Relation == FieldRelation.Collection)
				{
					example.InternalGetFieldCollection (field.Id).Add (target);
				}
			}

			Request request = new Request ()
			{
				RootEntity = example,
				ResolutionMode = resolutionMode,
			};

			return this.GetByRequest<AbstractEntity> (request).Select (sourceEntity => System.Tuple.Create (sourceEntity, sourceFieldPath));
		}


		public AbstractEntity ResolveEntity(DbKey dbKey, Druid entityId)
		{
			// TODO Change this method so that it takes an EntityKey as argument
			// Marc

			AbstractEntity entity = EntityClassFactory.CreateEmptyEntity (entityId);

			Request request = new Request ()
			{
				RootEntity = entity,
				RootEntityKey = dbKey,
			};

			return this.GetByRequest<AbstractEntity> (request).FirstOrDefault ();
		}


		public AbstractEntity ResolveEntity(EntityData entityData, ResolutionMode resolutionMode = ResolutionMode.Database)
		{
			Druid leafEntityId = entityData.LeafEntityId;
			DbKey rowKey = entityData.Key;

			EntityKey entityKey = new EntityKey (rowKey, leafEntityId);

			AbstractEntity entity = this.DataContext.GetEntity (entityKey);

			if (entity == null && resolutionMode == ResolutionMode.Database)
			{
				entity = this.DeserializeEntity (entityData);
			}

			return entity;
		}


		private AbstractEntity DeserializeEntity(EntityData entityData)
		{
			AbstractEntity entity = this.DataContext.CreateEntity (entityData.LeafEntityId);

			this.DataContext.DefineRowKey (entity, entityData.Key);

			using (entity.DefineOriginalValues ())
			{
				List<Druid> entityIds = this.EntityContext.GetInheritedEntityIds (entityData.LeafEntityId).ToList ();

				foreach (Druid currentId in entityIds.TakeWhile (id => id != entityData.LoadedEntityId))
				{
					this.DeserializeEntityLocalWithProxies (entity, currentId);
				}

				foreach (Druid currentId in entityIds.SkipWhile (id => id != entityData.LoadedEntityId))
				{
					this.DeserializeEntityLocal (entity, entityData, currentId);
				}
			}

			return entity;
		}


		private void DeserializeEntityLocalWithProxies(AbstractEntity entity, Druid entityId)
		{
			foreach (StructuredTypeField field in this.EntityContext.GetEntityLocalFieldDefinitions (entityId))
			{
				object proxy = GetProxyForField (entity, field);

				entity.InternalSetValue (field.Id, proxy);
			}
		}


		private object GetProxyForField(AbstractEntity entity, StructuredTypeField field)
		{
			switch (field.Relation)
			{
				case FieldRelation.None:
					return new ValueFieldProxy (this.DataContext, entity, field.CaptionId);
				
				case FieldRelation.Reference:
					return new EntityFieldProxy (this.DataContext, entity, field.CaptionId);
					
				case FieldRelation.Collection:
					return new EntityCollectionFieldProxy (this.DataContext, entity, field.CaptionId);
					
				default:
					throw new System.NotImplementedException ();
			}
		}

		private void DeserializeEntityLocal(AbstractEntity entity, EntityData entityData, Druid localEntityId)
		{
			foreach (StructuredTypeField field in this.EntityContext.GetEntityLocalFieldDefinitions (localEntityId))
			{
				this.DeserializeEntityLocalField (entity, entityData, field);
			}
		}


		private void DeserializeEntityLocalField(AbstractEntity entity, EntityData entityData, StructuredTypeField field)
		{
			switch (field.Relation)
			{
				case FieldRelation.None:
					this.DeserializeEntityLocalFieldValue (entity, entityData, field);
					break;

				case FieldRelation.Reference:
					this.DeserializeEntityLocalFieldReference (entity, entityData, field);
					break;

				case FieldRelation.Collection:
					this.DeserializeEntityLocalFieldCollection (entity, entityData, field);
					break;

				default:
					throw new System.NotImplementedException ();
			}
		}


		private void DeserializeEntityLocalFieldValue(AbstractEntity entity, EntityData entityData, StructuredTypeField field)
		{
			Druid fieldId = field.CaptionId;

			object internalValue = entityData.ValueData[fieldId];
			object externalValue = this.GetFieldValue (entity, fieldId, internalValue);

			entity.InternalSetValue (field.Id, externalValue);
		}


		private void DeserializeEntityLocalFieldReference(AbstractEntity entity, EntityData entityData, StructuredTypeField field)
		{
			DbKey? targetKey = entityData.ReferenceData[field.CaptionId];

			if (targetKey.HasValue)
			{
				EntityKey entityKey = new EntityKey (targetKey.Value, field.TypeId);

				object target = new EntityKeyProxy (this.DataContext, entityKey);

				entity.InternalSetValue (field.Id, target);
			}
		}


		private void DeserializeEntityLocalFieldCollection(AbstractEntity entity, EntityData entityData, StructuredTypeField field)
		{
			IList targets = entity.InternalGetFieldCollection (field.Id);

			foreach (DbKey targetKey in entityData.CollectionData[field.CaptionId])
			{
				EntityKey entityKey = new EntityKey (targetKey, field.TypeId);

				object target = new EntityKeyProxy (this.DataContext, entityKey);

				targets.Add (target);
			}
		}


		public object GetFieldValue(AbstractEntity entity, Druid fieldId)
		{
			object value = this.LoaderQueryGenerator.GetFieldValue (entity, fieldId);

			return this.GetFieldValue (entity, fieldId, value);
		}


		private object GetFieldValue(AbstractEntity entity, Druid fieldId, object value)
		{
			Druid leafEntityId = entity.GetEntityStructuredTypeId ();
			string fieldName = fieldId.ToResourceId ();
			StructuredTypeField field = this.EntityContext.GetEntityFieldDefinition (leafEntityId, fieldName);

			return this.ConvertFromInternal (leafEntityId, field, value);
		}


		// TODO Move these two methods (and other associated with them) to LoaderQueryGenerator, as
		// they are kind of low level. It would also be nice to move the unsupported type conversion
		// to the database level.
		// Marc

		private object ConvertFromInternal(Druid leafEntityId, StructuredTypeField field, object value)
		{
			object newValue = value;

			if (newValue != System.DBNull.Value)
			{
				IStringType stringType = field.Type as IStringType;

				if (stringType != null)
				{
					if (stringType.UseFormattedText)
					{
						newValue = FormattedText.CastToFormattedText (newValue);
					}
				}
				else
				{
					Druid localEntityId = this.EntityContext.GetLocalEntityId (leafEntityId, field.CaptionId);
					string columnName = this.SchemaEngine.GetEntityColumnName (field.Id);

					DbTable dbTable = this.SchemaEngine.GetEntityTableDefinition (localEntityId);
					DbColumn dbColumn = dbTable.Columns[columnName];
					
					//	The conversion is a two step process:
					//	1. Convert from an ADO.NET type to a simple type (i.e. almost all numbers map to decimal)
					//	2. Convert from the simple type to the expected field type

					newValue = this.ConvertFromInternal (dbColumn, newValue);
					InvariantConverter.Convert (newValue, field, out newValue);
				}
			}

			return newValue;
		}


		private object ConvertFromInternal(DbColumn dbColumn, object value)
		{
			object newValue = value;
			
			DbRawType rawType = dbColumn.Type.RawType;

			ITypeConverter typeConverter = this.DbInfrastructure.Converter;

			if (!typeConverter.CheckNativeSupport (rawType))
			{
				IRawTypeConverter rawTypeConverter;
				bool sucess = typeConverter.GetRawTypeConverter (rawType, out rawTypeConverter);

				if (!sucess)
				{
					throw new System.NotSupportedException ("Unable to convert unsupported raw type: " + rawType);
				}

				newValue = rawTypeConverter.ConvertFromInternalType (newValue);
			}

			if (value != System.DBNull.Value)
			{
				DbSimpleType typeDef = dbColumn.Type.SimpleType;
				DbNumDef numDef = dbColumn.Type.NumDef;

				return TypeConverter.ConvertToSimpleType (value, typeDef, numDef);
			}

			return newValue;
		}

	}


}
