﻿//	Copyright © 2009, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Graph.Widgets;

namespace Epsitec.Cresus.Graph.Functions
{
	public static class FunctionFactory
	{
		public static System.Func<IList<double>, double> GetFunction(string name)
		{
			switch (name)
			{
				case "sum":
					return x => x.Aggregate (0.0, (sum, value) => sum + value);
			}
			
			return null;
		}
	}
}
