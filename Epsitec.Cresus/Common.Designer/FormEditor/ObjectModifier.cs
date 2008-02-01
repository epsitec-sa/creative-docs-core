using System.Collections.Generic;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Widgets.Layouts;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.FormEngine;

namespace Epsitec.Common.Designer.FormEditor
{
	/// <summary>
	/// La classe ObjectModifier permet de g�rer les 'widgets' de Designer.
	/// </summary>
	public class ObjectModifier
	{
		public ObjectModifier(Editor formEditor)
		{
			//	Constructeur unique.
			this.formEditor = formEditor;

			this.tableContent = new List<TableItem>();
		}


		public List<TableItem> TableContent
		{
			get
			{
				return this.tableContent;
			}
		}


		public bool IsField(Widget obj)
		{
			//	Indique si l'objet correspond � un champ.
			FieldDescription field = this.GetFormDescription(obj);
			if (field == null)
			{
				return false;
			}

			return field.Type == FieldDescription.FieldType.Field;
		}

		public bool IsBox(Widget obj)
		{
			//	Indique si l'objet correspond � une bo�te.
			FieldDescription field = this.GetFormDescription(obj);
			if (field == null)
			{
				return false;
			}

			return field.Type == FieldDescription.FieldType.BoxBegin || field.Type == FieldDescription.FieldType.SubForm;
		}

		public bool IsGlue(Widget obj)
		{
			//	Indique si l'objet correspond � de la colle.
			FieldDescription field = this.GetFormDescription(obj);
			if (field == null)
			{
				return false;
			}

			return field.Type == FieldDescription.FieldType.Glue;
		}


		public int GetColumnsRequired(Widget obj)
		{
			//	Retourne le nombre de colonnes requises.
			FieldDescription field = this.GetFormDescription(obj);
			if (field == null)
			{
				return 0;
			}
			else
			{
				return field.ColumnsRequired;
			}
		}

		public void SetColumnsRequired(Widget obj, int columnsRequired)
		{
			//	Choix du nombre de colonnes requises.
			if (this.IsReadonly)
			{
				return;
			}

			FieldDescription field = this.GetFormDescription(obj);
			if (field != null)
			{
				field.ColumnsRequired = columnsRequired;
				this.DeltaUpdate(field);
			}
		}

		public int GetRowsRequired(Widget obj)
		{
			//	Retourne le nombre de lignes requises.
			FieldDescription field = this.GetFormDescription(obj);
			if (field == null)
			{
				return 0;
			}
			else
			{
				return field.RowsRequired;
			}
		}

		public void SetRowsRequired(Widget obj, int rowsRequired)
		{
			//	Choix du nombre de lignes requises.
			if (this.IsReadonly)
			{
				return;
			}

			FieldDescription field = this.GetFormDescription(obj);
			if (field != null)
			{
				field.RowsRequired = rowsRequired;
				this.DeltaUpdate(field);
			}
		}


		public FieldDescription.SeparatorType GetSeparatorBottom(Widget obj)
		{
			//	Retourne le type du s�parateur d'un champ.
			FieldDescription field = this.GetFormDescription(obj);
			if (field == null)
			{
				return FieldDescription.SeparatorType.Normal;
			}
			else
			{
				return field.SeparatorBottom;
			}
		}

		public void SetSeparatorBottom(Widget obj, FieldDescription.SeparatorType sep)
		{
			//	Choix du type du s�parateur d'un champ.
			if (this.IsReadonly)
			{
				return;
			}

			FieldDescription field = this.GetFormDescription(obj);
			if (field != null)
			{
				field.SeparatorBottom = sep;
				this.DeltaUpdate(field);
			}
		}


		public FieldDescription.BoxPaddingType GetBoxPadding(Widget obj)
		{
			//	Retourne le type des marges int�rieures d'une bo�te.
			FieldDescription field = this.GetFormDescription(obj);
			if (field == null)
			{
				return FieldDescription.BoxPaddingType.Normal;
			}
			else
			{
				return field.BoxPadding;
			}
		}

		public void SetBoxPadding(Widget obj, FieldDescription.BoxPaddingType type)
		{
			//	Choix du type des marges int�rieures d'une bo�te.
			if (this.IsReadonly)
			{
				return;
			}

			FieldDescription field = this.GetFormDescription(obj);
			if (field != null)
			{
				field.BoxPadding = type;
				this.DeltaUpdate(field);
			}
		}


