using Models.Dao;
using Models.EF;
using OnlineShop.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class AddressController : BaseController
    {
        // GET: Admin/Address
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create(int customerId)
        {
            var model = new AddressModel();
            model.CustomerId = customerId;
            return View(model);
        }

        [HttpPost]

        public ActionResult Create(AddressModel addressModel)
        {
            if (ModelState.IsValid)
            {
                var address = new Address
                {                    
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
                var customerDao = new CustomerDao();
                var customer = customerDao.GetCustomerById(addressModel.CustomerId);
                customer.Address = address;
                customer.Addresses.Add(address);
                var result = customerDao.UpdateCustomer(customer);

                if (result)
                {
                    SetNotification("Thêm mới Address thành công .", "success");
                    return Redirect("/Admin/Customer/Edit/" + customer.Id);
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới Address không thành công");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int customerId , int addressId)
        {
            var address = new AddressDao().GetAddressById(addressId);
            var addressModel = new AddressModel();
            addressModel.Id = address.Id;
            addressModel.CustomerId = customerId;
            addressModel.FirstName = address.FirstName;
            addressModel.LastName = address.LastName;
            addressModel.Email = address.Email;
            addressModel.Company = address.Company;
            addressModel.City = address.City;
            addressModel.Address1 = address.Address1;
            addressModel.Address2 = address.Address2;
            addressModel.ZipPostalCode = address.ZipPostalCode;
            addressModel.PhoneNumber = address.PhoneNumber;
            addressModel.FaxNumber = address.FaxNumber;

            return View(addressModel);
        }

        [HttpPost]
        public ActionResult Edit(AddressModel addressModel)
        {
            if (ModelState.IsValid)
            {
                var address = new Address
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
                    FaxNumber = addressModel.FaxNumber
                };
                var addressDao = new AddressDao();
                var result = addressDao.UpdateAddress(address);
                if (result)
                {
                    SetNotification("Cập nhật Address thành công .", "success");
                    return Redirect("/Admin/Customer/Edit/" + addressModel.CustomerId);
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới Address không thành công");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int customerId , int addressId)
        {
            var customerDao = new CustomerDao();
            var customer = customerDao.GetCustomerById(customerId);
            var address = new AddressDao().GetAddressById(addressId);
            foreach (Address item in customer.Addresses)
            {
                if (item.Id == address.Id)
                {
                    customer.Addresses.Remove(item);
                    break;
                }
            }
            customerDao.UpdateCustomer(customer);

            return Redirect("/Admin/Customer/Edit/" + customerId);
        }
    }
}