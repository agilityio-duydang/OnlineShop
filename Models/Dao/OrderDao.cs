using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
   public class OrderDao
    {
        OnlineShopDbContext dbContext;
        public OrderDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<Order> GetOrders(DateTime startDate, DateTime endDate, string productName, int orderStatus, int paymentStatus, int shippingStatus, string paymentMethod, int page, int pageSize)
        {
            IQueryable<Order> orders = dbContext.Orders;
            if (startDate != DateTime.MinValue)
            {
                orders = orders.Where(x => x.CreatedOnUtc >= startDate);
            }
            if (endDate != DateTime.MinValue)
            {
                orders = orders.Where(x => x.CreatedOnUtc <= endDate);
            }
            if (orderStatus > 0)
            {
                orders = orders.Where(x => x.OrderStatusId == orderStatus);
            }
            if (paymentStatus > 0)
            {
                orders = orders.Where(x => x.PaymentStatusId == paymentStatus);
            }
            if (shippingStatus > 0)
            {
                orders = orders.Where(x => x.ShippingStatusId == shippingStatus);
            }
            if (!String.IsNullOrWhiteSpace(paymentMethod))
            {
                orders = orders.Where(x => x.PaymentMethodSystemName == paymentMethod);
            }
            return orders.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InserOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            order.CreatedOnUtc = DateTime.UtcNow;
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
            return order.Id;
        }

        public string GetMaxOrderNumber()
        {
            int maxOrderNumber = Convert.ToInt32(dbContext.Orders.Max(x => x.CustomOrderNumber).FirstOrDefault().ToString());
            maxOrderNumber += 1;
            return maxOrderNumber.ToString();
        }
        public bool UpdateOrder(Order entity)
        {
            var order = dbContext.Orders.Find(entity.Id);
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            order.CustomerId = entity.CustomerId;
            order.BillingAddressId = entity.BillingAddressId;
            order.ShippingStatusId = entity.ShippingStatusId;
            order.OrderStatusId = entity.OrderStatusId;
            order.PaymentStatusId = entity.PaymentStatusId;
            order.PaymentMethodSystemName = entity.PaymentMethodSystemName;
            order.OrderSubtotalInclTax = entity.OrderSubtotalInclTax;
            order.OrderSubtotalExclTax = entity.OrderSubtotalExclTax;
            order.OrderSubTotalDiscountInclTax = entity.OrderSubTotalDiscountInclTax;
            order.OrderSubTotalDiscountExclTax = entity.OrderSubTotalDiscountExclTax;
            order.OrderShippingInclTax = entity.OrderShippingInclTax;
            order.OrderShippingExclTax = entity.OrderShippingExclTax;
            order.PaymentMethodAdditionalFeeInclTax = entity.PaymentMethodAdditionalFeeInclTax;
            order.PaymentMethodAdditionalFeeExclTax = entity.PaymentMethodAdditionalFeeExclTax;
            order.TaxRates = entity.TaxRates;
            order.OrderTax = entity.OrderTax;
            order.OrderDiscount = entity.OrderDiscount;
            order.OrderTotal = entity.OrderTotal;
            order.CustomerId = entity.CustomerId;
            order.ShippingMethod = entity.ShippingMethod;
            order.CustomOrderNumber = entity.CustomOrderNumber;

            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteOrder(int orderId)
        {
            if (orderId == 0)
                return false;

            var order = dbContext.Orders.Find(orderId);
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            dbContext.Orders.Remove(order);
            dbContext.SaveChanges();
            return true;
        }

        public Order GetOrderById(int orderId)
        {
            if (orderId == 0)
                return null;

            return dbContext.Orders.Find(orderId);
        }
        public Order GetOrderByOrderNumber(int orderNumber)
        {
            if (orderNumber == 0)
                return null;

            return dbContext.Orders.Where(x=>x.CustomOrderNumber==orderNumber.ToString()).ToList().FirstOrDefault();
        }
        public IEnumerable<Order> GetOrdersByCustomerId(long customerId, int page , int pageSize)
        {
            if (customerId == 0)
                return null;

            return dbContext.Orders.Where(x => x.CustomerId == customerId).OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
    }
}
