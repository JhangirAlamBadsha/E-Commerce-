using MVT.Entities;
using MVT.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVT.Web.Areas.Dashboard.ViewModels
{
    public class OrdersViewModel
    {
        public List<Order> Orders { get; set; }
        public Pager Pager { get; set; }
        public string Status { get; set; }
        public string UserID { get; set; }
    }
    public class OrderDetailsViewModel
    {
        public List<string> AvailableStatuses { get; set; }
        public Order Order { get; set; }
        public MVTUsers OrderBy { get; set; }
    }

}