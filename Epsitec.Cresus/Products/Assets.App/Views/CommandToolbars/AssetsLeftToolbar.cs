﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;

using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Views.CommandToolbars
{
	/// <summary>
	/// Toolbar utilisée pour montrer les objets d'immobilisations en mode "plusieurs
	/// timelines", dans la partie gauche.
	/// </summary>
	public class AssetsLeftToolbar : AbstractCommandToolbar
	{
		public AssetsLeftToolbar(DataAccessor accessor, CommandContext commandContext)
			: base (accessor, commandContext)
		{
		}


		public override void CreateUI(Widget parent)
		{
			base.CreateUI (parent);

			this.CreateButton (Res.Commands.AssetsLeft.Filter, 0);

			this.CreateButton (Res.Commands.AssetsLeft.First, 5);
			this.CreateButton (Res.Commands.AssetsLeft.Prev, 1);
			this.CreateButton (Res.Commands.AssetsLeft.Next, 1);
			this.CreateButton (Res.Commands.AssetsLeft.Last, 5);

			this.CreateSeparator (2);

			this.CreateButton (Res.Commands.AssetsLeft.CompactAll, 2);
			this.CreateButton (Res.Commands.AssetsLeft.CompactOne, 4);
			this.CreateButton (Res.Commands.AssetsLeft.ExpandOne, 4);
			this.CreateButton (Res.Commands.AssetsLeft.ExpandAll, 2);

			this.CreateSeparator (3);

			this.CreateButton (Res.Commands.AssetsLeft.New, 3);
			this.CreateButton (Res.Commands.AssetsLeft.Delete, 3);
			this.CreateButton (Res.Commands.AssetsLeft.Deselect, 6);
		}
	}
}
