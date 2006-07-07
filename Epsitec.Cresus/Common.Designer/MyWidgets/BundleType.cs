using System.Collections.Generic;
using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Designer.MyWidgets
{
	/// <summary>
	/// Widget permettant de choisir une cat�gorie de bundle.
	/// </summary>
	public class BundleType : Widget
	{
		public BundleType() : base()
		{
			Separator sep = new Separator(this);
			sep.PreferredHeight = 1;
			sep.Margins = new Margins(0, 0, 10, 0);
			sep.Dock = DockStyle.Bottom;

			this.buttonStrings = new IconButtonMark(this);
			this.buttonStrings.Text = Res.Strings.BundleType.Strings;
			this.buttonStrings.Name = "Strings";
			this.buttonStrings.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonStrings.SiteMark = SiteMark.OnBottom;
			this.buttonStrings.MarkDimension = 5;
			this.buttonStrings.PreferredWidth = 100;
			this.buttonStrings.MinHeight = 20+5;
			this.buttonStrings.AutoFocus = false;
			this.buttonStrings.Margins = new Margins(10, 0, 10, 0);
			this.buttonStrings.Dock = DockStyle.Left;
			this.buttonStrings.Clicked += new MessageEventHandler(this.HandleButtonClicked);

			this.buttonPanels = new IconButtonMark(this);
			this.buttonPanels.Text = Res.Strings.BundleType.Panels;
			this.buttonPanels.Name = "Panels";
			this.buttonPanels.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonPanels.SiteMark = SiteMark.OnBottom;
			this.buttonPanels.MarkDimension = 5;
			this.buttonPanels.PreferredWidth = 100;
			this.buttonPanels.MinHeight = 20+5;
			this.buttonPanels.AutoFocus = false;
			this.buttonPanels.Margins = new Margins(2, 0, 10, 0);
			this.buttonPanels.Dock = DockStyle.Left;
			this.buttonPanels.Clicked += new MessageEventHandler(this.HandleButtonClicked);

			this.buttonCaptions = new IconButtonMark(this);
			this.buttonCaptions.Text = Res.Strings.BundleType.Captions;
			this.buttonCaptions.Name = "Captions";
			this.buttonCaptions.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonCaptions.SiteMark = SiteMark.OnBottom;
			this.buttonCaptions.MarkDimension = 5;
			this.buttonCaptions.PreferredWidth = 100;
			this.buttonCaptions.MinHeight = 20+5;
			this.buttonCaptions.AutoFocus = false;
			this.buttonCaptions.Margins = new Margins(2, 0, 10, 0);
			this.buttonCaptions.Dock = DockStyle.Left;
			this.buttonCaptions.Clicked += new MessageEventHandler(this.HandleButtonClicked);

			this.buttonCommands = new IconButtonMark(this);
			this.buttonCommands.Text = Res.Strings.BundleType.Commands;
			this.buttonCommands.Name = "Commands";
			this.buttonCommands.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonCommands.SiteMark = SiteMark.OnBottom;
			this.buttonCommands.MarkDimension = 5;
			this.buttonCommands.PreferredWidth = 100;
			this.buttonCommands.MinHeight = 20+5;
			this.buttonCommands.AutoFocus = false;
			this.buttonCommands.Margins = new Margins(2, 0, 10, 0);
			this.buttonCommands.Dock = DockStyle.Left;
			this.buttonCommands.Clicked += new MessageEventHandler(this.HandleButtonClicked);

			this.buttonTypes = new IconButtonMark(this);
			this.buttonTypes.Text = Res.Strings.BundleType.Types;
			this.buttonTypes.Name = "Types";
			this.buttonTypes.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonTypes.SiteMark = SiteMark.OnBottom;
			this.buttonTypes.MarkDimension = 5;
			this.buttonTypes.PreferredWidth = 100;
			this.buttonTypes.MinHeight = 20+5;
			this.buttonTypes.AutoFocus = false;
			this.buttonTypes.Margins = new Margins(2, 0, 10, 0);
			this.buttonTypes.Dock = DockStyle.Left;
			this.buttonTypes.Clicked += new MessageEventHandler(this.HandleButtonClicked);

			this.buttonScripts = new IconButtonMark(this);
			this.buttonScripts.Text = Res.Strings.BundleType.Scripts;
			this.buttonScripts.Name = "Scripts";
			this.buttonScripts.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonScripts.SiteMark = SiteMark.OnBottom;
			this.buttonScripts.MarkDimension = 5;
			this.buttonScripts.PreferredWidth = 100;
			this.buttonScripts.MinHeight = 20+5;
			this.buttonScripts.AutoFocus = false;
			this.buttonScripts.Margins = new Margins(2, 0, 10, 0);
			this.buttonScripts.Dock = DockStyle.Left;
			this.buttonScripts.Clicked += new MessageEventHandler(this.HandleButtonClicked);

			this.UpdateButtons();
		}

		public BundleType(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}


		public string CurrentType
		{
			get
			{
				return this.currentType;
			}

			set
			{
				if (this.currentType != value)
				{
					this.currentType = value;
					this.UpdateButtons();
					this.OnTypeChanged();
				}
			}
		}


		protected void UpdateButtons()
		{
			this.buttonStrings.ActiveState  = (this.currentType == "Strings" ) ? ActiveState.Yes : ActiveState.No;
			this.buttonPanels .ActiveState  = (this.currentType == "Panels"  ) ? ActiveState.Yes : ActiveState.No;
			this.buttonCaptions.ActiveState = (this.currentType == "Captions") ? ActiveState.Yes : ActiveState.No;
			this.buttonCommands.ActiveState = (this.currentType == "Commands") ? ActiveState.Yes : ActiveState.No;
			this.buttonTypes.ActiveState    = (this.currentType == "Types"   ) ? ActiveState.Yes : ActiveState.No;
			this.buttonScripts.ActiveState  = (this.currentType == "Scripts" ) ? ActiveState.Yes : ActiveState.No;
		}


		void HandleButtonClicked(object sender, MessageEventArgs e)
		{
			IconButtonMark button = sender as IconButtonMark;
			this.CurrentType = button.Name;
		}


		#region Events handler
		protected virtual void OnTypeChanged()
		{
			//	G�n�re un �v�nement pour dire qu'une cellule a �t� s�lectionn�e.
			EventHandler handler = (EventHandler) this.GetUserEventHandler("TypeChanged");
			if (handler != null)
			{
				handler(this);
			}
		}

		public event Support.EventHandler TypeChanged
		{
			add
			{
				this.AddUserEventHandler("TypeChanged", value);
			}
			remove
			{
				this.RemoveUserEventHandler("TypeChanged", value);
			}
		}
		#endregion


		protected string					currentType = "Strings";
		protected IconButtonMark			buttonStrings;
		protected IconButtonMark			buttonPanels;
		protected IconButtonMark			buttonScripts;
		protected IconButtonMark			buttonCaptions;
		protected IconButtonMark			buttonCommands;
		protected IconButtonMark			buttonTypes;
	}
}
