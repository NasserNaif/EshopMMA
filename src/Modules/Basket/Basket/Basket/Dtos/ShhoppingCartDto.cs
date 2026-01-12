
namespace Basket.Basket.Dtos;

public record ShhoppingCartDto(
    Guid Id,
    string UserName,
    List<ShoppingCartItemDto> Items
);
