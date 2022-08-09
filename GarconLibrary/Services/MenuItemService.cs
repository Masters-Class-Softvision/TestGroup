using AutoMapper;
using AutoMapper.QueryableExtensions;
using GarconLibrary.DBFramework;
using GarconLibrary.Interfaces;
using GarconLibrary.Models;
using GarconLibrary.Repository;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarconLibrary.Services
{
    public class MenuItemService : IMenuItemService
    {
        readonly IEFRepository repository;
        public MenuItemService(IEFRepository repository)
        {
            this.repository = repository;
        }

        public List<MenuItemDto> GetMenuItems(int menuCategoryId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MenuItem, MenuItemDto>().MaxDepth(1);
            });

            var menuItems = new List<MenuItem>();

            //GetMenuItemsByCategory
            if (menuCategoryId > 0)
            {
                menuItems = repository.Context.MenuItem
                   .Where(u => u.IsActive == true && u.MenuItemCategoryId == menuCategoryId)
                   .ToList();
            }
            else
            {
                menuItems = repository.Context.MenuItem
                .Where(u => u.IsActive == true)
                .ToList();
            }

            var mapper = new Mapper(config);
            var menuItemDtos = mapper.Map<List<MenuItemDto>>(menuItems);

            return menuItemDtos;
        }

        public List<MenuItemDto> GetChefRecommended()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MenuItem, MenuItemDto>().MaxDepth(1);
            });

            var menuItems = repository.Context.MenuItem
                   .Where(u => u.IsActive == true && u.IsChefRecommended == true)
                   .ToList();

            var mapper = new Mapper(config);
            var menuItemDtos = mapper.Map<List<MenuItemDto>>(menuItems);

            return menuItemDtos;
        }

        public MenuItemDto GetMenuItemById(int menuItemId)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuItem, MenuItemDto>();
                });

                var menuItem = repository.Context.MenuItem
                       .Where(u => u.IsActive == true && u.Id == menuItemId)
                       .FirstOrDefault();

                if (menuItem == null)
                    throw new NullReferenceException(($"No Record found for Menu reference ID {menuItemId}"));

                var mapper = new Mapper(config);
                var menuItemDto = mapper.Map<MenuItemDto>(menuItem);

                return menuItemDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
