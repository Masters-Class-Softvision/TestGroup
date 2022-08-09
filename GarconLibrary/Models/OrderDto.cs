using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarconLibrary.Models
{
    [Table("Order", Schema = "dbo")]
    public class OrderDto
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal InclusiveTax { get; set; }
        public int OrderStatusId { get; set; }

        [ForeignKey("Bill")]
        public int? BillId { get; set; }
        public virtual BillDto Bill { get; set; }
        public virtual ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
