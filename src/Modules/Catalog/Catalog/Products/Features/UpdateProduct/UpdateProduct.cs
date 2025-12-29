

namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductCommand(ProductDto Product) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool isSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product).NotNull();
        RuleFor(x => x.Product.Id).NotEmpty().WithMessage("Product ID is required");
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Product.Catagory).NotEmpty().WithMessage("Catagory is required");
        RuleFor(x => x.Product.Description).MaximumLength(500).WithMessage("Description is required");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
internal class UpdateProduct(CatalogDbContext dbContext)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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

        return new UpdateProductResult(isSuccess: true);
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
