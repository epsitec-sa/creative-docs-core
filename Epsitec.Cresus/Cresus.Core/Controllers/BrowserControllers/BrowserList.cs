﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Support.Extensions;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Controllers.DataAccessors;
using Epsitec.Cresus.DataLayer;
using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.BrowserControllers
{
	public class BrowserList : IEnumerable<BrowserListItem>
	{
		public BrowserList(DataContext context)
		{
			this.list = new List<BrowserListItem> ();
			this.context = context;
		}

		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		public void DefineEntities(IEnumerable<AbstractEntity> entities)
		{
			this.list.Clear ();
			this.list.AddRange (entities.Select (x => new BrowserListItem (this, x)));
		}

		public void Invalidate()
		{
			this.list.ForEach (x => x.ClearCachedDisplayText ());
		}

		public EntityKey? GetEntityKey(int index)
		{
			if ((index >= 0) &&
				(index < this.list.Count))
			{
				var item = this.list[index];
				return item.EntityKey;
			}
			else
			{
				return null;
			}
		}

		public int GetIndex(EntityKey? key)
		{
			if (key == null)
			{
				return -1;
			}

			var rowKey = key.Value.RowKey;

			return this.list.FindIndex (x => x.EntityKey.RowKey == rowKey);
		}

		internal FormattedText GenerateEntityDisplayText(AbstractEntity entity)
		{
			if (entity == null)
			{
				return FormattedText.Empty;
			}

			if (entity is LegalPersonEntity)
			{
				var person = entity as LegalPersonEntity;
				return person.GetCompactSummary ();
			}

			if (entity is NaturalPersonEntity)
			{
				var person = entity as NaturalPersonEntity;
				return person.GetCompactSummary ();
			}

			if (entity is RelationEntity)
			{
				var customer = entity as RelationEntity;
				return customer.GetCompactSummary ();
			}

			if (entity is ArticleDefinitionEntity)
			{
				var article = entity as ArticleDefinitionEntity;
				return article.GetCompactSummary ();
			}

			if (entity is DocumentEntity)
			{
				var document = entity as DocumentEntity;
				return document.GetCompactSummary ();
			}

			if (entity is BusinessSettingsEntity)
			{
				var settings = entity as BusinessSettingsEntity;
				return settings.GetCompactSummary ();
			}

			return FormattedText.Empty;
		}


        internal EntityKey GetEntityKey(AbstractEntity entity)
		{
			if (entity == null)
			{
				throw new System.ArgumentNullException ("entity");
			}

			var key = this.context.GetNormalizedEntityKey (entity);

			if (key == null)
			{
				throw new System.ArgumentException ("Cannot resolve entity");
			}

			return key.Value;
		}

		internal static string ValueConverterFunction(object value)
		{
			BrowserListItem item = value as BrowserListItem;

			if (item == null)
			{
				return "";
			}
			else
			{
				return item.DisplayText.ToString ();
			}
		}
		
		private readonly List<BrowserListItem> list;
		private readonly DataContext context;

		#region IEnumerable<BrowserListItem> Members

		public IEnumerator<BrowserListItem> GetEnumerator()
		{
			return this.list.GetEnumerator ();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator ();
		}

		#endregion
	}
}