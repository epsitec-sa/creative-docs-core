//	Copyright � 2003, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Statut : en chantier
//	DD 19/04/2004, modif pour jointures

using FirebirdSql.Data.Firebird;

namespace Epsitec.Cresus.Database.Implementation
{
	using Epsitec.Cresus.Database;
	
	/// <summary>
	/// Impl�mentation de ISqlBuilder pour Firebird.
	/// </summary>
	public class FirebirdSqlBuilder : ISqlBuilder, System.IDisposable
	{
		public FirebirdSqlBuilder(FirebirdAbstraction fb)
		{
			this.fb = fb;
			this.buffer = new System.Text.StringBuilder ();
			this.command_params = new System.Collections.ArrayList ();
		}
		
		
		protected void UpdateCommand()
		{
			if (this.expect_more)
			{
				throw new DbSyntaxException (this.fb.DbAccess, string.Format ("Command is defined partially: AppendMore called without additional commands."));
			}
			
			if (this.command_cache == null)
			{
				this.command_cache = this.fb.NewDbCommand () as FbCommand;
				
				int field_i = 0;
				
				//	Construit l'objet commande en se basant sur les param�tres d�finis pour
				//	celle-ci.
				
				foreach (SqlField field in this.command_params)
				{
					FbDbType    fb_type  = this.GetFbType (field.RawType);
					string      fb_name  = string.Format (TypeConverter.InvariantFormatProvider, "@PARAM_{0}", field_i++);
					FbParameter fb_param = new FbParameter (fb_name, fb_type);
					
					//	Pour l'instant, on ne supporte que des valeurs constantes dans la d�finition des
					//	param�tres de la commande (on aurait pu imaginer accepter ici les r�sultats d'une
					//	sous-requ�te ou encore d'une proc�dure SQL) :
					
					System.Diagnostics.Debug.Assert (field.Type == SqlFieldType.Constant);
					
					fb_param.Value = field.AsConstant;
					fb_param.SourceColumn = field.Alias;
					
					this.command_cache.Parameters.Add (fb_param);
				}
				
				//	Pour l'instant, la commande est toujours de type texte et construite par les
				//	diverses m�thodes publiques.
				
				this.command_cache.CommandType = System.Data.CommandType.Text;
				this.command_cache.CommandText = this.buffer.ToString ();
			}
		}
		
		
		protected string GetSqlType(SqlColumn column)
		{
			string basic_type = null;
			string length;
			
			//	Construit le nom du type SQL en fonction de la description de la
			//	colonne.
			
			switch (column.Type)
			{
				case DbRawType.Int16:			basic_type = "SMALLINT";		break;
				case DbRawType.Int32:			basic_type = "INTEGER";			break;
				case DbRawType.Int64:			basic_type = "BIGINT";			break;
				case DbRawType.Date:			basic_type = "DATE";			break;
				case DbRawType.Time:			basic_type = "TIME";			break;
				case DbRawType.DateTime:		basic_type = "TIMESTAMP";		break;
				case DbRawType.SmallDecimal:	basic_type = "DECIMAL(18,9)";	break;
				case DbRawType.LargeDecimal:	basic_type = "DECIMAL(18,3)";	break;
				
				case DbRawType.String:
					length     = column.Length.ToString (TypeConverter.InvariantFormatProvider);
					basic_type = (column.IsFixedLength ? "CHAR(" : "VARCHAR(") + length + ") CHARACTER SET UNICODE_FSS";
					break;
				
				case DbRawType.ByteArray:
					length     = column.Length.ToString (TypeConverter.InvariantFormatProvider);
					basic_type = "BLOB SUB_TYPE 0 SEGMENT SIZE " + length;
					break;
				
				//	Tous les types ne sont pas g�r�s ici, seuls ceux support�s en natif par
				//	Firebird sont list�s ici. Pour une base plus compl�te que Firebird, il
				//	faudra par exemple ajouter un support pour Guid.
				
				default:
					break;
			}
			
			if (basic_type == null)
			{
				throw new DbFormatException (string.Format ("Unsupported type {0} in column {1}.", column.Type.ToString (), column.Name));
			}
			
			return basic_type;
		}
		
		protected string AddFieldAsParam(SqlField field)
		{
			//	Ajoute un champ SQL comme param�tre de la commande. C'est la m�thode UpdateCommand qui
			//	va faire le lien entre le num du param�tre (@PARAM_n) et sa valeur, telle que d�finie
			//	dans le champ SqlField.
			
			string name = string.Format (TypeConverter.InvariantFormatProvider, "@PARAM_{0}", this.command_params.Count);
			this.command_params.Add (field);
			return name;
		}

