//	Copyright � 2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using System.Collections.Generic;

namespace Epsitec.Common.Types
{
	/// <summary>
	/// The IListHost interface is implemented by the class which hosts a
	/// HostedList, so that the HostedList can notify the host of its
	/// changes.
	/// </summary>
	/// <typeparam name="T">Data type used by HostedList</typeparam>
	public interface IListHost<T>
	{
		void NotifyListInsertion(T item);
		void NotifyListRemoval(T item);
	}
}
