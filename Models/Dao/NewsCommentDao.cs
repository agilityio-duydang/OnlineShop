using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class NewsCommentDao
    {
        OnlineShopDbContext dbContext;
        public NewsCommentDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<NewsComment> GetNewsComments(DateTime createdFrom, DateTime createdTo, int isAproved, string message, int page, int pageSize)
        {
            IQueryable<NewsComment> newsComments = dbContext.NewsComments;
            if (createdFrom != null)
            {
                newsComments = newsComments.Where(x => x.CreatedOnUtc >= createdFrom);
            }
            if (createdTo != null)
            {
                newsComments = newsComments.Where(x => x.CreatedOnUtc <= createdTo);
            }
            if (isAproved > 0)
            {
                if (isAproved == 1)
                {
                    newsComments = newsComments.Where(x => x.IsApproved == true);
                }
                else
                {
                    newsComments = newsComments.Where(x => x.IsApproved == false);
                }
            }
            if (!String.IsNullOrWhiteSpace(message))
            {
                newsComments = newsComments.Where(x => x.CommentText.ToLower().Contains(message.ToLower().Trim()));
            }
            return newsComments.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertNewsComment(NewsComment newsComment)
        {
            if (newsComment == null)
                throw new ArgumentNullException(nameof(newsComment));

            dbContext.NewsComments.Add(newsComment);
            dbContext.SaveChanges();
            return newsComment.Id;
        }

        public bool UpdateNewsComment(NewsComment entity)
        {
            var newsComment = dbContext.NewsComments.Find(entity.Id);
            if (newsComment == null)
                throw new ArgumentNullException(nameof(newsComment));

            newsComment.CommentTitle = entity.CommentTitle;
            newsComment.CommentText = entity.CommentText;
            newsComment.NewsItemId = entity.NewsItemId;
            newsComment.CustomerId = entity.CustomerId;
            newsComment.IsApproved = entity.IsApproved;

            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteNewsComment(int newsCommentId)
        {
            if (newsCommentId == 0)
                return false;

            var newsComment = dbContext.NewsComments.Find(newsCommentId);
            if (newsComment == null)
                throw new ArgumentNullException(nameof(newsComment));
            dbContext.NewsComments.Remove(newsComment);
            dbContext.SaveChanges();
            return true;
        }

        public NewsComment GetNewsCommentById(int newsCommentId)
        {
            if (newsCommentId == 0)
                return null;

            return dbContext.NewsComments.Find(newsCommentId);
        }

        public NewsComment GetNewsCommentByTitle(string title)
        {
            if (String.IsNullOrWhiteSpace(title))
                return null;

            return dbContext.NewsComments.Where(x => x.CommentTitle.ToLower() == title.ToLower().Trim()).SingleOrDefault();
        }

        public IEnumerable<NewsComment> GetNewsCommentsByCustomerId(long customerId, int page, int pageSize)
        {
            if (customerId == 0)
                return null;

            return dbContext.NewsComments.Where(x => x.CustomerId == customerId).OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
    }
}
