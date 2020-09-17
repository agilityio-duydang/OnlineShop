using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class DisCountDao
    {
        OnlineShopDbContext dbContext;
        public DisCountDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<Discount> GetDiscounts(DateTime startDateUtc, DateTime endDateUtc, int discountTypeId, string couponCode, string name, int page, int pageSize)
        {
            IQueryable<Discount> discounts = dbContext.Discounts;
            if (startDateUtc != null)
            {
                discounts = discounts.Where(x => x.StartDateUtc >= startDateUtc);
            }
            if (endDateUtc != null)
            {
                discounts = discounts.Where(x => x.EndDateUtc <= endDateUtc);
            }
            if (discountTypeId > 0)
            {
                discounts = discounts.Where(x => x.DiscountTypeId == discountTypeId);
            }
            if (!String.IsNullOrWhiteSpace(couponCode))
            {
                discounts = discounts.Where(x => x.CouponCode.ToLower().Contains(couponCode.ToLower().Trim()));
            }
            if (!String.IsNullOrWhiteSpace(name))
            {
                discounts = discounts.Where(x => x.Name.ToLower().Contains(name.ToLower().Trim()));
            }
            return discounts.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public int InsertDiscount(Discount discount)
        {
            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            dbContext.Discounts.Add(discount);
            dbContext.SaveChanges();
            return discount.Id;
        }
        public bool UpdateDiscount(Discount entity)
        {
            var discount = dbContext.Discounts.Find(entity.Id);

            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            discount.Name = entity.Name;
            discount.DiscountTypeId = entity.DiscountTypeId;
            discount.UsePercentage = entity.UsePercentage;
            discount.DiscountPercentage = entity.DiscountPercentage;
            discount.DiscountAmount = entity.DiscountAmount;
            discount.MaximumDiscountAmount = entity.MaximumDiscountAmount;
            discount.StartDateUtc = entity.StartDateUtc;
            discount.EndDateUtc = entity.EndDateUtc;
            discount.RequiresCouponCode = entity.RequiresCouponCode;
            discount.CouponCode = entity.CouponCode;
            discount.IsCumulative = entity.IsCumulative;
            discount.DiscountLimitationId = entity.DiscountLimitationId;
            discount.LimitationTimes = entity.LimitationTimes;
            discount.MaximumDiscountedQuantity = entity.MaximumDiscountedQuantity;
            discount.AppliedToSubCategories = entity.AppliedToSubCategories;

            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteDiscount(int discountId)
        {
            if (discountId == 0)
                return false;
            var discount = dbContext.Discounts.Find(discountId);
            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            dbContext.Discounts.Remove(discount);
            dbContext.SaveChanges();
            return true;
        }

        public Discount GetDiscountById(int discountId)
        {
            if (discountId == 0)
                return null;

            return dbContext.Discounts.Find(discountId);
        }
        public Discount GetDiscountByCouponCode(string couponCode)
        {
            if (String.IsNullOrWhiteSpace(couponCode))
                return null;

            return dbContext.Discounts.Where(x => x.CouponCode.ToLower() == couponCode.ToLower().Trim()).SingleOrDefault(); 
        }
    }
}
