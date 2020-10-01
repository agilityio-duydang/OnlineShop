using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
   public class JCarouselDao
    {
        OnlineShopDbContext dbContext;
        public JCarouselDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<JCarousel> GetJCarousels(int page , int pageSize)
        {
            IQueryable<JCarousel> jCarousels = dbContext.JCarousels;

            return jCarousels.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertJCarousels (JCarousel jCarousel)
        {
            if (jCarousel == null)
                throw new ArgumentNullException(nameof(jCarousel));

            dbContext.JCarousels.Add(jCarousel);
            dbContext.SaveChanges();
            return jCarousel.Id;
        }

        public bool UpdateJCarousels(JCarousel entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var jCarousel = dbContext.JCarousels.Find(entity.Id);
            if (jCarousel == null)
                throw new ArgumentNullException(nameof(jCarousel));
            jCarousel.Name = entity.Name;
            jCarousel.Title = entity.Title;
            jCarousel.DataSourceType = entity.DataSourceType;
            jCarousel.CarouselType = entity.CarouselType;
            jCarousel.JCarousel_Product_Mapping = entity.JCarousel_Product_Mapping;

            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteJCarousels(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            var jCarousels = dbContext.JCarousels.Find(Id);
            if (jCarousels == null)
                throw new ArgumentNullException(nameof(jCarousels));

            dbContext.JCarousels.Remove(jCarousels);
            dbContext.SaveChanges();
            return true;
        }
        public JCarousel GetJCarouselById(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            var jCarousels = dbContext.JCarousels.Find(Id);
            if (jCarousels == null)
                throw new ArgumentNullException(nameof(jCarousels));

            return jCarousels;
        }
    }
}
