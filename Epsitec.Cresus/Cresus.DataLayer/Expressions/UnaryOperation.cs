﻿using Epsitec.Common.Support.Extensions;

namespace Epsitec.Cresus.DataLayer.Expressions
{


	/// <summary>
	/// The <c>UnaryOperation</c> class represents a logical operation on a single
	/// <see cref="Expression"/>, such as (Not (a > b)).
	/// </summary>
	public class UnaryOperation : Operation
	{
		

		/// <summary>
		/// Creates a new <c>UnaryOperation</c>.
		/// </summary>
		/// <param name="op">The operation to apply to the <see cref="Expression"/>.</param>
		/// <param name="expression">The <see cref="Expression"/> on which to apply the <see cref="UnaryOperator"/>.</param>
		public UnaryOperation(UnaryOperator op, Expression expression) : base()
		{
			expression.ThrowIfNull ("expression");
			
			this.Operator = op;
			this.Expression = expression;
		}
         

		/// <summary>
		/// Gets the <see cref="UnaryOperator"/> of the current instance.
		/// </summary>
		public UnaryOperator Operator
		{
			get;
			private set;
		}


		/// <summary>
		/// Gets the <see cref="Expression"/> of the current instance.
		/// </summary>
		public Expression Expression
		{
			get;
			private set;
		}


	}


}
