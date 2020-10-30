using Models.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class RegisterModel
    {
        private ICollection<CustomerPassword> _customerPasswords;
        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        [StringLength(1000,MinimumLength =6)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public virtual ICollection<CustomerPassword> CustomerPasswords
        {
            get { return _customerPasswords ?? (_customerPasswords = new List<CustomerPassword>()); }
            protected set { _customerPasswords = value; }
        }
    }
}