﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epsitec.Cresus.Assets.Server.NaiveEngine
{
	public class DataAccessor
	{
		public DataAccessor(DataMandat mandat)
		{
			this.mandat = mandat;
		}


		public int ObjectsCount
		{
			get
			{
				return this.mandat.Objects.Count;
			}
		}

		public DataObject GetObject(int index)
		{
			if (index >= 0 && index < this.mandat.Objects.Count)
			{
				return this.mandat.Objects[index];
			}
			else
			{
				return null;
			}
		}

		public DataObject GetObject(Guid guid)
		{
			return this.mandat.Objects.Where (x => x.Guid == guid).FirstOrDefault ();
		}



		public DataObject GetCategory(Guid guid)
		{
			return this.mandat.Categories.Where (x => x.Guid == guid).FirstOrDefault ();
		}


		private readonly DataMandat mandat;
	}
}