		public FieldDescription.BackColorType GetBackColor(Widget obj)
		{
			//	Retourne la couleur de fond d'un champ.
			FieldDescription field = this.GetFormDescription(obj);
			if (field == null)
			{
				return FieldDescription.BackColorType.None;
			}
			else
			{
				return field.BackColor;
			}
		}

		public void SetBackColor(Widget obj, FieldDescription.BackColorType color)
		{
			//	Choix de la couleur de fond d'un champ.
			if (this.IsReadonly)
			{
				return;
			}

			FieldDescription field = this.GetFormDescription(obj);
			if (field != null)
			{
				field.BackColor = color;
				this.DeltaUpdate(field);
			}
		}


		public FrameState GetBoxFrameState(Widget obj)
		{
			//	Retourne la couleur de fond d'un champ.
			FieldDescription field = this.GetFormDescription(obj);
			if (field == null)
			{
				return FrameState.None;
			}
			else
			{
				return field.BoxFrameState;
			}
		}

		public void SetBoxFrameState(Widget obj, FrameState state)
		{
			//	Choix de la couleur de fond d'un champ.
			if (this.IsReadonly)
			{
				return;
			}

			FieldDescription field = this.GetFormDescription(obj);
			if (field != null)
			{
				field.BoxFrameState = state;
				this.DeltaUpdate(field);
			}
		}


		public double GetBoxFrameWidth(Widget obj)
		{
			//	Retourne la couleur de fond d'un champ.
			FieldDescription field = this.GetFormDescription(obj);
			if (field == null)
			{
				return 0.0;
			}
			else
			{
				return (field.BoxFrameWidth+1)/2;
			}
		}

		public void SetBoxFrameWidth(Widget obj, double width)
		{
			//	Choix de la couleur de fond d'un champ.
			if (this.IsReadonly)
			{
				return;
			}

			FieldDescription field = this.GetFormDescription(obj);
			if (field != null)
			{
				field.BoxFrameWidth = 2*width-1;
				this.DeltaUpdate(field);
			}
		}


		protected void DeltaUpdate(FieldDescription field)
		{
			//	Si on est dans un masque delta, le champ modifi� doit �tre copi� dans la liste delta.
			if (this.IsDelta)
			{
				int index = FormEngine.Arrange.IndexOfGuid(this.formEditor.Form.Fields, field.Guid);
				if (index == -1)
				{
					FieldDescription copy = new FieldDescription(field);
					copy.DeltaModified = true;
					this.formEditor.Form.Fields.Add(copy);

					this.formEditor.OnUpdateCommands();  // pour mettre � jour le bouton fieldsButtonReset
				}
				else
				{
					FieldDescription original = this.formEditor.Form.Fields[index];

					FieldDescription copy = new FieldDescription(field);
					copy.DeltaModified = true;
					copy.DeltaMoved = original.DeltaMoved;
					copy.DeltaAttachGuid = original.DeltaAttachGuid;
					this.formEditor.Form.Fields[index] = copy;
				}
			}
		}


		public Widget GetWidget(System.Guid guid)
		{
			//	Cherche le widget correspondant � un Guid.
			foreach (FieldDescription field in this.finalFields)
			{
				if (guid == field.Guid)
				{
					Widget obj = this.GetWidget(this.formEditor.Panel, field.Guid);
					if (obj != null)
					{
						return obj;
					}
				}
			}

			return null;
		}

		protected Widget GetWidget(Widget parent, System.Guid guid)
		{
			if (parent.Name == guid.ToString())
			{
				return parent;
			}

			Widget[] children = parent.Children.Widgets;
			for (int i=0; i<children.Length; i++)
			{
				Widget widget = this.GetWidget(children[i], guid);
				if (widget != null)
				{
					return widget;
				}
			}

			return null;
		}


		public int GetFormCount
		{
			//	Retourne le nombre de champs.
			get
			{
				return this.finalFields.Count;
			}
		}

		public FieldDescription GetFormDescription(TableItem item)
		{
			//	Retourne un champ d'apr�s son TableItem.
			int index = this.GetFormDescriptionIndex(item.Guid);

			if (index == -1)
			{
				index = this.GetFormDescriptionIndex(item.DruidsPath);
			}

			if (index == -1)
			{
				return null;
			}
			else
			{
				return this.finalFields[index];
			}
		}

		public FieldDescription GetFormDescription(Widget obj)
		{
			//	Retourne un champ d'apr�s l'identificateur unique d'un widget.
			int index = this.GetFormDescriptionIndex(obj);
			if (index == -1)
			{
				return null;
			}
			else
			{
				return this.finalFields[index];
			}
		}

