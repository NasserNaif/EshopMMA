


using Catalog.Products.Exceptions;
using MediatR;

namespace Catalog.Products.Features.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProoductResult>;

public record DeleteProoductResult(bool isSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
    }
}
internal class DeleteProductHandler(CatalogDbContext dbContext)
        : ICommandHandler<DeleteProductCommand, DeleteProoductResult>
{
    public async Task<DeleteProoductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);


        return new DeleteProoductResult(isSuccess: true);
    }
}
