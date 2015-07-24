﻿//	Copyright © 2015, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Samuel LOUP, Maintainer: Samuel LOUP

using Epsitec.Aider.Entities;

using Epsitec.Common.IO;
using Epsitec.Common.Support.Extensions;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Entities;

using Epsitec.Cresus.DataLayer.Loader;
using System.Linq;
using System.Data;
using Epsitec.Cresus.Database;
using System.Collections.Generic;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Cresus.DataLayer.Expressions;
using Epsitec.Aider.Data.ECh;

namespace Epsitec.Aider.Data.Job
{
	/// <summary>
	/// Various dataquality fixes for households
	/// </summary>
	internal static class HouseholdsFix
	{
		public static void EchHouseholdsQuality(CoreData coreData, string currentEchFile)
		{
			using (var businessContext = new BusinessContext (coreData, false))
			{
				var dataContext = businessContext.DataContext;
				Logger.LogToConsole ("//////////////////////////////////////");
				Logger.LogToConsole ("////:::DATAQUALITY JOB STARTED::://///");
				Logger.LogToConsole ("//////////////////////////////////////");
				Logger.LogToConsole ("// CLEAN BAD ECH HOUSEHOLDS");
				SqlHelpers.SelectDbIds (
					businessContext, 
					"MUD_LVAG e","e.U_LVAH IS NULL AND e.U_LVAI IS NULL",
					(ids) => {
						foreach (var id in ids)
						{
							var entity = businessContext.DataContext.ResolveEntity<eCH_ReportedPersonEntity> (new DbKey (id));
							if (entity.Children.Count () == 0)
							{
								businessContext.DeleteEntity (entity);
								Logger.LogToConsole ("// DELETED");
							}
						}
					}
				);
				Logger.LogToConsole ("// Done!");
				businessContext.SaveChanges (LockingPolicy.KeepLock, EntitySaveMode.IgnoreValidationErrors);
				Logger.LogToConsole ("//////////////////////////////////////");
				Logger.LogToConsole ("// LOADING ECH REPOSITORY FROM XML...");
				var echData    = new EChReportedPersonRepository (currentEchFile);
				
				Logger.LogToConsole ("// Done!");
				Logger.LogToConsole ("//////////////////////////////////////");
				Logger.LogToConsole ("// CHECK PERSON WITH EMPTY HOUSEHOLDS");
				var fixedPersons = new List<AiderPersonEntity> ();
				var badEchStatus = new List<AiderPersonEntity> ();
				var unfixable    = new List<System.Tuple<AiderPersonEntity, string>> ();
				SqlHelpers.SelectColumn (
					businessContext,
					"a.PERSON_ECHID",
					"ECH_PERSON_WITH_HOUSEHOLDS a", "ECH_STATUS = 1 and a.HOUSEOLD1_ADULT1_CRID IS NULL",
					(personIds) =>
					{
						foreach (var personId in personIds)
						{
							var aiderPerson = EChDataHelpers.GetAiderPersonEntityById (businessContext, personId);
							var person = aiderPerson.eCH_Person;
							var housholdInfo = echData.GetHouseholdsInfo (person.PersonId);
							if (housholdInfo.Any ())
							{
								var secondHousehold = false;
								housholdInfo.ForEach ( (info) => {
									var household    = info.Item1;
									var isHead1      = info.Item2;
									var isHead2      = info.Item3;
									HouseholdsFix.FixPersonHousehold (businessContext, aiderPerson, household, null, isHead1, isHead2, secondHousehold);
									HouseholdsFix.SetPersonAsFixed (fixedPersons, aiderPerson);
									secondHousehold = true;
								});
							}
							else
							{
								switch (person.RemovalReason)
								{
									case Enumerations.RemovalReason.Unknown:
									case Enumerations.RemovalReason.None:
										person.DeclarationStatus = Enumerations.PersonDeclarationStatus.Undefined;
										break;
									case Enumerations.RemovalReason.Departed:
									case Enumerations.RemovalReason.Deceased:
										person.DeclarationStatus = Enumerations.PersonDeclarationStatus.Removed;
										break;
									default:
										person.DeclarationStatus = Enumerations.PersonDeclarationStatus.NotDeclared;
										break;
								}
								badEchStatus.Add (aiderPerson);
							}				
						}
					}
				);

				businessContext.SaveChanges (LockingPolicy.KeepLock, EntitySaveMode.IgnoreValidationErrors);
				Logger.LogToConsole ("// Done!");
				Logger.LogToConsole ("//////////////////////////////////////");
				Logger.LogToConsole ("// CHECK PERSON WITH BAD CONTACT");

				SqlHelpers.SelectColumn (
					businessContext,
					"a.PERSON_ECHID",
					"ECH_PERSON_WITH_HOUSEHOLDS a", "a.AIDER_HOUSEHOLD_CRID IS NULL AND a.HOUSEOLD1_ADULT1_CRID IS NOT NULL AND a.ECH_STATUS = 1",
					(personIds) =>
					{
						foreach (var personId in personIds)
						{
							
							var aiderPerson                = EChDataHelpers.GetAiderPersonEntityById (businessContext, personId);
							var refAdultId                 = aiderPerson.eCH_Person.ReportedPerson1.Adult1.PersonId;
							if (aiderPerson.eCH_Person.RemovalReason != Enumerations.RemovalReason.None)
							{
								var person = aiderPerson.eCH_Person;
								switch (person.RemovalReason)
								{
									case Enumerations.RemovalReason.Unknown:
										person.DeclarationStatus = Enumerations.PersonDeclarationStatus.Undefined;
										break;
									case Enumerations.RemovalReason.Departed:
									case Enumerations.RemovalReason.Deceased:
										person.DeclarationStatus = Enumerations.PersonDeclarationStatus.Removed;
										break;
									default:
										person.DeclarationStatus = Enumerations.PersonDeclarationStatus.NotDeclared;
										break;
								}
								badEchStatus.Add (aiderPerson);
								continue;
							}
							
							var contactToFix    = aiderPerson.Contacts.Where (c => c.Household.IsNull ());
							if (contactToFix.Any ())
							{
								AiderHouseholdEntity household = null;
								if (personId != refAdultId)
								{
									var echHousehold       = echData.LookupByMemberId[personId].ElementAtOrDefault (0);
																									
									if (personIds.Contains (echHousehold.Adult1.Id))
									{
										// Ensure save changes before requesting entities
										businessContext.SaveChanges (LockingPolicy.KeepLock, EntitySaveMode.IgnoreValidationErrors);
									}

									var eChHouseholdEntity = EChDataHelpers.GetEchReportedPersonEntity (businessContext, echHousehold);
									var refAiderPerson     = EChDataHelpers.GetAiderPersonEntity (businessContext, eChHouseholdEntity.Adult1);
									if (refAiderPerson.IsDeceased)
									{
										var message = "Ref person is deceased and declared in ECh registry";
										unfixable.Add (System.Tuple.Create (aiderPerson,message));
										continue;
									}

									if (refAiderPerson.HouseholdContact.IsNotNull ())
									{
										household = refAiderPerson.HouseholdContact.Household;
									}
									else
									{
										household = HouseholdsFix.CreateAiderHousehold (businessContext, refAiderPerson, echHousehold, eChHouseholdEntity);			
									}
									
								}
								else
								{
									var echHousehold    = echData.ByAdult1Id[personId];
									var eChHouseholdEntity = EChDataHelpers.GetEchReportedPersonEntity (businessContext, echHousehold);

									household = HouseholdsFix.CreateAiderHousehold (businessContext, aiderPerson, echHousehold, aiderPerson.eCH_Person.ReportedPerson1);
								}

								foreach (var contact in contactToFix)
								{
									contact.Household = household;
									HouseholdsFix.SetPersonAsFixed (fixedPersons, aiderPerson);
								}
							}
							

						}
					}
				);

				businessContext.SaveChanges (LockingPolicy.KeepLock, EntitySaveMode.IgnoreValidationErrors);

				foreach(var person in fixedPersons)
				{
					var household = person.HouseholdContact.Household;
					if (household.IsNotNull ())
					{
						AiderContactEntity.DeleteDuplicateContacts (businessContext, household.Contacts);
					}
				}

				businessContext.SaveChanges (LockingPolicy.KeepLock, EntitySaveMode.IgnoreValidationErrors);
				Logger.LogToConsole ("//////////////////////////////////////");
				Logger.LogToConsole ("// BadEchStatusFixed: " + badEchStatus.Count ());
				Logger.LogToConsole ("// HousholdFixed: " + fixedPersons.Count ());
				Logger.LogToConsole ("~             DONE!             ~");
			}
		}