		public int GetFormDescriptionIndex(Widget obj)
		{
			//	Retourne l'index d'un champ d'apr�s l'identificateur unique d'un widget.
			if (string.IsNullOrEmpty(obj.Name))
			{
				return -1;
			}

			System.Guid guid = new System.Guid(obj.Name);
			return this.GetFormDescriptionIndex(guid);
		}

		public int GetFormDescriptionIndex(System.Guid guid)
		{
			//	Retourne l'index d'un champ d'apr�s le Guid.
			for (int i=0; i<this.finalFields.Count; i++)
			{
				FieldDescription field = this.finalFields[i];

				if (field.Guid == guid)
				{
					return i;
				}
			}

			return -1;
		}

		protected int GetFormDescriptionIndex(string druidsPath)
		{
			//	Retourne l'index d'un champ d'apr�s le chemin de Druis.
			for (int i=0; i<this.finalFields.Count; i++)
			{
				FieldDescription field = this.finalFields[i];

				if (field.GetPath(null) == druidsPath)
				{
					return i;
				}
			}

			return -1;
		}


		public Rectangle GetActualBounds(Widget obj)
		{
			//	Retourne la position et les dimensions actuelles de l'objet.
			Rectangle bounds = obj.Client.Bounds;
			bounds = this.AdjustBounds(obj, bounds);

			while (obj != null && obj != this.formEditor.Panel)
			{
				bounds = obj.MapClientToParent(bounds);
				obj = obj.Parent;
			}

			return bounds;
		}

		public Rectangle AdjustBounds(Widget obj, Rectangle bounds)
		{
			//	Retourne le rectangle ajust� d'un objet.
			if (obj.Index == FormEngine.Engine.GlueNull)  // objet FieldDescription.FieldType.Glue avec un ColumnRequired = 0 ?
			{
				bounds.Width = 0;  // fait croire � un trait vertical cal� � gauche
			}

			return bounds;
		}

		public Rectangle InflateMinimalSize(Rectangle rect)
		{
			//	Retourne le rectangle aggrandi pour avoir au moins la taille minimale.
			double ix = 0;
			if (rect.Width < this.formEditor.Context.MinimalSize)
			{
				ix = this.formEditor.Context.MinimalSize;
			}

			double iy = 0;
			if (rect.Height < this.formEditor.Context.MinimalSize)
			{
				iy = this.formEditor.Context.MinimalSize;
			}

			rect.Inflate(ix, iy);
			return rect;
		}


		public bool IsReadonly
		{
			get
			{
				return this.formEditor.Module.DesignerApplication.IsReadonly;
			}
		}


