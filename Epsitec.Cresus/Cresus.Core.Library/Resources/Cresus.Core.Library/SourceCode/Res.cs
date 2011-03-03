﻿//  --------------------------------------------------------------------------- 
//  ATTENTION !
//  Ce fichier a été généré automatiquement. Ne pas l'éditer manuellement, car 
//  toute modification sera perdue. 
//  --------------------------------------------------------------------------- 

namespace Epsitec.Cresus.Core.Library
{
	public static class Res
	{
		//	Code mapping for 'Caption' resources
		
		public static class Commands
		{
			internal static void _Initialize()
			{
			}
			
			public static class Edition
			{
				internal static void _Initialize()
				{
				}
				
				//	designer:cap/EVA8
				public static readonly global::Epsitec.Common.Widgets.Command DiscardRecord = global::Epsitec.Common.Widgets.Command.Get (new global::Epsitec.Common.Support.Druid (_moduleId, 10, 8));
				//	designer:cap/EVA7
				public static readonly global::Epsitec.Common.Widgets.Command SaveRecord = global::Epsitec.Common.Widgets.Command.Get (new global::Epsitec.Common.Support.Druid (_moduleId, 10, 7));
			}
			
			public static class History
			{
				internal static void _Initialize()
				{
				}
				
				//	designer:cap/EVA5
				public static readonly global::Epsitec.Common.Widgets.Command NavigateBackward = global::Epsitec.Common.Widgets.Command.Get (new global::Epsitec.Common.Support.Druid (_moduleId, 10, 5));
				//	designer:cap/EVA6
				public static readonly global::Epsitec.Common.Widgets.Command NavigateForward = global::Epsitec.Common.Widgets.Command.Get (new global::Epsitec.Common.Support.Druid (_moduleId, 10, 6));
			}
		}
		
		public static class CommandIds
		{
			internal static void _Initialize()
			{
			}
			
			public static class Edition
			{
				internal static void _Initialize()
				{
				}
				
				//	designer:cap/EVA8
				public const long DiscardRecord = 0x3EE0000A000008L;
				//	designer:cap/EVA7
				public const long SaveRecord = 0x3EE0000A000007L;
			}
			
			public static class History
			{
				internal static void _Initialize()
				{
				}
				
				//	designer:cap/EVA5
				public const long NavigateBackward = 0x3EE0000A000005L;
				//	designer:cap/EVA6
				public const long NavigateForward = 0x3EE0000A000006L;
			}
			
		}
		
		public static class Types
		{
			internal static void _Initialize()
			{
			}
			
			//	designer:cap/EVA
			public static readonly Epsitec.Common.Types.EnumType EnumValueCardinality = (global::Epsitec.Common.Types.EnumType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 10, 0));
		}
		
		public static class Values
		{
			internal static void _Initialize()
			{
			}
			
			public static class EnumValueCardinality
			{
				internal static void _Initialize()
				{
				}
				
				//	designer:cap/EVA4
				public static global::Epsitec.Common.Types.Caption Any
				{
					get
					{
						return global::Epsitec.Cresus.Core.Library.Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 10, 4));
					}
				}
				//	designer:cap/EVA3
				public static global::Epsitec.Common.Types.Caption AtLeastOne
				{
					get
					{
						return global::Epsitec.Cresus.Core.Library.Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 10, 3));
					}
				}
				//	designer:cap/EVA2
				public static global::Epsitec.Common.Types.Caption ExactlyOne
				{
					get
					{
						return global::Epsitec.Cresus.Core.Library.Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 10, 2));
					}
				}
				//	designer:cap/EVA1
				public static global::Epsitec.Common.Types.Caption ZeroOrOne
				{
					get
					{
						return global::Epsitec.Cresus.Core.Library.Res._manager.GetCaption (new global::Epsitec.Common.Support.Druid (_moduleId, 10, 1));
					}
				}
			}
			
		}
		
		//	Code mapping for 'String' resources
		
		public static class Strings
		{
			
			public static global::Epsitec.Common.Types.FormattedText GetText(params string[] path)
			{
				string field = string.Join (".", path);
				return new global::Epsitec.Common.Types.FormattedText (_stringsBundle[field].AsString);
			}
			
			public static global::System.String GetString(params string[] path)
			{
				string field = string.Join (".", path);
				return _stringsBundle[field].AsString;
			}
			
			#region Internal Support Code
			
			private static global::Epsitec.Common.Types.FormattedText GetText(string bundle, params string[] path)
			{
				return new global::Epsitec.Common.Types.FormattedText (global::Epsitec.Cresus.Core.Library.Res.Strings.GetString (bundle, path));
			}
			
			private static global::Epsitec.Common.Types.FormattedText GetText(global::Epsitec.Common.Support.Druid druid)
			{
				return new global::Epsitec.Common.Types.FormattedText (global::Epsitec.Cresus.Core.Library.Res.Strings.GetString (druid));
			}
			
			private static global::System.String GetString(string bundle, params string[] path)
			{
				string field = string.Join (".", path);
				return _stringsBundle[field].AsString;
			}
			
			private static global::System.String GetString(global::Epsitec.Common.Support.Druid druid)
			{
				return _stringsBundle[druid].AsString;
			}
			
			private static readonly global::Epsitec.Common.Support.ResourceBundle _stringsBundle = Res._manager.GetBundle ("Strings");
			
			#endregion
		}
		
		static Res()
		{
			Res._manager = new global::Epsitec.Common.Support.ResourceManager (typeof (Res));
			Res._manager.DefineDefaultModuleName ("Cresus.Core.Library");
			Commands._Initialize ();
			Commands.Edition._Initialize ();
			Commands.History._Initialize ();
			CommandIds._Initialize ();
			CommandIds.Edition._Initialize ();
			CommandIds.History._Initialize ();
			Types._Initialize ();
			Values._Initialize ();
			Values.EnumValueCardinality._Initialize ();
		}
		
		public static void Initialize()
		{
		}
		
		public static global::Epsitec.Common.Support.ResourceManager Manager
		{
			get
			{
				return Res._manager;
			}
		}
		public static int ModuleId
		{
			get
			{
				return Res._moduleId;
			}
		}
		private static readonly global::Epsitec.Common.Support.ResourceManager _manager;
		private const int _moduleId = 1006;
	}
}
