


namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery(PaginationRequest Request) : IQuery<GetProductsResult>;

public record GetProductsResult(PaginationResult<ProductDto> Products);

internal class GetProductsHanlder(CatalogDbContext dbContext)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {

        var pageIndex = request.Request.PageIndex;
        var pageSize = request.Request.PageSize;

        var totalItems = await dbContext.Products.CountAsync(cancellationToken);

        var products = await dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();


        var prosuctsDto = products.Adapt<List<ProductDto>>();

        return new GetProductsResult(
            new PaginationResult<ProductDto>(
                pageIndex, 
                pageSize, 
                totalItems, 
                prosuctsDto)
            );
    }
}

