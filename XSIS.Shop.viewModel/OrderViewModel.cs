using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace XSIS.Shop.viewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Tanggal Pemesanan")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime OrderDate { get; set; }
        [Display(Name = "Nomor Pemesanan")]
        public string OrderNumber { get; set; }
        [Display(Name = "ID Pemesan")]
        public int CustomerId { get; set; }
        [Display(Name = "Nama Pemesan")]
        public string CustomerName { get; set; }
        [Display(Name = "Total")]
        public Nullable<decimal> TotalAmount { get; set; }
        
         public List<OrderItemViewModel> OrderItem { get; set; }
    }

}
