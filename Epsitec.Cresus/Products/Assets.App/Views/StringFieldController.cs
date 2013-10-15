﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class StringFieldController : AbstractFieldController
	{
		public int LineCount = 1;

		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (this.value != value)
				{
					this.value = value;

					if (this.textField != null)
					{
						this.textField.Text = this.value;
					}
				}
			}
		}

		public override void CreateUI(Widget parent)
		{
			base.CreateUI (parent);

			if (this.LineCount == 1)
			{
				this.textField = new TextField
				{
					Parent         = this.frameBox,
					Dock           = DockStyle.Left,
					PreferredWidth = this.EditWidth,
					Margins        = new Margins (0, 10, 0, 0),
					TabIndex       = this.TabIndex,
					Text           = this.value,
				};
			}
			else
			{
				this.textField = new TextFieldMulti
				{
					Parent          = this.frameBox,
					Dock            = DockStyle.Left,
					PreferredWidth  = this.EditWidth,
					PreferredHeight = StringFieldController.GetMultiHeight (this.LineCount),
					Margins         = new Margins (0, 10, 0, 0),
					TabIndex        = this.TabIndex,
					Text            = this.value,
				};
			}

			this.textField.TextChanged += delegate
			{
				this.value = this.textField.Text;
				this.OnValueChanged ();
			};
		}

		private static int GetMultiHeight(int lineCount)
		{
			return 7 + lineCount*15;
		}


		private AbstractTextField				textField;
		private string							value;
	}
}
