using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ProductsGroupDao
    {
        OnlineShopDbContext dbContext;
        public ProductsGroupDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<ProductsGroup> GetProductsGroups(int page, int pageSize)
        {
            IQueryable<ProductsGroup> productsGroups = dbContext.ProductsGroups;

            return productsGroups.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertProductsGroup(ProductsGroup productsGroup)
        {
            if (productsGroup == null)
                throw new ArgumentNullException(nameof(productsGroup));

            dbContext.ProductsGroups.Add(productsGroup);
            dbContext.SaveChanges();

            return productsGroup.Id;
        }

        public bool UpdateProductsGroup(ProductsGroup entity)
        {
            var productsGroup = dbContext.ProductsGroups.Find(entity.Id);
            if (productsGroup == null)
                throw new ArgumentNullException(nameof(productsGroup));

            productsGroup.Published = entity.Published;
            productsGroup.Title = entity.Title;
            productsGroup.NumberOfProductsPerItem = entity.NumberOfProductsPerItem;
            productsGroup.DisplayOrder = entity.DisplayOrder;

            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteProductsGroup(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            var productsGroups = dbContext.ProductsGroups.Find(Id);
            if (productsGroups == null)
                throw new ArgumentNullException(nameof(productsGroups));

            dbContext.ProductsGroups.Remove(productsGroups);
            dbContext.SaveChanges();
            return true;
        }

        public ProductsGroup GetProductsGroupById(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            var productsGroups = dbContext.ProductsGroups.Find(Id);
            if (productsGroups == null)
                throw new ArgumentNullException(nameof(productsGroups));

            return productsGroups;
        }
    }
}
