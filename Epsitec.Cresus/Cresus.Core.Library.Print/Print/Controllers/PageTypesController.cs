//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Dialogs;

using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Documents;
using Epsitec.Cresus.Core.Documents.Verbose;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Print.Controllers
{
	public class PageTypesController
	{
		public PageTypesController(IBusinessContext businessContext, DocumentPrintingUnitsEntity documentPrintingUnitsEntity)
		{
			this.businessContext             = businessContext;
			this.documentPrintingUnitsEntity = documentPrintingUnitsEntity;

			this.pageTypes = this.documentPrintingUnitsEntity.GetPageTypes ();
			this.allPageTypes = VerbosePageType.GetAll ().ToList ();
			this.checkButtons = new List<CheckButton> ();
		}


		public void CreateUI(Widget parent)
		{
			foreach (var pageType in this.allPageTypes)
			{
				var button = new CheckButton
				{
					Parent = parent,
					Text = pageType.ShortDescription,
					Name = pageType.Type.ToString (),
					ActiveState = (this.pageTypes.Contains (pageType.Type)) ? ActiveState.Yes : ActiveState.No,
					AutoToggle = false,
					Dock = DockStyle.Top,
				};

				button.Clicked += delegate
				{
					PageType type;
					if (System.Enum.TryParse (button.Name, out type))
					{
						this.ActionToggle (type);
					}
				};

				this.checkButtons.Add (button);
			}

			this.noneButton = new Button
			{
				Parent = parent,
				Text = "N'utilise aucun",
				Dock = DockStyle.Top,
				Margins = new Margins (0, 0, 10, 0),
			};

			this.allButton = new Button
			{
				Parent = parent,
				Text = "Utilise tout",
				Dock = DockStyle.Top,
				Margins = new Margins (0, 0, 1, 0),
			};

			//	Conexion des événements.
			this.noneButton.Clicked += delegate
			{
				this.ActionAll (false);
			};

			this.allButton.Clicked += delegate
			{
				this.ActionAll (true);
			};

			this.UpdateWidgets ();
		}


		public void SaveDesign()
		{
			this.documentPrintingUnitsEntity.SetPageTypes (this.pageTypes);
		}


		public void Update()
		{
		}


		private void ActionToggle(PageType pageType)
		{
			if (this.pageTypes.Contains (pageType))
			{
				this.pageTypes.Remove (pageType);
			}
			else
			{
				this.pageTypes.Add (pageType);
			}

			this.UpdateWidgets ();
		}

		private void ActionAll(bool value)
		{
			this.pageTypes.Clear ();

			if (value)
			{
				foreach (var pageType in this.allPageTypes)
				{
					this.pageTypes.Add (pageType.Type);
				}
			}

			this.UpdateWidgets ();
		}


		private void UpdateWidgets()
		{
			foreach (var button in this.checkButtons)
			{
				PageType type;
				if (System.Enum.TryParse (button.Name, out type))
				{
					button.ActiveState = (this.pageTypes.Contains (type)) ? ActiveState.Yes : ActiveState.No;
				}
			}

			this.noneButton.Enable = (this.pageTypes.Count != 0);
			this.allButton.Enable  = (this.pageTypes.Count != this.allPageTypes.Count);
		}


		private void SetDirty()
		{
			this.businessContext.NotifyExternalChanges ();
		}

	
		private readonly IBusinessContext					businessContext;
		private readonly DocumentPrintingUnitsEntity		documentPrintingUnitsEntity;
		private readonly List<PageType>						pageTypes;
		private readonly List<VerbosePageType>				allPageTypes;
		private readonly List<CheckButton>					checkButtons;

		private Button										noneButton;
		private Button										allButton;
	}
}
