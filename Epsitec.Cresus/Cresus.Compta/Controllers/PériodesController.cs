//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
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
	/// Ce contrôleur gère les périodes comptables de la comptabilité.
	/// </summary>
	public class PériodesController : AbstractController
	{
		public PériodesController(ComptaApplication app, BusinessContext businessContext, MainWindowController mainWindowController)
			: base (app, businessContext, mainWindowController)
		{
			this.dataAccessor = new PériodesDataAccessor (this);
		}


		public override ControllerType ControllerType
		{
			get
			{
				return Controllers.ControllerType.Périodes;
			}
		}


		public override bool AcceptPériodeChanged
		{
			get
			{
				return false;
			}
		}


		protected override int ArrayLineHeight
		{
			get
			{
				return 20;
			}
		}

		protected override FormattedText GetArrayText(int row, ColumnType columnType)
		{
			//	Retourne le texte contenu dans une cellule.
			return this.dataAccessor.GetText (row, columnType);
		}


		protected override void CreateEditor(FrameBox parent)
		{
			this.editorController = new PériodeEditorController (this);
			this.editorController.CreateUI (parent, this.UpdateArrayContent);
			this.editorController.ShowInfoPanel = this.mainWindowController.ShowInfoPanel;
		}


		protected override IEnumerable<ColumnMapper> InitialColumnMappers
		{
			get
			{
				yield return new ColumnMapper (ColumnType.Utilise,   0.20, ContentAlignment.MiddleCenter, "En cours",                 "Détermine l'exercice comptable en cours");
				yield return new ColumnMapper (ColumnType.DateDébut, 0.50, ContentAlignment.MiddleLeft,   "Dates de début et de fin", "Date de début de l'exercice (inclue)");
				yield return new ColumnMapper (ColumnType.DateFin,   0.00, ContentAlignment.MiddleLeft,   "",                         "Date de fin de l'exercice (inclue)");
				yield return new ColumnMapper (ColumnType.Titre,     0.00, ContentAlignment.MiddleLeft,   "Commentaire",              "Commentaire affiché entre parenthèses après l'exercice");
				yield return new ColumnMapper (ColumnType.Pièce,     0.00, ContentAlignment.MiddleLeft,   "Générateur de pièces",     "Générateur pour les numéros de pièces (facultatif)");
				yield return new ColumnMapper (ColumnType.Résumé,    1.50, ContentAlignment.MiddleLeft,   "Résumé", edition: false);
			}
		}
	}
}
