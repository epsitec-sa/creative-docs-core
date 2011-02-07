//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Dialogs;

using Epsitec.Cresus.Core.Entities;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.DocumentOptionsEditor
{
	public class MainController
	{
		public MainController(Widget parent, Core.Business.BusinessContext businessContext, DocumentOptionsEntity documentOptionsEntity)
		{
			this.businessContext       = businessContext;
			this.documentOptionsEntity = documentOptionsEntity;

		}


		public void CreateUI(Widget parent)
		{
			var t = new StaticText
			{
				Parent = parent,
				Text = "coucou",
				ContentAlignment = Common.Drawing.ContentAlignment.MiddleCenter,
				Dock = DockStyle.Fill,
			};
		}


		public void SaveDesign()
		{
		}


		private readonly Core.Business.BusinessContext		businessContext;
		private readonly DocumentOptionsEntity				documentOptionsEntity;
	}
}
