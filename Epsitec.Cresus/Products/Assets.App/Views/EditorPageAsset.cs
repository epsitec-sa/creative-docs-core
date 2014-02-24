﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class EditorPageAsset : AbstractEditorPage
	{
		public EditorPageAsset(DataAccessor accessor, BaseType baseType, BaseType subBaseType, bool isTimeless)
			: base (accessor, baseType, subBaseType, isTimeless)
		{
		}


		public override void CreateUI(Widget parent)
		{
			foreach (var userField in accessor.Settings.GetUserFields (BaseType.Assets)
				.Where (x => x.Type != FieldType.ComputedAmount && x.Type != FieldType.GuidPerson))
			{
				this.CreateController (parent, userField);
			}
		}
	}
}
