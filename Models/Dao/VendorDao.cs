using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class VendorDao
    {
        OnlineShopDbContext dbContext;
        public VendorDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public IEnumerable<Vendor> GetVendors(string name ,string email , int page , int pageSize)
        {
            IQueryable<Vendor> vendors = dbContext.Vendors;
            if (!String.IsNullOrWhiteSpace(name))
            {
                vendors = vendors.Where(x => x.Name.ToLower().Contains(name.ToLower().Trim()));
            }
            if (!String.IsNullOrWhiteSpace(email))
            {
                vendors = vendors.Where(x => x.Email.ToLower().Contains(email.ToLower().Trim()));
            }
            return vendors.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public List<Vendor> GetVendors()
        {
            return dbContext.Vendors.ToList();
        }
        public int InsertVendor(Vendor vendor)
        {
            if (vendor == null)
                throw new ArgumentNullException(nameof(vendor));
            
            dbContext.Vendors.Add(vendor);
            dbContext.SaveChanges();
            return vendor.Id;
        }

        public bool UpdateVendor(Vendor entity)
        {
            var vendor = dbContext.Vendors.Find(entity.Id);
            if (vendor == null)
                throw new ArgumentNullException(nameof(vendor));

            vendor.Name = entity.Name;
            vendor.Email = entity.Email;
            vendor.Description = entity.Description;
            vendor.PictureId = entity.PictureId;
            vendor.AddressId = entity.AddressId;
            vendor.AdminComment = entity.AdminComment;
            vendor.Active = entity.Active;
            vendor.DisplayOrder = entity.DisplayOrder;
            vendor.MetaKeywords = entity.MetaKeywords;
            vendor.MetaDescription = entity.MetaDescription;
            vendor.MetaTitle = entity.MetaTitle;

            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteVendor(int vendorId)
        {
            if (vendorId == 0)
                return false;

            var vendor = dbContext.Vendors.Find(vendorId);
            if (vendor == null)
                throw new ArgumentNullException(nameof(vendor));

            dbContext.Vendors.Remove(vendor);
            dbContext.SaveChanges();

            return true;
        }

        public Vendor GetVendorById(int vendorId)
        {
            if (vendorId == 0)
                return null;

            return dbContext.Vendors.Find(vendorId);
        }
        public Vendor GetVendorByName(string name)
        {
            if (!String.IsNullOrWhiteSpace(name))
                return null;

            return dbContext.Vendors.Where(x => x.Name.ToLower() == name.ToLower().Trim()).SingleOrDefault();
        }
        public Vendor GetVendorByEmail(string email)
        {
            if (!String.IsNullOrWhiteSpace(email))
                return null;

            return dbContext.Vendors.Where(x => x.Email.ToLower() == email.ToLower().Trim()).SingleOrDefault();
        }
    }
}
