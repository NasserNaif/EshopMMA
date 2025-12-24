using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Seed;

namespace Shared.Data;

public static class Extentions
{
    public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder app)
        where TContext : DbContext
    {
        // Run the migration synchronously
        MigrationDatabaseAsync<TContext>(app).GetAwaiter().GetResult();

        // Seed data if needed
        SeedDataAsync(app.ApplicationServices).GetAwaiter().GetResult();
        return app;
    }

 
    private static async Task MigrationDatabaseAsync<TContext>(IApplicationBuilder app)
        where TContext : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        await context.Database.MigrateAsync();
    }

 
    private static async Task SeedDataAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var seder = scope.ServiceProvider.GetServices<IDataSeeder>();

        foreach (var seeding in seder)
        {
            await seeding.SeedAllAsync();
        }
    }
}
