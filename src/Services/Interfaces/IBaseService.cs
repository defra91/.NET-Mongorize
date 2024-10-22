// <copyright file="IBaseService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Services.Interfaces;

using Mongorize.Entities;
using Mongorize.Models;
using Mongorize.Models.Enums;
using Mongorize.Models.Interfaces;

/// <summary>
/// Defines the basic operations for a service working with an entity of type <typeparamref name="TEntity"/>.
/// This interface encapsulates the business logic layer's interactions with the repository layer.
/// </summary>
/// <typeparam name="TEntity">The entity type this service handles.</typeparam>
public interface IBaseService<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <param name="cToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found.</returns>
    public Task<TEntity> GetByIdAsync(string id, CancellationToken cToken);

    /// <summary>
    /// Retrieves a list of entities based on the provided pagination, filters and sort options.
    /// </summary>
    /// <param name="pagination">The <see cref="Pagination"/> object.</param>
    /// <param name="filters">The filters object.</param>
    /// <param name="sortBy">The sortBy tuple that contains the field name and the direction.</param>
    /// <param name="cToken">Cancellation token used to cancel the async operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities.</returns>
    public Task<List<TEntity>> GetListAsync(
        Pagination pagination = null,
        IFilter<TEntity> filters = null,
        (string FieldName, ESortByDirection Direction) sortBy = default,
        CancellationToken cToken = default);

    /// <summary>
    /// Retrieves the number of entities matching the specified filters.
    /// </summary>
    /// <param name="filters">The filters object to be used.</param>
    /// <param name="cToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the count of matching entities.</returns>
    public Task<long> CountAsync(IFilter<TEntity> filters, CancellationToken cToken);

    /// <summary>
    /// Creates a new entity and persists it to the underlying data store.
    /// </summary>
    /// <param name="entity">The entity to be created.</param>
    /// <param name="cToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created entity.</returns>
    public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cToken);

    /// <summary>
    /// Updates an existing entity identified by its id.
    /// </summary>
    /// <param name="entity">The entity with updated data.</param>
    /// <param name="cToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
    public Task<TEntity> UpdateOneAsync(TEntity entity, CancellationToken cToken);

    /// <summary>
    /// Deletes an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to be deleted.</param>
    /// <param name="cToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of entities deleted.</returns>
    public Task<long> RemoveByIdAsync(string id, CancellationToken cToken);

    /// <summary>
    /// Deletes entities that match the provided filters.
    /// </summary>
    /// <param name="filters">The query filters to determine which entities to delete.</param>
    /// <param name="cToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of entities deleted.</returns>
    public Task<long> RemoveManyAsync(IFilter<TEntity> filters, CancellationToken cToken);
}