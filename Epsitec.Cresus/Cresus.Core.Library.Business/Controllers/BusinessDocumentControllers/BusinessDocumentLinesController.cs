﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;
using Epsitec.Common.Support;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Controllers.DataAccessors;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Widgets.Tiles;
using Epsitec.Cresus.Core.Helpers;
using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Library.Business.ContentAccessors;

using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.BusinessDocumentControllers
{
	public class BusinessDocumentLinesController
	{
		public BusinessDocumentLinesController(AccessData accessData)
		{
			this.accessData = accessData;

			this.commandDispatcher = new CommandDispatcher ("BusinessDocumentLinesController", CommandDispatcherLevel.Secondary, CommandDispatcherOptions.AutoForwardCommands);
			this.commandDispatcher.RegisterController (this);

			this.commandContext = new CommandContext ("BusinessDocumentLinesController", CommandContextOptions.ActivateWithoutFocus);

			this.lineInformations = new List<LineInformations> ();
			this.UpdateLineInformations ();
		}

		public void CreateUI(Widget parent)
		{
			var frame = new FrameBox
			{
				Parent = parent,
				Dock = DockStyle.Fill,
			};

			CommandDispatcher.SetDispatcher (frame, this.commandDispatcher);
			CommandContext.SetContext (frame, this.commandContext);

			//	Crée la toolbar.
			//?this.lineToolbarController = new LineToolbarController (this.accessData.DocumentMetadataEntity, this.accessData.BusinessDocumentEntity);
			//?this.lineToolbarController.CreateUI (frame);

			//	Crée la liste.
			this.linesController = new LinesController (this.accessData);
			this.linesController.CreateUI (frame, this.CallbackSelectionChanged);

			//	Crée l'éditeur pour une ligne.
			this.lineEditorController = new LineEditorController (this.accessData);
			this.lineEditorController.CreateUI (frame);
		}

		public void UpdateUI(int? sel = null)
		{
			this.linesController.UpdateUI (this.lineInformations.Count, this.CallbackGetLineInformations, this.CallbackGetCellContent, sel);
			this.UpdateCommands ();
		}


		private LineInformations CallbackGetLineInformations(int index)
		{
			//	Retourne les informations sur l'état d'une ligne du tableau.
			return this.lineInformations[index];
		}

		private FormattedText CallbackGetCellContent(int index, ColumnType columnType)
		{
			//	Retourne le contenu permettant de peupler une cellule du tableau.
			var info = this.lineInformations[index];

			switch (columnType)
			{
				case ColumnType.QuantityAndUnit:
					return info.GetColumnContent (DocumentItemAccessorColumn.UniqueQuantity);

				case ColumnType.Type:
					return info.GetColumnContent (DocumentItemAccessorColumn.UniqueType);

				case ColumnType.Date:
					return info.GetColumnContent (DocumentItemAccessorColumn.UniqueBeginDate);

				case ColumnType.ArticleId:
					return info.GetColumnContent (DocumentItemAccessorColumn.ArticleId);

				case ColumnType.ArticleDescription:
					return info.GetColumnContent (DocumentItemAccessorColumn.ArticleDescription);

				case ColumnType.Discount:
					return info.GetColumnContent (DocumentItemAccessorColumn.Discount);

				case ColumnType.UnitPrice:
					return info.GetColumnContent (DocumentItemAccessorColumn.UnitPrice);

				case ColumnType.LinePrice:
					return info.GetColumnContent (DocumentItemAccessorColumn.LinePrice);

				case ColumnType.Vat:
					return info.GetColumnContent (DocumentItemAccessorColumn.Vat);

				case ColumnType.Total:
					return info.GetColumnContent (DocumentItemAccessorColumn.Total);
			}

			return null;
		}

		private bool CallbackSelectionChanged()
		{
			//	Appelé lorsque la sélection dans la liste a changé.
			if (this.linesController.HasSingleSelection)
			{
				int? sel = this.linesController.LastSelection;
				var info = this.lineInformations[sel.Value];

				this.lineEditorController.UpdateUI (info);
			}
			else
			{
				this.lineEditorController.UpdateUI (null);
			}

			this.UpdateCommands ();

			return true;
		}


		[Command (Library.Business.Res.CommandIds.Lines.CreateArticle)]
		public void ProcessCreateArticle()
		{
			//	Insère une nouvelle ligne d'article.
			int? sel = this.linesController.LastSelection;

			if (sel != null)
			{
				var info = this.lineInformations[sel.Value];

				var newLine = this.accessData.BusinessContext.CreateEntity<ArticleDocumentItemEntity> ();
				newLine.GroupIndex = 1;

				this.accessData.BusinessDocumentEntity.Lines.Insert (info.LineIndex+1, newLine);

				this.UpdateAfterChange (newLine, null);
			}
		}

		[Command (Library.Business.Res.CommandIds.Lines.CreateText)]
		public void ProcessCreateText()
		{
			//	Insère une nouvelle ligne de texte.
			int? sel = this.linesController.LastSelection;

			if (sel != null)
			{
				var info = this.lineInformations[sel.Value];

				var newLine = this.accessData.BusinessContext.CreateEntity<TextDocumentItemEntity> ();
				newLine.Text = "Coucou !!!";
				newLine.GroupIndex = 1;

				this.accessData.BusinessDocumentEntity.Lines.Insert (info.LineIndex+1, newLine);

				this.UpdateAfterChange (newLine, null);
			}
		}

		[Command (Library.Business.Res.CommandIds.Lines.CreateTitle)]
		public void ProcessCreateTitle()
		{
			//	Insère une nouvelle ligne de titre.
			int? sel = this.linesController.LastSelection;
			int index = 0;

			if (sel != null)
			{
				var info = this.lineInformations[sel.Value];
				index = info.LineIndex+1;
			}


			var newLine = this.accessData.BusinessContext.CreateEntity<TextDocumentItemEntity> ();
			newLine.Text = "Titre !!!";
			newLine.GroupIndex = 1;

			this.accessData.BusinessDocumentEntity.Lines.Insert (index, newLine);
			this.UpdateAfterChange (newLine, null);
		}

		[Command (Library.Business.Res.CommandIds.Lines.CreateDiscount)]
		public void ProcessCreateDiscount()
		{
			//	Insère une nouvelle ligne de rabais.
		}

		[Command (Library.Business.Res.CommandIds.Lines.CreateTax)]
		public void ProcessCreateTax()
		{
			//	Insère une nouvelle ligne de taxe.
		}

		[Command (Library.Business.Res.CommandIds.Lines.CreateQuantity)]
		public void ProcessCreateQuantity()
		{
			//	Insère une nouvelle quantité.
			int? sel = this.linesController.LastSelection;

			if (sel != null)
			{
				var info = this.lineInformations[sel.Value];
				var line = this.accessData.BusinessDocumentEntity.Lines.ElementAt (info.LineIndex);

				if (line is ArticleDocumentItemEntity)
				{
					var article = line as ArticleDocumentItemEntity;
					var quantity = article.ArticleQuantities[info.SublineIndex];

					var newQuantity = this.accessData.BusinessContext.CreateEntity<ArticleQuantityEntity> ();
					newQuantity.Quantity = 1;
					newQuantity.Unit = quantity.Unit;
					newQuantity.QuantityColumn = this.SearchArticleQuantityColumnEntity (ArticleQuantityType.Delayed);
					newQuantity.BeginDate = new Date (Date.Today.Ticks + Time.TicksPerDay*7);  // arbitrairement dans une semaine !

					article.ArticleQuantities.Add (newQuantity);

					this.UpdateAfterChange (line, newQuantity);
				}
			}
		}

		[Command (Library.Business.Res.CommandIds.Lines.CreateGroup)]
		private void ProcessCreateGroup()
		{
			//	Insère un nouveau groupe.
		}

		[Command (Library.Business.Res.CommandIds.Lines.CreateGroupSeparator)]
		private void ProcessCreateGroupSeparator()
		{
			//	Insère un nouveau groupe après le groupe en cours (donc au même niveau).
		}

		[Command (Library.Business.Res.CommandIds.Lines.MoveUp)]
		private void ProcessMoveUp()
		{
			//	Monte la ligne sélectionnée.
			this.ProcessMove (-1);
		}

		[Command (Library.Business.Res.CommandIds.Lines.MoveDown)]
		private void ProcessMoveDown()
		{
			//	Descend la ligne sélectionnée.
			this.ProcessMove (1);
		}

		private void ProcessMove(int direction)
		{
			//	Monte ou descend la ligne sélectionnée.
			int? sel = this.linesController.LastSelection;

			if (sel != null)
			{
				var info = this.lineInformations[sel.Value];
				var line = this.accessData.BusinessDocumentEntity.Lines.ElementAt (info.LineIndex);
				var index = info.LineIndex;

				if (index+direction >= 0 && index+direction < this.accessData.BusinessDocumentEntity.Lines.Count)
				{
					this.accessData.BusinessDocumentEntity.Lines.RemoveAt (index);
					this.accessData.BusinessDocumentEntity.Lines.Insert (index+direction, line);

					this.UpdateAfterChange (line, null);
				}
			}
		}

		[Command (Library.Business.Res.CommandIds.Lines.Duplicate)]
		private void ProcessDuplicate()
		{
			//	Duplique la ligne sélectionnée.
			int? sel = this.linesController.LastSelection;

			if (sel != null)
			{
				var info = this.lineInformations[sel.Value];
				var line = this.accessData.BusinessDocumentEntity.Lines.ElementAt (info.LineIndex);
				var index = info.LineIndex;

				if (index+1 < this.accessData.BusinessDocumentEntity.Lines.Count)
				{
					var copy = line.CloneEntity (this.accessData.BusinessContext);
					this.accessData.BusinessDocumentEntity.Lines.Insert (index+1, copy);

					this.UpdateAfterChange (copy, null);
				}
			}
		}

		[Command (Library.Business.Res.CommandIds.Lines.Delete)]
		private void ProcessDelete()
		{
			//	Supprime la ligne sélectionnée.
			int? sel = this.linesController.LastSelection;

			if (sel != null)
			{
				var info = this.lineInformations[sel.Value];
				var line = this.accessData.BusinessDocumentEntity.Lines.ElementAt (info.LineIndex);

				if (line is ArticleDocumentItemEntity && info.SublineIndex > 0)  // quantité ?
				{
					var article = line as ArticleDocumentItemEntity;
					article.ArticleQuantities.RemoveAt (info.SublineIndex);
				}
				else
				{
					this.accessData.BusinessDocumentEntity.Lines.RemoveAt (info.LineIndex);
				}

				this.UpdateAfterChange (line, null);
			}
		}

		[Command (Library.Business.Res.CommandIds.Lines.Group)]
		private void ProcessGroup()
		{
			//	Groupe toutes les lignes sélectionnées.
		}

		[Command (Library.Business.Res.CommandIds.Lines.Ungroup)]
		private void ProcessUngroup()
		{
			//	Défait le groupe sélectionné.
		}


		private void UpdateAfterChange(AbstractDocumentItemEntity line, ArticleQuantityEntity quantity)
		{
			this.UpdateLineInformations ();

			int? sel = this.GetLineInformationsIndex (line, quantity);
			this.UpdateUI (sel);
		}

		private ArticleQuantityColumnEntity SearchArticleQuantityColumnEntity(ArticleQuantityType type)
		{
			var example = new ArticleQuantityColumnEntity ();
			example.QuantityType = type;

			return this.accessData.BusinessContext.DataContext.GetByExample (example).FirstOrDefault ();
		}


		private int? GetLineInformationsIndex(AbstractDocumentItemEntity line, ArticleQuantityEntity quantity)
		{
			for (int i = 0; i < this.lineInformations.Count; i++)
			{
				var info = this.lineInformations[i];

				if (quantity == null)
				{
					if (info.AbstractDocumentItemEntity == line)
					{
						return i;
					}
				}
				else
				{
					if (info.AbstractDocumentItemEntity == line &&
						info.ArticleQuantityEntity == quantity)
					{
						return i;
					}
				}
			}

			return null;
		}

		private void UpdateLineInformations()
		{
			this.lineInformations.Clear ();

			for (int i = 0; i < this.accessData.BusinessDocumentEntity.Lines.Count; i++)
			{
				var line = this.accessData.BusinessDocumentEntity.Lines[i];

				var accessor = new DocumentItemAccessor ();
				accessor.BuildContent (line, this.accessData.DocumentMetadataEntity.DocumentCategory.DocumentType, DocumentItemAccessorMode.ForceAllLines|DocumentItemAccessorMode.SpecialQuantitiesToDistinctLines);

				for (int row = 0; row < accessor.RowsCount; row++)
				{
					this.lineInformations.Add (new LineInformations (line, accessor, i, row));
				}
			}
		}


		private void UpdateCommands()
		{
			var selection     = this.linesController.Selection;
			var lastSelection = this.linesController.LastSelection;

			LineInformations info = null;
			if (lastSelection != null)
			{
				info = this.lineInformations[lastSelection.Value];
			}

			bool isArticle  = info != null && info.AbstractDocumentItemEntity is ArticleDocumentItemEntity;
			bool isText     = info != null && info.AbstractDocumentItemEntity is TextDocumentItemEntity;
			bool isTax      = info != null && info.AbstractDocumentItemEntity is TaxDocumentItemEntity;
			bool isSubTotal = info != null && info.AbstractDocumentItemEntity is SubTotalDocumentItemEntity;
			bool isEndTotal = info != null && info.AbstractDocumentItemEntity is EndTotalDocumentItemEntity;

			this.commandContext.GetCommandState (Library.Business.Res.Commands.Lines.CreateQuantity).Enable = isArticle;
			this.commandContext.GetCommandState (Library.Business.Res.Commands.Lines.CreateGroupSeparator).Enable = false;

			this.commandContext.GetCommandState (Library.Business.Res.Commands.Lines.MoveUp).Enable   = selection.Count == 1 && (isArticle || isText || isSubTotal);
			this.commandContext.GetCommandState (Library.Business.Res.Commands.Lines.MoveDown).Enable = selection.Count == 1 && (isArticle || isText || isSubTotal);

			this.commandContext.GetCommandState (Library.Business.Res.Commands.Lines.Duplicate).Enable = selection.Count == 1 && !isEndTotal;
			this.commandContext.GetCommandState (Library.Business.Res.Commands.Lines.Delete).Enable    = selection.Count != 0 && !isEndTotal;

			this.commandContext.GetCommandState (Library.Business.Res.Commands.Lines.Group).Enable   = selection.Count != 0 && !isEndTotal;
			this.commandContext.GetCommandState (Library.Business.Res.Commands.Lines.Ungroup).Enable = selection.Count != 0 && !isEndTotal;
		}

	
		private readonly AccessData						accessData;
		private readonly List<LineInformations>			lineInformations;
		private readonly CommandContext					commandContext;
		private readonly CommandDispatcher				commandDispatcher;

		private LineToolbarController					lineToolbarController;
		private LinesController							linesController;
		private LineEditorController					lineEditorController;
	}
}
