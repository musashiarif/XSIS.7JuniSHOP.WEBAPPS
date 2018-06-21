using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace XSIS.SHOP.WEBAPPS.ViewModel
{
    public class CustomerViewModel
    {        
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nama Depan")]
        [StringLength(40, ErrorMessage ="Karakter maksimal 40 huruf")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Nama Belakang")]
        [StringLength(40, ErrorMessage = "Karakter maksimal 40 huruf")]
        public string LastName { get; set; }
        [Display(Name = "Kota")]
        [StringLength(40, ErrorMessage = "Karakter Maksimal 40 huruf")]
        public string City { get; set; }
        [Display(Name = "Negara")]
        [StringLength(40, ErrorMessage = "Karakter Maksimal 40 huruf")]
        public string Country { get; set; }
        [Display(Name = "No Telepon")]
        [RegularExpression("^-()0-9*$", ErrorMessage ="Karakter hanya boleh - () dan angka")]
        [StringLength(20, ErrorMessage = "Karakter Maksimal 20 huruf")]
        public string Phone { get; set; }
        [Display(Name = "E-Mail")]
        [StringLength(35, ErrorMessage = "Karakter Maksimal 35 huruf")]
        public string Email { get; set; }
    }
}