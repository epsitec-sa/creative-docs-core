﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using System.Text;

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;

namespace Epsitec.Cresus.Core.Accessors
{
	public class UriContactAccessor : AbstractContactAccessor<Entities.UriContactEntity>
	{
		public UriContactAccessor(object parentEntities, Entities.UriContactEntity entity, bool grouped)
			: base (parentEntities, entity, grouped)
		{
		}


		public override string IconUri
		{
			get
			{
				return "Data.Uri";
			}
		}

		public override string Title
		{
			get
			{
				if (this.Grouped)
				{
					return "Mail";
				}
				else
				{
					var builder = new StringBuilder ();

					builder.Append ("Mail");
					builder.Append (Misc.Encapsulate (" (", this.Roles, ")"));

					return Misc.RemoveLastLineBreak (builder.ToString ());
				}
			}
		}

		protected override string GetSummary()
		{
			var builder = new StringBuilder ();

			builder.Append (this.Entity.Uri);

			if (this.Grouped)
			{
				builder.Append (Misc.Encapsulate (" (", this.Roles, ")"));
			}

			builder.Append ("<br/>");

			return builder.ToString ();
		}

		public override AbstractEntity Create()
		{
			var newEntity = new Entities.UriContactEntity ();

			foreach (var role in this.Entity.Roles)
			{
				newEntity.Roles.Add (role);
			}

			newEntity.UriScheme = this.Entity.UriScheme;

			int index = this.ParentAbstractContacts.IndexOf (this.Entity);
			if (index == -1)
			{
				this.ParentAbstractContacts.Add (newEntity);
			}
			else
			{
				this.ParentAbstractContacts.Insert (index+1, newEntity);
			}

			return newEntity;
		}
	}
}
