//	Copyright � 2006-2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using System.Collections.Generic;
using Epsitec.Common.Types;

namespace Epsitec.Common.Widgets.Layouts.Collections
{
	/// <summary>
	/// The <c>ColumnDefinitionCollection</c> class provides access to an ordered,
	/// strongly typed collection of <see cref="T:ColumnDefinition"/> objects.
	/// </summary>
	public class ColumnDefinitionCollection : Types.Collections.HostedDependencyObjectList<ColumnDefinition>
	{
		public ColumnDefinitionCollection(GridLayoutEngine grid) : base (grid)
		{
		}
	}
}
