// <copyright file="MongoUtils.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Utils;
using MongoDB.Driver;
using System.Linq.Expressions;

/// <summary>
/// Static utility class collecting utilities methods to work with mongodb.
/// </summary>
public static class MongoUtils
{
    /// <summary>
    /// Set the projection to the find fluent by using the provided lists of include and exclude.
    /// </summary>
    /// <typeparam name="TEntity">The entity type to work with.</typeparam>
    /// <param name="find">The find fluent object.</param>
    /// <param name="includeProjections">The projection include list.</param>
    /// <param name="excludeProjections">The projection exclude list.</param>
    /// <returns>The updated find fluent with the projection.</returns>
    public static IFindFluent<TEntity, TEntity> SetProjections<TEntity>(
        IFindFluent<TEntity, TEntity> find,
        List<Expression<Func<TEntity, object>>> includeProjections = null,
        List<Expression<Func<TEntity, object>>> excludeProjections = null)
    {
        var projectionBuilder = Builders<TEntity>.Projection;
        List<ProjectionDefinition<TEntity>> projectionDefinitions = new List<ProjectionDefinition<TEntity>>();

        foreach (var exp in includeProjections ?? new List<Expression<Func<TEntity, object>>>())
        {
            var includeProjection = projectionBuilder.Include(exp);
            projectionDefinitions.Add(includeProjection);
        }

        foreach (var exp in excludeProjections ?? new List<Expression<Func<TEntity, object>>>())
        {
            var excludeProjection = projectionBuilder.Exclude(exp);
            projectionDefinitions.Add(excludeProjection);
        }

        var combinedProjection = projectionBuilder.Combine(projectionDefinitions);
        return find.Project<TEntity>(combinedProjection);
    }
}