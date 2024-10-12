// <copyright file="BaseRepository.cs" company="Luca De Franceschi">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Repositories
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Mongorize.Contexts.Interfaces;
    using Mongorize.Entities;
    using Mongorize.Repositories.Interfaces;

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
    }
}