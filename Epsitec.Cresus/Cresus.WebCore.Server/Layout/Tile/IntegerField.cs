﻿using System.Collections.Generic;


namespace Epsitec.Cresus.WebCore.Server.Layout.Tile
{


	internal sealed class IntegerField : AbstractField
	{


		public long? Value
		{
			get;
			set;
		}


		protected override string GetEditionTilePartType()
		{
			return "integerField";
		}


		protected override object GetValue()
		{
			return this.Value;
		}


	}


}
