using System.Collections.Generic;
using System.Text.RegularExpressions;
using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Designer.Viewers
{
	/// <summary>
	/// Permet de représenter les ressources d'un module.
	/// </summary>
	public class Entities : AbstractCaptions
	{
		protected internal Entities(Module module, PanelsContext context, ResourceAccess access, DesignerApplication designerApplication)
			: base (module, context, access, designerApplication)
		{
			bool isFullScreen = (this.designerApplication.DisplayModeState == DesignerApplication.DisplayMode.FullScreen);
			bool isWindow     = (this.designerApplication.DisplayModeState == DesignerApplication.DisplayMode.Window);

			this.lastGroup.Dock = isWindow ? DockStyle.Fill : DockStyle.Top;
			this.lastGroup.Visibility = !isFullScreen;

			this.hsplitter = new HSplitter(this.lastPane);
			this.hsplitter.Dock = DockStyle.Top;
			this.hsplitter.Visibility = (!isFullScreen && !isWindow);

			var editorGroup = new FrameBox(isWindow ? (Widget) this.designerApplication.ViewersWindow.Root : this.lastPane);
			double m = isWindow ? 0 : 10;
			editorGroup.Padding = new Margins(m, m, m, m);
			editorGroup.Dock = DockStyle.Fill;

			//	Crée les grands blocs de widgets.
			var band = new FrameBox (editorGroup);
			band.Dock = DockStyle.Fill;

			this.editor = new EntitiesEditor.Editor(band);
			this.editor.Entities = this;
			this.editor.Module = this.module;
			this.editor.Dock = DockStyle.Fill;
			this.editor.AreaSize = this.areaSize;
			this.editor.Zoom = this.Zoom;
			this.editor.SizeChanged += new EventHandler<DependencyPropertyChangedEventArgs>(this.HandleEditorSizeChanged);
			this.editor.AreaSizeChanged += this.HandleEditorAreaSizeChanged;
			this.editor.AreaOffsetChanged += this.HandleEditorAreaOffsetChanged;
			this.editor.ZoomChanged += this.HandleEditorZoomChanged;
			ToolTip.Default.RegisterDynamicToolTipHost(this.editor);  // pour voir les tooltips dynamiques

			this.vscroller = new VScroller(band);
			this.vscroller.IsInverted = true;
			this.vscroller.Dock = DockStyle.Right;
			this.vscroller.ValueChanged += this.HandleScrollerValueChanged;
			this.editor.VScroller = this.vscroller;

			this.groupToolbar = new MyWidgets.ResetBox(editorGroup);
			this.groupToolbar.IsPatch = this.module.IsPatch;
			this.groupToolbar.Dock = DockStyle.Bottom;
			this.groupToolbar.Margins = new Margins(0, 0, 5, 0);
			this.groupToolbar.ResetButton.Clicked += this.HandleResetButtonClicked;

			this.toolbar = new HToolBar(this.groupToolbar.GroupBox);
			this.toolbar.Dock = DockStyle.Fill;

			this.hscroller = new HScroller(editorGroup);
			this.hscroller.Margins = new Margins(0, this.vscroller.PreferredWidth, 0, 0);
			this.hscroller.Dock = DockStyle.Bottom;
			this.hscroller.ValueChanged += this.HandleScrollerValueChanged;

			//	Peuple la toolbar.
			this.buttonSubViewA = new MyWidgets.EntitySubView(this.toolbar);
			this.buttonSubViewA.Text = Res.Strings.Entities.SubView.Quick.A;
			this.buttonSubViewA.PreferredWidth = this.buttonSubViewA.PreferredHeight;
			this.buttonSubViewA.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonSubViewA.AutoFocus = false;
			this.buttonSubViewA.Dock = DockStyle.Left;
			this.buttonSubViewA.Clicked += this.HandleButtonSubViewClicked;
			this.buttonSubViewA.DragStarting += this.HandleButtonSubViewDragStarting;
			this.buttonSubViewA.DragEnding += this.HandleButtonSubViewDragEnding;
			ToolTip.Default.SetToolTip(this.buttonSubViewA, Res.Strings.Entities.SubView.Long.A);

			this.buttonSubViewB = new MyWidgets.EntitySubView(this.toolbar);
			this.buttonSubViewB.Text = Res.Strings.Entities.SubView.Quick.B;
			this.buttonSubViewB.PreferredWidth = this.buttonSubViewB.PreferredHeight;
			this.buttonSubViewB.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonSubViewB.AutoFocus = false;
			this.buttonSubViewB.Dock = DockStyle.Left;
			this.buttonSubViewB.Clicked += this.HandleButtonSubViewClicked;
			this.buttonSubViewB.DragStarting += this.HandleButtonSubViewDragStarting;
			this.buttonSubViewB.DragEnding += this.HandleButtonSubViewDragEnding;
			ToolTip.Default.SetToolTip(this.buttonSubViewB, Res.Strings.Entities.SubView.Long.B);

			this.buttonSubViewC = new MyWidgets.EntitySubView(this.toolbar);
			this.buttonSubViewC.Text = Res.Strings.Entities.SubView.Quick.C;
			this.buttonSubViewC.PreferredWidth = this.buttonSubViewC.PreferredHeight;
			this.buttonSubViewC.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonSubViewC.AutoFocus = false;
			this.buttonSubViewC.Dock = DockStyle.Left;
			this.buttonSubViewC.Clicked += this.HandleButtonSubViewClicked;
			this.buttonSubViewC.DragStarting += this.HandleButtonSubViewDragStarting;
			this.buttonSubViewC.DragEnding += this.HandleButtonSubViewDragEnding;
			ToolTip.Default.SetToolTip(this.buttonSubViewC, Res.Strings.Entities.SubView.Long.C);

			this.buttonSubViewT = new MyWidgets.EntitySubView(this.toolbar);
			this.buttonSubViewT.Text = Res.Strings.Entities.SubView.Quick.T;
			this.buttonSubViewT.PreferredWidth = this.buttonSubViewT.PreferredHeight;
			this.buttonSubViewT.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonSubViewT.AutoFocus = false;
			this.buttonSubViewT.Dock = DockStyle.Left;
			this.buttonSubViewT.Margins = new Margins(2, 0, 0, 0);
			this.buttonSubViewT.Clicked += this.HandleButtonSubViewClicked;
			this.buttonSubViewT.DragStarting += this.HandleButtonSubViewDragStarting;
			this.buttonSubViewT.DragEnding += this.HandleButtonSubViewDragEnding;
			ToolTip.Default.SetToolTip(this.buttonSubViewT, Res.Strings.Entities.SubView.Long.T);

			IconSeparator sep = new IconSeparator(this.toolbar);
			sep.Dock = DockStyle.Left;

			this.buttonZoomPage = new IconButton(this.toolbar);
			this.buttonZoomPage.IconUri = Misc.Icon("ZoomPage");
			this.buttonZoomPage.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonZoomPage.AutoFocus = false;
			this.buttonZoomPage.Dock = DockStyle.Left;
			this.buttonZoomPage.Clicked += this.HandleButtonZoomClicked;
			ToolTip.Default.SetToolTip(this.buttonZoomPage, Res.Strings.Entities.Action.ZoomPage);

			this.buttonZoomMin = new IconButton(this.toolbar);
			this.buttonZoomMin.IconUri = Misc.Icon("ZoomMin");
			this.buttonZoomMin.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonZoomMin.AutoFocus = false;
			this.buttonZoomMin.Dock = DockStyle.Left;
			this.buttonZoomMin.Clicked += this.HandleButtonZoomClicked;
			ToolTip.Default.SetToolTip(this.buttonZoomMin, Res.Strings.Entities.Action.ZoomMin);

			this.buttonZoomDefault = new IconButton(this.toolbar);
			this.buttonZoomDefault.IconUri = Misc.Icon("ZoomDefault");
			this.buttonZoomDefault.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonZoomDefault.AutoFocus = false;
			this.buttonZoomDefault.Dock = DockStyle.Left;
			this.buttonZoomDefault.Clicked += this.HandleButtonZoomClicked;
			ToolTip.Default.SetToolTip(this.buttonZoomDefault, Res.Strings.Entities.Action.ZoomDefault);

			this.buttonZoomMax = new IconButton(this.toolbar);
			this.buttonZoomMax.IconUri = Misc.Icon("ZoomMax");
			this.buttonZoomMax.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonZoomMax.AutoFocus = false;
			this.buttonZoomMax.Dock = DockStyle.Left;
			this.buttonZoomMax.Clicked += this.HandleButtonZoomClicked;
			ToolTip.Default.SetToolTip(this.buttonZoomMax, Res.Strings.Entities.Action.ZoomMax);

			this.fieldZoom = new StatusField(this.toolbar);
			this.fieldZoom.PreferredWidth = 50;
			this.fieldZoom.Margins = new Margins(5, 5, 1, 1);
			this.fieldZoom.Dock = DockStyle.Left;
			this.fieldZoom.Clicked += this.HandleFieldZoomClicked;
			ToolTip.Default.SetToolTip(this.fieldZoom, Res.Strings.Entities.Action.ZoomMenu);

			this.sliderZoom = new HSlider(this.toolbar);
			this.sliderZoom.MinValue = (decimal) Entities.zoomMin;
			this.sliderZoom.MaxValue = (decimal) Entities.zoomMax;
			this.sliderZoom.SmallChange = (decimal) 0.1;
			this.sliderZoom.LargeChange = (decimal) 0.2;
			this.sliderZoom.Resolution = (decimal) 0.01;
			this.sliderZoom.PreferredWidth = 90;
			this.sliderZoom.Margins = new Margins(0, 0, 4, 4);
			this.sliderZoom.Dock = DockStyle.Left;
			this.sliderZoom.ValueChanged += this.HandleSliderZoomValueChanged;
			ToolTip.Default.SetToolTip(this.sliderZoom, Res.Strings.Entities.Action.ZoomSlider);

			this.buttonGrid = new IconButton (this.toolbar);
			this.buttonGrid.IconUri = Misc.Icon ("Grid");
			this.buttonGrid.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonGrid.AutoFocus = false;
			this.buttonGrid.Margins = new Margins (10, 0, 0, 0);
			this.buttonGrid.Dock = DockStyle.Left;
			this.buttonGrid.Clicked += this.HandleButtonGridClicked;
			ToolTip.Default.SetToolTip (this.buttonGrid, "Grille magnétique");

			this.buttonSaveAllImages = new IconButton (this.toolbar);
			this.buttonSaveAllImages.IconUri = Misc.Icon ("SaveAll");
			this.buttonSaveAllImages.AutoFocus = false;
			this.buttonSaveAllImages.Dock = DockStyle.Right;
			this.buttonSaveAllImages.Clicked += this.HandleButtonSaveAllImagesClicked;
			ToolTip.Default.SetToolTip (this.buttonSaveAllImages, Res.Strings.Entities.Action.SaveAllBitmaps);

			this.buttonSaveImage = new IconButton (this.toolbar);
			this.buttonSaveImage.IconUri = Misc.Icon ("Save");
			this.buttonSaveImage.AutoFocus = false;
			this.buttonSaveImage.Dock = DockStyle.Right;
			this.buttonSaveImage.Clicked += this.HandleButtonSaveImageClicked;
			ToolTip.Default.SetToolTip (this.buttonSaveImage, Res.Strings.Entities.Action.SaveBitmap);

			this.AreaSize = new Size (100, 100);

			this.editor.UpdateGeometry();
			this.UpdateZoom();
			this.UpdateAll();
			this.UpdateSubView();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.editor.SizeChanged -= new EventHandler<DependencyPropertyChangedEventArgs>(this.HandleEditorSizeChanged);
				this.editor.AreaSizeChanged -= this.HandleEditorAreaSizeChanged;
				this.editor.AreaOffsetChanged -= this.HandleEditorAreaOffsetChanged;
				this.editor.ZoomChanged -= this.HandleEditorZoomChanged;

				this.vscroller.ValueChanged -= this.HandleScrollerValueChanged;
				this.hscroller.ValueChanged -= this.HandleScrollerValueChanged;

				this.groupToolbar.ResetButton.Clicked -= this.HandleResetButtonClicked;

				this.buttonSubViewA.Clicked -= this.HandleButtonSubViewClicked;
				this.buttonSubViewA.DragStarting -= this.HandleButtonSubViewDragStarting;
				this.buttonSubViewA.DragEnding -= this.HandleButtonSubViewDragEnding;
				this.buttonSubViewB.Clicked -= this.HandleButtonSubViewClicked;
				this.buttonSubViewB.DragStarting -= this.HandleButtonSubViewDragStarting;
				this.buttonSubViewB.DragEnding -= this.HandleButtonSubViewDragEnding;
				this.buttonSubViewC.Clicked -= this.HandleButtonSubViewClicked;
				this.buttonSubViewC.DragStarting -= this.HandleButtonSubViewDragStarting;
				this.buttonSubViewC.DragEnding -= this.HandleButtonSubViewDragEnding;
				this.buttonSubViewT.Clicked -= this.HandleButtonSubViewClicked;
				this.buttonSubViewT.DragStarting -= this.HandleButtonSubViewDragStarting;
				this.buttonSubViewT.DragEnding -= this.HandleButtonSubViewDragEnding;
				
				this.buttonZoomPage.Clicked -= this.HandleButtonZoomClicked;
				this.buttonZoomMin.Clicked -= this.HandleButtonZoomClicked;
				this.buttonZoomDefault.Clicked -= this.HandleButtonZoomClicked;
				this.buttonZoomMax.Clicked -= this.HandleButtonZoomClicked;
				
				this.fieldZoom.Clicked -= this.HandleFieldZoomClicked;
				this.sliderZoom.ValueChanged -= this.HandleSliderZoomValueChanged;

				this.buttonGrid.Clicked -= this.HandleButtonGridClicked;
				this.buttonSaveImage.Clicked -= this.HandleButtonSaveImageClicked;
				this.buttonSaveAllImages.Clicked -= this.HandleButtonSaveAllImagesClicked;
			}

			base.Dispose(disposing);
		}


		public override ResourceAccess.Type ResourceType
		{
			get
			{
				return ResourceAccess.Type.Entities;
			}
		}

		public override bool HasUsefulViewerWindow
		{
			//	Indique si cette vue a l'utilité d'une fenêtre supplémentaire.
			get
			{
				return true;
			}
		}


		public bool EditExpression(Druid fieldId)
		{
			//	Edite l'expression d'un champ.
			if (this.editor == null)
			{
				return false;
			}

			return this.editor.EditExpression(fieldId);
		}


		private Size AreaSize
		{
			//	Dimensions de la surface pour représenter les boîtes et les liaisons.
			get
			{
				return this.areaSize;
			}
			set
			{
				if (this.areaSize != value)
				{
					this.areaSize = value;

					this.editor.AreaSize = this.areaSize;
					this.UpdateScroller();
				}
			}
		}

		public int SubView
		{
			//	Sous-vue utilisée pour représenter les boîtes et les liaisons.
			get
			{
				return Entities.subView;
			}
			set
			{
				if (Entities.subView != value)
				{
					if (!this.designerApplication.Terminate())
					{
						return;
					}

					Entities.subView = value;
					this.designerApplication.LocatorFix();

					this.UpdateSubView();
					this.UpdateTitle();
					this.UpdateEdit();
					this.UpdateColor();
					this.UpdateModificationsCulture();
					this.UpdateCommands();
				}
			}
		}

		public static string SubViewName(int subView)
		{
			//	Retourne le nom de la sous-vue utilisée.
			switch (subView)
			{
				case 0:   return Res.Strings.Entities.SubView.Quick.A;
				case 1:   return Res.Strings.Entities.SubView.Quick.B;
				case 2:   return Res.Strings.Entities.SubView.Quick.C;
				default:  return Res.Strings.Entities.SubView.Quick.T;
			}
		}

		public double Zoom
		{
			//	Zoom pour représenter les boîtes et les liaisons.
			get
			{
				if (Entities.isZoomPage)
				{
					return this.ZoomPage;
				}
				else
				{
					return Entities.zoom;
				}
			}
			set
			{
				if (Entities.zoom != value)
				{
					Entities.zoom = value;

					this.UpdateZoom();
					this.UpdateScroller();
				}
			}
		}

		private double ZoomPage
		{
			//	Retourne le zoom permettant de voir toute la surface de travail.
			get
			{
				double zx = this.editor.Client.Bounds.Width  / this.editor.AreaSize.Width;
				double zy = this.editor.Client.Bounds.Height / this.editor.AreaSize.Height;
				double zoom = System.Math.Min(zx, zy);

				zoom = System.Math.Max(zoom, Entities.zoomMin);
				zoom = System.Math.Min(zoom, Entities.zoomMax);
				
				zoom = System.Math.Floor(zoom*100)/100;  // 45.8% -> 46%
				return zoom;
			}
		}

		protected override void UpdateTitle()
		{
			//	Met à jour le titre en dessus de la zone scrollable.
			base.UpdateTitle();

			foreach (EntitiesEditor.ObjectBox box in this.editor.Boxes)
			{
				box.UpdateTitle();
			}
		}

		private void UpdateZoom()
		{
			//	Met à jour tout ce qui dépend du zoom.
			this.editor.Zoom = this.Zoom;

			this.fieldZoom.Text = string.Concat(System.Math.Floor(this.Zoom*100).ToString(), "%");

			this.ignoreChange = true;
			this.sliderZoom.Value = (decimal) this.Zoom;
			this.ignoreChange = false;

			this.buttonZoomPage.ActiveState    = (Entities.isZoomPage              ) ? ActiveState.Yes : ActiveState.No;
			this.buttonZoomMin.ActiveState     = (this.Zoom == Entities.zoomMin    ) ? ActiveState.Yes : ActiveState.No;
			this.buttonZoomDefault.ActiveState = (this.Zoom == Entities.zoomDefault) ? ActiveState.Yes : ActiveState.No;
			this.buttonZoomMax.ActiveState     = (this.Zoom == Entities.zoomMax    ) ? ActiveState.Yes : ActiveState.No;
		}

		private void UpdateScroller()
		{
			//	Met à jour les ascenseurs, en fonction du zoom courant et de la taille de l'éditeur.
			double w = this.areaSize.Width*this.Zoom - this.editor.Client.Size.Width;
			if (w <= 0 || this.editor.Client.Size.Width <= 0)
			{
				this.hscroller.Enable = false;
			}
			else
			{
				this.hscroller.Enable = true;
				this.hscroller.MinValue = (decimal) 0;
				this.hscroller.MaxValue = (decimal) w;
				this.hscroller.SmallChange = (decimal) (w/10);
				this.hscroller.LargeChange = (decimal) (w/5);
				this.hscroller.VisibleRangeRatio = (decimal) (this.editor.Client.Size.Width / (this.areaSize.Width*this.Zoom));
			}

			double h = this.areaSize.Height*this.Zoom - this.editor.Client.Size.Height;
			if (h <= 0 || this.editor.Client.Size.Height <= 0)
			{
				this.vscroller.Enable = false;
			}
			else
			{
				this.vscroller.Enable = true;
				this.vscroller.MinValue = (decimal) 0;
				this.vscroller.MaxValue = (decimal) h;
				this.vscroller.SmallChange = (decimal) (h/10);
				this.vscroller.LargeChange = (decimal) (h/5);
				this.vscroller.VisibleRangeRatio = (decimal) (this.editor.Client.Size.Height / (this.areaSize.Height*this.Zoom));
			}

			this.editor.IsScrollerEnable = this.hscroller.Enable || this.vscroller.Enable;
			this.HandleScrollerValueChanged(null);
		}

		private void UpdateGrid()
		{
			this.buttonGrid.ActiveState = this.editor.Grid ? ActiveState.Yes : ActiveState.No;
		}


		protected override void UpdateEdit()
		{
			//	Met à jour les lignes éditables en fonction de la sélection dans le tableau.
			base.UpdateEdit();

			this.editor.Clear();

			if (!this.Deserialize())
			{
				CultureMap item = this.access.CollectionView.CurrentItem as CultureMap;

				if (item != null)
				{
					var box = new EntitiesEditor.ObjectBox(this.editor);
					box.IsRoot = true;  // la première boîte est toujours la boîte racine
					box.SetContent(item);
					this.editor.AddBox(box);
				}

				this.editor.CreateConnections();
				this.editor.UpdateAfterGeometryChanged(null);
			}

			this.UpdateReset();
			this.Zoom = this.Zoom;
		}

		public void UpdateReset()
		{
			//	Met à jour le bouton 'reset'.
			CultureMap item = this.access.CollectionView.CurrentItem as CultureMap;
			CultureMapSource source = this.access.GetCultureMapSource(item);
			this.ColorizeResetBox(this.groupToolbar, source, false);
		}

		private void UpdateSubView()
		{
			//	Met à jour le bouton sélectionné pour la sous-vue.
			this.buttonSubViewA.ActiveState = (this.SubView == 0) ? ActiveState.Yes : ActiveState.No;
			this.buttonSubViewB.ActiveState = (this.SubView == 1) ? ActiveState.Yes : ActiveState.No;
			this.buttonSubViewC.ActiveState = (this.SubView == 2) ? ActiveState.Yes : ActiveState.No;
			this.buttonSubViewT.ActiveState = (this.SubView == 3) ? ActiveState.Yes : ActiveState.No;
		}

		public override bool Terminate(bool soft)
		{
			//	Termine le travail sur une ressource, avant de passer à une autre.
			//	Si soft = true, on sérialise temporairement sans poser de question.
			//	Retourne false si l'utilisateur a choisi "annuler".
			bool dirty = this.module.AccessEntities.IsLocalDirty;

			base.Terminate(soft);

			if (dirty)
			{
				if (soft)
				{
					if (this.druidToSerialize.IsValid)
					{
						Entities.softSerialize = this.editor.Serialize();
					}
					else
					{
						Entities.softSerialize = null;
					}

					Entities.softDirtySerialization = this.module.AccessEntities.IsLocalDirty;
				}
				else
				{
					this.Serialize();
                    this.module.AccessEntities.PersistChanges();
					this.module.AccessEntities.ClearLocalDirty();
				}
			}

			return true;
		}

		private void DragSubView(int srcSubView, int dstSubView)
		{
			//	Effectue le drag & drop d'une sous-vue dans une autre.
			string header = string.Format(Res.Strings.Entities.Question.SubView.Base, this.nameToSerialize);

			List<string> questions = new List<string>();
			questions.Add(ConfirmationButton.FormatContent(Res.Strings.Entities.Question.SubView.Quick.Copy, string.Format(Res.Strings.Entities.Question.SubView.Long.Copy, Entities.SubViewName(srcSubView), Entities.SubViewName(dstSubView))));
			questions.Add(ConfirmationButton.FormatContent(Res.Strings.Entities.Question.SubView.Quick.Swap, string.Format(Res.Strings.Entities.Question.SubView.Long.Swap, Entities.SubViewName(srcSubView), Entities.SubViewName(dstSubView))));

			var result = this.designerApplication.DialogConfirmation (header, questions, true);

			if (result == Epsitec.Common.Dialogs.DialogResult.Cancel)
			{
				return;
			}

			if (result == Epsitec.Common.Dialogs.DialogResult.Answer1)  // copier ?
			{
				if (srcSubView == this.SubView)  // drag de la sous-vue courante ?
				{
					string data = this.editor.Serialize();
					Entities.SetSerializedData(this.access.Accessor, this.CurrentDruid, dstSubView, data);
				}
				else if (dstSubView == this.SubView)  // drag dans la sous-vue courante ?
				{
					string data = Entities.GetSerializedData(this.access.Accessor, this.CurrentDruid, srcSubView);
					Entities.SetSerializedData(this.access.Accessor, this.CurrentDruid, dstSubView, data);
					this.UpdateEdit();
				}
				else  // drag d'une sous-vue cachée vers une autre cachée ?
				{
					string data = Entities.GetSerializedData(this.access.Accessor, this.CurrentDruid, srcSubView);
					Entities.SetSerializedData(this.access.Accessor, this.CurrentDruid, dstSubView, data);
				}
			}

			if (result == Epsitec.Common.Dialogs.DialogResult.Answer2)  // permuter ?
			{
				if (srcSubView == this.SubView)  // drag de la sous-vue courante ?
				{
					string srcData = this.editor.Serialize();
					string dstData = Entities.GetSerializedData(this.access.Accessor, this.CurrentDruid, dstSubView);
					Entities.SetSerializedData(this.access.Accessor, this.CurrentDruid, dstSubView, srcData);
					Entities.SetSerializedData(this.access.Accessor, this.CurrentDruid, srcSubView, dstData);
				}
				else if (dstSubView == this.SubView)  // drag dans la sous-vue courante ?
				{
					string srcData = Entities.GetSerializedData(this.access.Accessor, this.CurrentDruid, srcSubView);
					string dstData = this.editor.Serialize();
					Entities.SetSerializedData(this.access.Accessor, this.CurrentDruid, dstSubView, srcData);
					Entities.SetSerializedData(this.access.Accessor, this.CurrentDruid, srcSubView, dstData);
					this.UpdateEdit();
				}
				else  // drag d'une sous-vue cachée vers une autre cachée ?
				{
					string srcData = Entities.GetSerializedData(this.access.Accessor, this.CurrentDruid, srcSubView);
					string dstData = Entities.GetSerializedData(this.access.Accessor, this.CurrentDruid, dstSubView);
					Entities.SetSerializedData(this.access.Accessor, this.CurrentDruid, dstSubView, srcData);
					Entities.SetSerializedData(this.access.Accessor, this.CurrentDruid, srcSubView, dstData);
				}
			}

			this.SubView = dstSubView;
		}

		private void Serialize()
		{
			//	Sérialise les données.
			if (this.druidToSerialize.IsValid)
			{
				string data = this.editor.Serialize();
				Entities.SetSerializedData(this.access.Accessor, this.druidToSerialize, this.SubView, data);
			}
		}

		private bool Deserialize()
		{
			//	Désérialise les données sérialisées. Retourne false s'il n'existe aucune donnée sérialisée.
			this.nameToSerialize = this.CurrentName;
			this.druidToSerialize = this.CurrentDruid;

			if (Entities.softSerialize == null)
			{
				string data = Entities.GetSerializedData(this.access.Accessor, this.druidToSerialize, this.SubView);
				if (data == null)
				{
					this.module.AccessEntities.ClearLocalDirty();
					return false;
				}
				else
				{
					this.editor.Deserialize(data);
					this.module.AccessEntities.ClearLocalDirty();
					return true;
				}
			}
			else
			{
				if (this.module.AccessEntities.IsLocalDirty)
				{
					this.module.AccessEntities.SetLocalDirty();
				}
				else
				{
					this.module.AccessEntities.ClearLocalDirty();
				}

				this.editor.Deserialize(Entities.softSerialize);

				Entities.softDirtySerialization = false;
				Entities.softSerialize = null;
				return true;
			}
		}

		private static void SetSerializedData(IResourceAccessor accessor, Druid druid, int subView, string data)
		{
			//	Sérialise des données. data vaut null s'il faut effacer les données sérialisées.
			CultureMap resource = accessor.Collection[druid];
			
			if (resource != null)
			{
				StructuredData record = resource.GetCultureData(Resources.DefaultTwoLetterISOLanguageName);
				string key = subView.ToString(System.Globalization.CultureInfo.InvariantCulture);
				Dictionary<string, string> dict = Entities.GetSerializedLayouts(record);
				
				if (string.IsNullOrEmpty(data))
				{
					if (dict.ContainsKey(key))
					{
						dict.Remove(key);
					}
				}
				else
				{
					//	Supprime l'en-tête XML <?xml...?> qui est inutile ici; on le regénèrera
					//	au besoin à la désérialisation :

					if (data.StartsWith(EntitiesEditor.Xml.XmlHeader))
					{
						data = data.Substring(EntitiesEditor.Xml.XmlHeader.Length);
					}

					dict[key] = data;
				}

				Entities.SetSerializedLayouts(record, dict);
			}
		}

		private static string GetSerializedData(IResourceAccessor accessor, Druid druid, int subView)
		{
			//	Désérialise des données. Retourne null s'il n'existe aucune donnée sérialisée.
			CultureMap resource = accessor.Collection[druid];
			
			if (resource != null)
			{
				StructuredData record = resource.GetCultureData(Resources.DefaultTwoLetterISOLanguageName);
				string key = subView.ToString(System.Globalization.CultureInfo.InvariantCulture);
				Dictionary<string, string> dict = Entities.GetSerializedLayouts(record);
				
				if (dict.ContainsKey(key))
				{
					string data = dict[key];

					//	Si les données ont été purgées de leur en-tête <?xml ...?>, alors on
					//	leur en remet un artificiellement :
					if (data.StartsWith("<?xml"))
					{
						return data;
					}
					else
					{
						return EntitiesEditor.Xml.XmlHeader + data;
					}
				}
			}
			return null;
		}

		private static Dictionary<string, string> GetSerializedLayouts(StructuredData record)
		{
			Dictionary<string, string> dict = new Dictionary<string, string>();
			string data = record.GetValue(Support.Res.Fields.ResourceStructuredType.SerializedDesignerLayouts) as string;

			string openElementPrefix = "<"+EntitiesEditor.Xml.Layout;
			string closeElement      = "</"+EntitiesEditor.Xml.Layout+">";
			
			while (!string.IsNullOrEmpty(data))
			{
				System.Diagnostics.Debug.Assert(data.StartsWith(openElementPrefix));

				//										//	<Layout id="1">...</Layout><Layout id="2">...
				int pos1 = data.IndexOf('"')+1;			//              ^: :  :        :
				int pos2 = data.IndexOf('"', pos1);		//	             ^ :  :        :
				int pos3 = data.IndexOf('>')+1;			//	               ^  :        :
				int pos4 = data.IndexOf(closeElement);	//	                  ^        :
				int pos5 = pos4 + closeElement.Length;	//	                           ^
				
				string id   = data.Substring(pos1, pos2-pos1);
				string node = data.Substring(pos3, pos4-pos3);

				dict[id] = node;

				data = data.Substring(pos5);
			}
			
			return dict;
		}

		private static void SetSerializedLayouts(StructuredData record, Dictionary<string, string> dict)
		{
			System.Text.StringBuilder buffer = new System.Text.StringBuilder();

			foreach (KeyValuePair<string, string> pair in dict)
			{
				buffer.Append("<");
				buffer.Append(EntitiesEditor.Xml.Layout);
				buffer.Append(@" id=""");
				buffer.Append(pair.Key);
				buffer.Append(@""">");

				buffer.Append(pair.Value);

				buffer.Append("</");
				buffer.Append(EntitiesEditor.Xml.Layout);
				buffer.Append(">");
			}

			record.SetValue(Support.Res.Fields.ResourceStructuredType.SerializedDesignerLayouts, buffer.ToString());
		}


		private Druid CurrentDruid
		{
			get
			{
				CultureMap item = this.access.CollectionView.CurrentItem as CultureMap;
				if (item == null)
				{
					return Druid.Empty;
				}
				else
				{
					return item.Id;
				}
			}
		}

		private string CurrentName
		{
			get
			{
				CultureMap item = this.access.CollectionView.CurrentItem as CultureMap;
				if (item == null)
				{
					return null;
				}
				else
				{
					return item.Name;
				}
			}
		}

		private int GetSubView(object widget)
		{
			//	Retourne le rang d'une sous-vue correspondant à un widget.
			if (widget == this.buttonSubViewA)
			{
				return 0;
			}

			if (widget == this.buttonSubViewB)
			{
				return 1;
			}

			if (widget == this.buttonSubViewC)
			{
				return 2;
			}

			if (widget == this.buttonSubViewT)
			{
				return 3;
			}

			return -1;
		}


		private void HandleEditorSizeChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			//	Appelé lorsque la taille de la fenêtre de l'éditeur change.
			this.UpdateScroller();
			this.UpdateZoom();
		}

		private void HandleEditorAreaSizeChanged(object sender)
		{
			//	Appelé lorsque les dimensions de la zone de travail ont changé.
			this.AreaSize = this.editor.AreaSize;
			this.UpdateZoom();
		}

		private void HandleEditorAreaOffsetChanged(object sender)
		{
			//	Appelé lorsque l'offset de la zone de travail a changé.
			Point offset = this.editor.AreaOffset;

			if (this.hscroller.Enable)
			{
				offset.X = System.Math.Max(offset.X, (double) this.hscroller.MinValue/this.Zoom);
				offset.X = System.Math.Min(offset.X, (double) this.hscroller.MaxValue/this.Zoom);
			}
			else
			{
				offset.X = 0;
			}

			if (this.vscroller.Enable)
			{
				offset.Y = System.Math.Max(offset.Y, (double) this.vscroller.MinValue/this.Zoom);
				offset.Y = System.Math.Min(offset.Y, (double) this.vscroller.MaxValue/this.Zoom);
			}
			else
			{
				offset.Y = 0;
			}

			this.editor.AreaOffset = offset;

			this.hscroller.Value = (decimal) (offset.X*this.Zoom);
			this.vscroller.Value = (decimal) (offset.Y*this.Zoom);
		}

		private void HandleScrollerValueChanged(object sender)
		{
			//	Appelé lorsqu'un ascenseur a été bougé.
			double ox = 0;
			if (this.hscroller.IsEnabled)
			{
				ox = (double) this.hscroller.Value/this.Zoom;
			}

			double oy = 0;
			if (this.vscroller.IsEnabled)
			{
				oy = (double) this.vscroller.Value/this.Zoom;
			}

			this.editor.AreaOffset = new Point(ox, oy);
		}

		private void HandleButtonSubViewClicked(object sender, MessageEventArgs e)
		{
			//	Appelé lorsqu'un bouton de vue (A, B, C ou T) est cliqué.
			this.SubView = this.GetSubView(sender);
		}

		private void HandleButtonSubViewDragStarting(object sender)
		{
			//	Appelé lorsqu'un bouton de vue (A, B, C ou T) commencer à être draggé sur un autre.
			this.dragStarting = this.GetSubView(sender);
		}

		private void HandleButtonSubViewDragEnding(object sender)
		{
			//	Appelé lorsqu'un bouton de vue (A, B, C ou T) a été draggé sur un autre.
			int dragEnding = this.GetSubView(sender);
			if (this.dragStarting != -1 && dragEnding != -1)
			{
				this.DragSubView(this.dragStarting, dragEnding);
			}
		}

		private void HandleButtonZoomClicked(object sender, MessageEventArgs e)
		{
			//	Appelé lorsqu'un bouton de zoom prédéfini est cliqué.
			if (sender == this.buttonZoomPage)
			{
				Entities.isZoomPage = true;
				this.Zoom = 0;
			}

			if (sender == this.buttonZoomMin)
			{
				Entities.isZoomPage = false;
				this.Zoom = Entities.zoomMin;
			}

			if (sender == this.buttonZoomDefault)
			{
				Entities.isZoomPage = false;
				this.Zoom = Entities.zoomDefault;
			}
			
			if (sender == this.buttonZoomMax)
			{
				Entities.isZoomPage = false;
				this.Zoom = Entities.zoomMax;
			}
		}

		private void HandleFieldZoomClicked(object sender, MessageEventArgs e)
		{
			//	Appelé lorsque le champ du zoom a été cliqué.
			StatusField sf = sender as StatusField;
			if (sf == null)  return;
			VMenu menu = EntitiesEditor.ZoomMenu.CreateZoomMenu(Entities.zoomDefault, this.Zoom, this.ZoomPage, null);
			menu.Host = sf.Window;
			TextFieldCombo.AdjustComboSize(sf, menu, false);
			menu.ShowAsComboList(sf, Point.Zero, sf);
		}

		private void HandleSliderZoomValueChanged(object sender)
		{
			//	Appelé lorsque le slider du zoom a été bougé.
			if (this.ignoreChange)
			{
				return;
			}

			HSlider slider = sender as HSlider;
			Entities.isZoomPage = false;
			this.Zoom = (double) slider.Value;
		}

		private void HandleEditorZoomChanged(object sender)
		{
			//	Appelé lorsque le zoom a changé depuis l'éditeur.
			Entities.isZoomPage = false;
			this.Zoom = this.editor.Zoom;
		}

		private void HandleResetButtonClicked(object sender, MessageEventArgs e)
		{
			AbstractButton button = sender as AbstractButton;

			if (button == this.groupToolbar.ResetButton)
			{
				Support.ResourceAccessors.StructuredTypeResourceAccessor accessor = this.access.Accessor as Support.ResourceAccessors.StructuredTypeResourceAccessor;
				CultureMap item = this.access.CollectionView.CurrentItem as CultureMap;
				StructuredData data = item.GetCultureData(Resources.DefaultTwoLetterISOLanguageName);
				accessor.ResetToOriginalValue(item, data, Support.Res.Fields.ResourceStructuredType.Fields);
				accessor.ResetToOriginalValue(item, data, Support.Res.Fields.ResourceStructuredType.InterfaceIds);
				accessor.ResetToOriginalValue(item, data, Support.Res.Fields.ResourceStructuredType.SerializedDesignerLayouts);
				accessor.ResetToOriginalValue(item, data, Support.Res.Fields.ResourceStructuredType.Class);
				accessor.ResetToOriginalValue(item, data, Support.Res.Fields.ResourceStructuredType.BaseType);
				accessor.ResetToOriginalValue(item, data, Support.Res.Fields.ResourceStructuredType.DefaultLifetimeExpectancy);
				accessor.ResetToOriginalValue(item, data, Support.Res.Fields.ResourceStructuredType.Flags);
			}

			this.UpdateEdit();
			this.module.AccessEntities.SetLocalDirty();
		}

		private void HandleButtonGridClicked(object sender, MessageEventArgs e)
		{
			this.editor.Grid = !this.editor.Grid;
			this.UpdateGrid ();
		}

		private void HandleButtonSaveImageClicked(object sender, MessageEventArgs e)
		{
			string path;
			EntitiesEditor.BitmapParameters bitmapParameters;

			if (this.SaveImageDialog (out path, out bitmapParameters))
			{
				this.SaveImage (path, bitmapParameters);
			}
		}

		private void HandleButtonSaveAllImagesClicked(object sender, MessageEventArgs e)
		{
			//	Construit la liste de tous les noms d'entités.
			var allEntityNames = new List<string> ();
			foreach (CultureMap item in this.access.CollectionView.SourceCollection)
			{
				allEntityNames.Add (item.Name);
			}
			allEntityNames.Sort ();

			//	Reprend les paramètres sérialisés.
			var moduleName = this.module.ModuleId.Name;
			var data = this.designerApplication.Settings.GetSaveAllImagesData (moduleName);

			var selectedEntityNames = new List<string> ();
			string folder, extension;
			EntitiesEditor.BitmapParameters bitmapParameters;
			Entities.SetSaveAllImagesSerializeData (data, selectedEntityNames, out folder, out extension, out bitmapParameters);

			//	Demande les choix à l'utilisateur.
			var result = this.designerApplication.DlgSaveAllImages (allEntityNames, selectedEntityNames, ref folder, ref extension, ref bitmapParameters);

			if (result == System.Windows.Forms.DialogResult.OK)  // générer ?
			{
				this.SaveAllImages (selectedEntityNames, folder, extension, bitmapParameters);
			}

			if (result == System.Windows.Forms.DialogResult.OK ||  // générer ?
				result == System.Windows.Forms.DialogResult.Yes)   // appliquer ?
			{
				//	Sérialise les paramètres.
				data = Entities.GetSaveAllImagesSerializeData (selectedEntityNames, folder, extension, bitmapParameters);
				this.designerApplication.Settings.SetSaveAllImagesData (moduleName, data);
			}
		}

		private bool SaveImageDialog(out string path, out EntitiesEditor.BitmapParameters bitmapParameters)
		{
			var dialog = new Dialogs.FileSaveImageDialog (this.designerApplication);
			dialog.ImageSize = this.editor.AreaSize;
			dialog.ShowDialog ();

			if (dialog.Result != Common.Dialogs.DialogResult.Accept)
			{
				path = null;
				bitmapParameters = null;
				return false;
			}

			dialog.PathMemorize ();

			path = dialog.FileName;
			bitmapParameters = dialog.BitmapParameters;
			return true;
		}

		private void SaveAllImages(List<string> entityNames, string folder, string extension, EntitiesEditor.BitmapParameters bitmapParameters)
		{
			if (string.IsNullOrEmpty (folder))
			{
				return;
			}

			if (!System.IO.Directory.Exists (folder))
			{
				this.designerApplication.DialogMessage (string.Format ("Le dossier \"{0}\" n'existe pas.", folder));
				return;
			}

			int current = this.access.AccessIndex;

			for (int i = 0; i < this.access.CollectionView.Count; i++)
			{
				this.access.AccessIndex = i;
				CultureMap item = this.access.CollectionView.CurrentItem as CultureMap;

				if (entityNames.Contains (item.Name))
				{
					this.Deserialize ();

					string path = System.IO.Path.Combine (folder, string.Concat (item.Name, extension));
					this.SaveImage (path, bitmapParameters);
				}
			}

			this.access.AccessIndex = current;
			this.UpdateEdit ();
		}

		private void SaveImage(string path, EntitiesEditor.BitmapParameters bitmapParameters)
		{
			Graphics graphics = new Graphics ();

			var zoom = bitmapParameters.Zoom;
			var generateCartridge = bitmapParameters.GenerateUserCartridge || bitmapParameters.GenerateDateCartridge || bitmapParameters.GenerateSamplesCartridge;

			var cartridgeSize = this.editor.CartridgeSize (bitmapParameters);
			if (cartridgeSize.IsEmpty)
			{
				generateCartridge = false;
			}

			int cartridgeWidth  = (int) (generateCartridge ? cartridgeSize.Width  : 0);
			int cartridgeHeight = (int) (generateCartridge ? cartridgeSize.Height : 0);

			int dx = (int) this.editor.AreaSize.Width;
			int dy = (int) this.editor.AreaSize.Height;

			if (generateCartridge)
			{
				dx = System.Math.Max (dx, cartridgeWidth+1);
				dy += cartridgeHeight;
			}

			graphics.AllocatePixmap ();
			graphics.SetPixmapSize ((int) (dx*zoom), (int) (dy*zoom));
			graphics.Transform = graphics.Transform.MultiplyBy (Transform.CreateTranslationTransform (0, -dy));
			graphics.Transform = graphics.Transform.MultiplyBy (Transform.CreateScaleTransform (zoom, -zoom));

			graphics.Transform = graphics.Transform.MultiplyBy (Transform.CreateTranslationTransform (0, -cartridgeHeight*zoom));
			this.editor.PaintObjects (graphics);
			graphics.Transform = graphics.Transform.MultiplyBy (Transform.CreateTranslationTransform (0, cartridgeHeight*zoom));

			if (generateCartridge)
			{
				this.editor.PaintCartridge (graphics, bitmapParameters);
			}

			var bitmap = Bitmap.FromPixmap (graphics.Pixmap) as Bitmap;
			byte[] data = null;

			switch (System.IO.Path.GetExtension (path).ToLowerInvariant ())
			{
				case ".png":
					data = bitmap.Save (ImageFormat.Png, 24);
					break;

				case ".tif":
					data = bitmap.Save (ImageFormat.Tiff, 24, 100, ImageCompression.Lzw);
					break;

				case ".bmp":
					data = bitmap.Save (ImageFormat.Bmp, 24);
					break;

				case ".jpg":
					data = bitmap.Save (ImageFormat.Jpeg, 24, 70, ImageCompression.None);
					break;
			}

			if (data != null)
			{
				try
				{
					System.IO.File.Delete (path);
				}
				catch
				{
				}

				try
				{
					using (System.IO.FileStream stream = new System.IO.FileStream (path, System.IO.FileMode.CreateNew))
					{
						stream.Write (data, 0, data.Length);
					}
				}
				catch
				{
				}
			}

			graphics.Dispose ();
		}

		private static string GetSaveAllImagesSerializeData(List<string> selectedEntityNames, string folder, string extension, EntitiesEditor.BitmapParameters bitmapParameters)
		{
			var list = new List<string> ();

			list.Add (string.Join ("◦", selectedEntityNames));
			list.Add (folder);
			list.Add (extension);
			list.Add (bitmapParameters.GetSerializeData ());

			return string.Join ("◊", list);
		}

		private static void SetSaveAllImagesSerializeData(string data, List<string> selectedEntityNames, out string folder, out string extension, out EntitiesEditor.BitmapParameters bitmapParameters)
		{
			selectedEntityNames.Clear ();
			folder = null;
			extension = null;
			bitmapParameters = new EntitiesEditor.BitmapParameters ();

			if (!string.IsNullOrEmpty (data))
			{
				var parts = data.Split ('◊');

				if (parts.Length > 0)
				{
					var names = parts[0].Split ('◦');
					foreach (var name in names)
					{
						if (!string.IsNullOrEmpty (name))
						{
							selectedEntityNames.Add (name);
						}
					}
				}

				if (parts.Length > 1)
				{
					folder = parts[1];
				}

				if (parts.Length > 2)
				{
					extension = parts[2];
				}

				if (parts.Length > 3)
				{
					bitmapParameters.SetSerializeData (parts[3]);
				}
			}

			if (string.IsNullOrEmpty (extension))
			{
				extension = ".png";
			}
		}


		public static readonly double zoomMin = 0.2;
		public static readonly double zoomMax = 2.0;
		private static readonly double zoomDefault = 1.0;

		private static int subView = 0;
		private static double zoom = Entities.zoomDefault;
		private static bool isZoomPage = false;
		private static string softSerialize = null;
		private static bool softDirtySerialization = false;

		private HSplitter hsplitter;
		private EntitiesEditor.Editor editor;
		private VScroller vscroller;
		private HScroller hscroller;
		private Size areaSize;
		private MyWidgets.ResetBox groupToolbar;
		private HToolBar toolbar;
		private MyWidgets.EntitySubView buttonSubViewA;
		private MyWidgets.EntitySubView buttonSubViewB;
		private MyWidgets.EntitySubView buttonSubViewC;
		private MyWidgets.EntitySubView buttonSubViewT;
		private IconButton buttonZoomPage;
		private IconButton buttonZoomMin;
		private IconButton buttonZoomDefault;
		private IconButton buttonZoomMax;
		private StatusField fieldZoom;
		private HSlider sliderZoom;
		private IconButton buttonGrid;
		private IconButton buttonSaveImage;
		private IconButton buttonSaveAllImages;
		private int dragStarting;
		private Druid druidToSerialize;
		private string nameToSerialize;
	}
}
