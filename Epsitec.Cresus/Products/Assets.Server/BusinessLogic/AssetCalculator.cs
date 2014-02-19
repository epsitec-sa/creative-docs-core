﻿//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Server.NodeGetters;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.Server.BusinessLogic
{
	public static class AssetCalculator
	{
		public static Timestamp? GetLastTimestamp(DataObject obj)
		{
			//	Retourne la date à sélectionner dans la timeline après la sélection
			//	de l'objet.
			if (obj.EventsCount > 0)
			{
				return obj.GetEvent (obj.EventsCount-1).Timestamp;
			}

			return null;
		}


		#region Locked intervals logic
		public static bool IsLocked(List<LockedInterval> lockedIntervals, Timestamp timestamp)
		{
			//	Indique si une date est à l'intérieur d'un invervalle bloqué.
			foreach (var interval in lockedIntervals)
			{
				if (timestamp > interval.Start &&
					timestamp < interval.End)
				{
					return true;
				}
			}

			return false;
		}

		public static List<LockedInterval> GetLockedIntervals(DataObject obj)
		{
			//	Retourne la liste des intervalles bloqués, en fonction des événements
			//	d'entrée/sortie d'un objet.
			var intervals = new List<LockedInterval> ();

			if (obj != null)
			{
				var start = Timestamp.MinValue;
				bool isLocked = true;  // bloqué jusqu'au premier événement d'entrée

				int eventCount = obj.EventsCount;
				for (int i=0; i<eventCount; i++)
				{
					var e = obj.GetEvent (i);

					if (e.Type == EventType.Input)
					{
						var li = new LockedInterval
						{
							Start = start,
							End   = e.Timestamp,
						};

						intervals.Add (li);
						isLocked = false;
					}

					if (e.Type == EventType.Output)
					{
						start = e.Timestamp;
						isLocked = true;
					}
				}

				if (isLocked)
				{
					var li = new LockedInterval
					{
						Start = start,
						End   = Timestamp.MaxValue,
					};

					intervals.Add (li);
				}
			}

			return intervals;
		}

		public struct LockedInterval
		{
			public Timestamp Start;
			public Timestamp End;
		}
		#endregion


		#region Plausible event logic
		public static bool IsEventLocked(DataObject obj, Timestamp timestamp)
		{
			var prevEvent = AssetCalculator.GetPrevEvent (obj, timestamp);
			var nextEvent = AssetCalculator.GetNextEvent (obj, timestamp);

			return !((prevEvent == TerminalEvent.In || prevEvent == TerminalEvent.Other) &&
					 nextEvent != TerminalEvent.In);
		}

		public static IEnumerable<EventType> EventTypes
		{
			get
			{
				yield return EventType.Input;
				yield return EventType.Modification;
				yield return EventType.Reorganization;
				yield return EventType.Increase;
				yield return EventType.Decrease;
				yield return EventType.AmortizationExtra;
				yield return EventType.Output;
			}
		}

		public static IEnumerable<EventType> GetPlausibleEventTypes(DataObject obj, Timestamp timestamp)
		{
			var prevEvent = AssetCalculator.GetPrevEvent (obj, timestamp);
			var nextEvent = AssetCalculator.GetNextEvent (obj, timestamp);

			if ((prevEvent == TerminalEvent.None || prevEvent == TerminalEvent.Out) &&
				nextEvent == TerminalEvent.None)
			{
				yield return EventType.Input;
			}

			if ((prevEvent == TerminalEvent.In || prevEvent == TerminalEvent.Other) &&
				nextEvent != TerminalEvent.In)
			{
				yield return EventType.Modification;
				yield return EventType.Reorganization;
				yield return EventType.Increase;
				yield return EventType.Decrease;
				yield return EventType.AmortizationExtra;
			}

			if ((prevEvent == TerminalEvent.In || prevEvent == TerminalEvent.Other) &&
				nextEvent == TerminalEvent.None)
			{
				yield return EventType.Output;
			}
		}

		private static TerminalEvent GetPrevEvent(DataObject obj, Timestamp timestamp)
		{
			if (obj != null)
			{
				int i = obj.Events.Where (x => x.Timestamp <= timestamp).Count () - 1;

				if (i >= 0 && i < obj.EventsCount)
				{
					switch (obj.GetEvent (i).Type)
					{
						case EventType.Input:
							return TerminalEvent.In;

						case EventType.Output:
							return TerminalEvent.Out;

						default:
							return TerminalEvent.Other;
					}
				}
			}

			return TerminalEvent.None;
		}

		private static TerminalEvent GetNextEvent(DataObject obj, Timestamp timestamp)
		{
			if (obj != null)
			{
				int i = obj.Events.Where (x => x.Timestamp <= timestamp).Count ();

				if (i >= 0 && i < obj.EventsCount)
				{
					switch (obj.GetEvent (i).Type)
					{
						case EventType.Input:
							return TerminalEvent.In;

						case EventType.Output:
							return TerminalEvent.Out;

						default:
							return TerminalEvent.Other;
					}
				}
			}

			return TerminalEvent.None;
		}

		private enum TerminalEvent
		{
			None,
			In,
			Out,
			Other,
		}
		#endregion


		public static bool IsExistingObject(DataObject obj, Timestamp timestamp)
		{
			if (obj != null && obj.EventsCount > 0)
			{
				var e = obj.GetEvent (0);
				return e.Timestamp <= timestamp;
			}

			return false;
		}


		#region Get properties
		public static int? GetObjectPropertyInt(DataObject obj, Timestamp? timestamp, ObjectField field, bool synthetic = true)
		{
			var p = AssetCalculator.GetObjectProperty (obj, timestamp, field, synthetic) as DataIntProperty;

			if (p == null)
			{
				return null;
			}
			else
			{
				return p.Value;
			}
		}

		public static decimal? GetObjectPropertyDecimal(DataObject obj, Timestamp? timestamp, ObjectField field, bool synthetic = true)
		{
			var p = AssetCalculator.GetObjectProperty (obj, timestamp, field, synthetic) as DataDecimalProperty;

			if (p == null)
			{
				return null;
			}
			else
			{
				return p.Value;
			}
		}

		public static ComputedAmount? GetObjectPropertyComputedAmount(DataObject obj, Timestamp? timestamp, ObjectField field, bool synthetic = true)
		{
			var p = AssetCalculator.GetObjectProperty (obj, timestamp, field, synthetic) as DataComputedAmountProperty;

			if (p == null)
			{
				return null;
			}
			else
			{
				return p.Value;
			}
		}

		public static System.DateTime? GetObjectPropertyDate(DataObject obj, Timestamp? timestamp, ObjectField field, bool synthetic = true)
		{
			var p = AssetCalculator.GetObjectProperty (obj, timestamp, field, synthetic) as DataDateProperty;

			if (p == null)
			{
				return null;
			}
			else
			{
				return p.Value;
			}
		}

		public static Guid GetObjectPropertyGuid(DataObject obj, Timestamp? timestamp, ObjectField field, bool synthetic = true, bool inputValue = false)
		{
			var p = AssetCalculator.GetObjectProperty (obj, timestamp, field, synthetic) as DataGuidProperty;

			if (p == null)
			{
				if (inputValue)
				{
					p = AssetCalculator.GetObjectInputProperty (obj, field) as DataGuidProperty;
					if (p != null)
					{
						return p.Value;
					}
				}

				return Guid.Empty;
			}
			else
			{
				return p.Value;
			}
		}

		public static GuidRatio GetObjectPropertyGuidRatio(DataObject obj, Timestamp? timestamp, ObjectField field, bool synthetic = true, bool inputValue = false)
		{
			var p = AssetCalculator.GetObjectProperty (obj, timestamp, field, synthetic) as DataGuidRatioProperty;

			if (p == null)
			{
				if (inputValue)
				{
					p = AssetCalculator.GetObjectInputProperty (obj, field) as DataGuidRatioProperty;
					if (p != null)
					{
						return p.Value;
					}
				}

				return GuidRatio.Empty;
			}
			else
			{
				return p.Value;
			}
		}

		public static string GetObjectPropertyString(DataObject obj, Timestamp? timestamp, ObjectField field, bool synthetic = true, bool inputValue = false)
		{
			var p = AssetCalculator.GetObjectProperty (obj, timestamp, field, synthetic) as DataStringProperty;

			if (p == null)
			{
				if (inputValue)
				{
					p = AssetCalculator.GetObjectInputProperty (obj, field) as DataStringProperty;
					if (p != null && !string.IsNullOrEmpty (p.Value))
					{
						return string.Concat ("<i>", p.Value, "</i>");
					}
				}

				return null;
			}
			else
			{
				return p.Value;
			}
		}


		public static ComparableData GetComparableData(DataObject obj, Timestamp? timestamp, ObjectField field)
		{
			if (obj != null)
			{
				var p = AssetCalculator.GetObjectSyntheticProperty (obj, timestamp, field);

				if (p == null)
				{
					//	Pour le tri, si on n'a pas trouvé de propriété, on prend
					//	celle définie lors de l'événement d'entrée.
					p = AssetCalculator.GetObjectInputProperty (obj, field);
				}

				return AssetCalculator.GetComparableData (p);
			}

			return ComparableData.Empty;
		}

		public static ComparableData GetComparableData(AbstractDataProperty property)
		{
			if (property != null)
			{
				if (property is DataIntProperty)
				{
					return new ComparableData ((property as DataIntProperty).Value);
				}
				else if (property is DataDecimalProperty)
				{
					return new ComparableData ((property as DataDecimalProperty).Value);
				}
				else if (property is DataComputedAmountProperty)
				{
					return new ComparableData ((property as DataComputedAmountProperty).Value.FinalAmount.GetValueOrDefault ());
				}
				else if (property is DataDateProperty)
				{
					return new ComparableData ((property as DataDateProperty).Value);
				}
				else if (property is DataStringProperty)
				{
					return new ComparableData ((property as DataStringProperty).Value);
				}
			}

			return ComparableData.Empty;
		}


		public static AbstractDataProperty GetObjectProperty(DataObject obj, Timestamp? timestamp, ObjectField field, bool synthetic)
		{
			if (synthetic || !timestamp.HasValue)
			{
				return AssetCalculator.GetObjectSyntheticProperty (obj, timestamp, field);
			}
			else
			{
				return AssetCalculator.GetObjectSingleProperty (obj, timestamp.Value, field);
			}
		}

		public static AbstractDataProperty GetObjectSingleProperty(DataObject obj, Timestamp timestamp, ObjectField field)
		{
			//	Retourne l'état d'une propriété d'un objet à la date exacte.
			if (obj == null)
			{
				return null;
			}
			else
			{
				return obj.GetSingleProperty (timestamp, field);
			}
		}

		public static AbstractDataProperty GetObjectInputProperty(DataObject obj, ObjectField field)
		{
			//	Retourne l'état d'une propriété d'un objet lors de l'événement d'entrée.
			if (obj == null)
			{
				return null;
			}
			else
			{
				return obj.GetInputProperty (field);
			}
		}

		public static AbstractDataProperty GetObjectSyntheticProperty(DataObject obj, Timestamp? timestamp, ObjectField field)
		{
			//	Retourne l'état d'une propriété d'un objet à la date exacte ou antérieurement.
			if (obj == null)
			{
				return null;
			}
			else
			{
				if (!timestamp.HasValue)
				{
					timestamp = Timestamp.MaxValue;
				}

				return obj.GetSyntheticProperty (timestamp.Value, field);
			}
		}
		#endregion


		#region Update computed amount
		public static void UpdateComputedAmounts(DataAccessor accessor, DataObject obj)
		{
			if (obj != null)
			{
				foreach (var field in accessor.ValueFields)
				{
					decimal? last = null;

					foreach (var e in obj.Events)
					{
						var current = AssetCalculator.GetComputedAmount (e, field);

						if (current.HasValue)
						{
							if (last.HasValue == false)
							{
								last = current.Value.FinalAmount;
								current = new ComputedAmount (last);
								AssetCalculator.SetComputedAmount (e, field, current);
							}
							else
							{
								current = new ComputedAmount (last.Value, current.Value);
								last = current.Value.FinalAmount;
								AssetCalculator.SetComputedAmount (e, field, current);
							}
						}
					}
				}
			}
		}

		private static ComputedAmount? GetComputedAmount(DataEvent e, ObjectField field)
		{
			var p = e.GetProperty (field) as DataComputedAmountProperty;
			if (p == null)
			{
				return null;
			}
			else
			{
				return p.Value;
			}
		}

		private static void SetComputedAmount(DataEvent e, ObjectField field, ComputedAmount? value)
		{
			if (value.HasValue)
			{
				var newProperty = new DataComputedAmountProperty (field, value.Value);
				e.AddProperty (newProperty);
			}
			else
			{
				e.RemoveProperty (field);
			}
		}
		#endregion
	}
}
