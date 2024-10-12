// <copyright file="IMongoContext.cs" company="Luca De Franceschi">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Contexts.Interfaces
{
    using MongoDB.Driver;
    using Mongorize.Entities;

    /// <summary>
    /// Represents the interface for working with mongodb context.
    /// </summary>
    public interface IMongoContext
    {
        /// <summary>
        /// Returns the collection related to the provided type param.
        /// </summary>
        /// <typeparam name="T">The type to work with that should be of type <see cref="BaseEntity"/>.</typeparam>
        /// <returns>The <see cref="IMongoCollection{T}"/>.</returns>
        public IMongoCollection<T> GetCollection<T>()
            where T : BaseEntity;
    }
}