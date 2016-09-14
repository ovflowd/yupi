namespace Yupi.Util
{
    using System;
    using System.Linq.Expressions;

    public static class ExpressionExt
    {
        #region Methods

        /// <summary>
        /// And for LINQ Expressions
        /// </summary>
        /// <returns>The new Expression.</returns>
        /// <param name="expr1">Expression One.</param>
        /// <param name="expr2">Expression Two.</param>
        /// <typeparam name="T">The function parameter type</typeparam>
        /// <see cref="http://stackoverflow.com/a/457328"/>
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(left, right), parameter);
        }

        #endregion Methods

        #region Nested Types

        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            #region Fields

            private readonly Expression _newValue;
            private readonly Expression _oldValue;

            #endregion Fields

            #region Constructors

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            #endregion Constructors

            #region Methods

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                    return _newValue;
                return base.Visit(node);
            }

            #endregion Methods
        }

        #endregion Nested Types
    }
}