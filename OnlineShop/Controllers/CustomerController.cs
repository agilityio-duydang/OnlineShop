using Models.Dao;
using Models.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Info()
        {
            if (Session[Common.CommonConstants.USER_SESSION] !=null)
            {
                var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(session.UserId));
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public ActionResult Address()
        {
            if (Session[Common.CommonConstants.USER_SESSION] != null)
            {
                var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(session.UserId));
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public ActionResult DownloadAbleProducts()
        {
            if (Session[Common.CommonConstants.USER_SESSION] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public ActionResult BackinStockSubScriptions()
        {
            if (Session[Common.CommonConstants.USER_SESSION] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }


        [HttpGet]
        public ActionResult Avatar()
        {
            if (Session[Common.CommonConstants.USER_SESSION] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }


        [HttpGet]
        public ActionResult RewardPoints()
        {
            if (Session[Common.CommonConstants.USER_SESSION] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session[Common.CommonConstants.USER_SESSION] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }


        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (Session[Common.CommonConstants.USER_SESSION] != null)
            {
                var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
                var customerDao = new CustomerDao();
                var customer = customerDao.GetCustomerById(Convert.ToInt32(session.UserId));
                if (ModelState.IsValid)
                {
                    var password = customer.CustomerPasswords.FirstOrDefault().Password;
                    if (password != Encryptor.MD5Hash(model.OldPassword))
                    {
                        ModelState.AddModelError("OldPassword", "Old password not match");
                    }
                    else
                    {
                        customer.CustomerPasswords.FirstOrDefault().Password = Encryptor.MD5Hash(model.NewPassword);
                        var result = customerDao.UpdateCustomer(customer);
                        if (result)
                        {

                        }
                    }
                }
               return RedirectToAction("ChangePassword", "Customer");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public ActionResult AddressEdit(int Id)
        {
            if (Session[Common.CommonConstants.USER_SESSION] != null)
            {
                var address = new AddressDao().GetAddressById(Id);

                var addressModel = new AddressModel();
                addressModel.Id = address.Id;
                addressModel.FirstName = address.FirstName;
                addressModel.LastName = address.LastName;
                addressModel.Email = address.Email;
                addressModel.Company = address.Company;
                addressModel.City = address.City;
                addressModel.Address1 = address.Address1;
                addressModel.Address2 = address.Address2;
                addressModel.ZipPostalCode = address.ZipPostalCode;
                addressModel.PhoneNumber = address.PhoneNumber;
                address.FaxNumber = address.FaxNumber;
                return View(addressModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public ActionResult AddressEdit(AddressModel addressModel)
        {
            if (Session[Common.CommonConstants.USER_SESSION] != null)
            {
                if (ModelState.IsValid)
                {
                    var model = new AddressDao();
                    var address = new Address()
                    {
                        Id = addressModel.Id,
                        FirstName = addressModel.FirstName,
                        LastName = addressModel.LastName,
                        Email = addressModel.Email,
                        Company = addressModel.Company,
                        City = addressModel.City,
                        Address1 = addressModel.Address1,
                        Address2 = addressModel.Address2,
                        ZipPostalCode = addressModel.ZipPostalCode,
                        PhoneNumber = addressModel.PhoneNumber,
                        FaxNumber = addressModel.FaxNumber,
                        CreatedOnUtc = DateTime.UtcNow
                    };  
                    var result = model.UpdateAddress(address);
                    if (result)
                    {

                    }
                }
                return RedirectToAction("Address","Customer");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public ActionResult ProductReviews()
        {
            if (Session[Common.CommonConstants.USER_SESSION] != null)
            {
                var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
                var customerDao = new CustomerDao();
                var customer = customerDao.GetCustomerById(Convert.ToInt32(session.UserId));              
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}