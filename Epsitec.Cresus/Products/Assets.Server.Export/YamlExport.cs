﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Export.Helpers;
using Epsitec.Cresus.Assets.Server.DataFillers;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.Server.Export
{
	/// <summary>
	/// Exportation au format yaml.
	/// </summary>
	public class YamlExport<T> : AbstractExport<T>
		where T : struct
	{
		public override void Export(DataAccessor accessor, ExportFormat format, AbstractExportProfile profile, string filename, AbstractTreeTableFiller<T> filler, ColumnsState columnsState)
		{
			base.Export (accessor, format, profile, filename, filler, columnsState);

			this.FillArray (hasHeader: false);
			var data = this.GetData ();
			this.WriteData (data);
		}


		private string GetData()
		{
			//	Transforme le contenu du tableau en une string.
			var columnDescriptions = this.filler.Columns;
			System.Diagnostics.Debug.Assert (this.columnCount == columnDescriptions.Count ());

			var builder = new System.Text.StringBuilder ();

			builder.Append ("%YAML 1.1");
			builder.Append (this.Profile.FinalEndOfLine);
			builder.Append ("---");
			builder.Append (this.Profile.FinalEndOfLine);
			builder.Append ("# Assets export");
			builder.Append (this.Profile.FinalEndOfLine);

			for (int row=0; row<this.rowCount; row++)
			{
				builder.Append ("-");
				builder.Append (this.Profile.FinalEndOfLine);

				for (int column=0; column<this.columnCount; column++)
				{
					var description = columnDescriptions[column];

					var content = this.GetOutputString (this.array[column, row]);
					if (!string.IsNullOrEmpty (content))
					{
						builder.Append (this.Profile.FinalIndent);
						builder.Append (this.GetTag (description.Header));
						builder.Append (": ");
						builder.Append (content);
						builder.Append (this.Profile.FinalEndOfLine);
					}
				}
			}

			builder.Append ("...");
			builder.Append (this.Profile.FinalEndOfLine);

			return builder.ToString ();
		}


		private void WriteData(string data)
		{
			System.IO.File.WriteAllText (this.filename, data, this.Profile.Encoding);
		}


		private string GetTag(string text)
		{
			if (this.Profile.CamelCase)
			{
				text = text.ToCamelCase ();
			}

			if (string.IsNullOrEmpty (text))
			{
				return null;
			}
			else
			{
				text = text.Replace (" ", "_");
				text = text.Replace (":", "_");
				return text;
			}
		}


		private string GetOutputString(string text)
		{
			if (string.IsNullOrEmpty (text))
			{
				return null;
			}
			else
			{
				text = text.Replace ("\\", "\\\\");  // \ -> \\
				text = text.Replace ("\"", "\\\"");  // " -> \"
				return string.Concat ("\"", text, "\"");
			}
		}

		private YamlExportProfile Profile
		{
			get
			{
				return this.profile as YamlExportProfile;
			}
		}
	}
}