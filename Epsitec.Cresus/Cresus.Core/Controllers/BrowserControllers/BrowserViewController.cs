﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
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
	public class BrowserViewController : CoreViewController, INotifyCurrentChanged
	{
		public BrowserViewController(string name, CoreData data)
			: base (name)
		{
			this.data = data;
		}

		
		public override DataContext DataContext
		{
			get
			{
				return base.DataContext;
			}
			set
			{
				throw new System.InvalidOperationException ("Cannot set DataContext");
			}
		}

		public FrameBox SettingsPanel
		{
			get
			{
				return this.settingsPanel;
			}
		}

		public string DataSetName
		{
			get
			{
				return this.dataSetName;
			}
		}


		public void SelectDataSet(string dataSetName)
		{
			if (this.dataSetName != dataSetName)
			{
				this.DisposeDataContext ();
				this.dataSetName = dataSetName;
				this.CreateDataContext ();

				this.SelectContentsBasedOnDataSet ();
				this.OnDataSetSelected ();
			}
		}

		public void SelectActiveEntity(DataViewController dataViewController)
		{
			var dataContext           = dataViewController.DataContext;
			var activeEntity          = this.GetActiveEntity (dataContext);
			var activeEntityKey       = dataContext.GetEntityKey (activeEntity).GetValueOrDefault ();
			var navigationPathElement = new BrowserNavigationPathElement (this, activeEntityKey);

			dataViewController.SetActiveEntity (activeEntity, navigationPathElement);
		}

		public void CreateNewItem()
		{
			var item = this.data.CreateNewEntity (this.DataSetName, EntityCreationScope.Independent);

			if (item != null)
			{
				var controller = EntityViewController.CreateEntityViewController ("ItemCreation", item, ViewControllerMode.Creation, this.Orchestrator);
				this.Orchestrator.ShowSubView (null, controller);
			}
		}

		public AbstractEntity GetActiveEntity(DataContext context)
		{
			if (this.activeEntityKey == null)
			{
				return null;
			}

			int active    = this.scrollList.SelectedItemIndex;
			var entityKey = this.collection.GetEntityKey (active);

			if (entityKey.HasValue)
			{
				return context.ResolveEntity (entityKey.Value);
			}
			else
			{
				return null;
			}
		}


		public override IEnumerable<CoreController> GetSubControllers()
		{
			yield break;
		}

		public override void CreateUI(Widget container)
		{
			var frame = new FrameBox ()
			{
				Parent = container,
				Dock = DockStyle.Fill,
			};

			this.settingsPanel = new FrameBox
			{
				Parent = frame,
				Dock = DockStyle.Top,
				PreferredHeight = 28,
			};

			var listFrame = new FrameBox
			{
				Parent = frame,
				Dock = DockStyle.Fill,
			};

			this.scrollList = new ScrollList ()
			{
				Parent = listFrame,
				Anchor = AnchorStyles.All,
				ScrollListStyle = ScrollListStyle.Standard,
				Margins = new Common.Drawing.Margins (-1, -1, -1, -1),
			};

			this.scrollList.Items.ValueConverter = BrowserList.ValueConverterFunction;

			this.scrollList.SelectedItemChanged += this.HandleScrollListSelectedItemChanged;

			this.RefreshScrollList ();
		}


		private void CreateDataContext()
		{
			base.DataContext = data.CreateDataContext ();
			this.DataContext.Name = string.Format ("Browser.DataSet={0}", this.DataSetName);
			this.collection  = new BrowserList (this.DataContext);

			this.DataContext.EntityChanged += this.HandleDataContextEntityChanged;
		}

		private void DisposeDataContext()
		{
			if (this.DataContext != null)
			{
				this.DataContext.EntityChanged -= this.HandleDataContextEntityChanged;
				this.data.DisposeDataContext (this.DataContext);
				base.DataContext = null;
				this.collection = null;
			}
		}

		private void HandleDataContextEntityChanged(object sender, EntityChangedEventArgs e)
		{
			if ((this.collection != null) &&
				(this.scrollList != null))
			{
				if (this.scrollList.InvalidateTextLayouts ())
				{
					this.collection.Invalidate ();
				}
			}
		}

		private void HandleScrollListSelectedItemChanged(object sender)
		{
			if (this.suspendUpdates == 0)
			{
				this.NotifySelectedItemChange ();
			}
		}
		
		private void SelectContentsBasedOnDataSet()
		{
			switch (this.dataSetName)
			{
				case "Customers":
					this.SetContents (context => this.data.GetCustomers (context));
					break;

				case "ArticleDefinitions":
					this.SetContents (context => this.data.GetArticleDefinitions (context));
					break;

				case "InvoiceDocuments":
					this.SetContents (context => this.data.GetInvoiceDocuments (context));
					break;
			}
		}
	
		private void SetContents(System.Func<DataContext, IEnumerable<AbstractEntity>> collectionGetter)
		{
			//	When switching to some other contents, the browser first has to ensure that the
			//	UI no longer has an actively selected entity; clearing the active entity will
			//	also make sure that any changes will be automatically persisted:

			this.Orchestrator.Controller.ClearActiveEntity ();

			this.collectionGetter = collectionGetter;
			this.data.SetupDataContext ();
			this.UpdateCollection ();
		}


		private AbstractEntity[] GetCollectionEntities()
		{
			if (this.collectionGetter == null)
			{
				return new AbstractEntity[0];
			}
			else
			{
				return this.collectionGetter (this.DataContext).ToArray ();
			}
		}
		
		private void UpdateCollection()
		{
			if (this.collectionGetter != null)
			{
				this.collection.DefineEntities (this.GetCollectionEntities ());
				this.RefreshScrollList (reset: true);
			}
		}
		
		private void NotifySelectedItemChange()
		{
			int active    = this.scrollList.SelectedItemIndex;
			var entityKey = this.collection == null ? null : this.collection.GetEntityKey (active);

			System.Diagnostics.Debug.WriteLine (string.Format ("SelectedItemChanged : old key = {0} / new key = {1}", this.activeEntityKey, entityKey));

			if (this.activeEntityKey != entityKey)
			{
				this.activeEntityKey = entityKey;
				this.OnCurrentChanging (new CurrentChangingEventArgs (isCancelable: false));
				this.OnCurrentChanged ();
			}
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

		protected void OnDataSetSelected()
		{
			var handler = this.DataSetSelected;

			if (handler != null)
			{
				handler (this);
			}
		}

		
		private void RefreshScrollList(bool reset = false)
		{
			if (this.scrollList != null)
			{
				int newCount = this.collection == null ? 0 : this.collection.Count;

				int oldActive = reset ? 0 : this.scrollList.SelectedItemIndex;
				int newActive = oldActive < newCount ? oldActive : newCount-1;

				this.suspendUpdates++;
				this.scrollList.Items.Clear ();
				this.scrollList.Items.AddRange (this.collection);
				this.scrollList.SelectedItemIndex = newActive;
				this.suspendUpdates--;

				this.NotifySelectedItemChange ();
			}
		}

		#region INotifyCurrentChanged Members

		public event EventHandler  CurrentChanged;

		public event EventHandler<CurrentChangingEventArgs>  CurrentChanging;

		#endregion

		
		private class BrowserNavigationPathElement : Epsitec.Cresus.Core.Orchestrators.Navigation.NavigationPathElement
		{
			public BrowserNavigationPathElement(BrowserViewController controller, EntityKey entityKey)
			{
				this.dataSetName = controller.DataSetName;
				this.entityKey   = entityKey;
			}

			public override string ToString()
			{
				return string.Concat ("<Browser:", this.dataSetName, ":", this.entityKey.RowKey.ToString (), ">");
			}


			private readonly string dataSetName;
			private readonly EntityKey entityKey;
		}
		
		
		public event EventHandler				DataSetSelected;

		private readonly CoreData data;
		private BrowserList collection;
		private string dataSetName;
		private System.Func<DataContext, IEnumerable<AbstractEntity>> collectionGetter;
		private int suspendUpdates;

		private ScrollList scrollList;
		private EntityKey? activeEntityKey;
		private FrameBox settingsPanel;
	}
}
