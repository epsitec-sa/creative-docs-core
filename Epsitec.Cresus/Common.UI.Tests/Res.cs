﻿//	Automatically generated by ResGenerator, on 31.10.2007
//	Do not edit manually.

namespace Epsitec.Common.UI
{
	public static class Res
	{
		//	Code mapping for 'Caption' resources
		
		public static class Commands
		{
			public static class Data
			{
				internal static void _Initialize() { }
				
				public static readonly global::Epsitec.Common.Widgets.Command Persist = global::Epsitec.Common.Widgets.Command.Get (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 24));
				public static readonly global::Epsitec.Common.Widgets.Command Reload = global::Epsitec.Common.Widgets.Command.Get (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 23));
			}
			
			public static class Tools
			{
				internal static void _Initialize() { }
				
				public static readonly global::Epsitec.Common.Widgets.Command StartDesigner = global::Epsitec.Common.Widgets.Command.Get (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 1));
			}
			
			internal static void _Initialize()
			{
				Data._Initialize ();
				Tools._Initialize ();
			}
		}
		
		public static class CommandIds
		{
			public static class Data
			{
				
				public const long Persist = 0x1F400000000018L;
				public const long Reload = 0x1F400000000017L;
			}
			
			public static class Tools
			{
				
				public const long StartDesigner = 0x1F400000000001L;
			}
		}
		
		public static class Captions
		{
			public static global::Epsitec.Common.Types.Caption Address1 { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 5)); } }
			public static global::Epsitec.Common.Types.Caption Address2 { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 6)); } }
			public static global::Epsitec.Common.Types.Caption BillingAddress { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 18)); } }
			public static global::Epsitec.Common.Types.Caption City { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 7)); } }
			public static global::Epsitec.Common.Types.Caption Company { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 21)); } }
			public static global::Epsitec.Common.Types.Caption FirstName { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 0)); } }
			public static global::Epsitec.Common.Types.Caption InvoiceTitle { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 17)); } }
			public static global::Epsitec.Common.Types.Caption LastName { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 4)); } }
			public static global::Epsitec.Common.Types.Caption ShippingAddress { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 19)); } }
			public static global::Epsitec.Common.Types.Caption TotalAmount { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 20)); } }
			public static global::Epsitec.Common.Types.Caption ZipCode { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 8)); } }
			public static class Sample
			{
				public static global::Epsitec.Common.Types.Caption Fontenay6 { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 10)); } }
				public static global::Epsitec.Common.Types.Caption PierreArnaud { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 9)); } }
				public static global::Epsitec.Common.Types.Caption Yverdon { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 11)); } }
			}
			
			public static class Suggest
			{
				public static global::Epsitec.Common.Types.Caption ColumnHeader { get { return Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 25)); } }
			}
			
		}
		
		public static class Types
		{
			public static class Num
			{
				public static readonly global::Epsitec.Common.Types.DecimalType MonetaryAmount = (global::Epsitec.Common.Types.DecimalType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 15));
				public static readonly global::Epsitec.Common.Types.IntegerType ZipCode = (global::Epsitec.Common.Types.IntegerType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 13));
			}
			
			public static class Record
			{
				public static readonly global::Epsitec.Common.Types.StructuredType Address = (global::Epsitec.Common.Types.StructuredType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 14));
				public static readonly global::Epsitec.Common.Types.StructuredType Invoice = (global::Epsitec.Common.Types.StructuredType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 16));
				public static readonly global::Epsitec.Common.Types.StructuredType Staff = (global::Epsitec.Common.Types.StructuredType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 22));
			}
			
			public static class Text
			{
				public static readonly global::Epsitec.Common.Types.StringType Name = (global::Epsitec.Common.Types.StringType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 12));
			}
		}
		
		//	Code mapping for 'String' resources
		
		public static class Strings
		{
			public static string ApplicationAuthor { get { return global::Epsitec.Common.UI.Res.Strings.GetText (global::Epsitec.Common.Support.Druid.FromFieldId (2)); } }
			public static string ApplicationCopyright { get { return global::Epsitec.Common.UI.Res.Strings.GetText (global::Epsitec.Common.Support.Druid.FromFieldId (1)); } }
			public static string ApplicationName { get { return global::Epsitec.Common.UI.Res.Strings.GetText (global::Epsitec.Common.Support.Druid.FromFieldId (0)); } }
			
			public static string GetString(params string[] path)
			{
				string field = string.Join (".", path);
				return _stringsBundle[field].AsString;
			}
			
			#region Internal Support Code
			private static string GetText(string bundle, params string[] path)
			{
				string field = string.Join (".", path);
				return _stringsBundle[field].AsString;
			}
			private static string GetText(global::Epsitec.Common.Support.Druid druid)
			{
				return _stringsBundle[druid].AsString;
			}
			private static global::Epsitec.Common.Support.ResourceBundle _stringsBundle = Res._manager.GetBundle ("Strings");
			#endregion
		}
		
		static Res()
		{
			Res.Initialize (typeof (Res), "Cresus.Tests");
		}

		public static void Initialize()
		{
		}

		private static void Initialize(System.Type type, string name)
		{
			Res._manager = new global::Epsitec.Common.Support.ResourceManager (type);
			Res._manager.DefineDefaultModuleName (name);
			Commands._Initialize ();
		}
		
		public static global::Epsitec.Common.Support.ResourceManager Manager
		{
			get { return Res._manager; }
		}
		
		public static int ModuleId
		{
			get { return _moduleId; }
		}
		
		private static global::Epsitec.Common.Support.ResourceManager _manager = global::Epsitec.Common.Support.Resources.DefaultManager;
		private const int _moduleId = 500;
	}
}
