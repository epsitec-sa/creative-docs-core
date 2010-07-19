﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support.EntityEngine;

using System.Collections;
using System.Collections.Generic;

using System.Linq;


namespace Epsitec.Cresus.DataLayer.Context
{


	/// <summary>
	/// The <c>DataContextPool</c> is a singleton that is used to manage several
	/// <see cref="DataContext"/>.
	/// </summary>
	public sealed class DataContextPool : IEnumerable<DataContext>
	{


		/// <summary>
		/// Builds a new empty <c>DataContext</c>.
		/// </summary>
		private DataContextPool()
		{
			this.dataContexts = new HashSet<DataContext> ();
		}


		/// <summary>
		/// Adds a <see cref="DataContext"/> to the pool.
		/// </summary>
		/// <param name="context">The <see cref="DataContext"/> to add.</param>
		/// <returns><c>true</c> if the <see cref="DataContext"/> was not present in the pool, <c>false</c> if it was.</returns>
		/// <exception cref="System.ArgumentNullException">If <paramref name="context"/> is null.</exception>
		public bool Add(DataContext context)
		{
			if (context == null)
			{
				throw new System.ArgumentNullException ("context");
			}

			System.Diagnostics.Debug.WriteLine ("Added context #" + context.UniqueId);

			return this.dataContexts.Add (context);
		}


		/// <summary>
		/// Removes a <see cref="DataContext"/> from the pool.
		/// </summary>
		/// <param name="context">The <see cref="DataContext"/> to remove.</param>
		/// <returns><c>true</c> if the <see cref="DataContext"/> was present in the pool, <c>false</c> if it was not.</returns>
		/// <exception cref="System.ArgumentNullException">If <paramref name="context"/> is null.</exception>
		public bool Remove(DataContext context)
		{
			if (context == null)
			{
				throw new System.ArgumentNullException ("context");
			}

			System.Diagnostics.Debug.WriteLine ("Removed context #" + context.UniqueId);

			return this.dataContexts.Remove (context);
		}


		/// <summary>
		/// Tells whether the pool contains a given <see cref="DataContext"/>.
		/// </summary>
		/// <param name="context">The <see cref="DataContext"/> whose presence in the pool to check.</param>
		/// <returns><c>true</c> if <paramref name="dataContext"/> is in the pool, false if it is not.</returns>
		public bool Contains(DataContext context)
		{
			if (context == null)
			{
				throw new System.ArgumentNullException ("context");
			}

			return this.dataContexts.Contains (context);
		}


		/// <summary>
		/// Finds the <see cref="DataContext"/> which is responsible for <paramref name="entity"/> or
		/// null if there is none.
		/// </summary>
		/// <param name="entity">The <see cref="AbstractEntity"/> whose <see cref="DataContext"/> to find.</param>
		/// <returns>The <see cref="DataContext"/> responsible for <paramref name="entity"/>.</returns>
		/// <exception cref="System.ArgumentNullException">If <paramref name="entity"/> is null.</exception>
		public DataContext FindDataContext(AbstractEntity entity)
		{
			if (entity == null)
			{
				throw new System.ArgumentNullException ("entity");
			}
			
			return this.FirstOrDefault (context => context.Contains (entity));
		}


		/// <summary>
		/// Finds the <see cref="EntityKey"/> which represents the storage location of
		/// <paramref name="entity"/> in the database, if there is any.
		/// </summary>
		/// <param name="entity">The <see cref="AbstractEntity"/> whose <see cref="EntityKey"/> to find.</param>
		/// <returns>The <see cref="EntityKey"/> of <paramref name="entity"/>.</returns>
		/// <exception cref="System.ArgumentNullException">If <paramref name="entity"/> is null.</exception>
		public EntityKey? FindEntityKey(AbstractEntity entity)
		{
			if (entity == null)
			{
				throw new System.ArgumentNullException ("entity");
			}
			
			DataContext context = this.FindDataContext (entity);

			return (context == null) ? null : context.GetEntityKey (entity);
		}


		#region IEnumerable<DataContext> Members


		/// <summary>
		///Iterates over all the <see cref="DataContext"/> managed by this pool.
		/// </summary>
		/// <returns>The <see cref="DataContext"/> managed by this pool.</returns>
		public IEnumerator<DataContext> GetEnumerator()
		{
			return this.dataContexts.GetEnumerator ();
		}


		#endregion


		#region IEnumerable Members


		/// <summary>
		///Iterates over all the <see cref="DataContext"/> managed by this pool.
		/// </summary>
		/// <returns>The <see cref="DataContext"/> managed by this pool.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator ();
		}


		#endregion


		/// <summary>
		/// Get the single instance of <see cref="DataContextPool"/>.
		/// </summary>
		public static DataContextPool Instance
		{
			get
			{
				return DataContextPool.instance;
			}
		}


		/// <summary>
		/// The single instance of <see cref="DataContextPool"/>.
		/// </summary>
		private static readonly DataContextPool instance = new DataContextPool ();


		/// <summary>
		/// The <see cref="HashSet"/> used to store references to the managed <see cref="DataContext"/>.
		/// </summary>
		private readonly HashSet<DataContext> dataContexts;


	}


}
