﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.Server.NodeGetters
{
	public class WarningNodeGetter : INodeGetter<Warning>  // outputNodes
	{
		public WarningNodeGetter(DataAccessor accessor, List<Warning> warnings)
		{
			this.accessor = accessor;
			this.inputWarnings = warnings;
		}


		public void SetParams(SortingInstructions instructions)
		{
			this.sortingInstructions = instructions;
			this.UpdateData ();
		}


		public int Count
		{
			get
			{
				return this.inputWarnings.Count;
			}
		}

		public Warning this[int index]
		{
			get
			{
				if (index >= 0 && index < this.outputWarnings.Length)
				{
					return this.outputWarnings[index];
				}
				else
				{
					return Warning.Empty;
				}
			}
		}


		private void UpdateData()
		{
			this.outputWarnings = SortingMachine<Warning>.Sorts
			(
				this.sortingInstructions,
				this.inputWarnings,
				null,
				x => this.PrimaryData (x),
				x => this.SecondaryData (x)
			).ToArray ();
		}

		private ComparableData PrimaryData(Warning warning)
		{
			return this.GetComparableData (warning, this.sortingInstructions.PrimaryField);
		}

		private ComparableData SecondaryData(Warning warning)
		{
			return this.GetComparableData (warning, this.sortingInstructions.SecondaryField);
		}

		private ComparableData GetComparableData(Warning warning, ObjectField field)
		{
			var obj = this.accessor.GetObject (warning.BaseType, warning.ObjectGuid);
			var e = obj.GetEvent (warning.EventGuid);

			Timestamp?      timestamp;
			System.DateTime date;
			EventType       eventType;

			if (e == null)
			{
				timestamp = null;
				date      = System.DateTime.MinValue;
				eventType = EventType.Unknown;
			}
			else
			{
				timestamp = e.Timestamp;
				date      = timestamp.Value.Date;
				eventType = e.Type;
			}

			switch (field)
			{
				case ObjectField.WarningViewGlyph:
					return new ComparableData ((int) warning.BaseType.Kind);

				case ObjectField.WarningObject:
					string text = UniversalLogic.GetObjectSummary (this.accessor, warning.BaseType, obj, timestamp);
					return new ComparableData (text);

				case ObjectField.WarningDate:
					return new ComparableData (date);

				case ObjectField.WarningEventGlyph:
					return new ComparableData ((int) eventType);

				case ObjectField.WarningField:
					string f = UserFieldsLogic.GetFieldName (this.accessor, warning.BaseType, warning.Field);
					return new ComparableData (f);

				case ObjectField.WarningDescription:
					return new ComparableData (warning.Description);

				default:
					return new ComparableData ();
			}
		}


		private readonly DataAccessor			accessor;
		private readonly List<Warning>			inputWarnings;
		private Warning[]						outputWarnings;
		private SortingInstructions				sortingInstructions;
	}
}
