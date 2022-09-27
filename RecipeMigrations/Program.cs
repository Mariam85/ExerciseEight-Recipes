using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RecipeMigrations.Migrations;

namespace RecipeMigrations
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices()
        {
            //IConfiguration config = new ConfigurationBuilder()
              //.AddJsonFile("appsettings.json")
               //AddEnvironmentVariables()
               //.Build();
            return new ServiceCollection()
                       // Add common FluentMigrator services
                       .AddFluentMigratorCore()
                       .ConfigureRunner(rb => rb
                       // Add Postgres support to FluentMigrator
                       .AddPostgres()
                       // Set the connection string 
                      .WithGlobalConnectionString("Server=localhost;Database=recipe_app;User ID=postgres;Password=user9507lh;")
                       // Define the assembly containing the migrations.
                      .ScanIn(typeof(_0001_RecipeTable).Assembly).For.Migrations())
                      // Enable logging to console in the FluentMigrator way
                      .AddLogging(lb => lb.AddFluentMigratorConsole())   //for logging
                                                                         // Build the service provider
                      .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}