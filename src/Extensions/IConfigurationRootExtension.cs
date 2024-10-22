// <copyright file="IConfigurationRootExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Extensions;

using Microsoft.Extensions.Configuration;
using Mongorize.Settings;

/// <summary>
/// Represents an extension class for the <see cref="IConfigurationRoot"/> interface.
/// </summary>
public static class IConfigurationRootExtension
{
    /// <summary>
    /// Extend the IConfigurationRoot interface by adding a method to retrieve
    /// an instance of the <see cref="MongoSettings"/> class.
    /// </summary>
    /// <param name="config">The <see cref="IConfigurationRoot"/> reference.</param>
    /// <returns>The fetched <see cref="MongoSettings"/> object.</returns>
    public static MongoSettings GetMongoSettings(this IConfigurationRoot config)
    {
        return config.GetSection("MongoSettings").Get<MongoSettings>();
    }
}