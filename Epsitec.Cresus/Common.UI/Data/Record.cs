//	Copyright � 2004, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.UI.Data
{
	/// <summary>
	/// La classe Record d�crit un ensemble de champs utilis�s pour �changer des
	/// donn�es entre une application et son interface via mapper/binder/...
	/// </summary>
	public class Record : Types.AbstractDataCollection, Types.IDataFolder, Types.IDataGraph, Support.Data.IChangedSource
	{
		public Record()
		{
			this.graph = new Types.DataGraph (this);
		}
		
		public Record(string name) : this (name, null)
		{
		}
		
		public Record(string name, string prefix) : this ()
		{
			this.DefineName (name);
			this.DefineResourcePrefix (prefix);
		}
		
		
		public new Field						this[string name]
		{
			get
			{
				return base[name] as Field;
			}
		}

		public new Field						this[int index]
		{
			get
			{
				return base[index] as Field;
			}
		}
		
		public Support.IValidator				Validator
		{
			get
			{
				if (this.validator == null)
				{
					this.validator = new RecordValidator (this);
				}
				
				return this.validator;
			}
		}
		
		
		public void DefineName(string name)
		{
			if (name == "")
			{
				name = null;
			}
			
			this.name = name;
		}
		
		public void DefineResourcePrefix(string prefix)
		{
			if (prefix == "")
			{
				prefix = null;
			}
			
			this.resource_prefix = prefix;
		}
		
		
		
		public override void Add(Types.IDataItem item)
		{
			Field field = item as Field;
			
			if (field == null)
			{
				throw new System.ArgumentException ("Expected a valid field.", "item");
			}
			
			base.Add (field);
			
			field.AttachToRecord (this);
			
			this.OnChanged ();
		}
		
		public void AddRange(System.Collections.ICollection fields)
		{
			if (fields.Count > 0)
			{
				foreach (object field in fields)
				{
					if (!(field is Field))
					{
						throw new System.ArgumentException ("Collection contains invalid field.", "fields");
					}
				}
				
				this.list.AddRange (fields);
				this.ClearCachedItemArray ();
				
				foreach (Field field in fields)
				{
					field.AttachToRecord (this);
				}
				
				this.OnChanged ();
			}
		}
		
		public void AddGraph(Types.IDataGraph graph)
		{
			if ((graph == null) ||
				(graph.Root == null))
			{
				return;
			}
			
			int count = graph.Root.Count;
			
			if (count == 0)
			{
				return;
			}
			
			Types.IDataItem[]            items = new Types.IDataItem[count];
			System.Collections.ArrayList list  = new System.Collections.ArrayList ();
			
			graph.Root.CopyTo (items, 0);
			
			for (int i = 0; i < count; i++)
			{
				if (items[i].Classes == Types.DataItemClasses.Value)
				{
					list.Add (Field.CreateFromValue (items[i] as Types.IDataValue));
				}
			}
			
			this.AddRange (list);
		}
		
		public void Clear()
		{
			foreach (Field field in this.list)
			{
				field.DetachFromRecord (this);
			}
			
			this.list.Clear ();
			this.ClearCachedItemArray ();
			this.OnChanged ();
		}
		
		
		public Field AddField(string name)
		{
			return this.InitFieldAndAdd (new Field (name));
		}
		
		public Field AddField(string name, object value)
		{
			return this.InitFieldAndAdd (new Field (name, value));
		}
		
		public Field AddField(string name, object value, Types.INamedType type)
		{
			return this.InitFieldAndAdd (new Field (name, value, type));
		}
		
		public Field AddField(string name, object value, Types.INamedType type, Types.IDataConstraint constraint)
		{
			return this.InitFieldAndAdd (new Field (name, value, type, constraint));
		}
		
		
		public Types.IDataValue[] GetDataValues()
		{
			if (this.fields == null)
			{
				this.UpdateCachedItemArray ();
			}
			
			Types.IDataValue[] values = new Types.IDataValue[this.fields.Length];
			this.fields.CopyTo (values, 0);
			
			return values;
		}
		
		
		public Record Clone()
		{
			System.ICloneable cloneable = this;

			return cloneable.Clone () as Record;
		}
		
		
		#region IDataItem Members
		public Types.DataItemClasses			Classes
		{
			get
			{
				return Types.DataItemClasses.Folder;
			}
		}
		#endregion
		
		#region INameCaption Members
		public string							Description
		{
			get
			{
				return null;
			}
		}
		
		public string							Caption
		{
			get
			{
				return null;
			}
		}
		
		public string							Name
		{
			get
			{
				return this.name;
			}
		}
		#endregion
		
		#region IDataGraph Members
		public Types.IDataCollection Select(string query)
		{
			return this.graph.Select (query);
		}
		
		public Types.IDataFolder						Root
		{
			get
			{
				return this;
			}
		}
		
		public Types.IDataItem Navigate(string path)
		{
			return this.graph.Navigate (path);
		}
		#endregion
		
		protected override object CloneNewObject()
		{
			return new Record ();
		}
		
		protected override object CloneCopyToNewObject(object o)
		{
			Record that = o as Record;
			
			base.CloneCopyToNewObject (o);
			
			that.DefineName (this.name);
			that.DefineResourcePrefix (this.resource_prefix);
			
			return that;
		}
		
		protected virtual Field InitFieldAndAdd(Field field)
		{
			string field_name = field.Name;
			
			if ((field_name != null) &&
				(field_name.Length > 0) &&
				(this.resource_prefix != null) &&
				(this.name != null))
			{
				//	Les valeurs de 'caption' et 'description' sont recherch�es dans les ressources d�finies
				//	par un chemin de type "base:records#RecordName.FieldName.capt" et "...desc".
				
				string caption     = string.Concat (this.resource_prefix, "#", this.name, ".", field_name, ".", Support.Tags.Caption);
				string description = string.Concat (this.resource_prefix, "#", this.name, ".", field_name, ".", Support.Tags.Description);
				
				field.DefineCaption (Support.Resources.MakeTextRef (caption));
				field.DefineDescription (Support.Resources.MakeTextRef (description));
			}
			
			this.Add (field);
			
			return field;
		}
		
		
		protected virtual void OnChanged()
		{
			if (this.validator != null)
			{
				this.validator.MakeDirty (false);
			}
			
			if (this.Changed != null)
			{
				this.Changed (this);
			}
		}
		
		protected virtual void OnFieldChanged(Field field)
		{
			if (this.validator != null)
			{
				this.validator.MakeDirty (false);
			}
			
			if (this.FieldChanged != null)
			{
				this.FieldChanged (field);
			}
		}
		
		
		protected override Types.IDataItem[] GetCachedItemArray()
		{
			return this.fields;
		}
		
		protected override void ClearCachedItemArray()
		{
			base.ClearCachedItemArray ();
			
			this.fields = null;
		}
		
		protected override void UpdateCachedItemArray()
		{
			base.UpdateCachedItemArray ();
			
			this.fields = new Field[this.list.Count];
			this.list.CopyTo (this.fields);
		}

		
		internal void NotifyFieldChanged(Field field)
		{
			this.OnFieldChanged (field);
		}
		
		
		#region RecordValidator Class
		protected class RecordValidator : Support.IValidator
		{
			public RecordValidator(Record record)
			{
				this.record = record;
				this.state  = Support.ValidationState.Dirty;
			}
			
			
			public Support.ValidationState		State
			{
				get
				{
					return this.state;
				}
			}
			
			public bool							IsValid
			{
				get
				{
					if (this.state == Support.ValidationState.Dirty)
					{
						this.Validate ();
					}
				
					return (this.state == Support.ValidationState.Ok);
				}
			}
			
			public string						ErrorMessage
			{
				get
				{
					return null;
				}
			}
			
			
			public void Validate()
			{
				this.state = Support.ValidationState.Unknown;
				
				foreach (Field field in this.record.CachedItemArray)
				{
					if (field.IsValueValid == false)
					{
						this.state = Support.ValidationState.Error;
						return;
					}
				}
				
				this.state = Support.ValidationState.Ok;
			}
			
			public void MakeDirty(bool deep)
			{
				this.state = Support.ValidationState.Dirty;
				this.OnBecameDirty ();
			}
			
			
			protected void OnBecameDirty()
			{
				if (this.BecameDirty != null)
				{
					this.BecameDirty (this);
				}
			}
			
			
			public event Support.EventHandler	BecameDirty;
			
			private Record						record;
			private Support.ValidationState		state;
		}
		#endregion
		
		public event Support.EventHandler		Changed;
		public event Support.EventHandler		FieldChanged;
		
		private Field[]							fields;
		private Types.DataGraph					graph;
		private string							name;
		private string							resource_prefix;
		private RecordValidator					validator;
	}
}
