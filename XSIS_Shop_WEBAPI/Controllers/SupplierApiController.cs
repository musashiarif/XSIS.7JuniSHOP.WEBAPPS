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
    public class SupplierApiController : ApiController
    {
        private SupplierRepository service = new SupplierRepository();
        [HttpGet]
        public List<SupplierViewModel> GetSupplier()
        {
            var result = service.GetAllSupplier();
            return result;
        }
        [HttpGet]
        public List<ProductViewModel> listProduct(int id)
        {
            var result = service.listProduct(id);
            return result;
        }
        
        [HttpGet]
        public SupplierViewModel get(int id)
        {
            var result = service.getSupplierByID(id);
            return result;
        }
       
        [HttpPost]
        public int Post(SupplierViewModel supplier)
        {
            try
            {
                service.CreateSupplier(supplier);
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }
        [HttpPut]
        public int put(SupplierViewModel supplier)
        {
            try
            {
                service.EditSupplier(supplier);
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
                service.DeleteSupplier(id);
                return 1;
            }
            catch (Exception)
            {
                return 0;

            }
        }
    }
}
