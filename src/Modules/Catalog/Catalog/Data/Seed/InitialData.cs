
namespace Catalog.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Product> Products =>
        new List<Product>
        {
            // Create some initial products with this parameters: Guid id, string name, string description,List<string> catacory, string image, decimal price
            Product.Create(Guid.NewGuid(), "Product 1", "Description for Product 1", new List<string>{"Category1", "Category2"}, "image1.jpg", 9.99m),
            Product.Create(Guid.NewGuid(), "Product 2", "Description for Product 2", new List<string>{"Category2", "Category3"}, "image2.jpg", 19.99m),
            Product.Create(Guid.NewGuid(), "Product 3", "Description for Product 3", new List<string>{"Category1"}, "image3.jpg", 29.99m),
            Product.Create(Guid.NewGuid(), "Product 4", "Description for Product 4", new List<string>{"Category3"}, "image4.jpg", 39.99m)
        };
}
