//	Copyright � 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.Server.Engine
{
	/// <summary>
	/// Cette classe sait fabriquer un nouveau mandat MCH2, tout beau tout propre.
	/// </summary>
	public class MCH2MandatFactory : AbstractMandatFactory
	{
		public MCH2MandatFactory(DataAccessor accessor)
			: base (accessor)
		{
		}


		public override DataMandat Create(string name, System.DateTime startDate, bool withSamples)
		{
			this.withSamples = withSamples;

			this.accessor.Mandat = new DataMandat (name, startDate);

			this.AddAssetsSettings ();
			this.AddPersonsSettings ();

			if (this.withSamples)
			{
				DummyAccounts.AddAccounts (this.accessor.Mandat);
			}

			this.CreateGroupsSamples ();
			this.CreateCatsSamples ();

			if (this.withSamples)
			{
				this.AddPersonsSamples ();
				this.AddAssetsSamples ();
			}

			//	Recalcule tout.
			foreach (var obj in this.accessor.Mandat.GetData (BaseType.Assets))
			{
				Amortizations.UpdateAmounts (this.accessor, obj);
			}

			return this.accessor.Mandat;
		}


		protected override void AddAssetsSettings()
		{
			base.AddAssetsSettings ();

			if (this.withSamples)
			{
				this.fieldAssetValue1 = this.AddSettings (BaseType.Assets, "Valeur remplacement", FieldType.ComputedAmount, false, 120, null, null, null, 10);
				this.fieldAssetValue2 = this.AddSettings (BaseType.Assets, "Valeur fiscale",      FieldType.ComputedAmount, false, 120, null, null, null,  0);
				this.fieldAssetOwner1 = this.AddSettings (BaseType.Assets, "Responsable",         FieldType.GuidPerson,     false, 150, null, null, null, 10);
				this.fieldAssetOwner2 = this.AddSettings (BaseType.Assets, "Rempla�ant",          FieldType.GuidPerson,     false, 150, null, null, null,  0);
				this.fieldAssetDesc   = this.AddSettings (BaseType.Assets, "Description",         FieldType.String,         false, 120,  380,    5, null, 10);
			}
		}


		protected override void AddAssetsSamples()
		{
			var i1 = this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddDays  (  0), "B�timent Patrimoine administratif",   "100", 2400000.0m, 3500000.0m, 2100000.0m, "Dupond",   "Nicolet",  "Immobilier",             "B�timents",              "Immobilisations corporelles",   "Patrimoine administratif");
			         this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddDays  (  0), "Voirie",                   "105", 1200000.0m, 1500000.0m,  500000.0m, "Dupond",   "Nicolet",  "Immobilier",             "D�cheteries",            "Immobilisations corporelles",   "Patrimoine administratif");
			         this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddDays  (  0), "D�ch�terie communale",     "106", 3500000.0m, 4100000.0m, 3000000.0m, "Dupond",   null,       "Immobilier",             "D�cheteries",            "Immobilisations corporelles",   "Patrimoine administratif");
			var i2 = this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddDays  (  0), "Coll�ge de Marcolet",      "200", 5100000.0m, 7500000.0m, 4000000.0m, "Dupond",   null,       "Immobilier",             "Ecoles",                 "Immobilisations corporelles",   "Patrimoine administratif");
			         this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddDays  (  0), "Ecole des Trois Sapins",   "201", 3200000.0m, 3300000.0m, 3000000.0m, "Dupond",   null,       "Immobilier",             "Ecoles",                 "Immobilisations corporelles",   "Patrimoine administratif");
			         this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddYears (  2), "STEP intercommunale",      "115", 3200000.0m, 4000000.0m, 2500000.0m, "Dubosson", "Nicolet",  "Immobilier",             "Traitement des eaux",    "Immobilisations corporelles",   "Patrimoine administratif");
			         this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddDays  ( 50), "Scania X-20",              "200",  142000.0m,  160000.0m,  150000.0m, "Dupond",   "Nicolet",  "V�hicules",              "Camions",                "Immobilisations corporelles",   "Patrimoine administratif");
			         this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddDays  ( 31), "Scania X-45",              "201",   84000.0m,  100000.0m,  110000.0m, "Dupond",   "Nicolet",  "V�hicules",              "Camions",                "Immobilisations corporelles",   "Patrimoine administratif");
			         this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddDays  ( 31), "Volvo Truck P2",           "205",   90000.0m,  100000.0m,  100000.0m, "Nicolet",  "Zumstein", "V�hicules",              "Camions",                "Immobilisations corporelles",   "Patrimoine administratif");
			var v1 = this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddYears (  1), "Fiat Uno",                 "300",    8000.0m,   20000.0m,   10000.0m, "Nicolet",  null,       "V�hicules",              "Voitures",               "Immobilisations corporelles",   "Patrimoine administratif");
			var v2 = this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddDays  (100), "Citro�n C4 Picasso",       "304",   22000.0m,   35000.0m,   35000.0m, "Nicolet",  null,       "V�hicules",              "Voitures",               "Immobilisations corporelles",   "Patrimoine administratif");
			         this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddDays  (  0), "Parcelle du Cr�t-au-Clos", "400", 1000000.0m,       null,  900000.0m, "Dupond",   "Nicolet",  "Immobilier",             "Terrains",               "Immobilisations corporelles",   "Patrimoine administratif");
			         this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddDays  (  0), "Participations Nestl�",    "500",  300000.0m,       null,  290000.0m, "Zumstein", null,       "Amortissements manuels", "Autres immobilisations", "Immobilisations incorporelles", "Patrimoine financier");
			var p1 = this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddDays  (  0), "Participations Logitech",  "501",   10000.0m,       null,   15000.0m, "Zumstein", null,       "Amortissements manuels", "Autres immobilisations", "Immobilisations incorporelles", "Patrimoine financier");
			         this.AddAssetsSamples (this.accessor.Mandat.StartDate.AddDays  (200), "Participations Raifeisen", "502",  250000.0m,       null,  250000.0m, "Dubosson", null,       "Amortissements manuels", "Autres immobilisations", "Immobilisations incorporelles", "Patrimoine financier");

			{
				var e = this.AddAssetEvent (i1, this.accessor.Mandat.StartDate.AddYears (1), EventType.Modification);
				this.AddAssetComputedAmount (e, this.fieldAssetValue1, 3600000.0m);
				this.AddAssetComputedAmount (e, this.fieldAssetValue2, 1900000.0m);
			}

			{
				var e = this.AddAssetEvent (i1, this.accessor.Mandat.StartDate.AddYears (2), EventType.Modification);
				this.AddAssetComputedAmount (e, this.fieldAssetValue1, 3800000.0m);
			}

			{
				var e = this.AddAssetEvent (i2, this.accessor.Mandat.StartDate.AddYears (1).AddDays (-1), EventType.AmortizationExtra);
				this.AddAssetAmortizedAmount (e, 4200000.0m, 3600000.0m);
			}

			{
				var e = this.AddAssetEvent (v1, this.accessor.Mandat.StartDate.AddYears (1).AddDays (40), EventType.Modification);
				this.AddAssetPerson (e, this.fieldAssetOwner1, "Zumstein");
			}

			{
				var e1 = this.AddAssetEvent (v2, this.accessor.Mandat.StartDate.AddYears (1).AddDays (-1), EventType.AmortizationExtra);
				this.AddAssetAmortizedAmount (e1, 22000.0m, 18000.0m);

				var e2 = this.AddAssetEvent (v2, this.accessor.Mandat.StartDate.AddYears (2).AddDays (-1), EventType.AmortizationExtra);
				this.AddAssetAmortizedAmount (e2, 18000.0m, 10000.0m);
			}

			{
				var e = this.AddAssetEvent (p1, this.accessor.Mandat.StartDate.AddYears (2), EventType.Output);
				this.AddAssetAmortizedAmount (e, 0.0m);
			}
		}


		protected override void AddPersonsSamples()
		{
			this.AddPersonSample ("Monsieur", "Jean",      "Dupond",   null,                 "av. des Planches 12 bis",                     "1023", "Crissier",             "Suisse", null,            null,            null,            "jeandupond@bluewin.ch");
			this.AddPersonSample ("Madame",   "Renata",    "Zumstein", null,                 "Cr�sentine 21",                               "1023", "Crissier",             "Suisse", "021 512 44 55", null,            null,            "zumstein@crissier.ch");
			this.AddPersonSample ("Monsieur", "Alfred",    "Dubosson", null,                 "ch. des Tilleuls 4",                          "1020", "Renens",               "Suisse", "021 512 44 55", "021 600 22 33", null,            "dubosson@crissier.ch");
			this.AddPersonSample ("Madame",   "Sandra",    "Nicolet",  null,                 "Place du March�",                             "2000", "Neuch�tel",            "Suisse", null,            null,            "079 810 20 30", "sandranicolet5@gmail.com");
			this.AddPersonSample ("Madame",   "Sylvianne", "Galbato",  "Les Bons Tuyaux SA", "Z.I. Budron 12A",                             "1052", "Le Mont-sur-Lausanne", "Suisse", "021 312 28 29", null,            null,            "sylvianne@lesbonstuyaux.ch");
			this.AddPersonSample ("Monsieur", "Andr�",     "Mercier",  "Mecatronic SA",      "Y-Parc - Swiss Technopole<br/>Rue Galil�e 7", "1400", "Yverdon-les-Bains",    "Suisse", "024 444 11 22", "022 871 98 76", null,            "mercier@mecatronic.ch");
		}


		protected override void CreateGroupsSamples()
		{
			var root = this.AddGroup (null, "Groupes", null);

			this.CreateGroupsCatsMCH2Samples  (root);
			this.CreateGroupsTypesMCH2Samples (root);
			this.CreateGroupsPatsMCH2Samples  (root);
		}

		private void CreateGroupsCatsMCH2Samples(DataObject parent)
		{
			var root = this.AddGroup (parent, "Cat�gories MCH2", "100");

			          this.AddGroup (root, "Terrains",                "10");
			          this.AddGroup (root, "Routes",                  "15");
			          this.AddGroup (root, "Traitement des eaux",     "20");
			          this.AddGroup (root, "Traveaux de g�nie civil", "30");
			var imm = this.AddGroup (root, "Immeubles",               "40");
			          this.AddGroup (root, "Mobilier",                "45");
			var veh = this.AddGroup (root, "V�hicules",               "50");
			          this.AddGroup (root, "Machines",                "55");
			          this.AddGroup (root, "En construction",         "60");
			          this.AddGroup (root, "Autres immobilisations",  "90");

			this.AddGroup (imm, "B�timents",   "10", groupUsedDuringCreation: true);
			this.AddGroup (imm, "Ecoles",      "20");
			this.AddGroup (imm, "D�p�ts",      "30");
			this.AddGroup (imm, "D�cheteries", "40");

			this.AddGroup (veh, "Camions",      "10");
			this.AddGroup (veh, "Camionnettes", "20");
			this.AddGroup (veh, "Voitures",     "30");
		}

		private void CreateGroupsTypesMCH2Samples(DataObject parent)
		{
			var root = this.AddGroup (parent, "Types MCH2", "200");

			this.AddGroup (root, "Immobilisations corporelles",   "10");
			this.AddGroup (root, "Immobilisations incorporelles", "20");
			this.AddGroup (root, "Immobilisations financi�res",   "30");
		}

		private void CreateGroupsPatsMCH2Samples(DataObject parent)
		{
			var root = this.AddGroup (parent, "Patrimoine MCH2", "300");

			this.AddGroup (root, "Patrimoine administratif", "10", groupUsedDuringCreation: true);
			this.AddGroup (root, "Patrimoine financier",     "20");
		}


		protected override void CreateCatsSamples()
		{
			this.AddCat ("Amortissements manuels",
				"Un objet de cette cat�gorie ne sera jamais amorti automatiquement, car le taux d�fini est nul.",
				"0", 0.0m, AmortizationType.Linear,
				Periodicity.Annual, ProrataType.Prorata12, 1.0m, 1.0m);

			this.AddCat ("Immobilier", null, "10", 0.10m, AmortizationType.Linear,
				Periodicity.Annual, ProrataType.Prorata12, 1000.0m, 1.0m,
				"1000", "1010", "1600", "1600", "6930", "6900");

			this.AddCat ("V�hicules", null, "20", 0.20m, AmortizationType.Degressive,
				Periodicity.Annual, ProrataType.Prorata12,  100.0m, 1.0m,
				"1000", "1010", "1530", "1530", "6920", "6900");
		}
	}
}
