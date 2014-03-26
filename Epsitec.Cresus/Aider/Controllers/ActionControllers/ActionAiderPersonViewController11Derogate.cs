//	Copyright � 2014, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Samuel LOUP, Maintainer: Samuel LOUP

using Epsitec.Aider.Entities;
using Epsitec.Aider.Enumerations;

using Epsitec.Common.Support;
using Epsitec.Common.Types;

using Epsitec.Cresus.Bricks;

using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Controllers.ActionControllers;
using Epsitec.Cresus.Core.Entities;

using Epsitec.Aider.Controllers.SpecialFieldControllers;
using Epsitec.Aider.Override;

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Core.Library;
using Epsitec.Aider.Reporting;

namespace Epsitec.Aider.Controllers.ActionControllers
{
	[ControllerSubType (11)]
	public sealed class ActionAiderPersonViewController11Derogate : ActionViewController<AiderPersonEntity>
	{
		public override FormattedText GetTitle()
		{
			return Resources.Text ("D�roger vers...");
		}

		public override bool IsEnabled
		{
			get
			{
				var userManager = AiderUserManager.Current;
				var aiderUser   = userManager.AuthenticatedUser;

				return (aiderUser.Office.IsNotNull ())
					&& (aiderUser.OfficeSender.IsNotNull ());
			}
		}

		public override ActionExecutor GetExecutor()
		{
			return ActionExecutor.Create<AiderGroupEntity,Date> (this.Execute);
		}

		private void Execute(AiderGroupEntity destParish, Date date)
		{
			var needDerogationLetter = false;
			var person = this.Entity;
			var parishGroup = person.ParishGroup;

			if (person.Age.HasValue)
			{
				if (person.Age.Value <16)
				{
					var message = "Une personne de moins de 16 ans ne peut pas �tre au b�n�fice d'une d�rogation";
					throw new BusinessRuleException (message);
				}
			}
			if (destParish.IsNull ())
			{
				var message = "Vous n'avez pas s�lectionn� de paroisse.";
				throw new BusinessRuleException (message);
			}
			if (!destParish.IsParish ())
			{
				var message = "Le groupe s�lectionn� n'est pas une paroisse.";
				throw new BusinessRuleException (message);
			}

			if (parishGroup == destParish)
			{
				var message = "La personne est d�j� associ�e � cette paroisse.";
				throw new BusinessRuleException (message);
			}

			System.Diagnostics.Trace.WriteLine ("Derogating from " + parishGroup.Name);

			var derogationInGroup = destParish.Subgroups.Single (g => g.GroupDef.Classification == GroupClassification.DerogationIn);		
			var derogationOutGroup = parishGroup.Subgroups.SingleOrDefault (g => g.GroupDef.Classification == GroupClassification.DerogationOut);

			var participationData = new List<ParticipationData> ();
			participationData.Add (new ParticipationData (person.MainContact));

			//Check if a previous derogation is in place ?
			if (person.HasDerogation)
			{
				//Yes, existing derogation in place:
				//Remove old derogation in
				var oldDerogationInGroup = parishGroup.Subgroups.Single (g => g.GroupDef.Classification == GroupClassification.DerogationIn);
				oldDerogationInGroup.RemoveParticipations (this.BusinessContext, oldDerogationInGroup.FindParticipations (this.BusinessContext, person.MainContact));

				//Check for a "return to home"
				if (person.GeoParishGroupPathCache == destParish.Path)
				{
					//Reset state
					person.ClearDerogation ();

					//Remove old derogation out
					var oldDerogationOutGroup = destParish.Subgroups.Single (g => g.GroupDef.Classification == GroupClassification.DerogationOut);
					oldDerogationOutGroup.RemoveParticipations (this.BusinessContext, oldDerogationOutGroup.FindParticipations (this.BusinessContext, person.MainContact));

					//Warn old derogated parish
					AiderPersonWarningEntity.Create (this.BusinessContext, person, person.ParishGroupPathCache,
						WarningType.ParishDeparture, "Fin de d�rogation.");
					//Warn NewParish for return
					AiderPersonWarningEntity.Create (this.BusinessContext, person, destParish.Path,
						WarningType.ParishArrival, "Fin de d�rogation.");
				}
				else
				{
					//Add derogation in participation
					derogationInGroup.AddParticipations (this.BusinessContext, participationData, date, new FormattedText ("D�rogation entrante"));

					//Warn old derogated parish
					AiderPersonWarningEntity.Create (this.BusinessContext, person, person.ParishGroupPathCache,
						WarningType.ParishDeparture, "Fin de d�rogation.");
					
					this.WarnNewParish (destParish, person);
					//Warn GeoParish for a Derogation Change
					AiderPersonWarningEntity.Create (this.BusinessContext, person, person.GeoParishGroupPathCache,
						WarningType.DerogationChange, "Changement de d�rogation vers la\n" + destParish.Name + ".");

					
					needDerogationLetter = true;

					
				}
			}
			else
			{
				//No, first derogation:
				//Backup initial parish cache
				person.GeoParishGroupPathCache = person.ParishGroupPathCache;

				//	Add derogation out participation, but not if the person was in the
				//	'no parish' group before...

				if (derogationOutGroup != null)
				{
					derogationOutGroup.AddParticipations (this.BusinessContext, participationData, date, new FormattedText ("D�rogation sortante"));
					
					//	Warn GeoParish
					AiderPersonWarningEntity.Create (this.BusinessContext, person, person.GeoParishGroupPathCache,
						WarningType.ParishDeparture, "Personne d�rog�e vers la\n" + destParish.Name + ".");
				}

				//Add derogation in participation
				derogationInGroup.AddParticipations (this.BusinessContext, participationData, date, new FormattedText ("D�rogation entrante"));
				
				this.WarnNewParish (destParish, person);

				needDerogationLetter = true;
			}

			//Remove parish participations
			parishGroup.RemoveParticipations (this.BusinessContext, parishGroup.FindParticipations (this.BusinessContext, person.MainContact));

			//Add participation to the destination parish
			destParish.AddParticipations (this.BusinessContext, participationData, date, FormattedText.Null);
			
			//!Trigg business rules!
			person.ParishGroup = destParish;

			System.Diagnostics.Trace.WriteLine ("Derogated to " + destParish.Name);

			if (needDerogationLetter)
			{
				var userManager		= AiderUserManager.Current;
				var aiderUser       = userManager.AuthenticatedUser;
				var sender		    = this.BusinessContext.GetLocalEntity (aiderUser.OfficeSender);

				var letter = this.CreateDerogationLetter (this.BusinessContext, sender, destParish, parishGroup);
				//SaveChanges for ID purpose: BuildProcessorUrl need the entity ID
				this.BusinessContext.SaveChanges (LockingPolicy.ReleaseLock);
				letter.ProcessorUrl		= letter.GetProcessorUrlForSender (this.BusinessContext, "officeletter", sender);
				this.BusinessContext.SaveChanges (LockingPolicy.ReleaseLock);

				EntityBag.Add (letter, "Document PDF");	
			}

		}

