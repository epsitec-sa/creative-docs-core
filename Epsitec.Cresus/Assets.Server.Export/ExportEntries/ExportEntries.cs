﻿//	Copyright © 2013-2015, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Core.Helpers;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Data.Helpers;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.Server.Export
{
	public class ExportEntries : System.IDisposable
	{
		public ExportEntries(DataAccessor accessor)
		{
			this.accessor = accessor;
			this.eccLines = new List<EccLine> ();
		}

		public void Dispose()
		{
		}


		public int ExportFile(string filename)
		{
			this.filename = filename;

			this.ReadEcc ();

			var data = this.GetExportData (ExportEntries.uid);
			System.IO.File.WriteAllText (this.EcfFilename, data, System.Text.Encoding.Unicode);

			return this.entryCount;
		}


		private string GetExportData(int uid)
		{
			var builder = new System.Text.StringBuilder ();

			builder.Append ("#FSC 7.0 ECF\r\n");

			builder.Append ("#ECC 1;");
			builder.Append (TypeConverters.IntToString (1));  // nlot
			builder.Append (";");
			builder.Append (TypeConverters.DateToString (Timestamp.Now.Date));
			builder.Append (";");
			builder.Append (uid.ToStringIO ());  // uid
			builder.Append ("\r\n");

			var entries = this.accessor.Mandat.GetData (BaseType.Entries);

			this.entryCount = 0;
			int idno = 1;
			foreach (var entry in entries)
			{
				var date    = ObjectProperties.GetObjectPropertyDate    (entry, null, ObjectField.EntryDate);
				var debit   = ObjectProperties.GetObjectPropertyString  (entry, null, ObjectField.EntryDebitAccount);
				var credit  = ObjectProperties.GetObjectPropertyString  (entry, null, ObjectField.EntryCreditAccount);
				var stamp   = ObjectProperties.GetObjectPropertyString  (entry, null, ObjectField.EntryStamp);
				var title   = ObjectProperties.GetObjectPropertyString  (entry, null, ObjectField.EntryTitle);
				var value   = ObjectProperties.GetObjectPropertyDecimal (entry, null, ObjectField.EntryAmount);
				var vatCode = ObjectProperties.GetObjectPropertyString  (entry, null, ObjectField.EntryVatCode);

				if (vatCode.Length == 1)  // pas de code TVA (par exemple "-") ?
				{
					vatCode = null;
				}

				if (value.HasValue && value.Value != 0.0m)
				{
					builder.Append (TypeConverters.DateToString (date));
					builder.Append ("\t");
					builder.Append (debit);
					builder.Append ("\t");
					builder.Append (credit);
					builder.Append ("\t");
					builder.Append (stamp);
					builder.Append ("\t");
					builder.Append (title);
					builder.Append ("\t");
					builder.Append (value.Value.ToStringIO ());
					builder.Append ("\t");
					builder.Append ("0");  // montant_me
					builder.Append ("\t");
					builder.Append ("0");  // cours
					builder.Append ("\t");
					builder.Append ("0");  // nmult
					builder.Append ("\t");
					builder.Append ("0");  // net
					builder.Append ("\t");
					builder.Append ("0");  // typtva
					builder.Append ("\t");
					builder.Append ("0");  // notva
					builder.Append ("\t");
					builder.Append ("0");  // unused
					builder.Append ("\t");
					builder.Append ((idno++).ToStringIO ());  // idno
					builder.Append ("\t");
					builder.Append (vatCode);
					builder.Append ("\r\n");

					this.entryCount++;
				}
			}

			return builder.ToString ();
		}


		private void ReadEcc()
		{
			this.eccLines.Clear ();

			var lines = System.IO.File.ReadAllLines (this.EccFilename);
			foreach (var line in lines)
			{
				var ecc = new EccLine (line);
				this.eccLines.Add (ecc);
			}
		}

		private void WriteEcc()
		{
			System.IO.File.WriteAllLines (this.EccFilename, this.eccLines.Select (x => x.Line));
		}


		private struct EccLine
		{
			public EccLine(string line)
			{
				this.OriginalLine = line;

				var x = line.Split ('\t');

				this.Tag      = x[0];
				this.N        = null;
				this.Date     = null;
				this.Filename = null;
				this.Uid      = null;

				if (x.Length == 2)
				{
					var y = x[1].Split (new string[] { "; " }, System.StringSplitOptions.None);
					if (y.Length == 4)
					{
						this.N        = y[0];
						this.Date     = y[1];
						this.Filename = y[2];
						this.Uid      = y[3];
					}
				}
			}

			public string Line
			{
				get
				{
					if (this.IsBody)
					{
						return string.Concat (this.Tag, "\t", this.N, "; ", this.Date, "; ", this.Filename, "; ", this.Uid);
					}
					else
					{
						return this.OriginalLine;
					}
				}
			}

			public bool IsBody
			{
				get
				{
					return !string.IsNullOrEmpty (this.N)
						&& !string.IsNullOrEmpty (this.Date)
						&& !string.IsNullOrEmpty (this.Filename)
						&& !string.IsNullOrEmpty (this.Uid);
				}
			}

			public string OriginalLine;
			public string Tag;
			public string N;
			public string Date;
			public string Filename;
			public string Uid;
		}


		private string EccFilename
		{
			get
			{
				var dir = System.IO.Path.GetDirectoryName (this.filename);
				var name = System.IO.Path.GetFileNameWithoutExtension (this.filename);
				return System.IO.Path.Combine (dir, name + ".ecc");
			}
		}

		private string EcfFilename
		{
			get
			{
				var dir = System.IO.Path.GetDirectoryName (this.filename);
				var name = this.accessor.Mandat.Name;
				return System.IO.Path.Combine (dir, name + ".ecf");
			}
		}


		private const int uid = 123456;

		private readonly DataAccessor			accessor;
		private readonly List<EccLine>			eccLines;

		private string							filename;
		private int								entryCount;
	}
}