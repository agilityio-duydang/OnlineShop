using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ActivityLogController : BaseController
    {
        // GET: Admin/ActivityLog
        public ActionResult Index(DateTime? CreateFrom, DateTime? CreateTo, int ActivityLogTypeId = 0, string SearchIpAddess = null, int page = 1, int pageSize = 10)
        {
            var createFrom = CreateFrom ?? DateTime.Now;
            var createTo = CreateTo ?? DateTime.Now;
            var model = new ActivityLogDao().GetActivityLogs(createFrom, createTo, ActivityLogTypeId, SearchIpAddess, page, pageSize);
            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var result = new ActivityLogDao().DeleteActivityLog(Id);
            if (result)
            {
                SetNotification("Xoá Activity Log thành công .", "success");
                return RedirectToAction("Index", "ActivityLog");
            }
            else
            {
                ModelState.AddModelError("", "Xoá Activity Log không thành công .");
            }
            return View();
        }
    }
}