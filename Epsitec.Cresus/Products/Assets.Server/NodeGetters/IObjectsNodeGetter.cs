//	Copyright � 2014, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Assets.Server.NodeGetters
{
	public interface IObjectsNodeGetter
	{
		int Count
		{
			get;
		}
		
		CumulNode this[int index]
		{
			get;
		}
	}
}

