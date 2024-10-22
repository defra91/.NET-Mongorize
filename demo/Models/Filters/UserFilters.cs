// <copyright file="UserFilters.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Demo.Models.Filters;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Mongorize.Demo.Entities;
using Mongorize.Models.Interfaces;

/// <summary>
/// Represents the filters related to the <see cref="User"/> entity.
/// </summary>
public class UserFilters : IFilter<User>
{
    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    public string Email { get; set; }

    /// <inheritdoc />
    public List<Expression<Func<User, bool>>> GetExpressions()
    {
        var result = new List<Expression<Func<User, bool>>>();

        if (!string.IsNullOrEmpty(this.FirstName))
        {
            result.Add(x => x.FirstName.ToLower().Trim().Contains(this.FirstName.ToLower().Trim()));
        }

        if (!string.IsNullOrEmpty(this.LastName))
        {
            result.Add(x => x.LastName.ToLower().Trim().Contains(this.LastName.ToLower().Trim()));
        }

        if (!string.IsNullOrEmpty(this.Email))
        {
            result.Add(x => x.Email.ToLower().Trim().Contains(this.Email.ToLower().Trim()));
        }

        return result;
    }
}