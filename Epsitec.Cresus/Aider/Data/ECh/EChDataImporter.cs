﻿using Epsitec.Aider.Entities;
using Epsitec.Aider.Enumerations;
using Epsitec.Aider.Tools;

using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Support.Extensions;

using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.DataLayer.Context;

using System;

using System.Collections.Generic;

using System.Diagnostics;

using System.Linq;
using Epsitec.Data.Platform;
using Epsitec.Common.Support;



namespace Epsitec.Aider.Data.ECh
{


	internal static class EChDataImporter
	{


		public static void Import(BusinessContextManager businessContextManager, IList<EChReportedPerson> eChReportedPersons)
		{
			var townDataToEntityKey = EChDataImporter.ImportTowns (businessContextManager, eChReportedPersons);

			EChDataImporter.ImportPersons (businessContextManager, eChReportedPersons, townDataToEntityKey);
		}


		private static Dictionary<Tuple<int, string>, EntityKey> ImportTowns(BusinessContextManager businessContextManager, IEnumerable<EChReportedPerson> echReportedPersons)
		{
			Func<BusinessContext, Dictionary<Tuple<int, string>, EntityKey>> function = b =>
			{
				return EChDataImporter.ImportTowns (b, echReportedPersons);
			};

			return businessContextManager.Execute (function);
		}


		private static Dictionary<Tuple<int, string>, EntityKey> ImportTowns(BusinessContext businessContext, IEnumerable<EChReportedPerson> echReportedPersons)
		{
			var switzerland = EChDataImporter.ImportSwitzerland (businessContext);

			var towns = EChDataImporter.ImportTowns (businessContext, echReportedPersons, switzerland);

			businessContext.SaveChanges ();

			return towns.ToDictionary
			(
				t => Tuple.Create (InvariantConverter.ToInt (t.ZipCode), t.Name),
				t => businessContext.DataContext.GetNormalizedEntityKey (t).Value
			);
		}


		private static AiderCountryEntity ImportSwitzerland(BusinessContext businessContext)
		{
			var switzerlandExample = new AiderCountryEntity ()
			{
				IsoCode = "CH",
			};

			var switzerland = businessContext.DataContext
				.GetByExample<AiderCountryEntity> (switzerlandExample)
				.FirstOrDefault ();

			if (switzerland == null)
			{
				switzerland = businessContext.CreateEntity<AiderCountryEntity> ();

				switzerland.IsoCode = "CH";
				switzerland.Name = "Suisse";
			}

			return switzerland;
		}


		private static IEnumerable<AiderTownEntity> ImportTowns(BusinessContext businessContext, IEnumerable<EChReportedPerson> echReportedPersons, AiderCountryEntity switzerland)
		{
			// TODO Do we need to care about the swiss zip code, the swiss zip code add on and the
			// swiss zip code id ?
			
			var towns = echReportedPersons
				.Select (rp => rp.Address)
				.Select (a => Tuple.Create (a.SwissZipCode, a.Town))
				.Distinct ();

			var aiderTowns = new List<AiderTownEntity> ();

			foreach (var town in towns)
			{
				var zipCode = InvariantConverter.ToString (town.Item1);
				var name = town.Item2;

				var aiderTown = EChDataImporter.ImportTown (businessContext, switzerland, zipCode, name);

				aiderTowns.Add (aiderTown);
			}

			return aiderTowns;
		}


		private static AiderTownEntity ImportTown(BusinessContext businessContext, AiderCountryEntity switzerland, string zipCode, string name)
		{
			var townSample = new AiderTownEntity ()
			{
				ZipCode = zipCode,
				Name = name,
			};

			var aiderTown = businessContext.DataContext
				.GetByExample<AiderTownEntity> (townSample)
				.FirstOrDefault ();

			if (aiderTown == null)
			{
				aiderTown = businessContext.CreateEntity<AiderTownEntity> ();

				aiderTown.ZipCode = zipCode;
				aiderTown.Name = name;
				aiderTown.Country = switzerland;
			}

			return aiderTown;
		}


		private static void ImportPersons(BusinessContextManager businessContextManager, IList<EChReportedPerson> eChReportedPersons, Dictionary<Tuple<int, string>, EntityKey> townDataToEntityKey)
		{
			int batchSize = 1000;
			int nbBatches = 0;

			// NOTE This dictionary will store the mapping between the eChPersonIds and the entity
			// key for the entities that have been processed and saved to the database.

			var eChPersonIdToEntityKey = new Dictionary<string, EntityKey> ();

			foreach (var batch in EChDataImporter.GetBatches (eChReportedPersons, batchSize))
			{
				Action<BusinessContext> action = b =>
				{
					EChDataImporter.ImportBatch (b, batch, eChPersonIdToEntityKey, townDataToEntityKey);
				};

				businessContextManager.Execute (action);

				nbBatches++;

				Debug.WriteLine (string.Format ("[{0}]\tImported: {1}/{2}", DateTime.Now, nbBatches * batchSize, eChReportedPersons.Count));
			}
		}


