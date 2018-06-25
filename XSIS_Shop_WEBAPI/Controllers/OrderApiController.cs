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
    public class OrderApiController : ApiController
    {
        private OrderRepository service = new OrderRepository();

        [HttpGet]
        [Route("api/OrderApi/")]
        public List<OrderViewModel> GetOrder()
        {
            var result = service.GetAllOrder();
            return result;
        }
        [HttpPost]
        [Route("api/OrderApi/Create/")]
        public bool Post(OrderViewModel order)
        {
            try
            {
                service.createOrder(order);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        [HttpGet]
        [Route("api/OrderApi/{id}")]
        public OrderViewModel get(int id)
        {
            var result = service.getDetailByID(id);
            return result;
        }
        [HttpGet]
        [Route("api/OrderApi/Customer/")]
        public List<CustomerViewModel> Customer()
        {
            var result = service.GetAllCustomer();
            return result;
        }
        [HttpGet]
        [Route("api/OrderApi/SearchOrders/{id}")]
        public List<OrderViewModel> searchOrders(string id)
        {
            
            string[] Parameters = id.Split('|');

            string param1 = Parameters[0];
            string param2 = Parameters[1];
            string param3 = Parameters[2];  
            var result = service.SearchOrder(param1, param2, param3);
            return result;
        }
        [HttpGet]
        [Route("api/OrderApi/Create/")]
        public OrderViewModel Create()
        {
            var result = service.getCurrentId();
            return result;
        }
    }
}