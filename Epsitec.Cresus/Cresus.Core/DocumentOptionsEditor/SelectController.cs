//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Dialogs;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Widgets;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.DocumentOptionsEditor
{
	public class SelectController
	{
		public SelectController(Core.Business.BusinessContext businessContext, DocumentOptionsEntity documentOptionsEntity)
		{
			this.businessContext       = businessContext;
			this.documentOptionsEntity = documentOptionsEntity;

			this.options      = DocumentOption.GetAllDocumentOptions ().Where (x => !x.IsTitle).ToList ();
			this.titleOptions = DocumentOption.GetAllDocumentOptions ().Where (x =>  x.IsTitle).ToList ();
		}


		public void CreateUI(Widget parent)
		{
			this.table = new CellTable
			{
				Parent = parent,
				PreferredWidth = 310,
				Dock = DockStyle.Left,
				Margins = new Margins (0, 10, 0, 0),
				StyleH = CellArrayStyles.Separator | CellArrayStyles.Header,
				StyleV = CellArrayStyles.ScrollNorm | CellArrayStyles.Separator | CellArrayStyles.SelectLine,
			};

			var rightFrame = new FrameBox
			{
				Parent = parent,
				PreferredWidth = 200,
				Dock = DockStyle.Left,
				Margins = new Margins (0, 10, 0, 0),
			};

			{
				var title = new StaticText
				{
					Parent = rightFrame,
					Text = "Types de documents :",
					Dock = DockStyle.Top,
					Margins = new Margins (0, 0, 0, 2),
				};

				var box = new FrameBox
				{
					Parent = rightFrame,
					PreferredHeight = 150,
					DrawFullFrame = true,
					Dock = DockStyle.Top,
					Margins = new Margins (0, 0, 0, 10),
					Padding = new Margins (10),
				};

				this.typesText = new StaticText
				{
					Parent = box,
					ContentAlignment = Common.Drawing.ContentAlignment.TopLeft,
					TextBreakMode = Common.Drawing.TextBreakMode.Hyphenate,
					Dock = DockStyle.Fill,
				};
			}

			{
				var title = new StaticText
				{
					Parent = rightFrame,
					Text = "Description :",
					Dock = DockStyle.Top,
					Margins = new Margins (0, 0, 10, 2),
				};

				var box = new FrameBox
				{
					Parent = rightFrame,
					PreferredHeight = 60,
					DrawFullFrame = true,
					Dock = DockStyle.Top,
					Margins = new Margins (0, 0, 0, 10),
					Padding = new Margins (10),
				};

				this.descriptionText = new StaticText
				{
					Parent = box,
					ContentAlignment = Common.Drawing.ContentAlignment.TopLeft,
					TextBreakMode = Common.Drawing.TextBreakMode.Hyphenate,
					Dock = DockStyle.Fill,
				};
			}

			{
				var buttonsBox = new FrameBox
				{
					Parent = rightFrame,
					PreferredWidth = 200,
					DrawFullFrame = true,
					Dock = DockStyle.Fill,
					Margins = new Margins (0, 0, 10, 0),
					Padding = new Margins (10),
				};

				this.noButton = new RadioButton
				{
					Parent = buttonsBox,
					Text = "N'utilise pas",
					AutoToggle = false,
					Dock = DockStyle.Top,
				};

				this.yesButton = new RadioButton
				{
					Parent = buttonsBox,
					Text = "Utilise",
					AutoToggle = false,
					Dock = DockStyle.Top,
				};
			}

			//	Conexion des événements.
			this.table.SelectionChanged += delegate
			{
				this.UpdateWidgets ();
			};

			this.UpdateTable ();
			this.UpdateWidgets ();
		}


		public void Update()
		{
		}


		private void UpdateTable()
		{
			int rows = options.Count;
			this.table.SetArraySize (3, rows);

			this.table.SetWidthColumn (0, 140);
			this.table.SetWidthColumn (1, 100);
			this.table.SetWidthColumn (2, 50);

			this.table.SetHeaderTextH (0, "Nom");
			this.table.SetHeaderTextH (1, "Description");
			this.table.SetHeaderTextH (2, "Utilise");

			ContentAlignment[] alignments =
			{
				ContentAlignment.MiddleLeft,
				ContentAlignment.MiddleLeft,
				ContentAlignment.MiddleLeft,
			};

			for (int row=0; row<rows; row++)
			{
				this.table.FillRow (row, alignments);
				this.table.UpdateRow (row, this.GetRowTexts (row));
			}
		}

		private string[] GetRowTexts(int row)
		{
			var result = new string[3];

			result[0] = this.options[row].Name;
			result[1] = this.options[row].Description;
			result[2] = "Non";

			return result;
		}

		private void UpdateWidgets()
		{
			int sel = this.table.SelectedRow;

			if (sel == -1)
			{
				this.typesText.Text       = null;
				this.descriptionText.Text = null;

				this.noButton.Enable  = false;
				this.yesButton.Enable = false;
			}
			else
			{
				this.typesText.Text       = this.options[sel].DocumentTypeDescription;
				this.descriptionText.Text = this.GetFullDescription (sel);

				this.noButton.Enable  = true;
				this.yesButton.Enable = true;
			}
		}

		private string GetFullDescription(int row)
		{
			string title = "";

			if (!string.IsNullOrEmpty (this.options[row].Group))
			{
				title = this.titleOptions.Where (x => x.Group == this.options[row].Group).Select (x => x.Title).FirstOrDefault ();
				title = string.Concat (title, "<br/>");
			}

			return string.Concat (title, this.options[row].Description);
		}


		private readonly Core.Business.BusinessContext		businessContext;
		private readonly DocumentOptionsEntity				documentOptionsEntity;
		private readonly List<DocumentOption>				options;
		private readonly List<DocumentOption>				titleOptions;

		private CellTable									table;
		private StaticText									typesText;
		private StaticText									descriptionText;
		private RadioButton									noButton;
		private RadioButton									yesButton;
	}
}
