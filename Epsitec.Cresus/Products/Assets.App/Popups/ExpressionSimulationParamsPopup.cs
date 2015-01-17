﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Popups.StackedControllers;
using Epsitec.Cresus.Assets.App.Settings;
using Epsitec.Cresus.Assets.App.Views;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.Expression;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Popups
{
	/// <summary>
	/// Popup permettant de définir les paramètres d'une expression d'amortissement
	/// pour lancer la simulation des amortissements.
	/// </summary>
	public class ExpressionSimulationParamsPopup : AbstractStackedPopup
	{
		private ExpressionSimulationParamsPopup(DataAccessor accessor, IEnumerable<Guid> argumentGuids)
			: base (accessor)
		{
			this.argumentGuids = argumentGuids.ToArray ();
			this.fields = this.ArgumentFields.ToArray ();

			this.controllerRanks = new Dictionary<ObjectField, int> ();
			this.lastArguments = new Dictionary<ObjectField, object> ();

			this.title = Res.Strings.Popup.ExpressionSimulationParams.Title.ToString ();

			var list = new List<StackedControllerDescription> ();

			list.Add (new StackedControllerDescription  // 0
			{
				StackedControllerType = StackedControllerType.Date,
				Label                 = Res.Strings.Popup.ExpressionSimulationParams.StartDate.ToString (),
				DateRangeCategory     = DateRangeCategory.Free,
			});

			list.Add (new StackedControllerDescription  // 1
			{
				StackedControllerType = StackedControllerType.Date,
				Label                 = Res.Strings.Popup.ExpressionSimulationParams.EndDate.ToString (),
				DateRangeCategory     = DateRangeCategory.Free,
				BottomMargin          = 20,
			});

			list.Add (new StackedControllerDescription  // 2
			{
				StackedControllerType = StackedControllerType.Combo,
				Label                 = Res.Strings.Popup.ExpressionSimulationParams.Periodicity.ToString (),
				MultiLabels           = ExpressionSimulationParamsPopup.PeriodicityLabels,
				Width                 = 240,
			});

			list.Add (new StackedControllerDescription  // 3
			{
				StackedControllerType = StackedControllerType.Decimal,
				Label                 = Res.Strings.Popup.ExpressionSimulationParams.InitialAmount.ToString (),
				DecimalFormat         = DecimalFormat.Amount,
				BottomMargin          = 10,
			});

			list.Add (new StackedControllerDescription  // 4
			{
				StackedControllerType = StackedControllerType.Date,
				Label                 = Res.Strings.Popup.ExpressionSimulationParams.ExtraDate.ToString (),
				DateRangeCategory     = DateRangeCategory.Free,
			});

			list.Add (new StackedControllerDescription  // 5
			{
				StackedControllerType = StackedControllerType.Decimal,
				Label                 = Res.Strings.Popup.ExpressionSimulationParams.ExtraAmount.ToString (),
				DecimalFormat         = DecimalFormat.Amount,
			});

			list.Add (new StackedControllerDescription  // 6
			{
				StackedControllerType = StackedControllerType.Bool,
				Label                 = Res.Strings.Popup.ExpressionSimulationParams.AmortizationSuppl.ToString (),
			});

			list.Add (new StackedControllerDescription  // 7
			{
				StackedControllerType = StackedControllerType.Date,
				Label                 = Res.Strings.Popup.ExpressionSimulationParams.AdjustDate.ToString (),
				DateRangeCategory     = DateRangeCategory.Free,
			});

			list.Add (new StackedControllerDescription  // 8
			{
				StackedControllerType = StackedControllerType.Decimal,
				Label                 = Res.Strings.Popup.ExpressionSimulationParams.AdjustAmount.ToString (),
				DecimalFormat         = DecimalFormat.Amount,
				BottomMargin          = 20,
			});

			int rank = list.Count;  // 9
			foreach (var field in this.fields)
			{
				var argument = ArgumentsLogic.GetArgument (this.accessor, field);
				var type     = ArgumentsLogic.GetArgumentType (argument);
				var name     = ArgumentsLogic.GetShortName (argument);

				switch (type)
				{
					case ArgumentType.Decimal:
						list.Add (new StackedControllerDescription
						{
							StackedControllerType = StackedControllerType.Decimal,
							Label                 = name,
							DecimalFormat         = DecimalFormat.Real,
						});
						break;

					case ArgumentType.Amount:
						list.Add (new StackedControllerDescription
						{
							StackedControllerType = StackedControllerType.Decimal,
							Label                 = name,
							DecimalFormat         = DecimalFormat.Amount,
						});
						break;

					case ArgumentType.Rate:
						list.Add (new StackedControllerDescription
						{
							StackedControllerType = StackedControllerType.Decimal,
							Label                 = name,
							DecimalFormat         = DecimalFormat.Rate,
						});
						break;

					case ArgumentType.Years:
						list.Add (new StackedControllerDescription
						{
							StackedControllerType = StackedControllerType.Decimal,
							Label                 = name,
							DecimalFormat         = DecimalFormat.Years,
						});
						break;

					case ArgumentType.Int:
						list.Add (new StackedControllerDescription
						{
							StackedControllerType = StackedControllerType.Int,
							Label                 = name,
						});
						break;

					case ArgumentType.Bool:
						list.Add (new StackedControllerDescription
						{
							StackedControllerType = StackedControllerType.Bool,
							Label                 = name,
						});
						break;

					case ArgumentType.Date:
						list.Add (new StackedControllerDescription
						{
							StackedControllerType = StackedControllerType.Date,
							Label                 = name,
						});
						break;

					case ArgumentType.String:
						list.Add (new StackedControllerDescription
						{
							StackedControllerType = StackedControllerType.Text,
							Label                 = name,
						});
						break;

					default:
						throw new System.InvalidOperationException (string.Format ("Invalid ArgumentType {0}", type));
				}

				this.controllerRanks.Add (field, rank++);
			}

			this.SetDescriptions (list);

			this.defaultAcceptButtonName = "Simulation";
			this.defaultCancelButtonName = Res.Strings.Popup.Button.Cancel.ToString ();
			this.defaultControllerRankFocus = 2;
		}


		private DateRange Range
		{
			get
			{
				return new DateRange (this.StartDate, this.ToDate);
			}
			set
			{
				this.StartDate = value.IncludeFrom;
				this.ToDate    = value.ExcludeTo;
			}
		}

		private System.DateTime StartDate
		{
			get
			{
				var controller = this.GetController (0) as DateStackedController;
				System.Diagnostics.Debug.Assert (controller != null);

				if (controller.Value.HasValue)
				{
					return controller.Value.Value;
				}
				else
				{
					return new System.DateTime (2000, 1, 1);
				}
			}
			set
			{
				var controller = this.GetController (0) as DateStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				controller.Value = value;
			}
		}

		private System.DateTime ToDate
		{
			get
			{
				var controller = this.GetController (1) as DateStackedController;
				System.Diagnostics.Debug.Assert (controller != null);

				if (controller.Value.HasValue)
				{
					return controller.Value.Value;
				}
				else
				{
					return new System.DateTime (2100, 1, 1);
				}
			}
			set
			{
				var controller = this.GetController (1) as DateStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				controller.Value = value;
			}
		}

		private Periodicity Periodicity
		{
			get
			{
				var controller = this.GetController (2) as ComboStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				return ExpressionSimulationParamsPopup.RankToPeriodicity (controller.Value.GetValueOrDefault ());
			}
			set
			{
				var controller = this.GetController (2) as ComboStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				controller.Value = ExpressionSimulationParamsPopup.PeriodicityToRank (value);
			}
		}

		private decimal InitialAmount
		{
			get
			{
				var controller = this.GetController (3) as DecimalStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				return controller.Value.GetValueOrDefault ();
			}
			set
			{
				var controller = this.GetController (3) as DecimalStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				controller.Value = value;
			}
		}

		private System.DateTime? ExtraDate
		{
			get
			{
				var controller = this.GetController (4) as DateStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				return controller.Value;
			}
			set
			{
				var controller = this.GetController (4) as DateStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				controller.Value = value;
			}
		}

		private decimal? ExtraAmount
		{
			get
			{
				var controller = this.GetController (5) as DecimalStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				return controller.Value;
			}
			set
			{
				var controller = this.GetController (5) as DecimalStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				controller.Value = value;
			}
		}

		private bool AmortizationSuppl
		{
			get
			{
				var controller = this.GetController (6) as BoolStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				return controller.Value;
			}
			set
			{
				var controller = this.GetController (6) as BoolStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				controller.Value = value;
			}
		}

		private System.DateTime? AdjustDate
		{
			get
			{
				var controller = this.GetController (7) as DateStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				return controller.Value;
			}
			set
			{
				var controller = this.GetController (7) as DateStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				controller.Value = value;
			}
		}

		private decimal? AdjustAmount
		{
			get
			{
				var controller = this.GetController (8) as DecimalStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				return controller.Value;
			}
			set
			{
				var controller = this.GetController (8) as DecimalStackedController;
				System.Diagnostics.Debug.Assert (controller != null);
				controller.Value = value;
			}
		}

		private object GetArgument(int rank)
		{
			var controller = this.GetController (rank);

			if (controller is DecimalStackedController)
			{
				return (controller as DecimalStackedController).Value;
			}
			else if (controller is IntStackedController)
			{
				return (controller as IntStackedController).Value;
			}
			else if (controller is BoolStackedController)
			{
				return (controller as BoolStackedController).Value;
			}
			else if (controller is DateStackedController)
			{
				return (controller as DateStackedController).Value;
			}
			else if (controller is TextStackedController)
			{
				return (controller as TextStackedController).Value;
			}
			else
			{
				throw new System.InvalidOperationException (string.Format ("Invalid Argument {0}", rank));
			}
		}

		private void SetArgument(int rank, object value)
		{
			if (value == null)
			{
				return;
			}

			var controller = this.GetController (rank);

			if (controller is DecimalStackedController)
			{
				(controller as DecimalStackedController).Value = (decimal) value;
			}
			else if (controller is IntStackedController)
			{
				(controller as IntStackedController).Value = (int) value;
			}
			else if (controller is BoolStackedController)
			{
				(controller as BoolStackedController).Value = (bool) value;
			}
			else if (controller is DateStackedController)
			{
				(controller as DateStackedController).Value = (System.DateTime) value;
			}
			else if (controller is TextStackedController)
			{
				(controller as TextStackedController).Value = (string) value;
			}
			else
			{
				throw new System.InvalidOperationException (string.Format ("Invalid Argument {0}", rank));
			}
		}

		public ExpressionSimulationParams Params
		{
			//	Spécifie tous les paramètres pour la simulation. Le dictionnaire pour
			//	les arguments peut contenir des arguments qui n'ont pas de sens ici.
			//	Ils sont alors simplement conservés (set), pour être redonnés tels quels
			//	en sortie (get).
			get
			{
				var p = new ExpressionSimulationParams (this.Range, this.Periodicity, this.InitialAmount,
					this.ExtraDate, this.ExtraAmount, this.AmortizationSuppl, this.AdjustDate, this.AdjustAmount);

				//	Arguments édités -> this.lastArguments.
				foreach (var pair in this.controllerRanks)
				{
					var field = pair.Key;
					var rank  = pair.Value;

					var val = this.GetArgument (rank);
					this.lastArguments[field] = val;
				}

				//	this.lastArguments -> output.
				foreach (var pair in this.lastArguments)
				{
					var field = pair.Key;
					var val   = pair.Value;

					p.Arguments.Add (field, val);
				}

				return p;
			}
			set
			{
				this.Range             = value.Range;
				this.Periodicity       = value.Periodicity;
				this.InitialAmount     = value.InitialAmount;
				this.ExtraDate         = value.ExtraDate;
				this.ExtraAmount       = value.ExtraAmount;
				this.AmortizationSuppl = value.AmortizationSuppl;
				this.AdjustDate        = value.AdjustDate;
				this.AdjustAmount      = value.AdjustAmount;

				//	Input -> this.lastArguments.
				foreach (var pair in value.Arguments)
				{
					var field = pair.Key;
					var val   = pair.Value;

					this.lastArguments[field] = val;
				}

				//	this.lastArguments -> arguments en édition.
				foreach (var pair in this.lastArguments)
				{
					var field = pair.Key;
					var val   = pair.Value;

					int rank;
					if (this.controllerRanks.TryGetValue (field, out rank))
					{
						this.SetArgument (rank, val);
					}
				}
			}
		}

		protected override void UpdateWidgets(StackedControllerDescription description)
		{
		}


		private IEnumerable<ObjectField> ArgumentFields
		{
			get
			{
				foreach (var guid in this.argumentGuids)
				{
					yield return ArgumentsLogic.GetObjectField (this.accessor, guid);
				}
			}
		}


		#region Periodicity
		private static int PeriodicityToRank(Periodicity periodicity)
		{
			var list = EnumDictionaries.DictPeriodicities.Select (x => x.Key).ToList ();
			return list.IndexOf ((int) periodicity);
		}

		private static Periodicity RankToPeriodicity(int index)
		{
			var list = EnumDictionaries.DictPeriodicities.Select (x => x.Key).ToArray ();

			if (index >= 0 && index < list.Length)
			{
				return (Periodicity) list[index];
			}
			else
			{
				return Periodicity.Unknown;
			}
		}

		private static string PeriodicityLabels
		{
			get
			{
				return string.Join ("<br/>", EnumDictionaries.DictPeriodicities.Select (x => x.Value));
			}
		}
		#endregion


		#region Helpers
		public static void Show(Widget target, DataAccessor accessor, IEnumerable<Guid> argumentGuids, System.Action action)
		{
			if (target != null)
			{
				var popup = new ExpressionSimulationParamsPopup (accessor, argumentGuids)
				{
					Params = LocalSettings.ExpressionSimulationParams,
				};

				popup.Create (target, leftOrRight: true);

				popup.ButtonClicked += delegate (object sender, string name)
				{
					if (name == "ok")
					{
						LocalSettings.ExpressionSimulationParams = popup.Params;
						action ();
					}
				};
			}
		}
		#endregion


		private readonly Guid[]					argumentGuids;
		private readonly ObjectField[]			fields;
		private readonly Dictionary<ObjectField, int> controllerRanks;
		private readonly Dictionary<ObjectField, object> lastArguments;
	}
}