// <copyright file="IFilter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Models.Interfaces;

using System.Linq.Expressions;

/// <summary>
/// Represents an interface for working with filters.
/// Filters are used for fetching a list of a specific entity.
/// </summary>
/// <typeparam name="TEntity">The entity the filter is working with.</typeparam>
public interface IFilter<TEntity>
{
    /// <summary>
    /// Get the list of expressions the filters object
    /// produces.
    /// </summary>
    /// <returns>The list of expressions produced by the filters.</returns>
    public List<Expression<Func<TEntity, bool>>> GetExpressions();
}