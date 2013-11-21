﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.NodesGetter;

namespace Epsitec.Cresus.Assets.Server.SimpleEngine
{
	public class DataAccessor
	{
		public DataAccessor(DataMandat mandat)
		{
			this.mandat = mandat;

			this.editionAccessor = new EditionAccessor (this);

			//	Recalcule tout.
			foreach (var obj in this.mandat.GetData (BaseType.Objects))
			{
				ObjectCalculator.UpdateComputedAmounts (obj);
			}
		}


		public static int Simulation;


		public System.DateTime StartDate
		{
			get
			{
				return this.mandat.StartDate;
			}
		}

		public System.DateTime EndDate
		{
			get
			{
				return this.mandat.EndDate;
			}
		}

		public EditionAccessor EditionAccessor
		{
			get
			{
				return this.editionAccessor;
			}
		}


		#region NodesGetter
		public GuidNodesGetter GetNodesGetter(BaseType baseType)
		{
			//	Retourne un moyen standardisé d'accès en lecture aux données d'une base.
			return new GuidNodesGetter (this.mandat, baseType);
		}

		public class GuidNodesGetter : AbstractNodesGetter<GuidNode>
		{
			public GuidNodesGetter(DataMandat mandat, BaseType baseType)
			{
				this.mandat   = mandat;
				this.baseType = baseType;
			}

			public override int Count
			{
				get
				{
					return this.Data.Count;
				}
			}

			public override GuidNode this[int index]
			{
				get
				{
					var obj = this.Data[index];
					return new GuidNode (obj.Guid);
				}
			}

			private GuidList<DataObject> Data
			{
				get
				{
					return this.mandat.GetData (this.baseType);
				}
			}

			private readonly DataMandat			mandat;
			private readonly BaseType			baseType;
		}
		#endregion


		#region Objects
		public DataObject GetObject(BaseType baseType, Guid objectGuid)
		{
			return this.mandat.GetData (baseType)[objectGuid];
		}

		public Guid CreateObject(BaseType baseType, System.DateTime date, string name, Guid parent)
		{
			var obj = new DataObject ();
			mandat.GetData (baseType).Add (obj);

			var timestamp = new Timestamp (date, 0);
			var e = new DataEvent (timestamp, EventType.Entrée);
			obj.AddEvent (e);

			if (!parent.IsEmpty)
			{
				int position = this.GetCreatePosition (baseType, parent);

				e.AddProperty (new DataGuidProperty (ObjectField.Parent, parent));
				e.AddProperty (new DataIntProperty (ObjectField.Position, position));
			}

			e.AddProperty (new DataStringProperty (ObjectField.Nom, name));
		
			return obj.Guid;
		}

		private int GetCreatePosition(BaseType baseType, Guid parent)
		{
			//	Retourne la position d'un objet créé, qui vient toujours en dernier,
			//	après tous ses frères.
			int position = 0;

			foreach (var obj in mandat.GetData (baseType))
			{
				var guid = ObjectCalculator.GetObjectPropertyGuid (obj, null, ObjectField.Parent);
				if (guid == parent)
				{
					int? p = ObjectCalculator.GetObjectPropertyInt (obj, null, ObjectField.Position);
					if (p.HasValue)
					{
						position = System.Math.Max (position, p.Value+1);
					}
				}
			}

			return position;
		}

		public DataEvent CreateObjectEvent(DataObject obj, System.DateTime date, EventType type)
		{
			if (obj != null)
			{
				var position = obj.GetNewPosition (date);
				var ts = new Timestamp (date, position);
				var e = new DataEvent (ts, type);

				//	Ajoute la date du jour comme date valeur.
				var p = new DataDateProperty (ObjectField.OneShotDateOpération, Timestamp.Now.Date);
				e.AddProperty (p);

				obj.AddEvent (e);
				ObjectCalculator.UpdateComputedAmounts (obj);
				return e;
			}

			return null;
		}

		public void RemoveObject(BaseType baseType, Guid objectGuid)
		{
			var list = this.mandat.GetData (baseType);
			var obj = this.GetObject (baseType, objectGuid);

			if (obj != null)
			{
				list.Remove (obj);
			}
		}

		public void RemoveObjectEvent(DataObject obj, Timestamp? timestamp)
		{
			if (obj != null && timestamp.HasValue)
			{
				var e = obj.GetEvent (timestamp.Value);
				if (e != null)
				{
					obj.RemoveEvent (e);
					ObjectCalculator.UpdateComputedAmounts (obj);
				}
			}
		}
		#endregion


		public static bool IsOneShotField(ObjectField objectField)
		{
			switch (objectField)
			{
				case ObjectField.OneShotNuméro:
				case ObjectField.OneShotDateOpération:
				case ObjectField.OneShotCommentaire:
				case ObjectField.OneShotDocuments:
					return true;

				default:
					return false;
			}
		}

		public static FieldType GetFieldType(ObjectField objectField)
		{
			switch (objectField)
			{
				case ObjectField.FréquenceAmortissement:
					return FieldType.Int;

				case ObjectField.Valeur1:
				case ObjectField.Valeur2:
				case ObjectField.Valeur3:
					return FieldType.ComputedAmount;

				case ObjectField.TauxAmortissement:
				case ObjectField.ValeurRésiduelle:
					return FieldType.Decimal;

				case ObjectField.OneShotDateOpération:
				case ObjectField.DateAmortissement1:
				case ObjectField.DateAmortissement2:
					return FieldType.Date;

				case ObjectField.Parent:
				case ObjectField.GroupGuid+0:
				case ObjectField.GroupGuid+1:
				case ObjectField.GroupGuid+2:
				case ObjectField.GroupGuid+3:
				case ObjectField.GroupGuid+4:
				case ObjectField.GroupGuid+5:
				case ObjectField.GroupGuid+6:
				case ObjectField.GroupGuid+7:
				case ObjectField.GroupGuid+8:
				case ObjectField.GroupGuid+9:
					return FieldType.Guid;

				default:
					return FieldType.String;
			}
		}


		private readonly DataMandat				mandat;
		private readonly EditionAccessor		editionAccessor;
	}
}
