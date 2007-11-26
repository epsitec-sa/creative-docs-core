using System.Collections.Generic;

using Epsitec.Common.Types;
using Epsitec.Common.Widgets;
using Epsitec.Common.Drawing;
using Epsitec.Common.FormEngine;

namespace Epsitec.Common.Designer.FormEditor.Proxies
{
	public class Mode : Abstract
	{
		public Mode(ProxyManager manager) : base (manager)
		{
		}

		public override int Rank
		{
			//	Retourne le rang de ce proxy parmi la liste de tous les proxies.
			//	Plus le num�ro est petit, plus le proxy appara�tra haut dans la
			//	liste.
			//	Les Proxies de PanelEditor sont num�rot�s � partir de 0.
			//	Ceux de FormEditor sont num�rot�s � partir de 100.
			//	Ceci �vite les collisions dans PanelsContext.IsExtendedProxies !
			get
			{
				return 100+1;
			}
		}

		public override string IconName
		{
			get
			{
				return "PropertyAspect";
			}
		}

		public override double DataColumnWidth
		{
			get
			{
				return 22*4+1;
			}
		}

		public override double RowsSpacing
		{
			get
			{
				return 3;
			}
		}


		public FieldDescription.SeparatorType SeparatorBottom
		{
			get
			{
				return (FieldDescription.SeparatorType) this.GetValue(Mode.SeparatorBottomProperty);
			}
			set
			{
				this.SetValue(Mode.SeparatorBottomProperty, value);
			}
		}

		public FieldDescription.BoxPaddingType BoxPadding
		{
			get
			{
				return (FieldDescription.BoxPaddingType) this.GetValue(Mode.BoxPaddingProperty);
			}
			set
			{
				this.SetValue(Mode.BoxPaddingProperty, value);
			}
		}

		public FieldDescription.BackColorType BackColor
		{
			get
			{
				return (FieldDescription.BackColorType) this.GetValue(Mode.BackColorProperty);
			}
			set
			{
				this.SetValue(Mode.BackColorProperty, value);
			}
		}

		public FrameState BoxFrameState
		{
			get
			{
				return (FrameState) this.GetValue(Mode.BoxFrameStateProperty);
			}
			set
			{
				this.SetValue(Mode.BoxFrameStateProperty, value);
			}
		}

		public double BoxFrameWidth
		{
			get
			{
				return (double) this.GetValue(Mode.BoxFrameWidthProperty);
			}
			set
			{
				this.SetValue(Mode.BoxFrameWidthProperty, value);
			}
		}


		protected override void InitializePropertyValues()
		{
			//	Cette m�thode est appel�e par Proxies.Abstract quand on connecte
			//	le premier widget avec le proxy.
			if (this.ObjectModifier.IsField(this.DefaultWidget) ||
				this.ObjectModifier.IsBox(this.DefaultWidget))
			{
				this.SeparatorBottom = this.ObjectModifier.GetSeparatorBottom(this.DefaultWidget);
			}

			if (this.ObjectModifier.IsBox(this.DefaultWidget))
			{
				this.BoxPadding = this.ObjectModifier.GetBoxPadding(this.DefaultWidget);
			}

			if (this.ObjectModifier.IsField(this.DefaultWidget) ||
				this.ObjectModifier.IsBox(this.DefaultWidget) ||
				this.ObjectModifier.IsGlue(this.DefaultWidget))
			{
				this.BackColor = this.ObjectModifier.GetBackColor(this.DefaultWidget);
			}

			if (this.ObjectModifier.IsBox(this.DefaultWidget))
			{
				this.BoxFrameState = this.ObjectModifier.GetBoxFrameState(this.DefaultWidget);
				this.BoxFrameWidth = this.ObjectModifier.GetBoxFrameWidth(this.DefaultWidget);
			}
		}

