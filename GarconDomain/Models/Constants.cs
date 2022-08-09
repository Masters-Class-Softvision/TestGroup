using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarconDomain.Models
{
    public class Constants
    {
        public enum MenuItemCategory
        {
            Appetizers = 1,
            MainCourses,
            Desserts,
            Drinks,
        }

        public static Dictionary<int, string> MenuItemCategoryMap = new Dictionary<int, string>
        {
            {1, "Appetizers" },
            {2, "Main Courses" },
            {3, "Desserts" },
            {4, "Drinks" },
        };

        public enum OrderItemStatus
        {
            Queued = 1,
            BeingPrepared = 2,
            BeingCooked = 3,
            BeingServed = 4,
            Served = 5,
        }

        public static Dictionary<int, string> OrderItemStatusMap = new Dictionary<int, string>
        {
            { 1, "Queued" },
            { 2, "Being Prepared" },
            { 3, "Being Cooked" },
            { 4, "Being Served" },
            { 5, "Served" },
        };


        public enum OrderStatus
        {
            Draft = 1,
            Pending = 2,
            Served = 3,
            Paid = 4,
            Cancelled = 5,
        }

        public static Dictionary<int, string> OrderStatusMap = new Dictionary<int, string>
        {
            { 1, "Draft" },
            { 2, "Pending" },
            { 3, "Served" },
            { 4, "Paid" },
            { 5, "Cancelled" },
        };

        public enum OrderSettings
        {
            ServiceChargePercent = 1, 
            InclusiveTaxPercent = 2,  
            ServingTimeMinutes = 3,
        }

        public static Dictionary<int, decimal> OrderSettingsMap = new Dictionary<int, decimal>
        {
            { 1, 10.00M }, //10% Service Charge over the total bill.
            { 2, 12.00M }, //12% Inclusive Tax over the total bill.
            { 3, 2.00M },  //3 Mins Default Serving Time
        };

        public enum OrderItemDraftStatus
        {
            Increment = 1,
            Decrement = 2,
        }
    }
}
