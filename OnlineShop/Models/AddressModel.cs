using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class AddressModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Company { get; set; }

        public string City { get; set; }

        [Required]
        public string Address1 { get; set; }

        [Required]
        public string Address2 { get; set; }

        [Required]
        public string ZipPostalCode { get; set; }

        [Required]
        public string PhoneNumber { get; set; }


        public string FaxNumber { get; set; }


        public DateTime CreatedOnUtc { get; set; }
    }
}