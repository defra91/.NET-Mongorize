// <copyright file="QueryOptions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Models;

using System.Linq.Expressions;
using Mongorize.Entities;
using Mongorize.Models.Enums;
using Mongorize.Models.Interfaces;

/// <summary>
/// Represents an object container for bulding a query related to the
/// provided type param.
/// </summary>
/// <typeparam name="TEntity">The entity to work with.</typeparam>
public class QueryOptions<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the pagination object.
    /// </summary>
    public Pagination Pagination { get; set; }

    /// <summary>
    /// Gets or sets the filters object.
    /// </summary>
    public IFilter<TEntity> Filters { get; set; }

    /// <summary>
    /// Gets or sets the list of projections inclusions.
    /// </summary>
    public List<Expression<Func<TEntity, object>>> IncludeProjections { get; set; } = new ();

    /// <summary>
    /// Gets or sets the list of projections exclusions.
    /// </summary>
    public List<Expression<Func<TEntity, object>>> ExcludeProjections { get; set; } = new ();

    /// <summary>
    /// Gets or sets the list of sort criteria.
    /// </summary>
    public List<(Expression<Func<TEntity, object>> SortBy, ESortByDirection Direction)> SortCriteria { get; set; } = new ();
}