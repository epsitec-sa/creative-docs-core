//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

namespace Epsitec.Common.Support
{
	/// <summary>
	/// The <c>IIsDisposed</c> interface provides the <c>IsDisposed</c> property.
	/// </summary>
	public interface IIsDisposed
	{
		/// <summary>
		/// Gets a value indicating whether this instance was disposed.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance was disposed; otherwise, <c>false</c>.
		/// </value>
		bool IsDisposed
		{
			get;
		}
	}
}
