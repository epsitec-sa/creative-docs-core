﻿using System.Collections.Generic;


namespace Epsitec.Cresus.WebCore.Server.Layout.Tile
{


	internal sealed class TextAreaField : AbstractField
	{

		public string Value
		{
			get;
			set;
		}


		public override Dictionary<string, object> ToDictionary()
		{
			var brick = base.ToDictionary ();

			brick["type"] = "textAreaField";
			brick["value"] = this.Value;

			return brick;
		}


	}


}