		private static void ImportBatch(BusinessContext businessContext, IEnumerable<EChReportedPerson> batch, Dictionary<string, EntityKey> eChPersonIdToEntityKey, Dictionary<Tuple<int, string>, EntityKey> townDataToEntityKey)
		{
			// NOTE This dictionary will store the mapping between the eChpersonIds and the
			// entities for the entities that have been processed but not yet saved to the
			// database.

			var eChPersonIdToEntity = new Dictionary<string, AiderPersonEntity> ();

			foreach (var eChReportedPerson in batch)
			{
				EChDataImporter.ImportHousehold (businessContext, eChPersonIdToEntityKey, eChPersonIdToEntity, eChReportedPerson, townDataToEntityKey);
			}

			businessContext.SaveChanges ();

			// NOTE Now that the changes are saved, the newly created entities have an
			// entity key which we can store in the dictionary.

			foreach (var item in eChPersonIdToEntity)
			{
				var entityKey = businessContext.DataContext.GetNormalizedEntityKey (item.Value).Value;

				eChPersonIdToEntityKey[item.Key] = entityKey;
			}
		}


		private static IEnumerable<IEnumerable<EChReportedPerson>> GetBatches(IList<EChReportedPerson> eChReportedPersons, int batchSize)
		{
			for (int index = 0; index < eChReportedPersons.Count; index += batchSize)
			{
				yield return EChDataImporter.GetBatch (eChReportedPersons, index, batchSize);
			}
		}


		private static IEnumerable<EChReportedPerson> GetBatch(IList<EChReportedPerson> eChReportedPerson, int startIndex, int size)
		{
			int upperBound = Math.Min (startIndex + size, eChReportedPerson.Count);

			for (int i = startIndex; i < upperBound; i++)
			{
				yield return eChReportedPerson[i];
			}
		}


		private static eCH_ReportedPersonEntity ImportHousehold(BusinessContext businessContext, Dictionary<string, EntityKey> eChPersonIdToEntityKey, Dictionary<string, AiderPersonEntity> eChPersonIdToEntity, EChReportedPerson eChReportedPerson, Dictionary<Tuple<int, string>, EntityKey> townDataToEntityKey)
		{
			var eChReportedPersonEntity = businessContext.CreateAndRegisterEntity<eCH_ReportedPersonEntity> ();
			var aiderHousehold = businessContext.CreateAndRegisterEntity<AiderHouseholdEntity> ();

			var eChAddress = eChReportedPerson.Address;

			var eChAddressEntity = EChDataImporter.ImportEchAddressEntity (businessContext, eChAddress);
			var aiderAddressEntity = EChDataImporter.ImportAiderAddressEntity (businessContext, eChAddress, townDataToEntityKey);

			eChReportedPersonEntity.Address = eChAddressEntity;
			aiderHousehold.Address = aiderAddressEntity;

			var eChAdult1 = eChReportedPerson.Adult1;

			if (eChAdult1 != null)
			{
				var result = EChDataImporter.ImportPerson (businessContext, eChPersonIdToEntityKey, eChPersonIdToEntity, eChAdult1, eChReportedPersonEntity, eChAddressEntity, aiderHousehold);

				eChReportedPersonEntity.Adult1 = result.Item1;
				aiderHousehold.Head1 = result.Item2;
			}

			var eChAdult2 = eChReportedPerson.Adult2;

			if (eChAdult2 != null)
			{
				var result = EChDataImporter.ImportPerson (businessContext, eChPersonIdToEntityKey, eChPersonIdToEntity, eChAdult2, eChReportedPersonEntity, eChAddressEntity, aiderHousehold);

				eChReportedPersonEntity.Adult2 = result.Item1;
				aiderHousehold.Head2 = result.Item2;
			}

			foreach (var eChChild in eChReportedPerson.Children)
			{
				var result = EChDataImporter.ImportPerson (businessContext, eChPersonIdToEntityKey, eChPersonIdToEntity, eChChild, eChReportedPersonEntity, eChAddressEntity, aiderHousehold);

				eChReportedPersonEntity.Children.Add (result.Item1);
			}

			return eChReportedPersonEntity;
		}


