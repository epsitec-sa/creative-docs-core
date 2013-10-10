//	Copyright � 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epsitec.Cresus.Assets.Server.NaiveEngine
{
	public class DataDecimalProperty : AbstractDataProperty
	{
		public DataDecimalProperty(int id, decimal value)
			: base (id)
		{
			this.Value = value;
		}

		public readonly decimal Value;
	}
}
