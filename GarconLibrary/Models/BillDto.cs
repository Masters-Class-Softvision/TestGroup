using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarconLibrary.Models
{
    [Table("Bill", Schema = "dbo")]
    public class BillDto
    {
        [Key]
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BillDate { get; set; }
    }
}
