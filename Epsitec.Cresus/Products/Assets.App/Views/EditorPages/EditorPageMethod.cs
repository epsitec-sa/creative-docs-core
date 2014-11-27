﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Helpers;
using Epsitec.Cresus.Assets.App.NodeGetters;
using Epsitec.Cresus.Assets.App.Popups;
using Epsitec.Cresus.Assets.App.Settings;
using Epsitec.Cresus.Assets.App.Views.FieldControllers;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.Expression;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Views.EditorPages
{
	public class EditorPageMethod : AbstractEditorPage
	{
		public EditorPageMethod(DataAccessor accessor, BaseType baseType, bool isTimeless)
			: base (accessor, baseType, isTimeless)
		{
			this.sampleButtons = new List<Button> ();
		}


		protected internal override void CreateUI(Widget parent)
		{
			parent = this.CreateScrollable (parent, hasColorsExplanation: false);

			this.nameController       = this.CreateStringController (parent, ObjectField.Name);
			this.methodController     = this.CreateEnumController   (parent, ObjectField.AmortizationMethod, EnumDictionaries.DictAmortizationMethods);
			this.expressionController = this.CreateStringController (parent, ObjectField.Expression, lineCount: 20, maxLength: 10000);

			this.CreateSampleButtons (parent);
			this.CreateActionButtons (parent);
			this.CreateOutputConsole (parent);

			this.methodController.ValueEdited += delegate
			{
				this.UpdateControllers ();
			};
		}

		public override void SetObject(Guid objectGuid, Timestamp timestamp)
		{
			base.SetObject (objectGuid, timestamp);
			this.UpdateControllers ();
		}

		private void CreateSampleButtons(Widget parent)
		{
			var frame = new FrameBox
			{
				Parent          = parent,
				PreferredHeight = 21,
				Dock            = DockStyle.Top,
				Margins         = new Margins (110-21-5, 0, 5, 0),
			};

			bool locked = true;

			this.lockButton = new IconButton
			{
				Parent          = frame,
				PreferredWidth  = 21,
				IconUri         = Misc.GetResourceIconUri ("Method.Locked"),
				ButtonStyle     = ButtonStyle.ToolItem,
				AutoFocus       = false,
				Dock            = DockStyle.Left,
				Margins         = new Margins (0, 5, 0, 0),
			};

			ToolTip.Default.SetToolTip (this.lockButton, Res.Strings.EditorPages.Method.Locked.Tooltip.ToString ());

			this.lockButton.Clicked += delegate
			{
				locked = !locked;
				this.lockButton.IconUri = Misc.GetResourceIconUri (locked ? "Method.Locked" : "Method.Unlocked");

				foreach (var button in this.sampleButtons)
				{
					button.Enable = !locked;
				}
			};

			foreach (var sample in EditorPageMethod.Samples)
			{
				var button = new Button
				{
					Parent          = frame,
					PreferredWidth  = 90,
					Text            = sample.Title,
					ButtonStyle     = ButtonStyle.Icon,
					AutoFocus       = false,
					Enable          = !locked,
					Dock            = DockStyle.Left,
					Margins         = new Margins (0, 5, 0, 0),
				};

				ToolTip.Default.SetToolTip (button, sample.Tooltip);

				button.Clicked += delegate
				{
					this.expressionController.Value = AmortizationExpression.GetDefaultExpression (sample.Type);
				};

				this.sampleButtons.Add (button);
			}
		}

		private void CreateActionButtons(Widget parent)
		{
			var frame = new FrameBox
			{
				Parent          = parent,
				PreferredHeight = 21,
				Dock            = DockStyle.Top,
				Margins         = new Margins (110, 0, 5, 0),
			};

			this.compileButton = new Button
			{
				Parent          = frame,
				PreferredWidth  = 90,
				Text            = "Compile",  // anglais, ne pas traduire
				ButtonStyle     = ButtonStyle.Icon,
				AutoFocus       = false,
				Dock            = DockStyle.Left,
				Margins         = new Margins (0, 5, 0, 0),
			};

			this.showButton = new Button
			{
				Parent          = frame,
				PreferredWidth  = 90,
				Text            = "Show C#",  // anglais, ne pas traduire
				ButtonStyle     = ButtonStyle.Icon,
				AutoFocus       = false,
				Dock            = DockStyle.Left,
				Margins         = new Margins (0, 5, 0, 0),
			};

			this.testButton = new Button
			{
				Parent          = frame,
				PreferredWidth  = 90,
				Text            = "Test",  // anglais, ne pas traduire
				ButtonStyle     = ButtonStyle.Icon,
				AutoFocus       = false,
				Dock            = DockStyle.Left,
				Margins         = new Margins (0, 5, 0, 0),
			};

			this.simulButton = new Button
			{
				Parent          = frame,
				PreferredWidth  = 90,
				Text            = "Simulation",  // anglais, ne pas traduire
				ButtonStyle     = ButtonStyle.Icon,
				AutoFocus       = false,
				Dock            = DockStyle.Left,
				Margins         = new Margins (0, 5, 0, 0),
			};

			this.compileButton.Clicked += delegate
			{
				this.Compile ();
			};

			this.showButton.Clicked += delegate
			{
				this.Show ();
			};

			this.testButton.Clicked += delegate
			{
				this.Test ();
			};

			this.simulButton.Clicked += delegate
			{
				this.Simulation ();
			};
		}

		private void CreateOutputConsole(Widget parent)
		{
			this.outputConsole = new TextFieldMulti
			{
				Parent          = parent,
				IsReadOnly      = true,
				PreferredHeight = 100,
				Dock            = DockStyle.Top,
				Margins         = new Margins (110, 40, 5, 0),
			};
		}

		private void UpdateControllers()
		{
			bool expressionEnable = (this.CurrentMethod == AmortizationMethod.Custom);

			this.expressionController.SetFont (Font.GetFont ("Courier New", "Regular"));  // bof

			this.expressionController.IsReadOnly = !expressionEnable;
			this.lockButton          .Visibility =  expressionEnable;
			this.compileButton       .Visibility =  expressionEnable;
			this.showButton          .Visibility =  expressionEnable;
			this.testButton          .Visibility =  expressionEnable;
			this.simulButton         .Visibility =  expressionEnable;
			this.outputConsole       .Visibility =  expressionEnable;

			foreach (var button in this.sampleButtons)
			{
				button.Visibility = expressionEnable;
			}

			if (expressionEnable)
			{
				if (string.IsNullOrEmpty (this.expressionController.Value))
				{
					this.expressionController.Value = AmortizationExpression.GetDefaultExpression (SampleType.RateLinear);
				}
			}
			else
			{
				this.expressionController.Value = null;
			}

			this.outputConsole.Text = null;  // efface le message précédent
		}

		private string Compile()
		{
			this.outputConsole.Text = "Compile started";
			this.outputConsole.Window.ForceLayout ();

			var startTime = System.DateTime.Now;
			string err;

			using (var e = new AmortizationExpression (this.expressionController.Value))
			{
				var elapsedTime = System.DateTime.Now - startTime;
				var message = string.Format ("Time elapsed {0}", elapsedTime.ToString ());

				if (string.IsNullOrEmpty (e.Error))
				{
					message += "<br/>";
					message += "Compile succeeded";  // anglais, ne pas traduire

					err = null;  // ok
				}
				else
				{
					message += "<br/>";
					message += e.Error;

					err = e.Error;
				}

				this.outputConsole.Text = message;  // affiche le nouveau message
			}

			return err;
		}

		private void Show()
		{
			var expression = AmortizationExpression.GetDebugExpression (this.expressionController.Value);
			ShowExpressionPopup.Show (this.showButton, this.accessor, expression);
		}

		private void Test()
		{
			var err = this.Compile ();

			if (string.IsNullOrEmpty (err))  // ok ?
			{
				var expression = new AmortizationExpression (this.expressionController.Value);
				TestExpressionPopup.Show (this.testButton, this.accessor, expression);
			}
			else  // erreur ?
			{
				MessagePopup.ShowError (this.testButton, err);
			}
		}

		private void Simulation()
		{
			var err = this.Compile ();

			if (string.IsNullOrEmpty (err))  // ok ?
			{
				AmountExpressionSimulationPopup.Show (this.simulButton, this.accessor, delegate
				{
					var expression = new AmortizationExpression (this.expressionController.Value);
					var amount = LocalSettings.ExpressionSimulationAmount;

					var nodes = EditorPageMethod.ComputeSimulation (expression, amount);

					ShowExpressionSimulationPopup.Show (this.simulButton, this.accessor, nodes);
				});
			}
			else  // erreur ?
			{
				MessagePopup.ShowError (this.testButton, err);
			}
		}

		private static List<ExpressionSimulationNode> ComputeSimulation(AmortizationExpression expression, AmortizedAmount amount)
		{
			var nodes = new List<ExpressionSimulationNode> ();
			var date = new System.DateTime (2000-1, 12, 31);

			var baseAmount        = amount.BaseAmount;
			var monthCount        = amount.PeriodMonthCount;
			var periodicityFactor = amount.PeriodicityFactor;

			amount = AmortizedAmount.SetAmortizedAmount    (amount, baseAmount, baseAmount);
			amount = AmortizedAmount.SetRank               (amount, 0.0m);
			amount = AmortizedAmount.SetProrataNumerator   (amount, null);
			amount = AmortizedAmount.SetProrataDenominator (amount, null);

			for (int i=0; i<100; i++)  // nombre d'itérations arbitraire
			{
				date = date.AddMonths (monthCount);

				var initial = amount.InitialAmount.GetValueOrDefault ();
				var final = expression.Evaluate (amount).Value;

				var node = new ExpressionSimulationNode (i, date, initial, final);
				nodes.Add (node);

				amount = AmortizedAmount.SetAmortizedAmount (amount, final, baseAmount);
				amount = AmortizedAmount.SetRank (amount, (i+1)*periodicityFactor);
			}

			return nodes;
		}


		private AmortizationMethod CurrentMethod
		{
			get
			{
				if (this.methodController.Value.HasValue)
				{
					return (AmortizationMethod) this.methodController.Value.Value;
				}
				else
				{
					return AmortizationMethod.Unknown;
				}
			}
		}


		private static IEnumerable<Sample> Samples
		{
			get
			{
				yield return new Sample
				{
					Type    = SampleType.RateLinear,
					Title   = Res.Strings.EditorPages.Method.RateLinear.Button.ToString (),
					Tooltip = Res.Strings.EditorPages.Method.RateLinear.Tooltip.ToString (),
				};

				yield return new Sample
				{
					Type    = SampleType.RateDegressive,
					Title   = Res.Strings.EditorPages.Method.RateDegressive.Button.ToString (),
					Tooltip = Res.Strings.EditorPages.Method.RateDegressive.Tooltip.ToString (),
				};

				yield return new Sample
				{
					Type    = SampleType.YearsLinear,
					Title   = Res.Strings.EditorPages.Method.YearsLinear.Button.ToString (),
					Tooltip = Res.Strings.EditorPages.Method.YearsLinear.Tooltip.ToString (),
				};

				yield return new Sample
				{
					Type    = SampleType.YearsDegressive,
					Title   = Res.Strings.EditorPages.Method.YearsDegressive.Button.ToString (),
					Tooltip = Res.Strings.EditorPages.Method.YearsDegressive.Tooltip.ToString (),
				};
			}
		}


		private struct Sample
		{
			public SampleType					Type;
			public string						Title;
			public string						Tooltip;
		}


		private readonly List<Button>			sampleButtons;

		private StringFieldController			nameController;
		private EnumFieldController				methodController;
		private StringFieldController			expressionController;
		private IconButton						lockButton;
		private Button							compileButton;
		private Button							showButton;
		private Button							testButton;
		private Button							simulButton;
		private TextFieldMulti					outputConsole;
	}
}
