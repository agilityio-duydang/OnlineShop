using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class GiftCardUsageHistoryDao
    {
        OnlineShopDbContext dbContext;
        public GiftCardUsageHistoryDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<GiftCardUsageHistory> GetCardUsageHistories(int page, int pageSize)
        {
            return dbContext.GiftCardUsageHistories.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertGiftCardUsageHistory(GiftCardUsageHistory cardUsageHistory)
        {
            if (cardUsageHistory == null)
                throw new ArgumentNullException(nameof(cardUsageHistory));

            dbContext.GiftCardUsageHistories.Add(cardUsageHistory);
            dbContext.SaveChanges();
            return cardUsageHistory.Id;
        }

        public GiftCardUsageHistory GetGiftCardUsageHistoryById(int gifCardUsageHistoryId)
        {
            if (gifCardUsageHistoryId == 0)
                return null;

            return dbContext.GiftCardUsageHistories.Find(gifCardUsageHistoryId);
        }
    }
}