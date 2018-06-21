using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSIS.Shop.viewModel
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        [Display(Name = "ID Pemesanan")]
        public int OrderId { get; set; }
        [Display(Name = "Nomor Pemesanan")]
        public string OrderNumber { get; set; }
        [Display(Name = "Product Item")]
        public int ProductId { get; set; }
        [Display(Name = "Nama Produk")]
        public string ProductName { get; set; }
        [Display(Name = "ID Customer")]
        public int CustomerId { get; set; }
        [Display(Name = "Nama Pemesan")]
        public string CustomerName { get; set; }
        [Display(Name = "Harga")]
        public Nullable<decimal> UnitPrice { get; set; }
        [Display(Name = "Jumlah")]
        public int Quantity { get; set; }
        [Display(Name = "Total")]
        public Nullable<decimal> TotalAmount { get; set; }
    }
}
