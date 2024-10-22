// <copyright file="BaseSortResolver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Resolvers;

using System;
using System.Linq.Expressions;
using Mongorize.Entities;
using Mongorize.Resolvers.Interfaces;

/// <summary>
/// Base implementation of the <see cref="ISortResolver{TEntity}"/>.
/// </summary>
/// <typeparam name="TEntity">The entity to work with.</typeparam>
public class BaseSortResolver<TEntity> : ISortResolver<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Gets the sort dictionary that maps the field name with the field expression.
    /// </summary>
    protected Dictionary<string, Expression<Func<TEntity, object>>> SortDictionary { get; }
        = new Dictionary<string, Expression<Func<TEntity, object>>>
        {
            { "id", x => x.Id },
            { "createdAt", x => x.CreatedAt },
            { "updatedAt", x => x.UpdatedAt },
        };

    /// <inheritdoc/>
    public Expression<Func<TEntity, object>> Resolve(string fieldName)
    {
        if (string.IsNullOrEmpty(fieldName) || !this.SortDictionary.ContainsKey(fieldName))
        {
            return this.SortDictionary["id"];
        }

        return this.SortDictionary[fieldName];
    }
}