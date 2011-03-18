﻿//	Copyright © 2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Repositories;

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epsitec.Cresus.Core.Business.Actions
{
	public static class AffairActions
	{
		public static void CreateSalesQuote()
		{
			var workflowEngine  = WorkflowExecutionEngine.Current;
			var businessContext = workflowEngine.Transition.BusinessContext as BusinessContext;
			var categoryRepo    = businessContext.GetSpecificRepository<DocumentCategoryEntity.Repository> ();
			var currentAffair   = businessContext.GetMasterEntity<AffairEntity> ();

			var documentMetadata = businessContext.CreateEntity<DocumentMetadataEntity> ();
			var businessDocument = businessContext.CreateEntity<BusinessDocumentEntity> ();
			var documentCategory = categoryRepo.Find (DocumentType.SalesQuote).First ();
			
			documentMetadata.DocumentCategory = documentCategory;
			documentMetadata.DocumentTitle    = documentCategory.Name;
			documentMetadata.BusinessDocument = businessDocument;
			documentMetadata.DocumentState    = DocumentState.Active;

			documentMetadata.Workflow = WorkflowFactory.CreateDefaultWorkflow<DocumentMetadataEntity> (businessContext, "Document/SalesQuote");

			currentAffair.Documents.Add (documentMetadata);
		}
	}
}
