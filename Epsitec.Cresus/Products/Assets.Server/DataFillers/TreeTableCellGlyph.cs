//	Copyright � 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Server.BusinessLogic;

namespace Epsitec.Cresus.Assets.Server.DataFillers
{
	public class TreeTableCellGlyph : AbstractTreeTableCell
	{
		public TreeTableCellGlyph(bool isValid, TimelineGlyph? value, bool isSelected = false, bool isEvent = false, bool isError = false, bool isUnavailable = false)
		{
			this.IsValid       = isValid;
			this.Value         = value;
			this.IsSelected    = isSelected;
			this.IsEvent       = isEvent;
			this.IsError       = isError;
			this.IsUnavailable = isUnavailable;
		}


		public readonly bool					IsValid;
		public readonly TimelineGlyph?			Value;
		public readonly bool					IsSelected;
		public readonly bool					IsEvent;
		public readonly bool					IsError;
		public readonly bool					IsUnavailable;

		
		public override string ToString()
		{
			var buffer = new System.Text.StringBuilder ();

			if (!this.IsValid)
			{
				buffer.Append ("invalid ");
			}

			buffer.Append (this.Value);
			buffer.Append (" ");

			if (this.IsSelected)
			{
				buffer.Append (" selected");
			}
			if (this.IsError)
			{
				buffer.Append (" error");
			}

			return buffer.ToString ();
		}
	}
}
