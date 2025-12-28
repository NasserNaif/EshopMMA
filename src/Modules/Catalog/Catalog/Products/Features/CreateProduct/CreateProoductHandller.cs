


namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product) : ICommand<CreateProoductResult>;

public record CreateProoductResult(Guid id);


internal class CreateProoductHandller(CatalogDbContext dbContext)
    : ICommandHandler<CreateProductCommand, CreateProoductResult>
{
    public async Task<CreateProoductResult> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        // Implement the logic to create a product here.

        // Create new Product from the request.Product data
       
        var newProduct = CreateNewProduct(request.Product);

        // Add the new product to the database context and save changes
        dbContext.Products.Add(newProduct);
        await dbContext.SaveChangesAsync(cancellationToken);


        // Return the response with the new product's ID
        return new CreateProoductResult(newProduct.Id);
    }

    private Product CreateNewProduct(ProductDto product)
    {
        return Product.Create(
            Guid.NewGuid(),
            product.Name,
            product.Description,
            product.Catagory,
            product.ImageFile,
            product.Price
        );
    }
}
