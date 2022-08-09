using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarconLibrary.Models
{
    [Table("OrderItem", Schema = "dbo")]
    public class OrderItemDto
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public int OrderItemStatusId { get; set; }
        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }

        public virtual OrderDto Order { get; set; }
        public virtual MenuItemDto MenuItem { get; set; }
    }
}
