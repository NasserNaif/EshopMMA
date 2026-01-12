

using Basket.Basket.Dtos;

namespace Basket.Basket.Features.CreateBasket;

public record CreateBasketCommand(ShhoppingCartDto Dto) 
    : ICommand<CreateBasketResult>;

public record CreateBasketResult(Guid Id);

public class CreateBasketValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketValidator()
    {
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.UserName)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("UserName is required and should not exceed 100 characters.");
    }
}

public class CreateBasketHandler(BasketDbContext dbContext)
    : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    public async Task<CreateBasketResult> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        var newBasket = CreateNewShoppingCart(request.Dto);

        await dbContext.BasketCarts.AddAsync(newBasket, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateBasketResult(newBasket.Id);
    }

    private ShoppingCart CreateNewShoppingCart(ShhoppingCartDto dto)
    {
       var newBasket = ShoppingCart.Create(
            Guid.NewGuid(),
            dto.UserName
            );

        dto.Items.ForEach(item =>
        {
            newBasket.AddItem(
                item.ProductId,
                item.Quantity,
                item.Price,
                item.ProductName,
                item.Color
                );
        });

        return newBasket;
    }
}
