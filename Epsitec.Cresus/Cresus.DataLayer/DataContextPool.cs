﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support.EntityEngine;

using Epsitec.Cresus.Database;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.DataLayer
{
	public sealed class DataContextPool : IEnumerable<DataContext>
	{
		public DataContextPool()
		{
			this.dataContexts = new HashSet<DataContext> ();
		}

		public bool Add(DataContext context)
		{
			System.Diagnostics.Debug.WriteLine ("Added context #" + context.UniqueId);
			return this.dataContexts.Add (context);
		}

		public bool Remove(DataContext context)
		{
			System.Diagnostics.Debug.WriteLine ("Removed context #" + context.UniqueId);
			return this.dataContexts.Remove (context);
		}

		public DataContext FindDataContext(AbstractEntity entity)
		{
			return this.dataContexts.FirstOrDefault (context => context.Contains (entity));
		}

		public DbKey FindDbKey(AbstractEntity entity)
		{
			if (entity == null)
            {
				return DbKey.Empty;
            }

			var context = this.FindDataContext (entity);
			
			if (context == null)
            {
				return DbKey.Empty;
            }

			var mapping = context.GetEntityDataMapping (entity);

			if (mapping == null)
			{
				return DbKey.Empty;
			}

			return mapping.RowKey;
		}

		#region IEnumerable<DataContext> Members

		public IEnumerator<DataContext> GetEnumerator()
		{
			return this.dataContexts.GetEnumerator ();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator ();
		}

		#endregion

		private readonly HashSet<DataContext> dataContexts;
	}
}
