// <copyright file="QueryFiltersOptionsBuilder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Builders
{
    using Mongorize.Entities;
    using Mongorize.Models;
    using Mongorize.Models.Interfaces;

    /// <summary>
    /// Represents the builder object used for building <see cref="QueryFiltersOptions{TEntity}"/> objects.
    /// </summary>
    /// <typeparam name="TEntity">The type to work with.</typeparam>
    /// <typeparam name="TQueryOptions">The query options type to be used.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="QueryFiltersOptionsBuilder{TEntity, TQueryOptions}"/> class.
    /// </remarks>
    /// <param name="val">The value to be set.</param>
    public class QueryFiltersOptionsBuilder<TEntity, TQueryOptions>(TQueryOptions val)
        where TEntity : BaseEntity
        where TQueryOptions : QueryFiltersOptions<TEntity>
    {
        /// <summary>
        /// Gets or sets the query options object.
        /// </summary>
        protected TQueryOptions QueryOptions { get; set; } = val;

        /// <summary>
        /// Applies filtering conditions to the query based on the provided filter object.
        /// </summary>
        /// <param name="filters">An instance of <see cref="IFilter{TEntity}"/> to filter the results.</param>
        /// <returns>The current <see cref="QueryOptionsBuilder{TEntity}"/> instance for method chaining.</returns>
        public QueryFiltersOptionsBuilder<TEntity, TQueryOptions> WithFilters(IFilter<TEntity> filters)
        {
            this.QueryOptions.Filters = filters;
            return this;
        }

        /// <summary>
        /// Builds and returns the configured <see cref="QueryOptions{TEntity}"/> object
        /// with all the specified query parameters.
        /// </summary>
        /// <returns>A <see cref="QueryOptions{TEntity}"/> instance containing the query options.</returns>
        public TQueryOptions Build()
            => this.QueryOptions;
    }
}