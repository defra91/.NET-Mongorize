// <copyright file="ISortResolver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Resolvers.Interfaces;

using System.Linq.Expressions;
using Mongorize.Entities;

/// <summary>
/// Interface for defining the resolver pattern for the sort fields of an entity.
/// </summary>
/// <typeparam name="TEntity">The entity to work with.</typeparam>
public interface ISortResolver<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Given the field name coming from the application layers returns the related field expression on the entity.
    /// </summary>
    /// <param name="fieldName">The name of the field coming from the application layer.</param>
    /// <returns>The resulting field expression.</returns>
    public Expression<Func<TEntity, object>> Resolve(string fieldName);
}