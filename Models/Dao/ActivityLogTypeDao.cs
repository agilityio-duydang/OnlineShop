using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
   public class ActivityLogTypeDao
    {
        OnlineShopDbContext dbContext;
        public ActivityLogTypeDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<ActivityLogType> GetActivityLogTypes(int page , int pageSize)
        {
            return dbContext.ActivityLogTypes.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public bool UpdateActivityLogTypes(ActivityLogType entity)
        {
            var activityLogTypes = dbContext.ActivityLogTypes.Find(entity.Id);
            if (activityLogTypes == null)
                throw new ArgumentNullException(nameof(activityLogTypes));

            activityLogTypes.Enabled = entity.Enabled;
            dbContext.SaveChanges();
            return true;
        }
    }
}
