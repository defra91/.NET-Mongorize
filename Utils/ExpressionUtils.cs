// <copyright file="ExpressionUtils.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Utils;

using System.Linq.Expressions;

/// <summary>
/// Represents a collection of utilities methods related to linq <see cref="Expression"/>.
/// </summary>
public static class ExpressionUtils
{
    /// <summary>
    /// Given a list of linq expressions returns the combined expression with and logic.
    /// </summary>
    /// <typeparam name="TEntity">The entity type to work with.</typeparam>
    /// <param name="expressions">The list of expressions.</param>
    /// <returns>The combined expression.</returns>
    public static Expression<Func<TEntity, bool>> CombineAndExpression<TEntity>(List<Expression<Func<TEntity, bool>>> expressions)
    {
        Expression<Func<TEntity, bool>> combinedFilter;
        if ((expressions?.Count ?? 0) > 0)
        {
            combinedFilter = expressions.Aggregate(PredicateBuilder.And);
        }
        else
        {
            combinedFilter = x => true;
        }

        return combinedFilter;
    }
}