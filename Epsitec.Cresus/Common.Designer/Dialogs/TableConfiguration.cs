using System.Collections.Generic;
using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;
using Epsitec.Common.Types;

namespace Epsitec.Common.Designer.Dialogs
{
	/// <summary>
	/// Dialogue permettant de configurer les rubriques d'une table.
	/// </summary>
	public class TableConfiguration : Abstract
	{
		public TableConfiguration(MainWindow mainWindow) : base(mainWindow)
		{
		}

		public override void Show()
		{
			//	Cr�e et montre la fen�tre du dialogue.
			if ( this.window == null )
			{
				this.window = new Window();
				this.window.MakeSecondaryWindow();
				this.window.PreventAutoClose = true;
				this.WindowInit("TableConfiguration", 500, 400, true);
				this.window.Text = "Choix des rubriques";  // Res.Strings.Dialog.StructuredSelector.Title;
				this.window.Owner = this.parentWindow;
				this.window.WindowCloseClicked += new EventHandler(this.HandleWindowCloseClicked);
				this.window.Root.Padding = new Margins(8, 8, 8, 8);

				ResizeKnob resize = new ResizeKnob(this.window.Root);
				resize.Anchor = AnchorStyles.BottomRight;
				resize.Margins = new Margins(0, -8, 0, -8);
				ToolTip.Default.SetToolTip(resize, Res.Strings.Dialog.Tooltip.Resize);

				int tabIndex = 0;

				//	Cr�e la barre d'outils.
				this.toolbar = new HToolBar(this.window.Root);
				this.toolbar.Dock = DockStyle.Top;

				this.buttonAdd = new IconButton();
				this.buttonAdd.CaptionId = Res.Captions.Dialog.TableConfiguration.Add.Id;
				this.buttonAdd.Clicked += new MessageEventHandler(this.HandleButtonClicked);
				this.toolbar.Items.Add(this.buttonAdd);

				this.buttonRemove = new IconButton();
				this.buttonRemove.CaptionId = Res.Captions.Dialog.TableConfiguration.Remove.Id;
				this.buttonRemove.Clicked += new MessageEventHandler(this.HandleButtonClicked);
				this.toolbar.Items.Add(this.buttonRemove);

				this.buttonTemplate = new IconButton();
				this.buttonTemplate.CaptionId = Res.Captions.Dialog.TableConfiguration.Template.Id;
				this.buttonTemplate.Clicked += new MessageEventHandler(this.HandleButtonClicked);
				this.toolbar.Items.Add(this.buttonTemplate);

				this.toolbar.Items.Add(new IconSeparator());

				this.buttonPrev = new IconButton();
				this.buttonPrev.CaptionId = Res.Captions.Dialog.TableConfiguration.Prev.Id;
				this.buttonPrev.Clicked += new MessageEventHandler(this.HandleButtonClicked);
				this.toolbar.Items.Add(this.buttonPrev);

				this.buttonNext = new IconButton();
				this.buttonNext.CaptionId = Res.Captions.Dialog.TableConfiguration.Next.Id;
				this.buttonNext.Clicked += new MessageEventHandler(this.HandleButtonClicked);
				this.toolbar.Items.Add(this.buttonNext);

				this.toolbar.Items.Add(new IconSeparator());

				this.buttonSort = new IconButton();
				this.buttonSort.CaptionId = Res.Captions.Dialog.TableConfiguration.Sort.Id;
				this.buttonSort.Clicked += new MessageEventHandler(this.HandleButtonClicked);
				this.toolbar.Items.Add(this.buttonSort);

				this.slider = new HSlider(toolbar);
				this.slider.PreferredWidth = 80;
				this.slider.Margins = new Margins(2, 2, 4, 4);
				this.slider.MinValue = 20.0M;
				this.slider.MaxValue = 50.0M;
				this.slider.SmallChange = 5.0M;
				this.slider.LargeChange = 10.0M;
				this.slider.Resolution = 1.0M;
				this.slider.ValueChanged += new EventHandler(this.HandleSliderChanged);
				this.slider.Value = (decimal) TableConfiguration.arrayLineHeight;
				this.slider.Dock = DockStyle.Right;

				//	Cr�e l'en-t�te du tableau.
				this.header = new Widget(this.window.Root);
				this.header.Dock = DockStyle.Top;
				this.header.Margins = new Margins(0, 0, 4, 0);

				this.headerUse = new HeaderButton(this.header);
				this.headerUse.Text = "";
				this.headerUse.Style = HeaderButtonStyle.Top;
				this.headerUse.Dock = DockStyle.Left;

				this.headerName = new HeaderButton(this.header);
				this.headerName.Text = Res.Strings.Dialog.TableDescription.Name;
				this.headerName.Style = HeaderButtonStyle.Top;
				this.headerName.Dock = DockStyle.Left;

				this.headerCaption = new HeaderButton(this.header);
				this.headerCaption.Text = Res.Strings.Dialog.TableDescription.Caption;
				this.headerCaption.Style = HeaderButtonStyle.Top;
				this.headerCaption.Dock = DockStyle.Left;

				//	Cr�e le tableau principal.
				this.array = new MyWidgets.StringArray(this.window.Root);
				this.array.Columns = 3;
				this.array.SetColumnsRelativeWidth(0, 0.07);
				this.array.SetColumnsRelativeWidth(1, 0.50);
				this.array.SetColumnsRelativeWidth(2, 0.50);
				this.array.SetColumnAlignment(0, ContentAlignment.MiddleCenter);
				this.array.SetColumnAlignment(1, ContentAlignment.MiddleLeft);
				this.array.SetColumnAlignment(2, ContentAlignment.MiddleLeft);
				this.array.SetColumnBreakMode(1, TextBreakMode.Ellipsis | TextBreakMode.Split | TextBreakMode.SingleLine);
				this.array.SetColumnBreakMode(2, TextBreakMode.Ellipsis | TextBreakMode.Split | TextBreakMode.SingleLine);
				this.array.LineHeight = TableConfiguration.arrayLineHeight;
				this.array.Dock = DockStyle.Fill;
				this.array.ColumnsWidthChanged += new EventHandler(this.HandleArrayColumnsWidthChanged);
				this.array.CellCountChanged += new EventHandler(this.HandleArrayCellCountChanged);
				this.array.CellsContentChanged += new EventHandler(this.HandleArrayCellsContentChanged);
				this.array.SelectedRowChanged += new EventHandler(this.HandleArraySelectedRowChanged);

				//	Boutons de fermeture.
				Widget footer = new Widget(this.window.Root);
				footer.PreferredHeight = 22;
				footer.Margins = new Margins(0, 0, 8, 0);
				footer.Dock = DockStyle.Bottom;

				this.buttonOk = new Button(footer);
				this.buttonOk.PreferredWidth = 75;
				this.buttonOk.Text = Res.Strings.Dialog.Button.OK;
				this.buttonOk.Dock = DockStyle.Left;
				this.buttonOk.Margins = new Margins(0, 6, 0, 0);
				this.buttonOk.Clicked += new MessageEventHandler(this.HandleButtonOkClicked);
				this.buttonOk.TabIndex = tabIndex++;
				this.buttonOk.TabNavigationMode = TabNavigationMode.ActivateOnTab;

				this.buttonCancel = new Button(footer);
				this.buttonCancel.PreferredWidth = 75;
				this.buttonCancel.Text = Res.Strings.Dialog.Button.Cancel;
				this.buttonCancel.ButtonStyle = ButtonStyle.DefaultCancel;
				this.buttonCancel.Dock = DockStyle.Left;
				this.buttonCancel.Clicked += new MessageEventHandler(this.HandleButtonCloseClicked);
				this.buttonCancel.TabIndex = tabIndex++;
				this.buttonCancel.TabNavigationMode = TabNavigationMode.ActivateOnTab;
			}

			this.UpdateButtons();
			this.UpdateArray();

			this.window.ShowDialog();
		}

