using MVT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVT.Web.ViewModels
{
    public class ProductsWidgetViewModels
    {
        public List<Product> Products { get; set; }
        public bool IsLatestProducts { get; set; }
    }
}