// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Demo;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mongorize.Demo.Models.Filters;
using Mongorize.Demo.Repositories;
using Mongorize.Demo.Repositories.Interfaces;
using Mongorize.Demo.Services;
using Mongorize.Demo.Services.Interfaces;
using Mongorize.Extensions;

/// <summary>
/// Represents the main program class.
/// </summary>
internal class Program
{
    private static async Task Main(string[] args)
    {
        IServiceCollection serviceCollection = new ServiceCollection();

        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        ConfigureServices(serviceCollection, config);

        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        IUSerService userService = serviceProvider.GetService<IUSerService>();

        using CancellationTokenSource cts = new CancellationTokenSource();

        Console.CancelKeyPress += (sender, e) =>
        {
            Console.WriteLine("Cancellation requested...");
            cts.Cancel(); // Annulla il token
            e.Cancel = true; // Evita la chiusura immediata della console
        };

        await userService.CreateAsync(
            new Entities.User
            {
                FirstName = "Mario",
                LastName = "Rossi",
                Email = "mario.rossi@test.com",
            },
            cts.Token);

        await userService.GetListAsync(
            new Mongorize.Models.Pagination(),
            new UserFilters { FirstName = "Mario" },
            sortBy: default,
            cts.Token);
    }

    /// <summary>
    /// Configure the services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> reference.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> object.</param>
    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Add the mongorize dependencies.
        services.AddMongorize(configuration);

        // Add repositories
        services.AddTransient<IUserRepository, UserRepository>();

        // Add services
        services.AddTransient<IUSerService, UserService>();
    }
}