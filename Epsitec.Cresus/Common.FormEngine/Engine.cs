using System.Collections.Generic;

using Epsitec.Common.Drawing;
using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;

namespace Epsitec.Common.FormEngine
{
	public delegate FormDescription FormDescriptionFinder(Druid id);


	/// <summary>
	/// G�n�rateur de masques de saisie.
	/// </summary>
	public sealed class Engine
	{
		public Engine(IFormResourceProvider resourceProvider)
		{
			//	Constructeur.
			//	FindFormDescription permet de retrouver le FormDescription correspondant � un Druid,
			//	lorsque les ressources ne sont pas s�rialis�es. Pour un usage hors de Designer, avec
			//	des ressources s�rialis�es, ce param�tre peut �tre null.
			this.resourceProvider = resourceProvider;

			this.arrange = new Arrange(this.resourceProvider);
			this.entityContext = new EntityContext(this.resourceProvider, EntityLoopHandlingMode.Skip);
			this.defaultMode = FieldEditionMode.Data;
		}

		public Arrange Arrange
		{
			get
			{
				return this.arrange;
			}
		}

		public void EnableSearchMode()
		{
			this.defaultMode = FieldEditionMode.Search;
		}

		public UI.Panel CreateForm(Druid formId, ref Size defaultSize)
		{
			//	Cr�e un masque de saisie.
			//	Si le Druid correspond � un Form delta, il est fusionn� jusqu'au Form de base parent.
			//	Cette m�thode est utilis�e par une application finale pour construire un masque.
			string xml = this.resourceProvider.GetFormXmlSource(formId);
			
			if (string.IsNullOrEmpty(xml))
			{
				return null;
			}

			FormDescription formDescription = Serialization.DeserializeForm(xml);

			if (!double.IsNaN(formDescription.DefaultSize.Width))
			{
				defaultSize.Width = formDescription.DefaultSize.Width;
			}
			if (!double.IsNaN(formDescription.DefaultSize.Height))
			{
				defaultSize.Height = formDescription.DefaultSize.Height;
			}

			return this.CreateForm(formDescription);
		}

		public UI.Panel CreateForm(FormDescription formDescription)
		{
			if (formDescription == null)
			{
				return null;
			}

			List<FieldDescription> baseFields, finalFields;
			Druid entityId;
			this.arrange.Build(formDescription, null, out baseFields, out finalFields, out entityId);

			UI.Panel panel = this.CreateForm(finalFields, entityId, false);
			return panel;
		}

		public UI.Panel CreateForm(List<FieldDescription> fields, Druid entityId, bool forDesigner)
		{
			//	Cr�e un masque de saisie.
			//	La liste de FieldDescription doit �tre plate (pas de Node).
			//	Cette m�thode est utilis�e par Designer pour construire un masque.
			this.forDesigner = forDesigner;
			this.resourceProvider.ClearCache();

			string err = this.arrange.Check(fields);
			if (err != null)
			{
				UI.Panel container = new UI.Panel();

				StaticText warning = new StaticText(container);
				warning.Text = string.Concat("<i>", err, "</i>");
				warning.ContentAlignment = ContentAlignment.MiddleCenter;
				warning.Dock = DockStyle.Fill;

				return container;
			}

			List<FieldDescription> fields1 = this.arrange.DevelopSubForm(fields);
			List<FieldDescription> fields2 = this.arrange.Organize(fields1);

			if (this.GetEntityDefinition(entityId) == null)
			{
				return null;
			}

			AbstractEntity entityData = null;

			EntityContext.Push(this.entityContext);
			
			try
			{
				entityData = entityContext.CreateEntity(entityId);
			}
			finally
			{
				EntityContext.Pop();
			}

			//	Cr�e le panneau racine, le seul � d�finir DataSource. Les autres panneaux
			//	enfants h�ritent de cette propri�t�.
			UI.Panel root = new UI.Panel();
			root.ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow;
			root.CaptionResolver = this.resourceProvider;
			root.DataSource = new UI.DataSource();
			root.DataSource.AddDataSource(UI.DataSource.DataName, entityData);

#if true
			//	Cr�e un gestionnaire de styles pour le panneau dans son entier; un tel
			//	gestionnaire doit �tre attach� au panneau racine au moment de sa cr�ation
			UI.TextStyleManager textStyleManager = new Epsitec.Common.UI.TextStyleManager(root);

			//	Cr�e un style pour les labels :
			TextStyle staticTextStyle = new TextStyle();
			staticTextStyle.FontSize = 14.0;
			staticTextStyle.FontColor = Color.FromRgb(0, 0, 0.4);
			textStyleManager.StaticTextStyle = staticTextStyle;
			
			//	Cr�e un style pour les champs �ditables :
			TextStyle textFieldStyle = new TextStyle ();
			textFieldStyle.FontSize = 14.0;
			textFieldStyle.FontColor = Color.FromRgb(0.8, 0, 0);
			textFieldStyle.Font = Font.GetFont("Calibri", "Regular");
			textStyleManager.TextFieldStyle = textFieldStyle;

			//	Active les styles pour le panneau sp�cifi�, et tous ses enfants !
			textStyleManager.Attach (root);
#endif

			this.CreateFormBox(root, entityId, fields2, 0);

			return root;
		}

