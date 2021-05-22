using Models.Dao;
using OnlineShop.Models;
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

            var recentlyVievedProducts = Session[OnlineShop.Common.CommonConstants.RecentlyVievedSession];
            var listrecentlyVievedProduct = new List<RecentlyViewedProducts>();
            if (recentlyVievedProducts != null)
            {
                listrecentlyVievedProduct = (List<RecentlyViewedProducts>)recentlyVievedProducts;
                if (!listrecentlyVievedProduct.Exists(x=>x.Product.Id == model.Id))
                {
                    var recentlyViewedProducts = new RecentlyViewedProducts();
                    recentlyViewedProducts.Product = model;

                    listrecentlyVievedProduct.Add(recentlyViewedProducts);
                }
                Session[OnlineShop.Common.CommonConstants.RecentlyVievedSession] = listrecentlyVievedProduct;
            }
            return View(model);
        }

        public ActionResult RecentlyViewedProducts()
        {
            var recentlyVievedProducts = Session[OnlineShop.Common.CommonConstants.RecentlyVievedSession];
            var listrecentlyVievedProduct = new List<RecentlyViewedProducts>();
            if (recentlyVievedProducts != null)
            {
                listrecentlyVievedProduct = (List<RecentlyViewedProducts>)recentlyVievedProducts;
            }
            return View(listrecentlyVievedProduct);
        }

        public ActionResult QuickViewData(int productId)
        {
            var model = new ProductDao().GetProductById(productId);
            return PartialView(model);
        }
    }
}