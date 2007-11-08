//	Copyright � 2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;

using Epsitec.Cresus.Database;
using Epsitec.Cresus.DataLayer;
using Epsitec.Cresus.DataLayer.Helpers;

using System.Collections.Generic;

namespace Epsitec.Cresus.DataLayer
{
	public sealed partial class DataContext
	{
		public void LoadSchema(DbTable tableDefinition)
		{
			if (this.richCommand.Tables.Contains (tableDefinition.Name))
			{
				//	Nothing to do, we already know this table.
			}
			else
			{
				using (DbTransaction transaction = this.infrastructure.InheritOrBeginTransaction (DbTransactionMode.ReadOnly))
				{
					this.richCommand.ImportTable (transaction, tableDefinition, null);
				}
			}
		}

	}
}
