using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryDetails(string categoryName)
        {
            categoryName = categoryName.Replace("-", " ");
            var model = new CategoryDao().GetCategoryByName(categoryName);
            ViewBag.ChildCategory = new CategoryDao().GetCategoryChild(model.Id);
            ViewBag.ProductsCategory = new ProductDao().GetProductsByCategory(model.Id);
            return View(model);
        }
        [ChildActionOnly]
        public ActionResult Block()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult BlockCategory()
        {
            var model = new CategoryDao().GetCategories();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult BlockManufacture()
        {
            var model = new ManufacturerDao().GetManufacturers();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult BlockPopularTags()
        {
            var model = new ProductTagDao().GetProductTags();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult BlockRecentlyViewedProducts()
        {
            var model = new ProductDao().GetProductsRelated(1);
            return PartialView(model);
        }

    }
}