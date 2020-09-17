using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class DiscountRequirementDao
    {
        OnlineShopDbContext dbContext;
        public DiscountRequirementDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<DiscountRequirement> GetDiscountRequirements(int page , int pageSize)
        {
            return dbContext.DiscountRequirements.OrderByDescending(x => x.Id).ToPagedList(page,pageSize);
        }
        public int InsertDiscountRequirement(DiscountRequirement discountRequirement)
        {
            if (discountRequirement == null)
                throw new ArgumentNullException(nameof(discountRequirement));

            dbContext.DiscountRequirements.Add(discountRequirement);
            dbContext.SaveChanges();
            return discountRequirement.Id;
        }
        public bool DeleteDiscountRequirement(int discountRequirementId)
        {
            if(discountRequirementId==0)
                return false;

             var discountRequirement = dbContext.DiscountRequirements.Find(discountRequirementId);
             if(discountRequirement==null)
                throw new ArgumentNullException(nameof(discountRequirement));
            
            dbContext.DiscountRequirements.Remove(discountRequirement);
            dbContext.SaveChanges();
            return true;
        }
    }
}
