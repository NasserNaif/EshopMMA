
using Catalog.Products.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.Data;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // set default schema for the catalog module
        builder.HasDefaultSchema("catalog");
        // aply all configurations from the current assembly
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}


// ## EF Migrations Commands ##

// To create the initial migration, run the following command in the Package Manager Console:
// 1: PM> Add-Migration InitialCreate -OutputDir Data/Migrations -Project Catalog -StartupProject api

// To apply the migration and create the database schema, run:
// 2: PM> Update-Database -Project Catalog -StartupProject api
