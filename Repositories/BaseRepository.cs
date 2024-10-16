// <copyright file="BaseRepository.cs" company="Luca De Franceschi">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Repositories
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Mongorize.Contexts.Interfaces;
    using Mongorize.Entities;
    using Mongorize.Models;
    using Mongorize.Repositories.Interfaces;
    using Mongorize.Utils;

    /// <summary>
    /// Implementation of the <see cref="IRepository{TEntity}"/> interface.
    /// </summary>
    /// <typeparam name="TEntity">The entity to work with that is of type <see cref="BaseEntity"/>.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.
    /// </remarks>
    /// <param name="context">The <see cref="IMongoContext"/> reference.</param>
    public abstract class BaseRepository<TEntity>(IMongoContext context)
        : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly IMongoCollection<TEntity> dbCollection = context.GetCollection<TEntity>();

        /// <inheritdoc />
        public Task<TEntity> GetById(string id, CancellationToken cToken)
        {
            ArgumentNullException.ThrowIfNull(id);

            ObjectId objectId = new (id);

            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", objectId);

            return this.dbCollection
                .FindAsync(filter, cancellationToken: cToken)
                .Result
                .FirstOrDefaultAsync(cancellationToken: cToken);
        }

        /// <inheritdoc />
        public Task<List<TEntity>> GetList(QueryOptions<TEntity> options, CancellationToken cToken)
        {
            var combinedFilter = options.Filters != null
                ? ExpressionUtils.CombineAndExpression(options.Filters.GetExpressions())
                : _ => true;

            IFindFluent<TEntity, TEntity> find = this.dbCollection.Find(combinedFilter);

            if (options.Pagination != null)
            {
                find = find
                    .Limit(options.Pagination.ItemsPerPage)
                    .Skip((options.Pagination.Page - 1) * options.Pagination.ItemsPerPage);
            }

            foreach (var (sortBy, direction) in options.SortCriteria)
            {
                find = direction == Models.Enums.ESortByDirection.Ascending
                    ? find.SortBy(sortBy)
                    : find.SortByDescending(sortBy);
            }

            if (options.IncludeProjections.Count > 0 || options.ExcludeProjections.Count > 0)
            {
                find = Utils.MongoUtils.SetProjections<TEntity>(find, options.IncludeProjections, options.ExcludeProjections);
            }

            return find.ToListAsync(cToken);
        }
    }
}