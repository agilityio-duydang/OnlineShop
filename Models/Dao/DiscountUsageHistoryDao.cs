using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class DiscountUsageHistoryDao
    {
        OnlineShopDbContext dbContext;
        public DiscountUsageHistoryDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<DiscountUsageHistory> GetDiscountUsageHistories(int page , int pageSize)
        {
            return dbContext.DiscountUsageHistories.OrderByDescending(x=>x.Id).ToPagedList(page,pageSize);
        }
        public int InsertDiscountUsageHistory(DiscountUsageHistory discountUsageHistory)
        {
            if(discountUsageHistory==null)
                throw new ArgumentNullException(nameof(discountUsageHistory));

            dbContext.DiscountUsageHistories.Add(discountUsageHistory);
            dbContext.SaveChanges();
            return discountUsageHistory.Id;
        }

        public bool DeleteDiscountUsageHistory(int discountUsageHistoryId)
        {
            if(discountUsageHistoryId==0)
            return false;

            var discountUsageHistory = dbContext.DiscountUsageHistories.Find(discountUsageHistoryId);
            if(discountUsageHistory==null)
            throw new ArgumentNullException(nameof(discountUsageHistory));

            dbContext.DiscountUsageHistories.Remove(discountUsageHistory);
            dbContext.SaveChanges();
            return true;
        }

        public DiscountUsageHistory GetDiscountUsageHistoryById(int discountUsageHistoryId)
        {
            if(discountUsageHistoryId==0)
            return null;

            return dbContext.DiscountUsageHistories.Find(discountUsageHistoryId);
        }
    }
}
