using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GarconDomain.Models.Constants;

namespace GarconDomain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal ServiceCharge { get; private set; }
        public decimal InclusiveTax { get; private set; }
        public int OrderStatusId { get; private set; }
        public string OrderStatusName { get; private set; }
        public int? BillId { get; set; }
        public virtual Bill Bill { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public void SetOrderStatusToCancel() => OrderStatusId = (int)OrderStatus.Cancelled;
        public void SetOrderStatusToDraft() => OrderStatusId = (int)OrderStatus.Draft;
        public void SetServiceChargeToDefault() => ServiceCharge = OrderSettingsMap[(int)OrderSettings.ServiceChargePercent];
        public void SetInclusiveTaxToDefault() => InclusiveTax = OrderSettingsMap[(int)OrderSettings.InclusiveTaxPercent];
        public void SetOrderStatusName() => OrderStatusName = OrderStatusMap[OrderStatusId];
    }
}
