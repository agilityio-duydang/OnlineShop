using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ManufacturerDao
    {
        OnlineShopDbContext dbContext;
        public ManufacturerDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<Manufacturer> GetAllManufacturers(string name, int published, int page, int pageSize)
        {
            IQueryable<Manufacturer> manufacturers = dbContext.Manufacturers;
            if (!String.IsNullOrWhiteSpace(name))
            {
                manufacturers = manufacturers.Where(x => x.Name.ToLower().Contains(name.ToLower().Trim()));
            }
            if (published > 0)
            {
                if (published == 1)
                {
                    manufacturers = manufacturers.Where(x => x.Published == true);
                }
                else
                {
                    manufacturers = manufacturers.Where(x => x.Published == false);
                }
            }
            return manufacturers.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public List<Manufacturer> GetManufacturers()
        {
            return dbContext.Manufacturers.ToList();
        }
        public int InsertManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));
            manufacturer.CreatedOnUtc = DateTime.UtcNow;
            manufacturer.UpdatedOnUtc = DateTime.UtcNow;
            dbContext.Manufacturers.Add(manufacturer);
            dbContext.SaveChanges();
            return manufacturer.Id;
        }

        public bool UpdateManufacturer(Manufacturer entity)
        {
            try
            {
                var manufacturer = dbContext.Manufacturers.Find(entity.Id);
                if (manufacturer == null)
                    throw new ArgumentNullException(nameof(manufacturer));

                manufacturer.Name = entity.Name;
                manufacturer.Description = entity.Description;
                manufacturer.ManufacturerTemplateId = entity.ManufacturerTemplateId;
                manufacturer.MetaKeywords = entity.MetaKeywords;
                manufacturer.MetaDescription = entity.MetaDescription;
                manufacturer.MetaTitle = entity.MetaTitle;
                manufacturer.PictureId = entity.PictureId;
                manufacturer.PageSize = entity.PageSize;
                manufacturer.AllowCustomersToSelectPageSize = entity.AllowCustomersToSelectPageSize;
                manufacturer.PageSizeOptions = entity.PageSizeOptions;
                manufacturer.PriceRanges = entity.PriceRanges;
                manufacturer.Published = entity.Published;
                manufacturer.Deleted = entity.Deleted;
                manufacturer.DisplayOrder = entity.DisplayOrder;
                manufacturer.UpdatedOnUtc = DateTime.UtcNow;
                dbContext.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
                throw new ArgumentException(ex.Message);
            }
        }
        public bool DeleteManufacturer(int manufacturerId)
        {
            if (manufacturerId == 0)
                return false;

            var manufacturer = dbContext.Manufacturers.Find(manufacturerId);
            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

            dbContext.Manufacturers.Remove(manufacturer);
            dbContext.SaveChanges();
            return true;
        }

        public Manufacturer GetManufacturerById(int manufacturerId)
        {
            if (manufacturerId == 0)
                return null;

            return dbContext.Manufacturers.Find(manufacturerId);
        }
        public Manufacturer GetManufacturerByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;

            return dbContext.Manufacturers.Where(x => x.Name.ToLower() == name.ToLower().Trim()).SingleOrDefault();
        }
    }
}
