using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Models.Dao
{
    public class PollAnswerDao
    {
        OnlineShopDbContext dbContext;
        public PollAnswerDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<PollAnswer> GetPollAnswers(int page, int pageSize)
        {
            return dbContext.PollAnswers.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public int InsertPollAnswer(PollAnswer pollAnswer)
        {
            if (pollAnswer == null)
                throw new ArgumentNullException(nameof(pollAnswer));

            dbContext.PollAnswers.Add(pollAnswer);
            dbContext.SaveChanges();
            return pollAnswer.Id;
        }

        public bool UpdatePollAnswer(PollAnswer entity)
        {
            var pollAnswer = dbContext.PollAnswers.Find(entity.Id);
            if (pollAnswer == null)
                throw new ArgumentNullException(nameof(pollAnswer));

            pollAnswer.PollId = entity.PollId;
            pollAnswer.Name = entity.Name;
            pollAnswer.NumberOfVotes = entity.NumberOfVotes;
            pollAnswer.DisplayOrder = entity.DisplayOrder;
            return false;
        }
        public bool DeletePollAnswer(int pollAnswerId)
        {
            if (pollAnswerId == 0)
                return false;

            var pollAnswer = dbContext.PollAnswers.Find(pollAnswerId);
            if (pollAnswer == null)
                throw new ArgumentNullException(nameof(pollAnswer));

            dbContext.PollAnswers.Remove(pollAnswer);
            dbContext.SaveChanges();
            return true;
        }

        public PollAnswer GetPollAnswerById(int pollAnswerId)
        {
            if (pollAnswerId == 0)
                return null;

            return dbContext.PollAnswers.Find(pollAnswerId);
        }

        public PollAnswer GetPollAnswerByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;

            return dbContext.PollAnswers.Where(x => x.Name.ToLower().Trim() == name.ToLower().Trim()).SingleOrDefault();
        }
    }
}