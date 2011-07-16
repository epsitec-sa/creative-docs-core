﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Business;

using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.BusinessDocumentControllers
{
	/// <summary>
	/// C'est ici qu'est concentré toutes les opérations ayant trait aux lignes d'un document commercial.
	/// </summary>
	public static class LinesHelper
	{
		public static LineInformations CreateArticle(BusinessContext businessContext, BusinessDocumentEntity businessDocumentEntity, List<LineInformations> selection)
		{
			int index;

			if (selection.Count == 0)
			{
				index = LinesHelper.GetLDefaultArticleInsertionIndex (businessDocumentEntity);
			}
			else
			{
				index = businessDocumentEntity.Lines.IndexOf (selection.Last ().AbstractDocumentItemEntity) + 1;
			}

			var quantityColumnEntity = LinesHelper.SearchArticleQuantityColumnEntity (businessContext, ArticleQuantityType.Ordered);

			if (quantityColumnEntity == null)
			{
				return null;
			}

			var model = businessDocumentEntity.Lines[index-1];

			var newQuantity = businessContext.CreateEntity<ArticleQuantityEntity> ();
			newQuantity.Quantity = 1;
			newQuantity.QuantityColumn = quantityColumnEntity;

			var newLine = businessContext.CreateEntity<ArticleDocumentItemEntity> ();
			newLine.GroupIndex = model.GroupIndex;
			newLine.ArticleQuantities.Add (newQuantity);

			businessDocumentEntity.Lines.Insert (index, newLine);

			return new LineInformations (null, newLine, null, 0, 0);
		}

		public static bool Delete(BusinessContext businessContext, BusinessDocumentEntity businessDocumentEntity, List<LineInformations> selection)
		{
			using (businessContext.SuspendUpdates ())
			{
				foreach (var info in selection)
				{
					var line     = info.AbstractDocumentItemEntity;
					var quantity = info.ArticleQuantityEntity;

					if (line is ArticleDocumentItemEntity && quantity != null && info.SublineIndex > 0)  // quantité ?
					{
						var article = line as ArticleDocumentItemEntity;
						article.ArticleQuantities.Remove (quantity);
					}
					else
					{
						businessDocumentEntity.Lines.Remove (info.AbstractDocumentItemEntity);
					}
				}
			}

			return true;
		}

		public static bool Duplicate(BusinessContext businessContext, BusinessDocumentEntity businessDocumentEntity, List<LineInformations> selection)
		{
			if (selection.Count != 1)
			{
				return false;
			}

			var info = selection[0];
			var line = info.AbstractDocumentItemEntity;
			var index = info.LineIndex;

			if (line.Attributes.HasFlag (DocumentItemAttributes.AutoGenerated))
			{
				return false;
			}

			var copy = line.CloneEntity (businessContext);
			businessDocumentEntity.Lines.Insert (index+1, copy);

			return true;
		}


		private static int GetLDefaultArticleInsertionIndex(BusinessDocumentEntity businessDocumentEntity)
		{
			for (int i = businessDocumentEntity.Lines.Count-1; i >= 0; i--)
			{
				var line = businessDocumentEntity.Lines[i];

				if (line is ArticleDocumentItemEntity ||
					line is TextDocumentItemEntity)
				{
					return i+1;
				}
			}

			return 0;
		}

		private static ArticleQuantityColumnEntity SearchArticleQuantityColumnEntity(BusinessContext businessContext, ArticleQuantityType type)
		{
			var example = new ArticleQuantityColumnEntity ();
			example.QuantityType = type;

			return businessContext.DataContext.GetByExample (example).FirstOrDefault ();
		}
	}
}
