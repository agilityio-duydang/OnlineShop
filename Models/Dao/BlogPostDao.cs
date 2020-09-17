using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class BlogPostDao
    {
        OnlineShopDbContext dbContext;
        public BlogPostDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<BlogPost> GetBlogPosts(string tittle,int page, int pageSize)
        {
            IQueryable<BlogPost> blogPosts = dbContext.BlogPosts;
            if (!String.IsNullOrWhiteSpace(tittle))
            {
                blogPosts = blogPosts.Where(x => x.Title.ToLower().Contains(tittle.ToLower().Trim()));
            }
            return blogPosts.OrderByDescending(x => x.CreatedOnUtc).ToPagedList(page, pageSize);
        }

        public int InsertBlogPost(BlogPost blogPost)
        {
            if (blogPost == null)
                throw new ArgumentNullException(nameof(blogPost));
            blogPost.LanguageId = 1;
            blogPost.CreatedOnUtc = DateTime.UtcNow;
            dbContext.BlogPosts.Add(blogPost);
            dbContext.SaveChanges();
            return blogPost.Id;
        }

        public bool UpdateBlogPost(BlogPost entity)
        {
            var blogPost = dbContext.BlogPosts.Find(entity.Id);
            if (blogPost == null)
                throw new ArgumentNullException(nameof(blogPost));

            blogPost.LanguageId = entity.LanguageId;
            blogPost.Title = entity.Title;
            blogPost.Body = entity.Body;
            blogPost.BodyOverview = entity.BodyOverview;
            blogPost.AllowComments = entity.AllowComments;
            blogPost.Tags = entity.Tags;
            blogPost.StartDateUtc = entity.StartDateUtc;
            blogPost.EndDateUtc = entity.EndDateUtc;
            blogPost.MetaKeywords = entity.MetaKeywords;
            blogPost.MetaDescription = entity.MetaDescription;
            blogPost.MetaTitle = entity.MetaTitle;
            dbContext.SaveChanges();
            return true;
        }
        public bool DeleteBlogPost(int blogPostId)
        {
            if (blogPostId == 0)
                return false;
            var blogPost = dbContext.BlogPosts.Find(blogPostId);
            if (blogPost == null)
                throw new ArgumentNullException(nameof(blogPost));

            dbContext.BlogPosts.Remove(blogPost);
            dbContext.SaveChanges();
            return true;
        }
        public BlogPost GetBlogPostById(int blogPostId)
        {
            if (blogPostId == 0)
                return null;

            return dbContext.BlogPosts.Find(blogPostId);
        }

        public BlogPost GetBlogPostByTittle(string tittle)

        {
            if (String.IsNullOrWhiteSpace(tittle))
                return null;

            return dbContext.BlogPosts.Where(x => x.Title.ToLower() == tittle.ToLower().Trim()).SingleOrDefault();
        }
    }
}
