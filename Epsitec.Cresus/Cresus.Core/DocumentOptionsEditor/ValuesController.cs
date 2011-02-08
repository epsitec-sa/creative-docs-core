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
	public class ValuesController
	{
		public ValuesController(Core.Business.BusinessContext businessContext, DocumentOptionsEntity documentOptionsEntity, Dictionary<string, string> options)
		{
			this.businessContext       = businessContext;
			this.documentOptionsEntity = documentOptionsEntity;
			this.options               = options;
		}


		public void CreateUI(Widget parent)
		{
		}


		public void Update()
		{
		}


		private void SetDirty()
		{
			this.businessContext.NotifyExternalChanges ();
		}


		private readonly Core.Business.BusinessContext		businessContext;
		private readonly DocumentOptionsEntity				documentOptionsEntity;
		private readonly Dictionary<string, string>			options;
	}
}
