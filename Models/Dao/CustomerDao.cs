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
    public class CustomerDao
    {
        private readonly OnlineShopDbContext dbContext;
        public CustomerDao()
        {
            dbContext = new OnlineShopDbContext();
        }
        public long InsertCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            customer.CreatedOnUtc = DateTime.UtcNow;       
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
            return customer.Id;
        }

        public bool UpdateCustomer(Customer entity)
        {
            try
            {
                var customer = dbContext.Customers.Find(entity.Id);
                if (customer == null)
                    throw new ArgumentNullException(nameof(customer));

                customer.Username = entity.Username;
                customer.FirstName = entity.FirstName;
                customer.LastName = entity.LastName;
                customer.Gender = entity.Gender;
                customer.DateOfBirth = entity.DateOfBirth;
                customer.Company = entity.Company;
                customer.Email = entity.Email;
                customer.EmailToRevalidate = entity.EmailToRevalidate;
                customer.AdminComment = entity.AdminComment;
                customer.AffiliateId = entity.AffiliateId;
                customer.VendorId = entity.VendorId;
                customer.HasShoppingCartItems = entity.HasShoppingCartItems;
                customer.RequireReLogin = entity.RequireReLogin;
                customer.FailedLoginAttempts = entity.FailedLoginAttempts;
                customer.CannotLoginUntilDateUtc = entity.CannotLoginUntilDateUtc;
                customer.Active = entity.Active;
                customer.Deleted = entity.Deleted;
                customer.IsSystemAccount = entity.IsSystemAccount;
                customer.SystemName = entity.SystemName;
                customer.LastIpAddress = entity.LastIpAddress;
                customer.LastLoginDateUtc = entity.LastLoginDateUtc;
                customer.LastActivityDateUtc = entity.LastActivityDateUtc;
                customer.BillingAddress_Id = entity.BillingAddress_Id;
                customer.ShippingAddress_Id = entity.ShippingAddress_Id;

                customer.CustomerRoles = entity.CustomerRoles;
                customer.Addresses = entity.Addresses;
                customer.Orders = entity.Orders;
                customer.ShoppingCartItems = entity.ShoppingCartItems;
                customer.ActivityLogs = entity.ActivityLogs;

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new ArgumentException(ex.Message);
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            if (customerId == 0)
                return false;

            var customer = dbContext.Customers.Find(customerId);
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            dbContext.Customers.Remove(customer);
            dbContext.SaveChanges();
            return true;

        }
        public IEnumerable<Customer> GetCustomers(string email, string firsName, string lastName, int day, int month, string company, IList<int> roles, int page, int pageSize)
        {
            IQueryable<Customer> model = dbContext.Customers;
            if (!string.IsNullOrWhiteSpace(email))
            {
                model = model.Where(x => x.Email.ToLower().Contains(email.ToLower().Trim()));
            }
            if (!string.IsNullOrWhiteSpace(firsName))
            {
                model = model.Where(x => x.FirstName.ToLower().Contains(firsName.ToLower().Trim()));
            }
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                model = model.Where(x => x.LastName.ToLower().Contains(lastName.ToLower().Trim()));
            }
            if (day > 0)
            {
                model = model.Where(x =>x.DateOfBirth.Value.Day == day);
            }
            if (month > 0)
            {
                model = model.Where(x => x.DateOfBirth.Value.Month == month);
            }
            if (!string.IsNullOrWhiteSpace(company))
            {
                model = model.Where(x => x.Address.Company.ToLower().Contains(company.ToLower().Trim()));
            }
            return model.OrderByDescending(x => x.CreatedOnUtc).ToPagedList(page, pageSize);
        }
        public Customer GetCustomerByUserName(string userName)
        {
            if (String.IsNullOrWhiteSpace(userName))
                return null;

            return dbContext.Customers.SingleOrDefault(x => x.Username.ToLower() == userName.ToLower().Trim());
        }

        public Customer GetCustomerById(int customerId)
        {
            if (customerId == 0)
                return null;

            var customer = dbContext.Customers.Find(customerId);
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            //customer.CustomerRoles = GetCustomerRoles(customer.Id);
            //customer.CustomerPasswords = GetCustomerPasswords(customerId);
            //customer.Orders = GetOrders(customer.Id);
            //customer.ShoppingCartItems = GetShoppingCartItems(customer.Id);
            //customer.ActivityLogs = GetActivityLogs(customer.Id);

            return customer;
        }


        public ICollection<ActivityLog> GetActivityLogs(int customerId)
        {
            if (customerId == 0)
                return null;

            return dbContext.ActivityLogs.Where(x => x.CustomerId == customerId).ToList();
        }
        public ICollection<ShoppingCartItem> GetShoppingCartItems(int customerId)
        {
            if (customerId == 0)
                return null;

            return dbContext.ShoppingCartItems.Where(x => x.CustomerId == customerId).ToList();
        }
        public ICollection<Order> GetOrders(int customerId)
        {
            if (customerId == 0)
                return null;

            return dbContext.Orders.Where(x => x.CustomerId == customerId).ToList();
        }

        public ICollection<Address> GetAddresses(int customerId)
        {
            if (customerId == 0)
                return null;

            var customer = dbContext.Customers.Find(customerId);
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            return dbContext.Addresses.Where(x => x.Customers2.SingleOrDefault().Id == customerId).ToList();
        }
        public CustomerRole GetCustomerRoleById(int customerRoleid)
        {
            if (customerRoleid == 0)
                return null;

            return dbContext.CustomerRoles.Find(customerRoleid);
        }
        public ICollection<CustomerRole> GetCustomerRoles(int customerId)
        {
            if (customerId == 0)
                return null;

            return dbContext.CustomerRoles.Where(x => x.Customers.SingleOrDefault().Id == customerId).ToList();
        }
        public int InsertCustomerPassword(CustomerPassword customerPassword)
        {
            if (customerPassword == null)
                throw new ArgumentNullException(nameof(customerPassword));

            dbContext.CustomerPasswords.Add(customerPassword);
            dbContext.SaveChanges();
            return customerPassword.Id;
        }
        public CustomerLoginResults Login(string userName, string passWord)
        {
            var customer = dbContext.Customers.SingleOrDefault(x => x.Username.ToLower() == userName.ToLower().Trim());
            if (customer == null)
                return CustomerLoginResults.CustomerNotExist;
            if (!customer.Active)
                return CustomerLoginResults.NotActive;
            if (customer.Deleted)
                return CustomerLoginResults.Deleted;
            if (customer.CannotLoginUntilDateUtc.HasValue && customer.CannotLoginUntilDateUtc.Value > DateTime.Now)
                return CustomerLoginResults.LockedOut;

            if (GetCustomerCurrentPassword(customer.Id).Password != passWord)
            {
                customer.FailedLoginAttempts++;
                UpdateCustomer(customer);

                return CustomerLoginResults.WrongPassword;
            }
            customer.FailedLoginAttempts = 0;
            customer.CannotLoginUntilDateUtc = null;
            customer.RequireReLogin = false;
            customer.LastLoginDateUtc = DateTime.UtcNow;
            UpdateCustomer(customer);

            return CustomerLoginResults.Successful;
        }
        public Customer GetCustomerByEmail(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
                return null;

            return dbContext.Customers.Where(x => x.Email.ToLower() == email.ToLower().Trim()).FirstOrDefault(); ;
        }
        public bool ChangeStatusCustomer(long customerId)
        {
            if (customerId == 0)
                return false;

            var customer = dbContext.Customers.Find(customerId);
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            customer.Active = !customer.Active;
            dbContext.SaveChanges();
            return customer.Active;
        }

        public CustomerPassword GetCustomerCurrentPassword(long customerId)
        {
            if (customerId == 0)
                return null;

            return GetCustomerPasswords(customerId).FirstOrDefault();

        }
        public IList<CustomerPassword> GetCustomerPasswords(long customerId)
        {
            if (customerId == 0)
                return null;

            return dbContext.CustomerPasswords.Where(x => x.CustomerId == customerId).ToList();
        }
    }
}
