// <copyright file="MongoContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Contexts
{
    using MongoDB.Driver;
    using Mongorize.Entities;

    /// <summary>
    /// Represents the base abstract MongoDb context that initializes and provide
    /// the way to connect to the database.
    /// </summary>
    public abstract class MongoContext
    {
        private readonly IMongoDatabase database;
        private readonly IMongoClient mongoClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoContext"/> class.
        /// </summary>
        /// <param name="connectionString">The mongodb connection string.</param>
        /// <param name="databaseName">The name of the database.</param>
        public MongoContext(string connectionString, string databaseName)
        {
            this.mongoClient = new MongoClient(connectionString);
            if (this.mongoClient != null)
            {
                this.database = this.mongoClient.GetDatabase(databaseName);
            }
        }

        /// <summary>
        /// Given a collection name returns the collection related to the parameter entity.
        /// </summary>
        /// <typeparam name="T">The type to work with that should be of type <see cref="BaseEntity"/>.</typeparam>
        /// <param name="collectionName">The name of the collection.</param>
        /// <returns>The <see cref="IMongoCollection{T}"/>.</returns>
        public IMongoCollection<T> GetCollection<T>(string collectionName)
            where T : BaseEntity
        {
            return this.database.GetCollection<T>(collectionName);
        }
    }
}