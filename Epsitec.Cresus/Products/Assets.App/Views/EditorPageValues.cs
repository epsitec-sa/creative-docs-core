﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class EditorPageValues : AbstractEditorPage
	{
		public EditorPageValues(DataAccessor accessor, BaseType baseType, BaseType subBaseType, bool isTimeless)
			: base (accessor, baseType, subBaseType, isTimeless)
		{
		}


		protected internal override void CreateUI(Widget parent)
		{
			parent = this.CreateScrollable (parent);

			this.CreateAmortizedAmountController (parent, ObjectField.MainValue);

			foreach (var field in this.accessor.Settings.GetUserFields (BaseType.Assets)
				.Where (x => x.Type == FieldType.ComputedAmount)
				.Select (x => x.Field))
			{
				this.CreateComputedAmountController (parent, field);
			}
		}
	}
}
