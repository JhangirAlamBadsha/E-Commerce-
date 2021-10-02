using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVT.Entities
{
    public class Category :Basic
    {
        public string ImageURL { get; set; }
        public virtual List<Product> Product { get; set; }
        public bool isFeatured { get; set; }
    }
}
