using Epsitec.Aider.Data.Common;
using Epsitec.Aider.Entities;
using Epsitec.Aider.Enumerations;
using Epsitec.Common.IO;
using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Epsitec.Aider.Data.Job
{
	/// <summary>
	/// Applique un code M, indiquant que la personne n'a plus de ménage dans AIDER
	/// </summary>
	internal static class MXFlagger
	{
		public static void FlagContacts(CoreData coreData)
		{
			AiderEnumerator.Execute (coreData, MXFlagger.FlagContacts);
		}

		public static string GetCode(AiderPersonEntity person)
		{
			//M1 si personne a été supprimée du RCH
			//M2 si Source manuelle ET manque l'adresse
			//M3 si Source manuelle mais on a une adresse
			//M4 si adresse OK ET présent dans le RCH.

			var isGov = person.IsGovernmentDefined;
			var isRemoved = isGov && person.eCH_Person.DeclarationStatus == PersonDeclarationStatus.Removed;
			var badAddress = string.IsNullOrWhiteSpace (person.MainContact.Address.StreetHouseNumberAndComplement);
			var isVisible = person.MainContact.DisplayVisibility == PersonVisibilityStatus.Default;

			if (isRemoved)
			{
				return "M1;";
			}

			if (isGov)
			{
				if (!badAddress)
				{
					return "M4;";
				}
			}
			else
			{
				if (badAddress)
				{
					return "M2;";
				}
				else
				{
					return "M3;";
				}
			}

			return "M;";
		}

		private static void FlagContacts
		(
			BusinessContext businessContext,
			IEnumerable<AiderPersonEntity> batch)
		{
			var persons = batch
			.Where (
				p =>
				p.MainContact.IsNotNull () &&
				p.Households.Count == 0)
			.Where ( 
				pv => 
				pv.Visibility == PersonVisibilityStatus.Default || pv.MainContact.DisplayVisibility == PersonVisibilityStatus.Default
			);

			foreach (var person in persons)
			{
				MXFlagger.Flag (businessContext, person);
			}

			businessContext.SaveChanges (LockingPolicy.KeepLock, EntitySaveMode.IgnoreValidationErrors);
		}


		private static void Flag
		(
			BusinessContext businessContext,
			AiderPersonEntity person
		)
		{
			
            person.MainContact.QualityCode += MXFlagger.GetCode (person);
		}


	}


}
