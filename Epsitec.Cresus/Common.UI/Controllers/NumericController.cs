//	Copyright � 2006-2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using System.Collections.Generic;

using Epsitec.Common.UI;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;

[assembly: Controller (typeof (Epsitec.Common.UI.Controllers.NumericController))]

namespace Epsitec.Common.UI.Controllers
{
	public class NumericController : AbstractController, Widgets.Layouts.IGridPermeable
	{
		public NumericController(string parameter)
		{
		}

		public override object GetActualValue()
		{
			return this.field.Text;
		}

		protected override Widgets.Layouts.IGridPermeable GetGridPermeableLayoutHelper()
		{
			return this;
		}

		protected override void CreateUserInterface(INamedType namedType, Caption caption)
		{
			this.Placeholder.ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow;

			if (this.Placeholder.IsReadOnlyValueBinding)
			{
				this.CreateReadOnlyUserInterface (caption);
			}
			else
			{
				this.CreateReadWriteUserInterface (caption, namedType);
			}
		}

		private void CreateReadOnlyUserInterface(Caption caption)
		{
			this.label = new StaticText ();
			this.field = null;

			this.label.HorizontalAlignment = HorizontalAlignment.Stretch;
			this.label.VerticalAlignment = VerticalAlignment.BaseLine;
			this.label.ContentAlignment = Drawing.ContentAlignment.MiddleLeft;
			this.label.Dock = DockStyle.Stacked;

			ToolTip.Default.SetToolTip (this.label, caption.Description);

			this.AddWidget (this.label);
		}

		private void CreateReadWriteUserInterface(Caption caption, INamedType namedType)
		{
			this.label = new StaticText ();
			this.field = new TextFieldSlider ();

			this.label.HorizontalAlignment = HorizontalAlignment.Right;
			this.label.VerticalAlignment = VerticalAlignment.BaseLine;
			this.label.ContentAlignment = Drawing.ContentAlignment.MiddleRight;
			this.label.Dock = DockStyle.Stacked;

			if (caption.HasLabels)
			{
				if (caption.Id.IsValid)
				{
					this.label.CaptionId = caption.Id;
				}
				else
				{
					this.label.Text = Collection.Extract<string> (caption.Labels, 0);
				}
				this.label.PreferredWidth = this.label.GetBestFitSize ().Width;
				this.label.Margins = new Drawing.Margins (4, 4, 0, 0);
			}

			this.field.HorizontalAlignment = HorizontalAlignment.Stretch;
			this.field.VerticalAlignment = VerticalAlignment.BaseLine;
			this.field.TextChanged += this.HandleFieldTextChanged;
			this.field.PreferredWidth = 40;
			
			this.field.TabIndex = 1;
			this.field.TabNavigationMode = TabNavigationMode.ActivateOnTab;
			this.field.Dock = DockStyle.Stacked;

			INumericType numType = namedType as INumericType;

			if (numType != null)
			{
				if (!numType.Range.IsEmpty)
				{
					this.field.MinValue   = numType.Range.Minimum;
					this.field.MaxValue   = numType.Range.Maximum;
					this.field.Resolution = numType.Range.Resolution;
				}

				if (!numType.PreferredRange.IsEmpty)
				{
					this.field.PreferredRange = numType.PreferredRange;
				}
			}
			
			this.AddWidget (this.label);
			this.AddWidget (this.field);

			this.validator = new Validators.ControllerBasedValidator (this.field, this);
		}

		protected override void PrepareUserInterfaceDisposal()
		{
			base.PrepareUserInterfaceDisposal ();

			if (this.field != null)
			{
				this.field.TextChanged -= this.HandleFieldTextChanged;
			}
		}

		protected override void RefreshUserInterface(object oldValue, object newValue)
		{
			string text = "";
			
			if ((newValue != UndefinedValue.Value) &&
				(newValue != InvalidValue.Value) &&
				(newValue != null))
			{
				text = this.ConvertFromValue (newValue);
			}
			
			if (this.field != null)
			{
				this.field.Text = text;
			}
			else
			{
				this.label.Text = text;
				this.label.PreferredWidth = this.label.GetBestFitSize ().Width;
			}
		}
		
		private void HandleFieldTextChanged(object sender)
		{
			if (this.field.IsValid)
			{
				this.OnActualValueChanged ();
			}
		}

		private string ConvertFromValue(object newValue)
		{
			return newValue.ToString ();
		}
		
		#region IGridPermeable Members

		IEnumerable<Widgets.Layouts.PermeableCell> Widgets.Layouts.IGridPermeable.GetChildren(int column, int row, int columnSpan, int rowSpan)
		{
			yield return new Widgets.Layouts.PermeableCell (this.label, column+0, row+0, 1, 1);
			yield return new Widgets.Layouts.PermeableCell (this.field, column+1, row+0, columnSpan-1, 1);
		}

		bool Widgets.Layouts.IGridPermeable.UpdateGridSpan(ref int columnSpan, ref int rowSpan)
		{
			columnSpan = System.Math.Max (columnSpan, 2);
			rowSpan    = System.Math.Max (rowSpan, 1);
			
			return true;
		}

		#endregion
		
		private TextFieldSlider field;
		private StaticText label;
		private IValidator validator;
	}
}