		protected void Append(string str)
		{
			this.buffer.Append (str);
		}

		protected void Append(char c)
		{
			this.buffer.Append (c);
		}

		protected void Append(SqlField field)
		{
			//	Ajoute au buffer un champ, quel que soit son type

			switch(field.Type)
			{
				case SqlFieldType.Null:
					this.Append ("NULL");
					return;
				case SqlFieldType.All:
					this.Append ("*");
					return;
				case SqlFieldType.Constant:
				case SqlFieldType.ParameterOut:
				case SqlFieldType.ParameterInOut:
				case SqlFieldType.ParameterResult:
					this.Append (this.AddFieldAsParam (field));
					return;
				case SqlFieldType.Name:
					this.Append (field.AsName);
					return;
				case SqlFieldType.QualifiedName:
					this.Append (field.AsQualifiedName);
					return;
				case SqlFieldType.Aggregate:
					this.Append (field.AsAggregate);
					return;
				case SqlFieldType.Function:
					this.Append (field.AsFunction);
					return;
				case SqlFieldType.Procedure:
					this.Append (field.AsProcedure);
					return;
				case SqlFieldType.SubQuery:
					this.Append ('(');
					this.Append (field.AsSubQuery);
					this.Append (')');
					return;
				default:
					throw new System.NotImplementedException (string.Format ("Field {0} not implemented.", field.Type));
			}
		}
		
		protected void Append(SqlAggregate sql_aggregate)
		{
			switch (sql_aggregate.Type)
			{
				case SqlAggregateType.Count:
					this.Append ("COUNT(");
					break;
				case SqlAggregateType.Max:
					this.Append ("MAX(");
					break;
				case SqlAggregateType.Min:
					this.Append ("MIN(");
					break;
				case SqlAggregateType.Sum:
					this.Append ("SUM(");
					break;
				case SqlAggregateType.Average:
					this.Append ("AVG(");
					break;
				default:
					throw new System.NotImplementedException (string.Format ("Aggregate {0} not implemented.", sql_aggregate.Type));
			}
			this.Append (sql_aggregate.Field);
			this.Append (')');
		}

