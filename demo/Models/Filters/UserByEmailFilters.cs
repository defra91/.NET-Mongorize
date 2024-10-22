// <copyright file="UserByEmailFilters.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Demo.Models.Filters;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Mongorize.Demo.Entities;
using Mongorize.Models.Interfaces;

/// <summary>
/// Represents an implementation of the <see cref="IFilter{User}"/> interface
/// for representing the filters for searching a user by email.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UserByEmailFilters"/> class.
/// </remarks>
/// <param name="email">The email value.</param>
public class UserByEmailFilters(string email)
    : IFilter<User>
{
    private readonly string email = email;

    /// <inheritdoc/>
    public List<Expression<Func<User, bool>>> GetExpressions()
    {
        return
        [
            x => x.Email == this.email,
        ];
    }
}