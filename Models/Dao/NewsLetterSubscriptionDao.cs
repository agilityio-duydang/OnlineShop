using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class NewsLetterSubscriptionDao
    {
        OnlineShopDbContext dbContext;
        public NewsLetterSubscriptionDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<NewsLetterSubscription> NewsLetterSubscriptions(string email, DateTime startDate, DateTime endDate, bool isActive, int page, int pageSize)
        {
            IQueryable<NewsLetterSubscription> newsLetterSubscriptions = dbContext.NewsLetterSubscriptions;
            if (!String.IsNullOrWhiteSpace(email))
            {
                newsLetterSubscriptions = newsLetterSubscriptions.Where(x => x.Email.ToLower().Contains(email.ToLower().Trim()));
            }
            if (startDate != null)
            {
                newsLetterSubscriptions = newsLetterSubscriptions.Where(x => x.CreatedOnUtc >= startDate);
            }
            if (endDate != null)
            {
                newsLetterSubscriptions = newsLetterSubscriptions.Where(x => x.CreatedOnUtc <= endDate);
            }
            newsLetterSubscriptions = newsLetterSubscriptions.Where(x => x.Active == isActive);

            return newsLetterSubscriptions.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertNewsLetterSubscription(NewsLetterSubscription newsLetterSubscription)
        {
            if (newsLetterSubscription == null)
                throw new ArgumentNullException(nameof(newsLetterSubscription));

            newsLetterSubscription.CreatedOnUtc = DateTime.UtcNow;
            dbContext.NewsLetterSubscriptions.Add(newsLetterSubscription);
            dbContext.SaveChanges();
            return newsLetterSubscription.Id;
        }

        public bool UpdateNewsLetterSubscription(NewsLetterSubscription entity)
        {
            var newsLetterSubscription = dbContext.NewsLetterSubscriptions.Find(entity.Id);
            if (newsLetterSubscription == null)
                throw new ArgumentNullException(nameof(newsLetterSubscription));

            newsLetterSubscription.Email = entity.Email;
            newsLetterSubscription.Active = entity.Active;

            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteNewsLetterSubscription(int newsLetterSubscriptionId)
        {
            if (newsLetterSubscriptionId == 0)
                return false;

            var newsLetterSubscription = dbContext.NewsLetterSubscriptions.Find(newsLetterSubscriptionId);
            if (newsLetterSubscription == null)
                throw new ArgumentNullException(nameof(newsLetterSubscription));

            dbContext.NewsLetterSubscriptions.Remove(newsLetterSubscription);
            return true;
        }
        public NewsLetterSubscription NewsLetterSubscriptionById(int newsLetterSubscriptionId)
        {
            if (newsLetterSubscriptionId == 0)
                return null;

            return dbContext.NewsLetterSubscriptions.Find(newsLetterSubscriptionId);
        }
    }
}
