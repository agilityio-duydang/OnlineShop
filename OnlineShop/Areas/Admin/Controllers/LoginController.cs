using Microsoft.Ajax.Utilities;
using Models;
using Models.Dao;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var customerDao = new CustomerDao();
                var loginResult = customerDao.Login(loginModel.UserName, Encryptor.MD5Hash(loginModel.PassWord));
                switch (loginResult)
                {
                    case CustomerLoginResults.Successful:
                        var customer = customerDao.GetCustomerByUserName(loginModel.UserName);
                        var customerSession = new UserLogin();
                        customerSession.UserId = customer.Id;
                        customerSession.UserName = customer.Username;
                        Session.Add(CommonConstants.USER_SESSION, customerSession);
                        return RedirectToAction("Index", "Home");

                    case CustomerLoginResults.CustomerNotExist:
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                        break;
                    case CustomerLoginResults.WrongPassword:
                        ModelState.AddModelError("", "Mật khẩu đăng nhập không hợp lệ");
                        break;
                    case CustomerLoginResults.NotActive:
                        ModelState.AddModelError("", "Tài khoản chưa kích hoạt");
                        break;
                    case CustomerLoginResults.Deleted:
                        ModelState.AddModelError("", "Tài khoản bị xoá");
                        break;
                    case CustomerLoginResults.NotRegistered:
                        ModelState.AddModelError("", "Tài khoản chưa đăng ký");
                        break;
                    case CustomerLoginResults.LockedOut:
                        ModelState.AddModelError("", "Tài khoản đang bị khoá");
                        break;
                    default:
                        break;
                }   
            }
            return View("Index");
        }
    }
}