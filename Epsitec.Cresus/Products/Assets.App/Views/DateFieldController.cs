﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Helpers;
using Epsitec.Cresus.Assets.Server.Helpers;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class DateFieldController : AbstractFieldController
	{
		public DateFieldController(DataAccessor accessor)
			: base (accessor)
		{
		}


		public System.DateTime?					Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (this.value != value)
				{
					this.value = value;

					if (this.textField != null)
					{
						if (this.ignoreChanges.IsZero)
						{
							using (this.ignoreChanges.Enter ())
							{
								this.textField.Text = DateFieldController.ConvDateToString (this.value);
								this.textField.SelectAll ();
							}
						}
					}
				}
			}
		}

		public TextField TextField
		{
			get
			{
				return this.textField;
			}
		}

		private void UpdateValue()
		{
			using (this.ignoreChanges.Enter ())
			{
				this.textField.Text = DateFieldController.ConvDateToString (this.value);
				this.textField.SelectAll ();
			}
		}

		protected override void ClearValue()
		{
			this.Value = null;
			this.UpdateButtons ();
			this.OnValueEdited (this.Field);
		}

		protected override void UpdatePropertyState()
		{
			base.UpdatePropertyState ();

			AbstractFieldController.UpdateBackColor (this.textField, this.BackgroundColor);
			this.UpdateTextField (this.textField);
		}


		public override void CreateUI(Widget parent)
		{
			base.CreateUI (parent);

			this.textField = new TextField
			{
				Parent          = this.frameBox,
				PreferredWidth  = DateFieldController.fieldWidth,
				PreferredHeight = AbstractFieldController.lineHeight,
				Dock            = DockStyle.Left,
				TabIndex        = this.TabIndex,
				Text            = DateFieldController.ConvDateToString (this.value),
			};

			this.minusButton = new IconButton
			{
				Parent        = this.frameBox,
				IconUri       = Misc.GetResourceIconUri ("Date.Minus"),
				AutoFocus     = false,
				Dock          = DockStyle.Left,
				PreferredSize = new Size (AbstractFieldController.lineHeight, AbstractFieldController.lineHeight),
			};

			this.plusButton = new IconButton
			{
				Parent        = this.frameBox,
				IconUri       = Misc.GetResourceIconUri ("Date.Plus"),
				AutoFocus     = false,
				Dock          = DockStyle.Left,
				PreferredSize = new Size (AbstractFieldController.lineHeight, AbstractFieldController.lineHeight),
			};

			this.textField.TextChanged += delegate
			{
				if (this.ignoreChanges.IsZero)
				{
					using (this.ignoreChanges.Enter ())
					{
						this.Value = DateFieldController.ConvStringToDate (this.textField.Text);
						this.UpdateButtons ();
						this.OnValueEdited (this.Field);
					}
				}
			};

			this.textField.CursorChanged += delegate
			{
				this.UpdateButtons ();
				this.OnCursorChanged ();
			};

			this.textField.SelectionChanged += delegate
			{
				this.UpdateButtons ();
				this.OnCursorChanged ();
			};

			this.textField.KeyboardFocusChanged += delegate (object sender, DependencyPropertyChangedEventArgs e)
			{
				bool focused = (bool) e.NewValue;

				if (focused)  // pris le focus ?
				{
					this.SetFocus ();
				}
				else  // perdu le focus ?
				{
					this.UpdateValue ();
				}
			};

			this.minusButton.Clicked += delegate
			{
				this.AddValue (-1);
			};

			this.plusButton.Clicked += delegate
			{
				this.AddValue (1);
			};
		}

		public override void SetFocus()
		{
			this.textField.SelectAll ();
			this.textField.Focus ();

			base.SetFocus ();
		}


		private void UpdateButtons()
		{
			var part = this.SelectedPart;

			this.minusButton.Enable = part != Part.Unknown;
			this.plusButton.Enable  = part != Part.Unknown;

			switch (part)
			{
				case Part.Day:
					ToolTip.Default.SetToolTip (this.minusButton, "Jour précédent");
					ToolTip.Default.SetToolTip (this.plusButton,  "Jour suivant");
					break;

				case Part.Month:
					ToolTip.Default.SetToolTip (this.minusButton, "Mois précédent");
					ToolTip.Default.SetToolTip (this.plusButton,  "Mois suivant");
					break;

				case Part.Year:
					ToolTip.Default.SetToolTip (this.minusButton, "Année précédente");
					ToolTip.Default.SetToolTip (this.plusButton,  "Année suivante");
					break;
			}
		}


		private void AddValue(int value)
		{
			var part = this.SelectedPart;

			switch (part)
			{
				case Part.Day:
					this.AddDays (value);
					break;

				case Part.Month:
					this.AddMonths (value);
					break;

				default:
					this.AddYears (value);
					break;
			}

			this.SelectPart (part);
			this.UpdateButtons ();
		}

		private void AddYears(int years)
		{
			if (this.value.HasValue)
			{
				this.Value = this.value.Value.AddYears (years);
				this.textField.Focus ();
				this.OnValueEdited (this.Field);
			}
		}

		private void AddMonths(int days)
		{
			if (this.value.HasValue)
			{
				this.Value = this.value.Value.AddMonths (days);
				this.textField.Focus ();
				this.OnValueEdited (this.Field);
			}
		}

		private void AddDays(int days)
		{
			if (this.value.HasValue)
			{
				this.Value = this.value.Value.AddDays (days);
				this.textField.Focus ();
				this.OnValueEdited (this.Field);
			}
		}

		public Part SelectedPart
		{
			//	Détermine la partie de la date actuellement en édition.
			get
			{
				var text = this.textField.Text;

				if (!string.IsNullOrEmpty (text))
				{
					int f = System.Math.Min (this.textField.CursorFrom, this.textField.CursorTo);
					int t = System.Math.Max (this.textField.CursorFrom, this.textField.CursorTo);

					if (f == 0 && t == this.textField.Text.Length)
					{
						return Part.Year;
					}

					text = text.Replace (' ', '.');
					text = text.Replace ('/', '.');
					text = text.Replace (',', '.');
					text = text.Replace ('-', '.');

					var words = text.Split ('.');

					int part = 0;
					int i = 0;
					foreach (var word in words)
					{
						i += word.Length+1;

						if (this.textField.Cursor < i)
						{
							switch (part)
							{
								case 0:
									return Part.Day;

								case 1:
									return Part.Month;

								case 2:
									return Part.Year;
							}
						}

						part++;
					}
				}

				return Part.Unknown;
			}
		}

		private void SelectPart(Part part)
		{
			//	Sélectionne une partie du texte en édition.
			if (this.textField.Text.Length == 10)  // jj.mm.aaaa ?
			{
				switch (part)
				{
					case Part.Day:
						this.textField.CursorFrom = 0;
						this.textField.CursorTo   = 2;  // [31].03.2015
						break;

					case Part.Month:
						this.textField.CursorFrom = 3;
						this.textField.CursorTo   = 3+2;  // 31.[03].2015
						break;

					case Part.Year:
						this.textField.CursorFrom = 6;
						this.textField.CursorTo   = 6+4;  // 31.03.[2015]
						break;
				}
			}
		}

		public enum Part
		{
			Unknown,
			Day,
			Month,
			Year,
		}


		private static string ConvDateToString(System.DateTime? value)
		{
			return TypeConverters.DateToString (value);
		}

		private static System.DateTime? ConvStringToDate(string text)
		{
			return TypeConverters.ParseDate (text);
		}


		#region Events handler
		protected void OnCursorChanged()
		{
			this.CursorChanged.Raise (this);
		}

		public event EventHandler CursorChanged;
		#endregion


		private const int fieldWidth      = 70;
		public  const int controllerWidth = DateFieldController.fieldWidth + AbstractFieldController.lineHeight*2;


		private TextField						textField;
		private IconButton						minusButton;
		private IconButton						plusButton;
		private System.DateTime?				value;
	}
}
