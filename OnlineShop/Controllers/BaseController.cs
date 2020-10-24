using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    public abstract class BaseController : System.Web.Mvc.Controller
    {
        #region Notifications

        /// <summary>
        /// Error's JSON data
        /// </summary>
        /// <param name="error">Error text</param>
        /// <returns>Error's JSON data</returns>
        protected JsonResult ErrorJson(string error)
        {
            return Json(new
            {
                error = error
            });
        }

        /// <summary>
        /// Error's JSON data
        /// </summary>
        /// <param name="errors">Error messages</param>
        /// <returns>Error's JSON data</returns>
        protected JsonResult ErrorJson(object errors)
        {
            return Json(new
            {
                error = errors
            });
        }

        #endregion

        #region Security

        /// <summary>
        /// Access denied view
        /// </summary>
        /// <returns>Access denied view</returns>
        protected virtual ActionResult AccessDeniedView()
        {
            //return Challenge();
            return RedirectToAction("AccessDenied", "Security");
        }
        #endregion
    }
}