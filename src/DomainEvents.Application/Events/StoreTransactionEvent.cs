using MediatR;
using System;

namespace DomainEvents.Application.Events
{
    public class StoreTransactionEvent : INotification
    {
        public StoreTransactionEvent(int storeId, double transactionAmount, DateTime timeStamp)
        {
            StoreId = storeId;
            TransactionAmount = transactionAmount;
            TimeStamp = timeStamp;
        }

        public int StoreId { get; }

        public double TransactionAmount { get; }

        public DateTime TimeStamp { get; }
    }
}
