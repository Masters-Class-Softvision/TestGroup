using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GarconDomain.Models.Constants;

namespace GarconDomain.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int OrderItemStatusId { get; private set; }
        public string OrderItemStatusName { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual MenuItem MenuItem { get; set; }

        public void SetOrderItemToQueued() => OrderItemStatusId = (int)OrderItemStatus.Queued;
        public void SetOrderItemToBeingPrepared() => OrderItemStatusId = (int)OrderItemStatus.BeingPrepared;
        public void SetOrderItemToBeingCooked() => OrderItemStatusId = (int)OrderItemStatus.BeingCooked;
        public void SetOrderItemToReadyToServe() => OrderItemStatusId = (int)OrderItemStatus.BeingServed;
        public void SetOrderItemToServed() => OrderItemStatusId = (int)OrderItemStatus.Served;
    }
}
