


namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product) : ICommand<CreateProoductResult>;


public class CreateProdcutCommandvalidator : AbstractValidator<CreateProductCommand>
{
    public CreateProdcutCommandvalidator()
    {
        RuleFor(x => x.Product).NotNull();
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Product.Catagory).NotEmpty().WithMessage("Catagory is required");
        RuleFor(x => x.Product.Description).MaximumLength(500).WithMessage("Description is required");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

public record CreateProoductResult(Guid id);


internal class CreateProoductHandller(CatalogDbContext dbContext
    , ILogger<CreateProoductHandller> logger)
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
