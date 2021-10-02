using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVT.Entities
{
    public class Product :Basic
    {
        public decimal Price { get; set; }

        public string ImageURL { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}
