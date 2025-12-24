namespace Catalog.Products.Features.GetPreoductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResponse>;

public record GetProductByIdResponse(ProductDto Product);

internal class GetProductByIdHandler(CatalogDbContext dbContext)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
        {
            throw new Exception($"Product with Id {request.Id} not found.");
        }

        return new GetProductByIdResponse(product.Adapt<ProductDto>());
    }
}
