

namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductCommand(ProductDto Product) : ICommand<UpdateProductReasponse>;

public record UpdateProductReasponse(Guid id);
internal class UpdateProduct(CatalogDbContext dbContext)
    : ICommandHandler<UpdateProductCommand, UpdateProductReasponse>
{
    public async Task<UpdateProductReasponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == request.Product.Id, cancellationToken);

        if (product == null)
        {
            throw new Exception($"Product with ID {request.Product.Id} not found.");
        }

        UpdateProductWithNewValues(product, request.Product);

        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateProductReasponse(product.Id);
    }

    private void UpdateProductWithNewValues(Product product1, ProductDto product2)
    {
        product1.Update(
            product2.Name,
            product2.Description,
            product2.Catagory,
            product2.ImageFile,
            product2.Price);
    }
}
