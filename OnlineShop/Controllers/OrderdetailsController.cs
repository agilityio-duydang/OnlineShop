using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class OrderdetailsController : Controller
    {
        // GET: Orderdetails
        public ActionResult Index(int Id)
        {
            return View();
        }

        public ActionResult Print(int Id)
        {
            return View();
        }

        public ActionResult Pdf(int Id)
        {
            return View();
        }
    }
}