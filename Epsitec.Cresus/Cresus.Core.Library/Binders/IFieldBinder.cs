﻿//	Copyright © 2010-2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;
using Epsitec.Common.Types.Converters;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Binders
{
	/// <summary>
	/// The <c>IFieldBinder</c> interface provides conversion and validation
	/// methods used by the UI binding code, when reading/writing data stored in
	/// entity fields.
	/// </summary>
	public interface IFieldBinder
	{
		/// <summary>
		/// Converts the string from its raw representation to the UI representation.
		/// </summary>
		/// <param name="value">The raw value, as returned by the marshaler.</param>
		/// <returns>The value which should be used for edition.</returns>
		string ConvertToUI(string value);

		/// <summary>
		/// Converts the string from its UI representation to the raw representation.
		/// </summary>
		/// <param name="value">The UI value.</param>
		/// <returns>The value which should be handed back to the marshaler.</returns>
		string ConvertFromUI(string value);

		/// <summary>
		/// Validates the data from the UI. The data format is expected to be OK; this
		/// method can be used to apply additional validation rules.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The validation result.</returns>
		IValidationResult ValidateFromUI(string value);

		/// <summary>
		/// Attaches the field binder to the specified marshaler. This can be used to
		/// configure the marshaler's converter in order to filter and preprocess the
		/// data at the marshaler level.
		/// </summary>
		/// <param name="marshaler">The marshaler.</param>
		void Attach(Marshaler marshaler);
	}
}
