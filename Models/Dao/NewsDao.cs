using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class NewsDao
    {
        OnlineShopDbContext dbContext;
        public NewsDao()
        {
            dbContext = new OnlineShopDbContext();
        }
        
        public IEnumerable<News> GetNews(string title , int page , int pageSize)
        {
            IQueryable<News> news = dbContext.News;
            if (!String.IsNullOrWhiteSpace(title))
            {
                news = news.Where(x => x.Title.ToLower().Contains(title.ToLower().Trim()));
            }
            return news.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertNews(News news)
        {
            if (news == null)
                throw new ArgumentNullException(nameof(news));

            news.LanguageId = 1;
            news.CreatedOnUtc = DateTime.UtcNow;
            dbContext.News.Add(news);
            dbContext.SaveChanges();
            return news.Id;
        }

        public bool UpdateNews(News entity)
        {
            var news = dbContext.News.Find(entity.Id);
            if (entity == null)
                throw new ArgumentNullException(nameof(news));

            news.LanguageId = entity.LanguageId;
            news.Title = entity.Title;
            news.Short = entity.Short;
            news.Full = entity.Full;
            news.Published = entity.Published;
            news.StartDateUtc = entity.StartDateUtc;
            news.EndDateUtc = entity.EndDateUtc;
            news.AllowComments = entity.AllowComments;
            news.MetaKeywords = entity.MetaKeywords;
            news.MetaDescription = entity.MetaDescription;
            news.MetaTitle = entity.MetaTitle;

            dbContext.SaveChanges();
            return true;
        }
        public bool DeleteNews(int newsId)
        {
            if (newsId == 0)
                return false;

            var news = dbContext.News.Find(newsId);
            if (news == null)
                throw new ArgumentNullException(nameof(news));

            dbContext.News.Remove(news);
            dbContext.SaveChanges();
            return true;
        }

        public News GetNewsById(int newsId)
        {
            if (newsId == 0)
                return null;

            return dbContext.News.Find(newsId);
        }

        public News GetNewsByTitle(string title)
        {
            if (String.IsNullOrWhiteSpace(title))
                return null;

            return dbContext.News.Where(x => x.Title.ToLower() == title.ToLower().Trim()).SingleOrDefault();
        }
    }
}