		protected void Append(SqlFunction sql_function)
		{
			System.Diagnostics.Debug.Assert (sql_function != null);

			//	Converti la fonction en cha�ne de caract�re SQL
			switch (sql_function.ArgumentCount)
			{
				case 2:
/*					if ( sql_function.Type == SqlFunctionType.JoinInner )
					{
						this.Append (sql_function.A.AsName);
						this.Append (" INNER JOIN ");
						this.Append (sql_function.B.AsName);
						this.Append (" ON ");
						this.Append (sql_function.A.AsQualifiedName);
						this.Append (" = ");
						this.Append (sql_function.B.AsQualifiedName);
						return;
					}*/

					this.Append (sql_function.A);
					switch (sql_function.Type)
					{
						case SqlFunctionType.MathAdd:
							this.Append (" + ");	break;
						case SqlFunctionType.MathSubstract:
							this.Append (" - "); break;
						case SqlFunctionType.MathMultiply:
							this.Append (" * "); break;
						case SqlFunctionType.MathDivide:
							this.Append (" / "); break;
						case SqlFunctionType.CompareEqual:
							this.Append (" = "); break;
						case SqlFunctionType.CompareNotEqual:
							this.Append (" <> "); break;
						case SqlFunctionType.CompareLessThan:
							this.Append (" < "); break;
						case SqlFunctionType.CompareLessThanOrEqual:
							this.Append (" <= "); break;
						case SqlFunctionType.CompareGreaterThan:
							this.Append (" > "); break;
						case SqlFunctionType.CompareGreaterThanOrEqual:
							this.Append (" >= "); break;
						case SqlFunctionType.CompareLike:
							this.Append (" LIKE "); break;
						case SqlFunctionType.CompareNotLike:
							this.Append (" NOT LIKE "); break;				
						case SqlFunctionType.SetIn:
							this.Append (" IN "); break;
						case SqlFunctionType.SetNotIn:
							this.Append (" NOT IN "); break;
						case SqlFunctionType.LogicAnd:
							this.Append (" AND "); break;
						case SqlFunctionType.LogicOr:
							this.Append (" OR "); break;
						default:
							System.Diagnostics.Debug.Assert (false); break;
					}
					this.Append (sql_function.B);
					return;
				
				case 1:
					switch (sql_function.Type)
					{
						case SqlFunctionType.LogicNot:
							this.Append ("NOT ");
							this.Append (sql_function.A);
							return;
						case SqlFunctionType.CompareIsNull:
							this.Append (sql_function.A);
							this.Append (" IS NULL");
							return;
						case SqlFunctionType.CompareIsNotNull:
							this.Append (sql_function.A);
							this.Append (" IS NOT NULL");
							return;
						case SqlFunctionType.SetExists:
							this.Append (sql_function.A);
							this.Append (" EXISTS");
							return;

						case SqlFunctionType.SetNotExists:
							this.Append (sql_function.A);
							this.Append (" NOT EXISTS");
							return;
						case SqlFunctionType.Upper:
							this.Append ("UPPER(");
							this.Append (sql_function.A);
							this.Append (")");
							return;
						default:
							System.Diagnostics.Debug.Assert (false);
							return;
					}

				case 3:
					if (sql_function.Type == SqlFunctionType.Substring)
					{
						this.Append ("SUBSTRING(");
						this.Append (sql_function.A);
						this.Append (" FROM ");
						this.Append (sql_function.B);
						this.Append (" FOR ");
						this.Append (sql_function.C);
						this.Append (")");
						return;
					}

					this.Append (sql_function.A);
					switch (sql_function.Type)
					{
						case SqlFunctionType.SetBetween:
							this.Append (" BETWEEN ");
							this.Append (sql_function.B);
							this.Append (" AND ");
							break;

						case SqlFunctionType.SetNotBetween:
							this.Append (" NOT BETWEEN ");
							this.Append (sql_function.B);
							this.Append (" AND ");
							break;
						default:
							System.Diagnostics.Debug.Assert (false);
							break;
					}
					this.Append (sql_function.C);
					return;

				default:
					System.Diagnostics.Debug.Assert (false);
					return;
			}			
		}
		
		protected void Append(SqlJoin sql_join)
		{
			System.Diagnostics.Debug.Assert (sql_join != null);

			//	Converti la fonction en cha�ne de caract�re SQL
//			switch (sql_join.ArgumentCount)
			{
//				case 2:
					if (sql_join.Type == SqlJoinType.Inner)
					{
						this.Append (sql_join.A.AsQualifier);
						this.Append (" INNER JOIN ");
						this.Append (sql_join.B.AsQualifier);
						this.Append (" ON ");
						this.Append (sql_join.A.AsQualifiedName);
						this.Append (" = ");
						this.Append (sql_join.B.AsQualifiedName);
						return;
					}

					System.Diagnostics.Debug.Assert (false);
/*					break;

				default:
					System.Diagnostics.Debug.Assert (false);
					return;*/
			}			
		}
		