		#region TableContent
		public void FormDeltaMove(int index, int direction)
		{
			//	D�place un �l�ment en le montant ou en le descendant d'une ligne. Pour cela, les d�placements
			//	de tous les �l�ments sont recalcul�s, � partir de la situation initiale.
			//	this.baseFields				= liste initiale
			//	this.formEditor.Form.Fields	= liste delta
			//	this.finalFields			= liste r�sultante

			//	Construit la liste originale.
			List<System.Guid> original = new List<System.Guid>();
			foreach (FieldDescription field in this.baseFields)
			{
				original.Add(field.Guid);
			}

			//	Construit la liste finale souhait�e.
			List<System.Guid> final = new List<System.Guid>();
			foreach (FieldDescription field in this.finalFields)
			{
				final.Add(field.Guid);
			}
			System.Guid guid = final[index];
			final.RemoveAt(index);
			final.Insert(index+direction, guid);  // effectue le mouvement (monter ou descendre un �l�ment)

			//	Construit la liste de tous les �l�ments d�plac�s.
			List<System.Guid> moved = new List<System.Guid>();

			moved.Add(guid);  // ce n'est pas grave si cet �l�ment est 2x dans la liste

			foreach (FieldDescription field in this.formEditor.Form.Fields)
			{
				if (field.DeltaMoved || field.DeltaInserted)
				{
					moved.Add(field.Guid);
				}
			}

			//	Si l'�l�ment d�plac� est cass�, enl�ve imm�diatement son �tat cass�.
			index = FormEngine.Arrange.IndexOfGuid(this.formEditor.Form.Fields, guid);
			if (index != -1)
			{
				this.formEditor.Form.Fields[index].DeltaBrokenAttach = false;
			}

			//	G�n�re la liste des op�rations de d�placement n�cessaires.
			//	1) Traite d'abord les �l�ments qu'on sait devoir d�placer (selon la liste moved).
			List<LinkAfter> oper = new List<LinkAfter>();
			index = 0;
			while (index < final.Count)
			{
				if (moved.Contains(final[index]) && (index >= original.Count || original[index] != final[index]))
				{
					System.Guid element = final[index];  // �l�ment � d�placer
					System.Guid link = (index==0) ? System.Guid.Empty : final[index-1];  // *apr�s* cet �l�ment
					bool isInserted = !original.Contains(element);  // true = nouvel �l�ment, false = �l�ment existant
					oper.Insert(0, new LinkAfter(element, link, isInserted));  // ins�re au d�but

					final.RemoveAt(index);  // supprime l'�l�ment d�plac�

					int i = original.IndexOf(element);
					if (i != -1)
					{
						original.RemoveAt(i);  // supprime l'�l�ment d�plac�
					}
				}
				else
				{
					index++;
				}
			}

			//	2) Traite ensuite les �ventuels autres �l�ments.
			index = 0;
			while (index < final.Count)
			{
				if (index < original.Count && final[index] == original[index])  // �l�ment en place ?
				{
					index++;  // passe au suivant
				}
				else  // �l�ment d�plac� ?
				{
					System.Guid element = final[index];  // �l�ment � d�placer
					System.Guid link = (index==0) ? System.Guid.Empty : final[index-1];  // *apr�s* cet �l�ment
					bool isInserted = !original.Contains(element);  // true = nouvel �l�ment, false = �l�ment existant
					oper.Insert(0, new LinkAfter(element, link, isInserted));  // ins�re au d�but

					final.RemoveAt(index);  // supprime l'�l�ment d�plac�

					int i = original.IndexOf(element);
					if (i != -1)
					{
						original.RemoveAt(i);  // supprime l'�l�ment d�plac�
					}
				}
			}

			//	Supprime tous les d�placements dans la liste delta actuelle, mais en conservant les �l�ments.
			foreach (FieldDescription field in this.formEditor.Form.Fields)
			{
				field.DeltaMoved = false;
				field.DeltaInserted = false;
			}

			//	Remet tous les d�placements selon la liste des op�rations.
			foreach (LinkAfter item in oper)
			{
				int i = FormEngine.Arrange.IndexOfGuid(this.formEditor.Form.Fields, item.Element);
				if (i == -1)  // �l�ment pas (ou pas encore) adapt� ?
				{
					i = FormEngine.Arrange.IndexOfGuid(this.baseFields, item.Element);
					if (i != -1)  // �l�ment dans la liste de r�f�rence ?
					{
						FieldDescription field = new FieldDescription(this.baseFields[i]);  // copie de l'�l�ment

						field.DeltaMoved = !item.IsInserted;
						field.DeltaInserted = item.IsInserted;
						field.DeltaAttachGuid = item.Link;

						this.formEditor.Form.Fields.Add(field);  // met l'�l�ment � la fin de la liste delta
					}
				}
				else  // �l�ment d�j� dans la liste delta ?
				{
					FieldDescription field = this.formEditor.Form.Fields[i];
					this.formEditor.Form.Fields.RemoveAt(i);  // supprime l'�l�ment dans la liste delta

					field.DeltaMoved = !item.IsInserted;
					field.DeltaInserted = item.IsInserted;
					field.DeltaAttachGuid = item.Link;

					this.formEditor.Form.Fields.Add(field);  // remet l'�l�ment � la fin de la liste delta
				}
			}

			//	Supprime r�ellement tous les �l�ments dans la liste delta qui n'ont plus aucune fonction.
			index = 0;
			while (index < this.formEditor.Form.Fields.Count)
			{
				FieldDescription field = this.formEditor.Form.Fields[index];

				if (!field.DeltaMoved && !field.DeltaInserted && !field.DeltaModified && !field.DeltaHidden && !field.DeltaBrokenAttach)
				{
					this.formEditor.Form.Fields.RemoveAt(index);
				}
				else
				{
					index++;
				}
			}
		}

		protected struct LinkAfter
		{
			public LinkAfter(System.Guid element, System.Guid link, bool isInserted)
			{
				this.Element = element;
				this.Link = link;
				this.IsInserted = isInserted;
			}

			public System.Guid Element;  // �l�ment � d�placer
			public System.Guid Link;  // *apr�s* cet �l�ment
			public bool IsInserted;  // true = nouvel �l�ment, false = �l�ment existant 
		}

