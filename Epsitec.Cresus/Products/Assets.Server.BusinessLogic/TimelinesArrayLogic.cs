﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Server.NodeGetters;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.Server.BusinessLogic
{
	public class TimelinesArrayLogic
	{
		public TimelinesArrayLogic(DataAccessor accessor)
		{
			this.accessor = accessor;
		}


		public void Update(TimelineArray dataArray, IObjectsNodeGetter nodeGetter, TimelinesMode mode, DateRange groupedExcludeRange, System.Func<DataEvent, bool> filter = null)
		{
			//	Met à jour this.dataArray en fonction de l'ensemble des événements de
			//	tous les objets. Cela nécessite d'accéder à l'ensemble des données, ce
			//	qui peut être long. Néanmoins, cela est nécessaire, même si la timeline
			//	n'affiche qu'un nombre limité de lignes. En effet, il faut allouer toutes
			//	les colonnes pour lesquelles il existe un événement.
			dataArray.Clear (nodeGetter.Count, mode, groupedExcludeRange);

			for (int row=0; row<nodeGetter.Count; row++)
			{
				var node = nodeGetter[row];
				var obj = this.accessor.GetObject (node.BaseType, node.Guid);
				var field = this.accessor.GetMainStringField (node.BaseType);

				var label = ObjectProperties.GetObjectPropertyString (obj, null, field);
				dataArray.RowsLabel.Add (label);

				if (node.BaseType == BaseType.Assets)
				{
					foreach (var e in obj.Events)
					{
						if (filter == null || filter (e))
						{
							bool grouped = (mode == TimelinesMode.GroupedByMonth && !groupedExcludeRange.IsInside (e.Timestamp.Date));
							var column = dataArray.GetColumn (e.Timestamp, grouped);

							if (mode == TimelinesMode.GroupedByMonth)
							{
								if (column[row].IsEmpty)
								{
									column[row] = this.EventToCell (obj, e);
								}
								else
								{
									//	Effectue un merge.
									column[row] = new TimelineCell(column[row], this.EventToCell (obj, e));
								}
							}
							else
							{
								column[row] = this.EventToCell (obj, e);
							}
						}
					}
				}
			}

			//	Marque les intervalles bloqués, qui seront hachurés.
			for (int row=0; row<nodeGetter.Count; row++)
			{
				var node = nodeGetter[row];

				if (node.BaseType == BaseType.Assets)
				{
					var obj = this.accessor.GetObject (node.BaseType, node.Guid);
					var outOfBoundsIntervals = AssetCalculator.GetOutOfBoundsIntervals (obj);
					var lockedTimestamp = AssetCalculator.GetLockedTimestamp (obj);

					for (int c=0; c<dataArray.ColumnsCount; c++)
					{
						var column = dataArray.GetColumn (c);
						var flags = column[row].Flags;

						if (column.Timestamp < lockedTimestamp)
						{
							flags |= DataCellFlags.Locked;
						}

						if (AssetCalculator.IsOutOfBounds (outOfBoundsIntervals, column.Timestamp))
						{
							flags |= DataCellFlags.OutOfBounds;
						}

						if (column[row].Flags != flags)
						{
							column[row] = new TimelineCell (column[row].Glyphs, flags, column[row].Tooltip);
						}
					}
				}
				else
				{
					for (int c=0; c<dataArray.ColumnsCount; c++)
					{
						var column = dataArray.GetColumn (c);
						column[row] = new TimelineCell (column[row].Glyphs, DataCellFlags.Group, null);
					}
				}
			}
		}

		private TimelineCell EventToCell(DataObject obj, DataEvent e)
		{
			var glyph      = TimelineData.TypeToGlyph (e.Type);
			string tooltip = LogicDescriptions.GetTooltip (this.accessor, obj, e.Timestamp, e.Type, 8);

			return new TimelineCell (glyph, DataCellFlags.None, tooltip);
		}


		private readonly DataAccessor			accessor;
	}
}