		protected void Append(SqlSelect sql_query)
		{
			//	TODO-DD:  Compl�ter encore, notemment pour les JOINTURES
			
			this.Append ("SELECT ");
			bool first_field = true;

			if (sql_query.Predicate == SqlSelectPredicate.Distinct)
			{
				this.Append ("DISTINCT ");
			}
			
			foreach (SqlField field in sql_query.Fields)
			{
				if (first_field)
				{
					first_field = false;
				}
				else
				{
					this.Append (", ");
				}

				switch (field.Type)
				{
					case SqlFieldType.All:
						this.Append ("*");
						break;
					case SqlFieldType.Name:
						//	TODO?	s'il y a plusieures tables dans sql_query.Tables
						//			on devrait refuser les noms non qualifi�s
						this.Append (field.AsName);
						this.AppendAlias (field);
						break;
					case SqlFieldType.QualifiedName:
						this.Append (field.AsQualifiedName);
						this.AppendAlias (field);
						break;
					case SqlFieldType.Aggregate:
						this.Append (field.AsAggregate);
						this.AppendAlias (field);
						break;
					default:
						this.ThrowError (string.Format ("Unsupported field {0} in SELECT.", field.AsName));
						break;
				}
			}

			if (first_field)
			{
				// aucun champ sp�cifi�e !
				this.ThrowError (string.Format ("No field specified in SELECT."));
			}

			this.Append (" FROM ");
			first_field = true;

			if (sql_query.Joins.Count > 0)
			{
				//	cas particulier pour les jointures
				foreach (SqlField field in sql_query.Joins)
				{
					this.Append (field.AsJoin);
					first_field = false;
				}
			}
			else foreach (SqlField field in sql_query.Tables)
			{
				if (first_field)
				{
					first_field = false;
				}
				else
				{
					this.Append (", ");
				}

				switch ( field.Type )
				{
					case SqlFieldType.Name:
						this.Append (field.AsName);
						this.AppendAlias (field);
						this.Append (' ');
						break;
					default:
						this.ThrowError (string.Format ("Unsupported field {0} in SELECT FROM.", field.AsName));
						break;
				}
			}

			if (first_field)
			{
				// aucune table sp�cifi�e !
				this.ThrowError (string.Format ("No table specified in SELECT."));
			}

			first_field = true;

			foreach (SqlField field in sql_query.Conditions)
			{
				if (first_field)
				{
					this.Append ("WHERE ");
					first_field = false;
				}
				else
				{
					this.Append (" AND ");
				}

				if (field.Type != SqlFieldType.Function)
				{
					this.ThrowError (string.Format ("Invalid field {0} in SELECT ... WHERE.", field.AsName));
					break;
				}

				//	TODO?	s'il y a plusieures tables dans sql_query.Tables
				//			on devrait refuser les noms non qualifi�s
				//			n'importe o� dans la fonction donn�e
				this.Append (field.AsFunction);
			}

			first_field = true;

			foreach (SqlField field in sql_query.Fields)
			{
				if (field.Order == SqlFieldOrder.None) continue;
		
				if (first_field)
				{
					this.Append ("ORDER BY ");
				}

				//	TODO?	si un alias existe on devrait l'utiliser � la place du nom
				//	TODO?	sinon faut-il utiliser le nom qualifi� de pr�f�rence ?
				this.Append (field.AsName);
				this.Append (' ');

				if (field.Order == SqlFieldOrder.Inverse)
				{
					this.Append ("DESC");
				}

				if (first_field)
				{
					first_field = false;
				}				
				else
				{
					this.Append (", ");
				}
			}
		}

		protected void AppendAlias(SqlField field)
		{
			//	Si un alias existe, ajoute celui-ci dans le buffer.
			
			string alias = field.Alias;
			
			if ((alias != null) && (alias.Length > 0))
			{
				this.buffer.Append (' ');
				this.buffer.Append (alias);
			}
		}

		protected void AppendAliasOrName(SqlField field)
		{
			//	place le nom d'alias de pr�f�rence,
			//	sinon le nom qualifi�
			//	sinon le nom non qualifi�

			string alias = field.Alias;
			
			if ((alias != null) && (alias.Length > 0))
			{
				this.buffer.Append (alias);
			}
			else if (field.Type == SqlFieldType.QualifiedName)
			{
				this.buffer.Append (field.AsQualifiedName);
			}
			else
			{
				this.buffer.Append (field.AsName);
			}
		}
		
		protected string GetSqlColumnAttributes(SqlColumn column)
		{
			//	Construit les attributs de la colonne, tels qu'ils sont utilis�s dans la
			//	d�finition d'une table (se sont g�n�ralement des contraintes).
			
			System.Text.StringBuilder buffer = new System.Text.StringBuilder ();
			
			if (!column.IsNullAllowed)
			{
				buffer.Append (" NOT NULL");
			}
			
			if (column.IsUnique)
			{
				System.Diagnostics.Debug.WriteLine (string.Format ("Column {0} should be unique", column.Name));
			}
			
			return buffer.ToString ();
		}
		
		
		protected FbDbType GetFbType(DbRawType raw_type)
		{
			//	Convertit un type brut en un type Firebird correspondant.
			
			switch (raw_type)
			{
				case DbRawType.Int16:			return FbDbType.SmallInt;
				case DbRawType.Int32:			return FbDbType.Integer;
				case DbRawType.Int64:			return FbDbType.BigInt;
				case DbRawType.SmallDecimal:	return FbDbType.Decimal;
				case DbRawType.LargeDecimal:	return FbDbType.Decimal;
				case DbRawType.Time:			return FbDbType.Time;
				case DbRawType.Date:			return FbDbType.Date;
				case DbRawType.DateTime:		return FbDbType.TimeStamp;
				case DbRawType.String:			return FbDbType.VarChar;
				case DbRawType.ByteArray:		return FbDbType.LongVarBinary;
			}
			
			this.ThrowError (string.Format (TypeConverter.InvariantFormatProvider, "Type {0} cannot be mapped to Firebird Type.", raw_type.ToString ()));
			
			//	code jamais atteint:
			
			throw null;
		}
		
