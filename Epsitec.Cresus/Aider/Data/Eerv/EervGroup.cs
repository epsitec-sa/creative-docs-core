﻿using Epsitec.Common.Support.Extensions;

using Epsitec.Common.Types;

using System.Collections.Generic;


namespace Epsitec.Aider.Data.Eerv
{


	internal sealed class EervGroup : Freezable
	{


		public EervGroup(string id, string name)
		{
			this.Id = id;
			this.Name = name;

			this.activities = new List<EervActivity> ();
			this.groupDefinitionIds = new List<string> ();
		}


		public IList<string> GroupDefinitionIds
		{
			get
			{
				return this.groupDefinitionIds;
			}
		}


		public IList<EervActivity> Activities
		{
			get
			{
				return this.activities;
			}
		}


		protected override void HandleFreeze()
		{
			this.activities = this.activities.AsReadOnlyCollection ();
			this.groupDefinitionIds = this.groupDefinitionIds.AsReadOnlyCollection ();
		}


		public readonly string Id;
		public readonly string Name;


		private IList<EervActivity> activities;
		private IList<string> groupDefinitionIds;


	}


}
