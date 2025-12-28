


namespace Catalog.Products.Features.DeleteProduct;

public record DeleteProoductResponse(bool isSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {
            
            var result = await sender.Send(new DeleteProductCommand(id));
            var response = result.Adapt<DeleteProoductResponse>();
            return Results.Ok(response);
        }).WithDescription("Delete a product")
          .WithName("Delete Product")
          .Produces<DeleteProoductResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .ProducesProblem(StatusCodes.Status404NotFound)
          .WithSummary("Delete Product");
    }
}
