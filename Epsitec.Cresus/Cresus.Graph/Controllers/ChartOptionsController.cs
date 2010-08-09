﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Jonas SCHMID, Maintainer: Pierre ARNAUD, Jonas SCHMID

using Epsitec.Common.Drawing;
using Epsitec.Common.Graph.Widgets;
using Epsitec.Common.UI;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Graph.Widgets;
using Epsitec.Common.Support;
using System;

namespace Epsitec.Cresus.Graph.Controllers
{
    /// <summary>
    /// Controller for <see cref="ChartOptions"/>.
    /// Shows a commandbar in the chart window to change the options.
    /// </summary>
    public class ChartOptionsController
    {
        /// <summary>
        /// Create the controller into a frame
        /// </summary>
        /// <param name="commandBar">CommandSelectionBar where to put the configuration buttons</param>
        /// <param name="fixedCaptions">FrameBox showing the captions to show/hide</param>
        /// <param name="floatingCaptions">FrameBox showing the floating captions to show/hide</param>
        public ChartOptionsController (CommandSelectionBar commandBar, AnchoredPalette fixedCaptions, FloatingCaptionsView floatingCaptions)
        {
            this.chartOptions = new ChartOptions()
            {
                ShowFixedCaptions = true,
                ShowFloatingCaptions = true,
                FixedCaptionsPosition = new Margins (0, 4, 4, 0)
            };

			this.dispatcher = new CommandDispatcher ("ChartOptions Dispatcher", CommandDispatcherLevel.Secondary);
			this.dispatcher.RegisterController (this);

            // Change visibility when value is changed
            this.ChartOptions.ShowFixedCaptionsChanged +=
                (sender, e) =>
                {
                    fixedCaptions.Visibility = (bool) e.NewValue;
                };

            // Change visibility when value is changed
            this.ChartOptions.ShowFloatingCaptionsChanged +=
                (sender, e) =>
                {
                    floatingCaptions.Visibility = (bool) e.NewValue;
                };

            this.ChartOptions.FixedCaptionsPositionChanged +=
                (sender, e) =>
                {
                    fixedCaptions.Margins = (Margins)e.NewValue;
                };

            fixedCaptions.MarginsChanged +=
                (sender, e) =>
                {
                    this.ChartOptions.FixedCaptionsPosition = (Margins)e.NewValue;
                };

            this.SetupUI(commandBar, fixedCaptions, floatingCaptions);
        }

        /// <summary>
        /// Options handled by the controller
        /// </summary>
        internal ChartOptions ChartOptions
        {
            get
            {
                return this.chartOptions;
            }
            set
            {
                this.chartOptions.copyValues (value);
            }
        }

        private void SetupUI (CommandSelectionBar commandBar, AnchoredPalette fixedCaptions, FloatingCaptionsView floatingCaptions)
        {
            new Separator()
            {
                IsVerticalLine = true,
                Dock = DockStyle.Stacked,
                PreferredWidth = 1,
                Parent = commandBar,
                Margins = new Margins(2, 2, 0, 0),
            };

            // Buttons container
            var frame = new FrameBox ()
            {
                Dock = DockStyle.Stacked,
                Parent = commandBar,
                ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
            };

            // Used to dispatch the commands
            CommandDispatcher.SetDispatcher (frame, this.dispatcher);

            // Create the buttons
            CreateCommandButton (frame, Res.Commands.ChartOptions.ShowSummaryCaptions);
            CreateCommandButton (frame, Res.Commands.ChartOptions.ShowSeriesCaptions);
        }

        /// <summary>
        /// Create a button into a Widget using a command. Allows to easily create buttons with the same layout
        /// </summary>
        /// <param name="parent">Where to put the button</param>
        /// <param name="command">Associated command</param>
        private static void CreateCommandButton (Widget parent, Command command)
        {
            new MetaButton ()
            {
                CommandObject = command,
                Parent = parent,
                Dock = ChartOptionsController.preferredDockStyle,
                ButtonClass = ChartOptionsController.preferredButtonClass,
                PreferredSize = ChartOptionsController.preferredSize,
                Padding = ChartOptionsController.preferredPadding
            };
        }

        /// <summary>
        /// Called when clicking the summary captions button
        /// </summary>
        [Command (Res.CommandIds.ChartOptions.ShowSummaryCaptions)]
        private void ExecuteShowSummaryCaptionsCommand ()
        {
            this.ChartOptions.ShowFixedCaptions = !this.ChartOptions.ShowFixedCaptions;
        }

        /// <summary>
        /// Called when clicking the series captions button
        /// </summary>
        [Command (Res.CommandIds.ChartOptions.ShowSeriesCaptions)]
        private void ExecuteShowSeriesCaptionsCommand ()
        {
            this.ChartOptions.ShowFloatingCaptions = !this.ChartOptions.ShowFloatingCaptions;
        }

        // Options for the buttons
        private static readonly Size preferredSize = new Size (40, 40);
        private static readonly Margins preferredPadding = new Margins (4, 4, 0, 0);
        private static readonly ButtonClass preferredButtonClass = ButtonClass.FlatButton;
        private static readonly DockStyle preferredDockStyle = DockStyle.Left;

        private readonly ChartOptions chartOptions;
		private readonly CommandDispatcher dispatcher;
    }
}
