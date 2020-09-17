using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Models
{
    public class CustomerListModel
    {
        public CustomerListModel()
        {
            SearchCustomerRoleIds = new List<int>();
            AvailableCustomerRoles = new List<SelectListItem>();
        }
        public PagedList.IPagedList<Customer> Customers { get; set; }
        public IList<int> SearchCustomerRoleIds { get; set; }
        public IList<SelectListItem> AvailableCustomerRoles { get; set; }

        public string SearchEmail { get; set; }

        public string SearchUsername { get; set; }

        public string SearchFirstName { get; set; }

        public string SearchLastName { get; set; }

        public int SearchDayOfBirth { get; set; }

        public int SearchMonthOfBirth { get; set; }

        public string SearchCompany { get; set; }

        public string SearchPhone { get; set; }

        public string SearchZipPostalCode { get; set; }

        public string SearchIpAddress { get; set; }
    }
}