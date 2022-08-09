using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GarconDomain.Models.Constants;

namespace GarconDomain.ViewModels
{
    public class OrderItemTimeStatusViewModel
    {
        public int OrderId { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
        public List<OrderItemTimeStatus> OrderItemTimeStatuses { get; set; }
    }
    public class OrderItemTimeStatus
    {
        public int OrderItemId { get; set; }
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public int OrderItemStatusId { get; set; }
        public string OrderItemStatusName { get; private set; }
        public int MinutesLeft { get; set; }
        public int Quantity { get; set; }

        public void SetOrderItemStatusName() => OrderItemStatusName = OrderItemStatusMap[OrderItemStatusId];
}
}
