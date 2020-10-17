using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CompareProductsController : Controller
    {
        // GET: CompareProducts
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(int Id)
        {
            var product = new ProductDao().GetProductById(Id);

            return View();
        }
    }
}