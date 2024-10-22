// <copyright file="MongoSettings.cs" company="Luca De Franceschi">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Settings;

/// <summary>
/// Class representing the mongodb connection
/// settings. This class can be fetched from appsettings.json file.
/// </summary>
public class MongoSettings
{
    /// <summary>
    /// Gets or sets the connection string.
    /// </summary>
    public string Connection { get; set; }

    /// <summary>
    /// Gets or sets the database name.
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to log queries or not.
    /// </summary>
    public bool LogQueries { get; set; }
}