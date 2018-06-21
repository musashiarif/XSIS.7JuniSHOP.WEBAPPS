using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSIS.Shop.Model;
using XSIS.Shop.viewModel;

namespace XSIS.Shop.Repository
{
    public class OrderRepository
    {
        public List<OrderViewModel> GetAllOrder()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var list = db.Order.ToList();
                List<OrderViewModel> ListVM = new List<OrderViewModel>();
                foreach (var item in list)
                {
                    OrderViewModel viewModel = new OrderViewModel();
                    viewModel.Id = item.Id;
                    viewModel.CustomerName = item.Customer.FirstName+" "+item.Customer.LastName;
                    viewModel.OrderDate = item.OrderDate;
                    viewModel.OrderNumber = item.OrderNumber;
                    viewModel.TotalAmount = item.TotalAmount;

                    ListVM.Add(viewModel);
                }
                return (ListVM);
            }
        }

        public OrderViewModel getDetailByID(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                //Order order = db.Order.Find(id);

                OrderViewModel viewModel = new OrderViewModel();       
                          viewModel = (from a in db.Order
                                       join d in db.Customer on a.CustomerId equals d.Id
                                       where a.Id == id
                                       select new OrderViewModel
                                       {
                                         Id = a.Id,
                                         OrderDate = a.OrderDate,
                                         OrderNumber = a.OrderNumber,
                                         CustomerName = a.Customer.FirstName +" "+ a.Customer.LastName,
                                         CustomerId = d.Id,
                                         TotalAmount = a.TotalAmount
                                       }).Single();
                
                viewModel.OrderItem = (from a in db.Order
                                       join b in db.OrderItem on a.Id equals b.OrderId
                                       join c in db.Product on b.ProductId equals c.Id
                                       where a.Id == id
                                       select new OrderItemViewModel
                                       {
                                           Id = b.Id,
                                           OrderId = b.OrderId,
                                           OrderNumber = a.OrderNumber,                                           
                                           ProductId = c.Id,                                
                                           ProductName = c.ProductName,
                                           UnitPrice = c.UnitPrice,
                                           Quantity = b.Quantity,
                                           TotalAmount = c.UnitPrice * b.Quantity
                                       }).ToList();
                return viewModel;
            }          
        }

        public void createOrder(OrderViewModel order)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                int Id = (db.Order.ToList().Count != 0) ?
                    (from o in db.Order orderby o.Id descending select o.Id).First() + 1 : 1;
                Order model = new Order();
                model.OrderDate = order.OrderDate;
                model.OrderNumber = order.OrderNumber;
                model.CustomerId = order.CustomerId;
                model.TotalAmount = order.TotalAmount;
                db.Order.Add(model);

                OrderItem orderitem = new OrderItem();
                foreach (var things in order.OrderItem)
                {
                    orderitem.OrderId = Id;
                    orderitem.ProductId = things.ProductId;
                    orderitem.UnitPrice = (decimal)things.UnitPrice;
                    orderitem.Quantity = things.Quantity;
                    db.OrderItem.Add(orderitem);
                    db.SaveChanges();
                }
            }
        }
        public void EditOrder(OrderViewModel order)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Order model = new Order();
                model.Id = order.Id;
                model.OrderDate = order.OrderDate;
                model.OrderNumber = order.OrderNumber;
                model.CustomerId = order.CustomerId;
                model.TotalAmount = order.TotalAmount;
                model.CustomerId = order.CustomerId;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void DeleteOrder(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Order order = db.Order.Find(id);
                db.Order.Remove(order);
                db.SaveChanges();
            }
        }

        //public void EditCustomer(OrderViewModel order)
        //{
        //    using (ShopDBEntities db = new ShopDBEntities())
        //    {
        //        OrderViewModel model = new OrderViewModel();
        //        model.CustomerId = order.CustomerId;
        //        model.OrderDate = order.OrderDate.ToString();
        //        model.OrderNumber = order.OrderNumber;
        //        model.TotalAmount = order.TotalAmount;

        //        db.Entry(model).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //}

        //public void DeleteCustomer(int id)
        //{
        //    using (ShopDBEntities db = new ShopDBEntities())
        //    {
        //        Order order = db.Order.Find(id);
        //        db.Order.Remove(order);
        //        db.SaveChanges();
        //    }
        //}
        public List<OrderViewModel> SearchOrder(string ordrNumb, string Date, string cust)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                
                var list = db.Order.ToList();
                List<OrderViewModel> ListVM = new List<OrderViewModel>();
                DateTime ordDate = new DateTime();
                if (!string.IsNullOrEmpty(Date))
                {
                    ordDate = DateTime.Parse(Date.Replace("-","/")).Date;
                }
                foreach (var item in list)
                {
                    if (
                        (item.OrderNumber.Contains(ordrNumb) || string.IsNullOrEmpty(ordrNumb)||string.IsNullOrWhiteSpace(ordrNumb))
                        &&
                        (string.IsNullOrWhiteSpace(Date) || string.IsNullOrEmpty(Date) || item.OrderDate == ordDate )
                        &&
                        (string.IsNullOrEmpty(cust)||string.IsNullOrWhiteSpace(cust)|| item.CustomerId == int.Parse(cust))
                      )
                    {
                        OrderViewModel viewModel = new OrderViewModel();
                        viewModel.Id = item.Id;
                        viewModel.OrderNumber = item.OrderNumber;
                        viewModel.CustomerId = item.CustomerId;
                        viewModel.CustomerName = item.Customer.FirstName+" "+ item.Customer.LastName;
                        viewModel.OrderDate = item.OrderDate;                        
                        viewModel.TotalAmount = item.TotalAmount;
                        ListVM.Add(viewModel);
                    }
                }
                return (ListVM);
            }
        }
        public List<CustomerViewModel> GetAllCustomer()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                List<CustomerViewModel> listVM = new List<CustomerViewModel>();
                foreach (var item in db.Customer.ToList())
                {
                    CustomerViewModel customer = new CustomerViewModel();
                    customer.Id = item.Id;
                    customer.FirstName = item.FirstName;
                    customer.LastName = item.LastName;
                    customer.namaLengkap = item.FirstName + " " + item.LastName;
                    customer.City = item.City;
                    customer.Country = item.Country;
                    customer.Phone = item.Phone;
                    customer.Email = item.Email;

                    listVM.Add(customer);
                }
                return (listVM);
            }
        }
        //public OrderViewModel getCurrentId()
        //{
        //    using(ShopDBEntities db = new ShopDBEntities())
        //    {
        //        OrderViewModel orderVM = new OrderViewModel();
        //        List<Order> OrderDB = db.Order.ToList();
        //        int id = (db.Order.ToList().Count != 0)?
        //    }
        //    return OrderVM;

        //}

        public List<Product> GetProduct()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var list = db.Product.ToList();
                return list;
            }
        }
    }
}
