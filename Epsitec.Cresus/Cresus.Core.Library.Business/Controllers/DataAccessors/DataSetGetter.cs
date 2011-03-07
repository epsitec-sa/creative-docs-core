﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;

using Epsitec.Cresus.Core.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.DataAccessors
{
	/// <summary>
	/// The <c>DataSetGetter</c> class resolves a data set name to the data
	///	set itself.
	/// </summary>
	public static class DataSetGetter
	{
		public static DataSetCollectionGetter ResolveDataSet(CoreData data, string dataSetName)
		{
//			return context => data.GetAllEntities<NaturalPersonEntity> (dataContext: context);
//			throw new System.NotImplementedException ();

			switch (dataSetName)
			{
#if true
				case "Customers":
					return context => data.GetAllEntities<RelationEntity> (dataContext: context);

				case "ArticleDefinitions":
					return context => data.GetAllEntities<ArticleDefinitionEntity> (dataContext: context);

				case "Documents":
					return context => data.GetAllEntities<DocumentMetadataEntity> (dataContext: context);
				
				case "InvoiceDocuments":
					return context => data.GetAllEntities<BusinessDocumentEntity> (dataContext: context);

				case "BusinessSettings":
					return context => data.GetAllEntities<BusinessSettingsEntity> (dataContext: context);

				case "Images":
					return context => data.GetAllEntities<ImageEntity> (dataContext: context);

				case "ImageBlobs":
					return context => data.GetAllEntities<ImageBlobEntity> (dataContext: context);

				case "PriceCalculators":
					return context => data.GetAllEntities<PriceCalculatorEntity> (dataContext: context);

				case "WorkflowDefinitions":
					return context => data.GetAllEntities<WorkflowDefinitionEntity> (dataContext: context);

				case "DocumentCategoryMapping":
					return context => data.GetAllEntities<DocumentCategoryMappingEntity> (dataContext: context);

				case "DocumentCategory":
					return context => data.GetAllEntities<DocumentCategoryEntity> (dataContext: context);

				case "DocumentOptions":
					return context => data.GetAllEntities<DocumentOptionsEntity> (dataContext: context);

				case "DocumentPrintingUnits":
					return context => data.GetAllEntities<DocumentPrintingUnitsEntity> (dataContext: context);
#endif
				default:
					return null;
			}
		}
		
		public static Druid GetRootEntityId(string dataSetName)
		{
//			return EntityInfo<NaturalPersonEntity>.GetTypeId ();
			switch (dataSetName)
			{
#if true
				case "Customers":
					return EntityInfo<RelationEntity>.GetTypeId ();

				case "ArticleDefinitions":
					return EntityInfo<ArticleDefinitionEntity>.GetTypeId ();

				case "Documents":
					return EntityInfo<DocumentMetadataEntity>.GetTypeId ();

				case "InvoiceDocuments":
					return EntityInfo<BusinessDocumentEntity>.GetTypeId ();

				case "BusinessSettings":
					return EntityInfo<BusinessSettingsEntity>.GetTypeId ();

				case "Images":
					return EntityInfo<ImageEntity>.GetTypeId ();

				case "ImageBlobs":
					return EntityInfo<ImageBlobEntity>.GetTypeId ();

				case "PriceCalculators":
					return EntityInfo<PriceCalculatorEntity>.GetTypeId ();

				case "WorkflowDefinitions":
					return EntityInfo<WorkflowDefinitionEntity>.GetTypeId ();

				case "DocumentCategoryMapping":
					return EntityInfo<DocumentCategoryMappingEntity>.GetTypeId ();

				case "DocumentCategory":
					return EntityInfo<DocumentCategoryEntity>.GetTypeId ();

				case "DocumentOptions":
					return EntityInfo<DocumentOptionsEntity>.GetTypeId ();

				case "DocumentPrintingUnits":
					return EntityInfo<DocumentPrintingUnitsEntity>.GetTypeId ();
#endif
				default:
					return Druid.Empty;
			}
		}
	}
}
