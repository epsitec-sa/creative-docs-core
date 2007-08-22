//	Copyright � 2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Types;

using System.Collections.Generic;

namespace Epsitec.Common.Support.ResourceAccessors
{
	using CultureInfo=System.Globalization.CultureInfo;
	
	/// <summary>
	/// The <c>StringResourceAccessor</c> is used to access text resources,
	/// stored in the <c>Strings</c> resource bundle.
	/// </summary>
	public class StringResourceAccessor : AbstractResourceAccessor
	{
		public StringResourceAccessor()
		{
		}

		public override void Load(ResourceManager manager)
		{
			this.Initialize (manager);

			if (this.ResourceManager.BasedOnPatchModule)
			{
				ResourceManager patchModuleManager = this.ResourceManager;
				ResourceManager refModuleManager   = this.ResourceManager.GetManagerForReferenceModule ();

				System.Diagnostics.Debug.Assert (refModuleManager != null);
				System.Diagnostics.Debug.Assert (refModuleManager.BasedOnPatchModule == false);

				this.LoadFromBundle (refModuleManager.GetBundle (Resources.StringsBundleName, ResourceLevel.Default), Resources.DefaultTwoLetterISOLanguageName);
				this.LoadFromBundle (patchModuleManager.GetBundle (Resources.StringsBundleName, ResourceLevel.Default), Resources.DefaultTwoLetterISOLanguageName);
			}
			else
			{
				this.LoadFromBundle (this.ResourceManager.GetBundle (Resources.StringsBundleName, ResourceLevel.Default), Resources.DefaultTwoLetterISOLanguageName);
			}
		}

		public override Types.StructuredData LoadCultureData(CultureMap item, string twoLetterISOLanguageName)
		{
			ResourceBundle bundle;

			if (twoLetterISOLanguageName == Resources.DefaultTwoLetterISOLanguageName)
			{
				bundle = this.ResourceManager.GetBundle (Resources.StringsBundleName, ResourceLevel.Default);
			}
			else
			{
				CultureInfo culture = Resources.FindCultureInfo (twoLetterISOLanguageName);
				bundle = this.ResourceManager.GetBundle (Resources.StringsBundleName, ResourceLevel.Localized, culture);
			}

			ResourceBundle.Field field   = bundle == null ? ResourceBundle.Field.Empty : bundle[item.Id];
			Types.StructuredData data    = null;

			if (field.IsEmpty)
			{
				data = new Types.StructuredData (Res.Types.ResourceString);
				item.RecordCultureData (twoLetterISOLanguageName, data);
			}
			else
			{
				data = this.LoadFromField (field, bundle.Module.Id, twoLetterISOLanguageName);
			}

			return data;
		}

		public override IDataBroker GetDataBroker(StructuredData container, string fieldId)
		{
			return base.GetDataBroker (container, fieldId);
		}

		protected override Druid CreateId()
		{
			ResourceBundle bundle = this.ResourceManager.GetBundle (Resources.StringsBundleName, ResourceLevel.Default);
			return AbstractResourceAccessor.CreateId (bundle, this.Collection);
		}

		protected override void DeleteItem(CultureMap item)
		{
			foreach (string twoLetterISOLanguageName in item.GetDefinedCultures ())
			{
				ResourceBundle bundle;
				CultureInfo culture;

				if (twoLetterISOLanguageName == Resources.DefaultTwoLetterISOLanguageName)
				{
					bundle = this.ResourceManager.GetBundle (Resources.StringsBundleName, ResourceLevel.Default);
				}
				else
				{
					culture = Resources.FindCultureInfo (twoLetterISOLanguageName);
					bundle  = this.ResourceManager.GetBundle (Resources.StringsBundleName, ResourceLevel.Localized, culture);
				}

				if (bundle != null)
				{
					int pos = bundle.IndexOf (item.Id);
					
					if (pos >= 0)
					{
						bundle.Remove (pos);
					}
				}
			}
			
		}
		
