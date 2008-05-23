using System.Collections.Generic;
using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;
using Epsitec.Common.Document;

namespace Epsitec.Common.Document.Containers
{
	/// <summary>
	/// Cette classe gère le panneau des vues des pages miniatures.
	/// </summary>
	public class PageMiniatures
	{
		public PageMiniatures(Document document)
		{
			this.document = document;

			this.pages = new List<int>();
			this.pagesObject = new List<Objects.Page>();
			this.showAllPages = true;
		}


		public bool IsPanelShowed
		{
			get
			{
				return this.isPanelShowed;
			}
			set
			{
				this.isPanelShowed = value;
			}
		}

		public void CreateInterface(FrameBox parentPanel)
		{
			//	Crée l'interface nécessaire pour les pages miniatures dans un panneau dédié.
			this.parentPanel = parentPanel;

			//	Crée puis peuple la barre d'outils.
			VToolBar toolbar = new VToolBar(this.parentPanel);
			toolbar.Dock = DockStyle.Left;
			toolbar.Margins = new Margins(0, -1, 0, 0);

			this.buttonAll = new IconButton();
			this.buttonAll.Clicked += new MessageEventHandler(this.HandleButtonAll);
			toolbar.Items.Add(this.buttonAll);

			GlyphButton buttonMenu = new GlyphButton();
			buttonMenu.ButtonStyle = ButtonStyle.ComboItem;
			buttonMenu.GlyphShape = GlyphShape.Menu;
			buttonMenu.AutoFocus = false;
			buttonMenu.Clicked += new MessageEventHandler(this.HandleButtonMenu);
			ToolTip.Default.SetToolTip(buttonMenu, Res.Strings.Container.PageMiniatures.Button.Menu.Tooltip);
			toolbar.Items.Add(buttonMenu);

			this.slider = new VSlider();
			this.slider.MinValue = 1M;
			this.slider.MaxValue = 4M;
			this.slider.SmallChange = 1M;
			this.slider.LargeChange = 1M;
			this.slider.Resolution = 0.01M;
			this.slider.Value = 2M;
			this.slider.PreferredHeight = 60;
			//?this.slider.PreferredWidth = 22-4-4;
			this.slider.Margins = new Margins(4, 4, 4, 0);
			this.slider.Dock = DockStyle.Bottom;
			this.slider.ValueChanged += new EventHandler(this.HandleSliderValueChanged);
			ToolTip.Default.SetToolTip(this.slider, Res.Strings.Container.PageMiniatures.Slider.Tooltip);
			toolbar.Items.Add(this.slider);

			//	Crée la zone contenant les miniatures.
			this.scrollable = new Scrollable(this.parentPanel);
			this.scrollable.Dock = DockStyle.Fill;
			this.scrollable.VerticalScrollerMode = ScrollableScrollerMode.HideAlways;
			this.scrollable.HorizontalScrollerMode = ScrollableScrollerMode.ShowAlways;
			this.scrollable.Viewport.IsAutoFitting = true;
			this.scrollable.PaintForegroundFrame = true;
			this.scrollable.ForegroundFrameMargins = new Margins(0, 0, 0, 1);

			this.UpdateButtons();
		}


		public void UpdatePageAfterChanging()
		{
			//	Adapte les miniatures après un changement de page (création d'une
			//	nouvelle page, suppression d'une page, etc.).
			if (this.showAllPages)
			{
				this.pages.Clear();

				int total = this.TotalPages;
				for (int i=0; i<total; i++)
				{
					this.pages.Add(i);
				}
			}
			else
			{
				this.RestoreRealObjects();
			}

			this.Create();
		}


		private void HandleButtonMenu(object sender, MessageEventArgs e)
		{
			//	Appelé lorsque le bouton pour ajouter une nouvelle miniature est cliqué.
			AbstractButton button = sender as AbstractButton;
			if (button == null)  return;

			UndoableList pages = this.document.DocumentObjects;  // liste des pages
			VMenu menu = Objects.Page.CreateMenu(pages, this.pages, null, this.HandleButtonMenuPressed);
			menu.Host = button.Window;
			TextFieldCombo.AdjustComboSize(button, menu, false);
			menu.ShowAsComboList(button, Point.Zero, button);
		}

