using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ManufactureController : BaseController
    {
        // GET: Admin/Manufacture
        public ActionResult Index(string SearchName =null , int SearchPublished =0, int pageNumber =1 , int pageSize=10)
        {
            var dao = new ManufacturerDao();
            var manufacture = dao.GetAllManufacturers(SearchName, SearchPublished, pageNumber,pageSize);
            ViewBag.SeachName = SearchName;
            ViewBag.SearchPublished = SearchPublished;
            return View(manufacture);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var customer = new ManufacturerDao().GetManufacturerById(id);
            return View(customer);
        }

        [HttpPost]

        public ActionResult Create(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                var dao = new ManufacturerDao();          
                int id = dao.InsertManufacturer(manufacturer);
                if (id > 0)
                {
                    SetNotification("Thêm mới nhà cung cấp thành công .", "success");
                    return RedirectToAction("Index", "Manufacture");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới nhà cung cấp không thành công .");
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                var dao = new ManufacturerDao();
                var result = dao.UpdateManufacturer(manufacturer);
                if (result)
                {
                    SetNotification("Cập nhật nhà cung cấp thành công .", "success");
                    return RedirectToAction("Index", "Manufacture");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật nhà cung cấp không thành công .");
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var user = new ManufacturerDao().DeleteManufacturer(id);
            return RedirectToAction("Index");
        }

    }
}