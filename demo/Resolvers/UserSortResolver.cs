// <copyright file="UserSortResolver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Demo.Resolvers;

using Mongorize.Demo.Entities;
using Mongorize.Resolvers;

/// <summary>
/// Represents the sort resolver related to the <see cref="User"/> entity.
/// </summary>
public class UserSortResolver : BaseSortResolver<User>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserSortResolver"/> class.
    /// </summary>
    public UserSortResolver()
    {
        this.SortDictionary.Add("firstName", x => x.FirstName);
        this.SortDictionary.Add("lastName", x => x.LastName);
        this.SortDictionary.Add("email", x => x.Email);
    }
}