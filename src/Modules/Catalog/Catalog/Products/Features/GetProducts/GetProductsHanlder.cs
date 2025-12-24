


namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResponse>;

public record GetProductsResponse(IEnumerable<ProductDto> Products);

public class GetProductsHanlder(CatalogDbContext dbContext)
    : IQueryHandler<GetProductsQuery, GetProductsResponse>
{
    public async Task<GetProductsResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync();


        var prosuctsDto = products.Adapt<List<ProductDto>>();

        return new GetProductsResponse(prosuctsDto);
    }
}

