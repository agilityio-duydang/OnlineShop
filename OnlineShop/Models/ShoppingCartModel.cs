using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class ShoppingCartModel
    {
        public ShoppingCartModel()
        {
            Items = new List<ShoppingCartItemModel>();
        }
        public List<ShoppingCartItemModel> Items { get; set; }

        public AddressModel BillingAddress { get; set; }

        public AddressModel ShippingAddress { get; set; }

        public AddressModel PickupAddress { get; set; }

        public string ShippingMethod { get; set; }

        public string PaymentMethod { get; set; }
    }
}