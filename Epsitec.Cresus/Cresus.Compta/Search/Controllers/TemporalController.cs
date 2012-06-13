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
	public class TemporalController
	{
		public TemporalController(TemporalData data)
		{
			this.data = data;

			this.monthsInfo     = new List<ButtonInfo> ();
			this.quartersInfo   = new List<ButtonInfo> ();
			this.monthButtons   = new List<Button> ();
			this.quarterButtons = new List<Button> ();
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


		public FrameBox CreateUI(FrameBox parent, System.Func<TemporalData> getPériode, System.Action filterStartAction)
		{
			this.getPériode        = getPériode;
			this.filterStartAction = filterStartAction;

			this.mainFrame = new FrameBox
			{
				Parent          = parent,
				PreferredHeight = TemporalController.toolbarHeight,
				Dock            = DockStyle.Fill,
				Margins         = new Margins (0, 0, 0, 0),
			};

			this.CreateRegularFilterUI (this.mainFrame);
			this.CreateAnyFilterUI (this.mainFrame);

			this.UpdateWidgets ();

			return this.mainFrame;
		}

		public void UpdatePériode()
		{
			if (this.data.BeginDate.HasValue)
			{
				this.data.BeginDate = Dates.Max (this.data.BeginDate, this.getPériode ().BeginDate);
			}

			if (this.data.EndDate.HasValue)
			{
				this.data.EndDate = Dates.Min (this.data.EndDate, this.getPériode ().EndDate);
			}

			if (this.data.BeginDate.HasValue && this.data.EndDate.HasValue && this.data.BeginDate.Value > this.data.EndDate.Value)
			{
				this.data.Clear ();
				this.filterStartAction ();
			}

			this.CreateRegularWidgetsUI ();
			this.UpdateWidgets ();
		}


		private void CreateRegularFilterUI(FrameBox parent)
		{
			this.regularFrame = new FrameBox
			{
				Parent          = parent,
				PreferredHeight = TemporalController.toolbarHeight,
				Dock            = DockStyle.Left,
			};

			this.CreateRegularWidgetsUI ();
		}

		private void CreateRegularWidgetsUI()
		{
			this.regularFrame.Children.Clear ();

			this.monthButtons.Clear ();
			this.monthsInfo.Clear ();
			this.quarterButtons.Clear ();
			this.quartersInfo.Clear ();

			var période = this.getPériode ();
			if (période != null)
			{
				this.firstYear  = période.BeginDate.Value.Year;
				this.firstMonth = période.BeginDate.Value.Month;
				this.monthCount = période.EndDate.Value.Month - this.firstMonth + 1 + (période.EndDate.Value.Year-période.BeginDate.Value.Year)*12;

				//	Crée les boutons pour les mois.
				{
					this.tabIndex = 0;

					int m = this.firstMonth;
					int y = période.BeginDate.Value.Year;

					while (true)
					{
						int i = this.GetMonthIndex (m, y);

						if (i == -1)
						{
							break;
						}

						this.CreateMonthButton (this.regularFrame, m, y);

						m++;

						if (m > 12)
						{
							m = 1;
							y++;
						}
					}

					double max = 0;
					foreach (var button in this.monthButtons)
					{
						max = System.Math.Max (max, System.Math.Floor (button.GetBestFitSize ().Width+1));
					}

					foreach (var button in this.monthButtons)
					{
						button.PreferredWidth = max;
					}

					if (this.monthButtons.Count != 0)
					{
						this.monthButtons.Last ().Margins = new Margins (0, 10, 0, 0);
					}
				}

				//	Crée les boutons pour les trimestres.
				{
					this.tabIndex = 0;

					int m = ((this.firstMonth+1)/3*3)+1;
					int y = période.BeginDate.Value.Year;

					while (true)
					{
						int i = this.GetQuarterIndex (m, y);

						if (i == -1)
						{
							break;
						}

						this.CreateQuarterButton (this.regularFrame, m, y);

						m += 3;

						if (m > 12)
						{
							m = 1;
							y++;
						}
					}

					if (this.quarterButtons.Count != 0)
					{
						this.quarterButtons.Last ().Margins = new Margins (0, 10, 0, 0);
					}
				}
			}

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

			this.regularClearButton = new IconButton
			{
				Parent          = this.regularFrame,
				IconUri         = UIBuilder.GetResourceIconUri ("Level.Clear"),
				ButtonStyle     = ButtonStyle.ToolItem,
				PreferredHeight = TemporalController.toolbarHeight,
				PreferredWidth  = TemporalController.toolbarHeight,
				Dock            = DockStyle.Left,
				Margins         = new Margins (0, 0, 0, 0),
			};

			ToolTip.Default.SetToolTip (this.regularClearButton, "Termine le filtre temporel");

			this.regularClearButton.Clicked += delegate
			{
				for (int i = 0; i < this.monthsInfo.Count; i++)
				{
					this.monthsInfo[i].Selected = false;
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
				PreferredHeight = TemporalController.toolbarHeight,
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
				UIBuilder.AdjustWidth (label);

				this.beginDateField = new TextFieldEx
				{
					Parent                       = this.anyFrame,
					PreferredWidth               = 90,
					PreferredHeight              = TemporalController.toolbarHeight,
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
				UIBuilder.AdjustWidth (label);

				this.endDateField = new TextFieldEx
				{
					Parent                       = this.anyFrame,
					PreferredWidth               = 90,
					PreferredHeight              = TemporalController.toolbarHeight,
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

			this.anyClearButton = new IconButton
			{
				Parent          = this.anyFrame,
				IconUri         = UIBuilder.GetResourceIconUri ("Level.Clear"),
				ButtonStyle     = ButtonStyle.ToolItem,
				PreferredHeight = TemporalController.toolbarHeight,
				PreferredWidth  = TemporalController.toolbarHeight,
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
				this.monthsInfo[index].Selected = !this.monthsInfo[index].Selected;

				int first = this.FirstMonth;
				int last  = this.LastMonth;

				if (first != -1 && last != -1)
				{
					for (int i = 0; i < this.monthsInfo.Count; i++)
					{
						this.monthsInfo[i].Selected = (i >= first && i <= last);
					}
				}
			}
			else
			{
				for (int i = 0; i < this.monthsInfo.Count; i++)
				{
					if (i != index)
					{
						this.monthsInfo[i].Selected = false;
					}
				}

				this.monthsInfo[index].Selected = !this.monthsInfo[index].Selected;
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
				this.quartersInfo[index].Selected = !this.quartersInfo[index].Selected;

				int first = this.FirstQuarter;
				int last  = this.LastQuarter;

				if (first != -1 && last != -1)
				{
					for (int i = 0; i < this.quartersInfo.Count; i++)
					{
						this.quartersInfo[i].Selected = (i >= first && i <= last);
					}
				}
			}
			else
			{
				for (int i = 0; i < this.quartersInfo.Count; i++)
				{
					if (i != index)
					{
						this.quartersInfo[i].Selected = false;
					}
				}

				this.quartersInfo[index].Selected = !this.quartersInfo[index].Selected;
			}

			this.QuartersToMonths ();
			this.UpdateTemporalData ();
		}


		private void MonthsToQuarters()
		{
			for (int i = 0; i < this.quartersInfo.Count; i++)
			{
				int month = this.quartersInfo[i].Month;
				int year  = this.quartersInfo[i].Year;

				var m1 = this.monthsInfo[this.GetMonthIndex (month+0, year)].Selected;
				var m2 = this.monthsInfo[this.GetMonthIndex (month+1, year)].Selected;
				var m3 = this.monthsInfo[this.GetMonthIndex (month+2, year)].Selected;

				this.quartersInfo[i].Selected = (m1 && m2 && m3);
			}
		}

		private void QuartersToMonths()
		{
			for (int i = 0; i < this.quartersInfo.Count; i++)
			{
				int month = this.quartersInfo[i].Month;
				int year  = this.quartersInfo[i].Year;

				this.monthsInfo[this.GetMonthIndex (month+0, year)].Selected = this.quartersInfo[i].Selected;
				this.monthsInfo[this.GetMonthIndex (month+1, year)].Selected = this.quartersInfo[i].Selected;
				this.monthsInfo[this.GetMonthIndex (month+2, year)].Selected = this.quartersInfo[i].Selected;
			}
		}

		private int GetMonthIndex(int month, int year)
		{
			var index = (month-this.firstMonth) + (year-this.firstYear)*12;

			if (index >= 0 && index < this.monthCount)
			{
				return index;
			}
			else
			{
				return -1;
			}
		}

		private int GetQuarterIndex(int month, int year)
		{
			month += (year-this.firstYear)*12;

			var first = ((this.firstMonth+1)/3*3)+1;
			var last  = ((this.firstMonth+this.monthCount-1)/3*3)+1;

			if (month >= first && month < last)
			{
				return month-first;
			}
			else
			{
				return -1;
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
					first = this.data.BeginDate.Value.Month + (this.data.BeginDate.Value.Year-this.getPériode ().BeginDate.Value.Year)*12 - this.firstMonth;
				}

				var last = int.MaxValue;
				if (this.data.EndDate.HasValue)
				{
					last = this.data.EndDate.Value.Month + (this.data.EndDate.Value.Year-this.getPériode ().BeginDate.Value.Year)*12 - this.firstMonth;
				}

				for (int i = 0; i < this.monthsInfo.Count; i++)
				{
					if (first == int.MinValue && last == int.MaxValue)
					{
						this.monthsInfo[i].Selected = false;
					}
					else
					{
						this.monthsInfo[i].Selected = (i >= first && i <= last);
					}
				}

				this.MonthsToQuarters ();

				for (int i = 0; i < this.monthsInfo.Count; i++)
				{
					this.ActivateButton (this.monthButtons[i], this.monthsInfo[i].Selected);
				}

				for (int i = 0; i < this.quartersInfo.Count; i++)
				{
					this.ActivateButton (this.quarterButtons[i], this.quartersInfo[i].Selected);
				}
			}

			bool hasFilter = !this.data.IsEmpty;

			//?var color = Color.FromAlphaColor (0.3, UIBuilder.SelectionColor);
			var color = Color.FromHexa ("ffffff");
			this.regularFrame.BackColor = hasFilter && this.HasColorizedHilite ? color : Color.Empty;
			this.anyFrame.BackColor     = hasFilter && this.HasColorizedHilite ? color : Color.Empty;

			this.regularClearButton.Enable = hasFilter;
			this.anyClearButton.Enable     = hasFilter;
		}


		private Button CreateMonthButton(FrameBox parent, int month, int year)
		{
			bool extendedMode = this.monthCount <= 12;
			var text = Dates.GetMonthShortDescription (new Date (year, month, 1));  // "Janv.", "Fév.", etc.

			if (!extendedMode)
			{
				text = text.Substring (0, 1);  // juste la première lettre
			}

			var tooltip = Dates.GetMonthDescription (new Date (year, month, 1)) + " " + Converters.IntToString (year);

			var button = this.CreateButton (parent, text, tooltip);

			this.monthButtons.Add (button);
			this.monthsInfo.Add (new ButtonInfo (month, year));

			return button;
		}

		private Button CreateQuarterButton(FrameBox parent, int month, int year)
		{
			int q = (month-1)/3;  // 0..3
			var text = "Q" + Converters.IntToString (q+1);  // Q1..Q4

			string tooltip = "";

			switch (q)
			{
				case 0:
					tooltip = "Premier trimestre";
					break;

				case 1:
					tooltip = "Deuxième trimestre";
					break;

				case 2:
					tooltip = "Troisième trimestre";
					break;

				case 3:
					tooltip = "Quatrième trimestre";
					break;

			}

			tooltip = tooltip + " " + Converters.IntToString (year);

			var button = this.CreateButton (parent, text, tooltip);

			this.quarterButtons.Add (button);
			this.quartersInfo.Add (new ButtonInfo (month, year));

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
				PreferredHeight = TemporalController.toolbarHeight,
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
					int m1 = this.monthsInfo[first].Month;
					int m2 = this.monthsInfo[last].Month;

					var year = this.getPériode ().BeginDate.Value.Year;
					var begin = this.NewDate (year, m1);  // par exemple 01.01.2012
					var end   = this.NewDate (year, m2);  // par exemple 01.12.2012

					this.data.BeginDate = begin;
					this.data.EndDate = Dates.AddDays (Dates.AddMonths (end, 1), -1);
				}
			}

			this.UpdateWidgets ();
			this.filterStartAction ();
		}

		private Date NewDate(int year, int month)
		{
			while (month > 12)
			{
				month -= 12;
				year++;
			}

			return new Date(year, month, 1);
		}


		private int FirstMonth
		{
			get
			{
				for (int i = 0; i < this.monthsInfo.Count; i++)
				{
					if (this.monthsInfo[i].Selected)
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
				for (int i = this.monthsInfo.Count-1; i >= 0; i--)
				{
					if (this.monthsInfo[i].Selected)
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
				for (int i = 0; i < this.quartersInfo.Count; i++)
				{
					if (this.quartersInfo[i].Selected)
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
				for (int i = this.quartersInfo.Count-1; i >= 0; i--)
				{
					if (this.quartersInfo[i].Selected)
					{
						return i;
					}
				}

				return -1;
			}
		}


		private class ButtonInfo
		{
			public ButtonInfo(int month, int year)
			{
				this.Month = month;
				this.Year  = year;
			}

			public int Month
			{
				get;
				private set;
			}

			public int Year
			{
				get;
				private set;
			}

			public bool Selected
			{
				get;
				set;
			}
		}


		private static readonly double					toolbarHeight = 20;

		private readonly TemporalData					data;
		private readonly List<ButtonInfo>				monthsInfo;
		private readonly List<ButtonInfo>				quartersInfo;
		private readonly List<Button>					monthButtons;
		private readonly List<Button>					quarterButtons;

		private System.Func<TemporalData>				getPériode;
		private System.Action							filterStartAction;
		private int										firstYear;
		private int										firstMonth;
		private int										monthCount;
		private FrameBox								mainFrame;
		private FrameBox								regularFrame;
		private FrameBox								anyFrame;
		private TextFieldEx								beginDateField;
		private TextFieldEx								endDateField;
		private IconButton								regularClearButton;
		private IconButton								anyClearButton;
		private int										tabIndex;
	}
}