		private StructuredType GetEntityDefinition(Druid entityId)
		{
			//	Trouve la d�finition de l'entit� sp�cifi�e par son id.
			return this.resourceProvider.GetStructuredType(entityId);
		}

		private enum FieldEditionMode
		{
			Unknown,
			Data,							//	le champ contient des donn�es
			Search							//	le champ sert � r�aliser des recherches
		}

		private FieldEditionMode GetFieldEditionMode(Druid entityId, IList<Druid> fieldIds)
		{
			//	D�termine comment un champ doit �tre trait�. Il peut soit �tre
			//	consid�r� comme une donn�e, soit comme un crit�re de recherche.
			foreach (Druid fieldId in fieldIds)
			{
				StructuredType entityDef = this.GetEntityDefinition(entityId);
				if (entityDef == null)
				{
					return FieldEditionMode.Unknown;
				}

				StructuredTypeField fieldDef = entityDef.GetField(fieldId.ToString());
				if (fieldDef == null)
				{
					return FieldEditionMode.Unknown;
				}

				if (fieldDef.Relation == FieldRelation.None)
				{
					return this.defaultMode;
				}

				if (fieldDef.IsSharedRelation)
				{
					return FieldEditionMode.Search;
				}
				
				entityId = fieldDef.TypeId;
			}

			return this.defaultMode;
		}

