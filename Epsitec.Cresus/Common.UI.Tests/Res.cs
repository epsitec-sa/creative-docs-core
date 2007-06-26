﻿//	Automatically generated by ResGenerator, on 26.06.2007
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
				
				public static readonly Epsitec.Common.Widgets.Command Persist = Epsitec.Common.Widgets.Command.Get (Epsitec.Common.Support.Druid.FromLong (_moduleId, 24));
				public static readonly Epsitec.Common.Widgets.Command Reload = Epsitec.Common.Widgets.Command.Get (Epsitec.Common.Support.Druid.FromLong (_moduleId, 23));
			}
			
			public static class Tools
			{
				internal static void _Initialize() { }
				
				public static readonly Epsitec.Common.Widgets.Command StartDesigner = Epsitec.Common.Widgets.Command.Get (Epsitec.Common.Support.Druid.FromLong (_moduleId, 1));
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
			public static Epsitec.Common.Types.Caption Address1 { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 5)); } }
			public static Epsitec.Common.Types.Caption Address2 { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 6)); } }
			public static Epsitec.Common.Types.Caption BillingAddress { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 18)); } }
			public static Epsitec.Common.Types.Caption City { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 7)); } }
			public static Epsitec.Common.Types.Caption Company { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 21)); } }
			public static Epsitec.Common.Types.Caption FirstName { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 0)); } }
			public static Epsitec.Common.Types.Caption InvoiceTitle { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 17)); } }
			public static Epsitec.Common.Types.Caption LastName { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 4)); } }
			public static Epsitec.Common.Types.Caption ShippingAddress { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 19)); } }
			public static Epsitec.Common.Types.Caption TotalAmount { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 20)); } }
			public static Epsitec.Common.Types.Caption ZipCode { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 8)); } }
			public static class Sample
			{
				public static Epsitec.Common.Types.Caption Fontenay6 { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 10)); } }
				public static Epsitec.Common.Types.Caption PierreArnaud { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 9)); } }
				public static Epsitec.Common.Types.Caption Yverdon { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 11)); } }
			}
			
			public static class Suggest
			{
				public static Epsitec.Common.Types.Caption ColumnHeader { get { return Res._manager.GetCaption (Epsitec.Common.Support.Druid.FromLong (_moduleId, 25)); } }
			}
			
		}
		
		public static class Types
		{
			public static class Num
			{
				public static readonly Epsitec.Common.Types.DecimalType MonetaryAmount = (Epsitec.Common.Types.DecimalType) Epsitec.Common.Types.TypeRosetta.CreateTypeObject (Epsitec.Common.Support.Druid.FromLong (_moduleId, 15));
				public static readonly Epsitec.Common.Types.IntegerType ZipCode = (Epsitec.Common.Types.IntegerType) Epsitec.Common.Types.TypeRosetta.CreateTypeObject (Epsitec.Common.Support.Druid.FromLong (_moduleId, 13));
			}
			
			public static class Record
			{
				public static readonly Epsitec.Common.Types.StructuredType Address = (Epsitec.Common.Types.StructuredType) Epsitec.Common.Types.TypeRosetta.CreateTypeObject (Epsitec.Common.Support.Druid.FromLong (_moduleId, 14));
				public static readonly Epsitec.Common.Types.StructuredType Invoice = (Epsitec.Common.Types.StructuredType) Epsitec.Common.Types.TypeRosetta.CreateTypeObject (Epsitec.Common.Support.Druid.FromLong (_moduleId, 16));
				public static readonly Epsitec.Common.Types.StructuredType Staff = (Epsitec.Common.Types.StructuredType) Epsitec.Common.Types.TypeRosetta.CreateTypeObject (Epsitec.Common.Support.Druid.FromLong (_moduleId, 22));
			}
			
			public static class Text
			{
				public static readonly Epsitec.Common.Types.StringType Name = (Epsitec.Common.Types.StringType) Epsitec.Common.Types.TypeRosetta.CreateTypeObject (Epsitec.Common.Support.Druid.FromLong (_moduleId, 12));
			}
		}
		
		//	Code mapping for 'String' resources
		
		public static class Strings
		{
			public static string ApplicationAuthor { get { return Epsitec.Common.UI.Res.Strings.GetText (Epsitec.Common.Support.Druid.FromFieldId (2)); } }
			public static string ApplicationCopyright { get { return Epsitec.Common.UI.Res.Strings.GetText (Epsitec.Common.Support.Druid.FromFieldId (1)); } }
			public static string ApplicationName { get { return Epsitec.Common.UI.Res.Strings.GetText (Epsitec.Common.Support.Druid.FromFieldId (0)); } }
			
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
			private static string GetText(Epsitec.Common.Support.Druid druid)
			{
				return _stringsBundle[druid].AsString;
			}
			private static Epsitec.Common.Support.ResourceBundle _stringsBundle = Res._manager.GetBundle ("Strings");
			#endregion
		}
		
		//	Code mapping for 'Panel' resources
		
		//	Code mapping for 'Panel' resources
		
		//	Code mapping for 'Panel' resources
		
		//	Code mapping for 'Panel' resources
		
		//	Code mapping for 'Panel' resources
		
		//	Code mapping for 'Panel' resources
		
		//	Code mapping for 'Panel' resources
		
		//	Code mapping for 'Panel' resources
		
		static Res()
		{
			Res.Initialize (typeof (Res), "Cresus.Tests");
		}

		public static void Initialize()
		{
		}

		private static void Initialize(System.Type type, string name)
		{
			Res._manager = new Epsitec.Common.Support.ResourceManager (type);
			Res._manager.DefineDefaultModuleName (name);
			Commands._Initialize ();
		}
		
		public static Epsitec.Common.Support.ResourceManager Manager
		{
			get { return Res._manager; }
		}
		
		public static int ModuleId
		{
			get { return _moduleId; }
		}
		
		private static Epsitec.Common.Support.ResourceManager _manager = Epsitec.Common.Support.Resources.DefaultManager;
		private const int _moduleId = 500;
	}
}
