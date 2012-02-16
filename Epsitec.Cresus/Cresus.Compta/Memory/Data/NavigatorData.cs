﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Types;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Compta.Controllers;
using Epsitec.Cresus.Compta.Search.Data;
using Epsitec.Cresus.Compta.Options.Data;
using Epsitec.Cresus.Compta.Helpers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Memory.Data
{
	/// <summary>
	/// Mémorise les paramètres d'un contrôleur, pour permettre de le recréer dans le même état.
	/// </summary>
	public class NavigatorData
	{
		public NavigatorData(Command command, FormattedText description, MemoryData memory, SearchData search, SearchData filter, AbstractOptions options)
		{
			this.Command     = command;
			this.Description = description;
			this.Memory      = memory;
			this.Search      = search;
			this.Filter      = filter;
			this.Options     = options;
		}


		public Command Command
		{
			get;
			private set;
		}

		public FormattedText Description
		{
			get;
			private set;
		}


		public MemoryData Memory
		{
			get;
			private set;
		}

		public SearchData Search
		{
			get;
			private set;
		}

		public SearchData Filter
		{
			get;
			private set;
		}

		public AbstractOptions Options
		{
			get;
			private set;
		}
	}
}