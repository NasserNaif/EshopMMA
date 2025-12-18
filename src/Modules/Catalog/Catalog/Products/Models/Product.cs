


namespace Catalog.Products.Models
{
    // We will use Rich-Domain design. thats mean all the operations on the model it will be in the model class 
    public class Product : Aggregate<Guid>
    {
        public string Name { get;private  set; } = default!;
        public string Description { get; private set; } = default!;

        public List<string> Catagory { get;private  set; } = new();

        public string ImageFile { get;private  set; } = default!;
        public decimal Price { get;private  set; }  

        // Create Operation
        public static Product Create(Guid id, string name, string description,List<string> catacory, string image, decimal price)
        {
            // Check
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);


            var product = new Product()
            {
                Id = id,
                Name = name,
                Description = description,
                Catagory = catacory,
                ImageFile = image,
                Price = price
            };

            product.AddDomainEvent(new ProductCreatedEvent(product));
            return product;
        }

        // Update
        public  void Update(string name, string description, List<string> catacory, string image, decimal price)
        {
            // Check
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);


            // update fields
            Name = name;
            Description = description;
            Catagory = catacory;
            ImageFile = image;
            Price = price;
            
            // : if price has changed raise domain event ( ProsuctPriceChanged )
            if (Price > price)
            {
                Price = price;
                AddDomainEvent(new ProductPriceChangedEvent(this));
            }
        }
    }
}
