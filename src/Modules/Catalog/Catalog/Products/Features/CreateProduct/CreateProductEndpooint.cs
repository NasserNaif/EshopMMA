



namespace Catalog.Products.Features.CreateProduct;

public record CreateProductRequest(ProductDto Product);


public record CreateProoductRespoonse(Guid id);

public class CreateProductEndpooint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            
            var response = new CreateProoductRespoonse(result.id);

            return Results.Created($"/products/{response.id}", response);
        }).WithDescription("Create a new product")
          .WithName("Create Product")
          .Produces<CreateProoductRespoonse>(StatusCodes.Status201Created)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Create Product");
    }
}
