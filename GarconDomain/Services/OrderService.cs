using GarconDomain.Interfaces;
using LibInterfaces = GarconLibrary.Interfaces;
using LibModels = GarconLibrary.Models;
using GarconDomain.Models;
using System.Collections.Generic;
using AutoMapper;
using GarconLibrary.Models;
using System;
using GarconDomain.ViewModels;
using static GarconDomain.Models.Constants;
using System.Linq;

namespace GarconDomain.Services
{
    public class OrderService : IOrderService
    {
        readonly LibInterfaces.IOrderService libOrderService;
        public OrderService(LibInterfaces.IOrderService libOrderService)
        {
            this.libOrderService = libOrderService;
        }

        public int CreateOrder(Order order)
        {
            var orderConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>()
                 .ForMember(dest => dest.Bill, opt => opt.Ignore()); //Set to null
                cfg.CreateMap<OrderItem, OrderItemDto>()
                 .ForMember(dest => dest.Order, opt => opt.Ignore()) //Set to null
                 .ForMember(dest => dest.MenuItem, opt => opt.Ignore()); //Set to null
            });

            //Convert Order to OrderDto
            var orderForDto = new Mapper(orderConfig).Map<OrderDto>(order);

            return libOrderService.CreateOrder(orderForDto);
        }

        public Order GetOrder(int orderId)
        {
            if (orderId < 1)
                throw new ArgumentException($"Invalid Order reference ID {orderId}");

            var orderConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderDto, Order>();
                cfg.CreateMap<BillDto, Bill>();
                cfg.CreateMap<OrderItemDto, OrderItem>()
                 .ForMember(dest => dest.OrderItemStatusName, act => act.MapFrom(src => Constants.OrderItemStatusMap[src.OrderItemStatusId]))
                 .ForMember(dest => dest.Order, opt => opt.Ignore()) //Set to null
                 .ForMember(dest => dest.MenuItem, opt => opt.MapFrom(src => src.MenuItem));
                cfg.CreateMap<MenuItemDto, MenuItem>(); //Set to null
            });

            var orderDto = libOrderService.GetOrder(orderId);

            var order = new Mapper(orderConfig).Map<Order>(orderDto);


            if (order == null)
                throw new NullReferenceException(($"No Record found for Order reference ID {orderId}"));

            return order;
        }

        public int CreateDraftOrder()
        {
            var draftOrder = new Order
            {
                OrderDate = DateTime.Now,
           
            };
            draftOrder.SetOrderStatusToDraft();
            draftOrder.SetInclusiveTaxToDefault();
            draftOrder.SetServiceChargeToDefault();

            var orderConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>();
            });

            var draftOrderDto = new Mapper(orderConfig).Map<OrderDto>(draftOrder);

            var orderId = libOrderService.CreateOrder(draftOrderDto);

            return orderId;
        }

        public bool UpdateDraftOrder(OrderItemViewModel rec)
        {
            bool updated = false;
            var order = GetOrder(rec.OrderId);

            if (order != null)
            {
                var orderItem = order.OrderItems
                            .OfType<OrderItem>()
                            .Where(o => o.MenuItemId == rec.MenuItemId)
                            .FirstOrDefault();

                switch (rec.DraftStatus)
                {
                    case (int)OrderItemDraftStatus.Increment:
                        // Update Quantity
                        if (orderItem != null)
                        {
                            orderItem.Quantity += 1;
                            libOrderService.UpdateOrderItemQuantity(orderItem.Id, orderItem.Quantity);
                        }
                        //Add orderItem to order
                        else
                        {
                            var orderItemConfig = new MapperConfiguration(cfg =>
                            {
                                cfg.CreateMap<OrderItem, OrderItemDto>()
                                 .ForMember(dest => dest.Order, opt => opt.Ignore()) //Set to null
                                 .ForMember(dest => dest.MenuItem, opt => opt.Ignore()); //Set to null
                            });

                            var orderItem1 = new OrderItem
                            {
                                OrderId = rec.OrderId,
                                MenuItemId = rec.MenuItemId,
                                Quantity = 1
                            };
                            orderItem1.SetOrderItemToQueued();

                            var orderItemDto = new Mapper(orderItemConfig).Map<OrderItemDto>(orderItem1);

                            libOrderService.AddOrderItem(orderItemDto);
                        }
                        break;
                    case (int)OrderItemDraftStatus.Decrement:
                        //Remove or Update Quantity
                        if (orderItem != null && orderItem.Quantity > 1)
                        {
                            orderItem.Quantity -= 1;
                            libOrderService.UpdateOrderItemQuantity(orderItem.Id, orderItem.Quantity);
                        }
                        //Remove orderItem to order
                        else
                        {
                            libOrderService.RemoveOrderItem(orderItem.Id);
                        }
                        break;
                    default:
                        break;
                }
            }

            return updated;
        }

        public OrderDetailViewModel GetOrderDetailSummary(int orderId)
        {
            var result = new OrderDetailViewModel();

            var orderDetail = GetOrder(orderId);

            if (orderDetail != null)
            {
                result.OrderId = orderDetail.Id;
                result.OrderItems = new List<OrderItemDetailViewModel>();
                result.OrderStatuId = orderDetail.OrderStatusId;
                if (orderDetail.Bill != null)
                    result.BillDate = orderDetail.Bill.BillDate;

                foreach (var item in orderDetail.OrderItems)
                {
                    var rec = new OrderItemDetailViewModel();

                    rec.MenuItemId = item.MenuItemId;
                    rec.MenuItemName = item.MenuItem.Name;
                    rec.OrderItemId = item.Id;
                    rec.MenuItemPrice = item.MenuItem.Price;
                    rec.Quantity = item.Quantity;

                    result.OrderItems.Add(rec);
                }
                result.OrderComputation = Helpers.Helpers.CalculateTotalAmount(orderDetail);
            }
            return result;
        }

        public int GetDraftOrderId()
        {
            return libOrderService.GetDraftOrderId((int)OrderStatus.Draft);
        }

        public bool UpdateOrderStatus(int orderId, int orderStatus)
        {
            return libOrderService.UpdateOrderStatus(orderId, orderStatus);
        }

        public List<Order> GetOrders()
        {
            var orderConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderDto, Order>();
                cfg.CreateMap<BillDto, Bill>();
                cfg.CreateMap<OrderItemDto, OrderItem>()
                 .ForMember(dest => dest.OrderItemStatusName, act => act.MapFrom(src => Constants.OrderItemStatusMap[src.OrderItemStatusId]))
                 .ForMember(dest => dest.Order, opt => opt.Ignore()) //Set to null
                 .ForMember(dest => dest.MenuItem, opt => opt.MapFrom(src => src.MenuItem));
                cfg.CreateMap<MenuItemDto, MenuItem>(); //Set to null
            });

            var ordersDto = libOrderService.GetOrders();

            var mapper = new Mapper(orderConfig);
            var orders = mapper.Map<List<Order>>(ordersDto);

            return orders;
        }

        public List<OrderItemTimeStatusViewModel> CheckOrderTimeStatuses()
        {
            var results = new List<OrderItemTimeStatusViewModel>();
            var orders = GetOrders()
                .Where(x=>x.OrderStatusId != (int)OrderStatus.Draft && x.OrderStatusId != (int)OrderStatus.Cancelled);

            foreach (var order in orders)
            {
                var rec = Helpers.Helpers.CalculateOrderEstimatedTime(order);
                if (order.OrderStatusId == (int)OrderStatus.Paid)
                {
                    results.Add(rec);
                    continue;
                }
                else
                {
                    UpdateOrderItemStatuses(rec.OrderItemTimeStatuses);
                    if (IsOrderCompleted(rec))
                    {
                        libOrderService.UpdateOrderStatus(rec.OrderId, (int)OrderStatus.Served);
                        rec.OrderStatusId = (int)OrderStatus.Served;
                        rec.OrderStatusName = OrderStatusMap[(int)OrderStatus.Served];
                    }
                }
                results.Add(rec);
            }
            return results;
        }

        private void UpdateOrderItemStatuses(List<OrderItemTimeStatus> orderItems)
        {
            foreach (var item in orderItems)
            {
               libOrderService.UpdateOrderItemStatus(item.OrderItemId, item.OrderItemStatusId);
            }
        }
        private bool IsOrderCompleted(OrderItemTimeStatusViewModel order)
        {
            bool result = false;
            int servedItemCount = 0;

            foreach (var item in order.OrderItemTimeStatuses)
            {
                if (item.OrderItemStatusId == (int)OrderItemStatus.Served)
                    servedItemCount++;
            }

            if (servedItemCount == order.OrderItemTimeStatuses.Count)
                result = true;

            return result;
        }

        public int ProceedBill(int orderId)
        {
            var orderDetailSummary = GetOrderDetailSummary(orderId);
            var bill = new BillDto()
            {
                TotalPrice = orderDetailSummary.OrderComputation.TotalAmount,
                BillDate = DateTime.Now
            };

            bill.Id = libOrderService.CreateBill(bill);
            
            if (bill.Id > 0)
            {
                libOrderService.UpdateOrderBillId(orderId, bill.Id);
                libOrderService.UpdateOrderStatus(orderId, (int)OrderStatus.Paid);
            }

            return bill.Id;
        }
    }
}
