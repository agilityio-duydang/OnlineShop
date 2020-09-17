using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class Product_Picture_MappingDao
    {
        OnlineShopDbContext dbContext;
        public Product_Picture_MappingDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public int InsertProductPictureMapping(Product_Picture_Mapping product_Picture_Mapping)
        {
            if (product_Picture_Mapping == null)
                throw new ArgumentNullException(nameof(product_Picture_Mapping));

            dbContext.Product_Picture_Mapping.Add(product_Picture_Mapping);
            dbContext.SaveChanges();
            return product_Picture_Mapping.Id;
        }
    }
}
