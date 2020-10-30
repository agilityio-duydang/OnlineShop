using Microsoft.AspNet.Identity;
using Models.Dao;
using Models.EF;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ShoppingCartItem = Models.EF.ShoppingCartItem;

namespace OnlineShop.Controllers
{
    public class UserController : Controller
    {

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session[CommonConstants.USER_SESSION] ==null)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var customerDao = new CustomerDao();
                var loginResult = customerDao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                switch (loginResult)
                {
                    case CustomerLoginResults.Successful:
                        var customer = customerDao.GetCustomerByUserName(model.UserName);
                        var customerSession = new UserLogin();
                        customerSession.UserId = customer.Id;
                        customerSession.UserName = customer.Username;
                        Session.Add(CommonConstants.USER_SESSION, customerSession);

                        var shoppingCart = Session[CommonConstants.ShoppingCartSession];
                        var listSessionCart = new List<Models.ShoppingCartItem>();
                        var listShoppingCart = new List<ShoppingCartItem>();
                        if (shoppingCart != null)
                        {
                            listSessionCart = (List<Models.ShoppingCartItem>)shoppingCart;

                            foreach (var item in listSessionCart)
                            {
                                if (customer.ShoppingCartItems.ToList().Exists(x => x.ProductId == item.Product.Id))
                                {
                                    foreach (var ite in customer.ShoppingCartItems)
                                    {
                                        if (item.Product.Id == ite.ProductId)
                                        {
                                            ite.Quantity += item.Quantity;
                                        }
                                    }
                                }
                                else
                                {
                                    var shoppingCartItem = new ShoppingCartItem();
                                    shoppingCartItem.ShoppingCartTypeId = 1;
                                    shoppingCartItem.CustomerEnteredPrice = item.Product.Price;
                                    shoppingCartItem.ProductId = item.Product.Id;
                                    shoppingCartItem.Quantity = item.Quantity;
                                    shoppingCartItem.CustomerId = customer.Id;
                                    shoppingCartItem.CreatedOnUtc = DateTime.UtcNow;
                                    shoppingCartItem.UpdatedOnUtc = DateTime.UtcNow;

                                    customer.ShoppingCartItems.Add(shoppingCartItem);
                                }
                            }
                            var result = customerDao.UpdateCustomer(customer);
                        }
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
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }
        public static string PublicIPAddress()
        {
            string uri = "http://checkip.dyndns.org/";
            string ip = String.Empty;

            using (var client = new HttpClient())
            {
                var result = client.GetAsync(uri).Result.Content.ReadAsStringAsync().Result;

                ip = result.Split(':')[1].Split('<')[0];
            }

            return ip;
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {

            if (!String.IsNullOrWhiteSpace(model.Username))
            {
                var customer = new CustomerDao().GetCustomerByUserName(model.Username);
                if (customer != null)
                {
                    ModelState.AddModelError("", "Username is already registered");
                }
            }
            if (!String.IsNullOrWhiteSpace(model.Email))
            {
                var customer = new CustomerDao().GetCustomerByEmail(model.Email);
                if (customer != null)
                {
                    ModelState.AddModelError("", "Email is already registered");
                }
            }
            if (ModelState.IsValid)
            {
                var customerDao = new CustomerDao();
                var encryptedMd5 = OnlineShop.Common.Encryptor.MD5Hash(model.Password);
                var custommer = new Customer
                {
                    Email = model.Email,
                    Username = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    Company = model.Company,
                    CustomerGuid = Guid.NewGuid(),
                    VendorId = 0,
                    Active = true,
                    AdminComment = "",
                    LastIpAddress = PublicIPAddress(),
                    CreatedOnUtc = DateTime.UtcNow,
                    LastActivityDateUtc = DateTime.UtcNow,
                };

                var customerRole = customerDao.GetCustomerRoleById(1);
                if (customerRole != null)
                    custommer.CustomerRoles.Add(customerRole);

                long id = customerDao.InsertCustomer(custommer);
                if (id > 0)
                {
                    var customerPassord = new CustomerPassword
                    {
                        CustomerId = custommer.Id,
                        Password = encryptedMd5,
                        CreatedOnUtc = DateTime.UtcNow
                    };
                    model.CustomerPasswords.Add(customerPassord);
                    customerDao.UpdateCustomer(custommer);
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới người dùng không thành công .");
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

        [HttpGet]
        public ActionResult PasswordRecovery()
        {
            return View();
        }
    }
}