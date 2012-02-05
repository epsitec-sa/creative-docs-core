﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Types;
using Epsitec.Common.Support.EntityEngine;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Business;

using Epsitec.Cresus.Compta.Controllers;
using Epsitec.Cresus.Compta.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Accessors
{
	/// <summary>
	/// Gère l'accès aux journaux de la comptabilité.
	/// </summary>
	public class JournauxDataAccessor : AbstractDataAccessor
	{
		public JournauxDataAccessor(AbstractController controller)
			: base (controller)
		{
			this.StartCreationLine ();
		}


		public override void FilterUpdate()
		{
			this.UpdateAfterOptionsChanged ();
		}

		public override void UpdateAfterOptionsChanged()
		{
		}


		public override int AllCount
		{
			get
			{
				return this.Count;
			}
		}

		public override int Count
		{
			get
			{
				return this.comptaEntity.Journaux.Count;
			}
		}

		public override AbstractEntity GetEditionData(int row)
		{
			if (row < 0 || row >= this.comptaEntity.Journaux.Count)
			{
				return null;
			}
			else
			{
				return this.comptaEntity.Journaux[row];
			}
		}

		public override FormattedText GetText(int row, ColumnType column, bool all = false)
		{
			var journaux = comptaEntity.Journaux;

			if (row < 0 || row >= journaux.Count)
			{
				return FormattedText.Null;
			}

			var journal = journaux[row];

			switch (column)
			{
				case ColumnType.Titre:
					return journal.Name;

				case ColumnType.Libellé:
					return journal.Description;

				case ColumnType.Résumé:
					return this.comptaEntity.JournalRésumé (journal);

				default:
					return FormattedText.Null;
			}
		}


		public override void InsertEditionLine(int index)
		{
			var newData = new JournauxEditionLine (this.controller);

			if (index == -1)
			{
				this.editionLine.Add (newData);
			}
			else
			{
				this.editionLine.Insert (index, newData);
			}

			this.countEditedRow = this.editionLine.Count;
		}

		public override void StartCreationLine()
		{
			this.editionLine.Clear ();
			this.editionLine.Add (new JournauxEditionLine (this.controller));
			this.PrepareEditionLine (0);

			this.firstEditedRow = -1;
			this.countEditedRow = 1;

			this.isModification = false;
			this.justCreated = false;
		}

		public override void ResetCreationLine()
		{
			if (this.justCreated)
			{
				this.PrepareEditionLine (0);

				while (this.editionLine.Count > 1)
				{
					this.editionLine.RemoveAt (1);
				}

				this.countEditedRow = 1;
			}
		}

		public override void StartModificationLine(int row)
		{
			this.editionLine.Clear ();

			this.firstEditedRow = row;
			this.countEditedRow = 0;

			if (row >= 0 && row < this.comptaEntity.Journaux.Count)
			{
				var data = new JournauxEditionLine (this.controller);
				var journal = this.comptaEntity.Journaux[row];
				data.EntityToData (journal);

				this.editionLine.Add (data);
				this.countEditedRow++;
			}

			this.initialCountEditedRow = this.countEditedRow;
			this.isModification = true;
			this.justCreated = false;
		}

		public override void UpdateEditionLine()
		{
			if (this.isModification)
			{
				this.UpdateModificationData ();
				this.justCreated = false;
			}
			else
			{
				this.UpdateCreationData ();
				this.justCreated = true;
			}

			this.SearchUpdate ();
			this.controller.MainWindowController.SetDirty ();
		}

		private void UpdateCreationData()
		{
			int firstRow = -1;

			foreach (var data in this.editionLine)
			{
				var journal = this.CreateJournal ();
				data.DataToEntity (journal);

				this.comptaEntity.Journaux.Add (journal);

				if (firstRow == -1)
				{
					firstRow = this.comptaEntity.Journaux.Count-1;
				}
			}

			this.firstEditedRow = firstRow;
		}

		private void UpdateModificationData()
		{
			int row = this.firstEditedRow;

			var journal = this.comptaEntity.Journaux[row];
			this.editionLine[0].DataToEntity (journal);
		}

		public override void RemoveModificationLine()
		{
			if (this.isModification)
			{
				for (int row = this.firstEditedRow+this.countEditedRow-1; row >= this.firstEditedRow; row--)
                {
					var journal = this.comptaEntity.Journaux[row];
					this.DeleteJournal (journal);
					this.comptaEntity.Journaux.RemoveAt (row);
                }

				this.SearchUpdate ();
				this.StartCreationLine ();
			}
		}


		private ComptaJournalEntity CreateJournal()
		{
			this.controller.MainWindowController.SetDirty ();

			if (this.businessContext == null)
			{
				return new ComptaJournalEntity ();
			}
			else
			{
				return this.businessContext.CreateEntity<ComptaJournalEntity> ();
			}
		}

		private void DeleteJournal(ComptaJournalEntity journal)
		{
			this.controller.MainWindowController.SetDirty ();

			if (this.businessContext == null)
			{
				// rien à faire
			}
			else
			{
				this.businessContext.DeleteEntity (journal);
			}
		}
	}
}