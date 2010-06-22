﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Database;
using Epsitec.Cresus.DataLayer;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers
{
	class BrowserViewController : CoreViewController, INotifyCurrentChanged
	{
		public BrowserViewController(string name, CoreData data)
			: base (name)
		{
			this.data       = data;
			this.collection = new List<AbstractEntity> ();
			this.data.DataContextChanged +=
				delegate
				{
					this.UpdateCollection ();
				};
		}

		public override IEnumerable<CoreController> GetSubControllers()
		{
			yield break;
		}

		private static T GetActiveItem<T>(IList<T> collection, int index)
		{
			if (index < 0)
			{
				return default (T);
			}
			else
			{
				return collection[index];
			}
		}

		public override void CreateUI(Widget container)
		{
			var frame = new FrameBox ()
			{
				Parent = container,
				Dock = DockStyle.Fill,
			};

			//	TODO: widgets to manage the list
			var label = new StaticText ()
			{
				Parent = frame,
				Anchor = AnchorStyles.Top | AnchorStyles.LeftAndRight,
				Text = @"<font size=""120%"">Clients</font>",
				Margins = new Common.Drawing.Margins (0, 0, 0, 0),
				PreferredHeight = 26,
				ContentAlignment = Common.Drawing.ContentAlignment.MiddleCenter,
			};

			this.scrollList = new ScrollList ()
			{
				Parent = frame,
				Anchor = AnchorStyles.All,
				ScrollListStyle = ScrollListStyle.Standard,
				Margins = new Common.Drawing.Margins (-1, -1, 26, -1),
			};

			this.scrollList.SelectedItemChanged +=
				delegate
				{
					if (this.suspendUpdates == 0)
					{
						int active = this.scrollList.SelectedItemIndex;
						var entity = BrowserViewController.GetActiveItem (this.collection, active);
						var key    = DataContextPool.Instance.FindEntityKey (entity);

						if (this.activeEntityKey != key )
						{
							this.activeEntityKey = key;
							this.OnCurrentChanging (new CurrentChangingEventArgs (isCancelable: false));
							this.OnCurrentChanged ();
						}
					}
				};
			
			this.RefreshScrollList ();
		}


		public AbstractEntity GetActiveEntity()
		{
			if (this.activeEntityKey.IsEmpty)
			{
				return null;
			}

			int active   = this.scrollList.SelectedItemIndex;
			var entity   = BrowserViewController.GetActiveItem (this.collection, active);
			var entityId = entity.GetEntityStructuredTypeId ();

			entity = this.data.DataContext.ResolveEntity (this.activeEntityKey);

			return entity;
		}


		public void SetContents(System.Func<IEnumerable<AbstractEntity>> collectionGetter)
		{
			this.collectionGetter = collectionGetter;
			this.UpdateCollection ();
		}

		private void UpdateCollection()
		{
			this.OnCurrentChanging (new CurrentChangingEventArgs (isCancelable: false));
			this.collection.Clear ();
			this.collection.AddRange (this.collectionGetter ());
			this.RefreshScrollList ();
		}
		
		protected void OnCurrentChanged()
		{
			var handler = this.CurrentChanged;

			if (handler != null)
			{
				handler (this);
			}
		}

		protected void OnCurrentChanging(CurrentChangingEventArgs e)
		{
			var handler = this.CurrentChanging;

			if (handler != null)
			{
				handler (this, e);
			}
		}

		private void RefreshScrollList()
		{
			if (this.scrollList != null)
			{
				var updatedList = new List<FormattedText> ();

				foreach (var entity in this.collection)
				{
					updatedList.Add (BrowserViewController.GetEntityDisplayText (entity));
				}

				int oldActive = this.scrollList.SelectedItemIndex;
				int newActive = oldActive < updatedList.Count ? oldActive : updatedList.Count-1;

				this.suspendUpdates++;
				this.scrollList.Items.Clear ();
				this.scrollList.Items.AddRange (updatedList.Select (x => x.ToString ()));
				this.suspendUpdates--;
				
				this.scrollList.SelectedItemIndex = newActive;
			}
		}

		private static FormattedText GetEntityDisplayText(AbstractEntity entity)
		{
			if (entity == null)
			{
				return FormattedText.Empty;
			}

			if (entity is LegalPersonEntity)
			{
				var person = entity as LegalPersonEntity;
				return UIBuilder.FormatText (person.Name);
			}
			if (entity is NaturalPersonEntity)
			{
				var person = entity as NaturalPersonEntity;
				return UIBuilder.FormatText (person.Firstname, person.Lastname);
			}
			if (entity is CustomerEntity)
			{
				var customer = entity as CustomerEntity;
				return UIBuilder.FormatText (BrowserViewController.GetEntityDisplayText (customer.Person), customer.DefaultAddress.Location.PostalCode, customer.DefaultAddress.Location.Name);
			}
			
			return FormattedText.Empty;
		}

		#region INotifyCurrentChanged Members

		public event EventHandler  CurrentChanged;

		public event EventHandler<CurrentChangingEventArgs>  CurrentChanging;

		#endregion

		private readonly CoreData data;
		private readonly List<AbstractEntity> collection;
		private System.Func<IEnumerable<AbstractEntity>> collectionGetter;
		private int suspendUpdates;

		private ScrollList scrollList;
		private EntityKey activeEntityKey;
	}
}
