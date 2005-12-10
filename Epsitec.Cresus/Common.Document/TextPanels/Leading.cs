using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;
using Epsitec.Common.Text;

namespace Epsitec.Common.Document.TextPanels
{
	/// <summary>
	/// La classe Leading permet de choisir l'interligne.
	/// </summary>
	[SuppressBundleSupport]
	public class Leading : Abstract
	{
		public Leading(Document document) : base(document)
		{
			this.label.Text = Res.Strings.TextPanel.Leading.Title;

			this.fixIcon.Text = Misc.Image("TextLeading");
			ToolTip.Default.SetToolTip(this.fixIcon, Res.Strings.TextPanel.Leading.Title);

			this.fieldLeading = this.CreateTextFieldLabel(Res.Strings.TextPanel.Leading.Tooltip.Leading, Res.Strings.TextPanel.Leading.Short.Leading, Res.Strings.TextPanel.Leading.Long.Leading, 0,0,0, Widgets.TextFieldLabel.Type.TextFieldUnit, new EventHandler(this.HandleLeadingChanged));
			this.fieldLeading.SetRangeDimension(this.document, 0.0, 0.1, 1.0);
			this.fieldLeading.SetRangePercents(this.document, 50.0, 300.0, 10.0);
			this.fieldLeading.IsUnitPercent = true;
			this.fieldLeading.ButtonUnit.Clicked += new MessageEventHandler(this.HandleButtonUnitClicked);

			this.buttonLeadingMinus = this.CreateIconButton(Misc.Icon("ParaLeadingMinus"),      Res.Strings.TextPanel.Leading.Tooltip.LeadingMinus, new MessageEventHandler(this.HandleButtonLeadingMinusClicked), false);
			this.buttonLeadingPlus  = this.CreateIconButton(Misc.Icon("ParaLeadingPlus"),       Res.Strings.TextPanel.Leading.Tooltip.LeadingPlus,  new MessageEventHandler(this.HandleButtonLeadingPlusClicked), false);
			this.buttonAlignFirst   = this.CreateIconButton(Misc.Icon("ParaLeadingAlignFirst"), Res.Strings.TextPanel.Leading.Tooltip.AlignFirst,   new MessageEventHandler(this.HandleButtonAlignFirstClicked));
			this.buttonAlignAll     = this.CreateIconButton(Misc.Icon("ParaLeadingAlignAll"),   Res.Strings.TextPanel.Leading.Tooltip.AlignAll,     new MessageEventHandler(this.HandleButtonAlignAllClicked));
			this.buttonSettings     = this.CreateIconButton(Misc.Icon("Settings"),              Res.Strings.Action.Settings,                        new MessageEventHandler(this.HandleButtonSettingsClicked), false);

			this.buttonClear = this.CreateClearButton(new MessageEventHandler(this.HandleClearClicked));

			this.document.ParagraphWrapper.Active.Changed  += new EventHandler(this.HandleWrapperChanged);
			this.document.ParagraphWrapper.Defined.Changed += new EventHandler(this.HandleWrapperChanged);

			this.isNormalAndExtended = true;
			this.UpdateAfterChanging();
		}
		
		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
				this.fieldLeading.ButtonUnit.Clicked -= new MessageEventHandler(this.HandleButtonUnitClicked);
				this.document.ParagraphWrapper.Active.Changed  -= new EventHandler(this.HandleWrapperChanged);
				this.document.ParagraphWrapper.Defined.Changed -= new EventHandler(this.HandleWrapperChanged);
			}
			