		protected override void PersistItem(CultureMap item)
		{
			if (string.IsNullOrEmpty (item.Name))
			{
				throw new System.ArgumentException (string.Format ("No name for item {0}", item.Id));
			}

			ResourceLevel level;
			ResourceBundle bundle;
			CultureInfo culture;
			ResourceBundle.Field field;
			StructuredData data;

			string text;
			string about;
			object modId;

			foreach (string twoLetterISOLanguageName in item.GetDefinedCultures ())
			{
				culture = Resources.FindCultureInfo (twoLetterISOLanguageName);
				level   = twoLetterISOLanguageName == Resources.DefaultTwoLetterISOLanguageName ? ResourceLevel.Default : ResourceLevel.Localized;
				bundle  = this.ResourceManager.GetBundle (Resources.StringsBundleName, level, culture);

				if (bundle == null)
				{
					bundle = ResourceBundle.Create (this.ResourceManager, this.ResourceManager.ActivePrefix, this.ResourceManager.GetModuleFromFullId (item.Id.ToString ()), Resources.StringsBundleName, ResourceLevel.Localized, culture, 0);
					bundle.DefineType ("String");
					this.ResourceManager.SetBundle (bundle, ResourceSetMode.InMemory);
				}
				
				field = bundle[item.Id];

				if (field.IsEmpty)
				{
					field = bundle.CreateField (ResourceFieldType.Data);
					field.SetDruid (item.Id);
					bundle.Add (field);
				}

				data = item.GetCultureData (twoLetterISOLanguageName);
				
				if (Types.UndefinedValue.IsUndefinedValue (data.GetValue (Res.Fields.ResourceString.Text)))
				{
					bundle.Remove (bundle.IndexOf (item.Id));
				}
				else
				{
					text  = data.GetValue (Res.Fields.ResourceString.Text) as string;
					about = data.GetValue (Res.Fields.ResourceBase.Comment) as string;
					modId = data.GetValue (Res.Fields.ResourceBase.ModificationId);

					if (twoLetterISOLanguageName == Resources.DefaultTwoLetterISOLanguageName)
					{
						field.SetName (item.Name);
					}
					field.SetStringValue (text);
					field.SetAbout (about);

					StringResourceAccessor.SetModificationId (field, modId);
				}
			}
		}

		internal static void SetModificationId(ResourceBundle.Field field, object modId)
		{
			if (!UndefinedValue.IsUndefinedValue (modId))
			{
				field.SetModificationId ((int) modId);
			}
			else
			{
				field.SetModificationId (-1);
			}
		}

		protected override Types.StructuredData LoadFromField(ResourceBundle.Field field, int module, string twoLetterISOLanguageName)
		{
			Druid id = new Druid (field.Id, module);
			bool insert;
			bool record;

			CultureMap item = this.Collection[id];
			CultureMapSource fieldSource = this.GetCultureMapSource (field);
			StructuredData data = new StructuredData (Res.Types.ResourceString);
			
			StringResourceAccessor.SetDataFromField (field, data);

			if (item == null)
			{
				//	Fresh item, not yet known :

				item = new CultureMap (this, id, fieldSource);
				
				insert = true;
				record = true;
			}
			else if (item.Source == fieldSource)
			{
				//	We already have an item for this id, but since we are fetching
				//	data from the same source as before, we can safely assume that
				//	this will produce new data for a not yet known culture :

				insert = false;
				record = true;
			}
			else
			{
				//	The source which was used to fill this item is different from
				//	the current source...

				if (item.IsCultureDefined (twoLetterISOLanguageName))
				{
					//	...and we know that there is already some data available
					//	for the culture. Merge the data :

					StructuredData newData = data;
					StructuredData oldData = item.GetCultureData (twoLetterISOLanguageName);

					if (field.AsString != null)
					{
						oldData.SetValue (Res.Fields.ResourceString.Text, newData.GetValue (Res.Fields.ResourceString.Text));
					}
					if (field.About != null)
					{
						oldData.SetValue (Res.Fields.ResourceBase.Comment, newData.GetValue (Res.Fields.ResourceBase.Comment));
					}
					if (field.ModificationId > 0)
					{
						oldData.SetValue (Res.Fields.ResourceBase.ModificationId, newData.GetValue (Res.Fields.ResourceBase.ModificationId));
					}
					

					data = oldData;

					insert = false;
					record = false;
				}
				else
				{
					//	...but we are filling in data for an unknown culture.
					//	Simply add the data to the item :

					insert = false;
					record = true;
				}

				//	Make sure we remember that the item contains merged data.
				
				item.Source = CultureMapSource.DynamicMerge;
			}

			item.Name = field.Name ?? item.Name;

			if (record)
			{
				item.RecordCultureData (twoLetterISOLanguageName, data);
			}
			if (insert)
			{
				this.Collection.Add (item);
			}

			return data;
		}

		private static void SetDataFromField(ResourceBundle.Field field, Types.StructuredData data)
		{
			data.SetValue (Res.Fields.ResourceString.Text, field.AsString);
			data.SetValue (Res.Fields.ResourceBase.Comment, field.About);
			data.SetValue (Res.Fields.ResourceBase.ModificationId, field.ModificationId);
		}

		protected override bool FilterField(ResourceBundle.Field field)
		{
			return true;
		}
	}
}
