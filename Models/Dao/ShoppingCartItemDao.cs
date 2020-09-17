using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Models.Dao
{
    public class ShoppingCartItemDao
    {
        OnlineShopDbContext dbContext;
        public ShoppingCartItemDao()
        {
            dbContext = new OnlineShopDbContext();
        }
        public IEnumerable<ShoppingCartItem> GetShoppingCartItems(int page, int pageSize)
        {
            return dbContext.ShoppingCartItems.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            if (shoppingCartItem == null)
                throw new ArgumentNullException(nameof(shoppingCartItem));

            dbContext.ShoppingCartItems.Add(shoppingCartItem);
            dbContext.SaveChanges();
            return shoppingCartItem.Id;
        }

        public bool UpdateShoppingCartItem(ShoppingCartItem entity)
        {
            var shoppingCartItem = dbContext.ShoppingCartItems.Find(entity.Id);
            if (shoppingCartItem == null)
                throw new ArgumentNullException(nameof(shoppingCartItem));

            shoppingCartItem.ShoppingCartTypeId = entity.ShoppingCartTypeId;
            shoppingCartItem.CustomerId = entity.CustomerId;
            shoppingCartItem.ProductId = entity.ProductId;
            shoppingCartItem.CustomerEnteredPrice = entity.CustomerEnteredPrice;
            shoppingCartItem.Quantity = entity.Quantity;
            shoppingCartItem.RentalStartDateUtc = entity.RentalStartDateUtc;
            shoppingCartItem.RentalEndDateUtc = entity.RentalEndDateUtc;

            dbContext.SaveChanges();
            return true;
        }
        public bool DeleteShoppingCartItem(int shoppingCartItemId)
        {
            if (shoppingCartItemId == 0)
                return false;

            var shoppingCartItem = dbContext.ShoppingCartItems.Find(shoppingCartItemId);
            if (shoppingCartItem == null)
                throw new ArgumentNullException(nameof(shoppingCartItem));

            dbContext.ShoppingCartItems.Remove(shoppingCartItem);
            dbContext.SaveChanges();
            return true;
        }
        public ShoppingCartItem GetShoppingCartItemById(int shoppingCartItemId)
        {
            if(shoppingCartItemId==0)
            return null;

            return dbContext.ShoppingCartItems.Find(shoppingCartItemId);
        }
    }
}