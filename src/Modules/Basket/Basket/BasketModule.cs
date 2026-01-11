using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Basket
{
    public static class BasketModule
    {
        public static IServiceCollection AddBasketModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            // 1. API Endpoints services

            // 2. Application services

            // 3. Data - Infrastructure services

            // ## add DbContext ##
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            services.AddDbContext<BasketDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

            return services;
        }

        public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
        {

            // 1. Use Api Endpoints services

            // 2. Use Application services

            // 3. Use Data - Infrastructure services

            // ## Automatic database migration ##

            // New way - using shared Extentions
            app.UseMigration<BasketDbContext>();

            return app;
        }
    }
}
