﻿//	Copyright © 2015, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Samuel LOUP, Maintainer: Samuel LOUP

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Epsitec.Data.Platform.MatchSort.Mapping;
using Epsitec.Data.Platform.MatchSort.Rows;

namespace Epsitec.Data.Platform.MatchSort
{
	public sealed class MatchSortLoader
	{
		public static MatchSortMetaData GetFileMetaData(string csvFilePath)
		{
			if (System.IO.File.Exists (csvFilePath) == false)
			{
				throw new System.Exception ("The MAT[CH]sort file does not exist at path " + csvFilePath);
			}

			return MatchSortLoader.LoadRecords<MatchSortMetaData> (csvFilePath, "00").SingleOrDefault ();
		}

		public static IEnumerable<T> LoadRecords<T> (string csvFilePath, string recordType)
		{
			var records = new List<T> ();
			using (var stream = File.OpenText (csvFilePath))
			{
				var csv   = new CsvReader (stream, MatchSortLoader.ConfigureReader ());
				while (csv.Read ())
				{
					if(recordType == csv.GetField (0))
					{
						records.Add (csv.GetRecord<T> ());
					}
				}
			}

			return records;
		}

		private static CsvConfiguration ConfigureReader()
		{
			var config = new CsvConfiguration ();
			config.Encoding = System.Text.Encoding.GetEncoding ("Windows-1252");
			config.Delimiter = ";";
			config.HasHeaderRecord = false;
			config.IgnoreQuotes = true;
			config.RegisterClassMap<MatchSortMetaDataMap> ();
			config.RegisterClassMap<SwissPostZipInformationMap> ();
			config.RegisterClassMap<SwissPostStreetInformationMap> ();
			return config;
		}
	}
}
