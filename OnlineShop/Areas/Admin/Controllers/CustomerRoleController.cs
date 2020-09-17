using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CustomerRoleController : BaseController
    {
        // GET: Admin/CustomerRole
        public ActionResult Index(string searchString , int pageNumber=1 , int pageSize=10)
        {
            var roleDao = new CustomerRoleDao();
            var customerRole = roleDao.GetCustomerRoles(searchString,pageNumber,pageSize);
            ViewBag.SearchString = searchString;
            return View(customerRole);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CustomerRole customerRole)
        {
            if (!String.IsNullOrWhiteSpace(customerRole.Name))
            {
                var customerRol = new CustomerRoleDao().GetCustomerRoleByName(customerRole.Name);
                if (customerRol != null)
                {
                    ModelState.AddModelError("", "Name Customer Role is already registed");
                }
            }
            if (ModelState.IsValid)
            {
                var roleDao = new CustomerRoleDao();
                int id = roleDao.InsertCustomerRole(customerRole);
                if (id > 0)
                {
                    SetNotification("Thêm mới nhóm người dùng thành công .", "success");
                    return RedirectToAction("Index", "CustomerRole");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới nhóm người dùng không thành công .");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var customer = new CustomerRoleDao().GetCustomerRoleById(id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(CustomerRole customerRole) 
        {
            if (ModelState.IsValid)
            {
                var roleDao = new CustomerRoleDao();
                var result = roleDao.UpdateCustomerRole(customerRole);
                if (result)
                {
                    SetNotification("Cập nhật nhóm người dùng thành công .", "success");
                    return RedirectToAction("Index", "CustomerRole");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật nhóm người dùng không thành công .");
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var role = new CustomerRoleDao().DeleteCustomerRole(id);
            return RedirectToAction("Index");
        }
    }
}