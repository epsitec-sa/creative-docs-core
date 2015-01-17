//	Copyright � 2011-2012, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Metadata
{
	/// <summary>
	/// The <c>EntityMetadataRecorder</c> class is used to record column definitions. See
	/// the generic type for more details.
	/// </summary>
	public abstract class EntityMetadataRecorder
	{
		protected EntityMetadataRecorder(Druid entityId, string name)
		{
			this.entityId = entityId;
			this.name = name;
		}

		
		/// <summary>
		/// Gets the columns.
		/// </summary>
		public abstract IEnumerable<EntityColumnMetadata> Columns
		{
			get;
		}

		/// <summary>
		/// Gets the number of columns.
		/// </summary>
		public abstract int						ColumnCount
		{
			get;
		}

		/// <summary>
		/// Gets the entity id.
		/// </summary>
		public Druid							EntityId
		{
			get
			{
				return this.entityId;
			}
		}

		public string							Name
		{
			get
			{
				return this.name;
			}
		}
		
		

		/// <summary>
		/// Gets the metadata information, which is available only after all columns have
		/// been added to the recorder.
		/// </summary>
		/// <returns>The metadata information.</returns>
		public EntityTableMetadata GetMetadata()
		{
			return new EntityTableMetadata (this.EntityId, this.Name, this.Columns);
		}


		private readonly Druid					entityId;
		private readonly string					name;
	}
}
