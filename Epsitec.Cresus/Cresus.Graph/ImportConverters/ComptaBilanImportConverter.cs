﻿//	Copyright © 2009, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Graph.Data;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Graph.ImportConverters
{
	/// <summary>
	/// The <c>ComptaBilanImportConverter</c> class converts from the
	/// Crésus Comptabilité report named "Bilan".
	/// </summary>
	[Importer ("compta:blian")]
	public class ComptaBilanImportConverter : Compta
	{
		public ComptaBilanImportConverter(string name)
			: base (name, "4")
		{
		}

		
		public override GraphDataCube ToDataCube(IList<string> header, IEnumerable<IEnumerable<string>> lines, string sourcePath)
		{
			if (header.Count < 4)
			{
				return null;
			}

			var cube  = new GraphDataCube ();

			string colDimension = "Solde";
			string rowDimension = "Numéro/Compte";
			string[] sources = new string[3];

			sources[0] = Compta.GetSourceName (sourcePath);
			sources[1] = "Précédent";
			sources[2] = "Budget";

			int sourceYear;
			
			if (int.TryParse (sources[0], System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out sourceYear))
            {
				sources[1] = (sourceYear-1).ToString (System.Globalization.CultureInfo.InvariantCulture);
            }

			int column = 2;

			foreach (var sourceName in GraphDataSet.CreateNumberedLabels (sources, index => 2-index))
			{
				var table = new DataTable ();

				table.DimensionVector.Add ("Source", sourceName);
				table.DefineColumnLabels (new string[] { "Valeur" });
				table.ColumnDimensionKey = colDimension;
				table.RowDimensionKey    = rowDimension;

				foreach (var line in lines)
				{
					var item0 = line.ElementAt (0);
					var item1 = line.ElementAt (1);

					if (string.IsNullOrEmpty (item0) ||
					string.IsNullOrEmpty (item1))
					{
						continue;
					}

					int start = 0;

					while (start < item1.Length)
					{
						char c = item1[start];

						if ((c != ' ') &&
							(c != '>'))
						{
							break;
						}

						start++;
					}

					item1 = item1.Substring (start);

					if (string.IsNullOrEmpty (item1))
					{
						continue;
					}

					string label = string.Concat (item0, "\t", item1);
					string value = line.ElementAt (column);
					
					table.Add (label, new double?[] { GraphDataSet.GetNumericValue (string.IsNullOrEmpty (value) ? "0" : value) });
				}

				cube.AddTable (table);

				column++;
			}

			return cube;
		}

		public override string DataTitle
		{
			get
			{
				return "Compta – Bilan";
			}
		}
	}
}
