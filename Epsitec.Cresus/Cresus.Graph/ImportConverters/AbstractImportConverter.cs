﻿//	Copyright © 2009, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Graph.Data;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Graph.ImportConverters
{
	/// <summary>
	/// The <c>AbstractImportConverter</c> class provides the methods used to convert
	/// raw tabular data into a cube.
	/// </summary>
	public abstract class AbstractImportConverter
	{
		protected AbstractImportConverter(string name)
		{
			this.name = name;
		}

		public abstract string DataTitle
		{
			get;
		}

		public string Name
		{
			get
			{
				return this.name;
			}
		}

		public int Priority
		{
			get;
			internal set;
		}
		
		public abstract DataCube ToDataCube(IList<string> header, IEnumerable<IEnumerable<string>> lines);

		public DataCube ToDataCube(IList<string> header, IEnumerable<string[]> lines)
		{
			return this.ToDataCube (header, lines.Cast<IEnumerable<string>> ());
		}

		public virtual GraphDataCategory GetCategory(ChartSeries series)
		{
			return GraphDataCategory.Generic;
		}


		private readonly string name;
	}
}
