//	Copyright � 2012, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Aider
{
	[DesignerVisible]
	public enum GroupType
	{
		None = 0,

		Root,
		Node,
		NodeAndLeaf,
		Leaf,
	}
}