		protected void PrepareCommand()
		{
			if (this.expect_more)
			{
				this.expect_more = false;
			}
			else
			{
				if (this.auto_clear)
				{
					this.Clear ();
				}
				
				if (this.command_type != DbCommandType.None)
				{
					throw new DbException (this.fb.DbAccess, "Previous command not cleared");
				}
			}
		}
		
		
		#region ISqlBuilder Members
		public bool								AutoClear
		{
			get { return this.auto_clear; }
			set { this.auto_clear = value; }
		}
		
		public DbCommandType					CommandType
		{
			get { return this.command_type; }
		}
		
		public int								CommandCount
		{
			get { return this.command_count; }
		}

		public System.Data.IDbCommand			Command
		{
			get
			{
				this.UpdateCommand ();
				return this.command_cache;
			}
		}		
		
		public System.Data.IDbCommand CreateCommand(System.Data.IDbTransaction transaction)
		{
			this.UpdateCommand ();
			this.command_cache.Transaction = transaction as FirebirdSql.Data.Firebird.FbTransaction;
			return this.command_cache;
		}
		
		public System.Data.IDbCommand CreateCommand(System.Data.IDbTransaction transaction, string text)
		{
			FbCommand command = this.fb.NewDbCommand () as FbCommand;
			command.CommandText = text;
			command.CommandType = System.Data.CommandType.Text;
			command.Transaction = transaction as FirebirdSql.Data.Firebird.FbTransaction;
			return command;
		}
				
		public void Clear()
		{
			//	On n'a pas le droit de faire un Dispose de l'objet 'commande', car il peut encore
			//	�tre utilis� par un appelant. C'est le cas lorsque l'on est en mode AutoClear.
			
			this.expect_more   = false;
			this.command_cache = null;
			this.command_type  = DbCommandType.None;
			this.command_count = 0;
			this.buffer = new System.Text.StringBuilder ();
			this.command_params.Clear ();
		}
		
		public void AppendMore()
		{
			this.expect_more = true;
		}

		
		public bool ValidateName(string value)
		{
			return DbSqlStandard.ValidateName (value);
		}
		
		public bool ValidateQualifiedName(string value)
		{
			return DbSqlStandard.ValidateQualifiedName (value);
		}

		public bool ValidateString(string value)
		{
			return DbSqlStandard.ValidateString (value);
		}

		public bool ValidateNumber(string value)
		{
			return DbSqlStandard.ValidateNumber (value);
		}

		
		public void ThrowError(string message)
		{
			throw new DbSyntaxException (this.fb.DbAccess, message);
		}
		
		
		public void InsertTable(SqlTable table)
		{
			if (!table.Validate (this))
			{
				this.ThrowError (string.Format ("Invalid table {0}.", table.Name));
			}
			
			this.PrepareCommand ();
			this.command_type = DbCommandType.Silent;
			this.command_count++;
			
			this.Append ("CREATE TABLE ");
			this.Append (table.Name);
			this.Append ("(");
			
			bool first_field = true;
			
			foreach (SqlColumn column in table.Columns)
			{
				if (!column.Validate (this))
				{
					this.ThrowError (string.Format ("Invalid column {0} in table {1}.", column.Name, table.Name));
				}
				
				if (first_field)
				{
					first_field = false;
				}
				else
				{
					this.Append (", ");
				}
				
				this.Append (column.Name);
				this.Append (' ');
				this.Append (this.GetSqlType (column));
				this.Append (this.GetSqlColumnAttributes (column));
			}
			
			this.Append (");\n");
			
			if (table.HasPrimaryKeys)
			{
				this.command_count++;
				
				this.Append ("ALTER TABLE ");
				this.Append (table.Name);
				this.Append (" ADD CONSTRAINT ");
				this.Append (DbSqlStandard.ConcatNames ("PK_", table.Name));
				this.Append (" PRIMARY KEY ");
				this.Append ("(");
				
				first_field = true;
				
				foreach (SqlColumn column in table.PrimaryKey)
				{
					if (!table.Columns.Contains (column))
					{
						this.ThrowError (string.Format ("Column {0} specified as primary key, not found in table {1}.", column.Name, table.Name));
					}
					
					if (first_field)
					{
						first_field = false;
					}
					else
					{
						this.Append (", ");
					}
				
					this.Append (column.Name);
				}
				
				this.Append (");\n");
			}
		}

