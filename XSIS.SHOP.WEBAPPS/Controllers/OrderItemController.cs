using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XSIS.Shop.Model;
using XSIS.Shop.viewModel;
using XSIS.Shop.Repository;

namespace XSIS.SHOP.WEBAPPS.Controllers
{
    public class OrderItemController : Controller
    {
        private OrderRepository service = new OrderRepository();
        private ShopDBEntities db = new ShopDBEntities();
        // GET: OrderItem
        public ActionResult CreateItem()
        {
            ViewBag.ProductId = new SelectList(service.GetProduct(), "Id", "ProductName");
            return PartialView();
        }

        //public ActionResult Add(OrderItemViewModel OritVM)
        //{
        //    var order = service.temporaryList(OritVM,(List<OrderItemViewModel>)TempData["List"]);
        //    TempData["List"] = order.
        //    return OritVM;
        //}
    }
}