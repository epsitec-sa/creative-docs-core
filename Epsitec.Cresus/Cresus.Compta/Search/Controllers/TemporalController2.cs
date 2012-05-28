﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;
using Epsitec.Common.Support;

using Epsitec.Cresus.Compta.Entities;
using Epsitec.Cresus.Compta.Helpers;
using Epsitec.Cresus.Compta.Search.Data;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Search.Controllers
{
	/// <summary>
	/// Ce contrôleur gère la saisie d'une période comprise entre deux dates.
	/// </summary>
	public class TemporalController2
	{
		public TemporalController2(TemporalData data)
		{
			this.data = data;

			this.monthsSelected   = new List<bool> ();
			this.quartersSelected = new List<bool> ();
			this.monthButtons     = new List<Button> ();
			this.quarterButtons   = new List<Button> ();
		}


		public bool HasColorizedHilite
		{
			get;
			set;
		}

		public void Clear()
		{
			this.data.Clear ();
			this.UpdateTemporalData ();
		}


		public FrameBox CreateUI(FrameBox parent, System.Func<ComptaPériodeEntity> getPériode, System.Action filterStartAction)
		{
			this.getPériode        = getPériode;
			this.filterStartAction = filterStartAction;

			this.mainFrame = new FrameBox
			{
				Parent          = parent,
				PreferredHeight = TemporalController2.toolbarHeight,
				Dock            = DockStyle.Fill,
				Margins         = new Margins (0, 0, 0, 0),
			};

			this.CreateRegularFilterUI (this.mainFrame);
			this.CreateAnyFilterUI (this.mainFrame);

			this.UpdateWidgets ();

			return this.mainFrame;
		}


		private void CreateRegularFilterUI(FrameBox parent)
		{
			this.regularFrame = new FrameBox
			{
				Parent          = parent,
				PreferredHeight = TemporalController2.toolbarHeight,
				Dock            = DockStyle.Left,
			};

			this.tabIndex = 0;
			this.CreateMonthButton (this.regularFrame, "J", "01: Janvier",   16);
			this.CreateMonthButton (this.regularFrame, "F", "02: Février",   16);
			this.CreateMonthButton (this.regularFrame, "M", "03: Mars",      16);
			this.CreateMonthButton (this.regularFrame, "A", "04: Avril",     16);
			this.CreateMonthButton (this.regularFrame, "M", "05: Mai",       16);
			this.CreateMonthButton (this.regularFrame, "J", "06: Juin",      16);
			this.CreateMonthButton (this.regularFrame, "J", "07: Juillet",   16);
			this.CreateMonthButton (this.regularFrame, "A", "08: Août",      16);
			this.CreateMonthButton (this.regularFrame, "S", "09: Septembre", 16);
			this.CreateMonthButton (this.regularFrame, "O", "10: Octobre",   16);
			this.CreateMonthButton (this.regularFrame, "N", "11: Novembre",  16);
			this.CreateMonthButton (this.regularFrame, "D", "12: Décembre",  16);
			this.monthButtons.Last ().Margins = new Margins (0, 10, 0, 0);

			this.tabIndex = 0;
			this.CreateQuarterButton (this.regularFrame, "T1", "Premier trimestre (janvier à mars)");
			this.CreateQuarterButton (this.regularFrame, "T2", "Deuxième trimestre (avril à juin)");
			this.CreateQuarterButton (this.regularFrame, "T3", "Troisième trimestre (juillet à septembre)");
			this.CreateQuarterButton (this.regularFrame, "T4", "Quatrième trimestre (octobre à décembre)");
			this.quarterButtons.Last ().Margins = new Margins (0, 10, 0, 0);

			var anyButton = this.CreateButton (this.regularFrame, "Autre", "Choix d'une période quelconque", null, 5);
			anyButton.Margins = new Margins (0, 5, 0, 0);

			foreach (var button in this.monthButtons)
			{
				button.Clicked += new EventHandler<MessageEventArgs> (this.HandleButtonMonthClicked);
			}

			foreach (var button in this.quarterButtons)
			{
				button.Clicked += new EventHandler<MessageEventArgs> (this.HandleButtonQuarterClicked);
			}

			anyButton.Clicked += delegate
			{
				this.data.AnyMode = true;
				this.UpdateWidgets ();
			};

			this.regularClearButton = new GlyphButton
			{
				Parent          = this.regularFrame,
				GlyphShape      = GlyphShape.Close,
				ButtonStyle     = ButtonStyle.ToolItem,
				PreferredHeight = TemporalController2.toolbarHeight,
				PreferredWidth  = TemporalController2.toolbarHeight,
				Dock            = DockStyle.Left,
				Margins         = new Margins (0, 0, 0, 0),
			};

			ToolTip.Default.SetToolTip (this.regularClearButton, "Termine le filtre temporel");

			this.regularClearButton.Clicked += delegate
			{
				for (int i = 0; i < this.monthsSelected.Count; i++)
				{
					this.monthsSelected[i] = false;
				}

				this.MonthsToQuarters ();
				this.UpdateTemporalData ();
			};
		}

		private void CreateAnyFilterUI(FrameBox parent)
		{
			this.anyFrame = new FrameBox
			{
				Parent          = parent,
				PreferredHeight = TemporalController2.toolbarHeight,
				Dock            = DockStyle.Left,
			};

			{
				var label = new StaticText
				{
					Parent         = this.anyFrame,
					Text           = "Du",
					Dock           = DockStyle.Left,
					Margins        = new Margins (1, 5, 0, 0),
				};
				label.PreferredWidth = label.GetBestFitSize ().Width;

				this.beginDateField = new TextFieldEx
				{
					Parent                       = this.anyFrame,
					PreferredWidth               = 90,
					PreferredHeight              = TemporalController2.toolbarHeight,
					Dock                         = DockStyle.Left,
					DefocusAction                = DefocusAction.AutoAcceptOrRejectEdition,
					SwallowEscapeOnRejectEdition = true,
					SwallowReturnOnAcceptEdition = true,
					Margins                      = new Margins (0, 10, 0, 0),
					TabIndex                     = 1,
				};
			}

			{
				var label = new StaticText
				{
					Parent         = this.anyFrame,
					Text           = "Au",
					Dock           = DockStyle.Left,
					Margins        = new Margins (5, 5, 0, 0),
				};
				label.PreferredWidth = label.GetBestFitSize ().Width;

				this.endDateField = new TextFieldEx
				{
					Parent                       = this.anyFrame,
					PreferredWidth               = 90,
					PreferredHeight              = TemporalController2.toolbarHeight,
					Dock                         = DockStyle.Left,
					DefocusAction                = DefocusAction.AutoAcceptOrRejectEdition,
					SwallowEscapeOnRejectEdition = true,
					SwallowReturnOnAcceptEdition = true,
					Margins                      = new Margins (0, 10, 0, 0),
					TabIndex                     = 2,
				};
			}

			this.beginDateField.EditionAccepted += delegate
			{
				this.data.BeginDate = Converters.ParseDate (this.beginDateField.FormattedText);
				this.UpdateTemporalData ();
			};

			this.endDateField.EditionAccepted += delegate
			{
				this.data.EndDate = Converters.ParseDate (this.endDateField.FormattedText);
				this.UpdateTemporalData ();
			};

			var regularButton = this.CreateButton (this.anyFrame, "Mensuel", "Choix d'une période mensuelle", null, 5);
			regularButton.Margins = new Margins (10, 5, 0, 0);

			regularButton.Clicked += delegate
			{
				this.data.AnyMode = false;
				this.UpdateWidgets ();
			};

			this.anyClearButton = new GlyphButton
			{
				Parent          = this.anyFrame,
				GlyphShape      = GlyphShape.Close,
				ButtonStyle     = ButtonStyle.ToolItem,
				PreferredHeight = TemporalController2.toolbarHeight,
				PreferredWidth  = TemporalController2.toolbarHeight,
				Dock            = DockStyle.Left,
				Margins         = new Margins (0, 0, 0, 0),
			};

			ToolTip.Default.SetToolTip (this.anyClearButton, "Termine le filtre temporel");

			this.anyClearButton.Clicked += delegate
			{
				this.Clear ();
			};
		}

		private void HandleButtonMonthClicked(object sender, MessageEventArgs e)
		{
			var button = sender as Button;
			int index = button.TabIndex;

			if (e.Message.IsControlPressed)
			{
				this.monthsSelected[index] = !this.monthsSelected[index];

				int first = this.FirstMonth;
				int last  = this.LastMonth;

				if (first != -1 && last != -1)
				{
					for (int i = 0; i < this.monthsSelected.Count; i++)
					{
						this.monthsSelected[i] = (i >= first && i <= last);
					}
				}
			}
			else
			{
				for (int i = 0; i < this.monthsSelected.Count; i++)
				{
					if (i != index)
					{
						this.monthsSelected[i] = false;
					}
				}

				this.monthsSelected[index] = !this.monthsSelected[index];
			}

			this.MonthsToQuarters ();
			this.UpdateTemporalData ();
		}

		private void HandleButtonQuarterClicked(object sender, MessageEventArgs e)
		{
			var button = sender as Button;
			int index = button.TabIndex;

			if (e.Message.IsControlPressed)
			{
				this.quartersSelected[index] = !this.quartersSelected[index];

				int first = this.FirstQuarter;
				int last  = this.LastQuarter;

				if (first != -1 && last != -1)
				{
					for (int i = 0; i < this.quartersSelected.Count; i++)
					{
						this.quartersSelected[i] = (i >= first && i <= last);
					}
				}
			}
			else
			{
				for (int i = 0; i < this.quartersSelected.Count; i++)
				{
					if (i != index)
					{
						this.quartersSelected[i] = false;
					}
				}

				this.quartersSelected[index] = !this.quartersSelected[index];
			}

			this.QuartersToMonths ();
			this.UpdateTemporalData ();
		}


		private void MonthsToQuarters()
		{
			for (int i = 0; i < 4; i++)
			{
				if (this.monthsSelected[i*3] == this.monthsSelected[i*3+1] &&
					this.monthsSelected[i*3] == this.monthsSelected[i*3+2])
				{
					this.quartersSelected[i] = this.monthsSelected[i*3];
				}
				else
				{
					this.quartersSelected[i] = false;
				}
			}
		}

		private void QuartersToMonths()
		{
			for (int i = 0; i < 4; i++)
			{
				this.monthsSelected[i*3+0] = this.quartersSelected[i];
				this.monthsSelected[i*3+1] = this.quartersSelected[i];
				this.monthsSelected[i*3+2] = this.quartersSelected[i];
			}
		}


		public void Update()
		{
			this.UpdateWidgets ();
		}

		private void UpdateWidgets()
		{
			this.regularFrame.Visibility = !this.data.AnyMode;
			this.anyFrame.Visibility     =  this.data.AnyMode;

			if (this.data.AnyMode)
			{
				this.beginDateField.FormattedText = Converters.DateToString (this.data.BeginDate);
				this.endDateField.FormattedText   = Converters.DateToString (this.data.EndDate);
			}
			else
			{
				var first = int.MinValue;
				if (this.data.BeginDate.HasValue)
				{
					first = this.data.BeginDate.Value.Month-1;
				}

				var last = int.MaxValue;
				if (this.data.EndDate.HasValue)
				{
					last = this.data.EndDate.Value.Month-1;
				}

				for (int i = 0; i < this.monthsSelected.Count; i++)
				{
					if (first == int.MinValue && last == int.MaxValue)
					{
						this.monthsSelected[i] = false;
					}
					else
					{
						this.monthsSelected[i] = (i >= first && i <= last);
					}
				}

				this.MonthsToQuarters ();

				for (int i = 0; i < this.monthsSelected.Count; i++)
				{
					this.ActivateButton (this.monthButtons[i], this.monthsSelected[i]);
				}

				for (int i = 0; i < this.quartersSelected.Count; i++)
				{
					this.ActivateButton (this.quarterButtons[i], this.quartersSelected[i]);
				}
			}

			bool hasFilter = !this.data.IsEmpty;

			this.regularFrame.BackColor = hasFilter && this.HasColorizedHilite ? UIBuilder.SelectionColor : Color.Empty;
			this.anyFrame.BackColor     = hasFilter && this.HasColorizedHilite ? UIBuilder.SelectionColor : Color.Empty;

			this.regularClearButton.Enable = hasFilter;
			this.anyClearButton.Enable     = hasFilter;
		}


		private Button CreateMonthButton(FrameBox parent, FormattedText text, FormattedText tooltip, double? width = null, double margins = 0)
		{
			var button = this.CreateButton (parent, text, tooltip, width, margins);

			this.monthButtons.Add (button);
			this.monthsSelected.Add (false);

			return button;
		}

		private Button CreateQuarterButton(FrameBox parent, FormattedText text, FormattedText tooltip, double? width = null, double margins = 0)
		{
			var button = this.CreateButton (parent, text, tooltip, width, margins);

			this.quarterButtons.Add (button);
			this.quartersSelected.Add (false);

			return button;
		}

		private Button CreateButton(FrameBox parent, FormattedText text, FormattedText tooltip, double? width = null, double margins = 0)
		{
			var button = new Button
			{
				Parent          = parent,
				FormattedText   = text,
				Name            = text.ToString (),
				ButtonStyle     = ButtonStyle.ToolItem,
				AutoFocus       = false,
				PreferredHeight = TemporalController2.toolbarHeight,
				Dock            = DockStyle.Left,
				Margins         = new Margins (0, -1, 0, 0),
				TabIndex        = this.tabIndex++,
			};

			if (width.HasValue)
			{
				button.PreferredWidth = width.Value;
			}
			else
			{
				button.PreferredWidth = button.GetBestFitSize ().Width + margins;
			}

			ToolTip.Default.SetToolTip (button, tooltip);

			return button;
		}

		private void ActivateButton(Button button, bool active)
		{
			button.ActiveState = active ? ActiveState.Yes : ActiveState.No;

			if (active)
			{
				button.FormattedText = FormattedText.Concat (button.Name).ApplyFontColor (Color.FromName ("White")).ApplyBold ();
			}
			else
			{
				button.FormattedText = button.Name;
			}
		}

		private void CreateSeparator(FrameBox parent)
		{
			new Separator
			{
				Parent         = parent,
				PreferredWidth = 1,
				IsVerticalLine = true,
				Dock           = DockStyle.Left,
				Margins        = new Margins (10, 10, 0, 0),
			};
		}


		private void UpdateTemporalData()
		{
			if (!this.data.AnyMode)
			{
				int first = this.FirstMonth;
				int last  = this.LastMonth;

				if (first == -1 || last == -1)
				{
					this.data.Clear ();
				}
				else
				{
					var year = this.getPériode ().DateDébut.Year;
					var begin = new Date (year, first+1, 1);  // par exemple 01.01.2012
					var end   = new Date (year, last+1,  1);  // par exemple 01.12.2012

					this.data.BeginDate = begin;
					this.data.EndDate = Dates.AddDays (Dates.AddMonths (end, 1), -1);
				}
			}

			this.UpdateWidgets ();
			this.filterStartAction ();
		}


		private int FirstMonth
		{
			get
			{
				for (int i = 0; i < this.monthsSelected.Count; i++)
				{
					if (this.monthsSelected[i])
					{
						return i;
					}
				}

				return -1;
			}
		}

		private int LastMonth
		{
			get
			{
				for (int i = this.monthsSelected.Count-1; i >= 0; i--)
				{
					if (this.monthsSelected[i])
					{
						return i;
					}
				}

				return -1;
			}
		}

		private int FirstQuarter
		{
			get
			{
				for (int i = 0; i < this.quartersSelected.Count; i++)
				{
					if (this.quartersSelected[i])
					{
						return i;
					}
				}

				return -1;
			}
		}

		private int LastQuarter
		{
			get
			{
				for (int i = this.quartersSelected.Count-1; i >= 0; i--)
				{
					if (this.quartersSelected[i])
					{
						return i;
					}
				}

				return -1;
			}
		}


		private static readonly double					toolbarHeight = 20;

		private readonly TemporalData					data;
		private readonly List<bool>						monthsSelected;
		private readonly List<bool>						quartersSelected;
		private readonly List<Button>					monthButtons;
		private readonly List<Button>					quarterButtons;

		private System.Func<ComptaPériodeEntity>		getPériode;
		private System.Action							filterStartAction;
		private FrameBox								mainFrame;
		private FrameBox								regularFrame;
		private FrameBox								anyFrame;
		private TextFieldEx								beginDateField;
		private TextFieldEx								endDateField;
		private GlyphButton								regularClearButton;
		private GlyphButton								anyClearButton;
		private int										tabIndex;
	}
}
