﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Views.CommandToolbars;
using Epsitec.Cresus.Assets.App.Views.ViewStates;
using Epsitec.Cresus.Assets.App.Widgets;
using Epsitec.Cresus.Assets.Data.Reports;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class ReportsView : AbstractView
	{
		public ReportsView(DataAccessor accessor, MainToolbar toolbar, ViewType viewType, List<AbstractViewState> historyViewStates)
			: base (accessor, toolbar, viewType)
		{
			this.historyViewStates = historyViewStates;
		}


		public override void CreateUI(Widget parent)
		{
			base.CreateUI (parent);

			this.topTitle = new TopTitle
			{
				Parent = parent,
			};

			this.CreateToolbar (parent);

			this.mainFrame = new FrameBox
			{
				Parent              = parent,
				Dock                = DockStyle.Fill,
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
			};

			this.choiceFrame = new FrameBox
			{
				Parent              = parent,
				Dock                = DockStyle.Fill,
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
			};

			this.reportChoiceController = new ReportChoiceController (this.accessor);
			this.reportChoiceController.CreateUI (this.choiceFrame);

			this.reportChoiceController.ReportSelected += delegate (object sender, AbstractReportParams reportParams)
			{
				this.UpdateUI (reportParams);
			};

			this.UpdateReport (null);
		}

		public override void UpdateUI()
		{
			this.OnViewStateChanged (this.ViewState);
		}

		private void UpdateUI(AbstractReportParams reportParams)
		{
			this.UpdateReport (reportParams);
			this.OnViewStateChanged (this.ViewState);
		}


		private void CreateToolbar(Widget parent)
		{
			this.toolbar = new ReportsToolbar (this.accessor);
			this.toolbar.CreateUI (parent);

			this.toolbar.CommandClicked += delegate (object sender, ToolbarCommand command)
			{
				switch (command)
				{
					case ToolbarCommand.ReportParams:
						this.OnParams ();
						break;

					case ToolbarCommand.ReportExport:
						this.OnExport ();
						break;

					case ToolbarCommand.CompactAll:
						this.OnCompactAll ();
						break;

					case ToolbarCommand.CompactOne:
						this.OnCompactOne ();
						break;

					case ToolbarCommand.ExpandOne:
						this.OnExpandOne ();
						break;

					case ToolbarCommand.ExpandAll:
						this.OnExpandAll ();
						break;

					case ToolbarCommand.ReportPrevPeriod:
						this.OnChangePeriod (-1);
						break;

					case ToolbarCommand.ReportNextPeriod:
						this.OnChangePeriod (1);
						break;

					case ToolbarCommand.ReportClose:
						this.OnClose ();
						break;
				}
			};
		}


		private void OnParams()
		{
			//	Affiche le Popup pour choisir les paramètres d'un rapport.
			var target = this.toolbar.GetTarget (ToolbarCommand.ReportParams);
			this.report.ShowParamsPopup (target);
		}

		private void OnExport()
		{
			//	Affiche le Popup pour choisir comment exporter le rapport.
			var target = this.toolbar.GetTarget (ToolbarCommand.ReportExport);
			this.report.ShowExportPopup (target);
		}

		private void OnCompactAll()
		{
			this.report.OnCompactAll ();
		}

		private void OnCompactOne()
		{
			this.report.OnCompactOne ();
		}

		private void OnExpandOne()
		{
			this.report.OnExpandOne ();
		}

		private void OnExpandAll()
		{
			this.report.OnExpandAll ();
		}

		private void OnChangePeriod(int direction)
		{
			this.report.ReportParams = this.report.ReportParams.ChangePeriod (direction);
			this.report.UpdateParams ();
		}

		private void OnClose()
		{
			//	Ferme le rapport.
			this.UpdateUI (null);
		}


		private void UpdateReport(AbstractReportParams reportParams)
		{
			if (this.report != null)
			{
				this.DeleteTreeTable ();

				this.report.ParamsChanged  -= this.HandleParamsChanged;
				this.report.UpdateCommands -= this.HandleUpdateCommands;
				this.report.Dispose ();
				this.report = null;
			}

			this.report = ReportParamsHelper.CreateReport (this.accessor, reportParams);

			if (this.report != null)
			{
				this.CreateTreeTable ();

				this.report.ParamsChanged  += this.HandleParamsChanged;
				this.report.UpdateCommands += this.HandleUpdateCommands;
			}

			this.UpdateTitle ();

			this.mainFrame  .Visibility = (this.report != null);
			this.choiceFrame.Visibility = (this.report == null);

			this.reportChoiceController.ClearSelection ();

			this.UpdateToolbars ();
		}

		private void CreateTreeTable()
		{
			this.treeTableController = new NavigationTreeTableController ();
			this.treeTableController.CreateUI (this.mainFrame, footerHeight: 0);

			this.report.Initialize (this.treeTableController);
		}

		private void DeleteTreeTable()
		{
			this.treeTableController = null;
			this.mainFrame.Children.Clear ();
		}

		private void UpdateTitle()
		{
			if (this.report == null)
			{
				this.topTitle.SetTitle (this.GetViewTitle (ViewType.Reports));
			}
			else
			{
				this.topTitle.SetTitle (this.report.Title);
			}
		}

		private void HandleParamsChanged(object sender)
		{
			this.UpdateTitle ();
			this.OnViewStateChanged (this.ViewState);
		}

		private void HandleUpdateCommands(object sender)
		{
			this.UpdateToolbars ();
		}


		public override AbstractViewState ViewState
		{
			get
			{
				return new ReportsViewState
				{
					ViewType     = ViewType.Reports,
					ReportParams = this.report == null ? null : this.report.ReportParams,
				};
			}
			set
			{
				var viewState = value as ReportsViewState;
				System.Diagnostics.Debug.Assert (viewState != null);

				this.UpdateUI (viewState.ReportParams);
			}
		}


		private void UpdateToolbars()
		{
			bool isCompactEnable    = this.IsCompactEnable;
			bool isExpandEnable     = this.IsExpandEnable;
			bool changePeriodEnable = this.ChangePeriodEnable;

			this.toolbar.SetCommandEnable (ToolbarCommand.ReportParams,     this.HasParams);
			this.toolbar.SetCommandEnable (ToolbarCommand.ReportExport,     this.report != null);
			this.toolbar.SetCommandEnable (ToolbarCommand.CompactAll,       isCompactEnable);
			this.toolbar.SetCommandEnable (ToolbarCommand.CompactOne,       isCompactEnable);
			this.toolbar.SetCommandEnable (ToolbarCommand.ExpandOne,        isExpandEnable);
			this.toolbar.SetCommandEnable (ToolbarCommand.ExpandAll,        isExpandEnable);
			this.toolbar.SetCommandEnable (ToolbarCommand.ReportPrevPeriod, changePeriodEnable);
			this.toolbar.SetCommandEnable (ToolbarCommand.ReportNextPeriod, changePeriodEnable);
			this.toolbar.SetCommandEnable (ToolbarCommand.ReportClose,      this.report != null);
		}

		private bool IsCompactEnable
		{
			get
			{
				return this.report != null && this.report.IsCompactEnable;
			}
		}

		private bool IsExpandEnable
		{
			get
			{
				return this.report != null && this.report.IsExpandEnable;
			}
		}

		private bool ChangePeriodEnable
		{
			get
			{
				return this.report != null
					&& this.report.ReportParams != null
					&& this.report.ReportParams.ChangePeriod (1) != null;
			}
		}

		private bool HasParams
		{
			get
			{
				return this.report != null
					&& this.report.ReportParams != null
					&& this.report.ReportParams.HasParams;
			}
		}


		private readonly List<AbstractViewState> historyViewStates;

		private TopTitle						topTitle;
		private FrameBox						choiceFrame;
		private FrameBox						mainFrame;
		private ReportChoiceController			reportChoiceController;
		private ReportsToolbar					toolbar;
		private NavigationTreeTableController	treeTableController;
		private AbstractReport					report;
	}
}
