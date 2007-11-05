using System.Collections.Generic;

using Epsitec.Common.Drawing;
using Epsitec.Common.Support;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;

namespace Epsitec.Common.FormEngine
{
	/// <summary>
	/// G�n�rateur de masques de saisie.
	/// </summary>
	public class FormEngine
	{
		public FormEngine(ResourceManager resourceManager)
		{
			//	Constructeur.
			this.resourceManager = resourceManager;
		}

		public Widget CreateForm(Druid entityId, List<FieldDescription> fields)
		{
			//	Cr�e un masque de saisie pour une entit� donn�e.
			Caption entityCaption = this.resourceManager.GetCaption(entityId);
			StructuredType entity = TypeRosetta.GetTypeObject(entityCaption) as StructuredType;

			StructuredData entityData = new StructuredData(entity);
			entityData.UndefinedValueMode = UndefinedValueMode.Default;

			UI.Panel root = new UI.Panel();
			root.ResourceManager = this.resourceManager;
			root.DataSource = new UI.DataSource();
			root.DataSource.AddDataSource("Data", entityData);

			int column = 0, row = 0;
			bool[] isLabel = new bool[FormEngine.MaxColumnsRequired];
			foreach (FieldDescription field in fields)
			{
				this.PrecreateField(field, isLabel, ref column, ref row);
			}

			Widgets.Layouts.GridLayoutEngine grid = new Widgets.Layouts.GridLayoutEngine();
			for (int i=0; i<FormEngine.MaxColumnsRequired; i++)
			{
				if (isLabel[i])
				{
					grid.ColumnDefinitions.Add(new Widgets.Layouts.ColumnDefinition());
				}
				else
				{
					grid.ColumnDefinitions.Add(new Widgets.Layouts.ColumnDefinition(new Widgets.Layouts.GridLength(i, Widgets.Layouts.GridUnitType.Proportional), 20, double.PositiveInfinity));
				}
			}
			grid.ColumnDefinitions[0].RightBorder = 1;

			Widgets.Layouts.LayoutEngine.SetLayoutEngine(root, grid);

			column = 0;
			row = 0;
			foreach (FieldDescription field in fields)
			{
				string path = field.GetPath("Data");
				this.CreateField(root, grid, path, field, ref column, ref row);
			}

			return root;
		}

		private void PrecreateField(FieldDescription field, bool[] isLabel, ref int column, ref int row)
		{
			int columnsRequired = System.Math.Min(field.ColumnsRequired, FormEngine.MaxColumnsRequired-1);

			if (column+1+columnsRequired > FormEngine.MaxColumnsRequired)  // d�passe � droite ?
			{
				row++;
				column = 0;
			}

			isLabel[column] = true;

			if (field.BottomSeparator == FieldDescription.SeparatorType.Append)
			{
				column += 1+columnsRequired;
			}
			else
			{
				row++;
				column = 0;
			}
		}

		private void CreateField(UI.Panel root, Widgets.Layouts.GridLayoutEngine grid, string path, FieldDescription field, ref int column, ref int row)
		{
			//	Cr�e les widgets pour un champ dans la grille.
			int index = grid.RowDefinitions.Count;

			if (field.TopSeparator == FieldDescription.SeparatorType.Line)
			{
				grid.RowDefinitions.Add(new Widgets.Layouts.RowDefinition());
				grid.RowDefinitions[index].TopBorder = 10;
				grid.RowDefinitions[index].BottomBorder = 10;

				Separator sep = new Separator(root);
				sep.PreferredHeight = 1;

				Widgets.Layouts.GridLayoutEngine.SetColumn(sep, 0);
				Widgets.Layouts.GridLayoutEngine.SetRow(sep, row);
				Widgets.Layouts.GridLayoutEngine.SetColumnSpan(sep, 1+FormEngine.MaxColumnsRequired);

				index++;
				row++;
			}

			UI.Placeholder placeholder = new Epsitec.Common.UI.Placeholder(root);
			placeholder.SetBinding(UI.Placeholder.ValueProperty, new Binding(BindingMode.TwoWay, path));
			placeholder.BackColor = field.BackColor;
			placeholder.TabIndex = index;

			grid.RowDefinitions.Add(new Widgets.Layouts.RowDefinition());

			double m = 2;
			switch (field.BottomSeparator)
			{
				case FieldDescription.SeparatorType.Compact:
					m = -1;
					break;

				case FieldDescription.SeparatorType.Extend:
					m = 10;
					break;

				case FieldDescription.SeparatorType.Line:
					m = 0;
					break;
			}
			grid.RowDefinitions[index].BottomBorder = m;

			int columnsRequired = System.Math.Min(field.ColumnsRequired, FormEngine.MaxColumnsRequired-1);

			if (column+1+columnsRequired > FormEngine.MaxColumnsRequired)  // d�passe � droite ?
			{
				row++;
				column = 0;
			}

			if (field.RowsRequired > 1)
			{
				placeholder.PreferredHeight = field.RowsRequired*20;
			}

			Widgets.Layouts.GridLayoutEngine.SetColumn(placeholder, column);
			Widgets.Layouts.GridLayoutEngine.SetRow(placeholder, row);
			Widgets.Layouts.GridLayoutEngine.SetColumnSpan(placeholder, 1+columnsRequired);

			if (field.BottomSeparator == FieldDescription.SeparatorType.Append)
			{
				column += 1+columnsRequired;
			}
			else
			{
				row++;
				column = 0;
			}

			if (field.BottomSeparator == FieldDescription.SeparatorType.Line)
			{
				index++;
				grid.RowDefinitions.Add(new Widgets.Layouts.RowDefinition());
				grid.RowDefinitions[index].TopBorder = 10;
				grid.RowDefinitions[index].BottomBorder = 10;

				Separator sep = new Separator(root);
				sep.PreferredHeight = 1;

				Widgets.Layouts.GridLayoutEngine.SetColumn(sep, 0);
				Widgets.Layouts.GridLayoutEngine.SetRow(sep, row);
				Widgets.Layouts.GridLayoutEngine.SetColumnSpan(sep, 1+FormEngine.MaxColumnsRequired);

				row++;
			}
		}


		public static readonly int MaxColumnsRequired = 10;
		public static readonly int MaxRowsRequired = 20;

		ResourceManager resourceManager;
	}
}
