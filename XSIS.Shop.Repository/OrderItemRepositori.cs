using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XSIS.Shop.Model;
using XSIS.Shop.viewModel;



namespace XSIS.Shop.Repository
{
    public class OrderItemRepositori
    {
        public OrderViewModel temporaryList(OrderItemViewModel orderitemVM, List<OrderItemViewModel> ListItem)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product produk = db.Product.Find(orderitemVM.ProductId);
                OrderViewModel order = new OrderViewModel();
                order.OrderItem = ListItem;
                foreach (var item in order.OrderItem)
                {
                    if (item.ProductId == orderitemVM.ProductId)
                    {
                        item.Quantity += orderitemVM.Quantity;
                        item.TotalAmount = item.Quantity * item.UnitPrice;
                        return order;
                    }
                }
                decimal harga = produk.UnitPrice.HasValue ? produk.UnitPrice.Value : 0;
                int Id = (db.Order.ToList().Count != 0) ?
                    (from o in db.Order orderby o.Id descending select o.Id).First() + 1 : 1;
                orderitemVM.ProductName = produk.ProductName;
                orderitemVM.OrderId = Id;
                orderitemVM.UnitPrice = harga;
                orderitemVM.TotalAmount = orderitemVM.Quantity * orderitemVM.UnitPrice;

                int idItem = (order.OrderItem.Count != 0) ?
                    (from o in order.OrderItem orderby o.Id descending select o.Id).First() + 1 : 1;
                orderitemVM.Id = idItem;
                order.OrderItem.Add(orderitemVM);
                return order;
            }
        }

        public List<Product> GetProduct()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                List<Product> result = db.Product.ToList();
                return result;
            }
        }

        public List<OrderItemViewModel> RemoveItem(int id, List<OrderItemViewModel> list)
        {
            OrderItemViewModel VM = (from a in list
                                     where a.Id == id
                                     select a).FirstOrDefault();
            list.Remove(VM);
            return list;
        }

    }
}
