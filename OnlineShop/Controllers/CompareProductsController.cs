using Models.Dao;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CompareProductsController : Controller
    {
        private const string CompareProductsSession = "CompareProductsSession";

        // GET: CompareProducts
        public ActionResult Index()
        {
            var compareProduct = Session[CompareProductsSession];
            var listCompareProducts = new List<CompareProductsItem>();
            if (compareProduct != null)
            {
                listCompareProducts = (List<CompareProductsItem>)compareProduct;
            }
            return View(listCompareProducts);
        }

        [HttpPost]
        public ActionResult Add(int productId)
        {
            var listCompareProducts = new List<CompareProductsItem>();
            var model = new ProductDao().GetProductById(productId);

            var compareProduct = Session[CompareProductsSession];
            if (compareProduct != null)
            {
                listCompareProducts = (List<CompareProductsItem>)compareProduct;
                if (!listCompareProducts.Exists(x => x.Product.Id == productId))
                {
                    var compareProductItem = new CompareProductsItem();
                    compareProductItem.Product = model;

                    listCompareProducts.Add(compareProductItem);
                }
                Session[CompareProductsSession] = listCompareProducts;
            }
            else
            {
                var compareProductItem = new CompareProductsItem();
                compareProductItem.Product = model;

                listCompareProducts.Add(compareProductItem);

                Session[CompareProductsSession] = listCompareProducts;
            }
            return Json(new
            {
                success = true,
                Message = "The product has been added to your product comparison"
            });
        }
        public JsonResult Remove(int productId)
        {
            var compareProduct = (List<CompareProductsItem>)Session[CompareProductsSession];
            compareProduct.RemoveAll(x => x.Product.Id == productId);
            Session[CompareProductsSession] = compareProduct;
            return Json(new
            {
                Status = true
            });
        }
        public JsonResult ClearCompareList()
        {
            var compareProduct = (List<CompareProductsItem>)Session[CompareProductsSession];
            compareProduct.Clear();
            Session[CompareProductsSession] = compareProduct;
            return Json(new
            {
                Status = true,
                Message = "Clear list compare product success"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}