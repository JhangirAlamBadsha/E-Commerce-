﻿using MVT.Entities;
using MVT.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVT.Web.Areas.Dashboard.ViewModels
{
    public class NewCategoryViewModel
    {
        public string Name { get; set; }


        public string Description { get; set; }

        public string ImageURL { get; set; }

        public bool isFeatured { get; set; }
    }
    public class CategorySearchViewModel
    {
        public List<Category> Categories { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }
    public class EditCategoryViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string ImageURL { get; set; }

        public bool isFeatured { get; set; }
    }
}