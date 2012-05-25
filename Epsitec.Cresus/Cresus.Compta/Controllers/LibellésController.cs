﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Converters;

using Epsitec.Cresus.Core.Business;

using Epsitec.Cresus.Compta.Accessors;
using Epsitec.Cresus.Compta.Entities;
using Epsitec.Cresus.Compta.Helpers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Controllers
{
	/// <summary>
	/// Ce contrôleur gère les libellés usuels de la comptabilité.
	/// </summary>
	public class LibellésController : AbstractController
	{
		public LibellésController(ComptaApplication app, BusinessContext businessContext, MainWindowController mainWindowController)
			: base (app, businessContext, mainWindowController)
		{
			this.dataAccessor = new LibellésDataAccessor (this);
		}


		public override ControllerType ControllerType
		{
			get
			{
				return Controllers.ControllerType.Libellés;
			}
		}

		protected override void UpdateTitle()
		{
			this.SetTitle ();
		}


		public override bool AcceptPériodeChanged
		{
			get
			{
				return false;
			}
		}


		protected override FormattedText GetArrayText(int row, ColumnType columnType)
		{
			//	Retourne le texte contenu dans une cellule.
			return this.dataAccessor.GetText (row, columnType);
		}


		protected override void CreateEditor(FrameBox parent)
		{
			this.editorController = new LibellésEditorController (this);
			this.editorController.CreateUI (parent, this.UpdateArrayContent);
			this.editorController.ShowInfoPanel = this.mainWindowController.ShowInfoPanel;
		}


		protected override IEnumerable<ColumnMapper> InitialColumnMappers
		{
			get
			{
				yield return new ColumnMapper (ColumnType.Libellé,   1.60, ContentAlignment.MiddleLeft,   "Libellé",   "Texte du libellé usuel qui est conservé");
				yield return new ColumnMapper (ColumnType.Permanent, 0.20, ContentAlignment.MiddleCenter, "Permanent", "Détermine si le libellé est permamant");
			}
		}
	}
}
