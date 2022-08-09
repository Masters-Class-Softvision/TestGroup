using GarconDomain.Models;
using System.Collections.Generic;

namespace GarconDomain.Interfaces
{
    public interface IMenuItemService
    {
        List<MenuItem> GetMenuItems();
        List<MenuItem> GetMenuItemsByCategory(int menuCategoryId);
        MenuItem GetMenuItemById(int menuItemId);
    }
}
