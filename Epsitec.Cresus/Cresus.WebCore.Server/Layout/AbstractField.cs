﻿//	Copyright © 2012-2014, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Marc BETTEX, Maintainer: Pierre ARNAUD

using System.Collections.Generic;

namespace Epsitec.Cresus.WebCore.Server.Layout
{
	/// <summary>
	/// This is the base class for all fields that are used in edition tiles.
	/// </summary>
	internal abstract class AbstractField : AbstractEditionTilePart
	{
		protected AbstractField()
		{
		}


		public string							Id
		{
			get;
			set;
		}


		public string							Title
		{
			get;
			set;
		}


		public bool								IsReadOnly
		{
			get;
			set;
		}


		public bool								AllowBlank
		{
			get;
			set;
		}


		public object							Value
		{
			get;
			set;
		}

		
		public override Dictionary<string, object> ToDictionary()
		{
			var brick = base.ToDictionary ();

			brick["name"]       = this.Id;
			brick["title"]      = this.Title;
			brick["readOnly"]   = this.IsReadOnly;
			brick["allowBlank"] = this.AllowBlank;
			brick["value"]      = this.Value;

			return brick;
		}

		internal static string GetTypeName(System.Type type)
		{
			var name = type.Name;

			return name.Substring (0, 1).ToLowerInvariant () + name.Substring (1);
		}
	}
}
