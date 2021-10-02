using MVT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVT.Web.ViewModels
{
     
        public class HomeViewModels
        {
            public List<Category> FeaturedCategories { get; set; }
            public List<Product> FeaturedProducts { get; set; }

        }

   
}