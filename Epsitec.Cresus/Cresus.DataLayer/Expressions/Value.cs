﻿using Epsitec.Cresus.Database;

using Epsitec.Cresus.DataLayer.Loader;


namespace Epsitec.Cresus.DataLayer.Expressions
{


	public abstract class Value
	{


		/// <summary>
		/// Creates an <see cref="SqlField"/> that corresponds to this instance.
		/// </summary>
		/// <param name="builder">The object used to build <see cref="SqlField"/> instances.</param>
		internal abstract SqlField CreateSqlField(SqlFieldBuilder builder);


		/// <summary>
		/// Checks that the field contained by this instance is valid.
		/// </summary>
		/// <param name="checker">The <see cref="FieldChecker"/> instance used to check the field.</param>
		internal abstract void CheckField(FieldChecker checker);


	}


}
