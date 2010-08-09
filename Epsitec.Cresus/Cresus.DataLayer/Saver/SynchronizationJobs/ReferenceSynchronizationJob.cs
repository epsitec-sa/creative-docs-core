﻿using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Support.Extensions;

using Epsitec.Cresus.DataLayer.Context;


namespace Epsitec.Cresus.DataLayer.Saver.SynchronizationJobs
{


	/// <summary>
	/// The <c>ReferenceSynchronizationJob</c> class describes the modifications that have been made
	/// to a reference field of an <see cref="AbstractEntity"/>.
	/// </summary>
	internal class ReferenceSynchronizationJob : AbstractFieldSynchronizationJob
	{


		/// <summary>
		/// Creates a new <c>ReferenceSynchronizationJob</c>.
		/// </summary>
		/// <param name="dataContextId">The unique id of the <see cref="DataContext"/> that is creating the <c>ReferenceSynchronizationJob</c>.</param>
		/// <param name="sourceKey">The <see cref="EntityKey"/> that identifies the <see cref="AbstractEntity"/> targeted by the <c>ReferenceSynchronizationJob</c>.</param>
		/// <param name="fieldId">The <see cref="Druid"/> of the field targeted by the <c>ReferenceSynchronizationJob</c>.</param>
		/// <param name="newTargetKey">The <see cref="EntityKey"/> of the new target of the field targeted by the <c>ValueSynchronizationJob</c>.</param>
		/// <exception cref="System.ArgumentException">If <paramref name="sourceKey"/> is empty.</exception>
		/// <exception cref="System.ArgumentException">If <paramref name="fieldId"/> is empty.</exception>
		/// <exception cref="System.ArgumentException">If <paramref name="newTargetKey"/> is empty.</exception>
		public ReferenceSynchronizationJob(int dataContextId, EntityKey sourceKey, Druid fieldId, EntityKey? newTargetKey)
			: base (dataContextId, sourceKey, fieldId)
		{
			newTargetKey.ThrowIf (t => t.HasValue && t.Value == EntityKey.Empty, "newTargetKey cannot be empty.");

			this.NewTargetKey = newTargetKey;
		}


		/// <summary>
		/// The <see cref="EntityKey"/> that identifies the new target of the field of the
		/// <see cref="AbstractEntity"/> targeted by this instance.
		/// </summary>
		public EntityKey? NewTargetKey
		{
			get;
			private set;
		}


	}


}
