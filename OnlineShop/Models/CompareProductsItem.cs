using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    [Serializable]
    public class CompareProductsItem
    {
        public Product Product { get; set; }
    }
}