		private void CreateFormBox(UI.Panel root, Druid entityId, List<FieldDescription> fields, int index)
		{
			//	Cr�e tous les champs dans une bo�te.
			//	Cette m�thode est appel�e r�cursivement pour chaque BoxBegin/BoxEnd.

			//	Premi�re passe pour d�terminer quelles colonnes contiennent des labels.
			int column = 0, row = 0;
			int level = 0;
			List<int> labelsId = new List<int>();
			int labelId = 1;
			for (int i=index; i<fields.Count; i++)
			{
				FieldDescription field = fields[i];

				if (field.DeltaHidden)
				{
					continue;
				}

				bool isGlueAfter = false;
				FieldDescription nextField = Engine.SearchNextElement(fields, i);
				if (nextField != null && nextField.Type == FieldDescription.FieldType.Glue)
				{
					isGlueAfter = true;
				}

				if (field.Type == FieldDescription.FieldType.BoxBegin ||  // d�but de bo�te ?
					field.Type == FieldDescription.FieldType.SubForm)
				{
					if (level == 0)
					{
						this.PreprocessBoxBegin(field, labelsId, ref labelId, ref column, isGlueAfter);
					}

					level++;
				}
				else if (field.Type == FieldDescription.FieldType.BoxEnd)  // fin de bo�te ?
				{
					level--;

					if (level < 0)
					{
						break;
					}
				}
				else if (field.Type == FieldDescription.FieldType.Field)  // champ ?
				{
					if (level == 0)
					{
						this.PreprocessField(field, labelsId, ref labelId, ref column, isGlueAfter);
					}
				}
				else if (field.Type == FieldDescription.FieldType.Glue)  // colle ?
				{
					if (level == 0)
					{
						this.PreprocessGlue(field, labelsId, ref labelId, ref column, isGlueAfter);
					}
				}
				else if (field.Type == FieldDescription.FieldType.Node)
				{
					throw new System.InvalidOperationException("Type incorrect (la liste de FieldDescription devrait �tre aplatie).");
				}
			}

			//	Cr�e les diff�rentes colonnes, en fonction des r�sultats de la premi�re passe.
			Widgets.Layouts.GridLayoutEngine grid = new Widgets.Layouts.GridLayoutEngine();
			int lastLabelId = int.MinValue;
			for (int i=0; i<labelsId.Count; i++)
			{
				if (lastLabelId != labelsId[i])
				{
					lastLabelId = labelsId[i];

					if (labelsId[i] < 0)  // est-ce que cette colonne contient un label ?
					{
						//	Largeur automatique selon la taille minimale du contenu.
						grid.ColumnDefinitions.Add(new Widgets.Layouts.ColumnDefinition());
						System.Console.WriteLine(string.Format("Column {0}: automatic", i));
					}
					else
					{
						//	Largeur de 10%, 10 pixels au minimum, pas de maximum (par colonne virtuelle).
						double relWidth = 0;
						double minWidth = 0;
						for (int j=i; j<labelsId.Count; j++)
						{
							if (lastLabelId == labelsId[j])
							{
								relWidth += 10;  // largeur relative en %
								minWidth += 10;  // largeur minimale
							}
							else
							{
								break;
							}
						}

						grid.ColumnDefinitions.Add(new Widgets.Layouts.ColumnDefinition(new Widgets.Layouts.GridLength(relWidth, Widgets.Layouts.GridUnitType.Proportional), minWidth, double.PositiveInfinity));
						System.Console.WriteLine(string.Format("Column {0}: relWidth={1}%", i, relWidth));
					}
				}
			}
			System.Console.WriteLine(string.Format("GridLayoutEngine with {0} columns", grid.ColumnDefinitions.Count));

			if (grid.ColumnDefinitions.Count != 0)
			{
				grid.ColumnDefinitions[0].RightBorder = 1;
			}

			Widgets.Layouts.LayoutEngine.SetLayoutEngine(root, grid);

			//	Deuxi�me passe pour g�n�rer le contenu.
			column = 0;
			row = 0;
			level = 0;
			List<Druid> lastTitle = null;
			for (int i=index; i<fields.Count; i++)
			{
				FieldDescription field = fields[i];

				if (field.DeltaHidden)
				{
					continue;
				}

				bool isGlueAfter = false;
				FieldDescription nextField = Engine.SearchNextElement(fields, i);
				if (nextField != null && nextField.Type == FieldDescription.FieldType.Glue)
				{
					isGlueAfter = true;
				}

				//	Assigne l'identificateur unique, qui ira dans la propri�t� Index des widgets.
				//	La valeur -1 par d�faut indique un widget non identifi�.
				System.Guid guid;
				if (field.Source == null)
				{
					guid = field.Guid;
				}
				else
				{
					//	Un champ d'un sous-masque re�oit l'identificateur du SubForm qui l'a initi�,
					//	afin que sa s�lection dans l'�diteur s�lectionne le SubForm dans la liste.
					guid = field.Source.Guid;
				}

				if (field.Type == FieldDescription.FieldType.BoxBegin ||  // d�but de bo�te ?
					field.Type == FieldDescription.FieldType.SubForm)
				{
					if (level == 0)
					{
						UI.Panel box = this.CreateBox(root, grid, field, guid, labelsId, ref column, ref row, isGlueAfter);
						this.CreateFormBox(box, entityId, fields, i+1);
					}

					level++;
				}
				else if (field.Type == FieldDescription.FieldType.BoxEnd)  // fin de bo�te ?
				{
					level--;

					if (level < 0)
					{
						break;
					}
				}
				else if (field.Type == FieldDescription.FieldType.Field)  // champ ?
				{
					if (level == 0)
					{
						this.CreateField(root, entityId, grid, field, guid, labelsId, ref column, ref row, isGlueAfter);
					}
				}
				else if (field.Type == FieldDescription.FieldType.Glue)  // colle ?
				{
					if (level == 0)
					{
						this.CreateGlue(root, grid, field, guid, labelsId, ref column, ref row, isGlueAfter);
					}
				}
				else if (field.Type == FieldDescription.FieldType.Title ||
						 field.Type == FieldDescription.FieldType.Line)  // s�parateur ?
				{
					if (level == 0)
					{
						FieldDescription next = Engine.SearchNextField(fields, i);  // cherche le prochain champ
						this.CreateSeparator(root, grid, field, guid, next, labelsId, ref column, ref row, isGlueAfter, ref lastTitle);
					}
				}
			}
		}


