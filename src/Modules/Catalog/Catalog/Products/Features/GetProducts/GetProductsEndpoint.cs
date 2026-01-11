

namespace Catalog.Products.Features.GetProducts;

//public record GetProductsRequest();

public record GetProductsResponse(PaginationResult<ProductDto> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            
            var result = await sender.Send(new GetProductsQuery(request));
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);

        }).WithDescription("Get all products")
          .WithName("Get Products")
          .Produces<GetProductsResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Get Products");
    }
}
