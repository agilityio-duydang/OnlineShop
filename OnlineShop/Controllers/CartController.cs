using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private const string ShoppingCartSession = "ShoppingCartSession";
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProductToCartAjax(int productId ,int quantity , bool isAddToCartButton)
        {
            var model = new ProductDao().GetProductById(productId);
            return PartialView(model);
        }
        
        public ActionResult NopAjaxCartFlyoutShoppingCart()
        {
            return View();
        }
        public ActionResult MiniShoppingCart()
        {
            return View();
        }
    }
}