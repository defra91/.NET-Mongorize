// <copyright file="IRepository.cs" company="Luca De Franceschi">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Repositories.Interfaces;

using System.Linq.Expressions;
using Mongorize.Entities;
using Mongorize.Models;

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

    /// <summary>
    /// Returns a list of entity fetched from database. Fetch is made with parameters contained on
    /// the <paramref name="options"/> parameter.
    /// </summary>
    /// <param name="options">An object of type <see cref="QueryOptions{TEntity}"/> that contains
    /// all the parameters used for building the query.</param>
    /// <param name="cToken">The cancellation token used to cancel the async operation.</param>
    /// <returns>The list of entities found with the given filters and pagination.</returns>
    public Task<List<TEntity>> GetList(QueryOptions<TEntity> options, CancellationToken cToken);

    /// <summary>
    /// Returns the count of entities fetched from database. Fetch is made with parameters contained on
    /// the <paramref name="options"/> parameter.
    /// </summary>
    /// <param name="options">An object of type <see cref="QueryFiltersOptions{TEntity}"/> that contains
    /// all the parameters used for building the count query.</param>
    /// <param name="cToken">The cancellation token used to cancel the async operation.</param>
    /// <returns>The count of entities found with the given <paramef name="options" />.</returns>
    public Task<long> Count(QueryFiltersOptions<TEntity> options, CancellationToken cToken);

    /// <summary>
    /// Creates a new entity on database.
    /// </summary>
    /// <param name="entity">The entity object to create.</param>
    /// <param name="cToken">The cancellation token.</param>
    /// <returns>The created entity with its id.</returns>
    public Task<TEntity> Create(TEntity entity, CancellationToken cToken);

    /// <summary>
    /// Creates a range of entities on database.
    /// </summary>
    /// <param name="list">The list of entities.</param>
    /// <param name="cToken">The cancellation token.</param>
    /// <returns>The list of created entities.</returns>
    public Task<List<TEntity>> CreateRange(List<TEntity> list, CancellationToken cToken);

    /// <summary>
    /// Update an existing entity on database identified by its id.
    /// </summary>
    /// <param name="entity">The entity object.</param>
    /// <param name="cToken">The cancellation token.</param>
    /// <returns>Task containing the updated entity.</returns>
    public Task<TEntity> Update(TEntity entity, CancellationToken cToken);

    /// <summary>
    /// Given a query filters options and a dictionary containing
    /// the field expression and the field update, perform the update of documents
    /// that match the provided filters and set the fields as provided on the dictionary.
    /// </summary>
    /// <param name="options">The filters options.</param>
    /// <param name="updateDict">The update dictionary.</param>
    /// <param name="cToken">The cancellation token.</param>
    /// <returns>The count of updated entities.</returns>
    public Task<long> UpdateMultipleFieldsAsync(
        QueryFiltersOptions<TEntity> options,
        Dictionary<Expression<Func<TEntity, object>>, object> updateDict,
        CancellationToken cToken);

    /// <summary>
    /// Find a single entity with the provided qurery options.
    /// </summary>
    /// <param name="options">The query options object.</param>
    /// <param name="cToken">The cancellation token.</param>
    /// <returns>The found entity or null if the entity has not been found.</returns>
    public Task<TEntity> FindOne(QueryOptions<TEntity> options, CancellationToken cToken);

    /// <summary>
    /// Removes an existing entity by its id.
    /// </summary>
    /// <param name="id">The id of the entity.</param>
    /// <param name="cToken">The cancellation token.</param>
    /// <returns>The completed task containing the number of deleted elements.</returns>
    public Task<long> RemoveById(string id, CancellationToken cToken);

    /// <summary>
    /// Remove many entities by the provided query filters options.
    /// </summary>
    /// <param name="options">The query filters options.</param>
    /// <param name="cToken">The cancellation token.</param>
    /// <returns>The amount of removed items.</returns>
    public Task<long> RemoveMany(QueryFiltersOptions<TEntity> options, CancellationToken cToken);
}