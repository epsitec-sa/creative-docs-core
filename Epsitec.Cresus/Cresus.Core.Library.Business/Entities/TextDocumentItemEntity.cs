//	Copyright © 2010-2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;
using Epsitec.Common.Support.EntityEngine;

using Epsitec.Cresus.Core.Controllers.TabIds;
using Epsitec.Cresus.Core.Helpers;

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Core.Business;

namespace Epsitec.Cresus.Core.Entities
{
	public partial class TextDocumentItemEntity : ICopyableEntity<TextDocumentItemEntity>
	{
		public override FormattedText GetCompactSummary()
		{
			if (this.Text.IsNullOrEmpty ())
			{
				return "<i>Texte</i>";
			}
			else
			{
				return this.Text.Lines.FirstOrDefault ().ToSimpleText ();
			}
		}

		public override EntityStatus GetEntityStatus()
		{
			return EntityStatus.Valid;
		}


		#region ICloneable<TextDocumentItemEntity> Members

		void ICopyableEntity<TextDocumentItemEntity>.CopyTo(BusinessContext businessContext, TextDocumentItemEntity copy)
		{
			copy.Attributes    = this.Attributes;
			copy.GroupIndex    = this.GroupIndex;

			copy.Text          = this.Text;
		}

		#endregion
	}
}
