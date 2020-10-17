using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductDetails(string productName)
        {
            productName = productName.Replace("-", " ");
            var model = new ProductDao().GetProductByName(productName);            
            ViewBag.ProductsRelated = new ProductDao().GetProductsRelated(model.Id);
            return View(model);
        }

        public ActionResult QuickViewData(int productId)
        {
            var model = new ProductDao().GetProductById(productId);
            return PartialView(model);
        }
    }
}