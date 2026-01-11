using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Behaviors;


namespace Catalog;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        // add module services here

        // - API services

        // - Application services

        // Add MediatR services

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            // add pipeline behaviors for MediatR to Validate requests
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));

            // add pipeline behaviors for MediatR to Log requests
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        // add fluent validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // - Data - Infrastructure services

        // ## add DbContext ##
        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddDbContext<CatalogDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });


        // Register the data seeder
        services.AddScoped<IDataSeeder, CataloogDataSeeder>();

        // ## End DbContext ##

        return services;
    }


    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        // configure module middlewares and pipelines here

        // - API middlewares

        // - Application middlewares

        // - Data - Infrastructure middlewares

        // ## Automatic database migration ##

        // Old way
        //IntialiaseDatabaseAsync(app).GetAwaiter().GetResult();

        // New way - using shared Extentions
        app.UseMigration<CatalogDbContext>();

        // ## End Automatic database migration ##

        return app;
    }


    // Old way
 

    //private static async Task IntialiaseDatabaseAsync(IApplicationBuilder app)
    //{
    //    using var scope = app.ApplicationServices.CreateScope();

    //    var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

    //    await context.Database.MigrateAsync();
    //}
}


