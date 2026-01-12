

namespace Basket.Basket.Features.RemoveItemFromBasket;

public record RemoveItemFromBasketCommand(string UserName, Guid ProductId) 
    : ICommand<RemoveItemFromBasketResult>;

public record RemoveItemFromBasketResult(bool IsSuccess);

public class RemoveItemFromBasketValidator : AbstractValidator<RemoveItemFromBasketCommand>
{
    public RemoveItemFromBasketValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("UserName is required and should not exceed 100 characters.");

        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
    }
}


public class RemovveItemFromBasketHandler(BasketDbContext dbContext)
    : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
{
    public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = dbContext.BasketCarts
            .Include(b => b.Items)
            .FirstOrDefault(b => b.UserName == request.UserName);

        if (basket is null)
        {
            throw new NotFoundException($"Basket for user '{request.UserName}' not found.");
        }

        basket.RemoveItem(request.ProductId);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new RemoveItemFromBasketResult(IsSuccess: true);
    }
}
