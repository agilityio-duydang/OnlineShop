using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class LanguageDao
    {
        OnlineShopDbContext dbContext;
        public LanguageDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<Language> GetLanguages(int page, int pageSize)
        {
            return dbContext.Languages.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int InsertLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            dbContext.Languages.Add(language);
            dbContext.SaveChanges();
            return language.Id;
        }

        public bool UpdateLanguage(Language entity)
        {
            var language = dbContext.Languages.Find(entity.Id);
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            language.Name = entity.Name;
            language.LanguageCulture = entity.LanguageCulture;
            language.UniqueSeoCode = entity.UniqueSeoCode;
            language.FlagImageFileName = entity.FlagImageFileName;
            language.Rtl = entity.Rtl;
            language.DefaultCurrencyId = entity.DefaultCurrencyId;
            language.Published = entity.Published;
            language.DisplayOrder = entity.DisplayOrder;

            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteLanguage(int languageId)
        {
            if (languageId == 0)
                return false;

            var language = dbContext.Languages.Find(languageId);
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            dbContext.Languages.Remove(language);
            dbContext.SaveChanges();
            return true;
        }

        public Language GetLanguageById(int languageId)
        {
            if (languageId == 0)
                return null;

            return dbContext.Languages.Find(languageId);
        }
        public Language GetLanguageByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;

            return dbContext.Languages.Where(x => x.Name.ToLower().Trim() == name.ToLower().Trim()).SingleOrDefault();
        }
    }
}