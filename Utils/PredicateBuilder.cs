// <copyright file="PredicateBuilder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Utils;

using System.Linq.Expressions;

/// <summary>
/// Represents a class for building mongodb queries.
/// </summary>
public static class PredicateBuilder
{
    /// <summary>
    /// Add a new expression to the query with AND condition.
    /// </summary>
    /// <param name="a">The expression a.</param>
    /// <param name="b">The expression b.</param>
    /// <typeparam name="T">The entity parameterized.</typeparam>
    /// <returns>The combined expression.</returns>
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
    {
        ParameterExpression p = a.Parameters[0];

        SubstExpressionVisitor visitor = new SubstExpressionVisitor();
        visitor.Subst[b.Parameters[0]] = p;

        Expression body = Expression.AndAlso(a.Body, visitor.Visit(b.Body));
        return Expression.Lambda<Func<T, bool>>(body, p);
    }

    /// <summary>
    /// Add a new expression to the query with OR condition.
    /// </summary>
    /// <param name="a">The expression a.</param>
    /// <param name="b">The expression b.</param>
    /// <typeparam name="T">The entity parameterized.</typeparam>
    /// <returns>The combined expression.</returns>
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
    {
        ParameterExpression p = a.Parameters[0];

        SubstExpressionVisitor visitor = new SubstExpressionVisitor();
        visitor.Subst[b.Parameters[0]] = p;

        Expression body = Expression.OrElse(a.Body, visitor.Visit(b.Body));
        return Expression.Lambda<Func<T, bool>>(body, p);
    }
}