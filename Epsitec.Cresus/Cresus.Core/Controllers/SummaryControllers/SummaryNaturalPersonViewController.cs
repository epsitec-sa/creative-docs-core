﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Controllers.DataAccessors;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.SummaryControllers
{
	public class SummaryNaturalPersonViewController : SummaryAbstractPersonViewController<Entities.NaturalPersonEntity>
	{
		public SummaryNaturalPersonViewController(string name, Entities.NaturalPersonEntity entity)
			: base (name, entity)
		{
		}


		protected override void CreateUI(TileContainer container)
		{
			var containerController = new TileContainerController (this, container);
			var data = containerController.DataItems;

			data.Add (
				new SummaryData
				{
					Name				= "NaturalPerson",
					IconUri				= "Data.NaturalPerson",
					Title				= UIBuilder.FormatText ("Personne physique"),
					CompactTitle		= UIBuilder.FormatText ("Personne"),
					TextAccessor		= Accessor.Create (this.EntityGetter, x => UIBuilder.FormatText (x.Title.Name, "\n", x.Firstname, x.Lastname, "(", x.Gender.Name, ")", "\n", x.BirthDate)),
					CompactTextAccessor = Accessor.Create (this.EntityGetter, x => UIBuilder.FormatText (x.Title.ShortName, x.Firstname, x.Lastname)),
					EntityAccessor		= this.EntityGetter,
				});

			var template1 = new CollectionTemplate<Entities.MailContactEntity> ("MailContact")
				.DefineTitle		(x => UIBuilder.FormatText ("Adresse", "(", string.Join (", ", x.Roles.Select (role => role.Name)), ")"))
				.DefineText			(x => UIBuilder.FormatText (x.LegalPerson.Name, "\n", x.LegalPerson.Complement, "\n", x.Complement, "\n", x.Address.Street.StreetName, "\n", x.Address.Street.Complement, "\n", x.Address.PostBox.Number, "\n", x.Address.Location.Country.Code, "~-", x.Address.Location.PostalCode, x.Address.Location.Name))
				.DefineCompactText	(x => UIBuilder.FormatText (x.Address.Street.StreetName, "~,", x.Address.Location.PostalCode, x.Address.Location.Name));

			var template2 = new CollectionTemplate<Entities.TelecomContactEntity> ("TelecomContact")
				.DefineTitle		(x => UIBuilder.FormatText (x.TelecomType.Name))
				.DefineText			(x => UIBuilder.FormatText (x.Number, "(", string.Join (", ", x.Roles.Select (role => role.Name)), ")"))
				.DefineCompactText  (x => UIBuilder.FormatText (x.Number, "(", x.TelecomType.Name, ")"));

			var template3 = new CollectionTemplate<Entities.UriContactEntity> ("UriContact", filter: x => x.UriScheme.Code == "mailto")
				.DefineText			(x => UIBuilder.FormatText (x.Uri, "(", string.Join (", ", x.Roles.Select (role => role.Name)), ")"))
				.DefineCompactText	(x => UIBuilder.FormatText (x.Uri))
				.DefineSetupItem	(x => x.UriScheme = CoreProgram.Application.Data.GetUriScheme ("mailto"));

			data.Add (
				new SummaryData
				{
					Name		 = "MailContact",
					IconUri		 = "Data.Mail",
					Title		 = UIBuilder.FormatText ("Adresse"),
					CompactTitle = UIBuilder.FormatText ("Adresse"),
					Text		 = UIBuilder.FormatText ("<i>vide</i>")
				});

			data.Add (
				new SummaryData
				{
					AutoGroup    = true,
					Name		 = "TelecomContact",
					IconUri		 = "Data.Telecom",
					Title		 = UIBuilder.FormatText ("Téléphone"),
					CompactTitle = UIBuilder.FormatText ("Téléphone"),
					Text		 = UIBuilder.FormatText ("<i>vide</i>")
				});

			data.Add (
				new SummaryData
				{
					AutoGroup    = true,
					Name		 = "UriContact",
					IconUri		 = "Data.Uri",
					Title		 = UIBuilder.FormatText ("E-Mail"),
					CompactTitle = UIBuilder.FormatText ("E-Mail"),
					Text		 = UIBuilder.FormatText ("<i>vide</i>")
				});

			data.Add (CollectionAccessor.Create (this.EntityGetter, x => x.Contacts, template1));
			data.Add (CollectionAccessor.Create (this.EntityGetter, x => x.Contacts, template2));
			data.Add (CollectionAccessor.Create (this.EntityGetter, x => x.Contacts, template3));

			containerController.GenerateTiles ();
		}
	}
}
