namespace Catalog.Products.Features.GetProductsByCatagory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResponse>;

public record GetProductsByCategoryResponse(IEnumerable<ProductDto> Products);

internal class GetProductsByCategory(CatalogDbContext dbContext)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResponse>
{
    public async Task<GetProductsByCategoryResponse> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .AsNoTracking()
            .Where(p => p.Catagory.Contains(request.Category))
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        return new GetProductsByCategoryResponse(products.Adapt<List<ProductDto>>());
    }
}
