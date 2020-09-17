using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
   public class ProductTagDao
    {
        OnlineShopDbContext dbContext;
        public ProductTagDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<ProductTag> GetProductTags(string name, int page , int pageSize)
        {
            IQueryable<ProductTag> productTags = dbContext.ProductTags;
            if (!String.IsNullOrWhiteSpace(name))
            {
                productTags = productTags.Where(x => x.Name.ToLower().Contains(name.ToLower().Trim()));
            }
            return productTags.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertProductTag(ProductTag productTag)
        {
            if (productTag == null)
                throw new ArgumentNullException(nameof(productTag));
        
            dbContext.ProductTags.Add(productTag);
            dbContext.SaveChanges();
            return productTag.Id;
        }

        public bool UpdateProductTag(ProductTag entity)
        {
            var productTag = dbContext.ProductTags.Find(entity.Id);
            if (productTag == null)
                throw new ArgumentNullException(nameof(productTag));

            productTag.Name = entity.Name;
            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteProductTag(int productTagId)
        {
            if (productTagId == 0)
                return false;
            var productTag = dbContext.ProductTags.Find(productTagId);
            if (productTag == null)
                throw new ArgumentNullException(nameof(productTag));

            dbContext.ProductTags.Remove(productTag);
            dbContext.SaveChanges();
            return true;
        }

        public ProductTag GetProductTagById(int productTagId)
        {
            if (productTagId == 0)
                return null;
            return dbContext.ProductTags.Find(productTagId);
        }
        public List<ProductTag> GetProductTagsByProductId(int productTagId)
        {
            if (productTagId == 0)
                return null;

            return dbContext.ProductTags.Where(x => x.Id == productTagId).OrderByDescending(x => x.Id).ToList();
        }
        public ProductTag GetProductTagByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;

            return dbContext.ProductTags.Where(x => x.Name.ToLower() == name.ToLower().Trim()).SingleOrDefault();
        }
    }
}
