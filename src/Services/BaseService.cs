// <copyright file="BaseService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Services;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Mongorize.Builders;
using Mongorize.Entities;
using Mongorize.Models;
using Mongorize.Models.Enums;
using Mongorize.Models.Interfaces;
using Mongorize.Repositories.Interfaces;
using Mongorize.Resolvers;
using Mongorize.Resolvers.Interfaces;
using Mongorize.Services.Interfaces;

/// <summary>
/// Base service abstract class that implements the <see cref="IBaseService{TEntity}"/> interface.
/// This class is intended to provide the base service features working with the repository related to the
/// provided entity.
/// </summary>
/// <typeparam name="TEntity">The entity to work with.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="BaseService{TEntity}"/> class.
/// </remarks>
/// <param name="repository">The repository reference.</param>
public abstract class BaseService<TEntity>(IRepository<TEntity> repository)
    : IBaseService<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Gets the repository related to the provided entity.
    /// </summary>
    protected IRepository<TEntity> Repository { get; } = repository;

    /// <inheritdoc />
    public Task<long> CountAsync(IFilter<TEntity> filters, CancellationToken cToken)
    {
        QueryFiltersOptions<TEntity> filterOptions =
            new QueryFiltersOptionsBuilder<TEntity, QueryFiltersOptions<TEntity>>(new QueryFiltersOptions<TEntity>())
            .WithFilters(filters)
            .Build();

        return this.Repository.CountAsync(filterOptions, cToken);
    }

    /// <inheritdoc />
    public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cToken)
        => this.Repository.CreateAsync(entity, cToken);

    /// <inheritdoc />
    public Task<TEntity> GetByIdAsync(string id, CancellationToken cToken)
        => this.Repository.GetByIdAsync(id, cToken);

    /// <inheritdoc />
    public Task<List<TEntity>> GetListAsync(
        Pagination pagination = null,
        IFilter<TEntity> filters = null,
        (string FieldName, ESortByDirection Direction) sortBy = default,
        CancellationToken cToken = default)
    {
        QueryOptions<TEntity> queryOptions =
            new QueryOptionsBuilder<TEntity>()
            .WithPagination(pagination.Page, pagination.ItemsPerPage)
            .WithSorting(this.GetSortResolver().Resolve(sortBy.FieldName), sortBy.Direction)
            .WithFilters(filters)
            .Build();

        return this.Repository.GetListAsync(queryOptions, cToken);
    }

    /// <inheritdoc />
    public Task<long> RemoveByIdAsync(string id, CancellationToken cToken)
        => this.Repository.RemoveByIdAsync(id, cToken);

    /// <inheritdoc />
    public Task<long> RemoveManyAsync(IFilter<TEntity> filters, CancellationToken cToken)
    {
        QueryFiltersOptions<TEntity> filterOptions =
            new QueryFiltersOptionsBuilder<TEntity, QueryFiltersOptions<TEntity>>(new QueryFiltersOptions<TEntity>())
            .WithFilters(filters)
            .Build();

        return this.Repository.RemoveManyAsync(filterOptions, cToken);
    }

    /// <inheritdoc />
    public Task<TEntity> UpdateOneAsync(TEntity entity, CancellationToken cToken)
        => this.Repository.UpdateOneAsync(entity, cToken);

    /// <summary>
    /// Gets a reference to the sort resolver.
    /// </summary>
    /// <returns>The obtained sort resolver.</returns>
    protected virtual ISortResolver<TEntity> GetSortResolver()
        => new BaseSortResolver<TEntity>();
}