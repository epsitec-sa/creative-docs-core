//	Copyright � 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Types;

using Epsitec.Aider.Controllers.ActionControllers;
using Epsitec.Aider.Controllers.EditionControllers;
using Epsitec.Aider.Entities;
using Epsitec.Aider.Enumerations;

using Epsitec.Cresus.Bricks;
using Epsitec.Cresus.Core.Bricks;
using Epsitec.Cresus.Core.Controllers.SummaryControllers;
using Epsitec.Cresus.Core.Entities;

using System.Linq;
using Epsitec.Aider.Override;

namespace Epsitec.Aider.Controllers.SummaryControllers
{
	public sealed class SummaryAiderContactViewController : SummaryViewController<AiderContactEntity>
	{
		protected override void CreateBricks(BrickWall<AiderContactEntity> wall)
		{
			//	TODO: refactor this in a set of sub-controllers, just like SummaryAiderPersonWarningViewController
			var user = AiderUserManager.Current.AuthenticatedUser;
			var contact = this.Entity;
			var household = this.Entity.Household;

			FormattedText personTitle;

			if ((contact.Person.IsNotNull ()) &&
				(contact.Person.eCH_Person.DataSource == Enumerations.DataSource.Government))
			{
				personTitle = "Personne (RCH)";
			}
			else
			{
				personTitle = "Personne";
			}


			if ((contact.Person.IsNull ()) &&
				(contact.LegalPerson.IsNull ()))
			{
				wall.AddBrick ()
					.EnableActionMenu<ActionAiderContactViewController0CreatePerson> ();
			}

			wall.AddBrick ()
					.EnableActionMenu<ActionAiderContactViewController1AddToBag> ();

			var contactSummary = SummaryAiderContactViewController.GetPersonContactSummary (contact);

			//	TODO: add phone/...

			switch (this.Entity.ContactType)
			{
				case Enumerations.ContactType.Deceased:
					if (contact.Person.IsNotNull ())
					{						
						wall.AddBrick (x => x.Person)
							.Title (personTitle)
							.Icon (contact.Person.GetIconName ("Data"))
							.Text (contactSummary)
							.Attribute (BrickMode.DefaultToSummarySubView);
					}
					break;

				case Enumerations.ContactType.PersonHousehold:
					if (contact.Person.IsNotNull ())
					{						
						wall.AddBrick (x => x.Person)
							.Title (personTitle)
							.Icon (contact.Person.GetIconName ("Data"))
							.Text (contactSummary)
							.Attribute (BrickMode.DefaultToSummarySubView);
					}
					
					if (household.IsNotNull ())
					{
						if (contact.Address.IsNotNull ())
						{
							var html = contact.Address.GetHtmlForLocationWebServices (contact.DisplayName);

							wall.AddBrick ()
								.Title (new FormattedText (Resources.Text ("Adresse de domicile") + html))
								.Text (contact.Address.GetSummary ())
								.Icon ("Data.AiderAddress")
								.WithSpecialController (typeof (EditionAiderContactViewController1Address));
						}

						if (household.Members.Count > 1)
						{
							wall.AddBrick (x => x.Household.Members)
								.Title (Resources.Text ("Membres du m�nage"))
								.Icon ("Data.AiderPersons")
								.Attribute (BrickMode.HideAddButton)
								.Attribute (BrickMode.HideRemoveButton)
								.Attribute (BrickMode.AutoGroup)
								.Template ()
									.Text (x => x.GetCompactSummary (household))
								.End ()
								.Attribute (BrickMode.DefaultToSummarySubView);
						}
					}
					break;

				case Enumerations.ContactType.PersonAddress:
					if (contact.Person.IsNotNull ())
					{
						wall.AddBrick (x => x.Person)
							.Title (personTitle)
							.Icon (contact.Person.GetIconName ("Data"))
							.Text (contactSummary)
							.Attribute (BrickMode.DefaultToSummarySubView);
					}
					if (contact.Address.IsNotNull ())
					{
						if (contact.AddressType == AddressType.Confidential)
						{
							if (user.CanViewConfidentialAddress ())
							{
								wall.AddBrick ()
								.Title (TextFormatter.FormatText (contact.AddressType))
								.Text (contact.Address.GetSummary ())
								.Icon ("Data.AiderAddress")
								.WithSpecialController (typeof (EditionAiderContactViewController1Address));
							}
						}
						else
						{
							wall.AddBrick ()
							.Title (TextFormatter.FormatText (contact.AddressType))
							.Text (contact.Address.GetSummary ())
							.Icon ("Data.AiderAddress")
							.WithSpecialController (typeof (EditionAiderContactViewController1Address));
						}
						
					}
					break;

				case Enumerations.ContactType.Legal:
					if (contact.LegalPerson.IsNotNull ())
					{
						wall.AddBrick (x => x.LegalPerson)
							.Attribute (BrickMode.DefaultToSummarySubView);

						wall.AddBrick ()
							.Title ("Adresse de base")
							.Icon ("Data.AiderAddress")
							.Text (x => x.LegalPerson.Address.GetSummary ())
							.WithSpecialController (typeof (EditionAiderContactViewController1Address));
					}

					var contactPersonSummary = string.IsNullOrEmpty (contact.LegalPersonContactFullName)
						? FormattedText.Empty
						: TextFormatter.FormatText (contact.LegalPersonContactMrMrs.GetShortText (), contact.LegalPersonContactFullName);

					wall.AddBrick ()
						.Icon (AiderPersonEntity.GetIconName ("Data", contact.LegalPersonContactMrMrs, contact.LegalPerson.Language))
						.Title ("Personne de contact")
						.Text (contactPersonSummary)
						.WithSpecialController (typeof (EditionAiderContactViewController2LegalContact));
					break;

				default:
					break;
			}
		}

