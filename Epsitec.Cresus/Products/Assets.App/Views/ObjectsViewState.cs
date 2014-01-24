﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class ObjectsViewState : AbstractViewState, System.IEquatable<AbstractViewState>
	{
		public ViewMode							ViewMode;
		public PageType							PageType;
		public Timestamp?						SelectedTimestamp;
		public Guid								SelectedGuid;


		#region IEquatable<AbstractViewState> Members
		public override bool Equals(AbstractViewState other)
		{
			if (!base.Equals (other))
			{
				return false;
			}

			var o = other as ObjectsViewState;

			if (o == null)
			{
				return false;
			}

			return this.ViewType          == o.ViewType
				&& this.ViewMode          == o.ViewMode
				&& this.PageType          == o.PageType
				&& this.SelectedTimestamp == o.SelectedTimestamp
				&& this.SelectedGuid      == o.SelectedGuid;
		}
		#endregion
	}
}