		public string GetTableContentDescription(TableItem item, bool isImage, bool isShowPrefix, bool isShowGuid)
		{
			//	Retourne le texte permettant de d�crire un TableItem dans une liste, avec un effet
			//	d'indentation pour ressembler aux arborescences de Vista.
			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			if (isImage)
			{
				for (int i=0; i<item.Level; i++)
				{
					builder.Append(Misc.Image("TreeSpace"));
				}

				if (item.FieldType == FieldDescription.FieldType.BoxBegin)
				{
					builder.Append(Misc.Image("TreeBranch"));
				}
				else
				{
					builder.Append(Misc.Image("TreeMark"));
				}

				builder.Append(" ");
			}

			string name = this.formEditor.Module.AccessFields.GetFieldNames(item.DruidsPath);
			if (name == null)
			{
				FieldDescription field = this.formEditor.ObjectModifier.GetFormDescription(item);
				if (field != null)
				{
					if (field.Type == FieldDescription.FieldType.BoxBegin ||
						field.Type == FieldDescription.FieldType.SubForm)
					{
						name = Misc.Bold(field.Description);
					}
					else
					{
						name = Misc.Italic(field.Description);
					}
				}
			}
			else
			{
				if (item.Prefix != null && isShowPrefix)
				{
					name = string.Concat(item.Prefix, name);
				}

				if (isShowGuid)
				{
					name = string.Concat(item.Guid.ToString(), " ", name);
				}
			}

			builder.Append(name);

			return builder.ToString();
		}

		public string GetTableContentIcon1(TableItem item)
		{
			//	Retourne le texte permettant de d�crire l'op�ration delta d'un TableItem dans une liste.
			string icon = null;

			if (this.IsDelta)
			{
				int index = FormEngine.Arrange.IndexOfGuid(this.formEditor.Form.Fields, item.Guid);
				if (index != -1)
				{
					if (this.formEditor.Form.Fields[index].DeltaHidden)
					{
						icon = Misc.Image("FormDeltaHidden");  // peu prioritaire � cause du fond rouge
					}

					if (this.formEditor.Form.Fields[index].DeltaInserted)
					{
						icon = Misc.Image("FormDeltaInserted");  // peu prioritaire � cause du fond vert
					}

					if (this.formEditor.Form.Fields[index].DeltaMoved)
					{
						icon = Misc.Image("FormDeltaMoved");  // prioritaire, car pas de fond color�
					}
				}
			}

			return icon;
		}

		public string GetTableContentIcon2(TableItem item)
		{
			//	Retourne le texte permettant de d�crire la relation d'un TableItem dans une liste.
			string icon = null;

			if (item.FieldType == FieldDescription.FieldType.SubForm)
			{
				icon = Misc.Image("TreeSubForm");
			}

			return icon;
		}

		public Color GetTableContentUseColor(TableItem item)
		{
			//	Retourne la couleur d�crivant un TableItem dans une liste.
			Color color = Color.Empty;

			if (this.IsDelta)
			{
				int index = FormEngine.Arrange.IndexOfGuid(this.formEditor.Form.Fields, item.Guid);
				if (index != -1)
				{
					if (this.formEditor.Form.Fields[index].DeltaHidden)
					{
						color = Color.FromAlphaRgb(0.3, 1, 0, 0);  // rouge = champ cach�
					}

					if (this.formEditor.Form.Fields[index].DeltaModified)
					{
						color = Color.FromAlphaRgb(0.3, 1, 1, 0);  // jaune = champ modifi�
					}

					if (this.formEditor.Form.Fields[index].DeltaInserted)
					{
						color = Color.FromAlphaRgb(0.3, 0, 1, 0);  // vert = champ ins�r�
					}


					if (this.formEditor.Form.Fields[index].DeltaBrokenAttach)
					{
						color = Color.FromAlphaRgb(0.8, 1, 0, 0);  // rouge fonc� = lien cass�
					}
				}
			}

			return color;
		}

		public bool IsDelta
		{
			//	Indique si l'on est dans un masque delta.
			get
			{
				if (this.formEditor.Form == null)
				{
					return false;
				}
				else
				{
					return this.formEditor.Form.IsDelta;
				}
			}
		}

