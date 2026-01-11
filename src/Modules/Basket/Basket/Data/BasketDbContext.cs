
namespace Basket.Data;

public class BasketDbContext : DbContext
{

    public BasketDbContext(DbContextOptions<BasketDbContext> options)
        : base(options) { }


    public DbSet<ShoppingCart> BasketCarts => Set<ShoppingCart>();

    public DbSet<ShoppingCartItem> BasketItems => Set<ShoppingCartItem>();

    override protected void OnModelCreating(ModelBuilder builder)
    {
        // set default schema for the basket module
        builder.HasDefaultSchema("basket");
        // aply all configurations from the current assembly
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}


// ## EF Migrations Commands ##

// To create the initial migration, run the following command in the Package Manager Console:
// 1: PM> Add-Migration InitialCreate -OutputDir Data/Migrations -Project Basket -StartupProject api -Context BasketDbContext

// To apply the migration and create the database schema, run:
// 2: PM> Update-Database -Context BasketDbContext