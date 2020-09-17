using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class PictureDao
    {
        OnlineShopDbContext dbContext;
        public PictureDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<Picture> GetPictures(int page, int pageSize)
        {
            IEnumerable<Picture> pictures = dbContext.Pictures;
            return pictures.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertPicture(Picture picture)
        {
            if (picture == null)
                throw new ArgumentNullException(nameof(picture));

            dbContext.Pictures.Add(picture);
            dbContext.SaveChanges();
            return picture.Id;
        }
        public virtual Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename,
    string altAttribute = null, string titleAttribute = null,
    bool isNew = true, bool validateBinary = true)
        {

            var picture = new Picture
            {
                PictureBinary = pictureBinary,
                MimeType = mimeType,
                SeoFilename = seoFilename,
                AltAttribute = altAttribute,
                TitleAttribute = titleAttribute,
                IsNew = isNew,
            };
            dbContext.Pictures.Add(picture);
            dbContext.SaveChanges();
            return picture;
        }

        public virtual IList<Picture> GetPicturesByProductId(int productId, int recordsToReturn = 0)
        {
            if (productId == 0)
                return new List<Picture>();

            var query = from p in dbContext.Pictures
                        join pp in dbContext.Product_Picture_Mapping on p.Id equals pp.PictureId
                        orderby pp.DisplayOrder, pp.Id
                        where pp.ProductId == productId
                        select p;

            if (recordsToReturn > 0)
                query = query.Take(recordsToReturn);

            var pics = query.ToList();
            return pics;
        }
        public bool UpdatePicture(Picture entity)
        {
            var picture = dbContext.Pictures.Find(entity.Id);
            if (picture == null)
                throw new ArgumentNullException(nameof(picture));

            picture.PictureBinary = entity.PictureBinary;
            picture.MimeType = entity.MimeType;
            picture.SeoFilename = entity.SeoFilename;
            picture.AltAttribute = entity.AltAttribute;
            picture.TitleAttribute = entity.TitleAttribute;
            picture.IsNew = entity.IsNew;

            dbContext.SaveChanges();
            return true;
        }
        public bool DeletePicture(int pictureId)
        {
            if (pictureId == 0)
                return false;

            var picture = dbContext.Pictures.Find(pictureId);
            if (picture == null)
                throw new ArgumentNullException(nameof(picture));

            dbContext.Pictures.Remove(picture);
            dbContext.SaveChanges();
            return true;
        }

        public Picture GetPictureById(int pictureId)
        {
            if (pictureId == 0)
                return null;

            return dbContext.Pictures.Find(pictureId);
        }
    }
}
