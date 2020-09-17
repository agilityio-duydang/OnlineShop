using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class CustomerRoleDao
    {
        OnlineShopDbContext dbContext;
        public CustomerRoleDao()
        {
            dbContext = new OnlineShopDbContext();           
        }

        public IEnumerable<CustomerRole> GetCustomerRoles(string name, int page , int pageSize)
        {
            IQueryable<CustomerRole> customerRoles = dbContext.CustomerRoles;
            if (!String.IsNullOrEmpty(name))
            {
                customerRoles = customerRoles.Where(x => x.Name.ToLower().Contains(name.ToLower().Trim()));
            }
            return customerRoles.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public List<CustomerRole> ListAll()
        {
            return dbContext.CustomerRoles.ToList();
        }
        public int InsertCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException(nameof(customerRole));

            dbContext.CustomerRoles.Add(customerRole);
            dbContext.SaveChanges();
            return customerRole.Id;
        }
        public bool UpdateCustomerRole(CustomerRole entity)
        {
            try
            {

                var customerRole = dbContext.CustomerRoles.Find(entity.Id);
                if (customerRole == null)
                    throw new ArgumentNullException(nameof(customerRole));

                customerRole.Name = entity.Name;
                customerRole.FreeShipping = entity.FreeShipping;
                customerRole.Active = entity.Active;
                customerRole.SystemName = entity.SystemName;
                customerRole.IsSystemRole = entity.IsSystemRole;
                customerRole.EnablePasswordLifetime = entity.EnablePasswordLifetime;
                customerRole.PurchasedWithProductId = entity.PurchasedWithProductId;

                dbContext.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
                throw new ArgumentException(ex.Message);
            }
        }

        public bool DeleteCustomerRole(int customerRoleId)
        {
            if (customerRoleId == 0)
                return false;

            var customerRole = dbContext.CustomerRoles.Find(customerRoleId);
            if (customerRole == null)
                throw new ArgumentNullException(nameof(customerRole));

            dbContext.CustomerRoles.Remove(customerRole);
            dbContext.SaveChanges();
            return true;
        }

        public CustomerRole GetCustomerRoleById(int customerRoleid)
        {
            if (customerRoleid == 0)
                return null;

            return dbContext.CustomerRoles.Find(customerRoleid);
        }
        public CustomerRole GetCustomerRoleByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;

            return dbContext.CustomerRoles.Where(x => x.Name.ToLower() == name.ToLower().Trim()).SingleOrDefault();
        }
    }
}