		public static FormattedText GetPersonContactSummary(AiderContactEntity contact)
		{
			var user = AiderUserManager.Current.AuthenticatedUser;
			var text = contact.Person.GetCompactSummary ();
			var contactInfoPrivate   = text;
			var contactInfoConfidential = FormattedText.Empty;
			var contactInfoProf			= FormattedText.Empty;
			var contactInfoSecondary	= FormattedText.Empty;

			foreach (var detail in contact.Person.Contacts.Where (x => x.Address.IsNotNull ()))
			{
				var address = detail.Address;
				var phone   = address.GetPhoneSummary ();
				var email   = address.GetWebEmailSummary ();

				if ((phone.IsNullOrEmpty ()) &&
					(email.IsNullOrEmpty ()))
				{
					continue;
				}

				switch (detail.AddressType)
				{
					case Enumerations.AddressType.Default:
					
					case Enumerations.AddressType.Other:					
						contactInfoPrivate = TextFormatter.FormatText (contactInfoPrivate, "\n", phone, "\n", email);
						break;
					case Enumerations.AddressType.Confidential:
						if (user.CanViewConfidentialAddress ())
						{
							if (contactInfoConfidential.IsNullOrEmpty ())
							{
								contactInfoConfidential = new FormattedText ("<hr/><b>Confidentiel:</b>");
							}
							contactInfoConfidential = TextFormatter.FormatText (contactInfoConfidential, "\n", phone, "\n", email);
						}
						break;
					case Enumerations.AddressType.Professional:
						if (contactInfoProf.IsNullOrEmpty ())
						{
							contactInfoProf = new FormattedText ("<hr/><b>Professionnel:</b>");
						}
						contactInfoProf = TextFormatter.FormatText (contactInfoProf, "\n", phone, "\n", email);
						break;

					case Enumerations.AddressType.Secondary:
						if (contactInfoSecondary.IsNullOrEmpty ())
						{
							contactInfoSecondary = new FormattedText ("<hr/><b>Domicile secondaire:</b>");
						}
						contactInfoSecondary = TextFormatter.FormatText (contactInfoSecondary, "\n", phone, "\n", email);
						break;
				}
			}

			return TextFormatter.FormatText (contactInfoPrivate, "\n", contactInfoProf, "\n", contactInfoSecondary, "\n", contactInfoConfidential);
		}
	}
}