		private void HandleButtonMenuPressed(object sender, MessageEventArgs e)
		{
			//	Appelé lorsqu'une page est choisie dans le menu des pages miniatures.
			MenuItem item = sender as MenuItem;
			int page = System.Convert.ToInt32(item.Name);

			if (this.pages.Contains(page))
			{
				this.pages.Remove(page);  // ne montre plus cette page
			}
			else
			{
				this.pages.Add(page);  // montre cette page
				this.pages.Sort();
			}

			this.showAllPages = false;
			this.MemorizeRealObjects();

			this.Create();
		}

		private void HandleButtonAll(object sender, MessageEventArgs e)
		{
			//	Appelé lorsque le bouton pour montrer/cacher toutes les miniatures est cliqué.
			if (this.pages.Count == 0)  // montrer tout ?
			{
				int total = this.TotalPages;
				for (int i=0; i<total; i++)
				{
					this.pages.Add(i);
				}

				this.showAllPages = true;
			}
			else  // cacher tout ?
			{
				this.pages.Clear();

				this.showAllPages = false;
				this.MemorizeRealObjects();
			}

			this.Create();
		}

		private void HandleSliderValueChanged(object sender)
		{
			//	Appelé lorsque la taille des miniatures a changé.
			this.Create();
		}


		protected void Create()
		{
			//	Crée toutes les pages miniatures.
			if (this.parentPanel.Window == null)  // initialisation du logiciel ?
			{
				return;
			}

			double offsetX = this.scrollable.ViewportOffsetX;
			this.Clear();  // supprime les miniatures existantes

			if (!this.isPanelShowed)
			{
				return;
			}

			double zoom = (double) this.slider.Value;
			int currentPage = this.CurrentPage;
			double posX = 0;
			double requiredHeight = 0;

			foreach (int page in this.pages)
			{
				Size pageSize = this.document.GetPageSize(page);
				double w = System.Math.Ceiling(zoom*pageSize.Width*50/2970);
				double h = System.Math.Ceiling(zoom*pageSize.Height*50/2970);

				string pageName = this.PageName(page);

				FrameBox box = new FrameBox(this.scrollable.Viewport);
				box.Index = page;
				box.PreferredSize = new Size(w, h+PageMiniatures.labelHeight);
				box.Padding = new Margins(2, 2, 2, 2);
				box.Anchor = AnchorStyles.TopLeft;
				box.Margins = new Margins(posX, 0, 0, 0);
				box.Clicked += new MessageEventHandler(this.HandlePageBoxClicked);
				ToolTip.Default.SetToolTip(box, pageName);

				//	Crée la vue de la page miniature.
				Viewer viewer = new Viewer(this.document);
				viewer.SetParent(box);
				viewer.Dock = DockStyle.Fill;
				viewer.PreferredSize = new Size(w, h);
				viewer.IsDocumentPreview = true;
				viewer.PaintPageFrame = false;
				viewer.DrawingContext.LayerDrawingMode = LayerDrawingMode.ShowInactive;
				viewer.DrawingContext.InternalPageLayer(page, 0);
				viewer.DrawingContext.PreviewActive = true;
				viewer.DrawingContext.PreviewActive = true;
				viewer.DrawingContext.GridShow = false;
				viewer.DrawingContext.GuidesShow = false;
				viewer.Window.ForceLayout();
				viewer.DrawingContext.ZoomPageAndCenter();
				this.document.Modifier.AttachViewer(viewer);
				this.document.Notifier.NotifyArea(viewer);

				//	Crée la légende en bas, avec le numéro+nom de la page.
				StaticText label = new StaticText(box);
				label.PreferredSize = new Size(w, PageMiniatures.labelHeight);
				label.Dock = DockStyle.Bottom;
				label.ContentAlignment = ContentAlignment.TopCenter;
				label.Text = Misc.FontSize(pageName, 0.75);

				if (page == currentPage)
				{
					viewer.DrawingContext.PreviewActive = false;
					box.BackColor = viewer.DrawingContext.HiliteOutlineColor;
					viewer.DrawingContext.PreviewActive = true;
				}

				posX += w+2;
				requiredHeight = System.Math.Max(requiredHeight, h);
			}

			this.UpdateButtons();
			this.scrollable.ViewportOffsetX = offsetX;
			this.parentPanel.PreferredHeight = requiredHeight+PageMiniatures.labelHeight+23;
		}

