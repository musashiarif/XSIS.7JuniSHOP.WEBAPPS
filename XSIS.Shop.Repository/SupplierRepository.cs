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
    public class SupplierRepository
    {
        public List<SupplierViewModel> GetAllSupplier()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var list = db.Supplier.ToList();
                List<SupplierViewModel> ListVM = new List<SupplierViewModel>();
                foreach (var item in list)
                {
                    SupplierViewModel viewModel = new SupplierViewModel();
                    viewModel.Id = item.Id; //klo di edit harus semuanya ada
                    viewModel.CompanyName = item.CompanyName;
                    viewModel.ContactName = item.ContactName;
                    viewModel.ContactTitle = item.ContactName;
                    viewModel.City = item.City;
                    viewModel.Country = item.Country;
                    viewModel.Phone = item.Phone;
                    viewModel.Fax = item.Fax;

                    ListVM.Add(viewModel);
                }
                return (ListVM);
            }
        }

        public SupplierViewModel getSupplierByID(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Supplier Supplier = db.Supplier.Find(id);

                SupplierViewModel viewModel = new SupplierViewModel();
                viewModel.Id = Supplier.Id; //klo di edit harus semuanya ada
                viewModel.CompanyName = Supplier.CompanyName;
                viewModel.ContactName = Supplier.ContactName;
                viewModel.ContactTitle = Supplier.ContactName;
                viewModel.City = Supplier.City;
                viewModel.Country = Supplier.Country;
                viewModel.Phone = Supplier.Phone;
                viewModel.Fax = Supplier.Fax;
                viewModel.product = (from a in db.Product
                                     where a.SupplierId == id
                                     select new ProductViewModel
                                     {
                                         ProductName = a.ProductName,
                                         UnitPrice = a.UnitPrice,
                                         Package = a.Package,
                                         IsDiscontinued = a.IsDiscontinued
                                     }).ToList();
                return viewModel;
            }
        }

        public void CreateSupplier(SupplierViewModel supplier)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Supplier model = new Supplier();
                model.CompanyName = supplier.CompanyName;
                model.ContactName = supplier.ContactName;
                model.ContactTitle = supplier.ContactTitle;
                model.City = supplier.City;
                model.Country = supplier.Country;
                model.Phone = supplier.Phone;
                model.Fax = supplier.Fax;

                db.Supplier.Add(model);
                db.SaveChanges();
            }
        }

        public void EditSupplier(SupplierViewModel supplier)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Supplier model = new Supplier();
                model.Id = supplier.Id;
                model.CompanyName = supplier.CompanyName;
                model.ContactName = supplier.ContactName;
                model.ContactTitle = supplier.ContactTitle;
                model.City = supplier.City;
                model.Country = supplier.Country;
                model.Phone = supplier.Phone;
                model.Fax = supplier.Fax;

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteSupplier(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Supplier supplier = db.Supplier.Find(id);
                db.Supplier.Remove(supplier);
                db.SaveChanges();
            }
        }

        public List<ProductViewModel> listProduct(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {

                var result = (from a in db.Product
                              where a.SupplierId == id
                              select new ProductViewModel {
                                  ProductName = a.ProductName,
                                  UnitPrice = a.UnitPrice,
                                  Package = a.Package,
                                  IsDiscontinued = a.IsDiscontinued
                              }).ToList();
                
                List<ProductViewModel> ListVM = new List<ProductViewModel>();
                foreach (var item in result)
                {
                    ProductViewModel viewModel = new ProductViewModel();
                    viewModel.Id = item.Id;
                    viewModel.ProductName = item.ProductName;
                    viewModel.UnitPrice = item.UnitPrice;
                    viewModel.Package = item.Package;
                    viewModel.IsDiscontinued= item.IsDiscontinued;

                    ListVM.Add(viewModel);
                }
                return ListVM;
            }

        }
        


    }
}
