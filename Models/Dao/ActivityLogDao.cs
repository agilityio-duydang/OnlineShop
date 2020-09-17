using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ActivityLogDao
    {
        OnlineShopDbContext dbContext;
        public ActivityLogDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<ActivityLog> GetActivityLogs(DateTime createdFrom , DateTime createdTo,int activitylogTypeId,string ipAddress, int page , int pageSize)
        {
            IQueryable<ActivityLog> activityLogs = dbContext.ActivityLogs;
            if (createdFrom != null)
            {
                activityLogs = activityLogs.Where(x => x.CreatedOnUtc >= createdFrom);
            }
            if (createdTo!=null)
            {
                activityLogs = activityLogs.Where(x => x.CreatedOnUtc <= createdTo);
            }
            if (activitylogTypeId >0)
            {
                activityLogs = activityLogs.Where(x => x.ActivityLogTypeId == activitylogTypeId);
            }
            if (!String.IsNullOrWhiteSpace(ipAddress))
            {
                activityLogs = activityLogs.Where(x => x.IpAddress == ipAddress);
            }
            return activityLogs.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public int InsertActivityLog(ActivityLog activityLog)
        {
            if (activityLog == null)
                throw new ArgumentNullException(nameof(activityLog));

            dbContext.ActivityLogs.Add(activityLog);
            dbContext.SaveChanges();
            return activityLog.Id;
        }

        public bool DeleteActivityLog(int activityLogId)
        {
            if (activityLogId == 0)
                return false;

            var activityLog = dbContext.ActivityLogs.Find(activityLogId);
            if (activityLog == null)
                throw new ArgumentNullException(nameof(activityLog));

            dbContext.ActivityLogs.Remove(activityLog);
            dbContext.SaveChanges();
            return true;
        }
        public ActivityLog GetActivityLogById(int activityLogId)
        {
            if (activityLogId == 0)
                return null;

            return dbContext.ActivityLogs.Find(activityLogId); 
        }
    }
}
