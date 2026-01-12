namespace Basket.Basket.Features.AddItemToBasket;

public record AddItemToBasketCommand(string Username, ShoppingCartItemDto Item) 
    : ICommand<AddItemToBasketResult>;

public record AddItemToBasketResult(bool IsSuccess);

public class AddItemToBasketValidator : AbstractValidator<AddItemToBasketCommand>
{
    public AddItemToBasketValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("UserName is required and should not exceed 100 characters.");

        RuleFor(x => x.Item).NotNull();
        RuleFor(x => x.Item.ProductId).NotEmpty().WithMessage("ProductId is required.");
        RuleFor(x => x.Item.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }

}
public class AddItemToBasketHandler(BasketDbContext dbContext)
    : ICommandHandler<AddItemToBasketCommand, AddItemToBasketResult>
{
    public async Task<AddItemToBasketResult> Handle(AddItemToBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await dbContext.BasketCarts
            .Include(b => b.Items)
            .SingleOrDefaultAsync(b => b.UserName == request.Username, cancellationToken);

        if (basket is null)
        {
            throw new NotFoundException($"Basket for user '{request.Username}' not found.");
        }

        basket.AddItem(
            request.Item.ProductId,
            request.Item.Quantity,
            request.Item.Price,
            request.Item.ProductName,
            request.Item.Color);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new AddItemToBasketResult(IsSuccess: true);

    }

   
}
