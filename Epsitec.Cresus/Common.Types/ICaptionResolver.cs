//	Copyright � 2008, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;

namespace Epsitec.Common.Types
{
	/// <summary>
	/// The <c>ICaptionResolver</c> interface is used to resolve an id into a
	/// <see cref="Caption"/> instance.
	/// </summary>
	public interface ICaptionResolver
	{
		/// <summary>
		/// Gets the caption for the specified id.
		/// </summary>
		/// <param name="id">The caption id.</param>
		/// <returns>The caption <c>null</c>.</returns>
		Caption GetCaption(Druid id);
	}
}
