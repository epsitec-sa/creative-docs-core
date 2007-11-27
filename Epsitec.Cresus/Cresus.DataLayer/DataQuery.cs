﻿//	Copyright © 2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
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
	public class DataQuery
	{
		public DataQuery()
		{
			this.outputFields = new List<EntityFieldPath> ();
		}

		
		public IList<EntityFieldPath> OutputFields
		{
			get
			{
				return this.outputFields;
			}
		}


		private readonly List<EntityFieldPath> outputFields;
	}
}
