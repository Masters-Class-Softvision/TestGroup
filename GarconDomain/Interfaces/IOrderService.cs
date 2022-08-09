using GarconDomain.Models;
using GarconDomain.ViewModels;
using System.Collections.Generic;

namespace GarconDomain.Interfaces
{
    public interface IOrderService
    {
        int CreateOrder(Order order);
        int CreateDraftOrder();
        bool UpdateDraftOrder(OrderItemViewModel rec);
        Order GetOrder(int orderId);
        OrderDetailViewModel GetOrderDetailSummary(int orderId);
        int GetDraftOrderId();
        bool UpdateOrderStatus(int orderId, int orderStatus);
        List<Order> GetOrders();
        List<OrderItemTimeStatusViewModel> CheckOrderTimeStatuses();
        int ProceedBill(int orderId);
    }
}
