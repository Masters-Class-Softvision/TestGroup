using GarconDomain.Helpers;
using GarconDomain.Interfaces;
using GarconDomain.Models;
using GarconDomain.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GarconWeb.Controllers
{
    public class OrderController : Controller
    {
        readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UpdateDraftOrder(OrderItemViewModel orderItem)
        {
            if (orderItem.OrderId == 0)
                //Create Draft Order
                orderItem.OrderId = orderService.CreateDraftOrder();

            orderService.UpdateDraftOrder(orderItem);

            return Json(orderItem.OrderId);
        }
        public ActionResult GetOrderDetailsPartial(int orderId)
        {
            var orderDetail = orderService.GetOrderDetailSummary(orderId);

            return PartialView("_OrderDetailsPartial", orderDetail);
        }
        
        [HttpGet]
        public ActionResult GetOrderStatusPartial()
        {
            var records = orderService.CheckOrderTimeStatuses();
            return PartialView("_OrderStatusPartial", records);
        }

        public ActionResult GetOrderBillPartial(int orderId)
        {
            var orderDetail = orderService.GetOrderDetailSummary(orderId);
            return PartialView("_OrderBillPartial", orderDetail);
        }

        public JsonResult PlaceOrder(int orderId)
        {
            bool result = orderService.UpdateOrderStatus(orderId, (int)Constants.OrderStatus.Pending);

            return Json(result);
        }

        public JsonResult ProceedBill(int orderId)
        {
            int billId = orderService.ProceedBill(orderId);

            return Json(billId);
        }
        public JsonResult CancelOrder(int orderId)
        {
            bool cancelled = orderService.UpdateOrderStatus(orderId, (int)Constants.OrderStatus.Cancelled);

            return Json(cancelled);
        }
    }
}