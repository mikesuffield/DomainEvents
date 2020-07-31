using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DomainEvents.Application.Events
{
    public class WarehouseStockProcessor : INotificationHandler<ItemPurchasedEvent>
    {
        private readonly ILogger<WarehouseStockProcessor> _logger;

        public WarehouseStockProcessor(ILogger<WarehouseStockProcessor> logger)
        {
            _logger = logger;
        }

        public Task Handle(ItemPurchasedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("WarehouseStockProcessor - called...");
            _logger.LogInformation($"WarehouseStockProcessor - ItemPurchasedEvent additional processing...");

            return Task.CompletedTask;
        }
    }
}
