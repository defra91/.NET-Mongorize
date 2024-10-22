// <copyright file="IUserRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Demo.Repositories.Interfaces;

using Mongorize.Demo.Entities;
using Mongorize.Repositories.Interfaces;

/// <summary>
/// Represents the repository interface for the <see cref="User"/> entity.
/// </summary>
public interface IUserRepository : IRepository<User>
{
}