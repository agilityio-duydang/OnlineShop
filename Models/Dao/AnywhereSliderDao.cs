using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class AnywhereSliderDao
    {
        OnlineShopDbContext dbContext;
        public AnywhereSliderDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<AnywhereSlider> GetAnywhereSliders(int page, int pageSize)
        {
            IQueryable<AnywhereSlider> anywhereSliders = dbContext.AnywhereSliders;

            return anywhereSliders.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public int InsertAnywhereSlider(AnywhereSlider anywhereSlider)
        {
            if (anywhereSlider == null)
                throw new ArgumentNullException(nameof(anywhereSlider));

            dbContext.AnywhereSliders.Add(anywhereSlider);
            dbContext.SaveChanges();
            return anywhereSlider.Id;
        }
        public bool UpdateAnywhereSlider(AnywhereSlider entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var anywhereSlider = dbContext.AnywhereSliders.Find(entity.Id);
            if (anywhereSlider == null)
                throw new ArgumentNullException(nameof(anywhereSlider));

            anywhereSlider.SystemName = entity.SystemName;
            anywhereSlider.SliderType = entity.SliderType;
            dbContext.SaveChanges();

            return true;
        }

        public bool DeleteAnywhereSlider(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            var anywhereSlider = dbContext.AnywhereSliders.Find(Id);
            if (anywhereSlider == null)
                throw new ArgumentNullException(nameof(anywhereSlider));

            dbContext.AnywhereSliders.Remove(anywhereSlider);
            dbContext.SaveChanges();
            return true;
        }

        public AnywhereSlider GetAnywhereSliderById(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            var anywhereSlider = dbContext.AnywhereSliders.Find(Id);
            if (anywhereSlider == null)
                throw new ArgumentNullException(nameof(anywhereSlider));

            return anywhereSlider;
        }
    }
}
