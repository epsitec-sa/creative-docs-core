using System.Collections.Generic;


namespace Epsitec.Cresus.WebCore.Server.Layout
{


	/// <summary>
	/// This class represents an edition field used to edit enumeration fields.
	/// </summary>
	internal sealed class EnumerationField : AbstractField
	{


		public string TypeName
		{
			get;
			set;
		}


		protected override string GetEditionTilePartType()
		{
			return "enumerationField";
		}


		public override Dictionary<string, object> ToDictionary()
		{
			var brick = base.ToDictionary ();

			brick["enumerationName"] = this.TypeName;

			return brick;
		}


	}


}
