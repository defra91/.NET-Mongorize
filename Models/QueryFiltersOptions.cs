// <copyright file="QueryFiltersOptions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Models;

using Mongorize.Entities;
using Mongorize.Models.Interfaces;

/// <summary>
/// Represents the query options object containing only the filters.
/// </summary>
/// <typeparam name="TEntity">The entity type to work with.</typeparam>
public class QueryFiltersOptions<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the filters object.
    /// </summary>
    public IFilter<TEntity> Filters { get; set; }
}