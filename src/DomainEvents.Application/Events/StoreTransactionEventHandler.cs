using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DomainEvents.Application.Events
{
    public class StoreTransactionEventHandler : INotificationHandler<StoreTransactionEvent>
    {
        private readonly ILogger<StoreTransactionEventHandler> _logger;

        public StoreTransactionEventHandler(ILogger<StoreTransactionEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(StoreTransactionEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("StoreTransactionEventHandler - called...");
            _logger.LogInformation($"StoreTransactionEventHandler - store {notification.StoreId} made a transaction for £{notification.TransactionAmount} @ {notification.TimeStamp}...");

            return Task.CompletedTask;
        }
    }
}
