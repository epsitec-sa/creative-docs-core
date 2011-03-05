﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types.Converters;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Library;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Widgets.Tiles;
using Epsitec.Cresus.DataLayer;
using Epsitec.Cresus.DataLayer.Context;

using Epsitec.Cresus.Core.Controllers.DataAccessors;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.EditionControllers
{
	public class EditionNaturalPersonViewController : EditionViewController<NaturalPersonEntity>
	{
		public EditionNaturalPersonViewController(string name, NaturalPersonEntity entity)
			: base (name, entity)
		{
		}

#if false
		protected override void CreateUI()
		{
			using (var builder = new UIBuilder (this))
			{
				builder.CreateHeaderEditorTile ();
				builder.CreateEditionTitleTile ("Data.NaturalPerson", "Personne physique");

				this.CreateUITitle (builder);
				this.CreateUIFirstnameAndLastname (builder);
				this.CreateUIGender (builder);
				this.CreateUIBirthDate (builder);
				this.CreateUIPictures (builder);

				builder.CreateFooterEditorTile ();
			}
		}

		
		private void CreateUITitle(UIBuilder builder)
		{
			var controller = new SelectionController<PersonTitleEntity> (this.BusinessContext)
			{
				ValueGetter         = () => this.Entity.Title,
				ValueSetter         = x => this.Entity.Title = x,
				ReferenceController = new ReferenceController (() => this.Entity.Title, creator: this.CreateNewTitle),
			};

			builder.CreateAutoCompleteTextField ("Titre", controller);
		}
		
		private void CreateUIFirstnameAndLastname(UIBuilder builder)
		{
			var tile = builder.CreateEditionTile ();

			builder.CreateTextField (tile, 0, "Prénom", Marshaler.Create (() => this.Entity.Firstname, x => this.Entity.Firstname = x));
			builder.CreateTextField (tile, 0, "Nom",    Marshaler.Create (() => this.Entity.Lastname,  x => this.Entity.Lastname = x));
			builder.CreateMargin    (tile, horizontalSeparator: true);
		}

		private void CreateUIGender(UIBuilder builder)
		{
			var controller = new SelectionController<PersonGenderEntity> (this.BusinessContext)
			{
				ValueGetter         = () => this.Entity.Gender,
				ValueSetter         = x => this.Entity.Gender = x,
				ReferenceController = new ReferenceController (() => this.Entity.Gender, mode: ViewControllerMode.None),
			};

			builder.CreateAutoCompleteTextField ("Sexe", controller);
		}

		private void CreateUIBirthDate(UIBuilder builder)
		{
			var tile = builder.CreateEditionTile ();
			builder.CreateTextField (tile, 90, "Date de naissance", Marshaler.Create (() => this.Entity.DateOfBirth, x => this.Entity.DateOfBirth = x));
		}

		private void CreateUIPictures(UIBuilder builder)
		{
			var controller = new SelectionController<ImageEntity> (this.BusinessContext)
			{
				CollectionValueGetter    = () => this.Entity.Pictures,
				ToFormattedTextConverter = x => TextFormatter.FormatText (x.Name).IfNullOrEmptyReplaceWith (CollectionTemplate.DefaultEmptyText),
			};

			builder.CreateEditionDetailedItemPicker ("Pictures", this.Entity, "Photographies du client", controller, EnumValueCardinality.Any, ViewControllerMode.Summary, 6);
		}


#else
		protected override void CreateUI()
		{
			var bridge = new Bridge<NaturalPersonEntity> (this);

			var wall = bridge.CreateBrickWall ();

			wall.AddBrick ()
				.Name ("NaturalPerson")
				.Icon ("Data.NaturalPerson")
				.Title (TextFormatter.FormatText ("Personne physique"))
				.TitleCompact (TextFormatter.FormatText ("Personne physique"))
				.Input ()
				 .Field (x => x.Title)
				.End ()
				.Input ()
				 .Field (x => x.Firstname)
				 .Field (x => x.Lastname)
				.End ()
				.Separator ()
				.Input ()
				 .Field (x => x.Gender)
				.End ()
				.Separator ()
				.Input ()
				 .Field (x => x.DateOfBirth).Width (90)
				.End ()
				.Input ()
				 .Field (x => x.Pictures)
				.End ();


			using (var data = TileContainerController.Setup (this))
			{
				foreach (var brick in wall.Bricks)
				{
					bridge.CreateTileDataItem (data, brick);
				}
			}
		}

		private void CreateUITitle(TileDataItems data)
		{
			var tileData = new TileDataItem
			{
				Name            = "NaturalPersonTitle",
				IconUri	        = "Data.NaturalPerson",
				Title	        = TextFormatter.FormatText ("Personne physique"),
				CompactTitle    = TextFormatter.FormatText ("Personne physique"),
				Frameless       = true,
				CreateEditionUI = (tile, builder) =>
				{
					var controller = new SelectionController<PersonTitleEntity> (this.BusinessContext)
					{
						ValueGetter         = () => this.Entity.Title,
						ValueSetter         = x => this.Entity.Title = x,
						ReferenceController = new ReferenceController (() => this.Entity.Title, creator: this.CreateNewTitle),
					};

					builder.CreateAutoCompleteTextField (tile, "Titre", controller);
				}
			};

			data.Add (tileData);
		}

		private void CreateUIFirstNameAndLastname(TileDataItems data)
		{
			var tileData = new TileDataItem
			{
				Name            = "NaturalPersonName",
				Frameless       = true,
				CreateEditionUI = (tile, builder) =>
				{
					builder.CreateTextField (tile, 0, "Prénom", Marshaler.Create (() => this.Entity.Firstname, x => this.Entity.Firstname = x));
					builder.CreateTextField (tile, 0, "Nom", Marshaler.Create (() => this.Entity.Lastname, x => this.Entity.Lastname = x));
					builder.CreateMargin (tile, horizontalSeparator: true);

				}
			};

			data.Add (tileData);
		}

		private void CreateUIGender(TileDataItems data)
		{
			var tileData = new TileDataItem
			{
				Name            = "NaturalPersonGender",
				Frameless       = true,
				CreateEditionUI = (tile, builder) =>
				{
					var controller = new SelectionController<PersonGenderEntity> (this.BusinessContext)
					{
						ValueGetter         = () => this.Entity.Gender,
						ValueSetter         = x => this.Entity.Gender = x,
						ReferenceController = new ReferenceController (() => this.Entity.Gender, mode: ViewControllerMode.None),
					};

					builder.CreateAutoCompleteTextField (tile, "Sexe", controller);
				}
			};

			data.Add (tileData);
		}

		private void CreateUIBirthDate(TileDataItems data)
		{
			var tileData = new TileDataItem
			{
				Name            = "NaturalPersonBirthDate",
				Frameless       = true,
				CreateEditionUI = (tile, builder) =>
				{
					builder.CreateTextField (tile, 90, "Date de naissance", Marshaler.Create (() => this.Entity.DateOfBirth, x => this.Entity.DateOfBirth = x));
				}
			};

			data.Add (tileData);
		}
		private void CreateUIPictures(TileDataItems data)
		{
			var tileData = new TileDataItem
			{
				Name            = "NaturalPersonPictures",
				Frameless       = true,
				CreateEditionUI = (tile, builder) =>
				{
					var controller = new SelectionController<ImageEntity> (this.BusinessContext)
					{
						CollectionValueGetter    = () => this.Entity.Pictures,
						ToFormattedTextConverter = x => TextFormatter.FormatText (x.Name).IfNullOrEmptyReplaceWith (CollectionTemplate.DefaultEmptyText),
					};

					builder.CreateEditionDetailedItemPicker (tile, "Pictures", this.Entity, "Photographies du client", controller, EnumValueCardinality.Any, ViewControllerMode.Summary, 6);
				}
			};

			data.Add (tileData);
		}
#endif
		private NewEntityReference CreateNewTitle(DataContext context)
		{
			var title = context.CreateEntityAndRegisterAsEmpty<PersonTitleEntity> ();
			return title;
		}
	}
}
