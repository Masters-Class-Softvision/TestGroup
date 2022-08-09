using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarconLibrary.Models
{
    [Table("MenuItem", Schema = "dbo")]
    public class MenuItemDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MenuItemCategoryId { get; set; }
        public decimal Price { get; set; }
        public bool IsChefRecommended { get; set; }
        public int CookingTimeMinutes { get; set; }
        public int PrepTimeMinutes { get; set; }
        public string ImageLink { get; set; }
        public bool IsActive { get; set; }
    }
}
