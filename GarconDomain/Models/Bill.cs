using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarconDomain.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BillDate { get; set; }
    }
}
