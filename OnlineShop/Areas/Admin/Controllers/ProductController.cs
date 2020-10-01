using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string productName = null, long categoryId = 0, long manufactureId = 0, int vendorId = 0, bool published = true, string sku = null, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
            var model = dao.GetAllProducts(productName, categoryId, manufactureId, vendorId, published, sku, page, pageSize);
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
            SetViewBag();
            var customer = new ProductDao().GetProductById(id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                long id = dao.InsertProduct(product);
                if (id > 0)
                {
                    SetNotification("Thêm mới sản phẩm thành công .", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới sản phẩm không thành công .");
                }
            }
            SetViewBag();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var result = dao.UpdateProduct(product);
                if (result)
                {
                    SetNotification("Cập nhật sản phẩm thành công .", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật sản phẩm không thành công .");
                }
            }
            SetViewBag();
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var product = new ProductDao().DeleteProduct(id);
            return RedirectToAction("Index");
        }
        public void SetViewBag(int? selectedId = null)
        {
            var category = new CategoryDao();
            var manufacturerDao = new ManufacturerDao();

            ViewBag.CategoryID = new SelectList(category.ListAllCategories(), "ID", "Name", selectedId);
            ViewBag.Manufacturer = new SelectList(manufacturerDao.GetManufacturers(), "ID", "Name", selectedId);
        }
        public ActionResult ProductPictureAdd(int pictureId, int displayOrder, string AltAttribute, string TitleAttribute, int productId)
        {
            if (pictureId == 0)
                throw new ArgumentException(nameof(pictureId));

            var product = new ProductDao().GetProductById(productId);
            if (product == null)
                throw new ArgumentNullException();

            var picture = new PictureDao().GetPictureById(pictureId);
            if (picture == null)
                throw new ArgumentNullException(nameof(pictureId));

            picture.AltAttribute = AltAttribute;
            picture.TitleAttribute = TitleAttribute;
            picture.SeoFilename = product.MetaKeywords;

            var result = new PictureDao().UpdatePicture(picture);

            var productPictureMaping = new Product_Picture_Mapping
            {
                ProductId = product.Id,
                PictureId = picture.Id,
                DisplayOrder = displayOrder
            };
            int Id = new Product_Picture_MappingDao().InsertProductPictureMapping(productPictureMaping);
            if (Id > 0)
            {
                return Json(new { Result = true });
            }
            else
            {
                return Json(new { Result = false });
            }
        }

        [HttpPost]
        public ActionResult ProductPictureList(int productId)
        {
            if (productId == 0)
                throw new ArgumentNullException(nameof(productId));

            var product = new ProductDao().GetProductById(productId);
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            return PartialView("_ProductPictureList", product);
        }
    }
}