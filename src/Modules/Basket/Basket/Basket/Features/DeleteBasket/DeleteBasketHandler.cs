
namespace Basket.Basket.Features.DeleteBasket;

public record DeleteBasketCommand(string UserName) 
    : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("UserName is required and should not exceed 100 characters.");
    }
}

public class DeleteBasketHandler(BasketDbContext dbContext)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        
        var basket = await dbContext.BasketCarts
            .SingleOrDefaultAsync(b => b.UserName == request.UserName, cancellationToken);

        if (basket is null)
        {
            throw new NotFoundException($"Basket for user '{request.UserName}' not found.");
        }

        dbContext.BasketCarts.Remove(basket);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteBasketResult(IsSuccess: true);
    }
}
