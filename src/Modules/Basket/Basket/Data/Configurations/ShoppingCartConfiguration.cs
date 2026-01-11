

namespace Catalog.Data.Configurations;

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.UserName).IsUnique();

        builder.Property(p => p.UserName)
            .IsRequired()
            .HasMaxLength(100);


        // Configure the relationship between ShoppingCart and ShoppingCartItem
        builder.HasMany(p => p.Items)
            .WithOne()
            .HasForeignKey(p => p.ShoppingCartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