		public void UpdateTableContent()
		{
			//	Met � jour la liste qui refl�te le contenu de la table des champs, visible en haut � droite.
			if (this.formEditor.Form == null)
			{
				return;
			}

			if (this.IsDelta)  // masque delta ?
			{
				FormEngine.FormDescription refForm = this.formEditor.Module.AccessForms.GetForm(this.formEditor.Form.DeltaBaseFormId);
				this.baseFields = refForm.Fields;

				FormEngine.Engine engine = new FormEngine.Engine(this.formEditor.Module.FormResourceProvider);
				this.finalFields = engine.Arrange.Merge(this.baseFields, this.formEditor.Form.Fields);
			}
			else  // masque normal ?
			{
				this.finalFields = this.formEditor.Form.Fields;
				this.baseFields = null;
			}

			this.tableContent.Clear();

			//	Construit la liste des chemins de Druids, en commen�ant par ceux qui font
			//	partie du masque de saisie.
			int level = 0;
			foreach (FieldDescription field in this.finalFields)
			{
				string prefix = null;
				if (field.FieldIds != null)
				{
					Druid druid = field.FieldIds[0];
					Module module = this.formEditor.Module.DesignerApplication.SearchModule(druid);
					if (module != null)
					{
						CultureMap cultureMap = module.AccessFields.Accessor.Collection[druid];
						if (cultureMap != null)
						{
							prefix = string.Concat(module.ModuleInfo.SourceNamespace, ".", cultureMap.Prefix, ".");
						}
					}
				}

				TableItem item = new TableItem();
				item.Guid = field.Guid;
				item.FieldType = field.Type;
				item.SourceFieldType = field.SourceType;
				item.Prefix = prefix;
				item.DruidsPath = field.GetPath(null);
				item.Level = level;

				this.tableContent.Add(item);

				if (field.Type == FieldDescription.FieldType.BoxBegin)
				{
					level++;
				}

				if (field.Type == FieldDescription.FieldType.BoxEnd)
				{
					level--;
				}
			}
		}

		public int GetTableContentIndex(System.Guid guid)
		{
			//	Cherche l'index d'un Guid dans la table des champs.
			for (int i=0; i<this.tableContent.Count; i++)
			{
				if (guid == this.tableContent[i].Guid)
				{
					return i;
				}
			}

			return -1;
		}

		protected int GetTableContentIndex(string druidsPath)
		{
			//	Cherche l'index d'un chemin de Druids dans la table des champs.
			for (int i=0; i<this.tableContent.Count; i++)
			{
				if (druidsPath == this.tableContent[i].DruidsPath)
				{
					return i;
				}
			}

			return -1;
		}

		public struct TableItem
		{
			//	Cette structure repr�sente un �l�ment dans la liste de droite des champs.
			public System.Guid					Guid;
			public FieldDescription.FieldType	FieldType;
			public FieldDescription.FieldType	SourceFieldType;
			public string						Prefix;
			public string						DruidsPath;
			public int							Level;
		}
		#endregion


		#region TableRelation
		public List<RelationItem> TableRelations
		{
			//	Retourne la liste pour la table des relations. Chaque RelationItem correspondra � une ligne dans la table.
			get
			{
				return this.tableRelations;
			}
		}

		public string GetTableRelationDescription(int index)
		{
			//	Retourne le texte permettant de d�crire une relation dans une liste, avec un effet
			//	d'indentation pour ressembler aux arborescences de Vista.
			string druidsPath = this.tableRelations[index].DruidsPath;
			string name = this.formEditor.Module.AccessFields.GetFieldNames(druidsPath);

			string nextDruidsPath = null;
			if (index+1 < this.tableRelations.Count)
			{
				nextDruidsPath = this.tableRelations[index+1].DruidsPath;
			}

			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			for (int i=0; i<this.tableRelations[index].Level; i++)
			{
				builder.Append(Misc.Image("TreeSpace"));
			}

			if (this.tableRelations[index].Expandable)
			{
				if (this.tableRelations[index].Expanded)
				{
					builder.Append(Misc.Image("TreeBranch"));
					name = Misc.Bold(name);
				}
				else
				{
					builder.Append(Misc.Image("TreeCompact"));
				}
			}
			else
			{
				//?builder.Append(Misc.Image("TreeMark"));
				builder.Append(Misc.Image("TreeSpace"));
			}

			builder.Append(" ");
			builder.Append(name);

			return builder.ToString();
		}

		public string GetTableRelationRelIcon(int index)
		{
			//	Retourne le texte "ic�ne" permettant de d�crire une relation dans une liste.
			FieldRelation rel = this.tableRelations[index].Relation;

			string icon = null;

			if (rel == FieldRelation.Reference)
			{
				icon = Misc.Image("TreeRelationReference");
			}
			else if (rel == FieldRelation.Collection)
			{
				icon = Misc.Image("TreeRelationCollection");
			}

			return icon;
		}

		public string GetTableRelationUseIcon(int index)
		{
			//	Retourne le texte "ic�ne" permettant de d�crire l'utilisation dans une liste.
			string icon = null;

			if (this.IsTableRelationUsed(index))
			{
				icon = Misc.Image("ActiveYes");
			}
			else
			{
				icon = Misc.Image("ActiveNo");
			}

			return icon;
		}

