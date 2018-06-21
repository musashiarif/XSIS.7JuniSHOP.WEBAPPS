using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace XSIS.SHOP.WEBAPPS.ViewModel
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nama Perusahaan")]
        [Required(ErrorMessage = "Nama Perusahaan Harus Diisi")]
        [StringLength(40, ErrorMessage = "Karakter Maksimal 40 huruf")]
        public string CompanyName { get; set; }

        [Display(Name = "Nama Kontak")]
        [StringLength(50, ErrorMessage = "Karakter Maksimal 50 huruf")]
        public string ContactName { get; set; }

        [Display(Name = "Gelar")]
        [StringLength(40, ErrorMessage = "Karakter Maksimal 40 huruf")]
        public string ContactTitle { get; set; }

        [Display(Name = "Kota")]
        [StringLength(40, ErrorMessage = "Karakter Maksimal 40 huruf")]
        public string City { get; set; }

        [Display(Name = "Negara")]
        [StringLength(40, ErrorMessage = "Karakter Maksimal 40 huruf")]
        public string Country { get; set; }

        [Display(Name = "Nomor Telepon")]
        [StringLength(30, ErrorMessage = "Karakter Maksimal 30 huruf")]
        public string Phone { get; set; }

        [Display(Name = "Nomor Faksimile")]
        [StringLength(30, ErrorMessage = "Karakter Maksimal 30 huruf")]
        public string Fax { get; set; }
    }
}