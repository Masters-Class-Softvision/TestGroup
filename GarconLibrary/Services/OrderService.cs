using AutoMapper;
using GarconLibrary.DBFramework;
using GarconLibrary.Interfaces;
using GarconLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GarconLibrary.Services
{
    public class OrderService : IOrderService
    {
        readonly IEFRepository repository;
        public OrderService(IEFRepository repository)
        {
            this.repository = repository;
        }

        public int CreateOrder(OrderDto order)
        {
            try
            {
                var orderConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<OrderDto, Order>()
                     .ForMember(dest => dest.Bill, opt => opt.Ignore()); //Set to null
                    cfg.CreateMap<OrderItemDto, OrderItem>()
                     .ForMember(dest => dest.Order, opt => opt.Ignore()) //Set to null
                     .ForMember(dest => dest.MenuItem, opt => opt.Ignore()); //Set to null
                });

                //Convert OrderDto to Order
                var orderForDb = new Mapper(orderConfig).Map<Order>(order);

                repository.Create(orderForDb);
                return orderForDb.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        OrderDto IOrderService.GetOrder(int orderId)
        {
            if (orderId < 1)
                throw new ArgumentException($"Invalid Order reference ID {orderId}");

            var orderConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<Bill, BillDto>();
                cfg.CreateMap<OrderItem, OrderItemDto>()
                 .ForMember(dest => dest.Order, opt => opt.Ignore()) //Set to null
                 .ForMember(dest => dest.MenuItem, opt => opt.MapFrom(src => src.MenuItem));
                cfg.CreateMap<MenuItem, MenuItemDto>(); //Set to null
            });

            var order = repository.Context.Order
                    .Where(o => o.Id == orderId)
                    .FirstOrDefault();

            if (order == null)
                throw new NullReferenceException(($"No Record found for Order reference ID {orderId}"));

            return new Mapper(orderConfig).Map<OrderDto>(order);
        }

        public void RemoveOrderItem(int orderItemId)
        {
            var orderItem = repository.Context.OrderItem
                    .Where(o => o.Id == orderItemId)
                    .FirstOrDefault();

            if (orderItem != null)
            {
                repository.Context.DeleteObject(orderItem);
                repository.Context.SaveChanges();
            }
        }

        public bool UpdateOrderItemQuantity(int orderItemId, int quantity)
        {
            var orderItem = repository.Context.OrderItem
                     .Where(o => o.Id == orderItemId)
                     .FirstOrDefault();

            if (orderItem != null)
            {
                orderItem.Quantity = quantity;
                repository.Context.SaveChanges();

                return true;
            }

            return false;
        }
        public int AddOrderItem(OrderItemDto orderItemDto)
        {
            var orderConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderItemDto, OrderItem>()
                 .ForMember(dest => dest.Order, opt => opt.Ignore()) //Set to null
                 .ForMember(dest => dest.MenuItem, opt => opt.Ignore()); //Set to null
            });

            //Convert OrderItemDto to OrderItem
            var orderItemForDb = new Mapper(orderConfig).Map<OrderItem>(orderItemDto);

            repository.Create(orderItemForDb);
            repository.Context.SaveChanges();

            return orderItemForDb.Id;
        }

        public int GetDraftOrderId(int draftStatusId)
        {
            int result = 0;
            var order = repository.Context.Order
                    .Where(o => o.OrderStatusId == draftStatusId)
                    .OrderByDescending(o => o.Id)
                    .FirstOrDefault();

            if (order != null)
                result = order.Id;

            return result;
        }

        public bool UpdateOrderStatusToPending(int orderId, int orderStatusId)
        {
            bool updated = false;
            var order = repository.Context.Order
                    .Where(o => o.Id == orderId)
                    .FirstOrDefault();

            if (order != null)
            {
                order.OrderStatusId = orderStatusId;
                order.OrderDate = DateTime.Now;
                repository.Context.SaveChanges();
                updated = true;
            }
            return updated;
        }

        public List<OrderDto> GetOrders()
        {
            var orderConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<Bill, BillDto>();
                cfg.CreateMap<OrderItem, OrderItemDto>()
                 .ForMember(dest => dest.Order, opt => opt.Ignore()) //Set to null
                 .ForMember(dest => dest.MenuItem, opt => opt.MapFrom(src => src.MenuItem));
                cfg.CreateMap<MenuItem, MenuItemDto>(); //Set to null
            });

            var orders = repository.Context.Order
                    .Where(o=>o.OrderStatusId != 1 && o.OrderStatusId != 5) //Exclude Draft & Cancelled
                    .OrderByDescending(o=>o.OrderDate)
                    .ToList();

            return new Mapper(orderConfig).Map<List<OrderDto>>(orders);
        }

        public void UpdateOrderItemStatus(int orderItemId, int orderItemStatusId)
        {
            var item = repository.Context.OrderItem
                    .Where(o => o.Id == orderItemId)
                    .FirstOrDefault();

            if (item != null && item.OrderItemStatusId != orderItemStatusId)
            {
                item.OrderItemStatusId = orderItemStatusId;
                repository.Context.SaveChanges();
            }
        }

        public int CreateBill(BillDto billDto)
        {
            var billConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BillDto, Bill>();
            });

            //Convert OrderDto to Order
            var billForDb = new Mapper(billConfig).Map<Bill>(billDto);

            repository.Create(billForDb);
            return billForDb.Id;
        }

        public void UpdateOrderBillId(int orderId, int billId)
        {
            var item = repository.Context.Order
                    .Where(o => o.Id == orderId)
                    .FirstOrDefault();

            if (item != null)
            {
                item.BillId = billId;
                repository.Context.SaveChanges();
            }
        }

        bool IOrderService.UpdateOrderStatus(int orderId, int orderStatusId)
        {
            var item = repository.Context.Order
                   .Where(o => o.Id == orderId)
                   .FirstOrDefault();

            if (item != null && item.OrderStatusId != orderStatusId)
            {
                item.OrderStatusId = orderStatusId;
                repository.Context.SaveChanges();
                
                return true;
            }
            return false;

        }
    }
}
