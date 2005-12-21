using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;
using System.Runtime.Serialization;

namespace Epsitec.Common.Document.Properties
{
	/// <summary>
	/// La classe Aggregate repr�sente une collection de styles.
	/// </summary>
	[System.Serializable()]
	public class Aggregate : ISerializable
	{
		public Aggregate(Document document)
		{
			this.document = document;
			this.styles = new UndoableList(this.document, UndoableListType.StylesInsideAggregate);
			this.childrens = new UndoableList(this.document, UndoableListType.AggregatesChildrens);
		}


		public string AggregateName
		{
			//	Nom de l'agr�gat.
			get
			{
				return this.aggregateName;
			}

			set
			{
				if ( this.aggregateName != value )
				{
					this.InsertOpletAggregate();
					this.aggregateName = value;
				}
			}
		}

		public UndoableList Styles
		{
			//	Liste des styles de l'agr�gat.
			get
			{
				return this.styles;
			}
		}

		public UndoableList Childrens
		{
			//	Liste des fils de l'agr�gat.
			get
			{
				return this.childrens;
			}
		}

		public Properties.Abstract Property(Properties.Type type, bool deep)
		{
			//	Donne une propri�t� de l'agr�gat.
			if ( deep )  return this.PropertyDeep(type);
			else         return this.Property(type);
		}

		public Properties.Abstract Property(Properties.Type type)
		{
			//	Donne une propri�t� de l'agr�gat.
			foreach ( Properties.Abstract property in this.styles )
			{
				if ( property.Type == type )  return property;
			}
			return null;
		}

		public Properties.Abstract PropertyDeep(Properties.Type type)
		{
			//	Donne une propri�t� de l'agr�gat ou de l'un des agr�gats enfants.
			return this.PropertyDeep(type, 0);
		}
		
		protected Properties.Abstract PropertyDeep(Properties.Type type, int deep)
		{
			if ( deep > 10 )  return null;

			Properties.Abstract property = this.Property(type);
			if ( property != null )  return property;

			if ( this.childrens.Count != 0 )
			{
				foreach ( Properties.Aggregate children in this.childrens )
				{
					property = children.PropertyDeep(type, deep+1);
					if ( property != null )  return property;
				}
			}
			return null;
		}

		public bool Contains(Properties.Abstract property)
		{
			//	Indique si l'agr�gat contient une propri�t�.
			return this.styles.Contains(property);
		}

		public bool IsUsedByObject(Objects.Abstract obj)
		{
			//	V�rifie si un objet utilise cet agr�gat.
			return this.IsUsedByObject(obj, 0);
		}
		
		protected bool IsUsedByObject(Objects.Abstract obj, int deep)
		{
			if ( deep > 10 )  return false;

			UndoableList list = obj.Aggregates;
			for ( int i=0 ; i<list.Count ; i++ )
			{
				Properties.Aggregate agg = list[i] as Properties.Aggregate;

				if ( agg == this )  return true;

				if ( this.childrens.Count != 0 )
				{
					foreach ( Properties.Aggregate children in this.childrens )
					{
						if ( children.IsUsedByObject(obj, deep+1) )  return true;
					}
				}
			}
			return false;
		}


		public void CopyTo(Aggregate dst)
		{
			//	Copie tout l'agr�gat.
			dst.aggregateName = this.aggregateName;
			this.styles.CopyTo(dst.styles);
			this.childrens.CopyTo(dst.childrens);
		}

		public void DuplicateTo(Aggregate dst)
		{
			//	Duplique tout l'agr�gat.
			dst.aggregateName = this.aggregateName;
			this.childrens.CopyTo(dst.childrens);

			foreach ( Properties.Abstract srcProp in this.styles )
			{
				Abstract newProp = Abstract.NewProperty(this.document, srcProp.Type);
				srcProp.CopyTo(newProp);
				dst.styles.Add(newProp);
			}
		}


		public static int UniqueId(UndoableList aggregates, Properties.Abstract property)
		{
			//	Donne un identificateur unique pour une propri�t�.
			int id = (int) property.Type;

			if ( property.IsStyle )
			{
				id = 100;
				foreach ( Properties.Aggregate agg in aggregates )
				{
					int index = agg.Styles.IndexOf(property);
					if ( index == -1 )
					{
						id += agg.Styles.Count;
					}
					else
					{
						id += index;
						break;
					}
				}
			}

			return id;
		}


		#region OpletAggregate
		protected void InsertOpletAggregate()
		{
			//	Ajoute un oplet pour m�moriser l'agr�gat.
			if ( !this.document.Modifier.OpletQueueEnable )  return;
			OpletAggregate oplet = new OpletAggregate(this);
			this.document.Modifier.OpletQueue.Insert(oplet);
		}

		//	M�morise l'agr�gat.
		protected class OpletAggregate : AbstractOplet
		{
			public OpletAggregate(Aggregate host)
			{
				this.host = host;
				this.name = host.aggregateName;
			}

			protected void Swap()
			{
				string temp = this.host.aggregateName;
				this.host.aggregateName = this.name;
				this.name = temp;

				this.host.document.Notifier.NotifyStyleChanged();
			}

			public override IOplet Undo()
			{
				this.Swap();
				return this;
			}

			public override IOplet Redo()
			{
				this.Swap();
				return this;
			}

			protected Aggregate				host;
			protected string				name;
		}
		#endregion

		
		#region Serialization
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			//	S�rialise l'agr�gat.
			info.AddValue("AggregateName", this.aggregateName);
			info.AddValue("Styles", this.styles);
			info.AddValue("Childrens", this.childrens);
		}

		protected Aggregate(SerializationInfo info, StreamingContext context)
		{
			//	Constructeur qui d�s�rialise l'agr�gat.
			this.document = Document.ReadDocument;
			this.aggregateName = info.GetString("AggregateName");
			this.styles = (UndoableList) info.GetValue("Styles", typeof(UndoableList));

			if ( this.document.IsRevisionGreaterOrEqual(1,0,27) )
			{
				this.childrens = (UndoableList) info.GetValue("Childrens", typeof(UndoableList));
			}
			else
			{
				this.childrens = new UndoableList(this.document, UndoableListType.AggregatesChildrens);
			}
		}
		#endregion


		protected Document						document;
		protected string						aggregateName = "";
		protected UndoableList					styles;
		protected UndoableList					childrens;
	}
}
