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
    using Mongorize.Models.Enums;
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
            var combinedFilter = GetCombinedFilter(options);

            IFindFluent<TEntity, TEntity> find = this.dbCollection.Find(combinedFilter);

            if (options.Pagination is { ItemsPerPage: > 0, Page: > 0 })
            {
                find = find
                    .Limit(options.Pagination.ItemsPerPage)
                    .Skip((options.Pagination.Page - 1) * options.Pagination.ItemsPerPage);
            }

            find = ApplySorting(find, options.SortCriteria);

            if (options.IncludeProjections.Count > 0 || options.ExcludeProjections.Count > 0)
            {
                find = Utils.MongoUtils.SetProjections<TEntity>(find, options.IncludeProjections, options.ExcludeProjections);
            }

            return find.ToListAsync(cToken);
        }

        /// <inheritdoc />
        public Task<long> Count(QueryFiltersOptions<TEntity> options, CancellationToken cToken)
        {
            var combinedFilter = GetCombinedFilter(options);

            return this.dbCollection.CountDocumentsAsync(combinedFilter, null, cToken);
        }

        /// <inheritdoc />
        public async Task<TEntity> Create(TEntity entity, CancellationToken cToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            }

            this.BeforeWrite(entity, true);
            await this.dbCollection.InsertOneAsync(entity, null, cToken);
            return entity;
        }

        /// <inheritdoc/>
        public async Task<List<TEntity>> CreateRange(List<TEntity> list, CancellationToken cToken)
        {
            if (list?.Count <= 0)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " list is empty");
            }

            foreach (var t in list)
            {
                this.BeforeWrite(t, true);
            }

            await this.dbCollection.InsertManyAsync(list, null, cToken);
            return list;
        }

        /// <inheritdoc/>
        public async Task<TEntity> Update(TEntity entity, CancellationToken cToken)
        {
            this.BeforeWrite(entity, newRecord: false);
            await this.dbCollection.ReplaceOneAsync(x => x.Id == entity.Id, entity, new ReplaceOptions() { }, cToken);

            return await this.GetById(entity.Id, cToken);
        }

        /// <inheritdoc/>
        public async Task<long> UpdateMultipleFieldsAsync(
        QueryFiltersOptions<TEntity> options,
        Dictionary<Expression<Func<TEntity, object>>, object> updateDict,
        CancellationToken cToken)
        {
            if (updateDict?.Any() != true)
            {
                throw new ArgumentException("No fields provided for update.", nameof(updateDict));
            }

            var combinedFilter = GetCombinedFilter(options);

            UpdateDefinitionBuilder<TEntity> updateDefinitionBuilder = Builders<TEntity>.Update;

            // Build a list of updates and then combine
            List<UpdateDefinition<TEntity>> updates = new List<UpdateDefinition<TEntity>>();
            foreach (var keyPairValue in updateDict)
            {
                updates.Add(updateDefinitionBuilder.Set(keyPairValue.Key, keyPairValue.Value));
            }

            updates.Add(updateDefinitionBuilder.Set(x => x.UpdatedAt, DateTime.Now)); // Set updated at field to datetime now.

            UpdateDefinition<TEntity> combinedUpdate = updateDefinitionBuilder.Combine(updates);

            UpdateResult updateResult = await this.dbCollection.UpdateManyAsync(combinedFilter, combinedUpdate, null, cToken);

            return updateResult.ModifiedCount;
        }

         /// <inheritdoc />
        public Task<TEntity> FindOne(QueryOptions<TEntity> options, CancellationToken cToken)
        {
            var combinedFilter = GetCombinedFilter(options);

            var find = this.dbCollection.Find(combinedFilter);
            find = ApplySorting(find, options.SortCriteria);

            if (options.IncludeProjections.Count > 0 || options.ExcludeProjections.Count > 0)
            {
                find = Utils.MongoUtils.SetProjections<TEntity>(find, options.IncludeProjections, options.ExcludeProjections);
            }

            return find.FirstOrDefaultAsync(cToken);
        }

        /// <inheritdoc />
        public async Task<long> RemoveById(string id, CancellationToken cToken)
        {
            DeleteResult deleteResult = await this.dbCollection.DeleteOneAsync(x => x.Id == id, cToken);
            return deleteResult.DeletedCount;
        }

        /// <inheritdoc />
        public async Task<long> RemoveMany(QueryFiltersOptions<TEntity> options, CancellationToken cToken)
        {
            var combinedFilter = GetCombinedFilter(options);

            DeleteResult deleteResult = await this.dbCollection.DeleteManyAsync(combinedFilter, cToken);
            return deleteResult.DeletedCount;
        }

        /// <summary>
        /// Performs an action on the entity before writing (create or update).
        /// </summary>
        /// <param name="entity">The entity to work on.</param>
        /// <param name="newRecord">Value indicating whether the record is new or not.</param>
        protected virtual void BeforeWrite(TEntity entity, bool newRecord = false)
        {
            entity.UpdatedAt = DateTime.Now;
            if (newRecord)
            {
                entity.CreatedAt = entity.CreatedAt == null ? DateTime.Now : entity.CreatedAt;
            }
        }

        private static Expression<Func<TEntity, bool>> GetCombinedFilter(QueryFiltersOptions<TEntity> options)
        {
            return options.Filters != null
                ? ExpressionUtils.CombineAndExpression(options.Filters.GetExpressions())
                : _ => true;
        }

        private static IFindFluent<TEntity, TEntity> ApplySorting(
            IFindFluent<TEntity, TEntity> find,
            List<(Expression<Func<TEntity, object>> SortBy, ESortByDirection Direction)> sortCriteria)
        {
            foreach (var (sortBy, direction) in sortCriteria)
            {
                find = direction == ESortByDirection.Ascending
                    ? find.SortBy(sortBy)
                    : find.SortByDescending(sortBy);
            }

            return find;
        }
    }
}