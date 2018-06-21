using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace XSIS.Shop.viewModel
{
    public class ProductViewModel
    {
        
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nama Produk")]
        [StringLength(50, ErrorMessage = "Karakter maksimal 50 huruf")]
        public string ProductName { get; set; }
        [Display(Name = "ID Supplier")]
        public int SupplierId { get; set; }
        [Display(Name = "Nama Supplier")]
        public string SupplierName { get; set; }
        [Display(Name = "Harga Unit")]
        public Nullable<decimal> UnitPrice { get; set; }
        [Display(Name = "Paket")]
        public string Package { get; set; }
        [Display(Name = "Tidak Diproduksi Lagi")]
        public bool IsDiscontinued { get; set; }
    }
}
