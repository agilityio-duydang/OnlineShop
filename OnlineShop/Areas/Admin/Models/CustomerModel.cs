using Models.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Models
{
    public class CustomerModel
    {
        private ICollection<CustomerRole> _customerRoles;
        private ICollection<ShoppingCartItem> _shoppingCartItems;
        private ICollection<Address> _addresses;
        private ICollection<Order> _orders;
        private ICollection<ActivityLog> _activityLogs;
        private ICollection<CustomerPassword> _customerPasswords;
        public CustomerModel()
        {
            this.SelectedCustomerRoleIds = new List<int>();
            this.AvailableCustomerRoles = new List<SelectListItem>();
            this.AvailableVendors = new List<SelectListItem>();
        }
        public int Id { get; set; }

        public Guid CustomerGuid { get; set; }

        [Required]
        [StringLength(1000)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        public string Company { get; set; }

        [Required]
        [StringLength(1000)]
        public string Email { get; set; }

        [StringLength(1000)]
        public string EmailToRevalidate { get; set; }

        public string AdminComment { get; set; }

        public IList<int> SelectedCustomerRoleIds { get; set; }

        public int AffiliateId { get; set; }

        public int VendorId { get; set; }

        public bool HasShoppingCartItems { get; set; }

        public bool RequireReLogin { get; set; }

        public int FailedLoginAttempts { get; set; }

        public DateTime? CannotLoginUntilDateUtc { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public bool IsSystemAccount { get; set; }

        [StringLength(400)]
        public string SystemName { get; set; }

        public string LastIpAddress { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime? LastLoginDateUtc { get; set; }

        public DateTime LastActivityDateUtc { get; set; }

        public int? BillingAddress_Id { get; set; }

        public int? ShippingAddress_Id { get; set; }

        public IList<SelectListItem> AvailableCustomerRoles { get; set; }

        public IList<SelectListItem> AvailableVendors { get; set; }

        /// <summary>
        /// Gets or sets the customer roles
        /// </summary>
        public virtual ICollection<CustomerRole> CustomerRoles
        {
            get { return _customerRoles ?? (_customerRoles = new List<CustomerRole>()); }
            protected set { _customerRoles = value; }
        }
        /// <summary>
        /// Gets or sets shopping cart items
        /// </summary>
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems
        {
            get { return _shoppingCartItems ?? (_shoppingCartItems = new List<ShoppingCartItem>()); }
            protected set { _shoppingCartItems = value; }
        }

        /// <summary>
        /// Gets or sets customer addresses
        /// </summary>
        public virtual ICollection<Address> Addresses
        {
            get { return _addresses ?? (_addresses = new List<Address>()); }
            protected set { _addresses = value; }
        }

        public virtual ICollection<Order> Orders
        {
            get { return _orders ?? (_orders = new List<Order>()); }
            protected set { _orders = value; }
        }

        public virtual ICollection<ActivityLog> ActivityLogs
        {
            get { return _activityLogs ?? (_activityLogs = new List<ActivityLog>()); }
            protected set { _activityLogs = value; }
        }

        public virtual ICollection<CustomerPassword> CustomerPasswords
        {
            get { return _customerPasswords ?? (_customerPasswords = new List<CustomerPassword>()); }
            protected set { _customerPasswords = value; }
        }
    }
}