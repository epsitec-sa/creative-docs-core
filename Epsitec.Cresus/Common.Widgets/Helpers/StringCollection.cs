namespace Epsitec.Common.Widgets.Helpers
{
	public class StringCollection : System.Collections.IList, System.IDisposable
	{
		public StringCollection(IStringCollectionHost host)
		{
			this.host  = host;
			this.list  = new System.Collections.ArrayList ();
			this.names = new System.Collections.ArrayList ();
		}
		
		public string							this[int index]
		{
			get
			{
				if (index == -1) return null;
				return this.list[index].ToString ();
			}
		}
		
		public int Add(string name, object value)
		{
			int index_0 = this.list.Add (value);
			int index_1 = this.names.Add (name);
			
			System.Diagnostics.Debug.Assert (index_0 == index_1);
			
			this.HandleInsert (value);
			this.HandleChange ();
			
			return index_0;
		}
		
		
		public int FindNameIndex(string name)
		{
			return this.names.IndexOf (name);
		}
		
		public void SetName(int index, string name)
		{
			this.names[index] = name;
		}

		public string GetName(int index)
		{
			return this.names[index] as string;
		}

		
		public int FindExactMatch(string find)
		{
			find = find.ToUpper ();
			
			for (int i = 0; i < this.list.Count; i++)
			{
				string text = this[i].ToUpper ();
				
				if (text == find)
				{
					return i;
				}
			}
			
			return -1;
		}
		
		public int FindStartMatch(string find)
		{
			find = find.ToUpper ();
			
			for (int i = 0; i < this.list.Count; i++)
			{
				string text = this[i].ToUpper ();
				
				if (text.StartsWith (find))
				{
					return i;
				}
			}
			
			return -1;
		}
		
		
		public void Dispose()
		{
			this.Dispose (true);
			System.GC.SuppressFinalize (this);
		}
		
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.list.Clear ();
				this.names.Clear ();
				this.list = null;
				this.names = null;
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
			object item = this.list[index];
			this.HandleRemove (item);
			this.list.RemoveAt (index);
			this.names.RemoveAt (index);
			this.HandleChange ();
		}

		public void Insert(int index, object value)
		{
			this.list.Insert (index, value);
			this.names.Insert (index, null);
			this.HandleInsert (value);
			this.HandleChange ();
		}

		public void Remove(object value)
		{
			int index = this.list.IndexOf (value);
			if (index >= 0)
			{
				this.HandleRemove (value);
				this.list.RemoveAt (index);
				this.names.RemoveAt (index);
				this.HandleChange ();
			}
		}

		public bool Contains(object value)
		{
			return this.Contains (value);
		}

		public void Clear()
		{
			foreach (object item in this.list)
			{
				this.HandleRemove (item);
			}
			this.list.Clear ();
			this.names.Clear ();
			this.HandleChange ();
		}

		public int IndexOf(object value)
		{
			return this.list.IndexOf (value);
		}

		public int Add(object value)
		{
			int index_0 = this.list.Add (value);
			int index_1 = this.names.Add (null);
			
			System.Diagnostics.Debug.Assert (index_0 == index_1);
			
			this.HandleInsert (value);
			this.HandleChange ();
			
			return index_0;
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
		
		protected virtual void HandleInsert(object item)
		{
		}
		
		protected virtual void HandleRemove(object item)
		{
		}
		
		protected virtual void HandleChange()
		{
			if (this.host != null)
			{
				this.host.StringCollectionChanged ();
			}
		}
		
		
		private IStringCollectionHost			host;
		private System.Collections.ArrayList	list;
		private System.Collections.ArrayList	names;
	}
}
