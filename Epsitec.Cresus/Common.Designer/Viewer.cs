using System.Collections.Generic;
using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Designer
{
	/// <summary>
	/// Permet de repr�senter les ressources d'un module.
	/// </summary>
	public class Viewer : Widget
	{
		public Viewer(Module module)
		{
			this.module = module;

			this.labelsIndex = new List<string>();

			int tabIndex = 0;

			this.primaryCulture = new IconButtonMark(this);
			this.primaryCulture.ButtonStyle = ButtonStyle.ActivableIcon;
			this.primaryCulture.SiteMark = SiteMark.OnBottom;
			this.primaryCulture.MarkDimension = 5;
			this.primaryCulture.ActiveState = ActiveState.Yes;
			this.primaryCulture.AutoFocus = false;
			this.primaryCulture.TabIndex = tabIndex++;
			this.primaryCulture.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.array = new MyWidgets.StringArray(this);
			this.array.Columns = 3;
			this.array.SetColumnsRelativeWidth(0, 0.30);
			this.array.SetColumnsRelativeWidth(1, 0.35);
			this.array.SetColumnsRelativeWidth(2, 0.35);
			this.array.SetDynamicsToolTips(0, true);
			this.array.SetDynamicsToolTips(1, false);
			this.array.SetDynamicsToolTips(2, false);
			this.array.ColumnsWidthChanged += new EventHandler(this.HandleArrayColumnsWidthChanged);
			this.array.CellsQuantityChanged += new EventHandler(this.HandleArrayCellsQuantityChanged);
			this.array.CellsContentChanged += new EventHandler(this.HandleArrayCellsContentChanged);
			this.array.SelectedRowChanged += new EventHandler(this.HandleArraySelectedRowChanged);
			this.array.TabIndex = tabIndex++;
			this.array.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.labelStatic = new StaticText(this);
			this.labelStatic.Alignment = ContentAlignment.MiddleRight;
			this.labelStatic.Text = Res.Strings.Viewer.Edit;
			this.labelStatic.Visibility = (this.module.Mode != DesignerMode.Build);

			this.labelEdit = new TextFieldMulti(this);
			this.labelEdit.Name = "LabelEdit";
			this.labelEdit.TextChanged += new EventHandler(this.HandleTextChanged);
			this.labelEdit.KeyboardFocusChanged += new EventHandler<Epsitec.Common.Types.DependencyPropertyChangedEventArgs>(this.HandleEditKeyboardFocusChanged);
			this.labelEdit.TabIndex = tabIndex++;
			this.labelEdit.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			this.labelEdit.Visibility = (this.module.Mode == DesignerMode.Build);

			this.primaryEdit = new TextFieldMulti(this);
			this.primaryEdit.Name = "PrimaryEdit";
			this.primaryEdit.TextChanged += new EventHandler(this.HandleTextChanged);
			this.primaryEdit.KeyboardFocusChanged += new EventHandler<Epsitec.Common.Types.DependencyPropertyChangedEventArgs>(this.HandleEditKeyboardFocusChanged);
			this.primaryEdit.TabIndex = tabIndex++;
			this.primaryEdit.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.secondaryEdit = new TextFieldMulti(this);
			this.secondaryEdit.Name = "SecondaryEdit";
			this.secondaryEdit.TextChanged += new EventHandler(this.HandleTextChanged);
			this.secondaryEdit.KeyboardFocusChanged += new EventHandler<Epsitec.Common.Types.DependencyPropertyChangedEventArgs>(this.HandleEditKeyboardFocusChanged);
			this.secondaryEdit.TabIndex = tabIndex++;
			this.secondaryEdit.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.labelAbout = new StaticText(this);
			this.labelAbout.Alignment = ContentAlignment.MiddleRight;
			this.labelAbout.Text = Res.Strings.Viewer.About;

			this.primaryAbout = new TextFieldMulti(this);
			this.primaryAbout.Name = "PrimaryAbout";
			this.primaryAbout.TextChanged += new EventHandler(this.HandleTextChanged);
			this.primaryAbout.KeyboardFocusChanged += new EventHandler<Epsitec.Common.Types.DependencyPropertyChangedEventArgs>(this.HandleEditKeyboardFocusChanged);
			this.primaryAbout.TabIndex = tabIndex++;
			this.primaryAbout.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.secondaryAbout = new TextFieldMulti(this);
			this.secondaryAbout.Name = "SecondaryAbout";
			this.secondaryAbout.TextChanged += new EventHandler(this.HandleTextChanged);
			this.secondaryAbout.KeyboardFocusChanged += new EventHandler<Epsitec.Common.Types.DependencyPropertyChangedEventArgs>(this.HandleEditKeyboardFocusChanged);
			this.secondaryAbout.TabIndex = tabIndex++;
			this.secondaryAbout.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;

			this.UpdateCultures();
			this.UpdateEdit();
			this.UpdateCommands();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.array.ColumnsWidthChanged -= new EventHandler(this.HandleArrayColumnsWidthChanged);
				this.array.CellsQuantityChanged -= new EventHandler(this.HandleArrayCellsQuantityChanged);
				this.array.CellsContentChanged -= new EventHandler(this.HandleArrayCellsContentChanged);
				this.array.SelectedRowChanged -= new EventHandler(this.HandleArraySelectedRowChanged);

				this.labelEdit.TextChanged -= new EventHandler(this.HandleTextChanged);
				this.labelEdit.KeyboardFocusChanged -= new EventHandler<Epsitec.Common.Types.DependencyPropertyChangedEventArgs>(this.HandleEditKeyboardFocusChanged);

				this.primaryEdit.TextChanged -= new EventHandler(this.HandleTextChanged);
				this.primaryEdit.KeyboardFocusChanged -= new EventHandler<Epsitec.Common.Types.DependencyPropertyChangedEventArgs>(this.HandleEditKeyboardFocusChanged);

				this.secondaryEdit.TextChanged -= new EventHandler(this.HandleTextChanged);
				this.secondaryEdit.KeyboardFocusChanged -= new EventHandler<Epsitec.Common.Types.DependencyPropertyChangedEventArgs>(this.HandleEditKeyboardFocusChanged);

				this.primaryAbout.TextChanged -= new EventHandler(this.HandleTextChanged);
				this.primaryAbout.KeyboardFocusChanged -= new EventHandler<Epsitec.Common.Types.DependencyPropertyChangedEventArgs>(this.HandleEditKeyboardFocusChanged);

				this.secondaryAbout.TextChanged -= new EventHandler(this.HandleTextChanged);
				this.secondaryAbout.KeyboardFocusChanged -= new EventHandler<Epsitec.Common.Types.DependencyPropertyChangedEventArgs>(this.HandleEditKeyboardFocusChanged);
			}

			base.Dispose(disposing);
		}


		public AbstractTextField CurrentTextField
		{
			//	Retourne le texte �ditable en cours d'�dition.
			get
			{
				return this.currentTextField;
			}
		}

		public void DoSearch(string search, Searcher.SearchingMode mode)
		{
			//	Effectue une recherche.
#if false
			int sel = this.array.SelectedRow;
			if (sel == -1)
			{
				sel = ((mode&Searcher.SearchingMode.Reverse) != 0) ? 0 : this.labelsIndex.Count-1;
			}

			int dir = ((mode&Searcher.SearchingMode.Reverse) != 0) ? -1 : 1;

			if ((mode&Searcher.SearchingMode.CaseSensitive) == 0)
			{
				search = search.ToLower();
			}

			int column = -1;
			int index = -1;

			for (int i=0; i<this.labelsIndex.Count; i++)
			{
				sel += dir;

				if (sel >= this.labelsIndex.Count)
				{
					sel = 0;
				}

				if (sel < 0)
				{
					sel = this.labelsIndex.Count-1;
				}

				string label     = this.labelsIndex[sel];
				string primary   = this.primaryBundle[label].AsString;
				string secondary = this.secondaryBundle[label].AsString;
				string pAbout    = this.primaryBundle[label].About;
				string sAbout    = this.secondaryBundle[label].About;

				if ( secondary == null )  secondary = "";
				if ( pAbout    == null )  pAbout    = "";
				if ( sAbout    == null )  sAbout    = "";

				if ((mode&Searcher.SearchingMode.CaseSensitive) == 0)
				{
					label     = label.ToLower();
					primary   = primary.ToLower();
					secondary = secondary.ToLower();
					pAbout    = pAbout.ToLower();
					sAbout    = sAbout.ToLower();
				}

				if (((mode&Searcher.SearchingMode.SearchInLabel) != 0) && label.Contains(search))
				{
					break;
				}

				if ((mode&Searcher.SearchingMode.SearchInPrimaryText) != 0)
				{
					index = primary.IndexOf(search);
					if (index != -1)
					{
						column = 1;
						break;
					}
				}

				if ((mode&Searcher.SearchingMode.SearchInSecondaryText) != 0)
				{
					index = secondary.IndexOf(search);
					if (index != -1)
					{
						column = 2;
						break;
					}
				}

				if ((mode&Searcher.SearchingMode.SearchInPrimaryAbout) != 0)
				{
					index = pAbout.IndexOf(search);
					if (index != -1)
					{
						column = 3;
						break;
					}
				}

				if ((mode&Searcher.SearchingMode.SearchInSecondaryAbout) != 0)
				{
					index = sAbout.IndexOf(search);
					if (index != -1)
					{
						column = 4;
						break;
					}
				}
			}

			this.array.SelectedRow = sel;
			this.array.ShowSelectedRow();

			AbstractTextField edit = null;
			if (column == 1)  edit = this.primaryEdit;
			if (column == 2)  edit = this.secondaryEdit;
			if (column == 3)  edit = this.primaryAbout;
			if (column == 4)  edit = this.secondaryAbout;
			if (edit != null)
			{
				this.Window.MakeActive();
				edit.Focus();
				edit.CursorFrom  = index;
				edit.CursorTo    = index+search.Length;
				edit.CursorAfter = false;
			}
#endif

#if true
			Searcher searcher = new Searcher(this.labelsIndex, this.primaryBundle, this.secondaryBundle);
			searcher.FixStarting(mode, this.array.SelectedRow, this.currentTextField);

			if (searcher.Search(search))
			{
				this.array.SelectedRow = searcher.Row;
				this.array.ShowSelectedRow();

				AbstractTextField edit = null;
				if (searcher.Field == 0)  edit = this.labelEdit;
				if (searcher.Field == 1)  edit = this.primaryEdit;
				if (searcher.Field == 2)  edit = this.secondaryEdit;
				if (searcher.Field == 3)  edit = this.primaryAbout;
				if (searcher.Field == 4)  edit = this.secondaryAbout;
				if (edit != null && edit.Visibility)
				{
					this.Window.MakeActive();
					edit.Focus();
					edit.CursorFrom  = edit.TextLayout.FindIndexFromOffset(searcher.Index);
					edit.CursorTo    = edit.TextLayout.FindIndexFromOffset(searcher.Index+searcher.Length);
					edit.CursorAfter = false;
				}
			}
			else
			{
				this.module.MainWindow.DialogError(Res.Strings.Dialog.Search.Error);
			}
#endif
		}

		public void DoFilter(string filter, Searcher.SearchingMode mode)
		{
			//	Change le filtre des ressources visibles.
			string label = "";
			int sel = this.array.SelectedRow;
			if (sel != -1 && sel < this.labelsIndex.Count)
			{
				label = this.labelsIndex[sel];
			}

			this.UpdateLabelsIndex(filter, mode);
			this.UpdateArray();

			sel = this.labelsIndex.IndexOf(label);
			this.array.SelectedRow = sel;
			this.array.ShowSelectedRow();
			this.UpdateCommands();
		}

		public void DoAccess(string name)
		{
			//	Change la ressource visible.
			int sel = this.array.SelectedRow;

			if ( name == "AccessFirst" )  sel = 0;
			if ( name == "AccessPrev"  )  sel --;
			if ( name == "AccessNext"  )  sel ++;
			if ( name == "AccessLast"  )  sel = 1000000;

			this.array.SelectedRow = sel;
			this.array.ShowSelectedRow();
			this.UpdateCommands();
		}

		public void DoModification(string name)
		{
			//	Change la ressource modifi�e visible.
			int sel = this.array.SelectedRow;

			if (name == "ModificationAll")
			{
				if (sel == -1)  return;
				string label = this.labelsIndex[sel];
				if (this.primaryBundle[label] == null || this.primaryBundle[label].Name == null)  return;

				this.primaryBundle[label].SetModificationId(this.primaryBundle[label].ModificationId+1);
				this.UpdateArray();
				this.UpdateCommands();
				this.module.Modifier.IsDirty = true;
			}
			else if (name == "ModificationClear")
			{
				if (sel == -1)  return;
				string label = this.labelsIndex[sel];
				if (this.secondaryBundle[label] == null || this.secondaryBundle[label].Name == null)  return;

				this.secondaryBundle[label].SetModificationId(this.primaryBundle[label].ModificationId);
				this.UpdateArray();
				this.UpdateCommands();
				this.module.Modifier.IsDirty = true;
			}
			else
			{
				if (sel == -1)
				{
					sel = (name == "ModificationPrev") ? 0 : this.labelsIndex.Count-1;
				}

				int column = -1;
				int dir = (name == "ModificationPrev") ? -1 : 1;

				for (int i=0; i<this.labelsIndex.Count; i++)
				{
					sel += dir;

					if (sel >= this.labelsIndex.Count)
					{
						sel = 0;
					}

					if (sel < 0)
					{
						sel = this.labelsIndex.Count-1;
					}

					string label  = this.labelsIndex[sel];
					int primary   = this.primaryBundle[label].ModificationId;
					int secondary = this.secondaryBundle[label].ModificationId;

					if ( this.secondaryBundle[label] == null          ||
						 this.secondaryBundle[label].Name == null     ||
						 this.secondaryBundle[label].AsString == null ||
						 this.secondaryBundle[label].AsString == ""   )
					{
						column = 2;
						break;
					}

					if (primary > secondary)
					{
						column = 2;
						break;
					}
				}

				this.array.SelectedRow = sel;
				this.array.ShowSelectedRow();

				AbstractTextField edit = null;
				if (column == 1)  edit = this.primaryEdit;
				if (column == 2)  edit = this.secondaryEdit;
				if (edit != null)
				{
					this.Window.MakeActive();
					edit.Focus();
					edit.SelectAll();
				}
			}
		}

		public void DoWarning(string name)
		{
			//	Change la ressource manquante visible.
			int sel = this.array.SelectedRow;
			if (sel == -1)
			{
				sel = 0;
			}

			int column = -1;
			int dir = (name == "WarningPrev") ? -1 : 1;

			for (int i=0; i<this.labelsIndex.Count; i++)
			{
				sel += dir;

				if (sel >= this.labelsIndex.Count)
				{
					sel = 0;
				}

				if (sel < 0)
				{
					sel = this.labelsIndex.Count-1;
				}

				string label = this.labelsIndex[sel];

				if ( this.secondaryBundle[label] == null          ||
					 this.secondaryBundle[label].Name == null     ||
					 this.secondaryBundle[label].AsString == null ||
					 this.secondaryBundle[label].AsString == ""   )
				{
					column = 2;
					break;
				}
			}

			this.array.SelectedRow = sel;
			this.array.ShowSelectedRow();

			AbstractTextField edit = null;
			if (column == 1)  edit = this.primaryEdit;
			if (column == 2)  edit = this.secondaryEdit;
			if (edit != null)
			{
				this.Window.MakeActive();
				edit.Focus();
				edit.SelectAll();
			}
		}

		public void DoDelete()
		{
			//	Supprime la ressource s�lectionn�e.
			int sel = this.array.SelectedRow;
			if ( sel == -1 )  return;

			string name = this.labelsIndex[sel];
			this.module.Modifier.Delete(name);

			this.labelsIndex.RemoveAt(sel);
			this.UpdateArray();

			sel = System.Math.Min(sel, this.labelsIndex.Count-1);
			this.array.SelectedRow = sel;
			this.array.ShowSelectedRow();
			this.UpdateCommands();
			this.module.Modifier.IsDirty = true;
		}

		public void DoDuplicate(bool duplicate)
		{
			//	Duplique la ressource s�lectionn�e.
			int sel = this.array.SelectedRow;
			if ( sel == -1 )  return;

			string name = this.labelsIndex[sel];
			string newName = this.module.Modifier.GetDuplicateName(name);
			this.module.Modifier.Duplicate(name, newName, duplicate);

			int newSel = sel+1;
			this.labelsIndex.Insert(newSel, newName);
			this.UpdateArray();

			this.array.SelectedRow = newSel;
			this.array.ShowSelectedRow();
			this.UpdateCommands();
			this.module.Modifier.IsDirty = true;
		}

		public void DoMove(int direction)
		{
			//	D�place la ressource s�lectionn�e.
			int sel = this.array.SelectedRow;
			if ( sel == -1 )  return;

			string name = this.labelsIndex[sel];
			if ( !this.module.Modifier.Move(name, direction) )  return;
		
			int newSel = sel+direction;
			this.labelsIndex.RemoveAt(sel);
			this.labelsIndex.Insert(newSel, name);
			this.UpdateArray();

			this.array.SelectedRow = newSel;
			this.array.ShowSelectedRow();
			this.UpdateCommands();
			this.module.Modifier.IsDirty = true;
		}

		public void DoNewCulture()
		{
			//	Cr�e une nouvelle culture.
			string name = this.module.MainWindow.DlgNewCulture();
			if ( name == null )  return;
			ResourceBundle bundle = this.module.NewCulture(name);

			this.UpdateCultures();
			this.UpdateSelectedCulture(Misc.CultureName(bundle.Culture));
			this.UpdateArray();
			this.UpdateClientGeometry();
			this.UpdateCommands();
			this.module.Modifier.IsDirty = true;
		}

		public void DoDeleteCulture()
		{
			//	Supprime la culture courante.
			string question = string.Format(Res.Strings.Dialog.DeleteCulture.Question, Misc.CultureName(this.secondaryBundle.Culture));
			Common.Dialogs.DialogResult result = this.module.MainWindow.DialogQuestion(question);
			if ( result != Epsitec.Common.Dialogs.DialogResult.Yes )  return;

			this.module.DeleteCulture(this.secondaryBundle);

			this.UpdateCultures();
			if (this.secondaryBundle != null)
			{
				this.UpdateSelectedCulture(Misc.CultureName(this.secondaryBundle.Culture));
			}
			this.UpdateArray();
			this.UpdateClientGeometry();
			this.UpdateCommands();
			this.module.Modifier.IsDirty = true;
		}

		public void DoClipboard(string name)
		{
			//	Effectue une action avec le bloc-notes.
			if ( this.currentTextField == null )  return;

			if ( name == "Cut" )
			{
				this.currentTextField.ProcessCut();
			}

			if ( name == "Copy" )
			{
				this.currentTextField.ProcessCopy();
			}

			if ( name == "Paste" )
			{
				this.currentTextField.ProcessPaste();
			}
		}

		public void DoFont(string name)
		{
			//	Effectue une modification de typographie.
			if ( this.currentTextField == null )  return;

			if ( name == "FontBold" )
			{
				this.currentTextField.TextNavigator.SelectionBold = !this.currentTextField.TextNavigator.SelectionBold;
			}

			if ( name == "FontItalic" )
			{
				this.currentTextField.TextNavigator.SelectionItalic = !this.currentTextField.TextNavigator.SelectionItalic;
			}

			if ( name == "FontUnderlined" )
			{
				this.currentTextField.TextNavigator.SelectionUnderlined = !this.currentTextField.TextNavigator.SelectionUnderlined;
			}

			this.HandleTextChanged(this.currentTextField);
		}


		public string InfoAccessText
		{
			//	Donne le texte d'information sur l'acc�s en cours.
			get
			{
				System.Text.StringBuilder builder = new System.Text.StringBuilder();

				int sel = this.array.SelectedRow;
				if (sel == -1)
				{
					builder.Append("-");
				}
				else
				{
					builder.Append((sel+1).ToString());
				}

				builder.Append("/");
				builder.Append(this.labelsIndex.Count.ToString());

				if (this.labelsIndex.Count < this.primaryBundle.FieldCount)
				{
					builder.Append(" (");
					builder.Append(this.primaryBundle.FieldCount.ToString());
					builder.Append(")");
				}

				return builder.ToString();
			}
		}


		protected void UpdateCultures()
		{
			//	Met � jour les widgets pour les cultures.
			ResourceBundleCollection bundles = this.module.Bundles;

			if (this.secondaryCultures != null)
			{
				foreach (IconButtonMark button in this.secondaryCultures)
				{
					button.Clicked -= new MessageEventHandler(this.HandleSecondaryCultureClicked);
					button.Dispose();
				}
				this.secondaryCultures = null;
			}

			this.primaryBundle = bundles[ResourceLevel.Default];
			this.primaryCulture.Text = string.Format(Res.Strings.Viewer.Reference, Misc.CultureName(this.primaryBundle.Culture));

			this.secondaryBundle = null;

			if (bundles.Count-1 > 0)
			{
				List<CultureInfo> list = new List<CultureInfo>();

				for (int b=0; b<bundles.Count; b++)
				{
					ResourceBundle bundle = bundles[b];
					if (bundle != this.primaryBundle)
					{
						CultureInfo info = new CultureInfo(bundle.Culture);
						list.Add(info);

						if (this.secondaryBundle == null)
						{
							this.secondaryBundle = bundle;
						}
					}
				}

				list.Sort(Viewer.CompareCultureInfo);
				
				this.secondaryCultures = new IconButtonMark[list.Count];
				for (int i=0; i<list.Count; i++)
				{
					this.secondaryCultures[i] = new IconButtonMark(this);
					this.secondaryCultures[i].ButtonStyle = ButtonStyle.ActivableIcon;
					this.secondaryCultures[i].SiteMark = SiteMark.OnBottom;
					this.secondaryCultures[i].MarkDimension = 5;
					this.secondaryCultures[i].Name = list[i].Name;
					this.secondaryCultures[i].Text = list[i].Name;
					this.secondaryCultures[i].AutoFocus = false;
					this.secondaryCultures[i].Clicked += new MessageEventHandler(this.HandleSecondaryCultureClicked);
					ToolTip.Default.SetToolTip(this.secondaryCultures[i], list[i].Tooltip);
				}
			}

			if (this.secondaryBundle != null)
			{
				this.UpdateSelectedCulture(Misc.CultureName(this.secondaryBundle.Culture));
			}

			this.UpdateLabelsIndex("", Searcher.SearchingMode.None);
		}

		protected void UpdateSelectedCulture(string name)
		{
			//	S�lectionne le widget correspondant � la culture secondaire.
			ResourceBundleCollection bundles = this.module.Bundles;

			this.secondaryBundle = null;
			for (int b=0; b<bundles.Count; b++)
			{
				ResourceBundle bundle = bundles[b];
				if (Misc.CultureName(bundle.Culture) == name)
				{
					this.secondaryBundle = bundle;
				}
			}

			if (this.secondaryCultures == null)  return;

			for (int i=0; i<this.secondaryCultures.Length; i++)
			{
				if (this.secondaryCultures[i].Name == name)
				{
					this.secondaryCultures[i].ActiveState = ActiveState.Yes;
				}
				else
				{
					this.secondaryCultures[i].ActiveState = ActiveState.No;
				}
			}
		}

		protected void UpdateLabelsIndex(string filter, Searcher.SearchingMode mode)
		{
			//	Construit l'index en fonction des ressources primaires.
			this.labelsIndex.Clear();

			foreach (ResourceBundle.Field field in this.primaryBundle.Fields)
			{
				if (filter != "")
				{
					int index = Searcher.IndexOf(field.Name, filter, 0, mode);
					if ( index == -1 )  continue;
					if ( (mode&Searcher.SearchingMode.AtBeginning) != 0 && index != 0 )  continue;
				}

				this.labelsIndex.Add(field.Name);
			}
		}

		protected void UpdateArray()
		{
			//	Met � jour tout le contenu du tableau.
			this.array.TotalRows = this.labelsIndex.Count;

			int first = this.array.FirstVisibleRow;
			for (int i=0; i<this.array.LineCount; i++)
			{
				if (first+i < this.labelsIndex.Count)
				{
					ResourceBundle.Field primaryField   = this.primaryBundle[this.labelsIndex[first+i]];
					ResourceBundle.Field secondaryField = this.secondaryBundle[this.labelsIndex[first+i]];

					this.array.SetLineString(0, first+i, primaryField.Name);
					this.array.SetLineState(0, first+i, MyWidgets.StringList.CellState.Normal);
					this.UpdateArrayField(1, first+i, primaryField, secondaryField);
					this.UpdateArrayField(2, first+i, secondaryField, primaryField);
				}
				else
				{
					this.array.SetLineString(0, first+i, "");
					this.array.SetLineString(1, first+i, "");
					this.array.SetLineString(2, first+i, "");
					this.array.SetLineState(0, first+i, MyWidgets.StringList.CellState.Disabled);
					this.array.SetLineState(1, first+i, MyWidgets.StringList.CellState.Disabled);
					this.array.SetLineState(2, first+i, MyWidgets.StringList.CellState.Disabled);
				}
			}
		}

		protected void UpdateArrayField(int column, int row, ResourceBundle.Field field, ResourceBundle.Field secondaryField)
		{
			if (field != null)
			{
				string text = field.AsString;
				if (text != null && text != "")
				{
					this.array.SetLineString(column, row, text);

					int primaryId = field.ModificationId;
					int secondaryId = primaryId;
					if (secondaryField != null)
					{
						secondaryId = secondaryField.ModificationId;
					}

					if (primaryId < secondaryId)  // �ventuellement pas � jour (fond jaune) ?
					{
						this.array.SetLineState(column, row, MyWidgets.StringList.CellState.Modified);
					}
					else
					{
						this.array.SetLineState(column, row, MyWidgets.StringList.CellState.Normal);
					}

					return;
				}
			}

			this.array.SetLineString(column, row, "");
			this.array.SetLineState(column, row, MyWidgets.StringList.CellState.Warning);
		}

		protected void UpdateEdit()
		{
			//	Met � jour les lignes �ditables en fonction de la s�lection dans le tableau.
			bool iic = this.ignoreChange;
			this.ignoreChange = true;

			int sel = this.array.SelectedRow;
			int column = this.array.SelectedColumn;

			if (sel >= this.labelsIndex.Count)
			{
				sel = -1;
				column = -1;
			}

			if ( sel == -1 )
			{
				this.labelEdit.Enable = false;
				this.primaryEdit.Enable = false;
				this.secondaryEdit.Enable = false;
				this.primaryAbout.Enable = false;
				this.secondaryAbout.Enable = false;

				this.labelEdit.Text = "";
				this.primaryEdit.Text = "";
				this.secondaryEdit.Text = "";
				this.primaryAbout.Text = "";
				this.secondaryAbout.Text = "";
			}
			else
			{
				this.labelEdit.Enable = true;
				this.primaryEdit.Enable = true;
				this.secondaryEdit.Enable = true;
				this.primaryAbout.Enable = true;
				this.secondaryAbout.Enable = true;

				string label = this.labelsIndex[sel];

				this.SetTextField(this.labelEdit, label);

				this.SetTextField(this.primaryEdit, this.primaryBundle[label].AsString);
				this.SetTextField(this.secondaryEdit, this.secondaryBundle[label].AsString);

				this.SetTextField(this.primaryAbout, this.primaryBundle[label].About);
				this.SetTextField(this.secondaryAbout, this.secondaryBundle[label].About);

				AbstractTextField edit = null;
				if (column == 0)  edit = this.labelEdit;
				if (column == 1)  edit = this.primaryEdit;
				if (column == 2)  edit = this.secondaryEdit;
				if (edit != null && edit.Visibility)
				{
					edit.Focus();
					edit.SelectAll();
				}
				else
				{
					this.labelEdit.Cursor = 100000;
					this.primaryEdit.Cursor = 100000;
					this.secondaryEdit.Cursor = 100000;
				}
			}

			this.ignoreChange = iic;

			this.UpdateCommands();
		}

		public void UpdateCommands()
		{
			//	Met � jour les commandes en fonction de la ressource s�lectionn�e.
			int sel = this.array.SelectedRow;
			int count = this.labelsIndex.Count;
			bool build = (this.module.Mode == DesignerMode.Build);

			bool all = false;
			bool modified = false;
			if (sel != -1)
			{
				string label = this.labelsIndex[sel];
				all = this.module.Modifier.IsModificationAll(label);
				if (this.secondaryBundle[label] != null && this.secondaryBundle[label].Name != null)
				{
					modified = (this.primaryBundle[label].ModificationId > this.secondaryBundle[label].ModificationId);
				}
			}

			bool newCulture = (this.module.Bundles.Count < Dialogs.NewCulture.Cultures.Length);

			this.GetCommandState("Save").Enable = this.module.Modifier.IsDirty;
			this.GetCommandState("SaveAs").Enable = true;

			this.GetCommandState("NewCulture").Enable = newCulture;
			this.GetCommandState("DeleteCulture").Enable = true;

			this.GetCommandState("Filter").Enable = true;
			this.GetCommandState("Search").Enable = true;

			this.GetCommandState("AccessFirst").Enable = (sel != -1 && sel > 0);
			this.GetCommandState("AccessPrev").Enable = (sel != -1 && sel > 0);
			this.GetCommandState("AccessLast").Enable = (sel != -1 && sel < count-1);
			this.GetCommandState("AccessNext").Enable = (sel != -1 && sel < count-1);

			this.GetCommandState("ModificationAll").Enable = (sel != -1 && all);
			this.GetCommandState("ModificationClear").Enable = (sel != -1 && modified);

			this.GetCommandState("Delete").Enable = (sel != -1 && build);
			this.GetCommandState("Create").Enable = (sel != -1 && build);
			this.GetCommandState("Duplicate").Enable = (sel != -1 && build);

			this.GetCommandState("Up").Enable = (sel != -1 && sel > 0 && build);
			this.GetCommandState("Down").Enable = (sel != -1 && sel < count-1 && build);

			this.GetCommandState("FontBold").Enable = (sel != -1);
			this.GetCommandState("FontItalic").Enable = (sel != -1);
			this.GetCommandState("FontUnderlined").Enable = (sel != -1);
			this.GetCommandState("Glyphs").Enable = (sel != -1);

			this.module.MainWindow.UpdateInfoCurrentModule();
			this.module.MainWindow.UpdateInfoAccess();
		}

		protected CommandState GetCommandState(string command)
		{
			return this.module.MainWindow.GetCommandState(command);
		}


		protected override void UpdateClientGeometry()
		{
			//	Met � jour la g�om�trie.
			base.UpdateClientGeometry();

			if ( this.primaryCulture == null )  return;

			Rectangle box = this.Client.Bounds;
			box.Deflate(10);
			Rectangle rect;

			int lines = System.Math.Max((int)box.Height/50, 4);
			int editLines = lines*2/3;
			int aboutLines = lines-editLines;
			double cultureHeight = 20;
			double editHeight = editLines*13+8;
			double aboutHeight = aboutLines*13+8;

			//	Il faut obligatoirement s'occuper d'abord de this.array, puisque les autres
			//	widgets d�pendent des largeurs relatives de ses colonnes.
			rect = box;
			rect.Top -= cultureHeight+5;
			rect.Bottom += editHeight+5+aboutHeight+5;
			this.array.Bounds = rect;

			rect = box;
			rect.Bottom = rect.Top-cultureHeight-5;
			rect.Left += this.array.GetColumnsAbsoluteWidth(0);
			rect.Width = this.array.GetColumnsAbsoluteWidth(1)-1;
			this.primaryCulture.Bounds = rect;

			if (this.secondaryCultures != null)
			{
				rect.Left = rect.Right+2;
				rect.Width = this.array.GetColumnsAbsoluteWidth(2);
				double w = System.Math.Floor(rect.Width/this.secondaryCultures.Length);
				for (int i=0; i<this.secondaryCultures.Length; i++)
				{
					Rectangle r = rect;
					r.Left += w*i;
					r.Width = w;
					if (i == this.secondaryCultures.Length-1)
					{
						r.Right = rect.Right;
					}
					this.secondaryCultures[i].Bounds = r;
				}
			}

			rect = box;
			rect.Top = rect.Bottom+editHeight+aboutHeight+5;
			rect.Bottom = rect.Top-editHeight;
			rect.Width = this.array.GetColumnsAbsoluteWidth(0)-5;
			this.labelStatic.Bounds = rect;
			rect.Width += 5+1;
			this.labelEdit.Bounds = rect;
			rect.Left += this.array.GetColumnsAbsoluteWidth(0);
			rect.Width = this.array.GetColumnsAbsoluteWidth(1)+1;
			this.primaryEdit.Bounds = rect;
			rect.Left = rect.Right-1;
			rect.Width = this.array.GetColumnsAbsoluteWidth(2);
			this.secondaryEdit.Bounds = rect;

			rect = box;
			rect.Top = rect.Bottom+aboutHeight;
			rect.Bottom = rect.Top-aboutHeight;
			rect.Width = this.array.GetColumnsAbsoluteWidth(0)-5;
			this.labelAbout.Bounds = rect;
			rect.Left += this.array.GetColumnsAbsoluteWidth(0);
			rect.Width = this.array.GetColumnsAbsoluteWidth(1)+1;
			this.primaryAbout.Bounds = rect;
			rect.Left = rect.Right-1;
			rect.Width = this.array.GetColumnsAbsoluteWidth(2);
			this.secondaryAbout.Bounds = rect;
		}


		protected void SetTextField(AbstractTextField field, string text)
		{
			if (text == null)
			{
				field.Text = "";
			}
			else
			{
				field.Text = text;
			}
		}

		
		void HandleSecondaryCultureClicked(object sender, MessageEventArgs e)
		{
			IconButtonMark button = sender as IconButtonMark;
			this.UpdateSelectedCulture(button.Name);
			this.UpdateArray();
			this.UpdateEdit();
			this.UpdateCommands();
		}

		void HandleArrayColumnsWidthChanged(object sender)
		{
			this.UpdateClientGeometry();
		}

		void HandleArrayCellsQuantityChanged(object sender)
		{
			this.UpdateArray();
		}

		void HandleArrayCellsContentChanged(object sender)
		{
			this.UpdateArray();
		}

		void HandleArraySelectedRowChanged(object sender)
		{
			this.UpdateEdit();
			this.UpdateCommands();
		}

		void HandleTextChanged(object sender)
		{
			if ( this.ignoreChange )  return;

			AbstractTextField edit = sender as AbstractTextField;
			string text = edit.Text;
			int sel = this.array.SelectedRow;
			string label = this.labelsIndex[sel];

			if (edit == this.labelEdit)
			{
				this.labelsIndex[sel] = text;
				this.module.Modifier.Rename(label, text);
				this.array.SetLineString(0, sel, text);
			}

			if (edit == this.primaryEdit)
			{
				this.primaryBundle[label].SetStringValue(text);
				this.UpdateArrayField(1, sel, this.primaryBundle[label], this.secondaryBundle[label]);
				this.UpdateArrayField(2, sel, this.secondaryBundle[label], this.primaryBundle[label]);
			}

			if (edit == this.secondaryEdit)
			{
				this.module.Modifier.CreateIfNecessary(this.secondaryBundle, label, this.primaryBundle[label].ModificationId);
				this.secondaryBundle[label].SetStringValue(text);
				this.UpdateArrayField(1, sel, this.primaryBundle[label], this.secondaryBundle[label]);
				this.UpdateArrayField(2, sel, this.secondaryBundle[label], this.primaryBundle[label]);
			}

			if (edit == this.primaryAbout)
			{
				this.primaryBundle[label].SetAbout(text);
			}

			if (edit == this.secondaryAbout)
			{
				this.module.Modifier.CreateIfNecessary(this.secondaryBundle, label, -1);
				this.secondaryBundle[label].SetAbout(text);
			}

			this.module.Modifier.IsDirty = true;
		}


		void HandleEditKeyboardFocusChanged(object sender, Epsitec.Common.Types.DependencyPropertyChangedEventArgs e)
		{
			bool focused = (bool) e.NewValue;

			if (focused)
			{
				this.currentTextField = sender as AbstractTextField;
			}
		}


		#region CultureInfo
		protected class CultureInfo
		{
			public CultureInfo(System.Globalization.CultureInfo culture)
			{
				this.name = Misc.CultureName(culture);
				this.tooltip = Misc.CultureLongName(culture);
			}

			public string Name
			{
				get
				{
					return this.name;
				}
			}

			public string Tooltip
			{
				get
				{
					return this.tooltip;
				}
			}

			protected string			name;
			protected string			tooltip;
		}

		protected static int CompareCultureInfo(CultureInfo a, CultureInfo b)
		{
			//	Compare deux CultureInfo, afin que les noms soient dans l'ordre alphab�tique.
			return a.Name.CompareTo(b.Name);
		}
		#endregion


		protected Module					module;
		protected List<string>				labelsIndex;
		protected bool						ignoreChange = false;

		protected IconButtonMark			primaryCulture;
		protected IconButtonMark[]			secondaryCultures;
		protected ResourceBundle			primaryBundle;
		protected ResourceBundle			secondaryBundle;
		protected MyWidgets.StringArray		array;
		protected StaticText				labelStatic;
		protected TextFieldMulti			labelEdit;
		protected TextFieldMulti			primaryEdit;
		protected TextFieldMulti			secondaryEdit;
		protected StaticText				labelAbout;
		protected TextFieldMulti			primaryAbout;
		protected TextFieldMulti			secondaryAbout;
		protected AbstractTextField			currentTextField;
	}
}
