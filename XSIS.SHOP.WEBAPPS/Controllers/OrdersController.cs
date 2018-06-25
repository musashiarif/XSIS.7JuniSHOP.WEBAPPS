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
            var ordVM = JsonConvert.DeserializeObject<List<OrderViewModel>>(result);

            ApiAccess = ApiUrl + "api/OrderApi/Customer/";
            respon = client.GetAsync(ApiAccess).Result;
            string resultCustomer = respon.Content.ReadAsStringAsync().Result.ToString();
            var cust = JsonConvert.DeserializeObject<List<CustomerViewModel>>(resultCustomer);
            ViewBag.CustomerId = new SelectList(cust, "Id", "namaLengkap");
            return View(ordVM.ToList());            
        }
        [HttpPost]
        public ActionResult Index(FormCollection Search)
        {

            string replaceDate = Search["dateTime"].Replace("/", "-");
            string ApiAccess = ApiUrl + "api/OrderApi/SearchOrders/" + Search["orderNumber"] + "|" + replaceDate + "|" + Search["CustomerId"];
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;

            string result = respon.Content.ReadAsStringAsync().Result.ToString();
            var custVM = JsonConvert.DeserializeObject<List<OrderViewModel>>(result);
            //var custVM = service.SearchOrder(Search["orderNumber"], Search["dateTime"], Search["CustomerId"]);
            ApiAccess = ApiUrl + "api/OrderApi/Customer/";
            respon = client.GetAsync(ApiAccess).Result;
            string resultCustomer = respon.Content.ReadAsStringAsync().Result.ToString();
            var cust = JsonConvert.DeserializeObject<List<CustomerViewModel>>(resultCustomer);
            ViewBag.CustomerId = new SelectList(cust, "Id", "namaLengkap");
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

            if (orderVM == null)
            {
                return HttpNotFound();
            }
            return View(orderVM);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            string ApiEndPoint = ApiUrl + "api/OrderApi/Create/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;
            string resultList = response.Content.ReadAsStringAsync().Result.ToString();
            var OrderVM = JsonConvert.DeserializeObject<OrderViewModel>(resultList);
            if (TempData["List"] == null)
            {
                TempData["List"] = new List<OrderItemViewModel>();
            }            
            TempData.Keep();

            string ApiAccess = ApiUrl + "api/OrderApi/Customer/";            
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;
            string resultCustomer = respon.Content.ReadAsStringAsync().Result.ToString();
            var cust = JsonConvert.DeserializeObject<List<CustomerViewModel>>(resultCustomer);
            ViewBag.CustomerId = new SelectList(cust, "Id", "namaLengkap");
            return View(OrderVM);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel order)
        {
            string ApiAccess;
            HttpClient client = new HttpClient();
            HttpResponseMessage respon;
         
            if (ModelState.IsValid)
            {
                order.OrderItem = (List<OrderItemViewModel>)TempData["List"];

                string json = JsonConvert.SerializeObject(order);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                 ApiAccess = ApiUrl + "api/OrderApi/";                 
                 respon = client.PostAsync(ApiAccess, byteContent).Result;

                string result = respon.Content.ReadAsStringAsync().Result.ToString();
                int success = int.Parse(result);
                if (success == 1)
                {
                    return RedirectToAction("Index");
                }
                       
            }

            ApiAccess = ApiUrl + "api/OrderApi/Create/";
            respon = client.GetAsync(ApiAccess).Result;
            string resultCustomer = respon.Content.ReadAsStringAsync().Result.ToString();
            var cust = JsonConvert.DeserializeObject<List<CustomerViewModel>>(resultCustomer);
            ViewBag.CustomerId = new SelectList(cust, "Id", "namaLengkap");
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
    }
}
