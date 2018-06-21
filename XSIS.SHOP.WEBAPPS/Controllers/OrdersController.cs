    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XSIS.Shop.Repository;
using XSIS.Shop.Model;
using XSIS.Shop.viewModel;
using System.Web.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace XSIS.SHOP.WEBAPPS.Controllers
{
    public class OrdersController : Controller
    {
        private string ApiUrl = WebConfigurationManager.AppSettings["XSIS_Shop_WEBAPI"];
        private OrderRepository service = new OrderRepository();
        private ShopDBEntities db = new ShopDBEntities();
       
        // GET: Orders
        public ActionResult Index()
        {
            string ApiAccess = ApiUrl + "api/OrderApi/";
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;

            string result = respon.Content.ReadAsStringAsync().Result.ToString();
            List<OrderViewModel> ordVM = JsonConvert.DeserializeObject<List<OrderViewModel>>(result);

            ViewBag.CustomerId = new SelectList(service.GetAllCustomer(), "Id", ("namaLengkap"));
            return View(ordVM.ToList());            
        }
        [HttpPost]
        public ActionResult Index(FormCollection Search)
        {

            string replaceDate = Search["dateTime"].Replace("/", "-");
            string ApiSearch = ApiUrl + "api/OrderApi/SearchOrders/" + Search["orderNumber"] + "|" + replaceDate + "|" + Search["CustomerId"];
            HttpClient searchApi = new HttpClient();
            HttpResponseMessage searchRespon = searchApi.GetAsync(ApiSearch).Result;

            string result = searchRespon.Content.ReadAsStringAsync().Result.ToString();
            var custVM = JsonConvert.DeserializeObject<List<OrderViewModel>>(result);
            //var custVM = service.SearchOrder(Search["orderNumber"], Search["dateTime"], Search["CustomerId"]);
            ViewBag.CustomerId = new SelectList(service.GetAllCustomer(), "Id", ("namaLengkap"));
            return View(custVM.ToList());

        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;

            string ApiAccess = ApiUrl + "api/OrderApi/get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;

            string result = respon.Content.ReadAsStringAsync().Result.ToString();
            var orderVM = JsonConvert.DeserializeObject<OrderViewModel>(result);
//            OrderViewModel orderVM = service.getDetailByID(idx);
            if (orderVM == null)
            {
                return HttpNotFound();
            }
            return View(orderVM);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "FirstName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                service.createOrder(order);
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;

            Order order = db.Order.Find(idx);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
