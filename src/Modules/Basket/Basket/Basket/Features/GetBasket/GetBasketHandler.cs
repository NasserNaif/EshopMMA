


namespace Basket.Basket.Features.GetBasket;

public record GetBasketQuery(string UserName) 
    : IQuery<GetBasketResult>;

public record GetBasketResult(ShhoppingCartDto Basket);

public class GetBasketValidator : AbstractValidator<GetBasketQuery>
{
    public GetBasketValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("UserName is required and should not exceed 100 characters.");
    }
}

public class GetBasketHandler(BasketDbContext dbContext)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basket = await dbContext.BasketCarts
            .AsNoTracking()
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.UserName == request.UserName, cancellationToken);

        if (basket is null)
        {
            throw new NotFoundException($"Basket for user '{request.UserName}' not found.");
        }

        // Map to DTO
        var basketDto = basket.Adapt<ShhoppingCartDto>();

        return new GetBasketResult(basketDto);
    }
}
