using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ProductReviewDao
    {
        OnlineShopDbContext dbContext;
        public ProductReviewDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<ProductReview> GetProductReviews(DateTime createdFrom, DateTime createdTo, string message, bool isApproved, int productId, int page, int pageSize)
        {
            IQueryable<ProductReview> productReviews = dbContext.ProductReviews;
            if (createdFrom != null)
            {
                productReviews = productReviews.Where(x => x.CreatedOnUtc >= createdFrom);
            }
            if (createdTo != null)
            {
                productReviews = productReviews.Where(x => x.CreatedOnUtc <= createdTo);
            }
            if (!String.IsNullOrWhiteSpace(message))
            {
                productReviews = productReviews.Where(x => x.ReviewText.ToLower().Contains(message.ToLower().Trim()));
            }
            productReviews = productReviews.Where(x => x.IsApproved == isApproved);

            if (productId > 0)
            {
                productReviews = productReviews.Where(x => x.ProductId == productId);
            }
            return productReviews.OrderByDescending(x => x.CreatedOnUtc).ToPagedList(page, pageSize);
        }

        public int InsertProductReview(ProductReview productReview)
        {
            if (productReview == null)
                throw new ArgumentNullException(nameof(productReview));

            dbContext.ProductReviews.Add(productReview);
            dbContext.SaveChanges();
            return productReview.Id;
        }

        public bool UpdateProductReview(ProductReview entity)
        {
            var productReview = dbContext.ProductReviews.Find(entity.Id);
            if (productReview == null)
                throw new ArgumentNullException(nameof(productReview));

            productReview.CustomerId = entity.CustomerId;
            productReview.ProductId = entity.ProductId;
            productReview.IsApproved = entity.IsApproved;
            productReview.Title = entity.Title;
            productReview.ReviewText = entity.ReviewText;
            productReview.ReplyText = entity.ReplyText;
            productReview.Rating = entity.Rating;
            productReview.HelpfulYesTotal = entity.HelpfulYesTotal;
            productReview.HelpfulNoTotal = entity.HelpfulNoTotal;

            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteProductReview(int productReviewId)
        {
            if (productReviewId == 0)
                return false;

            var productReview = dbContext.ProductReviews.Find(productReviewId);
            if (productReview == null)
                throw new ArgumentNullException(nameof(productReview));

            dbContext.ProductReviews.Remove(productReview);
            dbContext.SaveChanges();
            return true;
        }
        public ProductReview GetProductReviewById(int productReviewId)
        {
            if (productReviewId == 0)
                return null;

            return dbContext.ProductReviews.Find(productReviewId);
        }

        public IEnumerable<ProductReview> GetProductReviewByProductId(long productId , int page , int pageSize)
        {
            if (productId == 0)
                return null;

            return dbContext.ProductReviews.Where(x => x.ProductId == productId).OrderByDescending(x=>x.Id).ToPagedList(page,pageSize);
        }
    }
}
