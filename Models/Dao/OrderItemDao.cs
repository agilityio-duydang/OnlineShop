using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class OrderItemDao
    {
        OnlineShopDbContext dbContext;
        public OrderItemDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<OrderItem> GetOrderItems(int page, int pageSize)
        {
            return dbContext.OrderItems.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertOrderItem(OrderItem orderItem)
        {
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem));

            dbContext.OrderItems.Add(orderItem);
            dbContext.SaveChanges();
            return orderItem.Id;
        }

        public bool UpdateOrderItem(OrderItem entity)
        {
            var orderItem = dbContext.OrderItems.Find(entity.Id);
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem));

            orderItem.OrderItemGuid = entity.OrderItemGuid;
            orderItem.OrderId = entity.OrderId;
            orderItem.ProductId = entity.ProductId;
            orderItem.Quantity = entity.Quantity;
            orderItem.UnitPriceInclTax = entity.UnitPriceInclTax;
            orderItem.UnitPriceExclTax = entity.UnitPriceExclTax;
            orderItem.PriceInclTax = entity.PriceInclTax;
            orderItem.PriceExclTax = entity.PriceExclTax;
            orderItem.DiscountAmountInclTax = entity.DiscountAmountInclTax;
            orderItem.DiscountAmountExclTax = entity.DiscountAmountExclTax;
            orderItem.OriginalProductCost = entity.OriginalProductCost;
            orderItem.ItemWeight = entity.ItemWeight;
            orderItem.RentalStartDateUtc = entity.RentalStartDateUtc;
            orderItem.RentalEndDateUtc = entity.RentalEndDateUtc;

            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteOrderItem(int orderItemId)
        {
            if (orderItemId == 0)
                return false;

            var orderItem = dbContext.OrderItems.Find(orderItemId);
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem));

            dbContext.OrderItems.Remove(orderItem);
            dbContext.SaveChanges();
            return true;
        }

        public OrderItem GetOrderItemById(int orderItemId)
        {
            if (orderItemId == 0)
                return null;

            return dbContext.OrderItems.Find(orderItemId);
        }
    }
}