﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epsitec.Cresus.Core.Library.Business.ContentAccessors
{
	public enum DocumentItemAccessorMode
	{
		None								= 0,
		SpecialQuantitiesToDistinctLines	= 0x00000001,	// met toutes les quantités dans les mêmes colonnes DocumentItemAccessorColumn.Unique*
		ForceAllLines						= 0x00000002,	// force toutes les lignes, même si elles sont vides
		IncludeTaxes						= 0x00000004,	// 
		UseArticleInternalDescriptions		= 0x00000008,	// utilise les descriptions internes des articles
	}
}
