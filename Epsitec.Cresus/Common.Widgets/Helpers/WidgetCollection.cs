namespace Epsitec.Common.Widgets.Helpers
{
	public class WidgetCollection : System.Collections.IList, System.IDisposable
	{
		public WidgetCollection(IWidgetCollectionHost host)
		{
			this.host = host;
			this.list = new System.Collections.ArrayList ();
		}
		
		public Widget this[int index]
		{
			get
			{
				return this.list[index] as Widget;
			}
		}
		
		public Widget this[string name]
		{
			get
			{
				foreach (Widget item in this.list)
				{
					if (item.Name == name)
					{
						return item;
					}
				}
				return null;
			}
		}
		
		
		public void Dispose()
		{
			System.Diagnostics.Debug.Assert (this.list.Count == 0);
			
			this.host = null;
			this.list = null;
		}
		
		
		public bool								AutoEmbedding
		{
			get
			{
				return this.auto_embedding;
			}
			set
			{
				this.auto_embedding = value;
			}
		}
		
		public virtual void RestoreFromBundle(string items_name, Support.ObjectBundler bundler, Support.ResourceBundle bundle)
		{
			Support.ResourceBundle.FieldList item_list = bundle[items_name].AsList;
			
			if (item_list != null)
			{
				//	Le bundle pass� en entr�e contient une liste de sous-bundles contenant les
				//	descriptions des widgets � placer dans la collection :
				
				foreach (Support.ResourceBundle.Field field in item_list)
				{
					Support.ResourceBundle item_bundle = field.AsBundle;
					Widget                 item_widget = bundler.CreateFromBundle (item_bundle) as Widget;
					
					this.Add (item_widget);
				}
			}
		}
		
		public virtual void SerializeToBundle(string items_name, Support.ObjectBundler bundler, Support.ResourceBundle bundle)
		{
			int n = this.list.Count;
			
			if (n > 0)
			{
				Support.ResourceBundle[] bundles = new Support.ResourceBundle[n];
				Widget[]                 widgets = new Widget[n];
				
				this.list.CopyTo (widgets, 0);
				
				for (int i = 0; i < n; i++)
				{
					bundles[i] = bundler.CreateEmptyBundle (widgets[i].BundleName);
					bundler.FillBundleFromObject (bundles[i], widgets[i]);
				}
				
				Support.ResourceBundle.Field field = bundle.CreateField (Support.ResourceFieldType.List, bundles);
				
				field.SetName (items_name);
				
				bundle.Add (field);
			}
		}
		
		
		#region IList Members
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		object System.Collections.IList.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		public void RemoveAt(int index)
		{
			Widget item = this.list[index] as Widget;
			this.HandleRemove (item);
			this.list.RemoveAt (index);
			this.HandlePostRemove (item);
		}

		public void Insert(int index, object value)
		{
			this.list.Insert (index, value);
			this.HandleInsert (value as Widget);
		}

		public void Remove(object value)
		{
			this.HandleRemove (value as Widget);
			this.list.Remove (value);
			this.HandlePostRemove (value as Widget);
		}

		public bool Contains(object value)
		{
			return this.list.Contains (value);
		}

		public void Clear()
		{
			Widget[] widgets = new Widget[this.list.Count];
			this.list.CopyTo (widgets, 0);
			
			for (int i = 0; i < widgets.Length; i++)
			{
				this.HandleRemove (widgets[i]);
			}
			
			this.list.Clear ();
			
			for (int i = 0; i < widgets.Length; i++)
			{
				this.HandlePostRemove (widgets[i]);
			}
		}

		public int IndexOf(object value)
		{
			return this.list.IndexOf (value);
		}

		public int Add(object value)
		{
			if (value is Widget)
			{
				int index = this.list.Add (value);
				this.HandleInsert (value as Widget);
				return index;
			}
			
			throw new System.ArgumentException ("Expecting Widget, got " + value.GetType ().Name);
		}

		public bool IsFixedSize
		{
			get
			{
				return this.list.IsFixedSize;
			}
		}
		#endregion
		
		#region ICollection Members
		public bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		public void CopyTo(System.Array array, int index)
		{
			this.list.CopyTo (array, index);
		}

		public object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		#endregion
		
		#region IEnumerable Members
		public System.Collections.IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator ();
		}
		#endregion
		
		protected void HandleInsert(Widget item)
		{
			if (this.auto_embedding)
			{
				Widget embedder = this.host as Widget;
				
				System.Diagnostics.Debug.Assert (embedder != null);
				System.Diagnostics.Debug.Assert (item.Parent == null);
				
				item.SetEmbedder (embedder);
			}
			
			this.RenumberItems ();
			this.host.NotifyInsertion (item);
		}
		
		protected void HandleRemove(Widget item)
		{
			this.host.NotifyRemoval (item);
			this.RenumberItems ();
		}
		
		protected void HandlePostRemove(Widget item)
		{
			item.Index = -1;
			this.host.NotifyPostRemoval (item);
		}
		
		protected void RenumberItems()
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				Widget item = this.list[i] as Widget;
				item.Index = i;
			}
		}
		
		
		private System.Collections.ArrayList	list;
		private IWidgetCollectionHost			host;
		private bool							auto_embedding;
	}
}
