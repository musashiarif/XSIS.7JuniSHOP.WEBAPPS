using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XSIS.Shop.Repository;
using XSIS.Shop.viewModel;

namespace XSIS_Shop_WEBAPI.Controllers
{
    public class CustomerApiController : ApiController
    {
        private CustomerRepository service = new CustomerRepository();
        [HttpGet]
        public List<CustomerViewModel> GetCustomers()
        {
            var result = service.GetAllCustomer();
            return result;
        }
        [HttpGet]
        public List<CustomerViewModel> searchCustomers(string id)
        {
            string[] Parameters = id.Split('|');

            string param1 = Parameters[0];
            string param2 = Parameters[1];
            string param3 = Parameters[2];

            var result = service.SearchCustomer(param1, param2, param3);
            return result;
        }

        [HttpGet]
        public CustomerViewModel get(int id)
        {
            var result = service.getCustomerByID(id);
            return result;
        }
        [HttpGet]
        public bool cekNama(string id, string id2)
        {
            return service.CekNama(id,id2);
        }
        [HttpGet]
        public bool cekEmail(string id)
        {
            return service.cekEmail(id);
        }

        [HttpPost]
        public int Post(CustomerViewModel customer)
        {
            try
            {
                service.CreateCustomer(customer);
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }
        [HttpPut]
        public int put(CustomerViewModel customer)
        {
            try
            {
                service.EditCustomer(customer);
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
        }
        [HttpDelete]
        public int delete(int id)
        {
            try
            {
                service.DeleteCustomer(id);
                return 1;
            }
            catch (Exception)
            {
                return 0;
                
            }
        }
    }
}
