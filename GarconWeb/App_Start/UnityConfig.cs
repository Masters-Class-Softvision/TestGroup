using DomainInterfaces = GarconDomain.Interfaces;
using DomainServices = GarconDomain.Services;

using LibInterfaces = GarconLibrary.Interfaces;
using LibServices = GarconLibrary.Services;

using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace GarconWeb
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<DomainInterfaces.IMenuItemService, DomainServices.MenuItemService>();
            container.RegisterType<DomainInterfaces.IOrderService, DomainServices.OrderService>();

            container.RegisterType<LibInterfaces.IEFRepository, GarconLibrary.Repository.EFRepository>();
            container.RegisterType<LibInterfaces.IMenuItemService, LibServices.MenuItemService>();
            container.RegisterType<LibInterfaces.IOrderService, LibServices.OrderService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}