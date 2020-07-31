using MediatR;
using System;

namespace DomainEvents.Application.Commands
{
    public class MakePurchaseCommand : IRequest<string>
    {
        public MakePurchaseCommand(int itemId,
            double itemCost,
            int quantity,
            int storeId,
            DateTime timeStamp)
        {
            ItemId = itemId;
            ItemCost = itemCost;
            Quantity = quantity;
            StoreId = storeId;
            TimeStamp = timeStamp;
        }

        public int ItemId { get; }

        public double ItemCost { get; }

        public int Quantity { get; }

        public int StoreId { get; }

        public DateTime TimeStamp { get; }

        public double TransactionAmount => ItemCost * Quantity;
    }
}
