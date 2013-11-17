﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Helpers;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.Helpers;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class ObjectSummaryController
	{
		public ObjectSummaryController(DataAccessor accessor, BaseType baseType, List<List<ObjectSummaryControllerTile>> tiles)
		{
			this.accessor = accessor;
			this.baseType = baseType;
			this.tiles    = tiles;

			this.controller = new SummaryController ();

			this.controller.TileClicked += delegate (object sender, int row, int column)
			{
				this.OnTileClicked (row, column);
			};
		}


		public void CreateUI(Widget parent)
		{
			this.informations = new StaticText
			{
				Parent          = parent,
				Dock            = DockStyle.Top,
				PreferredHeight = 30,
				Margins         = new Margins (0, 0, 0, 20),
			};

			this.controller.CreateUI (parent);
		}

		public void UpdateFields(Guid objectGuid, Timestamp? timestamp)
		{
			this.obj       = this.accessor.GetObject (this.baseType, objectGuid);
			this.timestamp = timestamp;
			this.hasEvent  = false;
			this.isLocked  = true;
			this.eventType = EventType.Unknown;

			if (!objectGuid.IsEmpty && this.obj != null)
			{
				var e = this.obj.GetEvent (this.timestamp.Value);

				if (e == null)
				{
					this.isLocked = ObjectCalculator.IsEventLocked (this.obj, this.timestamp.Value);
				}
				else
				{
					this.eventType = e.Type;
					this.hasEvent  = true;
					this.isLocked  = false;
				}
			}

			this.UpdateInformations ();

			var cells = new List<List<SummaryControllerTile?>> ();

			int columnsCount = this.ColumnsCount;
			int rowsCount    = this.RowsCount;

			for (int column = 0; column < columnsCount; column++)
			{
				//	Génère une colonne avec les noms des champs.
				{
					var columns = new List<SummaryControllerTile?> ();

					for (int row = 0; row < rowsCount; row++)
					{
						var tile = this.GetTile (column, row);
						var cell = this.GetLabel (tile);
						columns.Add (cell);
					}

					cells.Add (columns);
				}

				//	Génère une colonne avec les valeurs des champs.
				{
					var columns = new List<SummaryControllerTile?> ();

					for (int row = 0; row < rowsCount; row++)
					{
						var tile = this.GetTile (column, row);
						var cell = this.GetCell (tile);
						columns.Add (cell);
					}

					cells.Add (columns);
				}
			}

			this.controller.SetTiles (cells);
		}


		private void UpdateInformations()
		{
			this.informations.Text = string.Concat (this.Informations1, "<br/>", this.Informations2);
		}

		private string Informations1
		{
			get
			{
				if (this.hasEvent && this.obj != null)
				{
					var s1 = DataDescriptions.GetEventDescription (this.eventType);
					var s2 = this.SinglePropertiesCount;

					if (s2 <= 1)
					{
						return string.Format ("Cet événement de type \"{0}\" définit {1} champ.", s1, s2.ToString ());
					}
					else
					{
						return string.Format ("Cet événement de type \"{0}\" définit {1} champs.", s1, s2.ToString ());
					}
				}
				else if (!ObjectSummaryController.IsDefined (this.timestamp))
				{
					return "Il n'y a pas de date sélectionnée.";
				}
				else
				{
					return "Il n'y a pas d'événement à cette date.";
				}
			}
		}

		private string Informations2
		{
			get
			{
				if (ObjectSummaryController.IsDefined (this.timestamp))
				{
					string d = TypeConverters.DateToString (this.timestamp.Value.Date);
					return string.Format ("Le tableau ci-dessous montre l'état en date du {0} :", d);
				}
				else
				{
					return "Le tableau ci-dessous montre l'état final de l'objet :";
				}
			}
		}

		private static bool IsDefined(Timestamp? timestamp)
		{
			return timestamp != null && timestamp.Value.Date != System.DateTime.MaxValue;
		}


		private SummaryControllerTile? GetLabel(ObjectSummaryControllerTile tile)
		{
			if (tile.IsEmpty || tile.Field == ObjectField.Unknown)
			{
				return null;
			}

			string text = DataDescriptions.GetObjectFieldDescription (tile.Field);
			return new SummaryControllerTile (text, alignment: ContentAlignment.MiddleRight, simpleText: true);
		}

		private SummaryControllerTile? GetCell(ObjectSummaryControllerTile tile)
		{
			if (tile.IsEmpty || this.obj == null)
			{
				return null;
			}

			if (tile.Text != null)
			{
				return new SummaryControllerTile (tile.Text, alignment: ContentAlignment.MiddleCenter, simpleText: true);
			}

			string text = null;
			var alignment = ContentAlignment.MiddleCenter;

			switch (DataAccessor.GetFieldType (tile.Field))
			{
				case FieldType.Decimal:
					var d = ObjectCalculator.GetObjectPropertyDecimal (this.obj, this.timestamp, tile.Field);
					if (d.HasValue)
					{
						switch (Format.GetFieldFormat (tile.Field))
						{
							case DecimalFormat.Rate:
								text = TypeConverters.RateToString (d);
								alignment = ContentAlignment.MiddleRight;
								break;

							case DecimalFormat.Amount:
								text = TypeConverters.AmountToString (d);
								alignment = ContentAlignment.MiddleRight;
								break;

							case DecimalFormat.Real:
								text = TypeConverters.DecimalToString (d);
								alignment = ContentAlignment.MiddleRight;
								break;
						}
					}
					break;

				case FieldType.Int:
					var i = ObjectCalculator.GetObjectPropertyInt (this.obj, this.timestamp, tile.Field);
					if (i.HasValue)
					{
						text = TypeConverters.IntToString (i);
						alignment = ContentAlignment.MiddleRight;
					}
					break;

				case FieldType.ComputedAmount:
					var ca = ObjectCalculator.GetObjectPropertyComputedAmount (this.obj, this.timestamp, tile.Field);
					if (ca.HasValue)
					{
						text = TypeConverters.AmountToString (ca.Value.FinalAmount);
						alignment = ContentAlignment.MiddleRight;
					}
					break;

				case FieldType.Date:
					var da = ObjectCalculator.GetObjectPropertyDate (this.obj, this.timestamp, tile.Field);
					if (da.HasValue)
					{
						text = TypeConverters.DateToString (da.Value);
						alignment = ContentAlignment.MiddleLeft;
					}
					break;

				case FieldType.Guid:
					var g = ObjectCalculator.GetObjectPropertyGuid (this.obj, this.timestamp, tile.Field);
					if (!g.IsEmpty)
					{
						text = g.ToString ();  // TODO
						alignment = ContentAlignment.MiddleLeft;
					}
					break;

				default:
					string s = ObjectCalculator.GetObjectPropertyString (this.obj, this.timestamp, tile.Field);
					if (!string.IsNullOrEmpty (s))
					{
						text = s;
						alignment = ContentAlignment.MiddleLeft;
					}
					break;
			}

			string tooltip = DataDescriptions.GetObjectFieldDescription (tile.Field);
			bool hilited   = this.IsHilited (tile.Field);
			bool readOnly  = this.IsReadonly (tile.Field);

			return new SummaryControllerTile (text, tooltip, alignment, hilited, readOnly, this.isLocked);
		}

		private bool IsHilited(ObjectField field)
		{
			if (field != ObjectField.Unknown &&
				!DataAccessor.IsOneShotField(field))
			{
				return this.GetPropertyState (field) == PropertyState.Single;
			}
			else
			{
				return false;
			}
		}

		private bool IsReadonly(ObjectField field)
		{
			//	Un champ est non modifiable s'il appartient à une page interdite
			//	pour le type de l'événement en cours.
			if (this.hasEvent && field != ObjectField.Unknown)
			{
				var type = EditorPageSummary.GetPageType (this.baseType, field);
				var availables = ObjectEditor.GetAvailablePages (this.baseType, this.hasEvent, this.eventType);
				return !availables.Contains (type);
			}
			else
			{
				return true;
			}
		}

		private PropertyState GetPropertyState(ObjectField field)
		{
			if (this.hasEvent)
			{
				var p = ObjectCalculator.GetObjectSyntheticProperty (this.obj, this.timestamp, field);

				if (p != null)
				{
					return p.State;
				}
			}

			return PropertyState.Readonly;
		}

		private int SinglePropertiesCount
		{
			get
			{
				if (this.timestamp.HasValue)
				{
					var e = obj.GetEvent (this.timestamp.Value);
					if (e != null)
					{
						return e.PropertiesCount;
					}
				}

				return 0;
			}
		}

		private ObjectSummaryControllerTile GetTile(int column, int row)
		{
			if (column < this.tiles.Count)
			{
				var rows = this.tiles[column];

				if (row < rows.Count)
				{
					return rows[row];
				}
			}

			return ObjectSummaryControllerTile.Empty;
		}

		private int ColumnsCount
		{
			get
			{
				return this.tiles.Count;
			}
		}

		private int RowsCount
		{
			get
			{
				int count = 0;

				foreach (var columns in this.tiles)
				{
					count = System.Math.Max (count, columns.Count);
				}

				return count;
			}
		}


		#region Events handler
		private void OnTileClicked(int row, int column)
		{
			this.TileClicked.Raise (this, row, column);
		}

		public event EventHandler<int, int> TileClicked;
		#endregion


		private readonly DataAccessor				accessor;
		private readonly BaseType					baseType;
		private readonly List<List<ObjectSummaryControllerTile>> tiles;
		private readonly SummaryController			controller;

		private StaticText							informations;
		private DataObject							obj;
		private Timestamp?							timestamp;
		private bool								hasEvent;
		private bool								isLocked;
		private EventType							eventType;
	}
}
