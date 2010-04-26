﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using System.Text;

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

namespace Epsitec.Cresus.Core.Controllers
{
	public class MailContactViewController : EntityViewController
	{
		public MailContactViewController(string name)
			: base (name)
		{
		}

		public override IEnumerable<CoreController> GetSubControllers()
		{
			yield break;
		}

		public override void CreateUI(Widget container)
		{
			this.container = container;

			System.Diagnostics.Debug.Assert (this.Entity != null);
			var accessor = new EntitiesAccessors.MailContactAccessor (this.Entity as Entities.MailContactEntity, false);

			FrameBox frame = this.CreateEditionTile (accessor, ViewControllerMode.None);
			FrameBox group;

			this.CreateTextField (frame, 0, "Roles", accessor.Roles, x => accessor.Roles = x, Validators.StringValidator.Validate);
			this.CreateMargin (frame, true);

			this.CreateTextField (frame, 0, "Rue", accessor.StreetName, x => accessor.StreetName = x, Validators.StringValidator.Validate);
			this.CreateTextFieldMulti (frame, 52, "Complément de l'adresse", accessor.StreetComplement, x => accessor.StreetComplement = x, null);
			this.CreateTextField (frame, 0, "Boîte postale", accessor.PostBoxNumber, x => accessor.PostBoxNumber = x, Validators.StringValidator.Validate);

			group = this.CreateGroup (frame, "Numéro postal et ville");
			this.CreateTextField (group, 50, accessor.LocationPostalCode, x => accessor.LocationPostalCode = x, Validators.StringValidator.Validate);
			this.CreateTextField (group, 0, accessor.LocationName, x => accessor.LocationName = x, Validators.StringValidator.Validate);

			group = this.CreateGroup (frame, "Code et nom du pays");
			this.CreateTextField (group, 50, accessor.CountryCode, x => accessor.CountryCode = x, Validators.StringValidator.Validate);
			this.CreateTextField (group, 0, accessor.CountryName, x => accessor.CountryName = x, Validators.StringValidator.Validate);

			group = this.CreateGroup (frame, "Code et nom de la région");
			this.CreateTextField (group, 50, accessor.RegionCode, x => accessor.RegionCode = x, Validators.StringValidator.Validate);
			this.CreateTextField (group, 0, accessor.RegionName, x => accessor.RegionName = x, Validators.StringValidator.Validate);

			this.SetInitialFocus ();
		}
	}
}
