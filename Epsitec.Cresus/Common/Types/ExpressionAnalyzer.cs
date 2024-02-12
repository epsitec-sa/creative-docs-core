//	Copyright © 2010-2019, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Epsitec.Common.Types
{
    /// <summary>
    /// The <c>ExpressionAnalyzer</c> takes a lambda expression and returns runtime information
    /// about it (for instance the property info if the lambda is like <c>x =&gt; x.Foo</c>).
    /// </summary>
    public static class ExpressionAnalyzer
    {
        /// <summary>
        /// Gets the property info for the specified expression, which must be a simply lambda
        /// accessor (such as <c>x =&gt; x.Foo</c>).
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The matching property info; otherwise, <c>null</c>.</returns>
        public static PropertyInfo GetLambdaPropertyInfo(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Lambda)
            {
                return ExpressionAnalyzer.GetLambdaPropertyInfo(expression as LambdaExpression);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Explodes the lambda into a sequence of properties. For instance, <c>x => x.A.B</c>
        /// would return a list with properties <c>A</c> and <c>B</c>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="trimCount">The trim count (<c>1</c> means don't include the last property).</param>
        /// <returns>The collection of properties.</returns>
        public static IEnumerable<PropertyInfo> ExplodeLambda(
            Expression expression,
            int trimCount = 0
        )
        {
            if (expression.NodeType != ExpressionType.Lambda)
            {
                throw new System.ArgumentException("Expression is not a valid lambda");
            }

            var list = new List<PropertyInfo>();
            var lambda = expression as LambdaExpression;
            var body = lambda.Body as MemberExpression;

            System.Diagnostics.Debug.Assert(lambda != null);

            while (body != null)
            {
                if (trimCount == 0)
                {
                    list.Insert(0, body.Member as PropertyInfo);
                }
                else
                {
                    trimCount--;
                }

                body = body.Expression as MemberExpression;
            }

            return list;
        }

        /// <summary>
        /// Builds the expression tree based on a collection of properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns></returns>
        public static LambdaExpression BuildLambdaExpression(
            System.Type objectType,
            IEnumerable<PropertyInfo> properties
        )
        {
            var item = Expression.Parameter(objectType, "x");
            var expr = item as Expression;

            foreach (var property in properties)
            {
                expr = Expression.Property(expr, property);
            }

            return Expression.Lambda(expr, item);
        }

        /// <summary>
        /// Replaces the parameter of the lambda expression with another one.
        /// </summary>
        /// <param name="expression">The lambda expression.</param>
        /// <param name="paramNew">The replacement parameter.</param>
        /// <returns>The updated lambda expression.</returns>
        public static Expression ReplaceParameter(
            LambdaExpression expression,
            ParameterExpression paramNew
        )
        {
            return ExpressionAnalyzer.ReplaceParameter(
                expression,
                expression.Parameters.First(),
                paramNew
            );
        }

        /// <summary>
        /// Replaces the parameter of the lambda expression with another one.
        /// </summary>
        /// <param name="expression">The lambda expression.</param>
        /// <param name="paramOld">The original parameter.</param>
        /// <param name="paramNew">The replacement parameter.</param>
        /// <returns>
        /// The updated lambda expression.
        /// </returns>
        public static Expression ReplaceParameter(
            LambdaExpression expression,
            ParameterExpression paramOld,
            ParameterExpression paramNew
        )
        {
            var replacer = new ExpressionParameterReplacer(paramOld, paramNew);
            return replacer.Visit(expression.Body);
        }

        /// <summary>
        /// Recursively generates the expression tree, build by combining all individual expressions.
        /// </summary>
        /// <param name="list">The list of expressions.</param>
        /// <param name="combiner">The combiner.</param>
        /// <returns>
        /// The combined expression.
        /// </returns>
        public static Expression CombineExpressions(
            IEnumerable<Expression> list,
            System.Func<Expression, Expression, Expression> combiner
        )
        {
            Expression left = null;

            foreach (var right in list)
            {
                if (left == null)
                {
                    left = right;
                }
                else
                {
                    left = combiner(left, right);
                }
            }

            return left;
        }

        /// <summary>
        /// Creates a setter expression based on a getter expression.
        /// </summary>
        /// <param name="getterExpression">The getter expression.</param>
        /// <returns>The corresponding setter expression.</returns>
        public static LambdaExpression CreateSetter(LambdaExpression getterExpression)
        {
            //  If the getter expression is not a member access (i.e. it is a parameter
            //  provided by value or a method call), then we cannot infer a setter:

            if (
                (getterExpression.Body.NodeType == ExpressionType.Parameter)
                || (getterExpression.Body.NodeType == ExpressionType.Call)
            )
            {
                return null;
            }

            var lambdaMember = (MemberExpression)getterExpression.Body;
            var propertyInfo = lambdaMember.Member as System.Reflection.PropertyInfo;
            var fieldType = getterExpression.ReturnType;

            if (propertyInfo.CanWrite == false)
            {
                return null;
            }

            var sourceParameterExpression = getterExpression.Parameters[0];
            var valueParameterExpression = Expression.Parameter(fieldType, "value");

            var expressionBlock = Expression.Block(
                Expression.Assign(
                    Expression.Property(lambdaMember.Expression, propertyInfo.Name),
                    valueParameterExpression
                )
            );

            var setterLambda = Expression.Lambda(
                expressionBlock,
                sourceParameterExpression,
                valueParameterExpression
            );

            return setterLambda;
        }

        private static PropertyInfo GetLambdaPropertyInfo(LambdaExpression expression)
        {
            var body = expression.Body;

            if (body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression member = body as MemberExpression;
                PropertyInfo propertyInfo = member.Member as PropertyInfo;

                if ((propertyInfo != null) && (propertyInfo.CanRead))
                {
                    return propertyInfo;
                }
            }

            return null;
        }
    }
}
