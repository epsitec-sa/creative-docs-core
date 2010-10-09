//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;

using Epsitec.Cresus.Core.Entities;

using Epsitec.Cresus.WorkflowDesigner.Widgets;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.WorkflowDesigner
{
	public class MainController
	{
		public MainController(Core.Business.BusinessContext businessContext, WorkflowDefinitionEntity workflowEntity)
		{
			this.businessContext = businessContext;
			this.workflowEntity = workflowEntity;
		}

		public void CreateUI(Widget parent)
		{
			FrameBox editorGroup = new FrameBox(parent);
			editorGroup.Dock = DockStyle.Fill;

			//	Crée les grands blocs de widgets.
			FrameBox band = new FrameBox(editorGroup);
			band.Dock = DockStyle.Fill;

			this.editor = new Editor(band);
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

			this.groupToolbar = new Widgets.ResetBox(editorGroup);
			this.groupToolbar.IsPatch = false;
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
			this.buttonZoomPage = new IconButton(this.toolbar);
			this.buttonZoomPage.IconUri = Misc.Icon("ZoomPage");
			this.buttonZoomPage.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonZoomPage.AutoFocus = false;
			this.buttonZoomPage.Dock = DockStyle.Left;
			this.buttonZoomPage.Clicked += this.HandleButtonZoomClicked;
			//?ToolTip.Default.SetToolTip(this.buttonZoomPage, Res.Strings.Entities.Action.ZoomPage);

			this.buttonZoomMin = new IconButton(this.toolbar);
			this.buttonZoomMin.IconUri = Misc.Icon("ZoomMin");
			this.buttonZoomMin.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonZoomMin.AutoFocus = false;
			this.buttonZoomMin.Dock = DockStyle.Left;
			this.buttonZoomMin.Clicked += this.HandleButtonZoomClicked;
			//?ToolTip.Default.SetToolTip(this.buttonZoomMin, Res.Strings.Entities.Action.ZoomMin);

			this.buttonZoomDefault = new IconButton(this.toolbar);
			this.buttonZoomDefault.IconUri = Misc.Icon("ZoomDefault");
			this.buttonZoomDefault.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonZoomDefault.AutoFocus = false;
			this.buttonZoomDefault.Dock = DockStyle.Left;
			this.buttonZoomDefault.Clicked += this.HandleButtonZoomClicked;
			//?ToolTip.Default.SetToolTip(this.buttonZoomDefault, Res.Strings.Entities.Action.ZoomDefault);

			this.buttonZoomMax = new IconButton(this.toolbar);
			this.buttonZoomMax.IconUri = Misc.Icon("ZoomMax");
			this.buttonZoomMax.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonZoomMax.AutoFocus = false;
			this.buttonZoomMax.Dock = DockStyle.Left;
			this.buttonZoomMax.Clicked += this.HandleButtonZoomClicked;
			//?ToolTip.Default.SetToolTip(this.buttonZoomMax, Res.Strings.Entities.Action.ZoomMax);

			this.fieldZoom = new StatusField(this.toolbar);
			this.fieldZoom.PreferredWidth = 50;
			this.fieldZoom.Margins = new Margins(5, 5, 1, 1);
			this.fieldZoom.Dock = DockStyle.Left;
			this.fieldZoom.Clicked += this.HandleFieldZoomClicked;
			//?ToolTip.Default.SetToolTip(this.fieldZoom, Res.Strings.Entities.Action.ZoomMenu);

			this.sliderZoom = new HSlider(this.toolbar);
			this.sliderZoom.MinValue = (decimal) MainController.zoomMin;
			this.sliderZoom.MaxValue = (decimal) MainController.zoomMax;
			this.sliderZoom.SmallChange = (decimal) 0.1;
			this.sliderZoom.LargeChange = (decimal) 0.2;
			this.sliderZoom.Resolution = (decimal) 0.01;
			this.sliderZoom.PreferredWidth = 90;
			this.sliderZoom.Margins = new Margins(0, 0, 4, 4);
			this.sliderZoom.Dock = DockStyle.Left;
			this.sliderZoom.ValueChanged += this.HandleSliderZoomValueChanged;
			//?ToolTip.Default.SetToolTip(this.sliderZoom, Res.Strings.Entities.Action.ZoomSlider);

			this.buttonGrid = new IconButton (this.toolbar);
			this.buttonGrid.IconUri = Misc.Icon ("Grid");
			this.buttonGrid.ButtonStyle = ButtonStyle.ActivableIcon;
			this.buttonGrid.AutoFocus = false;
			this.buttonGrid.Margins = new Margins (10, 0, 0, 0);
			this.buttonGrid.Dock = DockStyle.Left;
			this.buttonGrid.Clicked += this.HandleButtonGridClicked;
			//?ToolTip.Default.SetToolTip (this.buttonGrid, "Grille magnétique");

			this.buttonSaveImage = new IconButton (this.toolbar);
			this.buttonSaveImage.IconUri = Misc.Icon ("Save");
			this.buttonSaveImage.AutoFocus = false;
			this.buttonSaveImage.Dock = DockStyle.Right;
			this.buttonSaveImage.Clicked += this.HandleButtonSaveImageClicked;
			//?ToolTip.Default.SetToolTip (this.buttonSaveImage, Res.Strings.Entities.Action.SaveBitmap);

			this.AreaSize = new Size(100, 100);

			this.editor.SetBusinessContext (this.businessContext);
			this.editor.SetWorkflowDefinitionEntity (this.workflowEntity);

			this.editor.UpdateGeometry();
			this.UpdateEdit ();
			this.UpdateZoom ();
		}


		protected Size AreaSize
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

		public double Zoom
		{
			//	Zoom pour représenter les boîtes et les liaisons.
			get
			{
				if (MainController.isZoomPage)
				{
					return this.ZoomPage;
				}
				else
				{
					return MainController.zoom;
				}
			}
			set
			{
				if (MainController.zoom != value)
				{
					MainController.zoom = value;

					this.UpdateZoom();
					this.UpdateScroller();
				}
			}
		}

		protected double ZoomPage
		{
			//	Retourne le zoom permettant de voir toute la surface de travail.
			get
			{
				double zx = this.editor.Client.Bounds.Width  / this.editor.AreaSize.Width;
				double zy = this.editor.Client.Bounds.Height / this.editor.AreaSize.Height;
				double zoom = System.Math.Min (zx, zy);

				zoom = System.Math.Max(zoom, MainController.zoomMin);
				zoom = System.Math.Min(zoom, MainController.zoomMax);
				
				zoom = System.Math.Floor(zoom*100)/100;  // 45.8% -> 46%
				return zoom;
			}
		}

		protected void UpdateZoom()
		{
			//	Met à jour tout ce qui dépend du zoom.
			this.editor.Zoom = this.Zoom;

			this.fieldZoom.Text = string.Concat(System.Math.Floor(this.Zoom*100).ToString(), "%");

			this.ignoreChange = true;
			this.sliderZoom.Value = (decimal) this.Zoom;
			this.ignoreChange = false;

			this.buttonZoomPage.ActiveState    = (MainController.isZoomPage              ) ? ActiveState.Yes : ActiveState.No;
			this.buttonZoomMin.ActiveState     = (this.Zoom == MainController.zoomMin    ) ? ActiveState.Yes : ActiveState.No;
			this.buttonZoomDefault.ActiveState = (this.Zoom == MainController.zoomDefault) ? ActiveState.Yes : ActiveState.No;
			this.buttonZoomMax.ActiveState     = (this.Zoom == MainController.zoomMax    ) ? ActiveState.Yes : ActiveState.No;
		}

		protected void UpdateScroller()
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

		protected void UpdateGrid()
		{
			this.buttonGrid.ActiveState = this.editor.Grid ? ActiveState.Yes : ActiveState.No;
		}


		protected void UpdateEdit()
		{
			//	Met à jour les lignes éditables en fonction de la sélection dans le tableau.
			this.editor.Clear();

			if (!this.Deserialize())
			{
				this.editor.CreateInitialWorkflow ();
			}

			this.Zoom = this.Zoom;
		}

		protected void Serialize()
		{
#if false
			//	Sérialise les données.
			if (this.druidToSerialize.IsValid)
			{
				string data = this.editor.Serialize();
				MainController.SetSerializedData(this.access.Accessor, this.druidToSerialize, this.SubView, data);
			}
#endif
		}

		protected bool Deserialize()
		{
#if false
			//	Désérialise les données sérialisées. Retourne false s'il n'existe aucune donnée sérialisée.
			this.nameToSerialize = this.CurrentName;
			this.druidToSerialize = this.CurrentDruid;

			if (MainController.softSerialize == null)
			{
				string data = MainController.GetSerializedData(this.access.Accessor, this.druidToSerialize, this.SubView);
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

				this.editor.Deserialize(MainController.softSerialize);

				MainController.softDirtySerialization = false;
				MainController.softSerialize = null;
				return true;
			}
#endif
			return false;
		}

#if false
		private static void SetSerializedData(IResourceAccessor accessor, Druid druid, int subView, string data)
		{
			//	Sérialise des données. data vaut null s'il faut effacer les données sérialisées.
			CultureMap resource = accessor.Collection[druid];
			
			if (resource != null)
			{
				StructuredData record = resource.GetCultureData(Resources.DefaultTwoLetterISOLanguageName);
				string key = subView.ToString(System.Globalization.CultureInfo.InvariantCulture);
				Dictionary<string, string> dict = MainController.GetSerializedLayouts(record);
				
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

				MainController.SetSerializedLayouts(record, dict);
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
				Dictionary<string, string> dict = MainController.GetSerializedLayouts(record);
				
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
#endif


#if false
		protected Druid CurrentDruid
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

		protected string CurrentName
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
#endif


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

		private void HandleButtonZoomClicked(object sender, MessageEventArgs e)
		{
			//	Appelé lorsqu'un bouton de zoom prédéfini est cliqué.
			if (sender == this.buttonZoomPage)
			{
				MainController.isZoomPage = true;
				this.Zoom = 0;
			}

			if (sender == this.buttonZoomMin)
			{
				MainController.isZoomPage = false;
				this.Zoom = MainController.zoomMin;
			}

			if (sender == this.buttonZoomDefault)
			{
				MainController.isZoomPage = false;
				this.Zoom = MainController.zoomDefault;
			}
			
			if (sender == this.buttonZoomMax)
			{
				MainController.isZoomPage = false;
				this.Zoom = MainController.zoomMax;
			}
		}

		private void HandleFieldZoomClicked(object sender, MessageEventArgs e)
		{
			//	Appelé lorsque le champ du zoom a été cliqué.
			StatusField sf = sender as StatusField;
			if (sf == null)  return;
			VMenu menu = ZoomMenu.CreateZoomMenu(MainController.zoomDefault, this.Zoom, this.ZoomPage, null);
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
			MainController.isZoomPage = false;
			this.Zoom = (double) slider.Value;
		}

		private void HandleEditorZoomChanged(object sender)
		{
			//	Appelé lorsque le zoom a changé depuis l'éditeur.
			MainController.isZoomPage = false;
			this.Zoom = this.editor.Zoom;
		}

		private void HandleResetButtonClicked(object sender, MessageEventArgs e)
		{
#if false
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
			}

			this.UpdateEdit();
			this.module.AccessEntities.SetLocalDirty();
#endif
		}

		private void HandleButtonGridClicked(object sender, MessageEventArgs e)
		{
			this.editor.Grid = !this.editor.Grid;
			this.UpdateGrid ();
		}

		private void HandleButtonSaveImageClicked(object sender, MessageEventArgs e)
		{
			string path;
			double zoom;

			if (this.SaveImageDialog (out path, out zoom))
			{
				this.SaveImage (path, zoom);
			}
		}

		private bool SaveImageDialog(out string path, out double zoom)
		{
#if false
			var dialog = new Dialogs.FileSaveImageDialog (this.designerApplication);
			dialog.ImageSize = this.editor.AreaSize;
			dialog.ShowDialog ();

			if (dialog.Result != Common.Dialogs.DialogResult.Accept)
			{
				path = null;
				zoom = 0;
				return false;
			}

			dialog.PathMemorize ();

			path = dialog.FileName;
			zoom = dialog.Zoom;
			return true;
#else
			path = null;
			zoom = 0;
			return false;
#endif
		}

		private void SaveImage(string path, double zoom)
		{
			Graphics graphics = new Graphics ();

			int dx = (int) this.editor.AreaSize.Width;
			int dy = (int) this.editor.AreaSize.Height;

			graphics.AllocatePixmap ();
			graphics.SetPixmapSize ((int) (dx*zoom), (int) (dy*zoom));
			graphics.Transform = graphics.Transform.MultiplyBy (Transform.CreateTranslationTransform (0, -dy));
			graphics.Transform = graphics.Transform.MultiplyBy (Transform.CreateScaleTransform (zoom, -zoom));

			this.editor.PaintObjects (graphics);

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
				using (System.IO.FileStream stream = new System.IO.FileStream (path, System.IO.FileMode.OpenOrCreate))
				{
					stream.Write (data, 0, data.Length);
				}
			}

			graphics.Dispose ();
		}


		public static readonly double					zoomMin = 0.2;
		public static readonly double					zoomMax = 2.0;
		private static readonly double					zoomDefault = 1.0;

		private readonly Core.Business.BusinessContext	businessContext;
		private readonly WorkflowDefinitionEntity		workflowEntity;
		
		private static double							zoom = MainController.zoomDefault;
		private static bool								isZoomPage = false;
		private static string							softSerialize = null;
		private static bool								softDirtySerialization = false;

		private Editor									editor;
		private VScroller								vscroller;
		private HScroller								hscroller;
		private Size									areaSize;
		private Widgets.ResetBox						groupToolbar;
		private HToolBar								toolbar;
		private IconButton								buttonZoomPage;
		private IconButton								buttonZoomMin;
		private IconButton								buttonZoomDefault;
		private IconButton								buttonZoomMax;
		private StatusField								fieldZoom;
		private HSlider									sliderZoom;
		private IconButton								buttonGrid;
		private IconButton								buttonSaveImage;
		private string									nameToSerialize;
		private bool									ignoreChange;
	}
}
