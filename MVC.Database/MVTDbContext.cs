using Microsoft.AspNet.Identity.EntityFramework;
using MVT.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Database
{
  public class MVTDbContext : IdentityDbContext<MVTUsers>//:DbContext
    {
        public MVTDbContext() :base("MVTTContext")
        {
                
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Config> Configurations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Contact> contacts { get; set; }

        public static MVTDbContext Create()
        {
            return new MVTDbContext();
        }
    }

}
