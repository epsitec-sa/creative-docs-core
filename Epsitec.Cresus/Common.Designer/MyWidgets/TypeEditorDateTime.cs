using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;
using Epsitec.Common.Types;

namespace Epsitec.Common.Designer.MyWidgets
{
	/// <summary>
	/// Widget permettant d'�diter un Caption.Type.
	/// </summary>
	public class TypeEditorDateTime : AbstractTypeEditor
	{
		public TypeEditorDateTime()
		{
			Widget group;

			this.CreateComboLabeled("R�solution", this, out group, out this.fieldResol);
			group.Dock = DockStyle.StackBegin;
			group.Margins = new Margins(0, 0, 0, 10);
			this.fieldResol.TextChanged += new EventHandler(this.HandleTextFieldChanged);
			this.fieldResol.Items.Add("Milliseconds");
			this.fieldResol.Items.Add("Seconds");
			this.fieldResol.Items.Add("Minutes");
			this.fieldResol.Items.Add("Hours");
			this.fieldResol.Items.Add("Days");
			this.fieldResol.Items.Add("Weeks");
			this.fieldResol.Items.Add("Months");
			this.fieldResol.Items.Add("Years");

			this.CreateStringLabeled("Date minimale", this, out group, out this.fieldMinDate);
			group.Dock = DockStyle.StackBegin;
			group.Margins = new Margins(0, 0, 0, 2);
			this.fieldMinDate.TextChanged += new EventHandler(this.HandleTextFieldChanged);

			this.CreateStringLabeled("Date maximale", this, out group, out this.fieldMaxDate);
			group.Dock = DockStyle.StackBegin;
			group.Margins = new Margins(0, 0, 0, 10);
			this.fieldMaxDate.TextChanged += new EventHandler(this.HandleTextFieldChanged);

			this.CreateStringLabeled("Heure minimale", this, out group, out this.fieldMinTime);
			group.Dock = DockStyle.StackBegin;
			group.Margins = new Margins(0, 0, 0, 2);
			this.fieldMinTime.TextChanged += new EventHandler(this.HandleTextFieldChanged);

			this.CreateStringLabeled("Heure maximale", this, out group, out this.fieldMaxTime);
			group.Dock = DockStyle.StackBegin;
			group.Margins = new Margins(0, 0, 0, 10);
			this.fieldMaxTime.TextChanged += new EventHandler(this.HandleTextFieldChanged);

			this.CreateStringLabeled("Pas pour la date", this, out group, out this.fieldDateStep);
			group.Dock = DockStyle.StackBegin;
			group.Margins = new Margins(0, 0, 0, 2);
			this.fieldDateStep.TextChanged += new EventHandler(this.HandleTextFieldChanged);

			this.CreateStringLabeled("Pas pour l'heure", this, out group, out this.fieldTimeStep);
			group.Dock = DockStyle.StackBegin;
			group.Margins = new Margins(0, 0, 0, 2);
			this.fieldTimeStep.TextChanged += new EventHandler(this.HandleTextFieldChanged);
		}

		public TypeEditorDateTime(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}
		
		
		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
				this.fieldResol.TextChanged -= new EventHandler(this.HandleTextFieldChanged);
				this.fieldMinDate.TextChanged -= new EventHandler(this.HandleTextFieldChanged);
				this.fieldMaxDate.TextChanged -= new EventHandler(this.HandleTextFieldChanged);
				this.fieldMinTime.TextChanged -= new EventHandler(this.HandleTextFieldChanged);
				this.fieldMaxTime.TextChanged -= new EventHandler(this.HandleTextFieldChanged);
				this.fieldDateStep.TextChanged -= new EventHandler(this.HandleTextFieldChanged);
				this.fieldTimeStep.TextChanged -= new EventHandler(this.HandleTextFieldChanged);
			}
			