		static Mode()
		{
			EnumType separatorBottomEnumType = Res.Types.FieldDescription.SeparatorType;
			Mode.SeparatorBottomProperty.DefaultMetadata.DefineNamedType(separatorBottomEnumType);
			Mode.SeparatorBottomProperty.DefaultMetadata.DefineCaptionId(Res.Captions.FieldMode.SeparatorBottomType.Id);

			EnumType boxPaddingBottomEnumType = Res.Types.FieldDescription.BoxPaddingType;
			Mode.BoxPaddingProperty.DefaultMetadata.DefineNamedType(boxPaddingBottomEnumType);
			Mode.BoxPaddingProperty.DefaultMetadata.DefineCaptionId(Res.Captions.FieldMode.BoxPaddingType.Id);

			EnumType backColorEnumType = Res.Types.FieldDescription.BackColorType;
			Mode.BackColorProperty.DefaultMetadata.DefineNamedType(backColorEnumType);
			Mode.BackColorProperty.DefaultMetadata.DefineCaptionId(Res.Captions.FieldMode.BackColorType.Id);

			EnumType boxFrameStateEnumType = Res.Types.FieldDescription.FrameState;
			Mode.BoxFrameStateProperty.DefaultMetadata.DefineNamedType(boxFrameStateEnumType);
			Mode.BoxFrameStateProperty.DefaultMetadata.DefineCaptionId(Res.Captions.FieldMode.BoxFrameState.Id);

			Mode.BoxFrameWidthProperty.DefaultMetadata.DefineNamedType(ProxyManager.WidthNumericType);
			Mode.BoxFrameWidthProperty.DefaultMetadata.DefineCaptionId(Res.Captions.FieldMode.BoxFrameWidth.Id);
		}


		private static void NotifySeparatorBottomChanged(DependencyObject o, object oldValue, object newValue)
		{
			FieldDescription.SeparatorType value = (FieldDescription.SeparatorType) newValue;
			Mode that = (Mode) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						that.ObjectModifier.SetSeparatorBottom(obj, value);
					}
				}
				finally
				{
					that.ResumeChanges();
					that.RegenerateProxiesAndForm();
				}
			}
		}

		private static void NotifyBoxPaddingChanged(DependencyObject o, object oldValue, object newValue)
		{
			FieldDescription.BoxPaddingType value = (FieldDescription.BoxPaddingType) newValue;
			Mode that = (Mode) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						that.ObjectModifier.SetBoxPadding(obj, value);
					}
				}
				finally
				{
					that.ResumeChanges();
					that.RegenerateProxiesAndForm();
				}
			}
		}

		private static void NotifyBackColorChanged(DependencyObject o, object oldValue, object newValue)
		{
			FieldDescription.BackColorType value = (FieldDescription.BackColorType) newValue;
			Mode that = (Mode) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						that.ObjectModifier.SetBackColor(obj, value);
					}
				}
				finally
				{
					that.ResumeChanges();
					that.RegenerateProxiesAndForm();
				}
			}
		}

		private static void NotifyBoxFrameStateChanged(DependencyObject o, object oldValue, object newValue)
		{
			FrameState value = (FrameState) newValue;
			Mode that = (Mode) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						that.ObjectModifier.SetBoxFrameState(obj, value);
					}
				}
				finally
				{
					that.ResumeChanges();
					that.RegenerateProxiesAndForm();
				}
			}
		}

		private static void NotifyBoxFrameWidthChanged(DependencyObject o, object oldValue, object newValue)
		{
			double value = (double) newValue;
			Mode that = (Mode) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						that.ObjectModifier.SetBoxFrameWidth(obj, value);
					}
				}
				finally
				{
					that.ResumeChanges();
					that.RegenerateProxiesAndForm();
				}
			}
		}


		public static readonly DependencyProperty SeparatorBottomProperty = DependencyProperty.Register("SeparatorBottom", typeof(FieldDescription.SeparatorType),  typeof(Mode), new DependencyPropertyMetadata(FieldDescription.SeparatorType.Normal,  Mode.NotifySeparatorBottomChanged));
		public static readonly DependencyProperty BoxPaddingProperty      = DependencyProperty.Register("BoxPadding",      typeof(FieldDescription.BoxPaddingType), typeof(Mode), new DependencyPropertyMetadata(FieldDescription.BoxPaddingType.Normal, Mode.NotifyBoxPaddingChanged));
		public static readonly DependencyProperty BackColorProperty       = DependencyProperty.Register("BackColor",       typeof(FieldDescription.BackColorType),  typeof(Mode), new DependencyPropertyMetadata(FieldDescription.BackColorType.None,    Mode.NotifyBackColorChanged));
		public static readonly DependencyProperty BoxFrameStateProperty   = DependencyProperty.Register("BoxFrameState",   typeof(FrameState),                      typeof(Mode), new DependencyPropertyMetadata(FrameState.None,                        Mode.NotifyBoxFrameStateChanged));
		public static readonly DependencyProperty BoxFrameWidthProperty   = DependencyProperty.Register("BoxFrameWidth",   typeof(double),                          typeof(Mode), new DependencyPropertyMetadata(1.0,                                    Mode.NotifyBoxFrameWidthChanged));
	}
}
