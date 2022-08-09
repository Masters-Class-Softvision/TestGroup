using GarconDomain.Models;
using GarconDomain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GarconDomain.Models.Constants;

namespace GarconDomain.Helpers
{
    public static class Helpers
    {
        public static OrderComputationViewModel CalculateTotalAmount(Order order)
        {
            try
            {
                decimal serviceChargePercent = order.ServiceCharge / 100;
                decimal inclusiveTaxPercent = order.InclusiveTax / 100;
                decimal subTotal = 0;
                decimal totalAmount = 0;

                foreach (var item in order.OrderItems)
                    totalAmount += item.MenuItem.Price * item.Quantity;

                decimal serviceCharge = serviceChargePercent * totalAmount;
                decimal inclusiveTax = inclusiveTaxPercent * totalAmount;
                subTotal = totalAmount;
                totalAmount += serviceCharge + inclusiveTax;

                var result = new OrderComputationViewModel()
                {
                    InclusiveTax = inclusiveTax,
                    ServiceCharge = serviceCharge,
                    SubTotal = subTotal,
                    TotalAmount = totalAmount,
                };
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static OrderItemTimeStatusViewModel CalculateOrderEstimatedTime(Order order)
        {
            try
            {
                if (order == null)
                {
                    throw new ArgumentNullException("Order can't be null");
                }
                var result = new OrderItemTimeStatusViewModel
                {
                    OrderId = order.Id,
                    OrderStatusId = order.OrderStatusId,
                    OrderStatusName = OrderStatusMap[order.OrderStatusId],
                    OrderItemTimeStatuses = new List<OrderItemTimeStatus>()
                };

                foreach (var item in order.OrderItems)
                {
                    int orderItemStatusId = GetStatusAndMinutesLeft(order.OrderDate, item, out int minutesLeft);
                    var orderItemTimeStatus = new OrderItemTimeStatus
                    {
                        OrderItemId = item.Id,
                        MenuItemName = item.MenuItem.Name,
                        Quantity = item.Quantity,
                        OrderItemStatusId = orderItemStatusId,
                        MinutesLeft = minutesLeft,
                    };
                    orderItemTimeStatus.SetOrderItemStatusName();

                    result.OrderItemTimeStatuses.Add(orderItemTimeStatus);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static int GetStatusAndMinutesLeft(DateTime orderDate, OrderItem orderItem, out int minutesLeft)
        {
            TimeSpan span = DateTime.Now.Subtract(orderDate);
            minutesLeft = 0;

            int totalPrepTime = orderItem.MenuItem.PrepTimeMinutes * orderItem.Quantity;
            int totalCookingTime = orderItem.MenuItem.CookingTimeMinutes * orderItem.Quantity;
            int totalServingTime = (int)OrderSettingsMap[(int)OrderSettings.ServingTimeMinutes];
            int spanMinsMinusPrep = span.Minutes - totalPrepTime;
            int spanMinsMinusPrepCook = span.Minutes - (totalPrepTime + totalCookingTime);

            if (span.Minutes < totalPrepTime)
            {
                minutesLeft = (orderItem.MenuItem.PrepTimeMinutes * orderItem.Quantity) - span.Minutes;
                orderItem.SetOrderItemToBeingPrepared();
            }
            else if (spanMinsMinusPrep < totalCookingTime)
            {
                minutesLeft = totalPrepTime + totalCookingTime - span.Minutes;
                orderItem.SetOrderItemToBeingCooked();
            }
            else if (spanMinsMinusPrepCook < totalServingTime)
            {
                minutesLeft = totalPrepTime + totalCookingTime + totalServingTime - span.Minutes;
                orderItem.SetOrderItemToReadyToServe();
            }
            else
            {
                orderItem.SetOrderItemToServed();
            }

            return orderItem.OrderItemStatusId;
        }
    }
}
