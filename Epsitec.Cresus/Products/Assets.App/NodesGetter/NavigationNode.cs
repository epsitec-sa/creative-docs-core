﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.App.Views;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.NodesGetter
{
	/// <summary>
	///	Noeud correspondant à une ligne de l'historique de navigation.
	/// </summary>
	public struct NavigationNode
	{
		public NavigationNode(Guid navigationGuid, ViewType viewType, PageType pageType, Timestamp? timestamp, string description)
		{
			this.NavigationGuid = navigationGuid;
			this.ViewType       = viewType;
			this.PageType       = pageType;
			this.Timestamp      = timestamp;
			this.Description    = description;
		}

		public bool IsEmpty
		{
			get
			{
				return this.NavigationGuid.IsEmpty
					&& this.ViewType    == ViewType.Unknown
					&& this.PageType    == PageType.Unknown
					&& this.Description == null;
			}
		}

		public static NavigationNode Empty = new NavigationNode (Guid.Empty, ViewType.Unknown, PageType.Unknown, null, null);

		public readonly Guid				NavigationGuid;
		public readonly ViewType			ViewType;
		public readonly PageType			PageType;
		public readonly Timestamp?			Timestamp;
		public readonly string				Description;
	}
}
