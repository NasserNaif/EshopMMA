
namespace Catalog.Products.DTO;

public record ProductDto
(
    Guid Id,
    string Name,
    string Description,
    List<string> Catagory,
    string ImageFile,
    decimal Price
);
