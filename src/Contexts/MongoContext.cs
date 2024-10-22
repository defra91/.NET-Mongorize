// <copyright file="MongoContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Contexts
{
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Core.Events;
    using Mongorize.Attributes;
    using Mongorize.Contexts.Interfaces;
    using Mongorize.Entities;
    using Mongorize.Settings;

    /// <summary>
    /// Represents the base abstract MongoDb context that initializes and provide
    /// the way to connect to the database.
    /// </summary>
    public abstract class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase database;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoContext"/> class.
        /// </summary>
        /// <param name="options">Options containing the <see cref="MongoSettings"/> configuration.</param>
        public MongoContext(IOptions<MongoSettings> options)
        {
            var mongoConnectionUrl = new MongoUrl(options.Value.Connection);
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);

            if (options.Value.LogQueries)
            {
                mongoClientSettings.ClusterConfigurator = cb =>
                {
                    cb.Subscribe<CommandStartedEvent>(e =>
                    {
                        Console.WriteLine($"Db command: {e.CommandName} - {e.Command.ToJson()}");
                    });
                };
            }

            var mongoClient = new MongoClient(mongoClientSettings);
            this.database = mongoClient.GetDatabase(options.Value.DatabaseName);
        }

        /// <inheritdoc/>
        public IMongoCollection<T> GetCollection<T>()
            where T : BaseEntity
        {
            string collectionName = (typeof(T).GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault() as BsonCollectionAttribute).CollectionName;

            return this.database.GetCollection<T>(collectionName);
        }
    }
}