		private void PreprocessBoxBegin(FieldDescription field, List<int> labelsId, ref int labelId, ref int column, bool isGlueAfter)
		{
			//	D�termine quelles colonnes contiennent des labels, lors de la premi�re passe.
			//	Un BoxBegin ne contient jamais de label, mais il faut tout de m�me faire �voluer
			//	le num�ro de la colonne.
			int columnsRequired = System.Math.Max(field.ColumnsRequired, 1);

			Engine.LabelIdUse(labelsId, labelId++, column, columnsRequired);

			if (isGlueAfter)
			{
				column += columnsRequired;
			}
			else
			{
				column = 0;
			}
		}

		private void PreprocessField(FieldDescription field, List<int> labelsId, ref int labelId, ref int column, bool isGlueAfter)
		{
			//	D�termine quelles colonnes contiennent des labels, lors de la premi�re passe.
			int columnsRequired = System.Math.Max(field.ColumnsRequired, 1);

			if (columnsRequired == 1)
			{
				Engine.LabelIdUse(labelsId, labelId++, column, 1);
			}
			else
			{
				Engine.LabelIdUse(labelsId, -(labelId++), column, 1);
				Engine.LabelIdUse(labelsId, labelId++, column+1, columnsRequired-1);
			}

			if (isGlueAfter)
			{
				column += columnsRequired;
			}
			else
			{
				column = 0;
			}
		}

		private void PreprocessGlue(FieldDescription field, List<int> labelsId, ref int labelId, ref int column, bool isGlueAfter)
		{
			//	D�termine quelles colonnes contiennent des labels, lors de la premi�re passe.
			int columnsRequired = field.ColumnsRequired;

			for (int i=0; i<columnsRequired; i++)
			{
				Engine.LabelIdUse(labelsId, labelId++, column+i, 1);
			}

			column += columnsRequired;
		}

		static private void LabelIdUse(List<int> labelsId, int labelId, int column, int count)
		{
			//	Indique que les colonnes comprises entre column et column+count-1 ont un contenu commun,
			//	c'est-�-dire qui ne n�cessite qu'une colonne physique dans GridLayoutEngine, si cela est
			//	en accord avec les autres lignes.
			//
			//	Contenu initial:				0 0 0 0 0 0 0 0 0 0
			//	labelId=1, column=0, count=1:	1 0 0 0 0 0 0 0 0 0  (cas I)
			//	labelId=2, column=1, count=9:	1 2 2 2 2 2 2 2 2 2  (cas I)
			//	labelId=3, column=1, count=2:	1 3 3 2 2 2 2 2 2 2  (cas I)
			//	labelId=5, column=1, count=5:	1 3 3 5 5 5 2 2 2 2  (cas R)
			//	labelId=6, column=1, count=2:	1 6 6 4 4 4 2 2 2 2  (cas I)
			//	labelId=7, column=3, count=1:	1 6 6 7 4 4 2 2 2 2  (cas I)
			//	labelId=8, column=4, count=6:	1 6 6 7 4 4 2 2 2 2  (cas N)
			//
			//	Apr�s cette initialisation, il faudra cr�er 5 colonnes physiques:
			//	1) 1
			//	2) 6 6
			//	3) 7
			//	4) 4 4
			//	5) 2 2 2 2
			count = System.Math.Max(count, 1);

			int n = (column+count)-labelsId.Count;
			for (int i=0; i<n; i++)
			{
				labelsId.Add(0);
			}

			int last = column+count-1;
			int id = labelsId[column];
			for (int i=column+1; i<=last; i++)
			{
				if (labelsId[i] != id)
				{
					if (column > 0 && labelsId[column-1] == labelsId[column])
					{
						//	Cas L:
						int m = labelsId[column];
						for (int j=column; j<=last; j++)
						{
							if (labelsId[j] != m)
							{
								break;
							}
							labelsId[j] = labelId;
						}
					}

					if (last < labelsId.Count-1 && labelsId[last+1] == labelsId[last])
					{
						//	Cas R:
						int m = labelsId[last];
						for (int j=last; j>=column; j--)
						{
							if (labelsId[j] != m)
							{
								break;
							}
							labelsId[j] = labelId;
						}
					}

					//	Cas N:
					return;
				}
			}

			//	Cas I:
			for (int i=column; i<=last; i++)
			{
				labelsId[i] = labelId;
			}
		}

