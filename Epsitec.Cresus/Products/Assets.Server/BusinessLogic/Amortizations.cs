﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.Server.BusinessLogic
{
	public class Amortizations
	{
		public Amortizations(DataAccessor accessor)
		{
			this.accessor = accessor;
		}


		public List<Error> Preview(DateRange processRange)
		{
			var errors = new List<Error> ();
			var getter = this.accessor.GetNodeGetter (BaseType.Assets);

			foreach (var node in getter.Nodes)
			{
				errors.AddRange (this.Create (processRange, node.Guid));
			}

			return errors;
		}

		public List<Error> Fix()
		{
			var errors = new List<Error> ();
			var getter = this.accessor.GetNodeGetter (BaseType.Assets);

			foreach (var node in getter.Nodes)
			{
				errors.AddRange (this.Fix (node.Guid));
			}

			return errors;
		}

		public List<Error> Unpreview()
		{
			var errors = new List<Error> ();
			var getter = this.accessor.GetNodeGetter (BaseType.Assets);

			foreach (var node in getter.Nodes)
			{
				errors.AddRange (this.Unpreview (node.Guid));
			}

			return errors;
		}

		public List<Error> Delete(System.DateTime startDate)
		{
			var errors = new List<Error> ();
			var getter = this.accessor.GetNodeGetter (BaseType.Assets);

			foreach (var node in getter.Nodes)
			{
				errors.AddRange (this.Delete (startDate, node.Guid));
			}

			return errors;
		}


		public List<Error> Create(DateRange processRange, Guid objectGuid)
		{
			var errors = new List<Error> ();

			var obj = this.accessor.GetObject (BaseType.Assets, objectGuid);
			System.Diagnostics.Debug.Assert (obj != null);

			this.GeneratesAmortizationsPreview (errors, processRange, objectGuid);

			return errors;
		}

		public List<Error> Fix(Guid objectGuid)
		{
			var errors = new List<Error> ();

			var obj = this.accessor.GetObject (BaseType.Assets, objectGuid);
			System.Diagnostics.Debug.Assert (obj != null);

			int count = Amortizations.FixEvents (obj, DateRange.Full);

			return errors;
		}

		public List<Error> Unpreview(Guid objectGuid)
		{
			var errors = new List<Error> ();

			var obj = this.accessor.GetObject (BaseType.Assets, objectGuid);
			System.Diagnostics.Debug.Assert (obj != null);

			int count = Amortizations.RemoveEvents (obj, EventType.AmortizationPreview, DateRange.Full);

			return errors;
		}

		public List<Error> Delete(System.DateTime startDate, Guid objectGuid)
		{
			var errors = new List<Error> ();

			var obj = this.accessor.GetObject (BaseType.Assets, objectGuid);
			System.Diagnostics.Debug.Assert (obj != null);

			int count = Amortizations.RemoveEvents (obj, EventType.AmortizationAuto, new DateRange (startDate, System.DateTime.MaxValue));

			return errors;
		}


		private void GeneratesAmortizationsPreview(List<Error> errors, DateRange processRange, Guid objectGuid)
		{
			//	Génère les aperçus d'amortissement pour un objet donné.
			var obj = this.accessor.GetObject (BaseType.Assets, objectGuid);
			System.Diagnostics.Debug.Assert (obj != null);

			//	Supprime tous les aperçus d'amortissement.
			Amortizations.RemoveEvents (obj, EventType.AmortizationPreview, DateRange.Full);

			//	Passe en revue les périodes.
			foreach (var period in this.GetPeriods (processRange, obj))
			{
				var ad = this.GetAmortizationDetails (obj, period.ExcludeTo.AddDays (-1));
				if (!ad.IsEmpty)
				{
					//	On crée un aperçu de l'amortissement au 31.12.
					this.CreateAmortizationPreview (obj, period.ExcludeTo.AddDays (-1), ad);
				}
			}

			//	Supprime tous les événements d'amortissement ordinaire après la date de fin.
			Amortizations.RemoveEvents (obj, EventType.AmortizationAuto, new DateRange (processRange.ExcludeTo, System.DateTime.MaxValue));

			//	Pour mettre à jour les éventuels amortissements extraordinaires suivants.
			Amortizations.UpdateAmounts (this.accessor, obj);
		}

		private IEnumerable<DateRange> GetPeriods(DateRange processRange, DataObject obj)
		{
			//	Retourne la liste des périodes pour lesquelles il faudra tenter des amortissements.
			if (obj.EventsCount > 0)
			{
				//	Cherche la date d'entrée de l'objet.
				var inputDate = AssetCalculator.GetFirstTimestamp (obj).Value.Date;

				if (inputDate <= processRange.ExcludeTo)
				{
					var beginDate = new System.DateTime (processRange.IncludeFrom.Year, 1, 1);

					while (beginDate < processRange.ExcludeTo)
					{
						var def = Amortizations.GetAmortizationDefinition (obj, new Timestamp (beginDate, 0));

						if (def.IsEmpty)
						{
							//	Si l'objet n'est pas entré au début de l'année, mais qu'il l'est plus
							//	tard dans la période choisie (processRange), on démarre un amortissement
							//	au début de l'année d'entrée, au prorata de la durée.
							def = Amortizations.GetAmortizationDefinition (obj, new Timestamp (inputDate, 0));

							if (def.IsEmpty)
							{
								beginDate = new System.DateTime (inputDate.Year, 1, 1);
							}
							else
							{
								beginDate = def.GetBeginRangeDate (beginDate);
							}
						}

						if (def.IsEmpty)
						{
							//	Si aucun amortissement n'est défini, on essaie de nouveaux amortissements
							//	à partir de l'année prochaine.
							beginDate = beginDate.AddYears (1);
						}
						else
						{
							var endDate = beginDate.AddMonths (def.PeriodMonthCount);
							yield return new DateRange (beginDate, endDate);

							beginDate = endDate;
						}
					}
				}
			}
		}

		private AmortizationDetails GetAmortizationDetails(DataObject obj, System.DateTime date)
		{
			//	Retourne tous les détails sur un amortissement ordinaire.
			var def = Amortizations.GetAmortizationDefinition (obj, new Timestamp (date, 0));
			if (def.IsEmpty)
			{
				return AmortizationDetails.Empty;
			}

			var beginDate = def.GetBeginRangeDate (date);
			var endDate = beginDate.AddMonths (def.PeriodMonthCount);
			var range = new DateRange (beginDate, endDate);

			if (AssetCalculator.IsEventLocked (obj, new Timestamp (range.ExcludeTo.AddSeconds (-1), 0)))
			{
				return AmortizationDetails.Empty;
			}

			//	S'il y a déjà un (ou plusieurs) amortissement (extra)ordinaire dans la période,
			//	on ne génère pas d'amortissement ordinaire.
			if (Amortizations.HasAmortizations (obj, EventType.AmortizationExtra, range) ||
				Amortizations.HasAmortizations (obj, EventType.AmortizationAuto, range))
			{
				return AmortizationDetails.Empty;
			}

			//	Génère l'aperçu d'amortissement.
			var valueDate = range.IncludeFrom;

			var inputDate = AssetCalculator.GetFirstTimestamp (obj).Value.Date;
			if (range.IsInside (inputDate))
			{
				//	Si l'objet est entrée durant la période, on utilise sa date d'entrée
				//	pour calculer l'amortissement "au prorata".
				valueDate = inputDate;
			}

			var pd = ProrataDetails.ComputeProrata (range, valueDate, def.ProrataType);
			return new AmortizationDetails (def, pd);
		}


		private static AmortizationDefinition GetAmortizationDefinition(DataObject obj, Timestamp timestamp)
		{
			//	Collecte tous les champs qui définissent comment amortir. Ils peuvent provenir
			//	de plusieurs événements différents.
			var taux     = ObjectProperties.GetObjectPropertyDecimal (obj, timestamp, ObjectField.AmortizationRate);
			var type     = ObjectProperties.GetObjectPropertyInt     (obj, timestamp, ObjectField.AmortizationType);
			var period   = ObjectProperties.GetObjectPropertyInt     (obj, timestamp, ObjectField.Periodicity);
			var prorata  = ObjectProperties.GetObjectPropertyInt     (obj, timestamp, ObjectField.Prorata);
			var round    = ObjectProperties.GetObjectPropertyDecimal (obj, timestamp, ObjectField.Round);
			var residual = ObjectProperties.GetObjectPropertyDecimal (obj, timestamp, ObjectField.ResidualValue);

			if (taux.HasValue && type.HasValue && period.HasValue)
			{
				var t = (AmortizationType) type;
				var p = (Periodicity) period;
				var r = (ProrataType) prorata;

				return new AmortizationDefinition (taux.GetValueOrDefault (0.0m), t, p, r, round.GetValueOrDefault (0.0m), residual.GetValueOrDefault (0.0m));
			}
			else
			{
				return AmortizationDefinition.Empty;
			}
		}

		private void CreateAmortizationPreview(DataObject obj, System.DateTime date, AmortizationDetails details)
		{
			//	Crée l'événement d'aperçu d'amortissement.
			var e = this.accessor.CreateObjectEvent (obj, date, EventType.AmortizationPreview);

			if (e != null)
			{
				//	InitialAmount et BaseAmount seront calculés plus tard.
				var aa = new AmortizedAmount (details.Def.Type, null, null, 
					details.Def.EffectiveRate, details.Prorata.Numerator, details.Prorata.Denominator,
					details.Def.Round, details.Def.Residual);

				var p = new DataAmortizedAmountProperty (ObjectField.MainValue, aa);
				e.AddProperty (p);
			}
		}


		private static bool HasAmortizations(DataObject obj, EventType type, DateRange range)
		{
			//	Indique s'il existe un ou plusieurs amortissements dans un intervalle de dates.
			return obj.Events
				.Where (x => x.Type == type && range.IsInside (x.Timestamp.Date))
				.Any ();
		}

		private static int RemoveEvents(DataObject obj, EventType type, DateRange range)
		{
			//	Supprime tous les événements d'un objet d'un type donné compris dans
			//	un intervalle de dates.
			int count = 0;

			if (obj != null)
			{
				var guids = obj.Events
					.Where (x => x.Type == type && range.IsInside (x.Timestamp.Date))
					.Select (x => x.Guid)
					.ToArray ();

				foreach (var guid in guids)
				{
					var e = obj.GetEvent (guid);
					obj.RemoveEvent (e);
					count++;
				}
			}

			return count;
		}

		private static int FixEvents(DataObject obj, DateRange range)
		{
			//	Transforme tous les événements d'un objet compris dans un intervalle de dates,
			//	de AmortizationPreview en AmortizationAuto.
			int count = 0;

			if (obj != null)
			{
				var guids = obj.Events
					.Where (x => x.Type == EventType.AmortizationPreview && range.IsInside (x.Timestamp.Date))
					.Select (x => x.Guid)
					.ToArray ();

				foreach (var guid in guids)
				{
					var currentEvent = obj.GetEvent (guid);
					obj.RemoveEvent (currentEvent);

					var newEvent = new DataEvent (currentEvent.Timestamp, EventType.AmortizationAuto);
					newEvent.SetProperties (currentEvent);
					obj.AddEvent (newEvent);

					count++;
				}
			}

			return count;
		}


		#region Update amounts
		public static void UpdateAmounts(DataAccessor accessor, DataObject obj)
		{
			//	Répercute les valeurs des montants selon la chronologie des événements.
			if (obj != null)
			{
				foreach (var field in accessor.ValueFields)
				{
					decimal? lastAmount = null;
					decimal? lastBase   = null;

					foreach (var e in obj.Events)
					{
						if (field == ObjectField.MainValue)
						{
							Amortizations.UpdateAmortizedAmount (e, field, ref lastAmount, ref lastBase);
						}
						else
						{
							Amortizations.UpdateComputedAmount (e, field, ref lastAmount);
						}
					}
				}
			}
		}

		private static void UpdateAmortizedAmount(DataEvent e, ObjectField field, ref decimal? lastAmount, ref decimal? lastBase)
		{
			var current = Amortizations.GetAmortizedAmount (e, field);

			if (current.HasValue)
			{
				if (current.Value.AmortizationType == AmortizationType.Unknown)  // montant fixe ?
				{
					lastBase = current.Value.FinalAmortizedAmount;
				}
				else  // amortissement ?
				{
					current = new AmortizedAmount
					(
						current.Value.AmortizationType,
						lastAmount.HasValue ? lastAmount.Value : current.Value.InitialAmount,
						lastBase.HasValue ? lastBase.Value : current.Value.BaseAmount,
						current.Value.EffectiveRate,
						current.Value.ProrataNumerator,
						current.Value.ProrataDenominator,
						current.Value.RoundAmount,
						current.Value.ResidualAmount
					);

					Amortizations.SetAmortizedAmount (e, field, current);
				}

				lastAmount = current.Value.FinalAmortizedAmount;
			}
		}

		private static void UpdateComputedAmount(DataEvent e, ObjectField field, ref decimal? lastAmount)
		{
			var current = Amortizations.GetComputedAmount (e, field);

			if (current.HasValue)
			{
				if (lastAmount.HasValue == false)
				{
					lastAmount = current.Value.FinalAmount;
				}
				else
				{
					current = new ComputedAmount (lastAmount.Value, current.Value);
					lastAmount = current.Value.FinalAmount;
					Amortizations.SetComputedAmount (e, field, current);
				}
			}
		}

		private static AmortizedAmount? GetAmortizedAmount(DataEvent e, ObjectField field)
		{
			var p = e.GetProperty (field) as DataAmortizedAmountProperty;
			if (p == null)
			{
				return null;
			}
			else
			{
				return p.Value;
			}
		}

		private static void SetAmortizedAmount(DataEvent e, ObjectField field, AmortizedAmount? value)
		{
			if (value.HasValue)
			{
				var newProperty = new DataAmortizedAmountProperty (field, value.Value);
				e.AddProperty (newProperty);
			}
			else
			{
				e.RemoveProperty (field);
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

	
		private readonly DataAccessor			accessor;
	}
}
