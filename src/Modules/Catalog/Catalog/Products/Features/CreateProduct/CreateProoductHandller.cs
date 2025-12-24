


namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product) : ICommand<CreateProoductReasponse>;

public record CreateProoductReasponse(Guid id);


public class CreateProoductHandller(CatalogDbContext dbContext)
    : ICommandHandler<CreateProductCommand, CreateProoductReasponse>
{
    public async Task<CreateProoductReasponse> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        // Implement the logic to create a product here.

        // Create new Product from the request.Product data
       
        var newProduct = CreateNewProduct(request.Product);

        // Add the new product to the database context and save changes
        dbContext.Products.Add(newProduct);
        await dbContext.SaveChangesAsync(cancellationToken);


        // Return the response with the new product's ID
        return new CreateProoductReasponse(newProduct.Id);
    }

    private Product CreateNewProduct(ProductDto product)
    {
        return Product.Create(
            product.Id,
            product.Name,
            product.Description,
            product.Catagory,
            product.ImageFile,
            product.Price
        );
    }
}
