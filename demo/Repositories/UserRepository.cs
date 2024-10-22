// <copyright file="UserRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Demo.Repositories;

using Mongorize.Contexts.Interfaces;
using Mongorize.Demo.Entities;
using Mongorize.Demo.Repositories.Interfaces;
using Mongorize.Repositories;

/// <summary>
/// Implementation of the <see cref="IUserRepository"/> interface.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UserRepository"/> class.
/// </remarks>
/// <param name="context">The mongodb context reference.</param>
public class UserRepository(IMongoContext context)
    : BaseRepository<User>(context), IUserRepository
{
}