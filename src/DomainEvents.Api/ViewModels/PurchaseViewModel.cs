using Microsoft.AspNetCore.Mvc;
using System;

namespace DomainEvents.Api.ViewModels
{
    public class PurchaseViewModel
    {
        [FromQuery(Name = "itemId")]
        public int ItemId { get; set; }

        [FromQuery(Name = "itemCost")]
        public double ItemCost { get; set; }

        [FromQuery(Name = "quantity")]
        public int Quantity { get; set; }

        [FromQuery(Name = "storeId")]
        public int StoreId { get; set; }

        public DateTime TimeStamp { get; } = DateTime.UtcNow;
    }
}