		private void WarnNewParish(AiderGroupEntity destParish, AiderPersonEntity person)
		{
			string message;

			if (person.ParishGroup.IsNoParish ())
			{
				message = "Personne d�rog�e.";
			}
			else
			{
				message = "Personne d�rog�e en provenance de la\n" + person.ParishGroup.Name + ".";
			}

			AiderPersonWarningEntity.Create (this.BusinessContext, person, destParish.Path, WarningType.ParishArrival, message);
		}
		
		protected override void GetForm(ActionBrick<AiderPersonEntity, SimpleBrick<AiderPersonEntity>> form)
		{
			var userManager			= AiderUserManager.Current;
			var aiderUser			= userManager.AuthenticatedUser;
			var defaultDestParish	= this.BusinessContext.GetLocalEntity (aiderUser.Office.ParishGroup);

			form
				.Title ("D�roger vers...")
				.Field<AiderGroupEntity> ()
					.Title ("Paroisse")
					.WithSpecialField<AiderGroupSpecialField<AiderPersonEntity>> ()
					.InitialValue (this.Entity.GetGeoParishGroup (this.BusinessContext) ?? defaultDestParish)
				.End ()
				.Field<Date> ()
					.Title ("Date de la d�rogation")
					.InitialValue (Date.Today)
				.End ()
			.End ();
		}


		private AiderOfficeLetterReportEntity CreateDerogationLetter(BusinessContext businessContext, AiderOfficeSenderEntity sender, AiderGroupEntity destParish, AiderGroupEntity origineParish)
		{
			
			var office			= AiderOfficeManagementEntity.Find (businessContext, destParish);
			var recipient		= this.Entity.MainContact;
			var documentName	= "Confirmation d�rogation " + recipient.DisplayName;

			if(sender.IsNull ())
			{
				var message = "Vous devez d'abord associer votre utilisateur � un secr�tariat; la cr�ation de la d�rogation est impossible.";
				throw new BusinessRuleException (message);
			}

			if (CoreContext.HasExperimentalFeature ("OfficeManagement") == false)
			{
				throw new BusinessRuleException ("Cette fonction n'est pas encore disponible.");
			}

			var greetings = (this.Entity.eCH_Person.PersonSex == PersonSex.Male) ? "Monsieur" : "Madame";
			var fullName  = sender.OfficialContact.Person.GetFullName ();
			var content   = FormattedContent.Escape (greetings, destParish.Name, origineParish.Name, destParish.Name, fullName);
			
			return AiderOfficeLetterReportEntity.Create (businessContext, recipient, sender, documentName, "template-letter-derogation", content);
		}
	}
}
