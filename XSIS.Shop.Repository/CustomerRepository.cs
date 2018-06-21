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
    public class CustomerRepository
    {
        public List<CustomerViewModel> GetAllCustomer()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var list = db.Customer.ToList();
                List<CustomerViewModel> ListVM = new List<CustomerViewModel>();
                foreach (var item in list)
                {
                    CustomerViewModel viewModel = new CustomerViewModel();
                    viewModel.Id = item.Id; //klo di edit harus semuanya ada
                    viewModel.FirstName = item.FirstName;
                    viewModel.LastName = item.LastName;
                    viewModel.namaLengkap = item.FirstName + " " + item.LastName;
                    viewModel.City = item.City;
                    viewModel.Country = item.Country;
                    viewModel.Phone = item.Phone;
                    viewModel.Email = item.Email;

                    ListVM.Add(viewModel);
                }
                return (ListVM);
            }
        }
        //select * from customer here id = id
        public CustomerViewModel getCustomerByID(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Customer customer = db.Customer.Find(id);

                CustomerViewModel viewModel = new CustomerViewModel();
                viewModel.Id = customer.Id; //klo di edit harus semuanya ada
                viewModel.FirstName = customer.FirstName;
                viewModel.LastName = customer.LastName;
                viewModel.City = customer.City;
                viewModel.Country = customer.Country;
                viewModel.Phone = customer.Phone;
                viewModel.Email = customer.Email;
                return viewModel;
            }                
        }

        public void CreateCustomer(CustomerViewModel customer)
        {
            using(ShopDBEntities db = new ShopDBEntities())
            {
                Customer model = new Customer();                                
                model.FirstName = customer.FirstName;
                model.LastName = customer.LastName;
                model.City = customer.City;
                model.Country = customer.Country;
                model.Phone = customer.Phone;
                model.Email = customer.Email;

                db.Customer.Add(model);
                db.SaveChanges();
            }            
        }

        public void EditCustomer(CustomerViewModel customer)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Customer model = new Customer();
                model.Id = customer.Id; //klo di edit harus semuanya ada
                model.FirstName = customer.FirstName;
                model.LastName = customer.LastName;
                model.City = customer.City;
                model.Country = customer.Country;
                model.Phone = customer.Phone;
                model.Email = customer.Email;

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }            
        }
        
        public void DeleteCustomer(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Customer customer = db.Customer.Find(id);
                db.Customer.Remove(customer);
                db.SaveChanges();
            }                
        }

        public List<CustomerViewModel> SearchCustomer(string nama, string cityCountry, string email)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var list = db.Customer.ToList();
                List<CustomerViewModel> ListVM = new List<CustomerViewModel>();
                foreach (var item in list)
                {
                    if (string.IsNullOrEmpty(item.Email))
                    {
                        item.Email = " ";
                    }
                    if (
                        (item.FirstName.ToLower().Contains(nama.ToLower()) || item.LastName.ToLower().Contains(nama.ToLower()) || (item.FirstName.ToLower() + " " + item.LastName.ToLower()).Contains(nama.ToLower()) || string.IsNullOrEmpty(nama) || string.IsNullOrWhiteSpace(nama))
                        && 
                        (item.City.ToLower().Contains(cityCountry.ToLower()) || item.Country.ToLower().Contains(cityCountry.ToLower()) || string.IsNullOrEmpty(cityCountry) || string.IsNullOrWhiteSpace(cityCountry)
                        && 
                        (item.Email.ToLower().Contains(cityCountry.ToLower()) || string.IsNullOrEmpty(email)|| string.IsNullOrWhiteSpace(email))
                      ))
                    {
                        CustomerViewModel viewModel = new CustomerViewModel();
                        viewModel.Id = item.Id; //klo di edit harus semuanya ada
                        viewModel.FirstName = item.FirstName;
                        viewModel.LastName = item.LastName;
                        viewModel.City = item.City;
                        viewModel.Country = item.Country;
                        viewModel.Phone = item.Phone;
                        viewModel.Email = item.Email;
                        ListVM.Add(viewModel);
                    }
                                        
                }
                return (ListVM);
            }
        }

        public bool CekNama(string namaDepan, string namaBelakang)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var result = (from a in db.Customer
                              where a.FirstName == namaDepan && a.LastName == namaBelakang
                              select a).SingleOrDefault();
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }            
        }


        public bool cekEmail(string email)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                if (email == null)
                {
                    return false;
                }

                var result = (from a in db.Customer
                        where a.Email == email
                        select a).SingleOrDefault();
                
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


    }
}