		static private int GetColumnIndex(List<int> labelsId, int column)
		{
			//	Conversion d'un num�ro de colonne virtuelle (0..9) en un index pour une colonne physique.
			//	Les colonnes physiques peuvent �tre moins nombreuses que les virtuelles.
			int index = column;
			int last = int.MinValue;
			for (int i=0; i<=column; i++)
			{
				if (i >= labelsId.Count)
				{
					break;
				}

				if (last == labelsId[i])
				{
					index--;
				}
				else
				{
					last = labelsId[i];
				}
			}

			return index;
		}


		private UI.Panel CreateBox(UI.Panel root, Widgets.Layouts.GridLayoutEngine grid, FieldDescription field, System.Guid guid, List<int> labelsId, ref int column, ref int row, bool isGlueAfter)
		{
			//	Cr�e les widgets pour une bo�te dans la grille, lors de la deuxi�me passe.
			UI.Panel box = new UI.Panel(root);
			box.DrawFrameState = FrameState.All;
			box.Padding = FieldDescription.GetRealBoxPadding(field.BoxPadding);
			box.BackColor = FieldDescription.GetRealBackColor(field.BackColor);
			box.DrawFrameState = field.BoxFrameState;
			box.DrawFrameWidth = field.BoxFrameWidth;
			box.Name = guid.ToString();
			
			grid.RowDefinitions.Add(new Widgets.Layouts.RowDefinition());

			int columnsRequired = System.Math.Max(field.ColumnsRequired, 1);

			grid.RowDefinitions[row].BottomBorder = FieldDescription.GetRealSeparator(field.SeparatorBottom);

			int i = Engine.GetColumnIndex(labelsId, column);
			int j = Engine.GetColumnIndex(labelsId, column+columnsRequired-1)+1;
			Widgets.Layouts.GridLayoutEngine.SetColumn(box, i);
			Widgets.Layouts.GridLayoutEngine.SetRow(box, row);
			Widgets.Layouts.GridLayoutEngine.SetColumnSpan(box, j-i);

			if (isGlueAfter)
			{
				column += columnsRequired;
			}
			else
			{
				row++;
				column = 0;
			}

			return box;
		}

		private void CreateField(UI.Panel root, Druid entityId, Widgets.Layouts.GridLayoutEngine grid, FieldDescription field, System.Guid guid, List<int> labelsId, ref int column, ref int row, bool isGlueAfter)
		{
			//	Cr�e les widgets pour un champ dans la grille, lors de la deuxi�me passe.
			UI.Placeholder placeholder = new UI.Placeholder(root);
			placeholder.SetBinding(UI.Placeholder.ValueProperty, new Binding(BindingMode.TwoWay, field.GetPath(UI.DataSource.DataName)));
			placeholder.BackColor = FieldDescription.GetRealBackColor(field.BackColor);
			placeholder.TabIndex = grid.RowDefinitions.Count;
			placeholder.Name = guid.ToString();

			//	D�termine si le placeholder doit �tre utilis� pour saisir du texte ou pour
			//	saisir un crit�re de recherche et le configure en cons�quence.
			FieldEditionMode editionMode = this.GetFieldEditionMode(entityId, field.FieldIds);
			switch (editionMode)
			{
				case FieldEditionMode.Data:
					placeholder.SuggestionMode = Epsitec.Common.UI.PlaceholderSuggestionMode.None;
					break;
				case FieldEditionMode.Search:
					placeholder.SuggestionMode = Epsitec.Common.UI.PlaceholderSuggestionMode.DisplayPassiveHint;
					break;
				default:
					throw new System.InvalidOperationException(string.Format("Invalid edition mode {0}", editionMode));
			}

			grid.RowDefinitions.Add(new Widgets.Layouts.RowDefinition());

			int columnsRequired = System.Math.Max(field.ColumnsRequired, 1);

			if (columnsRequired == 1)  // tout sur une seule colonne ?
			{
				placeholder.Controller = "*";
				placeholder.ControllerParameters = UI.Controllers.AbstractController.NoLabelsParameter;  // cache le label
			}

			grid.RowDefinitions[row].BottomBorder = FieldDescription.GetRealSeparator(field.SeparatorBottom);

			if (field.RowsRequired > 1)
			{
				placeholder.PreferredHeight = field.RowsRequired*20;
			}

			int i = Engine.GetColumnIndex(labelsId, column);
			int j = Engine.GetColumnIndex(labelsId, column+columnsRequired-1)+1;
			Widgets.Layouts.GridLayoutEngine.SetColumn(placeholder, i);
			Widgets.Layouts.GridLayoutEngine.SetRow(placeholder, row);
			Widgets.Layouts.GridLayoutEngine.SetColumnSpan(placeholder, j-i);

			if (isGlueAfter)
			{
				column += columnsRequired;
			}
			else
			{
				row++;
				column = 0;
			}
		}

