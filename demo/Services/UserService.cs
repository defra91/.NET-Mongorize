// <copyright file="UserService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Demo.Services;

using System.Threading;
using System.Threading.Tasks;
using Mongorize.Builders;
using Mongorize.Demo.Entities;
using Mongorize.Demo.Models.Filters;
using Mongorize.Demo.Repositories.Interfaces;
using Mongorize.Demo.Services.Interfaces;
using Mongorize.Models;
using Mongorize.Services;

/// <summary>
/// Represents the implementation of the <see cref="IUSerService"/> interface.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UserService"/> class.
/// </remarks>
/// <param name="repository">The <see cref="IUserRepository"/> reference.</param>
public class UserService(IUserRepository repository)
    : BaseService<User>(repository), IUSerService
{
    /// <inheritdoc />
    public Task<User> FindOneByEmailAsync(string email, CancellationToken cToken)
    {
        QueryOptions<User> options = new QueryOptionsBuilder<User>()
            .WithFilters(new UserByEmailFilters(email))
            .Build();

        return this.Repository.FindOneAsync(options, cToken);
    }
}