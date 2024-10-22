// <copyright file="Pagination.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Models;

/// <summary>
/// Represents an object responsible of collecting the necessary data to apply the pagination logic.
/// </summary>
public class Pagination
{
    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the number of items per page.
    /// </summary>
    public int ItemsPerPage { get; set; }
}