﻿using Epsitec.Common.Support;
using Epsitec.Common.Support.Extensions;

using Epsitec.Cresus.Database;

using Epsitec.Cresus.DataLayer.Expressions;


namespace Epsitec.Cresus.DataLayer.Loader
{


	/// <summary>
	/// The <c>ExpressionConverion</c> class contains methods that are used to convert
	/// <see cref="Expression"/> to <see cref="DbAbstractCondition"/>.
	/// </summary>
	internal static class ExpressionConversion
	{


		/// <summary>
		/// Converts an <see cref="UnaryOperation"/> to an equivalent <see cref="DbAbstractCondition"/>,
		/// using a resolver to convert the <see cref="Druid"/> of the fields to the appropriate
		/// <see cref="DbTableColumn"/>.
		/// </summary>
		/// <param name="operation">The <see cref="UnaryOperation"/> to convert.</param>
		/// <param name="columnResolver">The function used to resolve the <see cref="DbTableColumn"/> given an <see cref="Druid"/>.</param>
		/// <returns>The new <see cref="DbAbstractCondition"/>.</returns>
		/// <exception cref="System.ArgumentNullException">If <paramref name="operation"/> is null.</exception>
		/// <exception cref="System.ArgumentNullException">If <paramref name="columnResolver"/> is null.</exception>
		public static DbAbstractCondition CreateDbCondition(UnaryOperation operation, System.Func<Druid, DbTableColumn> columnResolver)
		{
			operation.ThrowIfNull ("operation");
			columnResolver.ThrowIfNull ("columnResolver");

			DbAbstractCondition condition = operation.Expression.CreateDbCondition (columnResolver);
			DbConditionModifierOperator op = EnumConverter.ToDbConditionModifierOperator (operation.Operator);

			return new DbConditionModifier (op, condition);
		}


		/// <summary>
		/// Converts an <see cref="BinaryOperation"/> to an equivalent <see cref="DbAbstractCondition"/>,
		/// using a resolver to convert the <see cref="Druid"/> of the fields to the appropriate
		/// <see cref="DbTableColumn"/>.
		/// </summary>
		/// <param name="operation">The <see cref="BinaryOperation"/> to convert.</param>
		/// <param name="columnResolver">The function used to resolve the <see cref="DbTableColumn"/> given an <see cref="Druid"/>.</param>
		/// <returns>The new <see cref="DbAbstractCondition"/>.</returns>
		/// <exception cref="System.ArgumentNullException">If <paramref name="operation"/> is null.</exception>
		/// <exception cref="System.ArgumentNullException">If <paramref name="columnResolver"/> is null.</exception>
		public static DbAbstractCondition CreateDbCondition(BinaryOperation operation, System.Func<Druid, DbTableColumn> columnResolver)
		{
			operation.ThrowIfNull ("operation");
			columnResolver.ThrowIfNull ("columnResolver");

			DbAbstractCondition left = operation.Left.CreateDbCondition (columnResolver);
			DbAbstractCondition right = operation.Right.CreateDbCondition (columnResolver);

			DbConditionCombinerOperator op = EnumConverter.ToDbConditionCombinerOperator (operation.Operator);

			return new DbConditionCombiner (op, left, right);
		}


		/// <summary>
		/// Converts an <see cref="UnaryComparison"/> to an equivalent <see cref="DbAbstractCondition"/>,
		/// using a resolver to convert the <see cref="Druid"/> of the fields to the appropriate
		/// <see cref="DbTableColumn"/>.
		/// </summary>
		/// <param name="comparison">The <see cref="UnaryComparison"/> to convert.</param>
		/// <param name="columnResolver">The function used to resolve the <see cref="DbTableColumn"/> given an <see cref="Druid"/>.</param>
		/// <returns>The new <see cref="DbAbstractCondition"/>.</returns>
		/// <exception cref="System.ArgumentNullException">If <paramref name="comparison"/> is null.</exception>
		/// <exception cref="System.ArgumentNullException">If <paramref name="columnResolver"/> is null.</exception>
		public static DbAbstractCondition CreateDbCondition(UnaryComparison comparison, System.Func<Druid, DbTableColumn> columnResolver)
		{
			comparison.ThrowIfNull ("comparison");
			columnResolver.ThrowIfNull ("columnResolver");

			DbTableColumn field = comparison.Field.GetDbTableColumn (columnResolver);
			DbSimpleConditionOperator op = EnumConverter.ToDbSimpleConditionOperator (comparison.Operator);

			return new DbSimpleCondition (field, op);
		}


