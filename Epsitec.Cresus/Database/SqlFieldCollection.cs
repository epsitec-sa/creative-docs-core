namespace Epsitec.Cresus.Database
{
	/// <summary>
	/// La classe SqlFieldCollection encapsule une collection d'instances de type SqlField.
	/// SqlField.
	/// </summary>
	public class SqlFieldCollection : InternalCollectionList
	{
		public SqlFieldCollection()
		{
		}
		
		
		public virtual void Add(SqlField field)
		{
			this.List.Add (field);
		}

		public virtual void AddRange(SqlField[] fields)
		{
			if (fields == null)
			{
				return;
			}
			
			this.List.AddRange (fields);
		}
		
		public virtual void Remove(SqlField field)
		{
			this.List.Remove (field);
		}
		
		
		public virtual bool Contains(SqlField field)
		{
			return this.List.Contains (field);
		}
		
		public virtual int IndexOf(SqlField field)
		{
			return this.List.IndexOf (field);
		}
		
		public override int IndexOf(string field_alias)
		{
			for (int i = 0; i < this.List.Count; i++)
			{
				SqlField field = this.List[i] as SqlField;
				
				if (field.Alias == field_alias)
				{
					return i;
				}
			}
			
			return -1;
		}
		
		
		
		public virtual SqlField							this[int index]
		{
			get
			{
				return this.List[index] as SqlField;
			}
		}
		
		public virtual SqlField							this[string field_alias]
		{
			get
			{
				int index = this.IndexOf (field_alias);
				
				if (index >= 0)
				{
					return this[index];
				}
				
				return null;
			}
		}
	}
}
