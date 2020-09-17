using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext dbContext;
        public ProductDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<Product> GetAllProducts(string productName, long categoryId, long manufactureId, int vendorId, bool published, string sku, int page, int pageSize)
        {
            IQueryable<Product> products = dbContext.Products;
            if (!String.IsNullOrWhiteSpace(productName))
            {
                products = products.Where(x => x.Name.ToLower().Contains(productName.ToLower().Trim()));
            }
            //if (categoryId > 0)
            //{
            //    products = products.Where(x => x.CategoryId == categoryId);
            //}
            //if (manufactureId > 0)
            //{
            //    products = products.Where(x => x.ManufacturerId == manufactureId);
            //}
            if (vendorId > 0)
            {
                products = products.Where(x => x.VendorId == vendorId);
            }
            products = products.Where(x => x.Published == published);
            if (!String.IsNullOrWhiteSpace(sku))
            {
                products = products.Where(x => x.Sku.ToLower().Contains(sku.ToLower().Trim()));
            }
            return products.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public int InsertProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            return product.Id;
        }

        public bool UpdateProduct(Product entity)
        {
            try
            {
                var product = dbContext.Products.Find(entity.Id);
                if (product == null)
                    throw new ArgumentNullException(nameof(product));

                product.ProductTypeId = entity.ProductTypeId;
                product.ParentGroupedProductId = entity.ParentGroupedProductId;
                product.Name = entity.Name;
                product.ShortDescription = entity.ShortDescription;
                product.FullDescription = entity.FullDescription;
                product.AdminComment = entity.AdminComment;
                product.VendorId = entity.VendorId;
                product.ShowOnHomePage = entity.ShowOnHomePage;
                product.MetaKeywords = entity.MetaKeywords;
                product.MetaDescription = entity.MetaDescription;
                product.MetaTitle = entity.MetaTitle;
                product.AllowCustomerReviews = entity.AllowCustomerReviews;
                product.ApprovedRatingSum = entity.ApprovedRatingSum;
                product.NotApprovedRatingSum = entity.NotApprovedRatingSum;
                product.ApprovedTotalReviews = entity.ApprovedTotalReviews;
                product.NotApprovedTotalReviews = entity.NotApprovedTotalReviews;
                product.Sku = entity.Sku;
                product.ManufacturerPartNumber = entity.ManufacturerPartNumber;
                product.IsShipEnabled = entity.IsShipEnabled;
                product.IsFreeShipping = entity.IsFreeShipping;
                product.AdditionalShippingCharge = entity.AdditionalShippingCharge;
                product.TaxCategoryId = entity.TaxCategoryId;
                product.ProductAvailabilityRangeId = entity.ProductAvailabilityRangeId;
                product.StockQuantity = entity.StockQuantity;
                product.DisplayStockAvailability = entity.DisplayStockAvailability;
                product.DisplayStockQuantity = entity.DisplayStockQuantity;
                product.MinStockQuantity = entity.MinStockQuantity;
                product.LowStockActivityId = entity.LowStockActivityId;
                product.OrderMinimumQuantity = entity.OrderMinimumQuantity;
                product.OrderMaximumQuantity = entity.OrderMaximumQuantity;
                product.AllowedQuantities = entity.AllowedQuantities;
                product.NotReturnable = entity.NotReturnable;
                product.DisableBuyButton = entity.DisableBuyButton;
                product.DisableWishlistButton = entity.DisableWishlistButton;
                product.CallForPrice = entity.CallForPrice;
                product.Price = entity.Price;
                product.OldPrice = entity.OldPrice;
                product.ProductCost = entity.ProductCost;
                product.CustomerEntersPrice = entity.CustomerEntersPrice;
                product.MinimumCustomerEnteredPrice = entity.MinimumCustomerEnteredPrice;
                product.MaximumCustomerEnteredPrice = entity.MaximumCustomerEnteredPrice;
                product.MarkAsNew = entity.MarkAsNew;
                product.MarkAsNewStartDateTimeUtc = entity.MarkAsNewStartDateTimeUtc;
                product.MarkAsNewEndDateTimeUtc = entity.MarkAsNewEndDateTimeUtc;
                product.HasTierPrices = entity.HasTierPrices;
                product.HasDiscountsApplied = entity.HasDiscountsApplied;
                product.Weight = entity.Weight;
                product.Length = entity.Length;
                product.Width = entity.Width;
                product.Height = entity.Height;
                product.AvailableStartDateTimeUtc = entity.AvailableStartDateTimeUtc;
                product.AvailableEndDateTimeUtc = entity.AvailableEndDateTimeUtc;
                product.UpdatedOnUtc = DateTime.UtcNow;
                product.DisplayOrder = entity.DisplayOrder;
                product.Published = entity.Published;
                product.Deleted = entity.Deleted;
                product.TaxCategoryId = entity.TaxCategoryId;

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new ArgumentException(ex.Message);
            }
        }

        public bool DeleteProduct(int productId)
        {
            if (productId == 0)
                return false;

            var product = dbContext.Products.Find(productId);
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
            return true;
        }
        public Product GetProductById(int productId)
        {
            if (productId == 0)
                return null;

            return dbContext.Products.Find(productId);
        }

        public Product GetProductBySku(string sku)
        {
            if (String.IsNullOrWhiteSpace(sku))
                return null;

            return dbContext.Products.Where(x => x.Sku.ToLower() == sku.ToLower().Trim()).FirstOrDefault();
        }
    }
}
