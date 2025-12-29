


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

        // Validation of the request
        // Manual validation handling (uncomment if needed)

        //var validationResult = await validator.ValidateAsync(request, cancellationToken);


        //if (!validationResult.IsValid)
        //{
        //    logger.LogWarning("CreateProductCommand validation failed: {Errors}",
        //        validationResult.Errors.Select(e => e.ErrorMessage));
        //    throw new ValidationException(validationResult.Errors);
        //}


        // Logging the creation attempt
        logger.LogInformation("CreateProoductHandller.handle called with: {ProductName}", request);


        // start acual logic to create product

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
