


using Catalog.Products.Features.CreateProduct;

namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductRequest(ProductDto Product);

public record UpdateProoductRespoonse(bool isSuccess);
internal class UpdateProductEndpointm : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();
            var result = await sender.Send(command);

            // didn't work
            //var response = result.Adapt<UpdateProoductRespoonse>();


            var response = new UpdateProoductRespoonse(result.isSuccess);

            return Results.Ok(response);
        }).WithDescription("Update a product")
          .WithName("Update Product")
          .Produces<CreateProoductRespoonse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .ProducesProblem(StatusCodes.Status404NotFound)
          .WithSummary("Update Product");
    }
}
