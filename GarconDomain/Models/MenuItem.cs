using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarconDomain.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MenuItemCategoryId { get; set; }
        public string MenuItemCategoryName { get; set; }
        public decimal Price { get; set; }
        public bool IsChefRecommended { get; set; }
        public int CookingTimeMinutes { get; set; }
        public int PrepTimeMinutes { get; set; }
        public string ImageLink { get; set; }
        public bool IsActive { get; set; }
    }
}
