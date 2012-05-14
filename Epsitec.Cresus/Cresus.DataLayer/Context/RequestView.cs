﻿//	Copyright © 2012, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Marc BETTEX, Maintainer: Marc BETTEX

using Epsitec.Cresus.Database;

using Epsitec.Cresus.DataLayer.Expressions;
using Epsitec.Cresus.DataLayer.Loader;

using System.Collections.Generic;


namespace Epsitec.Cresus.DataLayer.Context
{
	/// <summary>
	/// The <c>RequestView</c> class gives a view on a request that will not change over time.
	/// For this, it uses a new connection do the database and opens a readonly transaction. That means that the
	/// results of the queries made to the database via this class are totally independent of
	/// whatever might happen in the database via the DataContext bound to this class.
	/// Note also that the <c>RequestView</c> uses its request more than once. In fact it uses it each time
	/// one of its method is called. The <c>Request</c> used internally is copy of the one given in the
	/// constructor so you are free to modify it after the call to the constructor. However, the
	/// entities embedded in the request are not copied, and therefore any modification made to
	/// these entities might end up in causing problems here.
	/// </summary>
	public sealed class RequestView : System.IDisposable
	{
		internal RequestView(DataContext dataContext, Request request, IsolatedTransaction isolatedTransaction = null)
		{
			this.dataContext = dataContext;

			this.entityKeyRequest = RequestView.GetEntityKeyRequest (request);
			this.countRequest     = RequestView.GetCountRequest (request);

			if (isolatedTransaction == null)
			{
				this.isolatedTransaction        = new IsolatedTransaction (this.dataContext);
				this.disposeIsolatedTransaction = true;
			}
			else
			{
				this.isolatedTransaction = isolatedTransaction;
			}
		}


		public IList<EntityKey> GetKeys(int index, int count)
		{
			this.entityKeyRequest.Skip = index;
			this.entityKeyRequest.Take = count;

			return this.dataContext.DataLoader.GetEntityKeys (this.entityKeyRequest, this.isolatedTransaction.Transaction);
		}

		public int GetCount()
		{
			return this.dataContext.DataLoader.GetCount (this.countRequest, this.isolatedTransaction.Transaction);
		}

		
		private static Request GetEntityKeyRequest(Request request)
		{
			var entityKeyRequest = request.Clone ();

			entityKeyRequest.SortClauses.Add
			(
				new SortClause
				(
					InternalField.CreateId (request.RequestedEntity),
					SortOrder.Ascending
				)
			);

			return entityKeyRequest;
		}

		private static Request GetCountRequest(Request request)
		{
			var countRequest = request.Clone ();

			countRequest.SortClauses.RemoveRange (0, countRequest.SortClauses.Count);

			return countRequest;
		}


		#region System.IDisposable Members

		public void Dispose()
		{
			if (this.disposeIsolatedTransaction)
			{
				this.isolatedTransaction.Dispose ();
			}
		}

		#endregion


		private readonly IsolatedTransaction	isolatedTransaction;
		private readonly bool					disposeIsolatedTransaction;
		private readonly DataContext			dataContext;
		private readonly Request				entityKeyRequest;
		private readonly Request				countRequest;
	}
}