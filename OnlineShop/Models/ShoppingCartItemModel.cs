using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    [Serializable]
    public class ShoppingCartItemModel
    {
        public  Product Product  { get; set; }
        public int Quantity { get; set; }

        public int ShoppingCartTypeId { get; set; }
    }
}