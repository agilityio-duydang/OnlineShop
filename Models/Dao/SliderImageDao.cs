using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class SliderImageDao
    {
        OnlineShopDbContext dbContext;
        public SliderImageDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<SliderImage> GetSliderImages(int page, int pageSize)
        {
            IQueryable<SliderImage> sliderImages = dbContext.SliderImages;
            return sliderImages.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertSliderImage(SliderImage sliderImage)
        {
            if (sliderImage == null)
                throw new ArgumentNullException(nameof(sliderImage));

            dbContext.SliderImages.Add(sliderImage);
            dbContext.SaveChanges();
            return sliderImage.Id;
        }

        public bool UpdateSliderImage(SliderImage entity)
        {
            var sliderImage = dbContext.SliderImages.Find(entity.Id);
            if (sliderImage == null)
                throw new ArgumentNullException(nameof(sliderImage));

            sliderImage.DisplayText = entity.DisplayText;
            sliderImage.Url = entity.Url;
            sliderImage.Alt = entity.Alt;
            sliderImage.Visible = entity.Visible;
            sliderImage.DisplayOrder = entity.DisplayOrder;
            sliderImage.PictureId = entity.PictureId;
            sliderImage.SliderId = entity.SliderId;

            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteSliderImage(int Id)
        {
            if (Id == 0)
                return false;

            var sliderImage = dbContext.SliderImages.Find(Id);
            if (sliderImage == null)
                throw new ArgumentNullException(nameof(sliderImage));

            dbContext.SliderImages.Remove(sliderImage);
            dbContext.SaveChanges();
            return true;
        }

        public SliderImage GetSliderImageById(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            var sliderImage = dbContext.SliderImages.Find(Id);
            if (sliderImage == null)
                throw new ArgumentNullException(nameof(sliderImage));

            return sliderImage;
        }
    }
}
