using MediatR;
using System;

namespace DomainEvents.Application.Events
{
    public class ItemPurchasedEvent : INotification
    {
        public ItemPurchasedEvent(int itemId, int quantity, DateTime timeStamp)
        {
            ItemId = itemId;
            Quantity = quantity;
            TimeStamp = timeStamp;
        }

        public int ItemId { get; }

        public int Quantity { get; }

        public DateTime TimeStamp { get; }
    }
}