		public void RemoveTable(string table_name)
		{
			if (!this.ValidateName (table_name))
			{
				this.ThrowError (string.Format ("Invalid table {0}.", table_name));
			}
			
			this.PrepareCommand ();
			this.command_type = DbCommandType.Silent;
			this.command_count++;
			
			this.Append ("DROP TABLE ");
			this.Append (table_name);
			this.Append (";\n");
		}
		
		public void InsertTableColumns(string table_name, SqlColumn[] columns)
		{
			if (!this.ValidateName (table_name))
			{
				this.ThrowError (string.Format ("Invalid table {0}.", table_name));
			}
			
			this.PrepareCommand ();
			this.command_type = DbCommandType.Silent;
			
			foreach (SqlColumn column in columns)
			{
				if (!column.Validate (this))
				{
					this.ThrowError (string.Format ("Invalid column {0} in table {1}.", column.Name, table_name));
				}
				
				this.command_count++;
				
				this.Append ("ALTER TABLE ");
				this.Append (table_name);
				this.Append (" ADD ");			//	not " ADD COLUMN "
				this.Append (column.Name);
				this.Append (" ");
				this.Append (this.GetSqlType (column));
				this.Append (this.GetSqlColumnAttributes (column));
				this.Append (";\n");
			}
		}

		public void UpdateTableColumns(string table_name, SqlColumn[] columns)
		{
			this.PrepareCommand ();
			this.command_type = DbCommandType.Silent;
			
			this.ThrowError (string.Format ("Cannot update table {0}. Not supported.", table_name));
		}

		public void RemoveTableColumns(string table_name, SqlColumn[] columns)
		{
			if (!this.ValidateName (table_name))
			{
				this.ThrowError (string.Format ("Invalid table {0}.", table_name));
			}
			
			this.PrepareCommand ();
			this.command_type = DbCommandType.Silent;
			
			foreach (SqlColumn column in columns)
			{
				if (!column.Validate (this))
				{
					this.ThrowError (string.Format ("Invalid column {0} in table {1}.", column.Name, table_name));
				}
				
				this.command_count++;
				
				this.Append ("ALTER TABLE ");
				this.Append (table_name);
				this.Append (" DROP ");		// not " DROP COLUMN "
				this.Append (column.Name);
				this.Append (";\n");
			}
		}
		
		
		public void SelectData(SqlSelect sql_query)
		{
			this.PrepareCommand ();
			
			this.command_type = DbCommandType.ReturningData;
			this.command_count++;
			
			this.Append (sql_query);			
			this.Append (";\n");
		}

		public void InsertData(string table_name, SqlFieldCollection fields)
		{
			if (!this.ValidateName (table_name))
			{
				this.ThrowError (string.Format ("Invalid table {0}.", table_name));
			}
			
			if (fields.Count == 0)
			{
				return;
			}
			
			this.PrepareCommand ();
			this.command_type = DbCommandType.NonQuery;
			this.command_count++;
			
			this.Append ("INSERT INTO ");
			this.Append (table_name);
			this.Append ("(");
			
			bool first_field = true;
			
			foreach (SqlField field in fields)
			{
				if (!this.ValidateName (field.Alias))
				{
					this.ThrowError (string.Format ("Invalid field '{0}' in table {1}.", field.Alias, table_name));
				}
				
				if (first_field)
				{
					first_field = false;
				}
				else
				{
					this.Append (",");
				}
				
				this.Append (field.Alias);
			}
			
			this.Append (") VALUES (");
			
			first_field = true;
			
			foreach (SqlField field in fields)
			{
				string data = null;
				
				switch (field.Type)
				{
					case SqlFieldType.Default:		data = "DEFAULT";						break;
					case SqlFieldType.Null:			data = "NULL";							break;
					case SqlFieldType.Constant:		data = this.AddFieldAsParam (field);	break;
					
					default:
						this.ThrowError (string.Format ("Invalid field type {0} for field {1} in table {2}.", field.Type.ToString (), field.Alias, table_name));
						break;
				}
				
				if (first_field)
				{
					first_field = false;
				}
				else
				{
					this.Append (",");
				}
				
				this.Append (data);
			}
			
			this.Append (");\n");
		}

