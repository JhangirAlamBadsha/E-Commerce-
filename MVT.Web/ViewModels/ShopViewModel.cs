using MVT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVT.Web.ViewModels
{
    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProductIDs { get; set; }

        public MVTUsers User { get; set; }
    }
    public class ShopViewModel
    {
        public int MaximumPrice { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public List<Product> Products { get; set; }
        public int? SortBy { get; set; }


        public int? categoryID { get; set; }
        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
    }
    public class FilterProductsViewModel
    {

        public List<Product> Products { get; set; }
        public Pager Pager { get; set; }
        public int? SortBy { get; set; }
        public int? categoryID { get; set; }
        public string SearchTerm { get; set; }

    }
}