		public static void SetPersonAsFixed (List<AiderPersonEntity> list, AiderPersonEntity person)
		{
			list.Add (person);
			Logger.LogToConsole ("// " + person.GetDisplayName () + " Fixed!");
		}

		public static void FixPersonHousehold(BusinessContext businessContext, AiderPersonEntity person, EChReportedPerson echHousehold, eCH_ReportedPersonEntity householdEntity, bool isHead1, bool isHead2, bool secondaryHousehold)
		{
			if (householdEntity.IsNull ())
			{
				householdEntity = EChDataHelpers.GetEchReportedPersonEntity (businessContext, echHousehold);
				if (householdEntity.IsNull ())
				{
					householdEntity = HouseholdsFix.CreateEchReportedPersonEntity (businessContext, echHousehold);
				}
			}

			var refPerson    = EChDataHelpers.GetAiderPersonEntity (businessContext, householdEntity.Adult1);
			if (refPerson.HouseholdContact.IsNotNull ())
			{
				EChDataHelpers.SetupHousehold (businessContext, person, refPerson.HouseholdContact.Household, householdEntity, isHead1, isHead2, secondaryHousehold);
			}
			else
			{
				HouseholdsFix.CreateAiderHousehold (businessContext, refPerson, echHousehold, householdEntity);
			}
		}

