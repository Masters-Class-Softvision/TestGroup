using GarconLibrary.DBFramework;
using GarconLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarconLibrary.Interfaces
{
    public interface IMenuItemService
    {
        List<MenuItemDto> GetMenuItems(int menuCategoryId = 0);
        MenuItemDto GetMenuItemById(int menuItemId);
    }
}
