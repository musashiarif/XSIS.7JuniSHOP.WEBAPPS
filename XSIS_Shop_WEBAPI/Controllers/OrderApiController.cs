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
        public List<OrderViewModel> GetOrder()
        {
            var result = service.GetAllOrder();
            return result;
        }

        [HttpGet]
        public OrderViewModel get(int id)
        {
            var result = service.getDetailByID(id);
            return result;
        }
        [HttpGet]
        public List<OrderViewModel> searchOrders(string id)
        {
            
            string[] Parameters = id.Split('|');

            string param1 = Parameters[0];
            string param2 = Parameters[1];
            string param3 = Parameters[2];
//            string replaseDate = param2.Replace("-", "/");
            var result = service.SearchOrder(param1, param2, param3);
            return result;
        }
    }
}