		public Color GetTableRelationUseColor(int index)
		{
			//	Retourne la couleur d�crivant l'utilisation d'une relation.
			if (this.IsTableRelationUsed(index))
			{
				return Color.FromAlphaRgb(0.3, 0, 1, 0);  // vert = champ utilis�
			}
			else
			{
				return Color.FromAlphaRgb(0.3, 1, 0, 0);  // rouge = champ inutilis�
			}
		}

		public bool IsTableRelationUseable(int index)
		{
			//	Indique si l'op�ration "utiliser" est autoris�e.
			if (index == -1 || this.IsReadonly)
			{
				return false;
			}

			return !this.IsTableRelationUsed(index);
		}

		public bool IsTableRelationExpandable(int index)
		{
			//	Indique si l'op�ration "�tendre" est autoris�e.
			if (index == -1 || this.IsReadonly)
			{
				return false;
			}

			return this.tableRelations[index].Expandable && !this.tableRelations[index].Expanded;
		}

		public bool IsTableRelationCompactable(int index)
		{
			//	Indique si l'op�ration "compacter" est autoris�e.
			if (index == -1 || this.IsReadonly)
			{
				return false;
			}

			return this.tableRelations[index].Expanded;
		}

		public void TableRelationExpand(int index)
		{
			//	Etend une relation.
			string druidsPath = this.tableRelations[index].DruidsPath;
			IList<StructuredData> dataFields = this.TableRelationSearchStructuredData(druidsPath);

			this.tableRelations[index].Expanded = true;
			int level = this.tableRelations[index].Level + 1;

			foreach (StructuredData dataField in dataFields)
			{
				FieldRelation rel = (FieldRelation) dataField.GetValue(Support.Res.Fields.Field.Relation);
				Druid fieldCaptionId = (Druid) dataField.GetValue(Support.Res.Fields.Field.CaptionId);
				Druid typeId = (Druid) dataField.GetValue(Support.Res.Fields.Field.TypeId);

				RelationItem item = new RelationItem();
				item.DruidsPath = string.Concat(druidsPath, ".", fieldCaptionId.ToString());
				item.typeId = typeId;
				item.Relation = rel;
				item.Expandable = (rel != FieldRelation.None);
				item.Expanded = false;
				item.Level = level;

				index++;
				this.tableRelations.Insert(index, item);
			}
		}

		public void TableRelationCompact(int index)
		{
			//	Compacte une relation.
			string druidsPath = this.tableRelations[index].DruidsPath;
			IList<StructuredData> dataFields = this.TableRelationSearchStructuredData(druidsPath);

			this.tableRelations[index].Expanded = false;

			while (index+1 < this.tableRelations.Count)
			{
				if (this.tableRelations[index+1].DruidsPath.StartsWith(this.tableRelations[index].DruidsPath))
				{
					this.tableRelations.RemoveAt(index+1);
				}
				else
				{
					break;
				}
			}
		}

		public void UpdateTableRelation(Druid entityId, IList<StructuredData> entityFields, FormDescription form)
		{
			//	Initialise la table des relations possibles avec le premier niveau et les champs du masque.
			//	Tous les champs fr�res d'un champ faisant partie du masque sont �galement pris.
			this.UpdateTableRelation(entityId, entityFields);

			foreach (FieldDescription field in form.Fields)
			{
				if (field.Type != FieldDescription.FieldType.Field &&
					field.Type != FieldDescription.FieldType.SubForm)
				{
					continue;
				}

				string druidsPath = field.GetPath(null);
				int deep = Misc.DruidsPathLevel(druidsPath)-1;

				int existingIndex = this.TableRelationSearchIndex(Misc.DruidsPathPart(druidsPath, deep));
				if (existingIndex != -1 && this.tableRelations[existingIndex].Expanded)
				{
					continue;
				}

				int first = -1;
				for (int i=deep; i>0; i--)
				{
					string subDruidsPath = Misc.DruidsPathPart(druidsPath, i);
					int index = this.TableRelationSearchIndex(subDruidsPath);
					if (index != -1)
					{
						first = i;
					}
				}

				if (first == -1)
				{
					continue;
				}

				for (int i=first; i<=deep; i++)
				{
					string subDruidsPath = Misc.DruidsPathPart(druidsPath, i);
					int index = this.TableRelationSearchIndex(subDruidsPath);

					if (this.tableRelations[index].Expanded)
					{
						continue;
					}

					this.tableRelations[index].Expanded = true;

					IList<StructuredData> dataFields = this.TableRelationSearchStructuredData(subDruidsPath);
					foreach (StructuredData dataField in dataFields)
					{
						FieldRelation rel = (FieldRelation) dataField.GetValue(Support.Res.Fields.Field.Relation);
						Druid fieldCaptionId = (Druid) dataField.GetValue(Support.Res.Fields.Field.CaptionId);
						Druid typeId = (Druid) dataField.GetValue(Support.Res.Fields.Field.TypeId);

						RelationItem item = new RelationItem();
						item.DruidsPath = string.Concat(subDruidsPath, ".", fieldCaptionId.ToString());
						item.typeId = typeId;
						item.Relation = rel;
						item.Expandable = (rel != FieldRelation.None);
						item.Expanded = false;
						item.Level = i;

						this.tableRelations.Insert(++index, item);
					}
				}
			}
		}

