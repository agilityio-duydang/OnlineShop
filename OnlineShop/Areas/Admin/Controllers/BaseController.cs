using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var session = (UserLogin)Session[CommonConstants.USER_SESSION];           
            //if (session == null)
            //{
            //    filterContext.Result = new RedirectToRouteResult(
            //        new RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            //}
            base.OnActionExecuting(filterContext);
        }
        protected void SetNotification(string message , string type)
        {
            TempData["NotificationMessage"] = message;
            if (type=="success")
            {
                TempData["NotificationType"] = "alert-success";
            }
            else if (type == "warnings")
            {
                TempData["NotificationType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["NotificationType"] = "alert-danger";
            }
        }
    }
}