using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using System.Data.Entity;
using OnlineShop.Areas.Admin.Models;
using System.Net.Http;
using System.Text;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Admin/Customer
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
            SetViewBag();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var customer = new CustomerDao().GetCustomerById(id);
            var customerModel = new CustomerModel();

            customerModel.Id = customer.Id;
            customerModel.Email = customer.Email;
            customerModel.Username = customer.Username;
            customerModel.Password = customer.CustomerPasswords.SingleOrDefault().Password;
            customerModel.FirstName = customer.FirstName;
            customerModel.LastName = customer.LastName;
            customerModel.Gender = customer.Gender;
            customerModel.DateOfBirth = customer.DateOfBirth;
            customerModel.Company = customer.Company;
            customerModel.CustomerGuid = customer.CustomerGuid;
            customerModel.VendorId = customer.VendorId;
            customerModel.Active = customer.Active;
            customerModel.AdminComment = customer.AdminComment;
            customerModel.LastIpAddress = customer.LastIpAddress;
            customerModel.CreatedOnUtc = customer.CreatedOnUtc;
            customerModel.LastActivityDateUtc = customer.LastActivityDateUtc;

            customerModel.SelectedCustomerRoleIds = new List<int>();

            foreach (CustomerRole item in customer.CustomerRoles)
            {
                customerModel.SelectedCustomerRoleIds.Add(item.Id);
            }

            foreach (Order item in customer.Orders)
            {
                customerModel.Orders.Add(item);
            }

            foreach (Address item in customer.Addresses)
            {
                customerModel.Addresses.Add(item);
            }

            foreach (ShoppingCartItem item in customer.ShoppingCartItems)
            {
                customerModel.ShoppingCartItems.Add(item);
            }

            foreach (ActivityLog item in customer.ActivityLogs)
            {
                customerModel.ActivityLogs.Add(item);
            }
            SetViewBag();
            SetViewBagCustomerRoles(customerModel.SelectedCustomerRoleIds);
            return View(customerModel);
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
        protected virtual string GetCustomerRolesNames(IList<CustomerRole> customerRoles, string separator = ",")
        {
            var sb = new StringBuilder();
            for (var i = 0; i < customerRoles.Count; i++)
            {
                sb.Append(customerRoles[i].Name);
                if (i != customerRoles.Count - 1)
                {
                    sb.Append(separator);
                    sb.Append(" ");
                }
            }
            return sb.ToString();
        }
        [HttpPost]
        public ActionResult Create(CustomerModel customerModel)
        {
            if (!String.IsNullOrWhiteSpace(customerModel.Username))
            {
                var customer = new CustomerDao().GetCustomerByUserName(customerModel.Username);
                if (customer != null)
                {
                    ModelState.AddModelError("", "Username is already registered");
                }
            }
            if (!String.IsNullOrWhiteSpace(customerModel.Email))
            {
                var customer = new CustomerDao().GetCustomerByEmail(customerModel.Email);
                if (customer != null)
                {
                    ModelState.AddModelError("", "Email is already registered");
                }
            }
            if (ModelState.IsValid)
            {
                var customerDao = new CustomerDao();
                var encryptedMd5 = OnlineShop.Common.Encryptor.MD5Hash(customerModel.Password);
                var custommer = new Customer
                {
                    Email = customerModel.Email,
                    Username = customerModel.Username,
                    FirstName = customerModel.FirstName,
                    LastName = customerModel.LastName,
                    Gender = customerModel.Gender,
                    DateOfBirth = customerModel.DateOfBirth,
                    Company = customerModel.Company,
                    CustomerGuid = Guid.NewGuid(),
                    VendorId = customerModel.VendorId,
                    Active = customerModel.Active,
                    AdminComment = customerModel.AdminComment,
                    LastIpAddress = PublicIPAddress(),
                    CreatedOnUtc = DateTime.UtcNow,
                    LastActivityDateUtc = DateTime.UtcNow,
                };

                foreach (var item in customerModel.SelectedCustomerRoleIds)
                {
                    var customerRole = customerDao.GetCustomerRoleById(item);
                    if (customerRole != null)
                        custommer.CustomerRoles.Add(customerRole);
                }

                long id = customerDao.InsertCustomer(custommer);
                if (id > 0)
                {
                    var customerPassord = new CustomerPassword
                    {
                        CustomerId = custommer.Id,
                        Password = encryptedMd5,
                        CreatedOnUtc = DateTime.UtcNow
                    };
                    customerModel.CustomerPasswords.Add(customerPassord);
                    customerDao.UpdateCustomer(custommer);

                    SetNotification("Thêm mới người dùng thành công .", "success");
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới người dùng không thành công .");
                }
            }
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                var customerDao = new CustomerDao();
                var cus = customerDao.GetCustomerById(customerModel.Id);
                var custommer = new Customer
                {
                    Id = customerModel.Id,
                    Email = customerModel.Email,
                    Username = customerModel.Username,
                    FirstName = customerModel.FirstName,
                    LastName = customerModel.LastName,
                    Gender = customerModel.Gender,
                    DateOfBirth = customerModel.DateOfBirth,
                    Company = customerModel.Company,
                    CustomerGuid = cus.CustomerGuid,
                    VendorId = customerModel.VendorId,
                    Active = customerModel.Active,
                    AdminComment = customerModel.AdminComment,
                    LastIpAddress = cus.LastIpAddress,
                    CreatedOnUtc = cus.CreatedOnUtc,
                    LastActivityDateUtc = cus.LastActivityDateUtc,
                };
                foreach (var item in customerModel.SelectedCustomerRoleIds)
                {
                    var customerRole = customerDao.GetCustomerRoleById(item);
                    if (customerRole != null)
                        custommer.CustomerRoles.Add(customerRole);
                }
                foreach (var item in cus.Addresses)
                {
                        custommer.Addresses.Add(item);
                }
                foreach (var item in cus.Orders)
                {
                        custommer.Orders.Add(item);
                }
                foreach (var item in cus.ShoppingCartItems)
                {
                        custommer.ShoppingCartItems.Add(item);
                }
                foreach (var item in cus.ActivityLogs)
                {
                        custommer.ActivityLogs.Add(item);
                }

                var result = customerDao.UpdateCustomer(custommer);
                if (result)
                {
                    SetNotification("Cập nhật người dùng thành công .", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật người dùng không thành công .");
                }
            }
            SetViewBag();
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var user = new CustomerDao().DeleteCustomer(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new CustomerDao().ChangeStatusCustomer(id);
            return Json(new
            {
                status = result
            });
        }

        public void SetViewBag(int? selectedId = null)
        {
            var customerRoleDao = new CustomerRoleDao();
            var customerRoles = customerRoleDao.ListAll();

            Vendor vendor = new Vendor
            {
                Name = "Not a vendor",
                Id = 0,
            };
            var vendorDao = new VendorDao();
            var vendors = vendorDao.GetVendors();
            vendors.Add(vendor);

            ViewBag.AvailableCustomerRoles = new SelectList(customerRoles, "Id", "Name", selectedId);
            ViewBag.AvailableVendors = new SelectList(vendors, "Id", "Name", selectedId);
        }
        public void SetViewBagCustomerRoles(IList<int> selectedId = null)
        {
            var customerRoleDao = new CustomerRoleDao();
            var customerRoles = customerRoleDao.ListAll();

            ViewBag.SelectedCustomerRoleIds = new SelectList(customerRoles, "Id", "Name", selectedId);
        }
    }
}