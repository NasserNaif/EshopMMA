
namespace Basket.Basket.Models;

public class ShoppingCartItem : BaseEntity<Guid>
{
    public Guid ProductId { get; private set; } = default!;
    public Guid ShoppingCartId { get; private set; } = default!;
    public int Quantity { get; internal set; } = default!;

    public string Color { get; private set; } = default!;


    // will comes from Catalog module

    public decimal Price { get; private set; } = default!;
    public string ProductName { get; set; } = default!;

    internal ShoppingCartItem(Guid productId,Guid shoppingCartId, int quantity, decimal price, string productName, string color)
    {
        ProductId = productId;
        ShoppingCartId = shoppingCartId;
        Quantity = quantity;
        Price = price;
        ProductName = productName;
        Color = color;
    }
}
