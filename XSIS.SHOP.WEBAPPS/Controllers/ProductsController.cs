using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XSIS.SHOP.WEBAPPS.Models;
using XSIS.Shop.Repository;
using XSIS.Shop.viewModel;
using System.Web.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;


namespace XSIS.SHOP.WEBAPPS.Controllers
{
    public class ProductsController : Controller
    {
        private string ApiUrl = WebConfigurationManager.AppSettings["XSIS_Shop_WEBAPI"];
        private ProductRepository service = new ProductRepository();

        // GET: Products
        public ActionResult Index()
        {
            string ApiAccess = ApiUrl + "api/ProductApi/";
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;

            string result = respon.Content.ReadAsStringAsync().Result.ToString();
            List<ProductViewModel> prodVM = JsonConvert.DeserializeObject<List<ProductViewModel>>(result);
            return View(prodVM.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;

            string ApiAccess = ApiUrl + "api/ProductApi/get/"+idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;

            string result = respon.Content.ReadAsStringAsync().Result.ToString();
            var prodVM = JsonConvert.DeserializeObject<ProductViewModel>(result);  
            if (prodVM == null)
            {
                return HttpNotFound();
            }
            return View(prodVM);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
                        
            ViewBag.SupplierId = new SelectList(service.ListSupplier(), "Id", "CompanyName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(product);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string ApiAccess = ApiUrl + "api/ProductApi/";
                HttpClient client = new HttpClient();
                HttpResponseMessage respon = client.PostAsync(ApiAccess, byteContent).Result;

                string result = respon.Content.ReadAsStringAsync().Result.ToString();
                int success = int.Parse(result);
                if (success == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(product);

                }
            }

            ViewBag.SupplierId = new SelectList(service.ListSupplier(), "Id", "CompanyName", product.SupplierId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            string ApiAccess = ApiUrl + "api/ProductApi/get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;

            string result = respon.Content.ReadAsStringAsync().Result.ToString();
            ProductViewModel prodVM = JsonConvert.DeserializeObject<ProductViewModel>(result);
            
            if (prodVM == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(service.ListSupplier(), "Id", "CompanyName");

            return View(prodVM);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(product);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string ApiAccess = ApiUrl + "api/ProductApi/get/";
                HttpClient client = new HttpClient();
                HttpResponseMessage respon = client.PutAsync(ApiAccess, byteContent).Result;

                string result = respon.Content.ReadAsStringAsync().Result.ToString();
                int success = int.Parse(result);
                if (success == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(product);
                }
            }
            ViewBag.SupplierId = new SelectList(service.ListSupplier(), "Id", "CompanyName", product.SupplierId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            string ApiAccess = ApiUrl + "api/ProductApi/get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;

            string result = respon.Content.ReadAsStringAsync().Result.ToString();

            ProductViewModel prodVM = JsonConvert.DeserializeObject<ProductViewModel>(result);
            if (prodVM == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(service.ListSupplier(), "Id", "CompanyName");
            return View(prodVM);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string ApiAccess = ApiUrl + "api/ProductApi/get/";
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.DeleteAsync(ApiAccess).Result;

            string result = respon.Content.ReadAsStringAsync().Result.ToString();
            int success = int.Parse(result);
            if (success == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }     
    }
}
