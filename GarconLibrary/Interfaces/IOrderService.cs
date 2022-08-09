using GarconLibrary.Models;
using System.Collections.Generic;

namespace GarconLibrary.Interfaces
{
    public interface IOrderService
    {
        int CreateOrder(OrderDto order);
        OrderDto GetOrder(int orderId);
        List<OrderDto> GetOrders();
        int AddOrderItem(OrderItemDto orderItemDto);
        void RemoveOrderItem(int orderItemId);
        bool UpdateOrderItemQuantity(int orderItemId, int quantity);
        int GetDraftOrderId(int draftOrderStatusId);
        void UpdateOrderItemStatus(int orderItemId, int orderItemStatusId);
        bool UpdateOrderStatus(int orderId, int orderStatusId);
        int CreateBill(BillDto billDto);
        void UpdateOrderBillId(int orderId, int billId);
    }
}
