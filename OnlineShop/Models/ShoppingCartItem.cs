using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    [Serializable]
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}