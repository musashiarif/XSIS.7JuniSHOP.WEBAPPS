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
    public class ProductRepository
    {
        public List<ProductViewModel> GetAllProduct()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var product = db.Product.Include(p => p.Supplier);
                var list = db.Product.ToList();
                List<ProductViewModel> ListVM = new List<ProductViewModel>();
                foreach (var item in list)
                {
                    ProductViewModel viewModel = new ProductViewModel();
                    viewModel.Id = item.Id;
                    viewModel.ProductName = item.ProductName;
                    viewModel.SupplierId = item.SupplierId;
                    viewModel.UnitPrice = item.UnitPrice;
                    viewModel.Package = item.Package;
                    viewModel.IsDiscontinued = item.IsDiscontinued;
                    viewModel.SupplierName = item.Supplier.CompanyName;

                    ListVM.Add(viewModel);
                }
                return (ListVM);
            }
        }

        public ProductViewModel getProductByID(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product product = db.Product.Find(id);

                ProductViewModel viewModel = new ProductViewModel();
                viewModel.Id = product.Id;
                viewModel.ProductName = product.ProductName;
                viewModel.UnitPrice = product.UnitPrice;
                viewModel.Package = product.Package;
                viewModel.IsDiscontinued = product.IsDiscontinued;
                viewModel.SupplierName = product.Supplier.CompanyName;
                return viewModel;
            }
        }

        public void CreateProduct(ProductViewModel product)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product model = new Product();
                model.ProductName = product.ProductName;
                model.SupplierId = product.SupplierId;
                model.UnitPrice = product.UnitPrice;
                model.Package = product.Package;
                model.IsDiscontinued = product.IsDiscontinued;

                db.Product.Add(model);
                db.SaveChanges();
            }
        }
        public void EditProduct(ProductViewModel product)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product model = new Product();
                model.Id = product.Id;
                model.ProductName = product.ProductName;
                model.SupplierId = product.SupplierId;
                model.UnitPrice = product.UnitPrice;
                model.Package = product.Package;
                model.IsDiscontinued = product.IsDiscontinued;

                db.Product.Add(model);
                db.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product product = db.Product.Find(id);
                db.Product.Remove(product);
                db.SaveChanges();

            }
        }

        public List<Supplier> ListSupplier()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var list = db.Supplier.ToList();
                return list;
            }
        }
    }
}

