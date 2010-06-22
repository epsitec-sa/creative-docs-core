//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers
{
	/// <summary>
	/// The <c>BrowserSettingsMode</c> defines how the browser settings will
	/// be displayed in the user interface.
	/// </summary>
	public enum BrowserSettingsMode
	{
		/// <summary>
		/// Undefined; don't use this mode.
		/// </summary>
		Undefined,

		/// <summary>
		/// Hide the settings.
		/// </summary>
		Hidden,

		/// <summary>
		/// Show the compact settings, at the top of the browser list.
		/// </summary>
		Compact,

		/// <summary>
		/// Show the expanded settings, full width on the top border of the main view.
		/// </summary>
		Expanded,
	}
}