		private void CreateGlue(UI.Panel root, Widgets.Layouts.GridLayoutEngine grid, FieldDescription field, System.Guid guid, List<int> labelsId, ref int column, ref int row, bool isGlueAfter)
		{
			//	Cr�e les widgets pour un collage dans la grille, lors de la deuxi�me passe.
			int columnsRequired = field.ColumnsRequired;

			if (this.forDesigner)
			{
				FrameBox glue = new FrameBox(root);
				glue.BackColor = FieldDescription.GetRealBackColor(field.BackColor);
				glue.Name = guid.ToString();

				if (columnsRequired == 0)
				{
					glue.Index = Engine.GlueNull;  // pour feinter les dimensions lors des d�tections et du dessin de la s�lection
					glue.PreferredWidth = 0; // pour ne pas perturber le calcul de la largeur d'une colonne contenant un label

					int i = Engine.GetColumnIndex(labelsId, column);
					Widgets.Layouts.GridLayoutEngine.SetColumn(glue, i);
					Widgets.Layouts.GridLayoutEngine.SetRow(glue, row);
				}
				else
				{
					grid.RowDefinitions.Add(new Widgets.Layouts.RowDefinition());

					int i = Engine.GetColumnIndex(labelsId, column);
					int j = Engine.GetColumnIndex(labelsId, column+columnsRequired-1)+1;
					Widgets.Layouts.GridLayoutEngine.SetColumn(glue, i);
					Widgets.Layouts.GridLayoutEngine.SetRow(glue, row);
					Widgets.Layouts.GridLayoutEngine.SetColumnSpan(glue, j-i);
				}
			}

			column += columnsRequired;
		}

