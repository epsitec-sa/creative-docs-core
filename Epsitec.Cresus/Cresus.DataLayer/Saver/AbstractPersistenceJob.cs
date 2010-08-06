﻿using Epsitec.Common.Support.EntityEngine;


namespace Epsitec.Cresus.DataLayer.Saver
{


	internal abstract class AbstractPersistenceJob
	{


		protected AbstractPersistenceJob(AbstractEntity entity)
		{
			this.Entity = entity;
		}
		
		
		public AbstractEntity Entity
		{
			get;
			private set;
		}

		
	}


}
