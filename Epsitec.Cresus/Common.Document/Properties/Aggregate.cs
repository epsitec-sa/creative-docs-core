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
		}


		// Nom de l'agr�gat.
		public string AggregateName
		{
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

		// Liste des styles de l'agr�gat.
		public UndoableList Styles
		{
			get
			{
				return this.styles;
			}
		}

		// Agr�gat parent.
		public Aggregate Parent
		{
			get
			{
				return this.parent;
			}

			set
			{
				if ( this.parent != value )
				{
					this.InsertOpletAggregate();
					this.parent = value;
				}
			}
		}

		// Donne une propri�t� de l'agr�gat.
		public Properties.Abstract Property(Properties.Type type, bool deep)
		{
			if ( deep )  return this.PropertyDeep(type);
			else         return this.Property(type);
		}

		// Donne une propri�t� de l'agr�gat.
		public Properties.Abstract Property(Properties.Type type)
		{
			foreach ( Properties.Abstract property in this.styles )
			{
				if ( property.Type == type )  return property;
			}
			return null;
		}

		// Donne une propri�t� de l'agr�gat ou de l'un des agr�gats parents.
		public Properties.Abstract PropertyDeep(Properties.Type type)
		{
			Properties.Aggregate agg = this;

			int max = 10;  // profondeur maximale autoris�e !
			do
			{
				Properties.Abstract property = agg.Property(type);
				if ( property != null )  return property;

				agg = agg.parent;
				max --;
			}
			while ( agg != null && max > 0 );

			return null;
		}

		// V�rifie si un objet utilise cet agr�gat.
		public bool IsUsedByObject(Objects.Abstract obj)
		{
			Properties.Aggregate agg = obj.Aggregate;

			int max = 10;  // profondeur maximale autoris�e !
			while ( agg != null && max > 0 )
			{
				if ( agg == this )  return true;

				agg = agg.parent;
				max --;
			}

			return false;
		}


		// Copie tout l'agr�gat.
		public void CopyTo(Aggregate dst)
		{
			dst.aggregateName = this.aggregateName;
			dst.parent = this.parent;
			this.styles.CopyTo(dst.styles);
		}

		// Duplique tout l'agr�gat.
		public void DuplicateTo(Aggregate dst)
		{
			dst.aggregateName = this.aggregateName;
			dst.parent = this.parent;

			foreach ( Properties.Abstract srcProp in this.styles )
			{
				Abstract newProp = Abstract.NewProperty(this.document, srcProp.Type);
				srcProp.CopyTo(newProp);
				dst.styles.Add(newProp);
			}
		}


		// Donne un identificateur unique pour une propri�t�.
		public static int UniqueId(UndoableList aggregates, Properties.Abstract property)
		{
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
		// Ajoute un oplet pour m�moriser l'agr�gat.
		protected void InsertOpletAggregate()
		{
			if ( !this.document.Modifier.OpletQueueEnable )  return;
			OpletAggregate oplet = new OpletAggregate(this);
			this.document.Modifier.OpletQueue.Insert(oplet);
		}

		// M�morise l'agr�gat.
		protected class OpletAggregate : AbstractOplet
		{
			public OpletAggregate(Aggregate host)
			{
				this.host = host;
				this.name = host.aggregateName;
				this.parent = host.parent;
			}

			protected void Swap()
			{
				string temp = this.host.aggregateName;
				this.host.aggregateName = this.name;
				this.name = temp;

				Aggregate atemp = this.host.parent;
				this.host.parent = this.parent;
				this.parent = atemp;

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
			protected Aggregate				parent;
		}
		#endregion

		
		#region Serialization
		// S�rialise l'agr�gat.
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("AggregateName", this.aggregateName);
			info.AddValue("Styles", this.styles);
			info.AddValue("Parent", this.parent);
		}

		// Constructeur qui d�s�rialise l'agr�gat.
		protected Aggregate(SerializationInfo info, StreamingContext context)
		{
			this.document = Document.ReadDocument;
			this.aggregateName = info.GetString("AggregateName");
			this.styles = (UndoableList) info.GetValue("Styles", typeof(UndoableList));
			this.parent = (Aggregate) info.GetValue("Parent", typeof(Aggregate));
		}
		#endregion


		protected Document						document;
		protected string						aggregateName = "";
		protected UndoableList					styles;
		protected Aggregate						parent;
	}
}
