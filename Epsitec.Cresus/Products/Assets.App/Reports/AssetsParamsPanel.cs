﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Popups;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class AssetsParamsPanel : AbstractParamsPanel
	{
		public AssetsParamsPanel(DataAccessor accessor)
			: base (accessor)
		{
			this.reportParams = new AssetsParams ();
		}


		public override void CreateUI(Widget parent)
		{
			this.CreateTimestampUI (parent);
			this.CreateGroupUI (parent);

			this.UpdateUI ();
		}

		private void CreateTimestampUI(Widget parent)
		{
			this.timestampController = new StateAtController (this.accessor);

			var frame = this.timestampController.CreateUI (parent);
			frame.Dock = DockStyle.Left;

			this.timestampController.DateChanged += delegate
			{
				this.UpdateTimestamp (new Timestamp (this.timestampController.Date.Value, 0));
			};
		}

		private void CreateGroupUI(Widget parent)
		{
			this.groupButton = new Button
			{
				Parent         = parent,
				ButtonStyle    = ButtonStyle.Icon,
				AutoFocus      = false,
				PreferredWidth = 200,
				Dock           = DockStyle.Left,
			};

			this.groupButton.Clicked += delegate
			{
				this.ShowFilter (this.groupButton);
			};
		}


		protected override void UpdateUI()
		{
			this.timestampController.Date = this.Params.Timestamp.Date;

			if (this.Params.RootGuid.IsEmpty)
			{
				this.groupButton.Text = "Sans groupement";
			}
			else
			{
				var text = GroupsLogic.GetShortName (this.accessor, this.Params.RootGuid);
				this.groupButton.Text = "Grouper selon " + text;
			}
		}


		private void ShowFilter(Widget target)
		{
			var popup = new FilterPopup (this.accessor, this.Params.RootGuid);

			popup.Create (target, leftOrRight: true);

			popup.Navigate += delegate (object sender, Guid guid)
			{
				this.UpdateGuid (guid);
				this.UpdateUI ();
			};
		}


		private void UpdateTimestamp(Timestamp timestamp)
		{
			this.reportParams = new AssetsParams (timestamp, this.Params.RootGuid);
			this.OnParamsChanged ();
		}

		private void UpdateGuid(Guid groupGuid)
		{
			this.reportParams = new AssetsParams (this.Params.Timestamp, groupGuid);
			this.OnParamsChanged ();
		}


		private AssetsParams Params
		{
			get
			{
				return this.reportParams as AssetsParams;
			}
		}


		private StateAtController				timestampController;
		private Button							groupButton;
	}
}
