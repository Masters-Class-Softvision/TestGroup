using GarconDomain.Interfaces;
using LibInterfaces = GarconLibrary.Interfaces;
using LibModels = GarconLibrary.Models;
using GarconDomain.Models;
using System.Collections.Generic;
using AutoMapper;

namespace GarconDomain.Services
{
    public class MenuItemService : IMenuItemService
    {
        readonly LibInterfaces.IMenuItemService libMenuItemService;
        public MenuItemService(LibInterfaces.IMenuItemService libMenuItemService)
        {
            this.libMenuItemService = libMenuItemService;
        }

        public List<MenuItem> GetMenuItems()
        {
            var config = new MapperConfiguration(cfg =>
            {
            cfg.CreateMap<LibModels.MenuItemDto, MenuItem>()
                .ForMember(dest => dest.MenuItemCategoryName, act => act.MapFrom(src => Constants.MenuItemCategoryMap[src.MenuItemCategoryId]));
            });

            var menuItemsDto = libMenuItemService.GetMenuItems();

            var mapper = new Mapper(config);
            var menuItems = mapper.Map<List<MenuItem>>(menuItemsDto);

            return menuItems;
        }

        public List<MenuItem> GetMenuItemsByCategory(int menuCategoryId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LibModels.MenuItemDto, MenuItem>()
                    .ForMember(dest => dest.MenuItemCategoryName, act => act.MapFrom(src => Constants.MenuItemCategoryMap[src.MenuItemCategoryId]));
            });

            var menuItemsDto = libMenuItemService.GetMenuItems(menuCategoryId: menuCategoryId);

            var mapper = new Mapper(config);
            var menuItems = mapper.Map<List<MenuItem>>(menuItemsDto);

            return menuItems;
        }

        public MenuItem GetMenuItemById(int menuItemId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LibModels.MenuItemDto, MenuItem>()
                    .ForMember(dest => dest.MenuItemCategoryName, act => act.MapFrom(src => Constants.MenuItemCategoryMap[src.MenuItemCategoryId]));
            });

            var menuItemDto = libMenuItemService.GetMenuItemById(menuItemId);

            var mapper = new Mapper(config);
            var menuItem = mapper.Map<MenuItem>(menuItemDto);

            return menuItem;
        }
    }
}
