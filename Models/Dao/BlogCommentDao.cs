using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class BlogCommentDao
    {
        OnlineShopDbContext dbContext;
        public BlogCommentDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<BlogComment> GetBlogComments(DateTime createdFrom, DateTime createdTo, int isApproved, string message, int page, int pageSize)
        {
            IQueryable<BlogComment> blogComments = dbContext.BlogComments;
            if (createdFrom != null)
            {
                blogComments = blogComments.Where(x => x.CreatedOnUtc >= createdFrom);
            }
            if (createdTo != null)
            {
                blogComments = blogComments.Where(x => x.CreatedOnUtc <= createdTo);
            }
            if (isApproved > 0)
            {
                if (isApproved == 1)
                {
                    blogComments = blogComments.Where(x => x.IsApproved == true);
                }
                else
                {
                    blogComments = blogComments.Where(x => x.IsApproved == false);
                }
            }
            if (!String.IsNullOrWhiteSpace(message))
            {
                blogComments = blogComments.Where(x => x.CommentText.Contains(message));
            }
            return blogComments.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertBlogComment(BlogComment blogComment)
        {
            if (blogComment == null)
                throw new ArgumentNullException(nameof(blogComment));

            blogComment.CreatedOnUtc = DateTime.UtcNow;
            dbContext.BlogComments.Add(blogComment);
            return blogComment.Id;
        }

        public bool UpdateBlogComment(BlogComment entity)
        {
            var blogComment = dbContext.BlogComments.Find(entity.Id);
            if (blogComment == null)
                throw new ArgumentNullException(nameof(blogComment));

            blogComment.CustomerId = entity.CustomerId;
            blogComment.CommentText = entity.CommentText;
            blogComment.IsApproved = entity.IsApproved;
            blogComment.BlogPostId = entity.BlogPostId;

            dbContext.SaveChanges();
            return true;
        }
        public bool DeleteBlogComment(int blogCommentId)
        {
            if (blogCommentId == 0)
                return false;
            var blogComment = dbContext.BlogComments.Find(blogCommentId);
            if (blogComment == null)
                throw new ArgumentNullException(nameof(blogComment));

            dbContext.BlogComments.Remove(blogComment);
            dbContext.SaveChanges();
            return true;
        }

        public BlogComment GetBlogCommentById(int blogCommentId)
        {
            if (blogCommentId == 0)
                return null;

            return dbContext.BlogComments.Find(blogCommentId);
        }

        public IEnumerable<BlogComment> GetBlogCommentsByBlogPostId(int blogPostId, int page, int pageSize)
        {
            if (blogPostId == 0)
                return null;

            return dbContext.BlogComments.Where(x => x.BlogPostId == blogPostId).OrderByDescending(x => x.CreatedOnUtc).ToPagedList(page, pageSize);
        }

        public IEnumerable<BlogComment> GetBlogCommentsByCustomerId(int customerId, int page, int pageSize)
        {
            if (customerId == 0)
                return null;

            return dbContext.BlogComments.Where(x => x.CustomerId == customerId).OrderByDescending(x => x.CreatedOnUtc).ToPagedList(page, pageSize);
        }
    }
}
