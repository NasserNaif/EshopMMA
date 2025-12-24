


namespace Catalog.Products.EventHandlers;

public class ProductPriceChangedEventHandler(ILogger<ProductPriceChangedEventHandler> logger)
    : INotificationHandler<ProductPriceChangedEvent>
{
    public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Product price changed: {DomainName}, New Price: {NewPrice}",
            notification.Product.Name,
            notification.Product.Price
            );

        return Task.CompletedTask;
    }
}
