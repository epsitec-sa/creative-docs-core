﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Data;

namespace Epsitec.Cresus.Assets.Server.NodeGetters
{
	/// <summary>
	/// Accès en lecture à des données quelconques constituées de GuidNode/LevelNode/TreeNode.
	/// </summary>
	public abstract class AbstractNodeGetter<T> : INodeGetter<T>  // outputNodes
		where T : struct
	{
		public IEnumerable<T> Nodes
		{
			get
			{
				for (int i=0; i<this.Count; i++)
				{
					yield return this[i];
				}
			}
		}

		public abstract int Count
		{
			get;
		}

		public abstract T this[int row]
		{
			get;
		}
	}
}