		public void Initialise(Module module, List<UI.ItemTableColumn> columns)
		{
			//	Initialise le dialogue avec l'objet table.
			this.module = module;
			this.resourceAccess = module.AccessCaptions;

			this.columns = new List<UI.ItemTableColumn> ();
			foreach (UI.ItemTableColumn column in columns)
			{
				this.columns.Add(column);
			}

			this.columnsReturned = null;
		}

		public List<UI.ItemTableColumn> Columns
		{
			get
			{
				return this.columnsReturned;
			}
		}


		protected void UpdateButtons()
		{
			//	Met � jour tous les boutons en fonction de la ligne s�lectionn�e dans le tableau.
			int sel = this.array.SelectedRow;
		}

		protected void UpdateArray()
		{
			//	Met � jour tout le contenu du tableau.
			this.array.TotalRows = this.columns.Count;

			int first = this.array.FirstVisibleRow;
			for (int i=0; i<this.array.LineCount; i++)
			{
				if (first+i < this.columns.Count)
				{
					UI.ItemTableColumn column = this.columns[first+i];
					string name = column.FieldId;

					bool active = true;
					string icon = active ? Misc.Image("TypeEnumYes") : "";
					MyWidgets.StringList.CellState cs = active ? MyWidgets.StringList.CellState.Normal : MyWidgets.StringList.CellState.Unused;

					this.array.SetLineString(0, first+i, icon);
					this.array.SetLineState(0, first+i, cs);

					this.array.SetLineString(1, first+i, name);
					this.array.SetLineState(1, first+i, MyWidgets.StringList.CellState.Normal);

					this.array.SetLineString(2, first+i, "");
					this.array.SetLineState(2, first+i, MyWidgets.StringList.CellState.Normal);
				}
				else
				{
					this.array.SetLineString(0, first+i, "");
					this.array.SetLineState(0, first+i, MyWidgets.StringList.CellState.Disabled);

					this.array.SetLineString(1, first+i, "");
					this.array.SetLineState(1, first+i, MyWidgets.StringList.CellState.Disabled);

					this.array.SetLineString(2, first+i, "");
					this.array.SetLineState(2, first+i, MyWidgets.StringList.CellState.Disabled);
				}
			}
		}

