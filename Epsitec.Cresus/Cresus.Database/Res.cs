﻿//	Automatically generated by ResGenerator, on 16.11.2007
//	Do not edit manually.

namespace Epsitec.Cresus.Database
{
	public static class Res
	{
		//	Code mapping for 'Caption' resources
		
		public static class Types
		{
			public static class Num
			{
				//	designer:cap/K01
				public static readonly global::Epsitec.Common.Types.IntegerType CollectionRank = (global::Epsitec.Common.Types.IntegerType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 1, 0));
				//	designer:cap/K004
				public static readonly global::Epsitec.Common.Types.LongIntegerType KeyId = (global::Epsitec.Common.Types.LongIntegerType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 4));
				//	designer:cap/K005
				public static readonly global::Epsitec.Common.Types.IntegerType KeyStatus = (global::Epsitec.Common.Types.IntegerType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 5));
				//	designer:cap/K007
				public static readonly global::Epsitec.Common.Types.LongIntegerType NullableKeyId = (global::Epsitec.Common.Types.LongIntegerType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 7));
				//	designer:cap/K006
				public static readonly global::Epsitec.Common.Types.IntegerType ReqExecState = (global::Epsitec.Common.Types.IntegerType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 6));
			}
			
			public static class Other
			{
				//	designer:cap/K00D
				public static readonly global::Epsitec.Common.Types.DateTimeType DateTime = (global::Epsitec.Common.Types.DateTimeType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 13));
				//	designer:cap/K00C
				public static readonly global::Epsitec.Common.Types.BinaryType ReqData = (global::Epsitec.Common.Types.BinaryType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 12));
			}
			
			public static class Str
			{
				//	designer:cap/K009
				public static readonly global::Epsitec.Common.Types.StringType InfoXml = (global::Epsitec.Common.Types.StringType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 9));
				//	designer:cap/K008
				public static readonly global::Epsitec.Common.Types.StringType Name = (global::Epsitec.Common.Types.StringType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 8));
				public static class Dict
				{
					//	designer:cap/K00A
					public static readonly global::Epsitec.Common.Types.StringType Key = (global::Epsitec.Common.Types.StringType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 10));
					//	designer:cap/K00B
					public static readonly global::Epsitec.Common.Types.StringType Value = (global::Epsitec.Common.Types.StringType) global::Epsitec.Common.Types.TypeRosetta.CreateTypeObject (new global::Epsitec.Common.Support.Druid (_moduleId, 0, 11));
				}
			}
		}
		
		//	Code mapping for 'String' resources
		
		public static class Strings
		{
			//	designer:str/K
			public static string New { get { return global::Epsitec.Cresus.Database.Res.Strings.GetText (global::Epsitec.Common.Support.Druid.FromFieldId (0)); } }
			
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
			Res.Initialize (typeof (Res), "Cresus.Database");
		}

		public static void Initialize()
		{
		}

		private static void Initialize(System.Type type, string name)
		{
			Res._manager = new global::Epsitec.Common.Support.ResourceManager (type);
			Res._manager.DefineDefaultModuleName (name);
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
		private const int _moduleId = 20;
	}
}
