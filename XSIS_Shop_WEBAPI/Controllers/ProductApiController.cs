using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XSIS.Shop.Repository;
using XSIS.Shop.viewModel;
using XSIS.Shop.Model;

namespace XSIS_Shop_WEBAPI.Controllers
{
    public class ProductApiController : ApiController
    {
        private ProductRepository service = new ProductRepository();

        [HttpGet]
        public List<ProductViewModel> GetProduct()
        {
            var result = service.GetAllProduct();
            return result;
        }

       
        [HttpGet]
        public ProductViewModel get(int id)
        {
            var result = service.getProductByID(id);
            return result;
        }

        [HttpPost]
        public int Post(ProductViewModel product)
        {
            try
            {
                service.CreateProduct(product);
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }
        [HttpPut]
        public int put(ProductViewModel product)
        {
            try
            {
                service.EditProduct(product);
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
                service.DeleteProduct(id);
                return 1;
            }
            catch (Exception)
            {
                return 0;

            }
        }
    }
}