		protected void Clear()
		{
			//	Supprime toutes les miniatures.
			List<Viewer> viewers = new List<Viewer>();
			foreach (Viewer viewer in this.document.Modifier.Viewers)
			{
				if (viewer.IsDocumentPreview)
				{
					viewers.Add(viewer);
				}
			}

			foreach (Viewer viewer in viewers)
			{
				this.document.Modifier.DetachViewer(viewer);
			}

			this.scrollable.Viewport.Children.Clear();
		}


		private void HandlePageBoxClicked(object sender, MessageEventArgs e)
		{
			//	Une page miniature a été cliquée.
			FrameBox box = sender as FrameBox;
			this.CurrentPage = box.Index;  // rend la page cliquée active
		}


		protected void UpdateButtons()
		{
			//	Met à jour les boutons.
			if (this.pages.Count == 0)
			{
				this.buttonAll.IconName = Misc.Icon("MiniaturesAllShow");
				ToolTip.Default.SetToolTip(this.buttonAll, Res.Strings.Container.PageMiniatures.Button.AllShow.Tooltip);
			}
			else
			{
				this.buttonAll.IconName = Misc.Icon("MiniaturesAllHide");
				ToolTip.Default.SetToolTip(this.buttonAll, Res.Strings.Container.PageMiniatures.Button.AllHide.Tooltip);
			}
		}


		protected void MemorizeRealObjects()
		{
			//	Mémorise les objets Page réels correspondants aux numéros des miniatures.
			this.pagesObject.Clear();

			UndoableList doc = this.document.DocumentObjects;
			for (int rank=0; rank<doc.Count; rank++)
			{
				if (this.pages.Contains(rank))
				{
					Objects.Page page = doc[rank] as Objects.Page;
					this.pagesObject.Add(page);
				}
			}
		}

		protected void RestoreRealObjects()
		{
			//	Régénère la liste des numéros des miniatures en fonction des objets Page réels mémorisés.
			this.pages.Clear();

			UndoableList doc = this.document.DocumentObjects;
			foreach (Objects.Page page in this.pagesObject)
			{
				int rank = doc.IndexOf(page);
				if (rank != -1)
				{
					this.pages.Add(rank);
				}
			}

			this.pages.Sort();
		}


		protected string PageName(int rank)
		{
			//	Retourne le nom le plus complet possible d'une page, constitué du numéro
			//	suivi de son éventuel nom.
			UndoableList doc = this.document.DocumentObjects;
			Objects.Page page = doc[rank] as Objects.Page;

			string longName = page.LongName;
			if (string.IsNullOrEmpty(longName))
			{
				return string.Concat("<b>", page.ShortName, "</b>");
			}
			else
			{
				return string.Concat("<b>", page.ShortName, "</b> ", longName);
			}
		}

		protected int TotalPages
		{
			//	Retourne le nombre total de pages.
			get
			{
				return this.document.Modifier.ActiveViewer.DrawingContext.TotalPages();
			}
		}

		protected int CurrentPage
		{
			//	Retourne le rang de la page courante.
			get
			{
				return this.document.Modifier.ActiveViewer.DrawingContext.CurrentPage;
			}
			set
			{
				this.document.Modifier.ActiveViewer.DrawingContext.CurrentPage = value;
			}
		}


		protected static readonly double		labelHeight = 12;

		protected Document						document;
		protected List<int>						pages;
		protected List<Objects.Page>			pagesObject;
		protected FrameBox						parentPanel;
		protected IconButton					buttonAll;
		protected VSlider						slider;
		protected Scrollable					scrollable;
		protected bool							showAllPages;
		protected bool							isPanelShowed;
	}
}
