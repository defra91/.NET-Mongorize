// <copyright file="IUserService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Demo.Services.Interfaces;

using System.Threading;
using System.Threading.Tasks;
using Mongorize.Demo.Entities;
using Mongorize.Services.Interfaces;

/// <summary>
/// Represents the service layer related to the <see cref="User"/> entity.
/// </summary>
public interface IUSerService : IBaseService<User>
{
    /// <summary>
    /// Find a single <see cref="User"/> with the provided email.
    /// </summary>
    /// <param name="email">The email to use for the search.</param>
    /// <param name="cToken">The cancellation token used to cancel the async operation.</param>
    /// <returns>The task containing the found <see cref="User"/> or null if not found.</returns>
    public Task<User> FindOneByEmailAsync(string email, CancellationToken cToken);
}