			base.Dispose(disposing);
		}

		
		// Indique si ce panneau est visible pour un filtre donn�.
		public override bool IsFilterShow(string filter)
		{
			return ( filter == "All" || filter == "Frequently" || filter == "Paragraph" );
		}


		// Retourne la hauteur standard.
		public override double DefaultHeight
		{
			get
			{
				double h = this.LabelHeight;

				if ( this.isExtendedSize )  // panneau �tendu ?
				{
					if ( this.IsLabelProperties )  // �tendu/d�tails ?
					{
						h += 55;
					}
					else	// �tendu/compact ?
					{
						h += 55;
					}
				}
				else	// panneau r�duit ?
				{
					h += 30;
				}

				return h;
			}
		}


		// Le wrapper associ� a chang�.
		protected void HandleWrapperChanged(object sender)
		{
			this.UpdateAfterChanging();
		}

		
		// Met � jour la g�om�trie.
		protected override void UpdateClientGeometry()
		{
			base.UpdateClientGeometry();

			if ( this.fieldLeading == null )  return;

			Rectangle rect = this.UsefulZone;

			if ( this.isExtendedSize )
			{
				Rectangle r = rect;
				r.Bottom = r.Top-20;

				if ( this.IsLabelProperties )
				{
					r.Left = rect.Left;
					r.Width = 69;
					this.fieldLeading.Bounds = r;

					r.Offset(0, -25);
					r.Left = rect.Left;
					r.Width = 20;
					this.buttonLeadingMinus.Bounds = r;
					r.Offset(20, 0);
					this.buttonLeadingPlus.Bounds = r;
					r.Offset(20+10, 0);
					this.buttonAlignFirst.Bounds = r;
					this.buttonAlignFirst.Visibility = true;
					r.Offset(20, 0);
					this.buttonAlignAll.Bounds = r;
					this.buttonAlignAll.Visibility = true;
					r.Offset(20+5, 0);
					this.buttonSettings.Bounds = r;
					this.buttonSettings.Visibility = true;

					r.Left = rect.Right-20;
					r.Width = 20;
					this.buttonClear.Bounds = r;
				}
				else
				{
					r.Left = rect.Left;
					r.Width = 69;
					this.fieldLeading.Bounds = r;
					r.Offset(69, 0);
					r.Width = 20;
					this.buttonLeadingMinus.Bounds = r;
					r.Offset(20, 0);
					this.buttonLeadingPlus.Bounds = r;

					r.Left = rect.Right-20;
					r.Width = 20;
					this.buttonClear.Bounds = r;

					r.Offset(0, -25);
					r.Left = rect.Left;
					r.Width = 20;
					this.buttonAlignFirst.Bounds = r;
					this.buttonAlignFirst.Visibility = true;
					r.Offset(20, 0);
					this.buttonAlignAll.Bounds = r;
					this.buttonAlignAll.Visibility = true;
					r.Offset(20+5, 0);
					this.buttonSettings.Bounds = r;
					this.buttonSettings.Visibility = true;
				}
			}
			else
			{
				Rectangle r = rect;
				r.Bottom = r.Top-20;

				r.Left = rect.Left;
				r.Width = 69;
				this.fieldLeading.Bounds = r;
				r.Offset(69, 0);
				r.Width = 20;
				this.buttonLeadingMinus.Bounds = r;
				r.Offset(20, 0);
				this.buttonLeadingPlus.Bounds = r;

				r.Left = rect.Right-20;
				r.Width = 20;
				this.buttonClear.Bounds = r;

				this.buttonAlignFirst.Visibility = false;
				this.buttonAlignAll.Visibility = false;
				this.buttonSettings.Visibility = false;
			}
		}


		// Met � jour apr�s un changement du wrapper.
		protected override void UpdateAfterChanging()
		{
			base.UpdateAfterChanging();

			double leading = this.document.ParagraphWrapper.Active.Leading;
			Common.Text.Properties.SizeUnits units = this.document.ParagraphWrapper.Active.LeadingUnits;
			bool isLeading = this.document.ParagraphWrapper.Defined.IsLeadingDefined;

			bool alignFirst = (this.document.ParagraphWrapper.Active.AlignMode == Common.Text.Properties.AlignMode.First);
			bool alignAll   = (this.document.ParagraphWrapper.Active.AlignMode == Common.Text.Properties.AlignMode.All);
			bool isAlign = this.document.ParagraphWrapper.Defined.IsAlignModeDefined;

			this.ignoreChanged = true;

			this.fieldLeading.IsUnitPercent = (units == Common.Text.Properties.SizeUnits.Percent);
			this.SetTextFieldRealValue(this.fieldLeading.TextFieldReal, leading, units, isLeading);

			this.ActiveIconButton(this.buttonAlignFirst, alignFirst, isAlign);
			this.ActiveIconButton(this.buttonAlignAll,   alignAll,   isAlign);

			this.ignoreChanged = false;
		}


		private void HandleButtonUnitClicked(object sender, MessageEventArgs e)
		{
			if ( !this.document.ParagraphWrapper.IsAttached )  return;

			this.fieldLeading.IsUnitPercent = !this.fieldLeading.IsUnitPercent;

			double value;
			Common.Text.Properties.SizeUnits units;

			if ( this.fieldLeading.IsUnitPercent )
			{
				value = 1.2;
				units = Common.Text.Properties.SizeUnits.Percent;
			}
			else
			{
				if ( this.document.Modifier.RealUnitDimension == RealUnitType.DimensionInch )
				{
					value = 127.0;  // 0.5in
				}
				else
				{
					value = 100.0;  // 10mm
				}
				units = Common.Text.Properties.SizeUnits.Points;
			}

			this.document.ParagraphWrapper.SuspendSynchronisations();
			this.document.ParagraphWrapper.Defined.Leading = value;
			this.document.ParagraphWrapper.Defined.LeadingUnits = units;
			this.document.ParagraphWrapper.ResumeSynchronisations();
		}

		private void HandleLeadingChanged(object sender)
		{
			if ( this.ignoreChanged )  return;
			if ( !this.document.ParagraphWrapper.IsAttached )  return;

			TextFieldReal field = sender as TextFieldReal;
			if ( field == null )  return;

			double value;
			Common.Text.Properties.SizeUnits units;
			bool isDefined;
			this.GetTextFieldRealValue(field, out value, out units, out isDefined);

			this.document.ParagraphWrapper.SuspendSynchronisations();

			if ( isDefined )
			{
				this.document.ParagraphWrapper.Defined.Leading = value;
				this.document.ParagraphWrapper.Defined.LeadingUnits = units;
			}
			else
			{
				this.document.ParagraphWrapper.Defined.ClearLeading();
				this.document.ParagraphWrapper.Defined.ClearLeadingUnits();
			}

			this.document.ParagraphWrapper.ResumeSynchronisations();
		}

		private void HandleButtonLeadingMinusClicked(object sender, MessageEventArgs e)
		{
			this.LeadingIncrement(-1);
		}

		private void HandleButtonLeadingPlusClicked(object sender, MessageEventArgs e)
		{
			this.LeadingIncrement(1);
		}

		protected void LeadingIncrement(double delta)
		{
			if ( this.ignoreChanged )  return;
			if ( !this.document.ParagraphWrapper.IsAttached )  return;

			double leading = this.document.ParagraphWrapper.Active.Leading;
			Common.Text.Properties.SizeUnits units = this.document.ParagraphWrapper.Active.LeadingUnits;

			if ( units == Common.Text.Properties.SizeUnits.Percent )
			{
				if ( delta > 0 )  leading *= 1.2;
				else              leading /= 1.2;
				leading = System.Math.Max(leading, 0.5);
			}
			else
			{
				if ( this.document.Modifier.RealUnitDimension == RealUnitType.DimensionInch )
				{
					leading += delta*50.8;  // 0.2in
					leading = System.Math.Max(leading, 12.7);
				}
				else
				{
					leading += delta*50.0;  // 5mm
					leading = System.Math.Max(leading, 10.0);
				}
			}

			this.document.ParagraphWrapper.SuspendSynchronisations();
			this.document.ParagraphWrapper.Defined.Leading = leading;
			this.document.ParagraphWrapper.Defined.LeadingUnits = units;
			this.document.ParagraphWrapper.ResumeSynchronisations();
		}

		private void HandleButtonAlignFirstClicked(object sender, MessageEventArgs e)
		{
			if ( this.ignoreChanged )  return;
			if ( !this.document.ParagraphWrapper.IsAttached )  return;

			bool align = (this.buttonAlignFirst.ActiveState == ActiveState.No);
			Common.Text.Properties.AlignMode mode = align ? Common.Text.Properties.AlignMode.First : Common.Text.Properties.AlignMode.None;
			this.document.ParagraphWrapper.Defined.AlignMode = mode;
		}

		private void HandleButtonAlignAllClicked(object sender, MessageEventArgs e)
		{
			if ( this.ignoreChanged )  return;
			if ( !this.document.ParagraphWrapper.IsAttached )  return;

			bool align = (this.buttonAlignAll.ActiveState == ActiveState.No);
			Common.Text.Properties.AlignMode mode = align ? Common.Text.Properties.AlignMode.All : Common.Text.Properties.AlignMode.None;
			this.document.ParagraphWrapper.Defined.AlignMode = mode;
		}

		private void HandleButtonSettingsClicked(object sender, MessageEventArgs e)
		{
			this.document.Notifier.NotifySettingsShowPage("BookDocument", "Grid");
		}

		private void HandleClearClicked(object sender, MessageEventArgs e)
		{
			if ( this.ignoreChanged )  return;
			if ( !this.document.ParagraphWrapper.IsAttached )  return;

			this.document.ParagraphWrapper.SuspendSynchronisations();
			this.document.ParagraphWrapper.Defined.ClearLeading();
			this.document.ParagraphWrapper.Defined.ClearLeadingUnits();
			this.document.ParagraphWrapper.Defined.ClearAlignMode();
			this.document.ParagraphWrapper.ResumeSynchronisations();
		}


		protected Button					buttonUnits;
		protected Widgets.TextFieldLabel	fieldLeading;
		protected IconButton				buttonLeadingMinus;
		protected IconButton				buttonLeadingPlus;
		protected IconButton				buttonAlignFirst;
		protected IconButton				buttonAlignAll;
		protected IconButton				buttonSettings;
		protected IconButton				buttonClear;
	}
}