		public static AiderHouseholdEntity CreateAiderHousehold(BusinessContext businessContext, AiderPersonEntity refAiderPerson, EChReportedPerson echHousehold, eCH_ReportedPersonEntity householdEntity)
		{
			if (echHousehold == null)
			{
				throw new BusinessRuleException ("Cannot create household from missing ECH data");
			}

			if (refAiderPerson.IsNull ())
			{
				throw new BusinessRuleException ("Cannot create household: missing refAiderPerson");
			}
			else
			{
				if (refAiderPerson.eCH_Person.PersonId != echHousehold.Adult1.Id)
				{
					throw new BusinessRuleException ("Cannot create household: bad refAiderPerson entity provided (id mismatch)");
				}
			}

			if (householdEntity.IsNull ())
			{
				householdEntity = HouseholdsFix.CreateEchReportedPersonEntity (businessContext, echHousehold);
			}

			var addressTemplate         = EChDataHelpers.CreateAiderAddressEntityTemplate (businessContext, householdEntity);
			var newHousehold            = AiderHouseholdEntity.Create (businessContext, addressTemplate);

			EChDataHelpers.SetupHousehold (businessContext, refAiderPerson, newHousehold, householdEntity, true, false);

			if (echHousehold.Adult2 != null)
			{
				var person2       = EChDataHelpers.GetEchPersonEntity (businessContext, echHousehold.Adult2);
				var aiderPerson2 = EChDataHelpers.GetAiderPersonEntity (businessContext, person2);
				EChDataHelpers.SetupHousehold (businessContext, aiderPerson2, newHousehold, householdEntity, false, true);
			}

			foreach (var child in echHousehold.Children)
			{
				var childPerson = EChDataHelpers.GetEchPersonEntity (businessContext, child);
				var aiderChild  = EChDataHelpers.GetAiderPersonEntity (businessContext, childPerson);
				EChDataHelpers.SetupHousehold (businessContext, aiderChild, newHousehold, householdEntity, false, false);
			}

			return newHousehold;
		}

