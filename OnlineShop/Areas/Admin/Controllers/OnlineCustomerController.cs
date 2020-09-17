using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class OnlineCustomerController : Controller
    {
        // GET: Admin/OnlineCustomer
        public ActionResult Index(string SearchEmail, string SearchFirstName = null, string SearchLastName = null, int SearchDayOfBirth = 0, int SearchMonthOfBirth = 0, string SearchCompany = null, List<int> SearchCustomerRoleIds = null, int page = 1, int pageSize = 10)
        {
            var dao = new CustomerDao();
            var model = dao.GetCustomers(SearchEmail, SearchFirstName, SearchLastName, SearchDayOfBirth, SearchMonthOfBirth, SearchCompany, SearchCustomerRoleIds, page, pageSize);
            ViewBag.SearchEmail = SearchEmail;
            ViewBag.SearchFirstName = SearchFirstName;
            ViewBag.SearchLastName = SearchLastName;
            ViewBag.SearchDayOfBirth = SearchDayOfBirth;
            ViewBag.SearchMonthOfBirth = SearchMonthOfBirth;
            ViewBag.SearchCompany = SearchCompany;
            ViewBag.SearchCustomerRoleIds = SearchCustomerRoleIds;
            return View(model);
        }
    }
}