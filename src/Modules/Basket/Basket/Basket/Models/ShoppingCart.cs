
namespace Basket.Basket.Models;

public class ShoppingCart : Aggregate<Guid>
{
    public string UserName { get; set; } = default!;

    private readonly List<ShoppingCartItem> _items = new();

    public IReadOnlyCollection<ShoppingCartItem> Items => _items.AsReadOnly();

    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);


    // Methods


    // Create New ShoppingCart
    public static ShoppingCart Create(Guid id, string userName)
    {
        ArgumentException.ThrowIfNullOrEmpty(userName);

        var cart = new ShoppingCart
        {
            Id = id,
            UserName = userName
        };
        return cart;
    }


    // Add Item to ShoppingCart
    public void AddItem(Guid productId, int quantity, decimal price, string productName, string color)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var existingItem = _items.FirstOrDefault(i => i.ProductId == productId && i.Color == color);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            var newItem = new ShoppingCartItem(productId, this.Id, quantity, price, productName, color);
            _items.Add(newItem);
        }
    }


    // Remove Item from ShoppingCart
    public void RemoveItem(Guid productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            _items.Remove(item);
        }
    }
}
