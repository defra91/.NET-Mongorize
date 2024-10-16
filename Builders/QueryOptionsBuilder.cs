// <copyright file="QueryOptionsBuilder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Builders
{
    using System.Linq.Expressions;
    using Mongorize.Entities;
    using Mongorize.Models;
    using Mongorize.Models.Enums;
    using Mongorize.Models.Interfaces;

    /// <summary>
    /// Represents the builder object used for building <see cref="QueryOptions{TEntity}"/> objects.
    /// </summary>
    /// <typeparam name="TEntity">The type to work with.</typeparam>
    public class QueryOptionsBuilder<TEntity>
        where TEntity : BaseEntity
    {
        private readonly QueryOptions<TEntity> queryOptions = new QueryOptions<TEntity>();

        /// <summary>
        /// Specify the pagination parameters for the query.
        /// </summary>
        /// <param name="pageNumber">The number of the page to retrieve.</param>
        /// <param name="itemsPerPage">The number of elements per page.</param>
        /// <returns>The current <see cref="QueryOptionsBuilder{TEntity}"/> instance for method chaining.</returns>
        public QueryOptionsBuilder<TEntity> WithPagination(int pageNumber, int itemsPerPage)
        {
            this.queryOptions.Pagination = new Pagination { Page = pageNumber, ItemsPerPage = itemsPerPage };
            return this;
        }

        /// <summary>
        /// Applies filtering conditions to the query based on the provided filter object.
        /// </summary>
        /// <param name="filters">An instance of <see cref="IFilter{TEntity}"/> to filter the results.</param>
        /// <returns>The current <see cref="QueryOptionsBuilder{TEntity}"/> instance for method chaining.</returns>
        public QueryOptionsBuilder<TEntity> WithFilters(IFilter<TEntity> filters)
        {
            this.queryOptions.Filters = filters;
            return this;
        }

        /// <summary>
        /// Includes specified fields in the query projection.
        /// </summary>
        /// <param name="includeProjection">An expression selecting the field to include in the result.</param>
        /// <returns>The current <see cref="QueryOptionsBuilder{TEntity}"/> instance for method chaining.</returns>
        public QueryOptionsBuilder<TEntity> Include(Expression<Func<TEntity, object>> includeProjection)
        {
            this.queryOptions.IncludeProjections.Add(includeProjection);
            return this;
        }

        /// <summary>
        /// Excludes specified fields from the query projection.
        /// </summary>
        /// <param name="excludeProjection">An expression selecting the field to exclude from the result.</param>
        /// <returns>The current <see cref="QueryOptionsBuilder{TEntity}"/> instance for method chaining.</returns>
        public QueryOptionsBuilder<TEntity> Exclude(Expression<Func<TEntity, object>> excludeProjection)
        {
            this.queryOptions.ExcludeProjections.Add(excludeProjection);
            return this;
        }

        /// <summary>
        /// Specifies sorting parameters for the query.
        /// </summary>
        /// <param name="sortBy">An expression selecting the field to sort by.</param>
        /// <param name="direction">The direction of the sort result.</param>
        /// <returns>The current <see cref="QueryOptionsBuilder{TEntity}"/> instance for method chaining.</returns>
        public QueryOptionsBuilder<TEntity> WithSorting(Expression<Func<TEntity, object>> sortBy, ESortByDirection direction)
        {
            this.queryOptions.SortCriteria.Add((sortBy, direction));
            return this;
        }

        /// <summary>
        /// Builds and returns the configured <see cref="QueryOptions{TEntity}"/> object
        /// with all the specified query parameters.
        /// </summary>
        /// <returns>A <see cref="QueryOptions{TEntity}"/> instance containing the query options.</returns>
        public QueryOptions<TEntity> Build()
            => this.queryOptions;
    }
}