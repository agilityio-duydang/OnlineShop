using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class GiftCardDao
    {
        OnlineShopDbContext dbContext;
        public GiftCardDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<GiftCard> GetGiftCards(string name, string couponCode, bool isActive, int page, int pageSize)
        {
            IQueryable<GiftCard> giftCards = dbContext.GiftCards;
            if (!String.IsNullOrWhiteSpace(name))
            {
                giftCards = giftCards.Where(x => x.RecipientName.ToLower().Contains(name.ToLower().Trim()));
            }
            if (!String.IsNullOrWhiteSpace(couponCode))
            {
                giftCards = giftCards.Where(x => x.GiftCardCouponCode.ToLower().Contains(couponCode.ToLower().Trim()));
            }
            giftCards = giftCards.Where(x => x.IsGiftCardActivated == isActive);
            return giftCards.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertGiftCard(GiftCard giftCard)
        {
            if (giftCard == null)
                throw new ArgumentNullException(nameof(giftCard));

            giftCard.CreatedOnUtc = DateTime.UtcNow;
            dbContext.GiftCards.Add(giftCard);
            dbContext.SaveChanges();
            return giftCard.Id;
        }
        public bool UpdateGiftCard(GiftCard entity)
        {
            var giftCard = dbContext.GiftCards.Find(entity.Id);
            if (giftCard == null)
                throw new ArgumentNullException(nameof(giftCard));

            giftCard.PurchasedWithOrderItemId = entity.PurchasedWithOrderItemId;
            giftCard.GiftCardTypeId = entity.GiftCardTypeId;
            giftCard.Amount = entity.Amount;
            giftCard.IsGiftCardActivated = entity.IsGiftCardActivated;
            giftCard.GiftCardCouponCode = entity.GiftCardCouponCode;
            giftCard.RecipientName = entity.RecipientName;
            giftCard.RecipientEmail = entity.RecipientEmail;
            giftCard.SenderName = entity.SenderName;
            giftCard.SenderEmail = entity.SenderEmail;
            giftCard.Message = entity.Message;
            giftCard.IsRecipientNotified = entity.IsRecipientNotified;

            dbContext.SaveChanges();
            return true;
        }
        public bool DeleteGiftCard(int giftCardId)
        {
            if (giftCardId == 0)
                return false;
            var giftCard = dbContext.GiftCards.Find(giftCardId);
            if (giftCard == null)
                throw new ArgumentNullException(nameof(giftCard));

            dbContext.GiftCards.Remove(giftCard);
            dbContext.SaveChanges();
            return true;
        }

        public GiftCard GetGiftCardById(int giftCardId)
        {
            if (giftCardId == 0)
                return null;

            return dbContext.GiftCards.Find(giftCardId);
        }
        public GiftCard GetGiftCardByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;

            return dbContext.GiftCards.Where(x => x.GiftCardCouponCode.ToLower() == name.ToLower().Trim()).SingleOrDefault();
        }
    }
}
