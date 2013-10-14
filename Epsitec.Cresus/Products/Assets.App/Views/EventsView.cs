﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;

using Epsitec.Common.Widgets;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Cresus.Assets.App.Widgets;
using Epsitec.Cresus.Assets.Server.NaiveEngine;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class EventsView : AbstractView
	{
		public EventsView(DataAccessor accessor)
			: base (accessor)
		{
		}

		public override void CreateUI(Widget parent, MainToolbar toolbar)
		{
			base.CreateUI (parent, toolbar);

			this.Update ();
		}
	}
}
