//	Copyright � 2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Common.Support.EntityEngine
{
	/// <summary>
	/// The <c>EntityStatusAccumulationMode</c> is used to specify how the <see cref="EntityStatusAccumulator"/>
	/// handles <c>null</c> items or empty collections of items.
	/// </summary>
	public enum EntityStatusAccumulationMode
	{
		NoneIsValid,
		NoneIsInvalid,
	}
}
