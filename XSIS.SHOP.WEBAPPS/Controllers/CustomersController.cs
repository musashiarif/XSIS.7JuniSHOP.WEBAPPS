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
    public class CustomersController : Controller
    {
        private string ApiUrl = WebConfigurationManager.AppSettings["XSIS_Shop_WEBAPI"];
        private CustomerRepository service = new CustomerRepository();

        // GET: Customers
        public ActionResult Index()
        {
            string ApiAccess = ApiUrl + "api/CustomerApi/";
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;

            string result = respon.Content.ReadAsStringAsync().Result.ToString();
            List<CustomerViewModel> custVM = JsonConvert.DeserializeObject<List<CustomerViewModel>>(result);
            return View(custVM.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            //membuat path url api access http://localhost:51002/api/Customers/1
            string ApiAccess = ApiUrl + "api/CustomerApi/get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;

            string result = respon.Content.ReadAsStringAsync().Result.ToString();
            var custVM = JsonConvert.DeserializeObject<CustomerViewModel>(result);

            //CustomerViewModel custVM = service.getCustomerByID(idx);
            if (custVM == null)
            {
                return HttpNotFound();
            }
            
            return View(custVM);            
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Index(FormCollection Search)
        {
            string ApiSearchNama = ApiUrl + "api/CustomerApi/searchCustomers/" + Search["nama"]+"|"+ Search["cityCountry"]+ "|" + Search["email"];
            HttpClient searchNamaApi = new HttpClient();
            HttpResponseMessage searchNamaRespon = searchNamaApi.GetAsync(ApiSearchNama).Result;

            string result = searchNamaRespon.Content.ReadAsStringAsync().Result.ToString();
            List<CustomerViewModel> custVM = JsonConvert.DeserializeObject<List<CustomerViewModel>>(result);
            
            return View(custVM.ToList());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                string ApiCekNama = ApiUrl + "api/CustomerApi/cekNama/"+customer.FirstName+"/"+customer.LastName;
                HttpClient cekNamaApi = new HttpClient();
                HttpResponseMessage cekNamaRespon = cekNamaApi.GetAsync(ApiCekNama).Result;                
                bool namaSudahAda = bool.Parse(cekNamaRespon.Content.ReadAsStringAsync().Result.ToString());
                bool EmailSudahAda = false;
                if (!(string.IsNullOrEmpty(customer.Email) || string.IsNullOrWhiteSpace(customer.Email)))
                {
                    string ApiCekEmail = ApiUrl + "api/CustomerApi/cekEmail/" + customer.Email;
                    HttpClient cekEmailApi = new HttpClient();
                    HttpResponseMessage cekEmailRespon = cekEmailApi.GetAsync(ApiCekEmail).Result;
                    EmailSudahAda = bool.Parse(cekEmailRespon.Content.ReadAsStringAsync().Result.ToString());
                }                
                
                bool cek = true;

                if(namaSudahAda)
                {
                    ModelState.AddModelError("", "Nama Lengkap sudah ada.");
                    cek = false;

                }
                if (EmailSudahAda)
                {
                    ModelState.AddModelError("", "Email sudah ada.");
                    cek = false;
                }                
                if(cek)
                {
                    //membuat path url api access http://localhost:51002/api/Customers/1
                    string json = JsonConvert.SerializeObject(customer);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    string ApiAccess = ApiUrl + "api/CustomerApi/";
                    HttpClient client = new HttpClient();
                    HttpResponseMessage respon = client.PostAsync(ApiAccess, byteContent).Result;

                    string result = respon.Content.ReadAsStringAsync().Result.ToString();
                    int success = int.Parse(result);
                    if(success == 1)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(customer);
                    }                
                }
                else
                {
                    return View(customer);
                }                                          
            }
            return View(customer);
        }
        

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;

            string ApiAccess = ApiUrl + "api/CustomerApi/get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;

            string result = respon.Content.ReadAsStringAsync().Result.ToString();
            CustomerViewModel custVM = JsonConvert.DeserializeObject<CustomerViewModel>(result);
            if (custVM == null)
            {
                return HttpNotFound();
            }

            return View(custVM);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                //membuat path url api access http://localhost:51002/api/Customers/1
                string json = JsonConvert.SerializeObject(customer);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string ApiAccess = ApiUrl + "api/CustomerApi/";
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
                    return View(customer);
                }
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            string ApiAccess = ApiUrl + "api/CustomerApi/get/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage respon = client.GetAsync(ApiAccess).Result;

            string result = respon.Content.ReadAsStringAsync().Result.ToString();
            CustomerViewModel custVM = JsonConvert.DeserializeObject<CustomerViewModel>(result);
            if (custVM == null)
            {
                return HttpNotFound();
            }

            return View(custVM);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Delete path url api access http://localhost:51002/api/Customers/1
            string ApiAccess = ApiUrl + "api/CustomerApi/" + id;
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
