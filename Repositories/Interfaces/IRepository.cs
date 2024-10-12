// <copyright file="IRepository.cs" company="Luca De Franceschi">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Repositories.Interfaces;

using Mongorize.Entities;

/// <summary>
/// Represents the repository interface to work with the provided entity.
/// </summary>
/// <typeparam name="TEntity">The entity to work with that should be of type <see cref="BaseEntity"/>.</typeparam>
public interface IRepository<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Given the id of an entity performs an async operation and fetch the entity
    /// by the provided id.
    /// </summary>
    /// <param name="id">The id of the entity.</param>
    /// <param name="cToken">The cancellation token.</param>
    /// <returns>The <see cref="Task"/> that contains the found entity or null if not founds.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null.</exception>
    public Task<TEntity> GetById(string id, CancellationToken cToken);
}