using Microsoft.AspNet.Identity.EntityFramework;
using MVT.Entities;
using MVT.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVT.Web.Areas.Dashboard.ViewModels
{
    public class UserListingViewModel
    {
        public IEnumerable<MVTUsers> Users { get; set; }

        public string RoleID { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }

        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }
}