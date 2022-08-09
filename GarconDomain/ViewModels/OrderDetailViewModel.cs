using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarconDomain.ViewModels
{
    public class OrderDetailViewModel
    {
        public int OrderId { get; set; }
        public int OrderStatuId { get; set; }
        public DateTime? BillDate { get; set; }
        public List<OrderItemDetailViewModel> OrderItems { get; set; }
        public OrderComputationViewModel OrderComputation { get; set; }
    }

    public class OrderItemDetailViewModel
    {
        public int OrderItemId { get; set; }
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public decimal MenuItemPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderComputationViewModel
    {
        public decimal ServiceCharge { get; set; }
        public decimal InclusiveTax { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