		private static Tuple<eCH_PersonEntity, AiderPersonEntity> ImportPerson(BusinessContext businessContext, Dictionary<string, EntityKey> eChPersonIdToEntityKey, Dictionary<string, AiderPersonEntity> eChPersonIdToEntity, EChPerson eChPerson, eCH_ReportedPersonEntity eChReportedPersonEntity, eCH_AddressEntity eChAddressEntity, AiderHouseholdEntity household)
		{
			EntityKey entityKey;
			AiderPersonEntity aiderPersonEntity;

			// NOTE Before we create the entities for an EChPerson, we check if it has already been
			// created. We check that by looking at the entities that have already been created but
			// not yet saved and at the entities that have already been saved.

			if (eChPersonIdToEntity.TryGetValue (eChPerson.Id, out aiderPersonEntity))
			{
				// NOTE Nothing to do here. We simply want the side effect of the boolean
				// expression.
			}
			else if (eChPersonIdToEntityKey.TryGetValue (eChPerson.Id, out entityKey))
			{
				aiderPersonEntity = (AiderPersonEntity) businessContext.DataContext.ResolveEntity (entityKey);
			}

			if (aiderPersonEntity != null)
			{
				aiderPersonEntity.Household2 = household;

				var eChPersonEntity = aiderPersonEntity.eCH_Person;

				eChPersonEntity.ReportedPerson2 = eChReportedPersonEntity;
				eChPersonEntity.Address2 = eChAddressEntity;

				return Tuple.Create (eChPersonEntity, aiderPersonEntity);
			}
			else
			{
				var result = EChDataImporter.ImportPerson (businessContext, eChPerson, eChReportedPersonEntity, eChAddressEntity, household);

				// NOTE We add the newly created entity to the dictionary of the entities that have
				// been created but not yet saved.

				eChPersonIdToEntity[eChPerson.Id] = result.Item2;

				return result;
			}
		}


		private static Tuple<eCH_PersonEntity, AiderPersonEntity> ImportPerson(BusinessContext businessContext, EChPerson eChPerson, eCH_ReportedPersonEntity eChReportedPersonEntity, eCH_AddressEntity eChAddressEntity, AiderHouseholdEntity household)
		{
			var aiderPersonEntity = businessContext.CreateAndRegisterEntity<AiderPersonEntity> ();
			aiderPersonEntity.Household1 = household;

			var eChPersonEntity = aiderPersonEntity.eCH_Person;

			eChPersonEntity.PersonId = eChPerson.Id;
			eChPersonEntity.PersonOfficialName = eChPerson.OfficialName;
			eChPersonEntity.PersonFirstNames = eChPerson.FirstNames;
			eChPersonEntity.PersonDateOfBirth = eChPerson.DateOfBirth;
			eChPersonEntity.PersonSex = eChPerson.Sex;
			eChPersonEntity.NationalityStatus = eChPerson.NationalityStatus;
			eChPersonEntity.NationalityCountryCode = eChPerson.NationalCountryCode;
			eChPersonEntity.Origins = eChPerson.OriginPlaces
				.Select (p => p.Name + " (" + p.Canton + ")")
				.Join ("\n");
			eChPersonEntity.AdultMaritalStatus = eChPerson.MaritalStatus;

			eChPersonEntity.CreationDate = Date.Today;
			eChPersonEntity.DataSource = Enumerations.DataSource.Government;
			eChPersonEntity.DeclarationStatus = PersonDeclarationStatus.Declared;
			eChPersonEntity.RemovalReason = RemovalReason.None;

			eChPersonEntity.ReportedPerson1 = eChReportedPersonEntity;
			eChPersonEntity.Address1 = eChAddressEntity;

			return Tuple.Create (eChPersonEntity, aiderPersonEntity);
		}


		private static AiderAddressEntity ImportAiderAddressEntity(BusinessContext businessContext, EChAddress eChAddress, Dictionary<Tuple<int, string>, EntityKey> townDataToEntityKey)
		{
			var aiderAddressEntity = businessContext.CreateAndRegisterEntity<AiderAddressEntity> ();

			var houseNumber = StringUtils.ParseNullableInt (SwissPostStreet.StripHouseNumber (eChAddress.HouseNumber));
			var houseNumberComplement = SwissPostStreet.GetHouseNumberComplement (eChAddress.HouseNumber);

			if (string.IsNullOrWhiteSpace (houseNumberComplement))
			{
				houseNumberComplement = null;
			}

			var townEntityKey = townDataToEntityKey[Tuple.Create (eChAddress.SwissZipCode, eChAddress.Town)];
			var town = (AiderTownEntity) businessContext.DataContext.ResolveEntity (townEntityKey);

			aiderAddressEntity.AddressLine1 = eChAddress.AddressLine1;
			aiderAddressEntity.Street = eChAddress.Street;
			aiderAddressEntity.HouseNumber = houseNumber;
			aiderAddressEntity.HouseNumberComplement = houseNumberComplement;
			aiderAddressEntity.Town = town;

			return aiderAddressEntity;
		}


		private static eCH_AddressEntity ImportEchAddressEntity(BusinessContext businessContext, EChAddress eChAddress)
		{
			var eChAddressEntity = businessContext.CreateAndRegisterEntity<eCH_AddressEntity> ();

			eChAddressEntity.AddressLine1 = eChAddress.AddressLine1;
			eChAddressEntity.Street = eChAddress.Street;
			eChAddressEntity.HouseNumber = eChAddress.HouseNumber;
			eChAddressEntity.Town = eChAddress.Town;
			eChAddressEntity.SwissZipCode = eChAddress.SwissZipCode;
			eChAddressEntity.SwissZipCodeAddOn = eChAddress.SwissZipCodeAddOn;
			eChAddressEntity.SwissZipCodeId = eChAddress.SwissZipCodeId;
			eChAddressEntity.Country = eChAddress.CountryCode;

			return eChAddressEntity;
		}


	}


}
