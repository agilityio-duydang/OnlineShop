using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class PollVotingRecordDao
    {
        OnlineShopDbContext dbContext;
        public PollVotingRecordDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public int InsertPollVotingRecord(PollVotingRecord pollVotingRecord)
        {
            if (pollVotingRecord == null)
                throw new ArgumentNullException(nameof(pollVotingRecord));

            dbContext.PollVotingRecords.Add(pollVotingRecord);
            dbContext.SaveChanges();
            return pollVotingRecord.Id;
        }

        public bool UpdatePollVotingRecord(PollVotingRecord entity)
        {
            var pollVotingRecord = dbContext.PollVotingRecords.Find(entity.Id);
            if (pollVotingRecord == null)
                throw new ArgumentNullException(nameof(pollVotingRecord));

            pollVotingRecord.PollAnswerId = entity.PollAnswerId;
            pollVotingRecord.CustomerId = entity.CustomerId;

            return true;
        }
        public bool DeletePollVotingRecord(int pollVotingRecordId)
        {
            if (pollVotingRecordId == 0)
                return false;

            var pollVotingRecord = dbContext.PollVotingRecords.Find(pollVotingRecordId);
            if (pollVotingRecord == null)
                throw new ArgumentNullException(nameof(pollVotingRecord));

            dbContext.PollVotingRecords.Remove(pollVotingRecord);
            dbContext.SaveChanges();
            return true;
        }

        public PollVotingRecord GetPollVotingRecordById(int pollVotingRecordId)
        {
            if (pollVotingRecordId == 0)
                return null;

            return dbContext.PollVotingRecords.Find(pollVotingRecordId);
        }
        public IList<PollVotingRecord> GetPollVotingRecordsByPollAnswerId(int pollAnswerId)
        {
            if (pollAnswerId == 0)
                return null;

            return dbContext.PollVotingRecords.Where(x => x.PollAnswerId == pollAnswerId).OrderByDescending(x => x.Id).ToList();
        }
    }
}