			base.Dispose(disposing);
		}


		public override string GetSummary()
		{
			//	Retourne le texte du r�sum�.
			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			AbstractDateTimeType type = this.AbstractType as AbstractDateTimeType;

			if (type.Resolution != TimeResolution.Default)
			{
				builder.Append("R�solution = ");
				builder.Append(TypeEditorDateTime.Convert(type.Resolution));
			}

			if (type.MinimumDate != Date.Null)
			{
				this.PutSummaryLegend(builder, "Date min = ");
				builder.Append(TypeEditorDateTime.ToDate(type.MinimumDate.ToDateTime()));
			}

			if (type.MaximumDate != Date.Null)
			{
				this.PutSummaryLegend(builder, "Date max = ");
				builder.Append(TypeEditorDateTime.ToDate(type.MaximumDate.ToDateTime()));
			}

			if (type.MinimumTime != Time.Null)
			{
				this.PutSummaryLegend(builder, "Heure min = ");
				builder.Append(TypeEditorDateTime.ToTime(type.MinimumTime.ToDateTime()));
			}

			if (type.MaximumTime != Time.Null)
			{
				this.PutSummaryLegend(builder, "Heure max = ");
				builder.Append(TypeEditorDateTime.ToTime(type.MaximumTime.ToDateTime()));
			}

			this.PutSummaryLegend(builder, "Pas date = ");
			builder.Append(TypeEditorDateTime.ToTimeSpan(type.DateStep));

			this.PutSummaryLegend(builder, "Pas heure = ");
			builder.Append(TypeEditorDateTime.ToTimeSpan(type.TimeStep));

			return builder.ToString();
		}

		protected void PutSummaryLegend(System.Text.StringBuilder builder, string legend)
		{
			if (builder.Length > 0)
			{
				builder.Append(", ");
			}

			builder.Append(legend);
		}


		protected override void UpdateContent()
		{
			//	Met � jour le contenu de l'�diteur.
			AbstractDateTimeType type = this.AbstractType as AbstractDateTimeType;

			this.ignoreChange = true;
			this.fieldResol.Text = TypeEditorDateTime.Convert(type.Resolution);
			TypeEditorDateTime.ToDate(this.fieldMinDate, type.MinimumDate);
			TypeEditorDateTime.ToDate(this.fieldMaxDate, type.MaximumDate);
			TypeEditorDateTime.ToTime(this.fieldMinTime, type.MinimumTime);
			TypeEditorDateTime.ToTime(this.fieldMaxTime, type.MaximumTime);
			TypeEditorDateTime.ToTimeSpan(this.fieldDateStep, type.DateStep);
			TypeEditorDateTime.ToTimeSpan(this.fieldTimeStep, type.TimeStep);
			this.ignoreChange = false;
		}


		protected static TimeResolution Convert(string text)
		{
			switch (text)
			{
				case "Milliseconds":  return TimeResolution.Milliseconds;
				case "Seconds":       return TimeResolution.Seconds;
				case "Minutes":       return TimeResolution.Minutes;
				case "Hours":         return TimeResolution.Hours;
				case "Days":          return TimeResolution.Days;
				case "Weeks":         return TimeResolution.Weeks;
				case "Months":        return TimeResolution.Months;
				case "Years":         return TimeResolution.Years;
				default:              return TimeResolution.Default;
			}
		}

		protected static string Convert(TimeResolution resol)
		{
			switch (resol)
			{
				case TimeResolution.Milliseconds:  return "Milliseconds";
				case TimeResolution.Seconds:       return "Seconds";
				case TimeResolution.Minutes:       return "Minutes";
				case TimeResolution.Hours:         return "Hours";
				case TimeResolution.Days:          return "Days";
				case TimeResolution.Weeks:         return "Weeks";
				case TimeResolution.Months:        return "Months";
				case TimeResolution.Years:         return "Years";
				default:                           return "";
			}
		}

		protected static void ToDate(TextField field, Date date)
		{
			if (date == Date.Null)
			{
				field.Text = "";
			}
			else
			{
				field.Text = TypeEditorDateTime.ToDate(date.ToDateTime());
			}
		}

		protected static void ToTime(TextField field, Time time)
		{
			if (time == Time.Null)
			{
				field.Text = "";
			}
			else
			{
				field.Text = TypeEditorDateTime.ToTime(time.ToDateTime());
			}
		}

		protected static Date ToDate(TextField field)
		{
			if (!string.IsNullOrEmpty(field.Text))
			{
				System.DateTime dt = TypeEditorDateTime.ToDateTime(field.Text);
				if (dt != System.DateTime.MinValue)
				{
					return new Date(dt);
				}
			}

			return Date.Null;
		}

		protected static Time ToTime(TextField field)
		{
			if (!string.IsNullOrEmpty(field.Text))
			{
				System.DateTime dt = TypeEditorDateTime.ToDateTime(field.Text);
				if (dt != System.DateTime.MinValue)
				{
					return new Time(dt);
				}
			}

			return Time.Null;
		}

		protected static void ToTimeSpan(TextField field, System.TimeSpan ts)
		{
			if (ts == System.TimeSpan.Zero)
			{
				field.Text = "";
			}
			else
			{
				field.Text = TypeEditorDateTime.ToTimeSpan(ts);
			}
		}

		protected static System.TimeSpan ToTimeSpan(TextField field)
		{
			if (!string.IsNullOrEmpty(field.Text))
			{
				System.TimeSpan ts = TypeEditorDateTime.ToTimeSpan(field.Text);
				if (ts != System.TimeSpan.Zero)
				{
					return ts;
				}
			}

			return System.TimeSpan.Zero;
		}

		protected static string ToDate(System.DateTime dt)
		{
			//	(d) Short date: 4/17/2006
			return dt.ToString("d", System.Globalization.CultureInfo.CurrentCulture);
		}

		protected static string ToTime(System.DateTime dt)
		{
			//	(T) Long time: 14:22:48
			return dt.ToString("T", System.Globalization.CultureInfo.CurrentCulture);
		}

		protected static string ToDateTime(System.DateTime dt)
		{
			//	(G) General date/long time: 17.04.2006 14:22:48
			return dt.ToString("G", System.Globalization.CultureInfo.CurrentCulture);
		}

		protected static System.DateTime ToDateTime(string text)
		{
			System.DateTime dt;
			if (System.DateTime.TryParse(text, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.AssumeLocal|System.Globalization.DateTimeStyles.NoCurrentDateDefault, out dt))
			{
				return dt;
			}
			else
			{
				return System.DateTime.MinValue;
			}
		}

		protected static string ToTimeSpan(System.TimeSpan ts)
		{
			return ts.ToString();
		}

		protected static System.TimeSpan ToTimeSpan(string text)
		{
			System.TimeSpan ts;
			if (System.TimeSpan.TryParse(text, out ts))
			{
				return ts;
			}
			else
			{
				return System.TimeSpan.Zero;
			}
		}


		private void HandleTextFieldChanged(object sender)
		{
			if (this.ignoreChange)
			{
				return;
			}

			//	[Note1] On demande le type avec un ResourceAccess.GetField.
			AbstractDateTimeType type = this.AbstractType as AbstractDateTimeType;

			if (sender == this.fieldResol)
			{
				type.DefineResolution(TypeEditorDateTime.Convert(this.fieldResol.Text));
			}

			if (sender == this.fieldMinDate)
			{
				type.DefineMinimumDate(TypeEditorDateTime.ToDate(this.fieldMinDate));
			}

			if (sender == this.fieldMaxDate)
			{
				type.DefineMaximumDate(TypeEditorDateTime.ToDate(this.fieldMaxDate));
			}

			if (sender == this.fieldMinTime)
			{
				type.DefineMinimumTime(TypeEditorDateTime.ToTime(this.fieldMinTime));
			}

			if (sender == this.fieldMaxTime)
			{
				type.DefineMaximumTime(TypeEditorDateTime.ToTime(this.fieldMaxTime));
			}

			if (sender == this.fieldDateStep)
			{
				type.DefineDateStep(TypeEditorDateTime.ToTimeSpan(this.fieldDateStep));
			}

			if (sender == this.fieldTimeStep)
			{
				type.DefineTimeStep(TypeEditorDateTime.ToTimeSpan(this.fieldTimeStep));
			}

			//	[Note1] Cet appel va provoquer le ResourceAccess.SetField.
			this.OnContentChanged();
		}


		protected TextFieldCombo				fieldResol;
		protected TextField						fieldMinDate;
		protected TextField						fieldMaxDate;
		protected TextField						fieldMinTime;
		protected TextField						fieldMaxTime;
		protected TextField						fieldDateStep;
		protected TextField						fieldTimeStep;
	}
}
