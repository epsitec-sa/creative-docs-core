﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Data.DataProperties;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.Server.BusinessLogic
{
	public class AccountsMerge : System.IDisposable
	{
		public AccountsMerge(GuidList<DataObject> currentAccounts, GuidList<DataObject> importedAccounts, AccountsMergeMode mode)
		{
			this.currentAccounts  = currentAccounts;
			this.importedAccounts = importedAccounts;
			this.mode             = mode;

			this.todo  = new Dictionary<DataObject, DataObject> ();
			this.links = new Dictionary<DataObject, DataObject> ();

			this.UpdateLinks ();
		}

		public void Dispose()
		{
		}


		public Dictionary<DataObject, DataObject> Todo
		{
			get
			{
				return this.todo;
			}
		}

		public void Merge()
		{
			if (this.mode == AccountsMergeMode.Replace ||
				this.currentAccounts.Any () == false)
			{
				this.DoReplace ();
			}
			else
			{
				this.DoMerge ();
			}
		}


		private void DoReplace()
		{
			this.currentAccounts.Clear ();

			foreach (var account in this.importedAccounts)
			{
				this.currentAccounts.Add (account);
			}
		}

		private void DoMerge()
		{
			this.UpdateLinks ();

			//	On s'occupe d'abord des données brutes.
			foreach (var imported in this.todo)
			{
				if (imported.Value == null)
				{
					this.AddAccount (imported.Key);
				}
				else
				{
					this.MergeAccount (imported.Value, imported.Key);
				}

				//-DataObject current;
				//-if (this.links.TryGetValue (imported, out current))
				//-{
				//-	this.MergeAccount (current, imported);
				//-}
				//-else
				//-{
				//-	this.AddAccount (imported);
				//-}
			}

			//	On s'occupe ensuite de la parenté.
			foreach (var imported in this.todo)
			{
				var guid = ObjectProperties.GetObjectPropertyGuid (imported.Key, null, ObjectField.GroupParent);
				if (guid.IsEmpty)
				{
					continue;
				}

				var importedParent = this.importedAccounts[guid];
				var currentParent = this.links[importedParent];
				var current = this.links[imported.Key];
				var e = current.GetEvent (0);
				e.AddProperty (new DataGuidProperty (ObjectField.GroupParent, currentParent.Guid));

				//-var guid = ObjectProperties.GetObjectPropertyGuid (imported, null, ObjectField.GroupParent);
				//-if (guid.IsEmpty)
				//-{
				//-	continue;
				//-}
				//-
				//-var importedParent = this.importedAccounts[guid];
				//-var currentParent = this.links[importedParent];
				//-var current = this.links[imported];
				//-var e = current.GetEvent (0);
				//-e.AddProperty (new DataGuidProperty (ObjectField.GroupParent, currentParent.Guid));
			}
		}

		private void AddAccount(DataObject imported)
		{
			var o = new DataObject ();
			this.currentAccounts.Add (o);
			{
				var start  = new Timestamp (new System.DateTime (2000, 1, 1), 0);
				var e = new DataEvent (start, EventType.Input);
				o.AddEvent (e);

				this.MergeAccount (o, imported);
			}

			this.links.Add (imported, o);
		}

		private void MergeAccount(DataObject current, DataObject imported)
		{
			var e = current.GetEvent (0);

			this.MergeString (e, imported, ObjectField.Number);
			this.MergeString (e, imported, ObjectField.Name);
			this.MergeInt    (e, imported, ObjectField.AccountCategory);
			this.MergeInt    (e, imported, ObjectField.AccountType);
		}

		private void MergeString(DataEvent e, DataObject imported, ObjectField field)
		{
			var value = ObjectProperties.GetObjectPropertyString (imported, null, field);
			e.AddProperty (new DataStringProperty (field, value));
		}

		private void MergeInt(DataEvent e, DataObject imported, ObjectField field)
		{
			var value = ObjectProperties.GetObjectPropertyInt (imported, null, field);
			e.AddProperty (new DataIntProperty (field, value.Value));
		}


		private void UpdateLinks()
		{
			this.todo.Clear ();
			this.links.Clear ();

			foreach (var imported in this.importedAccounts)
			{
				var current = this.SearchAccordingCriterion (imported);

				if (current == null)
				{
					this.todo.Add (imported, null);  // compte à ajouter
				}
				else
				{
					if (!this.IsEqual (current, imported))
					{
						this.todo.Add (imported, current);  // compte à fusionner
					}

					this.links.Add (imported, current);
				}
			}
		}

		private DataObject SearchAccordingCriterion(DataObject imported)
		{
			var s = this.GetCriterion (imported);
			return this.currentAccounts.Where (x => this.GetCriterion (x) == s).FirstOrDefault ();
		}

		private string GetCriterion(DataObject account)
		{
			return ObjectProperties.GetObjectPropertyString (account, null, ObjectField.Number);
		}


		private bool IsEqual(DataObject current, DataObject imported)
		{
			return this.IsEqualParent (current, imported)
				&& this.IsEqualString (current, imported, ObjectField.Number)
				&& this.IsEqualString (current, imported, ObjectField.Name)
				&& this.IsEqualInt    (current, imported, ObjectField.AccountCategory)
				&& this.IsEqualInt    (current, imported, ObjectField.AccountType);
		}

		private bool IsEqualParent(DataObject current, DataObject imported)
		{
			//	Vérifie si les comptes parents ont le même numéro.
			var currentGuid = ObjectProperties.GetObjectPropertyGuid (current, null, ObjectField.GroupParent);
			var currentParent = currentGuid.IsEmpty ? null : this.currentAccounts[currentGuid];

			var importedGuid = ObjectProperties.GetObjectPropertyGuid (imported, null, ObjectField.GroupParent);
			var importedParent = importedGuid.IsEmpty ? null : this.importedAccounts[importedGuid];

			return this.IsEqualString (currentParent, importedParent, ObjectField.Number);
		}

		private bool IsEqualString(DataObject current, DataObject imported, ObjectField field)
		{
			var value1 = ObjectProperties.GetObjectPropertyString (current,  null, field);
			var value2 = ObjectProperties.GetObjectPropertyString (imported, null, field);
			return value1 == value2;
		}

		private bool IsEqualInt(DataObject current, DataObject imported, ObjectField field)
		{
			var value1 = ObjectProperties.GetObjectPropertyInt (current,  null, field);
			var value2 = ObjectProperties.GetObjectPropertyInt (imported, null, field);
			return value1 == value2;
		}


		private readonly GuidList<DataObject>				currentAccounts;
		private readonly GuidList<DataObject>				importedAccounts;
		private readonly AccountsMergeMode					mode;
		private readonly Dictionary<DataObject, DataObject>	todo;
		private readonly Dictionary<DataObject, DataObject>	links;
	}
}
