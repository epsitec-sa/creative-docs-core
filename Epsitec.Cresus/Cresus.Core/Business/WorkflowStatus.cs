//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;

namespace Epsitec.Cresus.Core.Business
{
	[DesignerVisible]
	public enum WorkflowStatus
	{
		None			= 0,

		Pending			= 1,
		Active			= 2,
		Done			= 3,

		Cancelled		= 4,
		TimedOut		= 5,
	}
}
