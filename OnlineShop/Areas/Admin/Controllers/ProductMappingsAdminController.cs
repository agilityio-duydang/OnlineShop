using Microsoft.Ajax.Utilities;
using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductMappingsAdminController : Controller
    {
        [HttpGet]
        // GET: Admin/ProductMappingsAdmin
        public ActionResult ProductAddJCarouselPopup(int JCarouselId = 0, string productName = null, long categoryId = 0, long manufactureId = 0, int vendorId = 0, bool published = true, string sku = null, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
            var model = dao.GetAllProducts(productName, categoryId, manufactureId, vendorId, published, sku, page, pageSize);
            SetViewBag();
            ViewBag.JCarouselId = JCarouselId;
            return View(model);
        }

        [HttpGet]
        // GET: Admin/ProductMappingsAdmin
        public ActionResult ProductAddSaleOfTheDayOfferPopup(int SaleOfTheDayOfferId = 0, string productName = null, long categoryId = 0, long manufactureId = 0, int vendorId = 0, bool published = true, string sku = null, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
            var model = dao.GetAllProducts(productName, categoryId, manufactureId, vendorId, published, sku, page, pageSize);
            SetViewBag();
            ViewBag.SaleOfTheDayOfferId = SaleOfTheDayOfferId;
            return View(model);
        }
        public void SetViewBag(int? selectedId = null)
        {
            var category = new CategoryDao();
            var manufacturerDao = new ManufacturerDao();

            ViewBag.CategoryID = new SelectList(category.ListAllCategories(), "ID", "Name", selectedId);
            ViewBag.Manufacturer = new SelectList(manufacturerDao.GetManufacturers(), "ID", "Name", selectedId);
        }

        [HttpPost]
        public ActionResult ProductAddJCarouselPopup(int jCarouselId, List<int> selectedIds)
        {
            List<JCarousel_Product_Mapping> jCarousels = new List<JCarousel_Product_Mapping>();
            if (jCarouselId == 0)
                throw new ArgumentException(nameof(jCarouselId));

            var jCarousel = new JCarouselDao().GetJCarouselById(jCarouselId);
            if (jCarousel == null)
                throw new ArgumentNullException(nameof(jCarousel));

            List<JCarousel_Product_Mapping> jCarouselCheck = new List<JCarousel_Product_Mapping>();

            if (jCarousel.JCarousel_Product_Mapping.Count > 0)
            {
                foreach (var Id in selectedIds)
                {
                    jCarouselCheck = jCarousel.JCarousel_Product_Mapping.Where(x => x.ProductId ==Id).ToList();
                    if (jCarouselCheck.Count ==0)
                    {
                        var product = new JCarousel_Product_Mapping
                        {
                            ProductId = Id,
                            JCarouselId = jCarouselId,
                            DisplayOrder = Id
                        };
                        jCarousels.Add(product);
                    }
                }
            }
            else
            {
                foreach (var Id in selectedIds)
                {
                    var product = new JCarousel_Product_Mapping
                    {
                        ProductId = Id,
                        JCarouselId = jCarouselId,
                        DisplayOrder = Id
                    };
                    jCarousels.Add(product);
                }
            }
            if (jCarousels.Count > 0)
            {
                foreach (JCarousel_Product_Mapping item in jCarousels)
                {
                    jCarousel.JCarousel_Product_Mapping.Add(item);
                }
            }
            var result = new JCarouselDao().UpdateJCarousels(jCarousel);
            if (result)
            {
                return Json(new { Result = true });
            }
            else
            {
                return Json(new { Result = false });
            }
        }

        [HttpPost]
        public ActionResult ProductAddSaleOfTheDayOfferPopup(int saleOfTheDayOfferId, List<int> selectedIds)
        {
            List<SaleOfTheDayOffer_Product_Mapping> saleOfTheDayOffers = new List<SaleOfTheDayOffer_Product_Mapping>();
            if (saleOfTheDayOfferId == 0)
                throw new ArgumentException(nameof(saleOfTheDayOfferId));

            var saleOfTheDays = new SaleOfTheDayOfferDao().SaleOfTheDayOfferById(saleOfTheDayOfferId);
            if (saleOfTheDays == null)
                throw new ArgumentNullException(nameof(saleOfTheDays));

            List<SaleOfTheDayOffer_Product_Mapping> saleOfTheDayOfferCheck = new List<SaleOfTheDayOffer_Product_Mapping>();

            if (saleOfTheDays.SaleOfTheDayOffer_Product_Mapping.Count > 0)
            {
                foreach (var Id in selectedIds)
                {
                    saleOfTheDayOfferCheck = saleOfTheDays.SaleOfTheDayOffer_Product_Mapping.Where(x => x.ProductId == Id).ToList();
                    if (saleOfTheDayOfferCheck.Count == 0)
                    {
                        var product = new SaleOfTheDayOffer_Product_Mapping
                        {
                            ProductId = Id,
                            SaleOfTheDayOfferId = saleOfTheDayOfferId,
                            DisplayOrder = Id
                        };
                        saleOfTheDayOffers.Add(product);
                    }
                }
            }
            else
            {
                foreach (var Id in selectedIds)
                {
                    var product = new SaleOfTheDayOffer_Product_Mapping
                    {
                        ProductId = Id,
                        SaleOfTheDayOfferId = saleOfTheDayOfferId,
                        DisplayOrder = Id
                    };
                    saleOfTheDayOffers.Add(product);
                }
            }
            if (saleOfTheDayOffers.Count > 0)
            {
                foreach (SaleOfTheDayOffer_Product_Mapping item in saleOfTheDayOffers)
                {
                    saleOfTheDays.SaleOfTheDayOffer_Product_Mapping.Add(item);
                }
            }
            var result = new SaleOfTheDayOfferDao().UpdateSaleOfTheDayOffer(saleOfTheDays);
            if (result)
            {
                return Json(new { Result = true });
            }
            else
            {
                return Json(new { Result = false });
            }
        }
    }
}