		public static bool ExecuteIfInHousehold(AiderHouseholdEntity household, string personId, System.Action<AiderPersonEntity> action)
		{
			try
			{
				var member = household.Members.Where (m => m.eCH_Person.PersonId == personId).Single ();
				action (member);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool ExecuteIfInEChHousehold(eCH_ReportedPersonEntity household, string personId, System.Action<eCH_PersonEntity> action)
		{
			try
			{
				var member = household.Members.Where (m => m.PersonId == personId).Single ();
				action (member);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool ExecuteIfInEChHouseholdRepository(EChReportedPerson household, string personId, System.Action<EChPerson> action)
		{
			try
			{
				var members = household.Children.Union (household.Adult1.ToEnumerable ()).Union (household.Adult2.ToEnumerable ());
				var member = members.Where (m => m.Id == personId).Single ();
				action (member);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static eCH_ReportedPersonEntity CreateEchReportedPersonEntity(BusinessContext businessContext, EChReportedPerson eChReportedPerson)
		{
			var eChAddressEntity          = EChDataImporter.ImportEchAddressEntity (businessContext, eChReportedPerson.Address);
			var eChReportedPersonEntity   = businessContext.CreateAndRegisterEntity<eCH_ReportedPersonEntity> ();
			eChReportedPersonEntity.Address = eChAddressEntity;
			eChReportedPersonEntity.Adult1  =  EChDataHelpers.GetEchPersonEntityById (businessContext, eChReportedPerson.Adult1.Id);
			if (eChReportedPerson.Adult2 != null)
			{
				eChReportedPersonEntity.Adult2 = EChDataHelpers.GetEchPersonEntityById (businessContext, eChReportedPerson.Adult2.Id);
			}
			foreach (var child in eChReportedPerson.Children)
			{
				eChReportedPersonEntity.Children.Add (EChDataHelpers.GetEchPersonEntityById (businessContext, child.Id));
			}

			return eChReportedPersonEntity;
		}

		public static string BuildFamilyKey (IList<AiderPersonEntity> members)
		{
			var sortedIds = members
				.Select (m => m.eCH_Person.PersonId)
				.OrderBy (id => id);

			return string.Concat (sortedIds);
		}

		public static string BuildFamilyKey(IList<eCH_PersonEntity> members)
		{
			var sortedIds = members
				.Select (m => m.PersonId)
				.OrderBy (id => id);

			return string.Concat (sortedIds);
		}

		public static DbId ToDbId (string crid)
		{
			return new DbId (System.Convert.ToInt64 (crid));
		}

		public static void RemoveHiddenPersonFromHouseholds(CoreData coreData)
		{
			using (var businessContext = new BusinessContext (coreData, false))
			{
				Logger.LogToConsole ("Removing hidden person from households...");
				Logger.LogToConsole ("~             START             ~");
				var personExample  = new AiderPersonEntity ();
				var request = new Request ()
				{
					RootEntity = personExample
				};

				request.AddCondition
				(
					businessContext.DataContext,
					personExample,
					p => p.Visibility != Enumerations.PersonVisibilityStatus.Default
				);

				var personsToCheck = businessContext.DataContext.GetByRequest<AiderPersonEntity> (request).ToArray ();

				foreach (var person in personsToCheck)
				{
					if (person.MainContact.IsNotNull ())
					{
						var household = person.MainContact.Household;

						if (household.IsNotNull ())
						{
							Logger.LogToConsole (string.Format ("Household members: {0}", household.GetMembersSummary ()));
							Logger.LogToConsole (string.Format ("removing: {0}", person.GetCompactSummary ()));
							Logger.LogToConsole (string.Format ("cause: {0}", person.Visibility));
							person.RemoveFromHousehold (businessContext, household);
							AiderHouseholdEntity.DeleteEmptyHouseholds (businessContext, household.ToEnumerable (), true);
							household.RefreshCache ();
							Logger.LogToConsole ("~             -next-             ~");
						}
					}
				}

				Logger.LogToConsole ("~             Saving...             ~");
				businessContext.SaveChanges
				(
					LockingPolicy.ReleaseLock, EntitySaveMode.IgnoreValidationErrors
				);
				Logger.LogToConsole ("~             DONE!             ~");
			}
		}
	}
}