		public void UpdateData(string table_name, SqlFieldCollection fields, SqlFieldCollection conditions)
		{
			this.PrepareCommand ();
			this.command_type = DbCommandType.NonQuery;
			this.command_count++;
			
			this.Append ("UPDATE ");
			this.Append (table_name);
			bool first_field = true;

			foreach (SqlField field in fields)
			{
				if (first_field)
				{
					this.Append (" SET ");
					first_field = false;
				}
				else
				{
					this.Append (",");
				}

				this.Append (field.Alias);
				this.Append (" = ");
#if false
				switch (field.Type)
				{
					case SqlFieldType.Constant:
						this.Append (field);
						break;
					case SqlFieldType.Function:
						this.Append (field.AsFunction);
						break;
						//	TODO-DD	y a-t-il d'autres cas possible ?
						//	ou plut�t quels sont les cas interdit ?
						//	this.Append (field) serait ok pour tous
					default:
						this.ThrowError (string.Format ("Unsupported field {0} in UPDATE.", field.AsName));
						break;
				}
#else
				this.Append (field);
#endif
			}

			if (first_field)
			{
				this.ThrowError (string.Format ("No field specified in UPDATE."));
			}

			first_field = true;

			foreach (SqlField field in conditions)
			{
				if (first_field)
				{
					this.Append (" WHERE ");
					first_field = false;
				}
				else
				{
					this.Append (" AND ");
				}

				if (field.Type != SqlFieldType.Function)
				{
					this.ThrowError (string.Format ("Invalid field {0} in UPDATE ... WHERE.", field.AsName));
					break;
				}

				this.Append (field.AsFunction);
			}
			this.Append (";\n");
		}

		public void RemoveData(string table_name, SqlFieldCollection conditions)
		{
			this.PrepareCommand ();
			this.command_type = DbCommandType.NonQuery;
			this.command_count++;
			
			this.Append ("DELETE FROM ");
			this.Append (table_name);
			bool first_field = true;

			foreach (SqlField field in conditions)
			{
				if (first_field)
				{
					this.Append (" WHERE ");
					first_field = false;
				}
				else
				{
					this.Append (" AND ");
				}

				if (field.Type != SqlFieldType.Function)
				{
					this.ThrowError (string.Format ("Invalid field {0} in UPDATE ... WHERE.", field.AsName));
					break;
				}

				this.Append (field.AsFunction);
			}
			this.Append (";\n");
		}

		
		public void ExecuteProcedure(string procedure_name, SqlFieldCollection fields)
		{
			this.PrepareCommand ();
//?			this.command_type = DbCommandType.NonQuery;
//			this.command_count++;
			
			// TODO:  Add FirebirdSqlBuilder.ExecuteProcedure implementation
		}

		
		public void GetSqlParameters(System.Data.IDbCommand command, SqlFieldCollection fields)
		{
			// TODO:  Add FirebirdSqlBuilder.GetSqlParameters implementation
#if false
			FbCommand fb_command = command as FbCommand;
			
			for (int i = 0; i < fb_command.Parameters.Count; i++)
			{
				FbParameter param = fb_command.Parameters[i] as FbParameter;
				
				System.Diagnostics.Debug.Assert (param != null);
				System.Diagnostics.Debug.WriteLine (param.ParameterName + ": " + param.FbDbType.ToString ());
				
				switch (param.Direction)
				{
					case System.Data.ParameterDirection.Input:
						break;
					
					case System.Data.ParameterDirection.InputOutput:
					case System.Data.ParameterDirection.Output:
						break;
					
					case System.Data.ParameterDirection.ReturnValue:
						break;
				}
			}
#endif
		}

		public void SetSqlParameters(System.Data.IDbCommand command, SqlFieldCollection fields)
		{
			// TODO:  Add FirebirdSqlBuilder.SetSqlParameters implementation
		}

		#endregion
		
		#region IDisposable Members
		public void Dispose()
		{
			this.Dispose (true);
			System.GC.SuppressFinalize (this);
		}
		#endregion
		
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.command_cache != null)
				{
					this.command_cache.Dispose ();
					this.command_cache = null;
				}
			}
		}
		
		
		private FirebirdAbstraction				fb;
		private bool							auto_clear;
		private bool							expect_more;
		private FbCommand						command_cache;
		private System.Text.StringBuilder		buffer;
		private System.Collections.ArrayList	command_params;
		private DbCommandType					command_type;
		private int								command_count;
	}
}
