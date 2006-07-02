using System.Collections.Generic;

using Epsitec.Common.Types;
using Epsitec.Common.Widgets;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Designer.Proxies
{
	public class Grid : Abstract
	{
		public Grid(ProxyManager manager) : base (manager)
		{
		}

		public override int Rank
		{
			//	Retourne le rang de ce proxy parmi la liste de tous les proxies.
			//	Plus le num�ro est petit, plus le proxy appara�tra haut dans la
			//	liste.
			get
			{
				return 4;
			}
		}

		public override string IconName
		{
			get
			{
				return "PropertyGrid";
			}
		}

		public override double DataColumnWidth
		{
			get
			{
				return 22*3+1;
			}
		}


		public int GridColumnsCount
		{
			get
			{
				return (int) this.GetValue(Grid.GridColumnsCountProperty);
			}
			set
			{
				this.SetValue(Grid.GridColumnsCountProperty, value);
			}
		}

		public int GridRowsCount
		{
			get
			{
				return (int) this.GetValue (Grid.GridRowsCountProperty);
			}
			set
			{
				this.SetValue(Grid.GridRowsCountProperty, value);
			}
		}

		public int GridColumnSpan
		{
			get
			{
				return (int) this.GetValue(Grid.GridColumnSpanProperty);
			}
			set
			{
				this.SetValue(Grid.GridColumnSpanProperty, value);
			}
		}

		public int GridRowSpan
		{
			get
			{
				return (int) this.GetValue(Grid.GridRowSpanProperty);
			}
			set
			{
				this.SetValue(Grid.GridRowSpanProperty, value);
			}
		}

		public ObjectModifier.GridMode GridColumnMode
		{
			get
			{
				return (ObjectModifier.GridMode) this.GetValue(Grid.GridColumnModeProperty);
			}
			set
			{
				this.SetValue(Grid.GridColumnModeProperty, value);
			}
		}

		public ObjectModifier.GridMode GridRowMode
		{
			get
			{
				return (ObjectModifier.GridMode) this.GetValue(Grid.GridRowModeProperty);
			}
			set
			{
				this.SetValue(Grid.GridRowModeProperty, value);
			}
		}

		public double GridColumnValue
		{
			get
			{
				return (double) this.GetValue(Grid.GridColumnValueProperty);
			}
			set
			{
				this.SetValue(Grid.GridColumnValueProperty, value);
			}
		}

		public double GridRowValue
		{
			get
			{
				return (double) this.GetValue(Grid.GridRowValueProperty);
			}
			set
			{
				this.SetValue(Grid.GridRowValueProperty, value);
			}
		}

		public double GridMinWidth
		{
			get
			{
				return (double) this.GetValue(Grid.GridMinWidthProperty);
			}
			set
			{
				this.SetValue(Grid.GridMinWidthProperty, value);
			}
		}

		public double GridMaxWidth
		{
			get
			{
				return (double) this.GetValue(Grid.GridMaxWidthProperty);
			}
			set
			{
				this.SetValue(Grid.GridMaxWidthProperty, value);
			}
		}

		public double GridMinHeight
		{
			get
			{
				return (double) this.GetValue(Grid.GridMinHeightProperty);
			}
			set
			{
				this.SetValue(Grid.GridMinHeightProperty, value);
			}
		}

		public double GridMaxHeight
		{
			get
			{
				return (double) this.GetValue(Grid.GridMaxHeightProperty);
			}
			set
			{
				this.SetValue(Grid.GridMaxHeightProperty, value);
			}
		}

		public double GridLeftBorder
		{
			get
			{
				return (double) this.GetValue(Grid.GridLeftBorderProperty);
			}
			set
			{
				this.SetValue(Grid.GridLeftBorderProperty, value);
			}
		}

		public double GridRightBorder
		{
			get
			{
				return (double) this.GetValue(Grid.GridRightBorderProperty);
			}
			set
			{
				this.SetValue(Grid.GridRightBorderProperty, value);
			}
		}

		public double GridBottomBorder
		{
			get
			{
				return (double) this.GetValue(Grid.GridBottomBorderProperty);
			}
			set
			{
				this.SetValue(Grid.GridBottomBorderProperty, value);
			}
		}

		public double GridTopBorder
		{
			get
			{
				return (double) this.GetValue(Grid.GridTopBorderProperty);
			}
			set
			{
				this.SetValue(Grid.GridTopBorderProperty, value);
			}
		}


		protected override void InitialisePropertyValues()
		{
			//	Cette m�thode est appel�e par Proxies.Abstract quand on connecte
			//	le premier widget avec le proxy.
			
			//	Recopie localement les diverses propri�t�s du widget s�lectionn�
			//	pour pouvoir ensuite travailler dessus :
			if (this.ObjectModifier.AreChildrenGrid(this.DefaultWidget))
			{
				this.GridColumnsCount = this.ObjectModifier.GetGridColumnsCount(this.DefaultWidget);
				this.GridRowsCount    = this.ObjectModifier.GetGridRowsCount(this.DefaultWidget);
			}

			if (this.ObjectModifier.AreChildrenGrid(this.DefaultWidget.Parent))
			{
				this.GridColumnSpan = this.ObjectModifier.GetGridColumnSpan(this.DefaultWidget);
				this.GridRowSpan    = this.ObjectModifier.GetGridRowSpan(this.DefaultWidget);
			}

			if (this.ObjectModifier.AreChildrenGrid(this.DefaultWidget))
			{
				GridSelection gs = GridSelection.Get(this.DefaultWidget);
				if (gs != null)
				{
					if (gs.Unit == GridSelection.SelectionUnit.Column)
					{
						this.GridColumnMode  = this.ObjectModifier.GetGridColumnMode(this.DefaultWidget, gs.Index);
						this.GridColumnValue = this.ObjectModifier.GetGridColumnWidth(this.DefaultWidget, gs.Index);
						
						this.GridMinWidth = this.ObjectModifier.GetGridColumnMinWidth(this.DefaultWidget, gs.Index);
						this.GridMaxWidth = this.ObjectModifier.GetGridColumnMaxWidth(this.DefaultWidget, gs.Index);
						
						this.GridLeftBorder = this.ObjectModifier.GetGridColumnLeftBorder(this.DefaultWidget, gs.Index);
						this.GridRightBorder = this.ObjectModifier.GetGridColumnRightBorder(this.DefaultWidget, gs.Index);
					}

					if (gs.Unit == GridSelection.SelectionUnit.Row)
					{
						this.GridRowMode  = this.ObjectModifier.GetGridRowMode(this.DefaultWidget, gs.Index);
						this.GridRowValue = this.ObjectModifier.GetGridRowHeight(this.DefaultWidget, gs.Index);
						
						this.GridMinHeight = this.ObjectModifier.GetGridRowMinHeight(this.DefaultWidget, gs.Index);
						this.GridMaxHeight = this.ObjectModifier.GetGridRowMaxHeight(this.DefaultWidget, gs.Index);
						
						this.GridTopBorder = this.ObjectModifier.GetGridRowTopBorder(this.DefaultWidget, gs.Index);
						this.GridBottomBorder = this.ObjectModifier.GetGridRowBottomBorder(this.DefaultWidget, gs.Index);
					}
				}
			}
		}

		private static void NotifyGridColumnsCountChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			int value = (int) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						that.ObjectModifier.SetGridColumnsCount(obj, value);
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridRowsCountChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			int value = (int) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						that.ObjectModifier.SetGridRowsCount(obj, value);
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridColumnSpanChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			int value = (int) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						that.ObjectModifier.SetGridColumnSpan(obj, value);
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridRowSpanChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			int value = (int) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						that.ObjectModifier.SetGridRowSpan(obj, value);
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridColumnModeChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			ObjectModifier.GridMode value = (ObjectModifier.GridMode) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						GridSelection gs = GridSelection.Get(obj);
						if (gs != null)
						{
							if (gs.Unit == GridSelection.SelectionUnit.Column)
							{
								that.ObjectModifier.SetGridColumnMode(obj, gs.Index, value);
							}
						}
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridRowModeChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			ObjectModifier.GridMode value = (ObjectModifier.GridMode) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						GridSelection gs = GridSelection.Get(obj);
						if (gs != null)
						{
							if (gs.Unit == GridSelection.SelectionUnit.Row)
							{
								that.ObjectModifier.SetGridRowMode(obj, gs.Index, value);
							}
						}
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridColumnValueChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			double value = (double) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						GridSelection gs = GridSelection.Get(obj);
						if (gs != null)
						{
							if (gs.Unit == GridSelection.SelectionUnit.Column)
							{
								that.ObjectModifier.SetGridColumnWidth(obj, gs.Index, value);
							}
						}
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridRowValueChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			double value = (double) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						GridSelection gs = GridSelection.Get(obj);
						if (gs != null)
						{
							if (gs.Unit == GridSelection.SelectionUnit.Row)
							{
								that.ObjectModifier.SetGridRowHeight(obj, gs.Index, value);
							}
						}
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridMinWidthChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			double value = (double) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						GridSelection gs = GridSelection.Get(obj);
						if (gs != null)
						{
							if (gs.Unit == GridSelection.SelectionUnit.Column)
							{
								that.ObjectModifier.SetGridColumnMinWidth(obj, gs.Index, value);
							}
						}
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridMaxWidthChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			double value = (double) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						GridSelection gs = GridSelection.Get(obj);
						if (gs != null)
						{
							if (gs.Unit == GridSelection.SelectionUnit.Column)
							{
								that.ObjectModifier.SetGridColumnMaxWidth(obj, gs.Index, value);
							}
						}
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridMinHeightChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			double value = (double) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						GridSelection gs = GridSelection.Get(obj);
						if (gs != null)
						{
							if (gs.Unit == GridSelection.SelectionUnit.Row)
							{
								that.ObjectModifier.SetGridRowMinHeight(obj, gs.Index, value);
							}
						}
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridMaxHeightChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			double value = (double) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						GridSelection gs = GridSelection.Get(obj);
						if (gs != null)
						{
							if (gs.Unit == GridSelection.SelectionUnit.Row)
							{
								that.ObjectModifier.SetGridRowMaxHeight(obj, gs.Index, value);
							}
						}
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridLeftBorderChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			double value = (double) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						GridSelection gs = GridSelection.Get(obj);
						if (gs != null)
						{
							if (gs.Unit == GridSelection.SelectionUnit.Column)
							{
								that.ObjectModifier.SetGridColumnLeftBorder(obj, gs.Index, value);
							}
						}
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridRightBorderChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			double value = (double) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						GridSelection gs = GridSelection.Get(obj);
						if (gs != null)
						{
							if (gs.Unit == GridSelection.SelectionUnit.Column)
							{
								that.ObjectModifier.SetGridColumnRightBorder(obj, gs.Index, value);
							}
						}
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridTopBorderChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			double value = (double) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						GridSelection gs = GridSelection.Get(obj);
						if (gs != null)
						{
							if (gs.Unit == GridSelection.SelectionUnit.Row)
							{
								that.ObjectModifier.SetGridRowTopBorder(obj, gs.Index, value);
							}
						}
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}

		private static void NotifyGridBottomBorderChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de nos propri�t�s
			//	de d�finition pour permettre de mettre � jour les widgets connect�s.
			double value = (double) newValue;
			Grid that = (Grid) o;

			if (that.IsNotSuspended)
			{
				that.SuspendChanges();

				try
				{
					foreach (Widget obj in that.Widgets)
					{
						GridSelection gs = GridSelection.Get(obj);
						if (gs != null)
						{
							if (gs.Unit == GridSelection.SelectionUnit.Row)
							{
								that.ObjectModifier.SetGridRowBottomBorder(obj, gs.Index, value);
							}
						}
					}
				}
				finally
				{
					that.ResumeChanges();
				}
			}
		}


		static Grid()
		{
			Grid.GridColumnsCountProperty.DefaultMetadata.DefineNamedType(ProxyManager.GridNumericType);
			Grid.GridRowsCountProperty.DefaultMetadata.DefineNamedType(ProxyManager.GridNumericType);
			Grid.GridColumnsCountProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[100J]").ToLong());
			Grid.GridRowsCountProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[100K]").ToLong());

			Grid.GridColumnSpanProperty.DefaultMetadata.DefineNamedType(ProxyManager.GridNumericType);
			Grid.GridRowSpanProperty.DefaultMetadata.DefineNamedType(ProxyManager.GridNumericType);
			Grid.GridColumnSpanProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[100L]").ToLong());
			Grid.GridRowSpanProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[100M]").ToLong());

			EnumType gridColumnModeEnumType = new EnumType(typeof(ObjectModifier.GridMode));
			gridColumnModeEnumType.DefineDefaultController("Enum", "Icons");
			gridColumnModeEnumType[ObjectModifier.GridMode.Proportional].DefineCaptionId(new Support.Druid("[10051]").ToLong());
			gridColumnModeEnumType[ObjectModifier.GridMode.Absolute].DefineCaptionId(new Support.Druid("[10052]").ToLong());
			gridColumnModeEnumType[ObjectModifier.GridMode.Auto].DefineCaptionId(new Support.Druid("[10053]").ToLong());
			Grid.GridColumnModeProperty.DefaultMetadata.DefineNamedType(gridColumnModeEnumType);
			Grid.GridColumnModeProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[100N]").ToLong());

			EnumType gridRowModeEnumType = new EnumType(typeof(ObjectModifier.GridMode));
			gridRowModeEnumType.DefineDefaultController("Enum", "Icons");
			gridRowModeEnumType[ObjectModifier.GridMode.Proportional].DefineCaptionId(new Support.Druid("[10051]").ToLong());
			gridRowModeEnumType[ObjectModifier.GridMode.Absolute].DefineCaptionId(new Support.Druid("[10052]").ToLong());
			gridRowModeEnumType[ObjectModifier.GridMode.Auto].DefineCaptionId(new Support.Druid("[10053]").ToLong());
			Grid.GridRowModeProperty.DefaultMetadata.DefineNamedType(gridRowModeEnumType);
			Grid.GridRowModeProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[100O]").ToLong());

			Grid.GridColumnValueProperty.DefaultMetadata.DefineNamedType(ProxyManager.SizeNumericType);
			Grid.GridRowValueProperty.DefaultMetadata.DefineNamedType(ProxyManager.SizeNumericType);
			Grid.GridColumnValueProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[100P]").ToLong());
			Grid.GridRowValueProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[100Q]").ToLong());

			Grid.GridMinWidthProperty.DefaultMetadata.DefineNamedType(ProxyManager.SizeNumericType);
			Grid.GridMaxWidthProperty.DefaultMetadata.DefineNamedType(ProxyManager.SizeNumericType);
			Grid.GridMinHeightProperty.DefaultMetadata.DefineNamedType(ProxyManager.SizeNumericType);
			Grid.GridMaxHeightProperty.DefaultMetadata.DefineNamedType(ProxyManager.SizeNumericType);
			Grid.GridLeftBorderProperty.DefaultMetadata.DefineNamedType(ProxyManager.SizeNumericType);
			Grid.GridRightBorderProperty.DefaultMetadata.DefineNamedType(ProxyManager.SizeNumericType);
			Grid.GridTopBorderProperty.DefaultMetadata.DefineNamedType(ProxyManager.SizeNumericType);
			Grid.GridBottomBorderProperty.DefaultMetadata.DefineNamedType(ProxyManager.SizeNumericType);
			Grid.GridMinWidthProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[10041]").ToLong());
			Grid.GridMaxWidthProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[10042]").ToLong());
			Grid.GridMinHeightProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[10043]").ToLong());
			Grid.GridMaxHeightProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[10044]").ToLong());
			Grid.GridLeftBorderProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[10045]").ToLong());
			Grid.GridRightBorderProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[10046]").ToLong());
			Grid.GridTopBorderProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[10047]").ToLong());
			Grid.GridBottomBorderProperty.DefaultMetadata.DefineCaptionId(new Support.Druid("[10048]").ToLong());
		}


		public static readonly DependencyProperty GridColumnsCountProperty = DependencyProperty.Register("GridColumnsCount", typeof(int), typeof(Grid), new DependencyPropertyMetadata(2, Grid.NotifyGridColumnsCountChanged));
		public static readonly DependencyProperty GridRowsCountProperty	   = DependencyProperty.Register("GridRowsCount",    typeof(int), typeof(Grid), new DependencyPropertyMetadata(2, Grid.NotifyGridRowsCountChanged));

		public static readonly DependencyProperty GridColumnSpanProperty   = DependencyProperty.Register("GridColumnSpan", typeof(int), typeof(Grid), new DependencyPropertyMetadata(1, Grid.NotifyGridColumnSpanChanged));
		public static readonly DependencyProperty GridRowSpanProperty	   = DependencyProperty.Register("GridRowSpan",    typeof(int), typeof(Grid), new DependencyPropertyMetadata(1, Grid.NotifyGridRowSpanChanged));

		public static readonly DependencyProperty GridColumnModeProperty   = DependencyProperty.Register("GridColumnMode", typeof(ObjectModifier.GridMode), typeof(Grid), new DependencyPropertyMetadata(ObjectModifier.GridMode.Proportional, Grid.NotifyGridColumnModeChanged));
		public static readonly DependencyProperty GridRowModeProperty	   = DependencyProperty.Register("GridRowMode",    typeof(ObjectModifier.GridMode), typeof(Grid), new DependencyPropertyMetadata(ObjectModifier.GridMode.Proportional, Grid.NotifyGridRowModeChanged));

		public static readonly DependencyProperty GridColumnValueProperty  = DependencyProperty.Register("GridColumnValue", typeof(double), typeof(Grid), new DependencyPropertyMetadata(1.0, Grid.NotifyGridColumnValueChanged));
		public static readonly DependencyProperty GridRowValueProperty	   = DependencyProperty.Register("GridRowValue",    typeof(double), typeof(Grid), new DependencyPropertyMetadata(1.0, Grid.NotifyGridRowValueChanged));

		public static readonly DependencyProperty GridMinWidthProperty     = DependencyProperty.Register("GridMinWidth",     typeof(double), typeof(Grid), new DependencyPropertyMetadata(0.0, Grid.NotifyGridMinWidthChanged));
		public static readonly DependencyProperty GridMaxWidthProperty     = DependencyProperty.Register("GridMaxWidth",     typeof(double), typeof(Grid), new DependencyPropertyMetadata(0.0, Grid.NotifyGridMaxWidthChanged));
		public static readonly DependencyProperty GridMinHeightProperty    = DependencyProperty.Register("GridMinHeight",    typeof(double), typeof(Grid), new DependencyPropertyMetadata(0.0, Grid.NotifyGridMinHeightChanged));
		public static readonly DependencyProperty GridMaxHeightProperty    = DependencyProperty.Register("GridMaxHeight",    typeof(double), typeof(Grid), new DependencyPropertyMetadata(0.0, Grid.NotifyGridMaxHeightChanged));
		public static readonly DependencyProperty GridLeftBorderProperty   = DependencyProperty.Register("GridLeftBorder",   typeof(double), typeof(Grid), new DependencyPropertyMetadata(0.0, Grid.NotifyGridLeftBorderChanged));
		public static readonly DependencyProperty GridRightBorderProperty  = DependencyProperty.Register("GridRightBorder",  typeof(double), typeof(Grid), new DependencyPropertyMetadata(0.0, Grid.NotifyGridRightBorderChanged));
		public static readonly DependencyProperty GridTopBorderProperty    = DependencyProperty.Register("GridTopBorder",    typeof(double), typeof(Grid), new DependencyPropertyMetadata(0.0, Grid.NotifyGridTopBorderChanged));
		public static readonly DependencyProperty GridBottomBorderProperty = DependencyProperty.Register("GridBottomBorder", typeof(double), typeof(Grid), new DependencyPropertyMetadata(0.0, Grid.NotifyGridBottomBorderChanged));
	}
}
