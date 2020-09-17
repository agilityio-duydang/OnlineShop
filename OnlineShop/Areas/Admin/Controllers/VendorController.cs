using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class VendorController : BaseController
    {
        // GET: Admin/Vendor
        public ActionResult Index(string SearchName =null , string SearchEmail=null ,int page=1 , int pageSize=10)
        {
            var vendorDao = new VendorDao();
            var model = vendorDao.GetVendors(SearchName, SearchEmail, page, pageSize);
            ViewBag.SearchName = SearchName;
            ViewBag.SearchEmail = SearchEmail;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Vendor vendor)
        {
            if (!String.IsNullOrWhiteSpace(vendor.Name))
            {
                var vendors = new VendorDao().GetVendorByName(vendor.Name);
                if (vendors != null)
                {
                    ModelState.AddModelError("","Name Vendor is already registered");
                }
            }
            if (!String.IsNullOrWhiteSpace(vendor.Email))
            {
                var vendors = new VendorDao().GetVendorByEmail(vendor.Email);
                if (vendors != null)
                {
                    ModelState.AddModelError("", "Email Vendor is already registered");
                }
            }
            if (ModelState.IsValid)
            {
                var vendorDao = new VendorDao();
                var Id = vendorDao.InsertVendor(vendor);
                if (Id > 0)
                {
                    SetNotification("Thêm mới Vendor thành công .", "success");
                    return RedirectToAction("Index", "Vendor");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới Vendor không thành công .");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)        
        {
            var vendor = new VendorDao().GetVendorById(id);
            return View(vendor);
        }
        [HttpPost]
        public ActionResult Edit (Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                var vendorDao = new VendorDao();
                var result = vendorDao.UpdateVendor(vendor);
                if (result)
                {
                    SetNotification("Cập nhật Vendor thành công .", "success");
                    return RedirectToAction("Index", "Vendor");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật Vendor không thành công .");
                }
            }
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var result = new VendorDao().DeleteVendor(Id);
            if (result)
            {
                SetNotification("Xoá Vendor thành công .", "success");
                return RedirectToAction("Index", "Vendor");
            }
            else
            {
                ModelState.AddModelError("", "Xoá Vendor không thành công .");
            }
            return RedirectToAction("Index");
        }
    }
}