		private void CreateSeparator(UI.Panel root, Widgets.Layouts.GridLayoutEngine grid, FieldDescription field, System.Guid guid, FieldDescription nextField, List<int> labelsId, ref int column, ref int row, bool isGlueAfter, ref List<Druid> lastTitle)
		{
			//	Cr�e les widgets pour un s�parateur dans la grille, lors de la deuxi�me passe.
			FieldDescription.FieldType type = field.Type;

			if (nextField == null)
			{
				type = FieldDescription.FieldType.Line;
			}

			if (type == FieldDescription.FieldType.Title)
			{
				List<Druid> druids = nextField.FieldIds;
				System.Text.StringBuilder builder = new System.Text.StringBuilder();

				for (int i=0; i<druids.Count-1; i++)
				{
					Druid druid = druids[i];

					if (lastTitle != null && i < lastTitle.Count && lastTitle[i] == druid)  // label d�j� mis pr�c�demment ?
					{
						continue;
					}

					if (builder.Length > 0)
					{
						builder.Append(", ");
					}

					builder.Append(this.GetCaptionDefaultLabel(druid));
				}

				if (builder.Length == 0)  // titre sans texte ?
				{
					type = FieldDescription.FieldType.Line;  // il faudra mettre une simple ligne
				}
				else
				{
					grid.RowDefinitions.Add(new Widgets.Layouts.RowDefinition());
					grid.RowDefinitions[row].TopBorder = 5;
					grid.RowDefinitions[row].BottomBorder = 0;

					double size = System.Math.Max(200-(druids.Count-2)*25, 100);

					StaticText text = new StaticText(root);
					text.Text = string.Concat("<font size=\"", size.ToString(System.Globalization.CultureInfo.InvariantCulture), "%\"><b>", builder.ToString(), "</b></font>");
					text.PreferredHeight = size/100*16;
					text.Name = guid.ToString();

					int i = Engine.GetColumnIndex(labelsId, 0);
					int j = Engine.GetColumnIndex(labelsId, labelsId.Count-1)+1;
					Widgets.Layouts.GridLayoutEngine.SetColumn(text, i);
					Widgets.Layouts.GridLayoutEngine.SetRow(text, row);
					Widgets.Layouts.GridLayoutEngine.SetColumnSpan(text, j-i);

					row++;
				}

				lastTitle = druids;  // pour se rappeler du titre pr�c�dent
			}

			if (type == FieldDescription.FieldType.Line ||
				type == FieldDescription.FieldType.Title)
			{
				grid.RowDefinitions.Add(new Widgets.Layouts.RowDefinition());
				grid.RowDefinitions[row].TopBorder = (type == FieldDescription.FieldType.Title) ? 0 : 10;
				grid.RowDefinitions[row].BottomBorder = 10;

				Separator sep = new Separator(root);
				sep.PreferredHeight = 1;
				sep.Name = guid.ToString();

				int i = Engine.GetColumnIndex(labelsId, 0);
				int j = Engine.GetColumnIndex(labelsId, labelsId.Count-1)+1;
				Widgets.Layouts.GridLayoutEngine.SetColumn(sep, i);
				Widgets.Layouts.GridLayoutEngine.SetRow(sep, row);
				Widgets.Layouts.GridLayoutEngine.SetColumnSpan(sep, j-i);

				row++;
			}
		}

		private string GetCaptionDefaultLabel(Druid druid)
		{
			Caption caption = this.resourceProvider.GetCaption(druid);
			return caption == null ? "" : caption.DefaultLabel;
		}


		static private int CountFields(List<FieldDescription> fields, int index)
		{
			//	Compte le nombre de descriptions de types champ, s�parateur ou titre.
			int count = 0;

			for (int i=index; i<fields.Count; i++)
			{
				if (fields[i].Type == FieldDescription.FieldType.Field ||
					fields[i].Type == FieldDescription.FieldType.Line  ||
					fields[i].Type == FieldDescription.FieldType.Title )
				{
					count++;
				}
				else
				{
					break;
				}
			}

			return count;
		}

		static private FieldDescription SearchNextElement(List<FieldDescription> fields, int index)
		{
			//	Cherche le prochain �l�ment.
			if (fields[index].Type == FieldDescription.FieldType.BoxBegin ||
				fields[index].Type == FieldDescription.FieldType.SubForm)
			{
				int level = 0;

				for (int i=index; i<fields.Count; i++)
				{
					if (fields[i].Type == FieldDescription.FieldType.BoxBegin ||
						fields[i].Type == FieldDescription.FieldType.SubForm)
					{
						level++;
					}
					else if (fields[i].Type == FieldDescription.FieldType.BoxEnd)
					{
						level--;

						if (level == 0)
						{
							index = i;
							break;
						}
					}
				}
			}

			index++;
			if (index < fields.Count)
			{
				return fields[index];
			}
			else
			{
				return null;
			}
		}

		static private FieldDescription SearchNextField(List<FieldDescription> fields, int index)
		{
			//	Cherche la prochaine description de champ (pas de s�parateur).
			for (int i=index+1; i<fields.Count; i++)
			{
				if (fields[i].Type == FieldDescription.FieldType.Field)
				{
					return fields[i];
				}
			}

			return null;
		}


		public static readonly int MaxColumnsRequired = 10;
		public static readonly int MaxRowsRequired = 20;
		public static readonly int GlueNull = 1;

		private readonly IFormResourceProvider resourceProvider;
		private readonly EntityContext entityContext;
		private Arrange arrange;
		private bool forDesigner;
		private FieldEditionMode defaultMode;
	}
}
