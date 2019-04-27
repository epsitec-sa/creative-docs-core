//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Core.Helpers;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Data.Reports;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.NodeGetters;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.Server.DataFillers
{
	public class MCH2SummaryTreeTableFiller : AbstractTreeTableFiller<SortableCumulNode>
	{
		public MCH2SummaryTreeTableFiller(DataAccessor accessor, INodeGetter<SortableCumulNode> nodeGetter)
			: base (accessor, nodeGetter)
		{
			this.userColumns = new List<UserColumn> ();
			this.InitializeUserColumns ();

			this.visibleRows    = new HashSet<int> ();
			this.visibleColumns = new HashSet<Column> ();
		}


		public DateRange						DateRange;
		public MCH2SummaryType					SummaryType;


		public override SortingInstructions		DefaultSorting
		{
			get
			{
				return new SortingInstructions (this.MainStringField, SortedType.Ascending, ObjectField.Unknown, SortedType.None);
			}
		}

		public override int						DefaultDockToLeftCount
		{
			get
			{
				return 1;
			}
		}

		public IEnumerable<ExtractionInstructionsArray> UsedExtractionInstructionsArray
		{
			//	Retourne la liste des instructions d'extraction, dans lesquelles on
			//	retrouve InitialTimestamp et FinalTimestamp.
			get
			{
				return this.OrderedColumns.Select (x => this.GetExtractionInstructions (x)).Where (x => !x.IsEmpty);
			}
		}

		public HashSet<int>						VisibleRows
		{
			get
			{
				return this.visibleRows;
			}
		}

		public override TreeTableColumnDescription[] Columns
		{
			get
			{
				var columns = new List<TreeTableColumnDescription> ();

				foreach (var column in this.ExistingOrderedColumns)
				{
					ObjectField field;

					if (column == Column.Name)
					{
						field = this.MainStringField;
					}
					else
					{
						if (column < Column.User)
						{
							field = ObjectField.MCH2Report + (int) column;
						}
						else
						{
							var userColumn = this.userColumns[(column-Column.User)];
							field = userColumn.Field;
						}
					}

					var type    = this.GetColumnType    (column);
					var width   = this.GetColumnWidth   (column);
					var name    = this.GetColumnName    (column);
					var tooltip = this.GetColumnTooltip (column);

					columns.Add (new TreeTableColumnDescription (field, type, width, name, tooltip));
				}

				return columns.ToArray ();
			}
		}

		public override TreeTableContentItem GetContent(int firstRow, int count, int selection)
		{
			var content = new TreeTableContentItem ();

			foreach (var column in this.ExistingOrderedColumns)
			{
				content.Columns.Add (new TreeTableColumnItem ());
			}

			for (int i=0; i<count; i++)
			{
				if (firstRow+i >= this.nodeGetter.Count)
				{
					break;
				}

				var node     = this.nodeGetter[firstRow+i];
				var guid     = node.Guid;
				var baseType = node.BaseType;
				var level    = node.Level;
				var type     = node.Type;

				var obj = this.accessor.GetObject (baseType, guid);

				var cellState1 = (i == selection) ? CellState.Selected : CellState.None;
				var cellState2 = cellState1 | (type == NodeType.Final ? CellState.None : CellState.Unavailable);

				int columnRank = 0;
				foreach (var column in this.ExistingOrderedColumns)
				{
					AbstractTreeTableCell cell;

					if (column == Column.Name)
					{
						string text;

						if (baseType == BaseType.Groups)
						{
							var name = ObjectProperties.GetObjectPropertyString (obj, this.Timestamp, ObjectField.Name, inputValue: true);
							var number = GroupsLogic.GetFullNumber (this.accessor, node.Guid);
							text = GroupsLogic.GetDescription (name, number);
						}
						else
						{
							text = ObjectProperties.GetObjectPropertyString (obj, this.Timestamp, this.MainStringField, inputValue: true);
						}

						cell = new TreeTableCellTree (level, type, text, cellState1);
					}
					else
					{
						var value = this.GetColumnValue (node, obj, column);
						var columnType = this.GetColumnType (column);

						switch (columnType)
						{
							case TreeTableColumnType.Date:
								{
									var v = value as DateCumulValue;
									if (v != null && v.IsRange)
									{
										cell = new TreeTableCellString ("...", cellState2);
									}
									else
									{
										cell = new TreeTableCellDate ((v == null) ? null : v.MinValue, cellState2);
									}
								}
								break;

							case TreeTableColumnType.String:
								{
									var v = value as StringCumulValue;
									cell = new TreeTableCellString ((v == null) ? null : v.Value, cellState2);
								}
								break;

							case TreeTableColumnType.Amount:
								{
									var v = value as DecimalCumulValue;
									cell = new TreeTableCellDecimal ((v == null) ? null : v.Value, cellState2);
								}
								break;

							default:
								throw new System.InvalidOperationException (string.Format ("Unknown TreeTableColumnType {0}", columnType.ToString ()));
						}
					}

					content.Columns[columnRank++].AddRow (cell);
				}
			}

			return content;
		}


		public void ClearVisibleData()
		{
			//	Rend toutes les lignes et les colonnes visibles.
			this.visibleRows.Clear ();
			this.visibleColumns.Clear ();

			this.NodeGetter.ClearSkipHiddenRows ();
		}

		public void ComputeVisibleData()
		{
			//	Cherche toutes les lignes et les colonnes visibles.
			this.ClearVisibleData  ();

			//	Ajoute toutes les lignes et les colonnes directement visibles.
			for (int row=0; row<this.nodeGetter.Count; row++)
			{
				var node     = this.nodeGetter[row];
				var guid     = node.Guid;
				var baseType = node.BaseType;

				var obj = this.accessor.GetObject (baseType, guid);

				foreach (var column in this.OrderedColumns)
				{
					var value = this.GetColumnValue (node, obj, column);

					if (value != null && value.IsExist)
					{
						this.visibleRows   .Add (row);
						this.visibleColumns.Add (column);
					}
				}
			}

			//	Ajoute tous les parents des lignes directement visibles.
			var parentRows = new HashSet<int> ();

			foreach (int row in this.visibleRows)
			{
				int level = this.nodeGetter[row].Level;

				int r = row;
				while (--r >= 0 && level >= 0)
				{
					if (this.nodeGetter[r].Level == level-1)
					{
						parentRows.Add (r);
						level--;
					}
				}
			}

			foreach (int row in parentRows)
			{
				this.visibleRows.Add (row);
			}
		}


		private AbstractCumulValue GetColumnValue(SortableCumulNode node, DataObject obj, Column column)
		{
			//	Calcule la valeur d'une colonne.
			ObjectField field;

			if (column < Column.User)
			{
				field = ObjectField.MCH2Report + (int) column;
			}
			else  // colonne supplémentaire ?
			{
				var userColumn = this.userColumns[(column-Column.User)];
				field = userColumn.Field;
			}

			//	Pour obtenir la valeur, il faut procéder avec le NodeGetter,
			//	pour tenir compte des cumuls (lorsque des lignes sont compactées).
			return this.NodeGetter.GetValue (obj, node, field);
		}


		private ExtractionInstructionsArray GetExtractionInstructions(Column column)
		{
			//	Retourne les instructions d'extraction permettant de peupler une colonne.
			if (column >= Column.User)
			{
				var userColumn = this.userColumns[(column-Column.User)];

				return new ExtractionInstructionsArray (userColumn.Field,
					new ExtractionInstructions (ExtractionAmount.UserColumn,
						new DateRange (System.DateTime.MinValue, this.DateRange.ExcludeTo.Date.AddTicks (-1)),
						false, null, null)
						);
			}

			var field = ObjectField.MCH2Report + (int) column;

			switch (column)
			{
				case Column.InitialState:
					//	Avec une période du 01.01.2014 au 31.12.2014, on cherche l'état avant
					//	le premier janvier, donc au 31.12.2013 23:59:59.
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (
						ExtractionAmount.StateAt,
						new DateRange (System.DateTime.MinValue, this.DateRange.IncludeFrom),
						false)
						);

				case Column.PreInputs:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						this.DateRange,
						false,
						new EventType[] { EventType.PreInput })
						);

				case Column.ReplacementValues:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.LastFiltered,
						new DateRange (System.DateTime.MinValue, this.DateRange.IncludeFrom),
						false,
						new EventType[] { EventType.Input })
						);

				case Column.Inputs:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						this.DateRange,
						false,
						new EventType[] { EventType.Input })
						);

				case Column.PostPreInputs:
					//	Si la période contient un événement d'entrée Input, il faut considérer les
					//	financements préalables comme nuls.
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						new DateRange (System.DateTime.MinValue, this.DateRange.IncludeFrom),
						false,
						filteredEventTypes: new EventType[] { EventType.PreInput },
						stoppedEventTypes:  new EventType[] { EventType.Input })
						);

				case Column.PostDecreases:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						new DateRange (System.DateTime.MinValue, this.DateRange.IncludeFrom),
						false,
						new EventType[] { EventType.Decrease })
						);

				case Column.PostIncreases:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						new DateRange (System.DateTime.MinValue, this.DateRange.IncludeFrom),
						false,
						new EventType[] { EventType.Increase })
						);

				case Column.PostAdjusts:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						new DateRange (System.DateTime.MinValue, this.DateRange.IncludeFrom),
						false,
						new EventType[] { EventType.Adjust })
						);

				case Column.PostOutputs:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						new DateRange (System.DateTime.MinValue, this.DateRange.IncludeFrom),
						false,
						new EventType[] { EventType.Output })
						);

				case Column.PostAmortizations:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						new DateRange (System.DateTime.MinValue, this.DateRange.IncludeFrom),
						false,
						new EventType[] { EventType.AmortizationAuto, EventType.AmortizationExtra, EventType.AmortizationPreview, EventType.AmortizationSuppl })
						);

				case Column.PostSummaries:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						new DateRange (System.DateTime.MinValue, this.DateRange.IncludeFrom),
						false,
						new EventType[] { EventType.Decrease, EventType.Increase, EventType.Adjust, EventType.Output })
						);

				case Column.Decreases:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						this.DateRange,
						false,
						new EventType[] { EventType.Decrease })
						);

				case Column.Increases:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						this.DateRange,
						false,
						new EventType[] { EventType.Increase })
						);

				case Column.Adjusts:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						this.DateRange,
						false,
						new EventType[] { EventType.Adjust })
						);

				case Column.Outputs:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						this.DateRange,
						false,
						new EventType[] { EventType.Output })
						);

				case Column.AmortizationsAuto:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						this.DateRange,
						false,
						new EventType[] { EventType.AmortizationAuto, EventType.AmortizationPreview })
						);

				case Column.AmortizationsExtra:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						this.DateRange,
						false,
						new EventType[] { EventType.AmortizationExtra })
						);

				case Column.AmortizationsSuppl:
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.DeltaSum,
						this.DateRange,
						false,
						new EventType[] { EventType.AmortizationSuppl })
						);

				case Column.FinalState:
					//	Avec une période du 01.01.2014 au 31.12.2014, on cherche l'état après
					//	le 31 décembre. Comme la date "au" est exclue dans un DateRange, la date
					//	ExcludeTo vaut 01.01.2015. On cherche donc l'état au 31.12.2014 23:59:59.
					return new ExtractionInstructionsArray (field,
						new ExtractionInstructions (ExtractionAmount.StateAt,
						new DateRange (System.DateTime.MinValue, this.DateRange.ExcludeTo.Date),
						false)
						);

				default:
					return new ExtractionInstructionsArray (ObjectField.Unknown, ExtractionInstructions.Empty);
			}
		}

		private int GetColumnWidth(Column column)
		{
			switch (column)
			{
				case Column.Name:
					return 200;

				default:
					return 100;
			}
		}

		private TreeTableColumnType GetColumnType(Column column)
		{
			if (column == Column.Name)
			{
				return TreeTableColumnType.Tree;
			}
			else if (column >= Column.User)
			{
				var userColumn = this.userColumns[(column-Column.User)];

				switch (userColumn.Type)
				{
					case FieldType.Date:
						return TreeTableColumnType.Date;

					case FieldType.String:
						return TreeTableColumnType.String;

					default:
						return TreeTableColumnType.Amount;
				}
			}
			else
			{
				return TreeTableColumnType.Amount;
			}
		}

		private string GetColumnName(Column column)
		{
			string text = null;

			if (column >= Column.User)
			{
				//	Retourne le nom d'une colonne supplémentaire.
				text = this.userColumns[(column-Column.User)].Name;
			}
			else
			{
				switch (column)
				{
					case Column.Name:
						text = Res.Strings.Enum.MCH2Summary.Column.Name.Text.ToString ();
						break;

					case Column.InitialState:
						text = Res.Strings.Enum.MCH2Summary.Column.InitialState.Text.ToString ();
						break;

					case Column.PreInputs:
						text = Res.Strings.Enum.MCH2Summary.Column.PreInputs.Text.ToString ();
						break;

					case Column.Inputs:
						text = Res.Strings.Enum.MCH2Summary.Column.Inputs.Text.ToString ();
						break;

					case Column.ReplacementValues:
						text = Res.Strings.Enum.MCH2Summary.Column.ReplacementValues.Text.ToString ();
						break;

					case Column.PostPreInputs:
						text = Res.Strings.Enum.MCH2Summary.Column.PostPreInputs.Text.ToString ();
						break;

					case Column.PostAdjusts:
						text = Res.Strings.Enum.MCH2Summary.Column.PostAdjusts.Text.ToString ();
						break;

					case Column.PostDecreases:
						text = Res.Strings.Enum.MCH2Summary.Column.PostDecreases.Text.ToString ();
						break;

					case Column.PostIncreases:
						text = Res.Strings.Enum.MCH2Summary.Column.PostIncreases.Text.ToString ();
						break;

					case Column.PostOutputs:
						text = Res.Strings.Enum.MCH2Summary.Column.PostOutputs.Text.ToString ();
						break;

					case Column.PostAmortizations:
						text = Res.Strings.Enum.MCH2Summary.Column.PostAmortizations.Text.ToString ();
						break;

					case Column.PostSummaries:
						text = Res.Strings.Enum.MCH2Summary.Column.PostSummaries.Text.ToString ();
						break;

					case Column.Decreases:
						text = Res.Strings.Enum.MCH2Summary.Column.Decreases.Text.ToString ();
						break;

					case Column.Increases:
						text = Res.Strings.Enum.MCH2Summary.Column.Increases.Text.ToString ();
						break;

					case Column.Adjusts:
						text = Res.Strings.Enum.MCH2Summary.Column.Adjusts.Text.ToString ();
						break;

					case Column.Outputs:
						text = Res.Strings.Enum.MCH2Summary.Column.Outputs.Text.ToString ();
						break;

					case Column.AmortizationsAuto:
						text = Res.Strings.Enum.MCH2Summary.Column.AmortizationsAuto.Text.ToString ();
						break;

					case Column.AmortizationsExtra:
						text = Res.Strings.Enum.MCH2Summary.Column.AmortizationsExtra.Text.ToString ();
						break;

					case Column.AmortizationsSuppl:
						text = Res.Strings.Enum.MCH2Summary.Column.AmortizationsSuppl.Text.ToString ();
						break;

					case Column.FinalState:
						text = Res.Strings.Enum.MCH2Summary.Column.FinalState.Text.ToString ();
						break;
				}
			}

			return this.ReplaceTags (text);
		}

		private string GetColumnTooltip(Column column)
		{
			string text = null;

			if (column >= Column.User)
			{
				//	Retourne le tooltip d'une colonne supplémentaire, par exemple "Valeur fiscale le 31.12.2015 à 23h59".
				var userColumn = this.userColumns[(column-Column.User)];
				text = string.Format (Res.Strings.Enum.MCH2Summary.Column.UserColumn.Tooltip.ToString (), userColumn.Name);
			}
			else
			{
				switch (column)
				{
					case Column.Name:
						text = Res.Strings.Enum.MCH2Summary.Column.Name.Tooltip.ToString ();
						break;

					case Column.InitialState:
						text = Res.Strings.Enum.MCH2Summary.Column.InitialState.Tooltip.ToString ();
						break;

					case Column.PreInputs:
						text = Res.Strings.Enum.MCH2Summary.Column.PreInputs.Tooltip.ToString ();
						break;

					case Column.Inputs:
						text = Res.Strings.Enum.MCH2Summary.Column.Inputs.Tooltip.ToString ();
						break;

					case Column.PostPreInputs:
						text = Res.Strings.Enum.MCH2Summary.Column.PostPreInputs.Tooltip.ToString ();
						break;

					case Column.ReplacementValues:
						text = Res.Strings.Enum.MCH2Summary.Column.ReplacementValues.Tooltip.ToString ();
						break;

					case Column.PostAdjusts:
						text = Res.Strings.Enum.MCH2Summary.Column.PostAdjusts.Tooltip.ToString ();
						break;

					case Column.PostDecreases:
						text = Res.Strings.Enum.MCH2Summary.Column.PostDecreases.Tooltip.ToString ();
						break;

					case Column.PostIncreases:
						text = Res.Strings.Enum.MCH2Summary.Column.PostIncreases.Tooltip.ToString ();
						break;

					case Column.PostOutputs:
						text = Res.Strings.Enum.MCH2Summary.Column.PostOutputs.Tooltip.ToString ();
						break;

					case Column.PostAmortizations:
						text = Res.Strings.Enum.MCH2Summary.Column.PostAmortizations.Tooltip.ToString ();
						break;

					case Column.PostSummaries:
						text = Res.Strings.Enum.MCH2Summary.Column.PostSummaries.Tooltip.ToString ();
						break;

					case Column.Decreases:
						text = Res.Strings.Enum.MCH2Summary.Column.Decreases.Tooltip.ToString ();
						break;

					case Column.Increases:
						text = Res.Strings.Enum.MCH2Summary.Column.Increases.Tooltip.ToString ();
						break;

					case Column.Adjusts:
						text = Res.Strings.Enum.MCH2Summary.Column.Adjusts.Tooltip.ToString ();
						break;

					case Column.Outputs:
						text = Res.Strings.Enum.MCH2Summary.Column.Outputs.Tooltip.ToString ();
						break;

					case Column.AmortizationsAuto:
						text = Res.Strings.Enum.MCH2Summary.Column.AmortizationsAuto.Tooltip.ToString ();
						break;

					case Column.AmortizationsExtra:
						text = Res.Strings.Enum.MCH2Summary.Column.AmortizationsExtra.Tooltip.ToString ();
						break;

					case Column.AmortizationsSuppl:
						text = Res.Strings.Enum.MCH2Summary.Column.AmortizationsSuppl.Tooltip.ToString ();
						break;

					case Column.FinalState:
						text = Res.Strings.Enum.MCH2Summary.Column.FinalState.Tooltip.ToString ();
						break;
				}
			}

			return this.ReplaceTags(text);
		}

		private string ReplaceTags(string text)
		{
			//	Remplace les tags par les valeurs correspondantes.
			//	Pour la période allant du 01.01.2015 (inclu) au 01.01.2016 (exclu), cela donne:
			//	<RANGE>             2015
			//	<INITIAL>           01.01.2015
			//	<FINAL>             01.01.2016
			//	<JUSTBEFOREFINAL>   31.12.2015 à 23h59

			if (!string.IsNullOrEmpty (text))
			{
				text = text.Replace ("&lt;RANGE&gt;",           this.RangeDate);
				text = text.Replace ("&lt;INITIAL&gt;",         this.InitialDate);
				text = text.Replace ("&lt;FINAL&gt;",           this.FinalDate);
				text = text.Replace ("&lt;JUSTBEFOREFINAL&gt;", this.JustDeforeFinalDate);

				if (text.Contains ("&lt;"))  // reste un tag inconnu ?
				{
					throw new System.InvalidOperationException (string.Format ("Unknown tag in {0}", text));
				}
			}

			return text;
		}

		private string RangeDate
		{
			get
			{
				if (this.DateRange.IsEmpty)
				{
					return "?";
				}
				else
				{
					return MCH2SummaryTreeTableFiller.ToNiceString (this.DateRange);
				}
			}
		}

		private string InitialDate
		{
			get
			{
				if (this.DateRange.IsEmpty)
				{
					return "?";
				}
				else
				{
					return TypeConverters.DateToString (this.DateRange.IncludeFrom);
				}
			}
		}

		private string FinalDate
		{
			get
			{
				if (this.DateRange.IsEmpty)
				{
					return "?";
				}
				else
				{
					return TypeConverters.DateToString (this.DateRange.ExcludeTo.AddDays (-1));
				}
			}
		}

		private string JustDeforeFinalDate
		{
			//	A partir de la date 01.01.2016, retourne le texte "31.12.15 à 23h59".
			get
			{
				if (this.DateRange.IsEmpty)
				{
					return "?";
				}
				else
				{
					var date = TypeConverters.DateToString (this.DateRange.ExcludeTo.AddDays (-1));
					return string.Format (Res.Strings.MCH2SummaryTreeTableFiller.DateOneMinuteToMidnight.ToString (), date);  // 31.12.xx à 23h59
				}
			}
		}


		private static string ToNiceString(DateRange range)
		{
			//	Retourne une jolie description d'une période, sous la forme
			//	"2014" ou "du 01.01.2014 au 31.03.2014".

			if (range.IsEmpty)
			{
				return "ø";
			}
			else
			{
				var f = range.IncludeFrom;
				var t = range.ExcludeTo.AddDays (-1);  // date de fin inclue

				if (f.Day   ==  1 &&
					f.Month ==  1 &&	// du premier janvier ?
					t.Day   == 31 &&
					t.Month == 12 &&	// au 31 décembre ?
					f.Year  == t.Year)	// de la même année ?
				{
					return f.Year.ToString ();
				}
				else
				{
					return string.Format (Res.Strings.MCH2SummaryTreeTableFiller.FromTo.ToString (),
						TypeConverters.DateToString (f),
						TypeConverters.DateToString (t));
				}
			}
		}
	
		
		private IEnumerable<Column> ExistingOrderedColumns
		{
			get
			{
				if (this.visibleColumns.Any ())
				{
					foreach (var column in this.OrderedColumns)
					{
						if (column == Column.Name ||
							this.visibleColumns.Contains (column))
						{
							yield return column;
						}
					}
				}
				else
				{
					foreach (var column in this.OrderedColumns)
					{
						yield return column;
					}
				}
			}
		}

		private IEnumerable<Column> OrderedColumns
		{
			//	Retourne les colonnes visibles, dans le bon ordre.
			get
			{
				switch (this.SummaryType)
				{
					case MCH2SummaryType.IndirectShort:
						yield return Column.Name;

						yield return Column.PostPreInputs;
						yield return Column.ReplacementValues;
						yield return Column.PostSummaries;
						yield return Column.PostAmortizations;

						yield return Column.PreInputs;
						yield return Column.Inputs;
						yield return Column.Decreases;
						yield return Column.Increases;
						yield return Column.Adjusts;
						yield return Column.Outputs;
						yield return Column.AmortizationsAuto;
						yield return Column.AmortizationsExtra;
						yield return Column.AmortizationsSuppl;
						yield return Column.FinalState;

						break;

					case MCH2SummaryType.IndirectDetailed:
						yield return Column.Name;

						yield return Column.PostPreInputs;
						yield return Column.ReplacementValues;
						yield return Column.PostDecreases;
						yield return Column.PostIncreases;
						yield return Column.PostAdjusts;
						yield return Column.PostOutputs;
						yield return Column.PostAmortizations;

						yield return Column.PreInputs;
						yield return Column.Inputs;
						yield return Column.Decreases;
						yield return Column.Increases;
						yield return Column.Adjusts;
						yield return Column.Outputs;
						yield return Column.AmortizationsAuto;
						yield return Column.AmortizationsExtra;
						yield return Column.AmortizationsSuppl;
						yield return Column.FinalState;

						break;

					case MCH2SummaryType.Direct:
						yield return Column.Name;
						yield return Column.InitialState;
						yield return Column.PreInputs;

						yield return Column.Inputs;

						yield return Column.Decreases;
						yield return Column.Increases;
						yield return Column.Adjusts;
						yield return Column.Outputs;
						yield return Column.AmortizationsAuto;
						yield return Column.AmortizationsExtra;
						yield return Column.AmortizationsSuppl;
						yield return Column.FinalState;

						break;
				}

				foreach (var userField in this.userColumns)
				{
					yield return userField.Column;
				}
			}
		}

		private ObjectField MainStringField
		{
			get
			{
				return this.accessor.GetMainStringField (BaseType.Assets);
			}
		}

		private enum Column
		{
			Name,

			InitialState,
			PreInputs,
			Inputs,
			ReplacementValues,
			PostPreInputs,
			PostAdjusts,
			PostDecreases,
			PostIncreases,
			PostOutputs,
			PostAmortizations,
			PostSummaries,
			Decreases,
			Increases,
			Adjusts,
			Outputs,
			AmortizationsAuto,
			AmortizationsExtra,
			AmortizationsSuppl,
			FinalState,
			User,					// +n, pour toutes les colonnes de l'utilisateur
		}


		private ObjectsNodeGetter NodeGetter
		{
			get
			{
				return this.nodeGetter as ObjectsNodeGetter;
			}
		}


		private void InitializeUserColumns()
		{
			this.userColumns.Clear ();

			int i = 0;
			foreach (var userField in accessor.UserFieldsAccessor.GetUserFields (BaseType.AssetsUserFields)
				.Where   (x => x.MCH2SummaryOrder.HasValue)
				.OrderBy (x => x.MCH2SummaryOrder.Value))
			{
				if (userField.MCH2SummaryOrder.HasValue)
				{
					var userColumn = new UserColumn (userField.Field, Column.User + (i++), userField.Name, userField.Type);
					this.userColumns.Add(userColumn);
				}
			}
		}


		private class UserColumn
		{
			public UserColumn(ObjectField field, Column column, string name, FieldType type)
			{
				this.Field  = field;
				this.Column = column;
				this.Name   = name;
				this.Type   = type;
			}

			public readonly ObjectField			Field;
			public readonly Column				Column;
			public readonly string				Name;
			public readonly FieldType			Type;
		}


		private readonly List<UserColumn>		userColumns;
		private readonly HashSet<int>			visibleRows;
		private readonly HashSet<Column>		visibleColumns;
	}
}
