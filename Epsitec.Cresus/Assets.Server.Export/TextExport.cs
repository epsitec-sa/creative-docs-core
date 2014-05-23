﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Core.Helpers;
using Epsitec.Cresus.Assets.Server.DataFillers;

namespace Epsitec.Cresus.Assets.Server.Export
{
	/// <summary>
	/// Exportation au format txt ou csv.
	/// </summary>
	public class TextExport<T> : AbstractExport<T>
		where T : struct
	{
		public TextExport()
		{
			this.Profile = TextExportProfile.TxtProfile;
		}


		public TextExportProfile				Profile;


		public override void Export(AbstractTreeTableFiller<T> filler, ExportInstructions instructions)
		{
			this.instructions = instructions;

			var data = this.GetData (filler);
			this.WriteData (data);
		}


		private string GetData(AbstractTreeTableFiller<T> filler)
		{
			var columnDescriptions = filler.Columns;

			int columnCount = columnDescriptions.Count ();
			int rowCount = filler.Count;

			var array = new string[columnCount, this.RowOffet+rowCount];

			//	Génère la première ligne d'en-tête.
			if (this.instructions.HasHeader)
			{
				for (int column=0; column<columnCount; column++)
				{
					var description = columnDescriptions[column];
					array[column, 0] = description.Header;
				}
			}

			//	Génère tout le contenu.
			for (int row=0; row<rowCount; row++)
			{
				var contentItem = filler.GetContent (row, 1, -1);  // toutes les colonnes d'une ligne

				for (int column=0; column<columnCount; column++)
				{
					var columnItem = contentItem.Columns[column];
					var cell = columnItem.Cells.First ();
					var description = columnDescriptions[column];
					array[column, this.RowOffet+row] = this.ConvertToString (cell, description);
				}
			}

			//	Transforme le contenu du tableau en une string.
			var builder = new System.Text.StringBuilder ();

			if (this.instructions.Inverted)
			{
				for (int column=0; column<columnCount; column++)
				{
					for (int row=0; row<this.RowOffet+rowCount; row++)
					{
						builder.Append (this.GetOutputString (array[column, row]));

						if (row < rowCount-1)
						{
							builder.Append (this.Profile.ColumnSeparator);
						}
					}

					builder.Append (this.Profile.EndOfLine);
				}
			}
			else
			{
				for (int row=0; row<this.RowOffet+rowCount; row++)
				{
					for (int column=0; column<columnCount; column++)
					{
						builder.Append (this.GetOutputString (array[column, row]));

						if (column < columnCount-1)
						{
							builder.Append (this.Profile.ColumnSeparator);
						}
					}

					builder.Append (this.Profile.EndOfLine);
				}
			}

			return builder.ToString ();
		}

		private int RowOffet
		{
			get
			{
				return this.instructions.HasHeader ? 1 : 0;
			}
		}


		private void WriteData(string data)
		{
			System.IO.File.WriteAllText (this.instructions.Filename, data);
		}


		private string GetOutputString(string text)
		{
			if (string.IsNullOrEmpty (this.Profile.ColumnBracket))
			{
				if (string.IsNullOrEmpty (text))
				{
					return null;
				}
				else
				{
					if (this.Profile.ColumnSeparator.Length == 1)
					{
						return TextExport<T>.GetEscaped (text, this.Profile.ColumnSeparator[0], this.Profile.Escape);
					}
					else
					{
						return text;
					}
				}
			}
			else
			{
				if (string.IsNullOrEmpty (text))
				{
					return null;
				}
				else
				{
					//	Remplace " par "".
					text = text.Replace (this.Profile.ColumnBracket, this.Profile.ColumnBracket+this.Profile.ColumnBracket);

					//	A partir de Toto, retourne "Toto".
					return string.Concat (this.Profile.ColumnBracket, text, this.Profile.ColumnBracket);
				}
			}
		}

		private static string GetEscaped(string text, char separator, string escape)
		{
			var builder = new System.Text.StringBuilder ();

			foreach (char c in text)
			{
				if (c == separator)
				{
					builder.Append (escape);
				}

				builder.Append (c);
			}

			return builder.ToString ();
		}


		private string ConvertToString(AbstractTreeTableCell cell, TreeTableColumnDescription description)
		{
			if (cell is TreeTableCellAmortizedAmount)
			{
				return this.BaseConvertToString (cell as TreeTableCellAmortizedAmount, description);
			}
			else if (cell is TreeTableCellComputedAmount)
			{
				return this.BaseConvertToString (cell as TreeTableCellComputedAmount, description);
			}
			else if (cell is TreeTableCellDate)
			{
				return this.BaseConvertToString (cell as TreeTableCellDate, description);
			}
			else if (cell is TreeTableCellDecimal)
			{
				return this.BaseConvertToString (cell as TreeTableCellDecimal, description);
			}
			else if (cell is TreeTableCellInt)
			{
				return this.BaseConvertToString (cell as TreeTableCellInt, description);
			}
			else if (cell is TreeTableCellString)
			{
				return (cell as TreeTableCellString).Value;
			}
			else if (cell is TreeTableCellTree)
			{
				return this.BaseConvertToString (cell as TreeTableCellTree, description);
			}
			else
			{
				return null;
			}
		}

		private string BaseConvertToString(TreeTableCellAmortizedAmount cell, TreeTableColumnDescription description)
		{
			if (cell.Value.HasValue)
			{
				return TypeConverters.AmountToString (cell.Value.Value.FinalAmortizedAmount);
			}
			else
			{
				return null;
			}
		}

		private string BaseConvertToString(TreeTableCellComputedAmount cell, TreeTableColumnDescription description)
		{
			if (cell.Value.HasValue)
			{
				return TypeConverters.AmountToString (cell.Value.Value.FinalAmount);
			}
			else
			{
				return null;
			}
		}

		private string BaseConvertToString(TreeTableCellDate cell, TreeTableColumnDescription description)
		{
			return TypeConverters.DateToString (cell.Value);
		}

		private string BaseConvertToString(TreeTableCellDecimal cell, TreeTableColumnDescription description)
		{
			switch (description.Type)
			{
				case TreeTableColumnType.Rate:
					return TypeConverters.RateToString (cell.Value);

				case TreeTableColumnType.Amount:
					return TypeConverters.AmountToString (cell.Value);

				default:
					return TypeConverters.DecimalToString (cell.Value);
			}
		}

		private string BaseConvertToString(TreeTableCellInt cell, TreeTableColumnDescription description)
		{
			return TypeConverters.IntToString (cell.Value);
		}

		private string BaseConvertToString(TreeTableCellTree cell, TreeTableColumnDescription description)
		{
			return cell.Value;
		}


		private ExportInstructions instructions;
	}
}