using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Document.Containers
{
	/// <summary>
	/// La classe Containers.Pages contient tous les panneaux des pages.
	/// </summary>
	[SuppressBundleSupport]
	public class Pages : Abstract
	{
		public Pages(Document document) : base(document)
		{
			this.toolBar = new HToolBar(this);
			this.toolBar.Dock = DockStyle.Top;
			this.toolBar.DockMargins = new Margins(0, 0, 0, -1);
			this.toolBar.TabIndex = 1;
			this.toolBar.TabNavigation = Widget.TabNavigationMode.ForwardTabPassive;

			int index = 0;

			this.buttonNew = new IconButton("PageNew", Misc.Icon("PageNew"));
			this.toolBar.Items.Add(this.buttonNew);
			this.buttonNew.TabIndex = index++;
			this.buttonNew.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			ToolTip.Default.SetToolTip(this.buttonNew, Res.Strings.Action.PageNewLong);
			this.Synchro(this.buttonNew);

			this.buttonDuplicate = new IconButton("PageDuplicate", Misc.Icon("DuplicateItem"));
			this.toolBar.Items.Add(this.buttonDuplicate);
			this.buttonDuplicate.TabIndex = index++;
			this.buttonDuplicate.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			ToolTip.Default.SetToolTip(this.buttonDuplicate, Res.Strings.Action.PageDuplicate);
			this.Synchro(this.buttonDuplicate);

			this.toolBar.Items.Add(new IconSeparator());

			this.buttonUp = new IconButton("PageUp", Misc.Icon("Up"));
			this.toolBar.Items.Add(this.buttonUp);
			this.buttonUp.TabIndex = index++;
			this.buttonUp.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			ToolTip.Default.SetToolTip(this.buttonUp, Res.Strings.Action.PageUp);
			this.Synchro(this.buttonUp);

			this.buttonDown = new IconButton("PageDown", Misc.Icon("Down"));
			this.toolBar.Items.Add(this.buttonDown);
			this.buttonDown.TabIndex = index++;
			this.buttonDown.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			ToolTip.Default.SetToolTip(this.buttonDown, Res.Strings.Action.PageDown);
			this.Synchro(this.buttonDown);

			this.toolBar.Items.Add(new IconSeparator());

			this.buttonDelete = new IconButton("PageDelete", Misc.Icon("DeleteItem"));
			this.toolBar.Items.Add(this.buttonDelete);
			this.buttonDelete.TabIndex = index++;
			this.buttonDelete.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			ToolTip.Default.SetToolTip(this.buttonDelete, Res.Strings.Action.PageDelete);
			this.Synchro(this.buttonDelete);

			this.table = new CellTable(this);
			this.table.Dock = DockStyle.Fill;
			this.table.SelectionChanged += new EventHandler(this.HandleTableSelectionChanged);
			this.table.DoubleClicked += new MessageEventHandler(this.HandleTableDoubleClicked);
			this.table.StyleH  = CellArrayStyle.ScrollNorm;
			this.table.StyleH |= CellArrayStyle.Header;
			this.table.StyleH |= CellArrayStyle.Separator;
			this.table.StyleH |= CellArrayStyle.Mobile;
			this.table.StyleV  = CellArrayStyle.ScrollNorm;
			this.table.StyleV |= CellArrayStyle.Separator;
			this.table.StyleV |= CellArrayStyle.SelectLine;
			this.table.DefHeight = 16;
			this.table.TabIndex = 2;
			this.table.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			//	--- D�but panelMisc
			this.buttonPageStack = new Button(this);
			this.buttonPageStack.Dock = DockStyle.Bottom;
			this.buttonPageStack.DockMargins = new Margins(0, 0, 0, 0);
			this.buttonPageStack.Command = "PageStack";
			this.buttonPageStack.Text = Res.Strings.Container.Pages.Button.PageStack;
			this.buttonPageStack.TabIndex = 100;
			this.buttonPageStack.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			
			this.panelMisc = new Widget(this);
			this.panelMisc.Dock = DockStyle.Bottom;
			this.panelMisc.DockMargins = new Margins(0, 0, 5, 0);
			this.panelMisc.Height = (this.document.Type == DocumentType.Pictogram) ? 186+24 : 186;
			this.panelMisc.TabIndex = 99;
			this.panelMisc.TabNavigation = Widget.TabNavigationMode.ForwardTabPassive;


			if ( this.document.Type == DocumentType.Pictogram )
			{
				this.languageGroup = new Widget(this.panelMisc);
				this.languageGroup.Dock = DockStyle.Bottom;
				this.languageGroup.DockMargins = new Margins(0, 0, 0, 4);
				this.languageGroup.Height = 20;
				this.languageGroup.TabIndex = 4;
				this.languageGroup.TabNavigation = Widget.TabNavigationMode.ForwardTabPassive;

				StaticText labelLanguage = new StaticText(this.languageGroup);
				labelLanguage.Text = Res.Strings.Container.Pages.Language.Label;
				labelLanguage.Alignment = ContentAlignment.MiddleRight;
				labelLanguage.Width = 94;
				labelLanguage.Dock = DockStyle.Left;
				labelLanguage.DockMargins = new Margins(0, 4, 0, 0);

				this.languageField = new TextFieldCombo(this.languageGroup);
				this.languageField.Width = 120;
				this.languageField.Items.Add("fr");
				this.languageField.Items.Add("en");
				this.languageField.Items.Add("de");
				this.languageField.Items.Add("it");
				this.languageField.Dock = DockStyle.Left;
				this.languageField.DockMargins = new Margins(0, 0, 0, 0);
				this.languageField.TextChanged += new EventHandler(this.HandleLanguageChanged);
			}

			this.pageSizeGroup = new Widget(this.panelMisc);
			this.pageSizeGroup.Dock = DockStyle.Bottom;
			this.pageSizeGroup.DockMargins = new Margins(0, 0, 0, 4);
			this.pageSizeGroup.Height = 22;
			this.pageSizeGroup.TabIndex = 3;
			this.pageSizeGroup.TabNavigation = Widget.TabNavigationMode.ForwardTabPassive;

			StaticText labelSize = new StaticText(this.pageSizeGroup);
			labelSize.Text = Res.Strings.Container.Pages.Size.Label;
			labelSize.Alignment = ContentAlignment.MiddleRight;
			labelSize.Width = 94;
			labelSize.Dock = DockStyle.Left;
			labelSize.DockMargins = new Margins(0, 4, 0, 0);

			this.pageSizeWidth = new TextFieldReal(this.pageSizeGroup);
			this.pageSizeWidth.Width = 54;
			this.pageSizeWidth.Dock = DockStyle.Left;
			this.pageSizeWidth.DockMargins = new Margins(0, 0, 0, 0);
			this.pageSizeWidth.FactorMinRange = 0.01M;
			this.pageSizeWidth.FactorMaxRange = 1.0M;
			this.pageSizeWidth.FactorStep     = 1.0M;
			this.document.Modifier.AdaptTextFieldRealDimension(this.pageSizeWidth);
			this.pageSizeWidth.TabIndex = 1;
			this.pageSizeWidth.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			this.pageSizeWidth.DefocusAction = DefocusAction.AutoAcceptOrRejectEdition;
			this.pageSizeWidth.AutoSelectOnFocus = true;
			this.pageSizeWidth.SwallowEscape = true;
			this.pageSizeWidth.SwallowReturn = true;
			this.pageSizeWidth.EditionAccepted += new EventHandler(this.HandlePageWidthEditionAccepted);
			ToolTip.Default.SetToolTip(this.pageSizeWidth, Res.Strings.Container.Pages.Size.Width);

			this.pageSizeSwap = new IconButton(this.pageSizeGroup);
			this.pageSizeSwap.Width = 12;
			this.pageSizeSwap.AutoFocus = false;
			this.pageSizeSwap.IconName = Misc.Icon("SwapDataV");
			this.pageSizeSwap.Dock = DockStyle.Left;
			this.pageSizeSwap.DockMargins = new Margins(0, 0, 0, 0);
			this.pageSizeSwap.TabIndex = 2;
			this.pageSizeSwap.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			this.pageSizeSwap.Clicked += new MessageEventHandler(this.HandlePageSwapClicked);
			ToolTip.Default.SetToolTip(this.pageSizeSwap, Res.Strings.Container.Pages.Size.Swap);

			this.pageSizeHeight = new TextFieldReal(this.pageSizeGroup);
			this.pageSizeHeight.Width = 54;
			this.pageSizeHeight.Dock = DockStyle.Left;
			this.pageSizeHeight.DockMargins = new Margins(0, 0, 0, 0);
			this.pageSizeHeight.FactorMinRange = 0.01M;
			this.pageSizeHeight.FactorMaxRange = 1.0M;
			this.pageSizeHeight.FactorStep     = 1.0M;
			this.document.Modifier.AdaptTextFieldRealDimension(this.pageSizeHeight);
			this.pageSizeHeight.TabIndex = 3;
			this.pageSizeHeight.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			this.pageSizeHeight.DefocusAction = DefocusAction.AutoAcceptOrRejectEdition;
			this.pageSizeHeight.AutoSelectOnFocus = true;
			this.pageSizeHeight.SwallowEscape = true;
			this.pageSizeHeight.SwallowReturn = true;
			this.pageSizeHeight.EditionAccepted += new EventHandler(this.HandlePageHeightEditionAccepted);
			ToolTip.Default.SetToolTip(this.pageSizeHeight, Res.Strings.Container.Pages.Size.Height);

			this.pageSizeClear = new IconButton(this.pageSizeGroup);
			this.pageSizeClear.AutoFocus = false;
			this.pageSizeClear.IconName = Misc.Icon("Nothing");
			this.pageSizeClear.Dock = DockStyle.Left;
			this.pageSizeClear.DockMargins = new Margins(0, 0, 0, 0);
			this.pageSizeClear.TabIndex = 4;
			this.pageSizeClear.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			this.pageSizeClear.Clicked += new MessageEventHandler(this.HandlePageClearClicked);
			ToolTip.Default.SetToolTip(this.pageSizeClear, Res.Strings.Container.Pages.Size.Clear);

			
			this.radioMasterGroup = new GroupBox(this.panelMisc);
			this.radioMasterGroup.Dock = DockStyle.Bottom;
			this.radioMasterGroup.DockMargins = new Margins(0, 0, 0, 4);
			this.radioMasterGroup.Height = 130;
			this.radioMasterGroup.Text = Res.Strings.Container.Pages.Button.MasterGroup;
			this.radioMasterGroup.TabIndex = 2;
			this.radioMasterGroup.TabNavigation = Widget.TabNavigationMode.ForwardTabPassive;

			this.radioAll = new RadioButton(this.radioMasterGroup);
			this.radioAll.Dock = DockStyle.Top;
			this.radioAll.DockMargins = new Margins(10, 10, 5, 0);
			this.radioAll.Text = Res.Strings.Container.Pages.Button.MasterAll;
			this.radioAll.ActiveStateChanged += new EventHandler(this.HandleRadioChanged);
			this.radioAll.Index = 1;
			this.radioAll.TabIndex = 1;
			this.radioAll.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.radioOdd = new RadioButton(this.radioMasterGroup);
			this.radioOdd.Dock = DockStyle.Top;
			this.radioOdd.DockMargins = new Margins(10, 10, 0, 0);
			this.radioOdd.Text = Res.Strings.Container.Pages.Button.MasterOdd;
			this.radioOdd.ActiveStateChanged += new EventHandler(this.HandleRadioChanged);
			this.radioOdd.Index = 2;
			this.radioOdd.TabIndex = 2;
			this.radioOdd.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.radioEven = new RadioButton(this.radioMasterGroup);
			this.radioEven.Dock = DockStyle.Top;
			this.radioEven.DockMargins = new Margins(10, 10, 0, 0);
			this.radioEven.Text = Res.Strings.Container.Pages.Button.MasterEven;
			this.radioEven.ActiveStateChanged += new EventHandler(this.HandleRadioChanged);
			this.radioEven.Index = 3;
			this.radioEven.TabIndex = 3;
			this.radioEven.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.radioNone = new RadioButton(this.radioMasterGroup);
			this.radioNone.Dock = DockStyle.Top;
			this.radioNone.DockMargins = new Margins(10, 10, 0, 0);
			this.radioNone.Text = Res.Strings.Container.Pages.Button.MasterNone;
			this.radioNone.ActiveStateChanged += new EventHandler(this.HandleRadioChanged);
			this.radioNone.Index = 4;
			this.radioNone.TabIndex = 4;
			this.radioNone.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.checkAutoStop = new CheckButton(this.radioMasterGroup);
			this.checkAutoStop.Dock = DockStyle.Top;
			this.checkAutoStop.DockMargins = new Margins(10, 10, 4, 0);
			this.checkAutoStop.Text = Res.Strings.Container.Pages.Button.MasterStop;
			this.checkAutoStop.Clicked += new MessageEventHandler(this.HandleCheckClicked);
			this.checkAutoStop.TabIndex = 5;
			this.checkAutoStop.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.specificGroup = new Widget(this.radioMasterGroup);
			this.specificGroup.Dock = DockStyle.Top;
			this.specificGroup.DockMargins = new Margins(10, 10, 2, 0);
			this.specificGroup.Width = 170;
			this.specificGroup.TabIndex = 6;
			this.specificGroup.TabNavigation = Widget.TabNavigationMode.ForwardTabPassive;

			this.checkSpecific = new CheckButton(this.specificGroup);
			this.checkSpecific.Width = 160;
			this.checkSpecific.Dock = DockStyle.Left;
			this.checkSpecific.DockMargins = new Margins(0, 0, 0, 0);
			this.checkSpecific.Text = Res.Strings.Container.Pages.Button.MasterSpecific;
			this.checkSpecific.Clicked += new MessageEventHandler(this.HandleCheckClicked);
			this.checkSpecific.TabIndex = 1;
			this.checkSpecific.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.specificMasterPage = new TextFieldCombo(this.specificGroup);
			this.specificMasterPage.Width = 50;
			this.specificMasterPage.IsReadOnly = true;
			this.specificMasterPage.Dock = DockStyle.Left;
			this.specificMasterPage.DockMargins = new Margins(0, 0, 0, 0);
			this.specificMasterPage.OpeningCombo += new CancelEventHandler(this.HandleOpeningCombo);
			this.specificMasterPage.ClosedCombo += new EventHandler(this.HandleClosedCombo);
			this.specificMasterPage.TabIndex = 2;
			this.specificMasterPage.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;


			this.radioSlaveGroup = new GroupBox(this.panelMisc);
			this.radioSlaveGroup.Dock = DockStyle.Bottom;
			this.radioSlaveGroup.DockMargins = new Margins(0, 0, 0, 4);
			this.radioSlaveGroup.Height = 130;
			this.radioSlaveGroup.Text = Res.Strings.Container.Pages.Button.SlaveGroup;
			this.radioSlaveGroup.TabIndex = 3;
			this.radioSlaveGroup.TabNavigation = Widget.TabNavigationMode.ForwardTabPassive;

			this.radioGroupLeft = new Widget(this.radioSlaveGroup);
			this.radioGroupLeft.Dock = DockStyle.Left;
			this.radioGroupLeft.DockMargins = new Margins(0, 0, 5, 0);
			this.radioGroupLeft.Width = 170;
			this.radioGroupLeft.TabIndex = 1;
			this.radioGroupLeft.TabNavigation = Widget.TabNavigationMode.ForwardTabPassive;

			this.radioNever = new RadioButton(this.radioGroupLeft);
			this.radioNever.Dock = DockStyle.Top;
			this.radioNever.DockMargins = new Margins(10, 0, 0, 0);
			this.radioNever.Text = Res.Strings.Container.Pages.Button.SlaveNever;
			this.radioNever.ActiveStateChanged += new EventHandler(this.HandleRadioChanged);
			this.radioNever.Index = 1;
			this.radioNever.TabIndex = 1;
			this.radioNever.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.radioDefault = new RadioButton(this.radioGroupLeft);
			this.radioDefault.Dock = DockStyle.Top;
			this.radioDefault.DockMargins = new Margins(10, 0, 0, 0);
			this.radioDefault.Text = Res.Strings.Container.Pages.Button.SlaveDefault;
			this.radioDefault.ActiveStateChanged += new EventHandler(this.HandleRadioChanged);
			this.radioDefault.Index = 2;
			this.radioDefault.TabIndex = 2;
			this.radioDefault.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.radioSpecific = new RadioButton(this.radioGroupLeft);
			this.radioSpecific.Dock = DockStyle.Top;
			this.radioSpecific.DockMargins = new Margins(10, 0, 0, 0);
			this.radioSpecific.Text = Res.Strings.Container.Pages.Button.SlaveSpecific;
			this.radioSpecific.ActiveStateChanged += new EventHandler(this.HandleRadioChanged);
			this.radioSpecific.Index = 3;
			this.radioSpecific.TabIndex = 3;
			this.radioSpecific.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.checkGuides = new CheckButton(this.radioGroupLeft);
			this.checkGuides.Dock = DockStyle.Top;
			this.checkGuides.DockMargins = new Margins(10, 0, 4, 0);
			this.checkGuides.Text = Res.Strings.Container.Pages.Button.SlaveGuides;
			this.checkGuides.Clicked += new MessageEventHandler(this.HandleCheckClicked);
			this.checkGuides.TabIndex = 4;
			this.checkGuides.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.radioGroupRight = new Widget(this.radioSlaveGroup);
			this.radioGroupRight.Dock = DockStyle.Left;
			this.radioGroupRight.DockMargins = new Margins(0, 0, 5, 0);
			this.radioGroupRight.Width = 50;
			this.radioGroupRight.TabIndex = 2;
			this.radioGroupRight.TabNavigation = Widget.TabNavigationMode.ForwardTabPassive;

			this.specificSlavePage = new TextFieldCombo(this.radioGroupRight);
			this.specificSlavePage.IsReadOnly = true;
			this.specificSlavePage.Dock = DockStyle.Bottom;
			this.specificSlavePage.DockMargins = new Margins(0, 0, 0, 61);
			this.specificSlavePage.OpeningCombo += new CancelEventHandler(this.HandleOpeningCombo);
			this.specificSlavePage.ClosedCombo += new EventHandler(this.HandleClosedCombo);
			this.specificSlavePage.TabIndex = 1;
			this.specificSlavePage.TabNavigation = Widget.TabNavigationMode.ForwardTabPassive;


			this.radioGroup = new Widget(this.panelMisc);
			this.radioGroup.Dock = DockStyle.Bottom;
			this.radioGroup.DockMargins = new Margins(0, 0, 0, 4);
			this.radioGroup.Height = 20;
			this.radioGroup.TabIndex = 1;
			this.radioGroup.TabNavigation = Widget.TabNavigationMode.ForwardTabPassive;

			this.radioSlave = new RadioButton(this.radioGroup);
			this.radioSlave.Width = 100;
			this.radioSlave.Dock = DockStyle.Left;
			this.radioSlave.DockMargins = new Margins(10, 10, 0, 0);
			this.radioSlave.Text = Res.Strings.Container.Pages.Button.Slave;
			this.radioSlave.ActiveStateChanged += new EventHandler(this.HandleRadioChanged);
			this.radioSlave.Index = 1;
			this.radioSlave.TabIndex = 1;
			this.radioSlave.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.radioMaster = new RadioButton(this.radioGroup);
			this.radioMaster.Width = 100;
			this.radioMaster.Dock = DockStyle.Left;
			this.radioMaster.DockMargins = new Margins(10, 10, 0, 0);
			this.radioMaster.Text = Res.Strings.Container.Pages.Button.Master;
			this.radioMaster.ActiveStateChanged += new EventHandler(this.HandleRadioChanged);
			this.radioMaster.Index = 2;
			this.radioMaster.TabIndex = 2;
			this.radioMaster.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			//	--- Fin panelMisc
			
			this.extendedButton = new GlyphButton(this);
			this.extendedButton.Dock = DockStyle.Bottom;
			this.extendedButton.DockMargins = new Margins(0, 0, 5, 0);
			this.extendedButton.ButtonStyle = ButtonStyle.Icon;
			this.extendedButton.Clicked += new MessageEventHandler(this.ExtendedButtonClicked);
			this.extendedButton.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			this.extendedButton.TabIndex = 98;
			this.extendedButton.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			ToolTip.Default.SetToolTip(this.extendedButton, Res.Strings.Dialog.Button.More);
			

			this.toolBarName = new HToolBar(this);
			this.toolBarName.Dock = DockStyle.Bottom;
			this.toolBarName.DockMargins = new Margins(0, 0, 0, 0);
			this.toolBarName.TabIndex = 97;
			this.toolBarName.TabNavigation = Widget.TabNavigationMode.ForwardTabPassive;

			StaticText st = new StaticText();
			st.Width = 80;
			st.Text = Res.Strings.Panel.PageName.Label.Name;
			this.toolBarName.Items.Add(st);

			this.name = new TextField();
			this.name.Width = 140;
			this.name.DockMargins = new Margins(0, 0, 1, 1);
			this.name.TextChanged += new EventHandler(this.HandleNameTextChanged);
			this.name.TabIndex = 1;
			this.name.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			this.toolBarName.Items.Add(this.name);
			ToolTip.Default.SetToolTip(this.name, Res.Strings.Panel.PageName.Tooltip.Name);

			
			this.UpdateExtended();
		}
		
		protected void Synchro(Widget widget)
		{
			//	Synchronise avec l'�tat de la commande.
			//	TODO: devrait �tre inutile, � supprimer donc !!!
#if false //#fix
			widget.Enable = (this.toolBar.CommandDispatcher[widget.Command].Enabled);
#endif
		}


		protected override void DoUpdateContent()
		{
			//	Effectue la mise � jour du contenu.
			this.UpdateTable();
			this.UpdatePanel();
		}

		protected override void DoUpdateObject(Objects.Abstract obj)
		{
			//	Effectue la mise � jour d'un objet.
			Objects.Page page = obj as Objects.Page;
			UndoableList pages = this.document.GetObjects;
			int rank = pages.IndexOf(obj);
			this.TableUpdateRow(rank, page);
		}

		protected void UpdateTable()
		{
			//	Met � jour le contenu de la table.
			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			int sel = context.CurrentPage;

			int rows = context.TotalPages();
			int initialColumns = this.table.Columns;
			this.table.SetArraySize(2, rows);

			if ( initialColumns == 0 )
			{
				this.table.SetWidthColumn(0, 40);
				this.table.SetWidthColumn(1, 177);
			}

			this.table.SetHeaderTextH(0, Res.Strings.Container.Pages.Header.Number);
			this.table.SetHeaderTextH(1, Res.Strings.Container.Pages.Header.Name);

			UndoableList doc = this.document.GetObjects;
			for ( int i=0 ; i<rows ; i++ )
			{
				Objects.Page page = doc[i] as Objects.Page;
				this.TableFillRow(i);
				this.TableUpdateRow(i, page);
			}
		}

		protected void TableFillRow(int row)
		{
			//	Peuple une ligne de la table, si n�cessaire.
			for ( int column=0 ; column<this.table.Columns ; column++ )
			{
				if ( this.table[column, row].IsEmpty )
				{
					StaticText st = new StaticText();
					st.Alignment = (column==0) ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft;
					st.Dock = DockStyle.Fill;
					st.DockMargins = new Margins(4, 4, 0, 0);
					this.table[column, row].Insert(st);
				}
			}
		}

		protected void TableUpdateRow(int row, Objects.Page page)
		{
			//	Met � jour le contenu d'une ligne de la table.
			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			StaticText st;

			st = this.table[0, row].Children[0] as StaticText;
			st.Text = page.ShortName;

			st = this.table[1, row].Children[0] as StaticText;
			st.Text = page.LongName;

			this.table.SelectRow(row, row==context.CurrentPage);
		}

		protected void UpdatePanel()
		{
			//	Met � jour le panneau pour �diter la page s�lectionn�e.
			this.UpdatePageName();

			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			Objects.Page page = context.RootObject(1) as Objects.Page;

			Size size = page.PageSize;
			string language = page.Language;

			bool widthDefined = true;
			if ( size.Width == 0 )
			{
				size.Width = this.document.DocumentSize.Width;
				widthDefined = false;
			}

			bool heightDefined = true;
			if ( size.Height == 0 )
			{
				size.Height = this.document.DocumentSize.Height;
				heightDefined = false;
			}

			this.ignoreChanged = true;

			this.pageSizeWidth.InternalValue = (decimal) size.Width;
			this.pageSizeWidth.TextDisplayMode = widthDefined ? TextDisplayMode.Defined : TextDisplayMode.Proposal;

			this.languageField.Text = language;

			this.pageSizeHeight.InternalValue = (decimal) size.Height;
			this.pageSizeHeight.TextDisplayMode = heightDefined ? TextDisplayMode.Defined : TextDisplayMode.Proposal;

			this.radioSlave.ActiveState  = (page.MasterType == Objects.MasterType.Slave) ? ActiveState.Yes : ActiveState.No;
			this.radioMaster.ActiveState = (page.MasterType != Objects.MasterType.Slave) ? ActiveState.Yes : ActiveState.No;

			this.radioSlaveGroup.Visibility = (page.MasterType == Objects.MasterType.Slave);
			this.radioMasterGroup.Visibility = (page.MasterType != Objects.MasterType.Slave);

			this.radioAll.ActiveState =  (page.MasterType == Objects.MasterType.All ) ? ActiveState.Yes : ActiveState.No;
			this.radioEven.ActiveState = (page.MasterType == Objects.MasterType.Even) ? ActiveState.Yes : ActiveState.No;
			this.radioOdd.ActiveState =  (page.MasterType == Objects.MasterType.Odd ) ? ActiveState.Yes : ActiveState.No;
			this.radioNone.ActiveState = (page.MasterType == Objects.MasterType.None) ? ActiveState.Yes : ActiveState.No;

			this.radioNever.ActiveState    = (page.MasterUse == Objects.MasterUse.Never   ) ? ActiveState.Yes : ActiveState.No;
			this.radioDefault.ActiveState  = (page.MasterUse == Objects.MasterUse.Default ) ? ActiveState.Yes : ActiveState.No;
			this.radioSpecific.ActiveState = (page.MasterUse == Objects.MasterUse.Specific) ? ActiveState.Yes : ActiveState.No;

			this.checkGuides.ActiveState = page.MasterGuides ? ActiveState.Yes : ActiveState.No;

			this.specificSlavePage.Enable = (page.MasterUse == Objects.MasterUse.Specific);
			if ( page.MasterPageToUse == null ||
				 page.MasterPageToUse.MasterType == Objects.MasterType.Slave )
			{
				this.specificSlavePage.Text = "";
			}
			else
			{
				this.specificSlavePage.Text = page.MasterPageToUse.ShortName;
			}

			this.checkAutoStop.ActiveState = page.MasterAutoStop ? ActiveState.Yes : ActiveState.No;
			this.checkSpecific.ActiveState = page.MasterSpecific ? ActiveState.Yes : ActiveState.No;

			this.specificMasterPage.Enable = (page.MasterSpecific);
			if ( page.MasterPageToUse == null || !page.MasterSpecific )
			{
				this.specificMasterPage.Text = "";
			}
			else
			{
				this.specificMasterPage.Text = page.MasterPageToUse.ShortName;
			}

			this.ignoreChanged = false;
		}

		protected void UpdatePageName()
		{
			//	Met � jour le panneau pour �diter le nom de la page s�lectionn�e.
			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			int sel = context.CurrentPage;
			string text = this.document.Modifier.PageName(sel);

			this.ignoreChanged = true;
			this.name.Text = text;
			this.ignoreChanged = false;
		}


		private void HandleTableSelectionChanged(object sender)
		{
			//	Liste cliqu�e.
			if ( this.table.SelectedRow == -1 )  return;
			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			context.CurrentPage = this.table.SelectedRow;
		}

		private void HandleTableDoubleClicked(object sender, MessageEventArgs e)
		{
			//	Liste double-cliqu�e.
			this.name.SelectAll();
			this.name.Focus();
		}

		private void HandleNameTextChanged(object sender)
		{
			//	Le nom de la page a chang�.
			if ( this.ignoreChanged )  return;

			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			int sel = context.CurrentPage;

			if ( this.document.Modifier.PageName(sel) != this.name.Text )
			{
				this.document.Modifier.PageName(sel, this.name.Text);
			}
		}

		private void ExtendedButtonClicked(object sender, MessageEventArgs e)
		{
			//	Le bouton pour �tendre/r�duire le panneau a �t� cliqu�.
			this.isExtended = !this.isExtended;
			this.UpdateExtended();
		}

		protected void UpdateExtended()
		{
			//	Met � jour l'�tat r�duit/�tendu du panneau.
			this.extendedButton.GlyphShape = this.isExtended ? GlyphShape.ArrowDown : GlyphShape.ArrowUp;

			this.panelMisc.Visibility = (this.isExtended);
			this.buttonPageStack.Visibility = (this.isExtended);
		}

		private void HandleLanguageChanged(object sender)
		{
			if ( this.ignoreChanged )  return;

			this.document.Modifier.OpletQueueBeginAction(Res.Strings.Container.Pages.Language.Label, "SpecialPageLanguage");

			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			Objects.Page page = context.RootObject(1) as Objects.Page;

			page.Language = this.languageField.Text;

			this.document.Modifier.ActiveViewer.DrawingContext.ZoomPageAndCenter();
			this.document.Notifier.NotifyPagesChanged();
			this.document.Modifier.OpletQueueValidateAction();
		}

		private void HandlePageWidthEditionAccepted(object sender)
		{
			if ( this.ignoreChanged )  return;

			this.document.Modifier.OpletQueueBeginAction(Res.Strings.Container.Pages.Size.Label, "SpecialPageSize");

			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			Objects.Page page = context.RootObject(1) as Objects.Page;

			Size size = page.PageSize;
			size.Width = (double) this.pageSizeWidth.InternalValue;
			page.PageSize = size;

			this.document.Modifier.ActiveViewer.DrawingContext.ZoomPageAndCenter();
			this.document.Notifier.NotifyPagesChanged();
			this.document.Modifier.OpletQueueValidateAction();
		}

		private void HandlePageHeightEditionAccepted(object sender)
		{
			if ( this.ignoreChanged )  return;

			this.document.Modifier.OpletQueueBeginAction(Res.Strings.Container.Pages.Size.Label, "SpecialPageSize");

			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			Objects.Page page = context.RootObject(1) as Objects.Page;

			Size size = page.PageSize;
			size.Height = (double) this.pageSizeHeight.InternalValue;
			page.PageSize = size;

			this.document.Modifier.ActiveViewer.DrawingContext.ZoomPageAndCenter();
			this.document.Notifier.NotifyPagesChanged();
			this.document.Modifier.OpletQueueValidateAction();
		}

		private void HandlePageSwapClicked(object sender, MessageEventArgs e)
		{
			this.document.Modifier.OpletQueueBeginAction(Res.Strings.Container.Pages.Size.Swap);

			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			Objects.Page page = context.RootObject(1) as Objects.Page;

			Size size = new Size(page.PageSize.Height, page.PageSize.Width);
			if ( size.Width == 0 && size.Height == 0 )
			{
				size.Width  = this.document.DocumentSize.Height;
				size.Height = this.document.DocumentSize.Width;
			}
			page.PageSize = size;

			this.document.Modifier.ActiveViewer.DrawingContext.ZoomPageAndCenter();
			this.document.Notifier.NotifyPagesChanged();
			this.document.Modifier.OpletQueueValidateAction();
		}

		private void HandlePageClearClicked(object sender, MessageEventArgs e)
		{
			this.document.Modifier.OpletQueueBeginAction(Res.Strings.Container.Pages.Size.Clear);

			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			Objects.Page page = context.RootObject(1) as Objects.Page;

			page.PageSize = new Size(0,0);

			this.document.Modifier.ActiveViewer.DrawingContext.ZoomPageAndCenter();
			this.document.Notifier.NotifyPagesChanged();
			this.document.Modifier.OpletQueueValidateAction();
		}

		private void HandleRadioChanged(object sender)
		{
			//	Un bouton radio a �t� cliqu�.
			RadioButton radio = sender as RadioButton;
			if ( radio == null )  return;
			if ( radio.ActiveState != ActiveState.Yes )  return;

			this.document.Modifier.OpletQueueBeginAction(Res.Strings.Action.PageChangeStatus);

			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			Objects.Page page = context.RootObject(1) as Objects.Page;

			if ( sender == this.radioSlave )
			{
				page.MasterType = Objects.MasterType.Slave;
			}
			if ( sender == this.radioMaster )
			{
				page.MasterType = Objects.MasterType.All;
			}

			if ( sender == this.radioAll )
			{
				page.MasterType = Objects.MasterType.All;
			}
			if ( sender == this.radioEven )
			{
				page.MasterType = Objects.MasterType.Even;
			}
			if ( sender == this.radioOdd )
			{
				page.MasterType = Objects.MasterType.Odd;
			}
			if ( sender == this.radioNone )
			{
				page.MasterType = Objects.MasterType.None;
			}

			if ( sender == this.radioNever )
			{
				page.MasterUse = Objects.MasterUse.Never;
			}
			if ( sender == this.radioDefault )
			{
				page.MasterUse = Objects.MasterUse.Default;
			}
			if ( sender == this.radioSpecific )
			{
				page.MasterUse = Objects.MasterUse.Specific;
			}

			this.UpdatePanel();
			this.document.Notifier.NotifyPagesChanged();
			this.document.Modifier.OpletQueueValidateAction();
		}

		private void HandleCheckClicked(object sender, MessageEventArgs e)
		{
			//	Un bouton � cocher a �t� cliqu�.
			this.document.Modifier.OpletQueueBeginAction(Res.Strings.Action.PageChangeStatus);

			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			Objects.Page page = context.RootObject(1) as Objects.Page;

			if ( sender == this.checkGuides )
			{
				page.MasterGuides = !page.MasterGuides;
			}

			if ( sender == this.checkAutoStop )
			{
				page.MasterAutoStop = !page.MasterAutoStop;
			}

			if ( sender == this.checkSpecific )
			{
				page.MasterSpecific = !page.MasterSpecific;
			}

			this.UpdatePanel();
			this.document.Notifier.NotifyPagesChanged();
			this.document.Modifier.OpletQueueValidateAction();
		}

		private void HandleOpeningCombo(object sender, CancelEventArgs e)
		{
			//	Combo ouvert.
			TextFieldCombo field = sender as TextFieldCombo;
			field.Items.Clear();

			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			UndoableList doc = this.document.GetObjects;
			Objects.Page currentPage = context.RootObject(1) as Objects.Page;
			int total = context.TotalPages();
			for ( int i=0 ; i<total ; i++ )
			{
				Objects.Page page = doc[i] as Objects.Page;
				if ( page == currentPage )  continue;
				if ( page.MasterType != Objects.MasterType.Slave )
				{
					field.Items.Add(page.ShortName);
				}
			}
		}

		private void HandleClosedCombo(object sender)
		{
			//	Combo ferm�.
			if ( this.ignoreChanged )  return;
			TextFieldCombo field = sender as TextFieldCombo;
			DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
			UndoableList doc = this.document.GetObjects;
			int total = context.TotalPages();
			for ( int i=0 ; i<total ; i++ )
			{
				Objects.Page page = doc[i] as Objects.Page;
				if ( page.ShortName == field.Text )
				{
					this.document.Modifier.OpletQueueBeginAction(Res.Strings.Action.PageChangeStatus);
					Objects.Page currentPage = context.RootObject(1) as Objects.Page;
					currentPage.MasterPageToUse = page;
					this.document.Notifier.NotifyPagesChanged();
					this.document.Modifier.OpletQueueValidateAction();
					return;
				}
			}
		}


		protected HToolBar					toolBar;
		protected IconButton				buttonNew;
		protected IconButton				buttonDuplicate;
		protected IconButton				buttonUp;
		protected IconButton				buttonDown;
		protected IconButton				buttonDelete;
		protected CellTable					table;
		protected HToolBar					toolBarName;
		protected TextField					name;
		protected GlyphButton				extendedButton;
		protected Widget					panelMisc;

		protected Widget					pageSizeGroup;
		protected TextFieldReal				pageSizeWidth;
		protected TextFieldReal				pageSizeHeight;
		protected IconButton				pageSizeSwap;
		protected IconButton				pageSizeClear;

		protected Widget					languageGroup;
		protected TextFieldCombo			languageField;

		protected Widget					radioGroup;
		protected RadioButton				radioSlave;
		protected RadioButton				radioMaster;

		protected GroupBox					radioMasterGroup;
		protected RadioButton				radioAll;
		protected RadioButton				radioEven;
		protected RadioButton				radioOdd;
		protected RadioButton				radioNone;
		protected CheckButton				checkAutoStop;
		protected Widget					specificGroup;
		protected CheckButton				checkSpecific;
		protected TextFieldCombo			specificMasterPage;
	
		protected GroupBox					radioSlaveGroup;
		protected Widget					radioGroupLeft;
		protected Widget					radioGroupRight;
		protected RadioButton				radioNever;
		protected RadioButton				radioDefault;
		protected RadioButton				radioSpecific;
		protected CheckButton				checkGuides;
		protected TextFieldCombo			specificSlavePage;

		protected Button					buttonPageStack;

		protected bool						isExtended = false;
		protected bool						ignoreChanged = false;
	}
}
