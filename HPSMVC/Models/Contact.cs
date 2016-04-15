using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HPSMVC.Models
{
    public class Contact
    {
        public int ID { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "You cannot leave the Address blank.")]
        [StringLength(100, ErrorMessage = "The Address cannot be more than 100 characters")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "You cannot leave the City blank.")]
        [StringLength(50, ErrorMessage = "The City cannot be more than 50 characters")]
        public string City { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "You cannot leave the Province blank.")]
        [StringLength(25, ErrorMessage = "The Province cannot be more than 25 characters")]
        public string Province { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "You cannot leave the Postal Code blank.")]
        [StringLength(7, ErrorMessage = "The Postal Code cannot be more than 7 characters")]
        public string PostalCode { get; set; }

        [Display(Name = "Telephone")]
        [Required(ErrorMessage = "You cannot leave the Telephone blank.")]
        [StringLength(20, ErrorMessage = "The Telephone cannot be more than 20 characters")]
        public string Telephone { get; set; }

        [Display(Name = "Fax")]
        [StringLength(20, ErrorMessage = "The Fax cannot be more than 20 characters")]
        public string Fax { get; set; }

        [Display(Name = "Hours")]
        [Required(ErrorMessage = "You cannot leave the Hours blank.")]
        [StringLength(50, ErrorMessage = "The Hours cannot be more than 50 characters")]
        public string Hours { get; set; }

        [Display(Name = "Message")]
        [StringLength(50, ErrorMessage = "The Message cannot be more than 50 characters")]
        public string Message { get; set; }
    }
}