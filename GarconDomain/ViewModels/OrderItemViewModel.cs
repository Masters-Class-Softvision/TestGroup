using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarconDomain.ViewModels
{
    public class OrderItemViewModel
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int DraftStatus { get; set; }
    }
}
