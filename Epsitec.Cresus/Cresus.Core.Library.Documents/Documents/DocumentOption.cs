﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Epsitec.Cresus.Core.Documents
{
	public enum DocumentOption
	{
		//	ATTENTION: Les noms des options sont sérialisés. Il ne faut donc pas les changer !

		None,

		Orientation,					// enum: Portrait, Landscape

		Specimen,						// bool

		FontSize,						// dimension: taille de la police

		LeftMargin,						// distance
		RightMargin,					// distance
		TopMargin,						// distance
		BottomMargin,					// distance

		HeaderLogo,						// bool
		HeaderLogoLeft,					// distance: logo
		HeaderLogoTop,					// distance
		HeaderLogoWidth,				// distance
		HeaderLogoHeight,				// distance

		HeaderFromLeft,					// distance: adresse de l'entreprise
		HeaderFromTop,					// distance
		HeaderFromWidth,				// distance
		HeaderFromHeight,				// distance

		HeaderForLeft,					// distance: concerne
		HeaderForTop,					// distance
		HeaderForWidth,					// distance
		HeaderForHeight,				// distance

		HeaderNumberLeft,				// distance: numéro de facture
		HeaderNumberTop,				// distance
		HeaderNumberWidth,				// distance
		HeaderNumberHeight,				// distance

		HeaderToLeft,					// distance: adresse destinataire
		HeaderToTop,					// distance
		HeaderToWidth,					// distance
		HeaderToHeight,					// distance

		HeaderLocDateLeft,				// distance: localité, le xxx
		HeaderLocDateTop,				// distance
		HeaderLocDateWidth,				// distance
		HeaderLocDateHeight,			// distance

		TableTopAfterHeader,			// distance
		
		LayoutFrame,					// enum: Frameless, WithLine, WithFrame
		GapBeforeGroup,					// bool
		IndentWidth,					// distance: longueur de l'indentation par niveau

		LineNumber,						// enum: None, Group, Line, Full

		ArticleAdditionalQuantities,	// bool
		ArticleId,						// bool

		ColumnsOrder,					// enum: QD, DQ

		IsrPosition,					// enum: WithInside, WithOutside, Without
		IsrType,						// enum: Isr, Is
		IsrFacsimile,					// bool

		Signing,						// bool

		RelationMail,					// bool
		RelationTelecom,				// bool
		RelationUri,					// bool
	}
}
