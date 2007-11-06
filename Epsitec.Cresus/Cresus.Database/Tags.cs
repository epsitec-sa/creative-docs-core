//	Copyright � 2004-2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

namespace Epsitec.Cresus.Database
{
	/// <summary>
	/// The <c>Tags</c> class defines all well known tags (texts) used to name
	/// columns, tables, types, etc.
	/// </summary>
	public static class Tags
	{
		//	Basic CRESUS type names :

		public const string TypeKeyId			= "K004"; //"Num.KeyId";
		public const string TypeNullableKeyId	= "K007"; //"Num.NullableKeyId";
		public const string TypeKeyStatus		= "K005"; //"Num.KeyStatus";
		public const string TypeReqExState		= "K006"; //"Num.ReqExecState";
		public const string TypeName			= "K008"; //"Str.Name";
		public const string	TypeInfoXml			= "K009"; //"Str.InfoXml";
		public const string TypeDictKey			= "K00A"; //"Str.Dict.Key";
		public const string TypeDictValue		= "K00B"; //"Str.Dict.Value";
		public const string TypeReqData			= "K00C"; //"Other.ReqData";
		public const string TypeDateTime		= "K00D"; //"Other.DateTime";
		
		//	Basic CRESUS column names :
		
		public const string	ColumnId			= "CR_ID";
		public const string	ColumnStatus		= "CR_STAT";
		public const string ColumnName			= "CR_NAME";
		public const string ColumnDisplayName	= "CR_DISPLAY_NAME";
		public const string ColumnInfoXml		= "CR_INFO";
		public const string	ColumnNextId		= "CR_NEXT_ID";
		public const string ColumnDateTime		= "CR_DATETIME";
		public const string ColumnInstanceType	= "CR_TYPE";
		
		public const string ColumnDictKey		= "CR_DICT_KEY";
		public const string ColumnDictValue		= "CR_DICT_VALUE";
		
		public const string ColumnReqData		= "CR_RQ_DATA";
		public const string ColumnReqExState	= "CR_RQ_EX_STATE";
		
		public const string	ColumnRefTable		= "CREF_TABLE";
		public const string	ColumnRefType		= "CREF_TYPE";
		public const string	ColumnRefColumn		= "CREF_COLUMN";
		public const string	ColumnRefTarget		= "CREF_TARGET_TABLE";
		public const string ColumnRefLog		= "CREF_LOG";
		public const string ColumnRefId			= "CREF_ID";
		public const string ColumnRefModel		= "CREF_MODEL";
		
		public const string ColumnClientName	= "CR_CLIENT_NAME";
		public const string ColumnClientId		= "CR_CLIENT_ID";
		public const string ColumnClientSync	= "CR_CLIENT_SYNC";
		public const string ColumnClientCreDate	= "CR_CLIENT_CRE_DATE";
		public const string ColumnClientConDate	= "CR_CLIENT_CON_DATE";
		
		//	Basic CRESUS table names :
		
		public const string	TableTableDef		= "CR_TABLE_DEF";
		public const string	TableColumnDef		= "CR_COLUMN_DEF";
		public const string	TableTypeDef		= "CR_TYPE_DEF";
		public const string TableLog			= "CR_LOG";
		public const string TableRequestQueue	= "CR_RQ_QUEUE";
		public const string TableClientDef      = "CR_CLIENT_DEF";
	}
}
