﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types.Converters;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Widgets.Tiles;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.EditionControllers
{
	public class EditionDefaultAddressViewController : EditionViewController<Entities.AddressEntity>
	{
		public EditionDefaultAddressViewController(string name, Entities.AddressEntity entity)
			: base (name, entity)
		{
		}

		protected override void CreateUI()
		{
			using (var builder = new UIBuilder (this))
			{
				builder.CreateHeaderEditorTile ();
				builder.CreateEditionTitleTile ("Data.Mail", "Adresse par défaut du client");

				this.CreateUIMargin (builder);
				this.CreateUICountry (builder);
				this.CreateUIMain (builder);
				this.CreateUILocation (builder);

				builder.CreateFooterEditorTile ();
			}
		}


		private void CreateUIMargin(UIBuilder builder)
		{
			var tile = builder.CreateEditionTile ();

			builder.CreateMargin (tile, horizontalSeparator: true);
		}

		private void CreateUIMain(UIBuilder builder)
		{
			var tile = builder.CreateEditionTile ();

			builder.CreateMargin         (tile, horizontalSeparator: true);
			builder.CreateTextField      (tile,  0, "Rue",                     Marshaler.Create (() => this.Entity.Street.StreetName, x => this.Entity.Street.StreetName = x));
			builder.CreateTextFieldMulti (tile, 52, "Complément de l'adresse", Marshaler.Create (() => this.Entity.Street.Complement, x => this.Entity.Street.Complement = x));
			builder.CreateTextField      (tile,  0, "Boîte postale",           Marshaler.Create (() => this.Entity.PostBox.Number,    x => this.Entity.PostBox.Number = x));
			builder.CreateMargin         (tile, horizontalSeparator: true);
		}

		private void CreateUICountry(UIBuilder builder)
		{
			builder.CreateAutoCompleteTextField ("Nom et code du pays",
				new SelectionController<Entities.CountryEntity> (this.BusinessContext)
				{
					ValueGetter = () => this.Entity.Location.Country,
					ValueSetter = x => this.Entity.Location.Country = x,

					ToTextArrayConverter     = x => new string[] { x.Code, TextFormatter.FormatText (x.Name).ToSimpleText () },
					ToFormattedTextConverter = x => TextFormatter.FormatText (x.Name, "(", x.Code, ")"),
				});
		}

		private void CreateUILocation(UIBuilder builder)
		{
			builder.CreateAutoCompleteTextField ("Numéro postal et ville",
				new SelectionController<Entities.LocationEntity> (this.BusinessContext)
				{
					ValueGetter = () => this.Entity.Location,
					ValueSetter = x => this.Entity.Location = x,
					PossibleItemsGetter = () => CoreProgram.Application.Data.GetLocations (),

					ToTextArrayConverter     = x => new string[] { TextFormatter.FormatText (x.PostalCode).ToSimpleText (), TextFormatter.FormatText (x.Name).ToSimpleText () },
					ToFormattedTextConverter = x => TextFormatter.FormatText (x.PostalCode, x.Name),
				});
		}
	}
}