		protected void UpdateColumnsWidth()
		{
			//	Place les widgets en dessus et en dessous du tableau en fonction des
			//	largeurs des colonnes.
			double w1 = this.array.GetColumnsAbsoluteWidth(0);
			double w2 = this.array.GetColumnsAbsoluteWidth(1);
			double w3 = this.array.GetColumnsAbsoluteWidth(2);

			this.headerUse.PreferredWidth = w1;
			this.headerName.PreferredWidth = w2;
			this.headerCaption.PreferredWidth = w3+1;
		}


		protected void ArrayAdd()
		{
			//	Ajoute une nouvelle rubrique dans la table.
		}

		protected void ArrayRemove()
		{
			//	Supprime une rubrique de la table.
		}

		protected void ArrayTemplate()
		{
			//	Ajoute un template dans la table.
		}

		protected void ArrayMove(int direction)
		{
			//	D�place une rubrique dans la table.
			int sel = this.array.SelectedRow;
			if (sel == -1)
			{
				return;
			}

			UI.ItemTableColumn column = this.columns[sel];
			this.columns.RemoveAt(sel);
			this.columns.Insert(sel+direction, column);

			this.array.SelectedRow = sel+direction;
			this.array.ShowSelectedRow();

			this.UpdateButtons();
			this.UpdateArray();
		}

		protected void ArraySort()
		{
			//	Met les rubrique de la table en t�te de liste.
		}


		private void HandleButtonClicked(object sender, MessageEventArgs e)
		{
			if (sender == this.buttonAdd)
			{
				this.ArrayAdd();
			}

			if (sender == this.buttonRemove)
			{
				this.ArrayRemove();
			}

			if (sender == this.buttonTemplate)
			{
				this.ArrayTemplate();
			}

			if (sender == this.buttonPrev)
			{
				this.ArrayMove(-1);
			}

			if (sender == this.buttonNext)
			{
				this.ArrayMove(1);
			}

			if (sender == this.buttonSort)
			{
				this.ArraySort();
			}
		}

		private void HandleSliderChanged(object sender)
		{
			//	Appel� lorsque le slider a �t� d�plac�.
			if (this.array == null)
			{
				return;
			}

			HSlider slider = sender as HSlider;
			TableConfiguration.arrayLineHeight = (double) slider.Value;
			this.array.LineHeight = TableConfiguration.arrayLineHeight;
		}

		private void HandleArrayColumnsWidthChanged(object sender)
		{
			//	La largeur des colonnes a chang�.
			this.UpdateColumnsWidth();
		}

		private void HandleArrayCellCountChanged(object sender)
		{
			//	Le nombre de lignes a chang�.
			this.UpdateArray();
		}

		private void HandleArrayCellsContentChanged(object sender)
		{
			//	Le contenu des cellules a chang�.
			this.UpdateArray();
		}

		private void HandleArraySelectedRowChanged(object sender)
		{
			//	La ligne s�lectionn�e a chang�.
			this.UpdateButtons();

			if (this.array.SelectedColumn == 0)
			{
				if (this.buttonAdd.Enable)
				{
					this.ArrayAdd();
				}
				else
				{
					this.ArrayRemove();
				}
			}
		}

		private void HandleWindowCloseClicked(object sender)
		{
			this.parentWindow.MakeActive();
			this.window.Hide();
			this.OnClosed();
		}

		private void HandleButtonCloseClicked(object sender, MessageEventArgs e)
		{
			this.parentWindow.MakeActive();
			this.window.Hide();
			this.OnClosed();
		}

		private void HandleButtonOkClicked(object sender, MessageEventArgs e)
		{
			this.parentWindow.MakeActive();
			this.window.Hide();
			this.OnClosed();

			this.columnsReturned = this.columns;
		}


		protected static double					arrayLineHeight = 20;

		protected Module						module;
		protected ResourceAccess				resourceAccess;
		protected List<UI.ItemTableColumn>		columns;
		protected List<UI.ItemTableColumn>		columnsReturned;

		protected HToolBar						toolbar;
		protected IconButton					buttonAdd;
		protected IconButton					buttonRemove;
		protected IconButton					buttonTemplate;
		protected IconButton					buttonPrev;
		protected IconButton					buttonNext;
		protected IconButton					buttonSort;
		protected HSlider						slider;

		protected Widget						header;
		protected HeaderButton					headerUse;
		protected HeaderButton					headerName;
		protected HeaderButton					headerCaption;
		protected MyWidgets.StringArray			array;

		protected Button						buttonOk;
		protected Button						buttonCancel;
	}
}
