using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class PollDao
    {
        OnlineShopDbContext dbContext;
        public PollDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<Poll> GetPolls(int page, int pageSize)
        {
            return dbContext.Polls.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertPoll(Poll poll)
        {
            if (poll == null)
                throw new ArgumentNullException(nameof(poll));

            poll.LanguageId = 1;
            dbContext.Polls.Add(poll);
            dbContext.SaveChanges();
            return poll.Id;
        }

        public bool UpdatePoll(Poll entity)
        {
            var poll = dbContext.Polls.Find(entity.Id);
            if (poll == null)
                throw new ArgumentNullException(nameof(poll));

            poll.LanguageId = entity.LanguageId;
            poll.Name = entity.Name;
            poll.SystemKeyword = entity.SystemKeyword;
            poll.Published = entity.Published;
            poll.ShowOnHomePage = entity.ShowOnHomePage;
            poll.AllowGuestsToVote = entity.AllowGuestsToVote;
            poll.DisplayOrder = entity.DisplayOrder;
            poll.StartDateUtc = entity.StartDateUtc;
            poll.EndDateUtc = entity.EndDateUtc;

            dbContext.SaveChanges();
            return true;
        }

        public bool DeletePoll(int pollId)
        {
            if (pollId == 0)
                return false;

            var poll = dbContext.Polls.Find(pollId);
            if (poll == null)
                throw new ArgumentNullException(nameof(poll));

            dbContext.Polls.Remove(poll);
            dbContext.SaveChanges();
            return true;
        }

        public Poll GetPollById(int pollId)
        {
            if (pollId == 0)
                return null;

            return dbContext.Polls.Find(pollId);
        }

        public Poll GetPollByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;

            return dbContext.Polls.Where(x => x.Name.ToLower().Trim() == name.ToLower().Trim()).SingleOrDefault();
        }
    }
}