		public void UpdateTableRelation(Druid entityId, IList<StructuredData> entityFields)
		{
			//	Initialise la table des relations possibles avec le premier niveau
			//	(mais seulement les champs sans relation).
			this.entityId = entityId;

			this.tableRelations = new List<RelationItem>();

			if (entityFields == null)
			{
				return;
			}

			foreach (StructuredData dataField in entityFields)
			{
				FieldRelation rel = (FieldRelation) dataField.GetValue(Support.Res.Fields.Field.Relation);
				Druid fieldCaptionId = (Druid) dataField.GetValue(Support.Res.Fields.Field.CaptionId);
				Druid typeId = (Druid) dataField.GetValue(Support.Res.Fields.Field.TypeId);

				RelationItem item = new RelationItem();
				item.DruidsPath = fieldCaptionId.ToString();
				item.typeId = typeId;
				item.Relation = rel;
				item.Expandable = (rel != FieldRelation.None);
				item.Expanded = false;
				item.Level = 0;

				this.tableRelations.Add(item);
			}
		}

		protected bool IsTableRelationUsed(int index)
		{
			//	Indique si une relation est utilis�e dans le masque de saisie.
			string druidsPath = this.tableRelations[index].DruidsPath;

			foreach (FieldDescription field in this.finalFields)
			{
				if (field.GetPath(null) == druidsPath)
				{
					return true;
				}
			}

			return false;
		}

		protected int TableRelationSearchIndex(string druidsPath)
		{
			//	Cherche l'index correspondant � un chemin de Druids.
			for (int i=0; i<this.tableRelations.Count; i++)
			{
				if (this.tableRelations[i].DruidsPath == druidsPath)
				{
					return i;
				}
			}

			return -1;
		}

		protected IList<StructuredData> TableRelationSearchStructuredData(string druidsPath)
		{
			string[] druids = druidsPath.Split('.');

			IList<StructuredData> dataFields = this.formEditor.Module.AccessEntities.GetEntityDruidsPath(this.entityId);
			foreach (string druid in druids)
			{
				StructuredData dataField = this.TableRelationSearchStructuredData(dataFields, Druid.Parse(druid));
				Druid typeId = (Druid) dataField.GetValue(Support.Res.Fields.Field.TypeId);
				Module typeModule = this.formEditor.Module.DesignerApplication.SearchModule(typeId);
				if (typeModule != null)
				{
					CultureMap typeCultureMap = typeModule.AccessEntities.Accessor.Collection[typeId];
					if (typeCultureMap != null)
					{
						StructuredData typeData = typeCultureMap.GetCultureData(Resources.DefaultTwoLetterISOLanguageName);
						dataFields = typeData.GetValue(Support.Res.Fields.ResourceStructuredType.Fields) as IList<StructuredData>;
					}
				}
			}

			return dataFields;
		}

		protected StructuredData TableRelationSearchStructuredData(IList<StructuredData> dataFields, Druid fieldId)
		{
			foreach (StructuredData dataField in dataFields)
			{
				Druid fieldCaptionId = (Druid) dataField.GetValue(Support.Res.Fields.Field.CaptionId);
				if (fieldCaptionId == fieldId)
				{
					return dataField;
				}
			}

			return null;
		}

		public class RelationItem
		{
			//	Cette classe repr�sente une ligne dans la table des relations.
			public string						DruidsPath;
			public Druid						typeId;
			public FieldRelation				Relation;
			public bool							Expandable;
			public bool							Expanded;
			public int							Level;
		}
		#endregion


		protected Editor							formEditor;
		protected Druid								entityId;
		protected List<FieldDescription>			baseFields;
		protected List<FieldDescription>			finalFields;
		protected List<TableItem>					tableContent;
		protected List<RelationItem>				tableRelations;
	}
}
