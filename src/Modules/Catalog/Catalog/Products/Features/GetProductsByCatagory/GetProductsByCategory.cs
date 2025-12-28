namespace Catalog.Products.Features.GetProductsByCatagory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

public record GetProductsByCategoryResult(IEnumerable<ProductDto> Products);

internal class GetProductsByCategory(CatalogDbContext dbContext)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .AsNoTracking()
            .Where(p => p.Catagory.Contains(request.Category))
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        return new GetProductsByCategoryResult(products.Adapt<List<ProductDto>>());
    }
}
