using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class SaleOfTheDayOfferDao
    {
        OnlineShopDbContext dbContext;
        public SaleOfTheDayOfferDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<SaleOfTheDayOffer> GetSaleOfTheDayOffers(int page, int pageSize)
        {
            IQueryable<SaleOfTheDayOffer> saleOfTheDayOffers = dbContext.SaleOfTheDayOffers;

            return saleOfTheDayOffers.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertSaleOfTheDayOffer(SaleOfTheDayOffer saleOfTheDayOffer)
        {
            if (saleOfTheDayOffer == null)
                throw new ArgumentNullException(nameof(saleOfTheDayOffer));

            dbContext.SaleOfTheDayOffers.Add(saleOfTheDayOffer);
            dbContext.SaveChanges();

            return saleOfTheDayOffer.Id;
        }
        public bool UpdateSaleOfTheDayOffer(SaleOfTheDayOffer entity)
        {
            var saleOfTheDayOffer = dbContext.SaleOfTheDayOffers.Find(entity.Id);
            if (saleOfTheDayOffer == null)
                throw new ArgumentNullException(nameof(saleOfTheDayOffer));

            saleOfTheDayOffer.Published = entity.Published;
            saleOfTheDayOffer.Title = entity.Title;
            saleOfTheDayOffer.FromDate = entity.FromDate;
            saleOfTheDayOffer.ToDate = entity.ToDate;
            saleOfTheDayOffer.SchedulePatternType = entity.SchedulePatternType;
            saleOfTheDayOffer.SchedulePatternFromTime = entity.SchedulePatternFromTime;
            saleOfTheDayOffer.SchedulePatternToTime = entity.SchedulePatternToTime;
            saleOfTheDayOffer.ExactDayValue = entity.ExactDayValue;
            saleOfTheDayOffer.EveryMonthFromDayValue = entity.EveryMonthFromDayValue;
            saleOfTheDayOffer.EveryMonthToDayValue = entity.EveryMonthToDayValue;
            saleOfTheDayOffer.SaleOfTheDayOffer_Product_Mapping = entity.SaleOfTheDayOffer_Product_Mapping;

            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteSaleOfTheDayOffer(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            var saleOfTheDayOffer = dbContext.SaleOfTheDayOffers.Find(Id);
            if (saleOfTheDayOffer == null)
                throw new ArgumentNullException(nameof(saleOfTheDayOffer));

            dbContext.SaleOfTheDayOffers.Remove(saleOfTheDayOffer);
            dbContext.SaveChanges();
            return true;
        }

        public SaleOfTheDayOffer SaleOfTheDayOfferById(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            var saleOfTheDayOffer = dbContext.SaleOfTheDayOffers.Find(Id);
            if (saleOfTheDayOffer == null)
                throw new ArgumentNullException(nameof(saleOfTheDayOffer));

            return saleOfTheDayOffer;
        }    
    }
}
