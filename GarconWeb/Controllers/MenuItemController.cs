using GarconDomain.Interfaces;
using System.Web.Mvc;

namespace GarconWeb.Controllers
{
    public class MenuItemController : Controller
    {
        readonly IMenuItemService menuItemService;
        readonly IOrderService orderService;

        public MenuItemController(IMenuItemService menuItemService, IOrderService orderService)
        {
            this.menuItemService = menuItemService;
            this.orderService = orderService;
        }
        // GET: MenuItem
        public ActionResult Index()
        {
            var allItems = menuItemService.GetMenuItems();
            int draftOrderId = orderService.GetDraftOrderId();
            ViewBag.DraftOrderId = draftOrderId;
            return View(allItems);
        }
    }
}