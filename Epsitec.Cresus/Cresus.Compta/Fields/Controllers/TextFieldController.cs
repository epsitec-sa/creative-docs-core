//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Converters;
using Epsitec.Common.Support;

using Epsitec.Cresus.Compta.Accessors;
using Epsitec.Cresus.Compta.Entities;
using Epsitec.Cresus.Compta.Controllers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Fields.Controllers
{
	/// <summary>
	/// Contrôleur générique permettant l'édition d'un champ "texte". Les champs monétaires sont
	/// gérés comme des champs textuels.
	/// </summary>
	public class TextFieldController : AbstractFieldController
	{
		public TextFieldController(AbstractController controller, int line, ColumnMapper columnMapper, System.Action<int, ColumnType> clearFocusAction, System.Action<int, ColumnType> setFocusAction, System.Action<int, ColumnType> contentChangedAction)
			: base (controller, line, columnMapper, clearFocusAction, setFocusAction, contentChangedAction)
		{
		}


		public override void CreateUI(Widget parent)
		{
			this.CreateLabelUI (parent);
			this.CreateBoxUI (parent);

			this.container = new FrameBox
			{
				Parent          = this.box,
				PreferredHeight = 20,
				Dock            = DockStyle.Fill,
				TabIndex        = 1,
			};

			this.editWidget = new TextField
			{
				Parent          = this.container,
				PreferredHeight = 20,
				Dock            = DockStyle.Fill,
				TabIndex        = 1,
			};

			this.editWidget.TextChanged += new EventHandler (this.HandleTextChanged);
			this.editWidget.IsFocusedChanged += new EventHandler<DependencyPropertyChangedEventArgs> (this.HandleIsFocusedChanged);

			base.CreateForegroundUI ();
		}


		public override bool IsReadOnly
		{
			get
			{
				return this.InternalField.IsReadOnly;
			}
			protected set
			{
				this.InternalField.IsReadOnly = value;
				this.InternalField.Invalidate ();  // pour contourner un bug !
			}
		}

		public override void SetFocus()
		{
			this.InternalField.SelectAll ();
			this.InternalField.ClearFocus ();
			this.InternalField.Focus ();
		}

		public override void EditionDataToWidget()
		{
			if (this.editionData != null)
			{
				using (this.ignoreChanges.Enter ())
				{
					this.InternalField.FormattedText = this.editionData.Text;
					this.IsReadOnly = !this.editionData.Enable || !this.columnMapper.Enable;
				}
			}
		}

		public override void WidgetToEditionData()
		{
			if (this.editionData != null)
			{
				this.editionData.Text = this.InternalField.FormattedText;
				this.editionData.Validate ();
			}
		}

		public override void Validate()
		{
			if (this.editionData == null)
			{
				return;
			}

			this.WidgetToEditionData ();

			this.editWidget.SetError (this.editionData.HasError);
			this.UpdateTooltip ();
		}


		private void HandleTextChanged(object sender)
		{
			if (this.ignoreChanges.IsNotZero || this.editionData == null)
			{
				return;
			}

			this.Validate ();
			this.ContentChangedAction ();
		}

		private void HandleIsFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (this.editionData == null)
			{
				return;
			}

			this.hasFocus = (bool) e.NewValue;

			if (this.hasFocus)  // prise du focus ?
			{
				this.SetFocusAction ();
			}
			else  // perte du focus ?
			{
				//	Lorsqu'on perd le focus, il faut mettre à jour le contenu. Par exemple, si on a entré "123"
				//	dans un champ monétaire, il faut le replacer par "123.00" lorsque le focus passe à un autre
				//	widget.
				var text = this.editionData.Text;

				if (this.InternalField.FormattedText != text)
				{
					this.InternalField.FormattedText = text;
					//?this.ContentChangedAction ();  // TODO: Normalement inutile ?
				}

				this.ClearFocusAction ();
			}
		}


		private TextField InternalField
		{
			get
			{
				return this.editWidget as TextField;
			}
		}
	}
}
