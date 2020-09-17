using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class AddressDao
    {
        OnlineShopDbContext dbContext;
        public AddressDao()
        {
            dbContext = new OnlineShopDbContext();
        }

        public Address GetAddressById(int addressId)
        {
            if (addressId == 0)
                return null;

            return dbContext.Addresses.Find(addressId);
        }

        public int InsertAddress(Address address)
        {
            if(address==null)
                throw new ArgumentNullException(nameof(address));

            address.CreatedOnUtc = DateTime.UtcNow;
            dbContext.Addresses.Add(address);
            dbContext.SaveChanges();
            return address.Id;
        }

        public bool UpdateAddress (Address entity)
        {
            try
            {
                var address = dbContext.Addresses.Find(entity.Id);
                if (address == null)
                    throw new ArgumentNullException(nameof(address));

                address.FirstName = entity.FirstName;
                address.LastName = entity.LastName;
                address.Email = entity.Email;
                address.Company = entity.Company;
                address.City = entity.City;
                address.Address1 = entity.Address1;
                address.Address2 = entity.Address2;
                address.ZipPostalCode = entity.ZipPostalCode;
                address.PhoneNumber = entity.PhoneNumber;
                address.FaxNumber = entity.FaxNumber;
                address.CreatedOnUtc = DateTime.UtcNow;

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new ArgumentException(ex.Message);
            }
        }
        public bool DeleteAddress(int addressId)
        {
            if (addressId == 0)
                return false;

            var address = dbContext.Addresses.Find(addressId);
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            dbContext.Addresses.Remove(address);
            dbContext.SaveChanges();
            return true;
        }
    }
}
