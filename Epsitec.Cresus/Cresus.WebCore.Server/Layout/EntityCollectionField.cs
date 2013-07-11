using System.Collections.Generic;


namespace Epsitec.Cresus.WebCore.Server.Layout
{


	internal sealed class EntityCollectionField : AbstractField
	{


		public string DatabaseName
		{
			get;
			set;
		}


		protected override string GetEditionTilePartType()
		{
			return "entityCollectionField";
		}


		public override Dictionary<string, object> ToDictionary()
		{
			var brick = base.ToDictionary ();

			brick["databaseName"] = this.DatabaseName;

			return brick;
		}


	}


}
