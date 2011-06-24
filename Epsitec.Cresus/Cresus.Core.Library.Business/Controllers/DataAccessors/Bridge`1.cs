﻿//	Copyright © 2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Support.Extensions;

using Epsitec.Cresus.Bricks;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Widgets.Tiles;

using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.DataLayer.Context;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Core.Factories;

namespace Epsitec.Cresus.Core.Controllers.DataAccessors
{
	/// <summary>
	/// The <c>Bridge</c> class is used to transform <see cref="Brick"/> definitions into
	/// <see cref="Tile"/> instances, in collaboration with <see cref="EntityViewController{T}"/>.
	/// </summary>
	/// <typeparam name="T">The entity type on which the bridge operates.</typeparam>
	public class Bridge<T> : Bridge
		where T : AbstractEntity, new ()
	{
		public Bridge(EntityViewController<T> controller)
			: base (controller)
		{
			this.controller = controller;
			this.walls = new List<BrickWall<T>> ();
		}


		public override bool ContainsBricks
		{
			get
			{
				return this.walls.Any (x => x.Bricks.Any ());
			}
		}

		
		public BrickWall<T> CreateBrickWall()
		{
			var wall = new BrickWall<T> ();

			wall.BrickAdded += this.HandleBrickWallBrickAdded;
			wall.BrickPropertyAdded += this.HandleBrickWallBrickPropertyAdded;

			this.walls.Add (wall);

			return wall;
		}

		public override void CreateTileDataItems(TileDataItems data)
		{
			foreach (var brick in this.walls.SelectMany (x => x.Bricks))
			{
				this.CreateTileDataItem (data, brick);
			}
		}
		
		private TileDataItem CreateTileDataItem(TileDataItems data, Brick brick)
		{
			var item = new TileDataItem ();
			var root = brick;


		again:
			if (Brick.ContainsProperty (brick, BrickPropertyKey.AsType))
			{

			}
			else if (Brick.ContainsProperty (brick, BrickPropertyKey.Template))
			{
				//	Don't produce default text properties for bricks which contain AsType
				//	or Template bricks.

				if (!Brick.ContainsProperty (brick, BrickPropertyKey.Text))
				{
					Brick.AddProperty (brick, new BrickProperty (BrickPropertyKey.Text, CollectionTemplate.DefaultEmptyText));
				}
			}
			else
			{
				Bridge.CreateDefaultTextProperties (brick);
			}

			this.ProcessProperty (brick, BrickPropertyKey.Name, x => item.Name = x);
			this.ProcessProperty (brick, BrickPropertyKey.Icon, x => item.IconUri = x);
			
			this.ProcessProperty (brick, BrickPropertyKey.Title, x => item.Title = x);
			this.ProcessProperty (brick, BrickPropertyKey.TitleCompact, x => item.CompactTitle = x);
			this.ProcessProperty (brick, BrickPropertyKey.Text, x => item.Text = x);
			this.ProcessProperty (brick, BrickPropertyKey.TextCompact, x => item.CompactText = x);

			this.ProcessProperty (brick, BrickPropertyKey.Attribute, x => this.ProcessAttribute (item, x));
			this.ProcessProperty (brick, BrickPropertyKey.Include, x => this.ProcessInclusion (x));

			if ((!item.Title.IsNullOrEmpty) &&
				(item.CompactTitle.IsNull))
			{
				item.CompactTitle = item.Title;
			}

			this.ProcessProperty (brick, BrickPropertyKey.Title, x => item.TitleAccessor = x);
			this.ProcessProperty (brick, BrickPropertyKey.TitleCompact, x => item.CompactTitleAccessor = x);
			this.ProcessProperty (brick, BrickPropertyKey.Text, x => item.TextAccessor = x);
			this.ProcessProperty (brick, BrickPropertyKey.TextCompact, x => item.CompactTextAccessor = x);

			Brick asTypeBrick = Brick.GetProperty (brick, BrickPropertyKey.AsType).Brick;

			if (asTypeBrick != null)
			{
				brick = asTypeBrick;
				goto again;
			}

			Brick templateBrick = Brick.GetProperty (brick, BrickPropertyKey.Template).Brick;

			if (templateBrick != null)
			{
				data.Add (item);
				this.ProcessTemplate (data, item, root, templateBrick);
			}
			else if (Brick.ContainsProperty (brick, BrickPropertyKey.Input))
			{
				var processor = new InputProcessor (this.controller, data, item, brick);
				
				processor.ProcessInputs ();
			}
			else
			{
				item.EntityMarshaler = this.controller.CreateEntityMarshaler ();

				if (brick.GetFieldType () == typeof (T))
				{
					//	Type already ok.
				}
				else
				{
					item.SetEntityConverter<T> (brick.GetResolver (brick.GetFieldType ()));
				}
				data.Add (item);
			}

			return item;
		}


		private void ProcessAttribute(TileDataItem item, BrickMode value)
		{
			switch (value)
			{
				case BrickMode.AutoGroup:
					item.AutoGroup = true;
					break;
				
				case BrickMode.DefaultToSummarySubview:
					item.DefaultMode = ViewControllerMode.Summary;
					break;

				case BrickMode.HideAddButton:
					item.HideAddButton = true;
					break;

				case BrickMode.HideRemoveButton:
					item.HideRemoveButton = true;
					break;
			}
		}

		private void ProcessInclusion(Expression expression)
		{
			var lambda = expression as LambdaExpression;
			var func   = lambda.Compile ();
			var entity = func.DynamicInvoke (this.controller.Entity) as AbstractEntity;
			var name   = this.controller.Name + EntityInfo.GetFieldCaption (lambda).Name;

			//	Create the controller for the included subview, which will represent the entity
			//	pointed to by the expression :

			var sub = EntityViewControllerFactory.Create (name, entity, ViewControllerMode.Edition, this.controller.Orchestrator);

			this.controller.AddUIController (sub);
		}

		#region InputProcessor Class

		private class InputProcessor
		{
			public InputProcessor(EntityViewController<T> controller, TileDataItems data, TileDataItem item, Brick root)
			{
				this.controller = controller;
				this.business = this.controller.BusinessContext;
				this.data  = data;
				this.item  = item;
				this.root  = root;
				this.actions = new List<System.Action<FrameBox, UIBuilder>> ();
				this.inputProperties = Brick.GetProperties (this.root, BrickPropertyKey.Input);
			}
			
			public void ProcessInputs()
			{
				foreach (var property in this.inputProperties)
				{
					switch (property.Key)
					{
						case BrickPropertyKey.Input:
							this.CreateActionsForInput (property.Brick, this.inputProperties);
							break;
					}
				}

				this.RecordActions ();
			}

			private void CreateActionsForInput(Brick brick, BrickPropertyCollection inputProperties)
			{
				var fieldProperties = Brick.GetProperties (brick, BrickPropertyKey.Field, BrickPropertyKey.HorizontalGroup);

				foreach (var property in fieldProperties)
				{
					switch (property.Key)
					{
						case BrickPropertyKey.Field:
							this.CreateActionForInputField (property.ExpressionValue, fieldProperties);
							break;

						case BrickPropertyKey.HorizontalGroup:
							this.CreateActionsForHorizontalGroup (property);
							break;
					}
				}

				if (inputProperties != null)
				{
					if (inputProperties.PeekAfter (BrickPropertyKey.Separator, -1).HasValue)
					{
						this.CreateActionForSeparator ();
					}

					if (inputProperties.PeekBefore (BrickPropertyKey.GlobalWarning, -1).HasValue)
					{
						this.CreateActionForGlobalWarning ();
					}
				}
			}

			private void CreateActionsForHorizontalGroup(BrickProperty property)
			{
				int index = this.actions.Count ();

				var title = Brick.GetProperty (property.Brick, BrickPropertyKey.Title).StringValue;

				this.CreateActionsForInput (property.Brick, null);

				var actions = new List<System.Action<FrameBox, UIBuilder>> ();

				while (index < this.actions.Count)
				{
					actions.Add (this.actions[index]);
					this.actions.RemoveAt (index);
				}

				if (actions.Count == 0)
				{
					return;
				}

				System.Action<FrameBox, UIBuilder> groupAction =
					(tile, builder) =>
					{
						var group = builder.CreateGroup (tile as EditionTile, title);
						group.ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow;
						actions.ForEach (x => x (group, builder));
					};

				this.actions.Add (groupAction);
			}

			private void CreateActionForInputField(Expression expression, BrickPropertyCollection fieldProperties)
			{
				LambdaExpression lambda = expression as LambdaExpression;

				if (lambda == null)
				{
					throw new System.ArgumentException (string.Format ("Expression {0} for input must be a lambda", expression.ToString ()));
				}

				var fieldType  = lambda.ReturnType;
				var entityType = typeof (AbstractEntity);

				int    width  = InputProcessor.GetInputWidth (fieldProperties);
				int    height = InputProcessor.GetInputHeight (fieldProperties);
				string title  = InputProcessor.GetInputTitle (fieldProperties);
				
				System.Collections.IEnumerable collection = InputProcessor.GetInputCollection (fieldProperties);
				int? specialController = InputProcessor.GetSpecialController (fieldProperties);

				if (fieldType.IsEntity ())
				{
					//	The field is an entity : use an AutoCompleteTextField for it.

					var factory = DynamicFactories.EntityAutoCompleteTextFieldDynamicFactory.Create<T> (business, lambda, this.controller.EntityGetter, title, collection, specialController);
					this.actions.Add ((tile, builder) => factory.CreateUI (tile, builder));

					return;
				}

				if ((fieldType == typeof (string)) ||
					(fieldType == typeof (FormattedText)) ||
					(fieldType == typeof (System.DateTime)) ||
					(fieldType == typeof (System.DateTime?)) ||
					(fieldType == typeof (Date)) ||
					(fieldType == typeof (Date?)) ||
					(fieldType == typeof (long)) ||
					(fieldType == typeof (long?)) ||
					(fieldType == typeof (decimal)) ||
					(fieldType == typeof (decimal?)) ||
					(fieldType == typeof (int)) ||
					(fieldType == typeof (int?)) )
				{
					//	Produce either a text field or a variation of such a widget (pull-down list, etc.)
					//	based on the real type being edited.

					var factory = DynamicFactories.TextFieldDynamicFactory.Create<T> (business, lambda, this.controller.EntityGetter, title, width, height, collection);
					this.actions.Add ((tile, builder) => factory.CreateUI (tile, builder));

					return;
				}

				if (fieldType.IsGenericIListOfEntities ())
				{
					var factory = DynamicFactories.ItemPickerDynamicFactory.Create<T> (business, lambda, this.controller.EntityGetter, title);
					this.actions.Add ((tile, builder) => factory.CreateUI (tile, builder));

					return;
				}

				var underlyingType = fieldType.GetNullableTypeUnderlyingType ();

				if ((fieldType.IsEnum) ||
					((underlyingType != null) && (underlyingType.IsEnum)))
				{
					//	The field is an enumeration : use an AutoCompleteTextField for it.

					var factory = DynamicFactories.EnumAutoCompleteTextFieldDynamicFactory.Create<T> (business, lambda, this.controller.EntityGetter, title, width);
					this.actions.Add ((tile, builder) => factory.CreateUI (tile, builder));

					return;
				}

				System.Diagnostics.Debug.WriteLine (
					string.Format ("*** Field {0} of type {1} : no automatic binding implemented in Bridge<{2}>",
						lambda.ToString (), fieldType.FullName, typeof (T).Name));
			}

			private void CreateActionForSeparator()
			{
				this.actions.Add ((tile, builder) => builder.CreateMargin (tile as EditionTile, horizontalSeparator: true));
			}

			private void CreateActionForGlobalWarning()
			{
				this.actions.Add ((tile, builder) => builder.CreateWarning (tile as EditionTile));
			}

			private void RecordActions()
			{
				if ((this.actions != null) &&
					(this.actions.Count > 0))
				{
					if (this.item == null)
					{
						this.item = new TileDataItem ();
					}

					if (this.actions.Count == 1)
					{
						var singleAction = this.actions[0];
						this.item.CreateEditionUI = (tile, builder) => singleAction (tile, builder);
					}
					else
					{
						var multiActions = this.actions.ToArray ();
						
						this.item.CreateEditionUI = (tile, builder) =>
							{
								foreach (var action in multiActions)
								{
									var subTile = builder.CreateEditionTile (tile);
									action (subTile, builder);
								}
							};
					}
				}

				if (this.item != null)
				{
					this.data.Add (this.item);
					this.actions.Clear ();
					this.item = null;
				}
			}

			private static int GetInputWidth(BrickPropertyCollection properties)
			{
				var property = properties.PeekAfter (BrickPropertyKey.Width, -1);

				if (property.HasValue)
				{
					return property.Value.IntValue.GetValueOrDefault (0);
				}
				else
				{
					return 0;
				}
			}

			private static int GetInputHeight(BrickPropertyCollection properties)
			{
				var property = properties.PeekAfter (BrickPropertyKey.Height, -1);

				if (property.HasValue)
				{
					return property.Value.IntValue.GetValueOrDefault (0);
				}
				else
				{
					return 0;
				}
			}

			private static string GetInputTitle(BrickPropertyCollection properties)
			{
				var property = properties.PeekBefore (BrickPropertyKey.Title, -1);

				if (property.HasValue)
				{
					return property.Value.StringValue;
				}
				else
				{
					return null;
				}
			}

			private static int? GetSpecialController(BrickPropertyCollection properties)
			{
				var property = properties.PeekAfter (BrickPropertyKey.SpecialController, -1);

				if (property.HasValue)
				{
					return property.Value.IntValue;
				}
				else
				{
					return null;
				}
			}

			private static System.Collections.IEnumerable GetInputCollection(BrickPropertyCollection properties)
			{
				var property = properties.PeekAfter (BrickPropertyKey.FromCollection, -1);

				if (property.HasValue)
				{
					return property.Value.CollectionValue;
				}
				else
				{
					return null;
				}
			}

			private readonly EntityViewController<T> controller;
			private readonly BusinessContext business;
			private readonly TileDataItems data;
			private readonly Brick root;
			private readonly List<System.Action<FrameBox, UIBuilder>> actions;
			private readonly BrickPropertyCollection inputProperties;
			
			private TileDataItem item;
		}

		#endregion

		
		private void ProcessTemplate(TileDataItems data, TileDataItem item, Brick root, Brick templateBrick)
		{
			var templateName      = Brick.GetProperty (templateBrick, BrickPropertyKey.Name).StringValue ?? item.Name;
			var templateFieldType = templateBrick.GetFieldType ();

			CollectionTemplate collectionTemplate = this.DynamicCreateCollectionTemplate (templateName, templateFieldType);

			this.ProcessTemplateProperty (templateBrick, BrickPropertyKey.Title,        x => collectionTemplate.GenericDefine (CollectionTemplateProperty.Title, x));
			this.ProcessTemplateProperty (templateBrick, BrickPropertyKey.TitleCompact, x => collectionTemplate.GenericDefine (CollectionTemplateProperty.CompactTitle, x));
			this.ProcessTemplateProperty (templateBrick, BrickPropertyKey.Text,         x => collectionTemplate.GenericDefine (CollectionTemplateProperty.Text, x));
			this.ProcessTemplateProperty (templateBrick, BrickPropertyKey.TextCompact,  x => collectionTemplate.GenericDefine (CollectionTemplateProperty.CompactText, x));

			CollectionAccessor accessor = this.DynamicCreateCollectionAccessor (root, templateFieldType, collectionTemplate);
			
			data.Add (accessor);
		}

		private CollectionTemplate DynamicCreateCollectionTemplate(string name, System.Type templateFieldType)
		{
			var genericCollectionTemplateType     = typeof (CollectionTemplate<>);
			var genericCollectionTemplateTypeArg  = templateFieldType;
			var constructedCollectionTemplateType = genericCollectionTemplateType.MakeGenericType (genericCollectionTemplateTypeArg);

			object arg1 = name;
			object arg2 = this.controller.BusinessContext;

			return System.Activator.CreateInstance (constructedCollectionTemplateType, arg1, arg2) as CollectionTemplate;
		}

		private CollectionAccessor DynamicCreateCollectionAccessor(Brick root, System.Type templateFieldType, CollectionTemplate collectionTemplate)
		{
			var accessorFactoryType = typeof (DynamicAccessorFactory<,,>);
			var accessorFactoryTypeArg1 = typeof (T);
			var accessorFactoryTypeArg2 = root.GetFieldType ();
			var accessorFactoryTypeArg3 = templateFieldType;
			
			var genericAccessorFactoryType = accessorFactoryType.MakeGenericType (accessorFactoryTypeArg1, accessorFactoryTypeArg2, accessorFactoryTypeArg3);
			
			var accessorFactory = System.Activator.CreateInstance (genericAccessorFactoryType,
				/**/											   this.controller, root.GetResolver (templateFieldType), collectionTemplate) as DynamicAccessorFactory;

			return accessorFactory.CollectionAccessor;
		}

		private void ProcessProperty(Brick brick, BrickPropertyKey key, System.Action<BrickMode> setter)
		{
			foreach (var attributeValue in Brick.GetProperties (brick, key).Select (x => x.AttributeValue))
			{
				if ((attributeValue != null) &&
					(attributeValue.ContainsValue<BrickMode> ()))
				{
					setter (attributeValue.GetValue<BrickMode> ());
				}
			}
		}

		private void ProcessProperty(Brick brick, BrickPropertyKey key, System.Action<Accessor<FormattedText>> setter)
		{
			var formatter = this.ToAccessor (brick, Brick.GetProperty (brick, key));

			if (formatter != null)
			{
				setter (formatter);
			}
		}

		private Accessor<FormattedText> ToAccessor(Brick brick, BrickProperty property)
		{
			var getter    = this.controller.EntityGetter;
			var resolver  = brick.GetResolver (null);
			var formatterExpression = (property.ExpressionValue as LambdaExpression);

			if (formatterExpression == null)
			{
				return null;
			}

			var formatterFunc = formatterExpression.Compile ();
			
			if (resolver == null)
			{
				System.Func<FormattedText> composite =
					delegate
					{
						var expression = property.ExpressionValue as LambdaExpression;
						var source = getter ();
						var target = source as AbstractEntity;

						if (target.IsNull ())
						{
							return FormattedText.Empty;
						}
						else
						{
							return (FormattedText) formatterFunc.DynamicInvoke (target);
						}
					};

				return new Accessor<FormattedText> (composite);
			}
			else
			{
				System.Func<FormattedText> composite =
					delegate
					{
						var expression = property.ExpressionValue as LambdaExpression;
						var source = getter ();
						var target = resolver.DynamicInvoke (source) as AbstractEntity;

						if (target.IsNull ())
						{
							return FormattedText.Empty;
						}
						else
						{
							return (FormattedText) formatterFunc.DynamicInvoke (target);
						}
					};

				return new Accessor<FormattedText> (composite);
			}
		}


		private readonly EntityViewController<T> controller;
		private readonly List<BrickWall<T>> walls;
	}
}