		/// <summary>
		/// Converts an <see cref="ComparisonFieldField"/> to an equivalent <see cref="DbAbstractCondition"/>,
		/// using a resolver to convert the <see cref="Druid"/> of the fields to the appropriate
		/// <see cref="DbTableColumn"/>.
		/// </summary>
		/// <param name="comparison">The <see cref="ComparisonFieldField"/> to convert.</param>
		/// <param name="columnResolver">The function used to resolve the <see cref="DbTableColumn"/> given an <see cref="Druid"/>.</param>
		/// <returns>The new <see cref="DbAbstractCondition"/>.</returns>
		/// <exception cref="System.ArgumentNullException">If <paramref name="comparison"/> is null.</exception>
		/// <exception cref="System.ArgumentNullException">If <paramref name="columnResolver"/> is null.</exception>
		public static DbAbstractCondition CreateDbCondition(ComparisonFieldField comparison, System.Func<Druid, DbTableColumn> columnResolver)
		{
			comparison.ThrowIfNull ("comparison");
			columnResolver.ThrowIfNull ("columnResolver");

			DbTableColumn left = comparison.Left.GetDbTableColumn (columnResolver);
			DbSimpleConditionOperator op = EnumConverter.ToDbSimpleConditionOperator (comparison.Operator);
			DbTableColumn right = comparison.Right.GetDbTableColumn (columnResolver);

			return new DbSimpleCondition (left, op, right);
		}


		/// <summary>
		/// Converts an <see cref="ComparisonFieldValue"/> to an equivalent <see cref="DbAbstractCondition"/>,
		/// using a resolver to convert the <see cref="Druid"/> of the fields to the appropriate
		/// <see cref="DbTableColumn"/>.
		/// </summary>
		/// <param name="comparison">The <see cref="ComparisonFieldValue"/> to convert.</param>
		/// <param name="columnResolver">The function used to resolve the <see cref="DbTableColumn"/> given an <see cref="Druid"/>.</param>
		/// <returns>The new <see cref="DbAbstractCondition"/>.</returns>
		/// <exception cref="System.ArgumentNullException">If <paramref name="comparison"/> is null.</exception>
		/// <exception cref="System.ArgumentNullException">If <paramref name="columnResolver"/> is null.</exception>
		public static DbAbstractCondition CreateDbCondition(ComparisonFieldValue comparison, System.Func<Druid, DbTableColumn> columnResolver)
		{
			comparison.ThrowIfNull ("comparison");
			columnResolver.ThrowIfNull ("columnResolver");

			DbTableColumn left = comparison.Left.GetDbTableColumn (columnResolver);
			DbSimpleConditionOperator op = EnumConverter.ToDbSimpleConditionOperator (comparison.Operator);
			object rightValue = comparison.Right.Value;
			DbRawType rightType = EnumConverter.ToDbRawType (comparison.Right.Type);

			return new DbSimpleCondition (left, op, rightValue, rightType);
		}


		/// <summary>
		/// Gets the <see cref="DbTableColumn"/> corresponding to the given <see cref="Field"/> out
		/// of the resolver.
		/// </summary>
		/// <param name="field">The <see cref="Field"/> whose corresponding <see cref="DbTableColumn"/> to get.</param>
		/// <param name="columnResolver">The function used to resolve the <see cref="DbTableColumn"/> given an <see cref="Druid"/>.</param>
		/// <returns>The <see cref="DbTableColumn"/>.</returns>
		/// <exception cref="System.ArgumentNullException">If <paramref name="field"/> is null.</exception>
		/// <exception cref="System.ArgumentNullException">If <paramref name="columnResolver"/> is null.</exception>
		private static DbTableColumn GetDbTableColumn(this Field field, System.Func<Druid, DbTableColumn> columnResolver)
		{
			field.ThrowIfNull ("field");
			columnResolver.ThrowIfNull ("columnResolver");

			return columnResolver (field.FieldId);
		}


	}


}
