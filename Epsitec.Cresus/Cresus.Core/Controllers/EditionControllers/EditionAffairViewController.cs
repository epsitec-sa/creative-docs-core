﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Support.Extensions;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Converters;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Controllers.DataAccessors;
using Epsitec.Cresus.Core.Orchestrators.Navigation;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Widgets.Tiles;

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.DataLayer.Context;

namespace Epsitec.Cresus.Core.Controllers.EditionControllers
{
	public class EditionAffairViewController : EditionViewController<Entities.AffairEntity>
	{
		public EditionAffairViewController(string name, Entities.AffairEntity entity)
			: base (name, entity)
		{
		}


		protected override void CreateUI()
		{
			using (var builder = new UIBuilder (this))
			{
				builder.CreateHeaderEditorTile ();
				builder.CreateEditionTitleTile ("Data.Affair", "Affaire");

				this.CreateUIMain (builder);
				this.CreateUIActionButtons (builder);

				builder.CreateFooterEditorTile ();
			}
		}

		protected override void AboutToCloseUI()
		{
			base.AboutToCloseUI ();

			this.CloseUIActionButtons ();
		}

		private void CreateUIActionButtons(UIBuilder builder)
		{
#if false
			var mainViewController   = this.Orchestrator.MainViewController;
			var actionViewController = mainViewController.ActionViewController;

			actionViewController.AddButton ("NewOffer", "Nouvelle offre", "Crée une nouvelle offre (vide)", this.ExecuteNewOffer);
			
				
			mainViewController.SetActionVisibility (true);
#else
			builder.CreateMargin ();
			builder.CreateButton ("NewOffer", "Nouvelle offre", "Crée une nouvelle offre (vide)", this.ExecuteNewOffer);
			builder.CreateMargin ();
#endif
		}

		private void CloseUIActionButtons()
		{
#if false
			var mainViewController   = this.Orchestrator.MainViewController;
			var actionViewController = mainViewController.ActionViewController;

			actionViewController.RemoveButton ("NewOffer");
			
			this.Orchestrator.MainViewController.SetActionVisibility (false);
#endif
		}

		private void ExecuteNewOffer()
		{
#if false
			var workflow       = this.Entity.Workflow.UnwrapNullEntity () ?? this.BusinessContext.CreateEntity<WorkflowEntity> ();
			var workflowThread = this.BusinessContext.CreateEntity<WorkflowThreadEntity> ();
			var workflowStep   = this.BusinessContext.CreateEntity<WorkflowStepEntity> ();
			var document       = this.BusinessContext.CreateEntity<BusinessDocumentEntity> ();
			var documentMeta   = this.BusinessContext.CreateEntity<DocumentMetadataEntity> ();
			
			var now = System.DateTime.Now;

			//	TODO: définir le n° IdA plus proprement (business rule ?)
			document.IdA                   = string.Format (System.Globalization.CultureInfo.InvariantCulture, "{0}-{1}", this.Entity.IdA, this.Entity.Documents.Count + 1);
			document.OtherPartyBillingMode = Business.Finance.BillingMode.IncludingTax;
			document.OtherPartyTaxMode     = Business.Finance.TaxMode.LiableForVat;
			document.BillingCurrencyCode   = Business.Finance.CurrencyCode.Chf;
			document.BillingStatus         = Business.Finance.BillingStatus.None;
			document.BillingDate           = new Date (now);
//-			document.Description           = FormattedText.FromSimpleText ("Offre");

			var relation = this.Orchestrator.MainViewController.GetVisibleEntities ().Select (x => x as RelationEntity).Where (x => x.IsNull () == false).FirstOrDefault ();

			if (relation.DefaultAddress.IsNull () == false)
            {
				var mailContact = relation.Person.Contacts.Where (x => x is Entities.MailContactEntity).Cast<Entities.MailContactEntity> ().Where (x => x.Address == relation.DefaultAddress).FirstOrDefault ();
				
				//	TODO: sélectionner l'adresse de facturation et l'adresse de livraison selon le type d'adresse !

				document.BillingMailContact  = mailContact;
				document.ShippingMailContact = mailContact;
            }

			//	TODO: clean up horrible hack

			documentMeta.IdA = document.IdA;
			documentMeta.DocumentSource = Business.DocumentSource.Generated;
			documentMeta.DocumentType = Business.DocumentType.SalesQuote;
			documentMeta.BusinessDocument = document;

			workflowStep.Date      = now;
			workflowThread.ActiveDocuments.Add (documentMeta);
			workflowThread.History.Add (workflowStep);

			this.DataContext.UpdateEmptyEntityStatus (workflow, false);
			this.DataContext.UpdateEmptyEntityStatus (workflowThread, false);
			this.DataContext.UpdateEmptyEntityStatus (workflowStep, false);

			this.Entity.Workflow = workflow;

			this.ReopenSubView (new TileNavigationPathElement (this.GetOfferTileName (document) + ".0"));
#endif
		}


		private void CreateUIMain(Epsitec.Cresus.Core.UIBuilder builder)
		{
			var tile = builder.CreateEditionTile ();

			FrameBox group = builder.CreateGroup (tile, "N° de l'affaire (principal, externe et interne)");
			builder.CreateTextField (group, DockStyle.Left, 74, Marshaler.Create (() => this.Entity.IdA));
			builder.CreateTextField (group, DockStyle.Left, 74, Marshaler.Create (() => this.Entity.IdB, x => this.Entity.IdB = x));
			builder.CreateTextField (group, DockStyle.Left, 74, Marshaler.Create (() => this.Entity.IdC, x => this.Entity.IdC = x));

			builder.CreateMargin (tile, horizontalSeparator: true);
			builder.CreateAccountEditor (tile, "Compte débiteur (comptabilité)", Marshaler.Create (() => this.Entity.DefaultDebtorBookAccount, x => this.Entity.DefaultDebtorBookAccount = x), this.BusinessContext);
		}

		private static string GetOfferTileName(int index)
		{

			if (index < 0)
			{
				return null;
			}
			else
			{
				return string.Format (System.Globalization.CultureInfo.InstalledUICulture, "Offer.{0}", index);
			}
		}

		private string GetOfferTileName(BusinessDocumentEntity doc)
		{
			return EditionAffairViewController.GetOfferTileName (this.Entity.WorkflowDocuments.IndexOf (doc));
		}

	}
}
