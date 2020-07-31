using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DomainEvents.Application.Events
{
    public class ItemPurchasedEventHandler : INotificationHandler<ItemPurchasedEvent>
    {
        private readonly ILogger<ItemPurchasedEventHandler> _logger;

        public ItemPurchasedEventHandler(ILogger<ItemPurchasedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ItemPurchasedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ItemPurchasedEventHandler - called...");
            _logger.LogInformation($"ItemPurchasedEventHandler - user purchased {notification.Quantity}x {notification.ItemId} item @ {notification.TimeStamp}...");

            return Task.CompletedTask;
        }
    }
}
