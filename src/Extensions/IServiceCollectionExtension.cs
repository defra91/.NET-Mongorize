// <copyright file="IServiceCollectionExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using Mongorize.Contexts;
using Mongorize.Contexts.Interfaces;
using Mongorize.Settings;

/// <summary>
/// Represents a static class for collecting the Mongorize extension methods
/// for the <see cref="IServiceCollection"/> interface.
/// </summary>
public static class IServiceCollectionExtension
{
    /// <summary>
    /// Add the mongorize features to the <see cref="IServiceCollection"/> object.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/> reference.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> object reference.</param>
    /// <returns>The extended <see cref="IServiceCollection"/> object.</returns>
    public static IServiceCollection AddMongorize(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var mongoSettingsSection = configuration.GetSection("MongoSettings");
        serviceCollection.Configure<MongoSettings>(mongoSettingsSection);

        // Use lower camel case convention registry of MongoDb.
        var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("camelCase", conventionPack, t => true);

        // Add the MongoContext to the service collection.
        serviceCollection.AddTransient<IMongoContext, MongoContext>();

        return serviceCollection;
    }
}