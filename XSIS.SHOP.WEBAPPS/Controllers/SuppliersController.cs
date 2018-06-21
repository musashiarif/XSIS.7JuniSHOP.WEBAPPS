using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XSIS.Shop.Repository;
using XSIS.Shop.viewModel;
using System.Web.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace XSIS.SHOP.WEBAPPS.Controllers
{
    public class SuppliersController : Controller
    {
        private string ApiUrl = WebConfigurationManager.AppSettings["XSIS_Shop_WEBAPI"];
        private SupplierRepository service = new SupplierRepository();
        // GET: Suppliers
        public ActionResult Index()
        {
            string ApiAccess = ApiUrl + "api/SupplierApi/";
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;

            string result = respon.Content.ReadAsStringAsync().Result.ToString();
            List<SupplierViewModel> supVM = JsonConvert.DeserializeObject<List<SupplierViewModel>>(result);            
            return View(supVM.ToList());
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;

            string ApiAccessDetail = ApiUrl + "api/SupplierApi/get/" +idx;
            HttpClient detailsClient = new HttpClient();
            HttpResponseMessage detailRespon = detailsClient.GetAsync(ApiAccessDetail).Result;

            string result = detailRespon.Content.ReadAsStringAsync().Result.ToString();
            var supVM = JsonConvert.DeserializeObject<SupplierViewModel>(result);

            //SupplierViewModel supVM = service.getSupplierByID(idx);
            //supVM.product = service.listProduct(idx);
            if (supVM == null)
            {
                return HttpNotFound();
            }
            
            return View(supVM);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierViewModel supplier)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(supplier);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string ApiAccessCreate = ApiUrl + "api/SupplierApi/";
                HttpClient createClient = new HttpClient();
                HttpResponseMessage createRespon = createClient.PostAsync(ApiAccessCreate, byteContent).Result;

                string result = createRespon.Content.ReadAsStringAsync().Result.ToString();
                int success = int.Parse(result);
                if (success == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(supplier);
                }
            }

            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;

            string ApiAccess = ApiUrl + "api/SupplierApi/get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;            

            string result = respon.Content.ReadAsStringAsync().Result.ToString();
            SupplierViewModel supVM = JsonConvert.DeserializeObject<SupplierViewModel>(result);
            if (supVM == null)
            {
                return HttpNotFound();
            }
            
            return View(supVM);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierViewModel supplier)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(supplier);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string ApiAccess = ApiUrl + "api/SupplierApi/get/";
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
                    return View(supplier);
                }
               
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;

            string ApiAccess = ApiUrl + "api/SupplierApi/get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;

            string result = respon.Content.ReadAsStringAsync().Result.ToString();
            SupplierViewModel supVM = JsonConvert.DeserializeObject<SupplierViewModel>(result);
            //SupplierViewModel supVM = service.getSupplierByID(idx);
            if (supVM == null)
            {
                return HttpNotFound();
            }
           
            return View(supVM);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string ApiAccess = ApiUrl + "api/SupplierApi/" + id;
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
