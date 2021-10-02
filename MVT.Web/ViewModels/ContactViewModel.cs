using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVT.Web.ViewModels
{
    public class ContactViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Gmail { get; set; }
        public string Comment { get; set; }
    }
}