//	Copyright � 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Aider.Entities;
using Epsitec.Aider.Enumerations;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Business;

using System.Collections.Generic;
using System.Linq;
using Nest;
using Epsitec.Common.Support;
using Epsitec.Cresus.Core.NoSQL;

namespace Epsitec.Aider.Rules
{
	[BusinessRule]
	internal class AiderContactBusinessRules : GenericBusinessRule<AiderContactEntity>
	{
		public override void ApplyBindRule(AiderContactEntity entity)
		{
			var businessContext = this.GetBusinessContext ();

			var household = entity.Household;
			if (household.IsNotNull ())
			{
				businessContext.Register (household);
			}

			var person = entity.Person;
			if (person.IsNotNull ())
			{
				businessContext.Register (person);
			}

			var legalPerson = entity.LegalPerson;
			if (legalPerson.IsNotNull ())
			{
				businessContext.Register (legalPerson);
			}

			if (entity.ContactType == ContactType.PersonAddress)
			{
				//	We don't register the address in other cases, as it is owned by another entity
				//	like an household or a legal person, and it will therefore be registered through
				//	it.

				businessContext.Register (entity.Address);
			}
		}

		public override void ApplyUpdateRule(AiderContactEntity contact)
		{
			if (contact.Person.IsNotNull ())
			{
				//	TODO#PA

				//	TODO#SLO ElasticSearch Update
				/*var client = ElasticClient;
				var id = this.GetBusinessContext().DataContext.GetNormalizedEntityKey (contact).Value.ToString ();
				var druid = (Druid) Res.CommandIds.Base.ShowAiderContact;
				var name = contact.GetDisplayName ();
				var text = contact.Address.GetDisplayAddress ().ToSimpleText ();

				var document = new ElasticSearchDocument ()
				{
					DocumentId  = id,
					DatasetId = druid.ToCompactString (),
					EntityId  = id.Replace ('/', '-'),
					Name = name,
					Text = text
				};
				*/
				//client.Index (document, "aider", "contacts", document.DocumentId);
			}

			contact.RefreshCache ();
		}

		private static ElasticClient ElasticClient
		{
			get
			{
				try
				{
					var uriString = "http://localhost:9200";
					var uri = new System.Uri (uriString);
					var settings = new ConnectionSettings (uri);
					settings.SetDefaultIndex ("contacts");
					return new ElasticClient (settings);
				}
				catch (System.Exception)
				{
					throw;
				}
			}
		}
	}
}
