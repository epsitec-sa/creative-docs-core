﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;
using Epsitec.Common.Support.EntityEngine;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Controllers.DataAccessors;
using Epsitec.Cresus.Core.Widgets;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.SummaryControllers
{
	public class SummaryRelationViewController : SummaryViewController<Entities.RelationEntity>
	{
		public SummaryRelationViewController(string name, Entities.RelationEntity entity)
			: base (name, entity)
		{
		}


		protected override void CreateUI(TileContainer container)
		{
			var containerController = new TileContainerController (this, container);
			var data = containerController.DataItems;

			this.CreateUIRelation (data);
			this.CreateUIMailContacts (data);
			this.CreateUITelecomContacts (data);
			this.CreateUIUriContacts (data);

			containerController.GenerateTiles ();
		}

		protected override void OnChildItemCreated(AbstractEntity entity)
		{
			this.SetupNewContact (entity as AbstractContactEntity);
			base.OnChildItemCreated (entity);
		}

		protected override EditionStatus GetEditionStatus()
		{
			var entity = this.Entity;
			return entity.IsEmpty () ? EditionStatus.Empty : EditionStatus.Valid;
		}

		protected override void UpdateEmptyEntityStatus(DataLayer.DataContext context, bool isEmpty)
		{
			var entity = this.Entity;
			
			this.UpdateRelationDefaultAddress ();
			
			context.UpdateEmptyEntityStatus (entity, isEmpty);
			context.UpdateEmptyEntityStatus (entity.Person, x => x.IsEmpty ());
		}

		private void CreateUIRelation(SummaryDataItems data)
		{
			data.Add (
				new SummaryData
				{
					Name				= "Customer",
					IconUri				= "Data.Customer",
					Title				= UIBuilder.FormatText ("Client"),
					CompactTitle		= UIBuilder.FormatText ("Client"),
					TextAccessor		= Accessor.Create (this.EntityGetter, x => UIBuilder.FormatText ("N°", x.Id, "\n", this.PersonText, "\n", "Client depuis: ~", x.FirstContactDate, "\n", "Représentant: ~", this.SalesRepresentativeText)),
					CompactTextAccessor = Accessor.Create (this.EntityGetter, x => UIBuilder.FormatText ("N°", x.Id, "\n", this.PersonCompactText)),
					EntityAccessor		= this.EntityMarshaler,
				});
		}

		private FormattedText PersonText
		{
			get
			{
				if (this.Entity.Person is Entities.NaturalPersonEntity)
				{
					var x = this.Entity.Person as Entities.NaturalPersonEntity;

					return UIBuilder.FormatText (x.Title.Name, "\n", x.Firstname, x.Lastname, "(", x.Gender.Name, ")");
				}

				if (this.Entity.Person is Entities.LegalPersonEntity)
				{
					var x = this.Entity.Person as Entities.LegalPersonEntity;

					return UIBuilder.FormatText (x.Name);
				}

				return FormattedText.Empty;
			}
		}

		private FormattedText PersonCompactText
		{
			get
			{
				if (this.Entity.Person is Entities.NaturalPersonEntity)
				{
					var x = this.Entity.Person as Entities.NaturalPersonEntity;

					return UIBuilder.FormatText (x.Title.ShortName, x.Firstname, x.Lastname);
				}

				if (this.Entity.Person is Entities.LegalPersonEntity)
				{
					var x = this.Entity.Person as Entities.LegalPersonEntity;

					return UIBuilder.FormatText (x.Name);
				}

				return FormattedText.Empty;
			}
		}

		private FormattedText SalesRepresentativeText
		{
			get
			{
				if (this.Entity.SalesRepresentative is Entities.NaturalPersonEntity)
				{
					var x = this.Entity.SalesRepresentative as Entities.NaturalPersonEntity;

					return UIBuilder.FormatText (x.Title.ShortName, x.Firstname, x.Lastname);
				}

				return FormattedText.Empty;
			}
		}


		private void CreateUIMailContacts(SummaryDataItems data)
		{
			Common.CreateUIMailContacts (data, this.EntityGetter, x => x.Person.Contacts);
		}

		private void CreateUITelecomContacts(SummaryDataItems data)
		{
			Common.CreateUITelecomContacts (data, this.EntityGetter, x => x.Person.Contacts);
		}

		private void CreateUIUriContacts(SummaryDataItems data)
		{
			Common.CreateUIUriContacts (data, this.EntityGetter, x => x.Person.Contacts);
		}
		
		private void UpdateRelationDefaultAddress()
		{
			var customer   = this.Entity;
			var oldAddress = EntityNullReferenceVirtualizer.UnwrapNullEntity (customer.DefaultAddress);
			var newAddress = EntityNullReferenceVirtualizer.UnwrapNullEntity (customer.Person.Contacts.Where (x => x is Entities.MailContactEntity).Cast<Entities.MailContactEntity> ().Select (x => x.Address).FirstOrDefault ());

			if (oldAddress != newAddress)
			{
				customer.DefaultAddress = newAddress;
			}
		}

		private void SetupNewContact(AbstractContactEntity contact)
		{
			if (contact != null)
			{
				var naturalPerson = this.Entity.Person as NaturalPersonEntity;
				var legalPerson   = this.Entity.Person as LegalPersonEntity;

				//	Attach the contact to the customer's person entity, which can be either a natural
				//	person or a legal person:

				if (naturalPerson != null)
				{
					contact.NaturalPerson = naturalPerson;
				}
				if (legalPerson != null)
				{
					contact.LegalPerson = legalPerson;
				}
			}
		}
	}
}
