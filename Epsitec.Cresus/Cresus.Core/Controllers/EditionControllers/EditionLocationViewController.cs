//	Copyright © 2011-2012, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Cresus.Bricks;
using Epsitec.Cresus.Core.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.EditionControllers
{
	public class EditionLocationViewController : EditionViewController<LocationEntity>
	{
		protected override void CreateBricks(BrickWall<LocationEntity> wall)
		{
			wall.AddBrick ()
				.GlobalWarning ()
				.Input ()
				  .Field (x => x.Country)
				  .Field (x => x.Region)
				  .Field (x => x.PostalCode).Width (100)
				  .Field (x => x.Name)
			    .End ()
//				.Separator ()
//				.Input ()
//				  .Field (x => x.Language1)
//				  .Field (x => x.Language2)
//				.End ()
				;
		}
	}
}
