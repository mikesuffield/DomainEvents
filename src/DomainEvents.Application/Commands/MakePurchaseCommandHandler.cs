using AutoMapper;
using DomainEvents.Application.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DomainEvents.Application.Commands
{
    public class MakePurchaseCommandHandler : IRequestHandler<MakePurchaseCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<MakePurchaseCommandHandler> _logger;

        public MakePurchaseCommandHandler(IMediator mediator,
            IMapper mapper,
            ILogger<MakePurchaseCommandHandler> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        public Task<string> Handle(MakePurchaseCommand purchase, CancellationToken cancellationToken)
        {
            _logger.LogInformation("MakePurchaseCommandHandler - called...");

            // Raise StoreTransactionEvent - will be handled by the finance system to log store balances etc
            var storeTransactionEvent = _mapper.Map<StoreTransactionEvent>(purchase);
            _mediator.Publish(storeTransactionEvent);

            // Raise ItemPurchasedEvent - will be handled by the warehouse system to ammend stock quantities etc
            var itemPurchasedEvent = _mapper.Map<ItemPurchasedEvent>(purchase);
            _mediator.Publish(itemPurchasedEvent);

            return Task.FromResult("Thanks for your purchase. We are processing your order.");
        }
    }
}
