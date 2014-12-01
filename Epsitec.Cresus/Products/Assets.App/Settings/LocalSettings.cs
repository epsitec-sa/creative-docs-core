﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Support;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Data.Helpers;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.DataFillers;
using Epsitec.Cresus.Assets.Server.Export;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Settings
{
	/// <summary>
	/// C'est ici que sont concentrés tous les réglages liés au mandat, à l'utilisateur
	/// et à la UI.
	/// </summary>
	public static class LocalSettings
	{
		static LocalSettings()
		{
			LocalSettings.columnsStates = new Dictionary<string, ColumnsState> ();
			LocalSettings.searchInfos = new Dictionary<SearchKind, SearchInfo> ();
			LocalSettings.createAssetDefaultGroups = new Dictionary<Guid, Guid> ();
			LocalSettings.hiddenWarnings = new HashSet<string> ();

			LocalSettings.Initialize (Timestamp.Now.Date);
		}

		public static void Initialize(System.DateTime defaultDate)
		{
			LocalSettings.columnsStates.Clear ();
			LocalSettings.searchInfos.Clear ();
			LocalSettings.createAssetDefaultGroups.Clear ();

			var defaultTimestamp = new Timestamp (defaultDate, 0);

			LocalSettings.CreateMandatDate     = new System.DateTime (defaultTimestamp.Date.Year, 1, 1);
			LocalSettings.CreateAssetDate      = new System.DateTime (defaultTimestamp.Date.Year, 1, 1);
			LocalSettings.AmortizationDateFrom = new System.DateTime (defaultTimestamp.Date.Year, 1, 1);
			LocalSettings.AmortizationDateTo   = new System.DateTime (defaultTimestamp.Date.Year, 12, 31);
			LocalSettings.LockedDate           = defaultTimestamp.Date;
			LocalSettings.DefaultMandatDate    = defaultTimestamp.Date;
			LocalSettings.DefaultFreeDate      = defaultTimestamp.Date;

			FolderItem item = FileManager.GetFolderItem (FolderId.VirtualMyDocuments, FolderQueryMode.NoIcons);
			var filename = System.IO.Path.Combine (item.FullPath, "export.pdf");
			LocalSettings.ExportInstructions = new ExportInstructions (ExportFormat.Pdf, filename);

			LocalSettings.ExportTxtProfile  = TextExportProfile.TxtProfile;
			LocalSettings.ExportCsvProfile  = TextExportProfile.CsvProfile;
			LocalSettings.ExportXmlProfile  = XmlExportProfile.Default;
			LocalSettings.ExportYamlProfile = YamlExportProfile.Default;
			LocalSettings.ExportJsonProfile = JsonExportProfile.Default;
			LocalSettings.ExportPdfProfile  = PdfExportProfile.Default;

			LocalSettings.AccountsImportFilename = null;

			LocalSettings.SplitterAssetsEventPos    = 190;
			LocalSettings.SplitterAssetsMultiplePos = 180;

			LocalSettings.AccountCategories = AccountCategory.Actif |
											  AccountCategory.Passif |
											  AccountCategory.Charge |
											  AccountCategory.Produit |
											  AccountCategory.Exploitation |
											  AccountCategory.Revenu |
											  AccountCategory.Depense |
											  AccountCategory.Recette;

			LocalSettings.TestExpressionAmount = new AmortizedAmount (AmortizationMethod.Unknown, null,
				0.10m,
				0.0m, 10.0m,
				0.0m,
				Periodicity.Annual,
				null,
				4000.0m,
				4000.0m,
				5000.0m,
				5000.0m,
				0.0m, 0.0m,
				1.0m,
				1.0m,
				EntryScenario.None, System.DateTime.MinValue,
				Guid.Empty, Guid.Empty, Guid.Empty, 0);

			LocalSettings.ExpressionSimulationRange = new DateRange (
				new System.DateTime (2000, 1, 1),
				new System.DateTime (2100, 1, 1));

			LocalSettings.ExpressionSimulationAmount = new AmortizedAmount (AmortizationMethod.Unknown, null,
				0.10m,
				0.0m, 10.0m,
				0.0m,
				Periodicity.Annual,
				null,
				10000.0m,
				10000.0m,
				10000.0m,
				10000.0m,
				null, null,
				1.0m,
				1.0m,
				EntryScenario.None, System.DateTime.MinValue,
				Guid.Empty, Guid.Empty, Guid.Empty, 0);
		}


		#region Columns state
		public static ColumnsState GetColumnsState(string name)
		{
			ColumnsState columnsState;
			if (LocalSettings.columnsStates.TryGetValue (name, out columnsState))
			{
				return columnsState;
			}
			else
			{
				return ColumnsState.Empty;
			}
		}

		public static void SetColumnsState(string name, ColumnsState columnsState)
		{
			LocalSettings.columnsStates[name] = columnsState;
		}
		#endregion


		#region Search info
		public static SearchInfo GetSearchInfo(SearchKind kind)
		{
			SearchInfo info;
			if (LocalSettings.searchInfos.TryGetValue (kind, out info))
			{
				return info;
			}
			else
			{
				return SearchInfo.Default;
			}
		}

		public static void SetSearchInfo(SearchKind kind, SearchInfo info)
		{
			LocalSettings.searchInfos[kind] = info;
		}
		#endregion


		#region Create group
		public static void AddCreateGroup(Guid parentGroupGuid, Guid selectedGroupGuid)
		{
			LocalSettings.createAssetDefaultGroups[parentGroupGuid] = selectedGroupGuid;
		}

		public static Guid GetCreateGroup(Guid parentGroupGuid)
		{
			Guid guid;
			if (LocalSettings.createAssetDefaultGroups.TryGetValue (parentGroupGuid, out guid))
			{
				return guid;
			}
			else
			{
				return Guid.Empty;
			}
		}
		#endregion


		#region Hidden warnings
		public static IEnumerable<Warning> GetVisibleWarnings(DataAccessor accessor)
		{
			//	Retourne les avertissements, en masquant ceux qui sont cachés par l'utilisateur.
			return WarningsLogic.GetWarnings (accessor)
				.Where (x => !LocalSettings.IsHiddenWarnings (x.PersistantUniqueId));
		}

		public static void ClearHiddenWarnings()
		{
			LocalSettings.hiddenWarnings.Clear ();
		}

		public static void AddHiddenWarnings(string persistantUniqueId)
		{
			LocalSettings.hiddenWarnings.Add (persistantUniqueId);
		}

		public static bool IsHiddenWarnings(string persistantUniqueId)
		{
			return LocalSettings.hiddenWarnings.Contains (persistantUniqueId);
		}

		public static bool HasHiddenWarnings()
		{
			return LocalSettings.hiddenWarnings.Any ();
		}
		#endregion


		#region Serialize
		public static void Serialize(System.Xml.XmlWriter writer)
		{
			writer.WriteStartDocument ();
			writer.WriteStartElement ("LocalSettings");

			writer.WriteElementString ("DocumentVersion", DataMandat.DocumentVersion);

			LocalSettings.SerializeColumnsState (writer);
			LocalSettings.SerializeSearchInfo (writer);
			LocalSettings.SerializeCreateAssetDefaultGroups (writer);
			LocalSettings.SerializeHiddenWarnings (writer);

			writer.WriteElementString ("CreateMandatDate",     LocalSettings.CreateMandatDate.ToStringIO ());
			writer.WriteElementString ("CreateAssetDate",      LocalSettings.CreateAssetDate.ToStringIO ());
			writer.WriteElementString ("AmortizationDateFrom", LocalSettings.AmortizationDateFrom.ToStringIO ());
			writer.WriteElementString ("AmortizationDateTo",   LocalSettings.AmortizationDateTo.ToStringIO ());
			writer.WriteElementString ("LockedDate",           LocalSettings.LockedDate.ToStringIO ());
			writer.WriteElementString ("DefaultMandatDate",    LocalSettings.DefaultMandatDate.ToStringIO ());
			writer.WriteElementString ("DefaultFreeDate",      LocalSettings.DefaultFreeDate.ToStringIO ());

			LocalSettings.ExportInstructions.Serialize (writer, "ExportInstructions");
			LocalSettings.ExportTxtProfile  .Serialize (writer, "ExportTxtProfile");
			LocalSettings.ExportCsvProfile  .Serialize (writer, "ExportCsvProfile");
			LocalSettings.ExportXmlProfile  .Serialize (writer, "ExportXmlProfile");
			LocalSettings.ExportYamlProfile .Serialize (writer, "ExportYamlProfile");
			LocalSettings.ExportJsonProfile .Serialize (writer, "ExportJsonProfile");
			LocalSettings.ExportPdfProfile  .Serialize (writer, "ExportPdfProfile");

			writer.WriteElementString ("AccountsImportFilename", LocalSettings.AccountsImportFilename);

			writer.WriteElementString ("SplitterAssetsEventPos",    LocalSettings.SplitterAssetsEventPos.ToStringIO ());
			writer.WriteElementString ("SplitterAssetsMultiplePos", LocalSettings.SplitterAssetsMultiplePos.ToStringIO ());

			writer.WriteElementString ("AccountCategories", LocalSettings.AccountCategories.ToStringIO ());

			writer.WriteEndElement ();
			writer.WriteEndDocument ();
		}

		private static void SerializeColumnsState(System.Xml.XmlWriter writer)
		{
			var columnsStates = LocalSettings.columnsStates.Where (x => !x.Value.IsEmpty).ToArray ();

			if (columnsStates.Any ())
			{
				writer.WriteStartElement ("ColumnsState");

				foreach (var pair in columnsStates)
				{
					pair.Value.Serialize (writer, pair.Key);
				}

				writer.WriteEndElement ();
			}
		}

		private static void SerializeSearchInfo(System.Xml.XmlWriter writer)
		{
			if (LocalSettings.searchInfos.Any ())
			{
				writer.WriteStartElement ("SearchInfo");

				foreach (var pair in LocalSettings.searchInfos)
				{
					pair.Value.Serialize (writer, pair.Key.ToStringIO ());
				}

				writer.WriteEndElement ();
			}
		}

		private static void SerializeCreateAssetDefaultGroups(System.Xml.XmlWriter writer)
		{
			if (LocalSettings.createAssetDefaultGroups.Any ())
			{
				writer.WriteStartElement ("CreateAssetDefaultGroups");

				foreach (var pair in LocalSettings.createAssetDefaultGroups)
				{
					writer.WriteStartElement ("Group");

					IOHelpers.WriteStringAttribute (writer, "Parent",    pair.Key  .ToStringIO ());
					IOHelpers.WriteStringAttribute (writer, "Selection", pair.Value.ToStringIO ());

					writer.WriteEndElement ();
				}

				writer.WriteEndElement ();
			}
		}

		private static void SerializeHiddenWarnings(System.Xml.XmlWriter writer)
		{
			if (LocalSettings.hiddenWarnings.Any ())
			{
				writer.WriteStartElement ("HiddenWarnings");

				foreach (var persistantUniqueId in LocalSettings.hiddenWarnings)
				{
					writer.WriteStartElement ("Warning");
					IOHelpers.WriteStringAttribute (writer, "PersistantUniqueId", persistantUniqueId);
					writer.WriteEndElement ();
				}

				writer.WriteEndElement ();
			}
		}
		#endregion


		#region Deserialization
		public static void Deserialize(System.Xml.XmlReader reader)
		{
			while (reader.Read ())
			{
				if (reader.NodeType == System.Xml.XmlNodeType.Element)
				{
					if (reader.Name == "LocalSettings")
					{
						LocalSettings.DeserializeLocalSettings (reader);
					}
				}
				else if (reader.NodeType == System.Xml.XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		private static void DeserializeLocalSettings(System.Xml.XmlReader reader)
		{
			while (reader.Read ())
			{
				if (reader.NodeType == System.Xml.XmlNodeType.Element)
				{
					switch (reader.Name)
					{
						case "DocumentVersion":
							var version = reader.ReadElementContentAsString ();
							break;

						case "ColumnsState":
							LocalSettings.DeserializeColumnsState (reader);
							break;

						case "SearchInfo":
							LocalSettings.DeserializeSearchInfo (reader);
							break;

						case "CreateAssetDefaultGroups":
							LocalSettings.DeserializeCreateAssetDefaultGroups (reader);
							break;

						case "HiddenWarnings":
							LocalSettings.DeserializeHiddenWarnings (reader);
							break;

						case "CreateMandatDate":
							LocalSettings.CreateMandatDate = reader.ReadElementContentAsString ().ParseDate ();
							break;

						case "CreateAssetDate":
							LocalSettings.CreateAssetDate = reader.ReadElementContentAsString ().ParseDate ();
							break;

						case "AmortizationDateFrom":
							LocalSettings.AmortizationDateFrom = reader.ReadElementContentAsString ().ParseDate ();
							break;

						case "AmortizationDateTo":
							LocalSettings.AmortizationDateTo = reader.ReadElementContentAsString ().ParseDate ();
							break;

						case "LockedDate":
							LocalSettings.LockedDate = reader.ReadElementContentAsString ().ParseDate ();
							break;

						case "DefaultMandatDate":
							LocalSettings.DefaultMandatDate = reader.ReadElementContentAsString ().ParseDate ();
							break;

						case "DefaultFreeDate":
							LocalSettings.DefaultFreeDate = reader.ReadElementContentAsString ().ParseDate ();
							break;

						case "ExportInstructions":
							LocalSettings.ExportInstructions = new ExportInstructions (reader);
							break;

						case "ExportTxtProfile":
							LocalSettings.ExportTxtProfile = new TextExportProfile (reader);
							break;

						case "ExportCsvProfile":
							LocalSettings.ExportCsvProfile = new TextExportProfile (reader);
							break;

						case "ExportXmlProfile":
							LocalSettings.ExportXmlProfile = new XmlExportProfile (reader);
							break;

						case "ExportYamlProfile":
							LocalSettings.ExportYamlProfile = new YamlExportProfile (reader);
							break;

						case "ExportJsonProfile":
							LocalSettings.ExportJsonProfile = new JsonExportProfile (reader);
							break;

						case "ExportPdfProfile":
							LocalSettings.ExportPdfProfile = new PdfExportProfile (reader);
							break;

						case "AccountsImportFilename":
							LocalSettings.AccountsImportFilename = reader.ReadElementContentAsString ();
							break;

						case "SplitterAssetsEventPos":
							LocalSettings.SplitterAssetsEventPos = reader.ReadElementContentAsString ().ParseInt ();
							break;

						case "SplitterAssetsMultiplePos":
							LocalSettings.SplitterAssetsMultiplePos = reader.ReadElementContentAsString ().ParseInt ();
							break;

						case "AccountCategories":
							LocalSettings.AccountCategories = (AccountCategory) reader.ReadElementContentAsString ().ParseType (typeof (AccountCategory));
							break;
					}
				}
				else if (reader.NodeType == System.Xml.XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		private static void DeserializeColumnsState(System.Xml.XmlReader reader)
		{
			LocalSettings.columnsStates.Clear ();

			while (reader.Read ())
			{
				if (reader.NodeType == System.Xml.XmlNodeType.Element)
				{
					var c = new ColumnsState (reader);
					LocalSettings.columnsStates.Add (reader.Name, c);
				}
				else if (reader.NodeType == System.Xml.XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		private static void DeserializeSearchInfo(System.Xml.XmlReader reader)
		{
			LocalSettings.searchInfos.Clear ();

			while (reader.Read ())
			{
				if (reader.NodeType == System.Xml.XmlNodeType.Element)
				{
					var c = new SearchInfo (reader);
					var k = (SearchKind) IOHelpers.ParseType (reader.Name, typeof (SearchKind));
					LocalSettings.searchInfos.Add (k, c);
				}
				else if (reader.NodeType == System.Xml.XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		private static void DeserializeCreateAssetDefaultGroups(System.Xml.XmlReader reader)
		{
			LocalSettings.searchInfos.Clear ();

			while (reader.Read ())
			{
				if (reader.NodeType == System.Xml.XmlNodeType.Element)
				{
					var k = IOHelpers.ParseGuid (reader["Parent"]);
					var v = IOHelpers.ParseGuid (reader["Selection"]);
					LocalSettings.createAssetDefaultGroups.Add (k, v);
				}
				else if (reader.NodeType == System.Xml.XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		private static void DeserializeHiddenWarnings(System.Xml.XmlReader reader)
		{
			LocalSettings.hiddenWarnings.Clear ();

			while (reader.Read ())
			{
				if (reader.NodeType == System.Xml.XmlNodeType.Element)
				{
					LocalSettings.hiddenWarnings.Add (reader["PersistantUniqueId"]);
				}
				else if (reader.NodeType == System.Xml.XmlNodeType.EndElement)
				{
					break;
				}
			}
		}
		#endregion


		public static System.DateTime				CreateMandatDate;
		public static System.DateTime				CreateAssetDate;
		public static System.DateTime				AmortizationDateFrom;
		public static System.DateTime				AmortizationDateTo;
		public static System.DateTime				LockedDate;
		public static System.DateTime				DefaultMandatDate;
		public static System.DateTime				DefaultFreeDate;

		public static ExportInstructions			ExportInstructions;
		public static TextExportProfile				ExportTxtProfile;
		public static TextExportProfile				ExportCsvProfile;
		public static XmlExportProfile				ExportXmlProfile;
		public static YamlExportProfile				ExportYamlProfile;
		public static JsonExportProfile				ExportJsonProfile;
		public static PdfExportProfile				ExportPdfProfile;
		public static string						AccountsImportFilename;

		public static int							SplitterAssetsEventPos;
		public static int							SplitterAssetsMultiplePos;

		public static AccountCategory				AccountCategories;

		public static AmortizedAmount				TestExpressionAmount;
		public static DateRange						ExpressionSimulationRange;
		public static AmortizedAmount				ExpressionSimulationAmount;

		private static readonly Dictionary<string, ColumnsState> columnsStates;
		private static readonly Dictionary<SearchKind, SearchInfo> searchInfos;
		private static readonly Dictionary<Guid, Guid> createAssetDefaultGroups;
		private static readonly HashSet<string>		hiddenWarnings;
	}
}
