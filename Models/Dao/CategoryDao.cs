using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class CategoryDao
    {
        OnlineShopDbContext context;

        public CategoryDao()
        {
            context = new OnlineShopDbContext();
        }

        public IEnumerable<Category> ListAllCategories(string name, int published, int pageNumber, int pageSize)
        {
            IQueryable<Category> categories = context.Categories;
            if (!String.IsNullOrWhiteSpace(name))
            {
                categories = categories.Where(x => x.Name.ToLower().Contains(name.ToLower().Trim()));
            }
            if (published > 0)
            {
                if (published == 1)
                {
                    categories = categories.Where(x => x.Published == true);
                }
                else
                {
                    categories = categories.Where(x => x.Published == false);
                }
            }
            return categories.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);
        }
        public List<Category> ListAllCategories()
        {
            List<Category> categories = context.Categories.OrderByDescending(x => x.Id).ToList(); ;
            var category = new Category
            {
                Id = 0,
                Name = "[None]",
            };
            categories.Add(category);

            return categories.OrderByDescending(x => x.Id).ToList();
        }
        public long InsertCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            category.CreatedOnUtc = DateTime.UtcNow;
            category.UpdatedOnUtc = DateTime.UtcNow;
            context.Categories.Add(category);
            context.SaveChanges();
            return category.Id;
        }

        public bool UpdateCategory(Category entity)
        {
            var category = context.Categories.Find(entity.Id);
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            category.Name = entity.Name;
            category.Description = entity.Description;
            category.MetaKeywords = entity.MetaKeywords;
            category.MetaDescription = entity.MetaDescription;
            category.MetaTitle = entity.MetaTitle;
            category.ParentCategoryId = entity.ParentCategoryId;
            category.PictureId = entity.PictureId;
            category.PageSize = entity.PageSize;
            category.AllowCustomersToSelectPageSize = entity.AllowCustomersToSelectPageSize;
            category.PageSizeOptions = entity.PageSizeOptions;
            category.PriceRanges = entity.PriceRanges;
            category.ShowOnHomePage = entity.ShowOnHomePage;
            category.IncludeInTopMenu = entity.IncludeInTopMenu;
            category.Published = entity.Published;
            category.Deleted = entity.Deleted;
            category.DisplayOrder = entity.DisplayOrder;
            category.UpdatedOnUtc = DateTime.UtcNow;

            context.SaveChanges();
            return true;
        }

        public bool DeleteCategory(long categoryId)
        {
            if (categoryId == 0)
                return false;

            var category = context.Categories.Find(categoryId);
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            context.Categories.Remove(category);
            context.SaveChanges();
            return true;
        }

        public Category GetCategoryById(long categoryId)
        {
            if (categoryId == 0)
                return null;

            return context.Categories.Find(categoryId);
        }
        public Category GetCategoryByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;

            return context.Categories.Where(x => x.Name.ToLower() == name.ToLower().Trim()).SingleOrDefault();
        }
    }
}
