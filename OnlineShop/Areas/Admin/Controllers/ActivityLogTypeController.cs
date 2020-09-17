using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ActivityLogTypeController : Controller
    {
        // GET: Admin/ActivityLogType
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var model = new ActivityLogTypeDao().GetActivityLogTypes(page, pageSize);
            return View(model);
        }
    }
}