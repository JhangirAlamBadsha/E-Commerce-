using MVC.Database;
using MVT.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVT.Services
{
     
    public class ProductService
    {
        public static ProductService Instance
        {
            get
            {

                if (instance == null) instance = new ProductService();
                return instance;
            }
        }
        private static ProductService instance { get; set; }
        private ProductService()
        {

        }
        public Product getProducts(int Id)
        {
            using (var con = new  MVTDbContext())
            {
                return con.Products.Find(Id);

            }
        }
        public Product GetProduct(int ID)
        {
            using (var context = new  MVTDbContext())
            {
                return context.Products.Where(x => x.ID == ID).Include(x => x.Category).FirstOrDefault();
            }
        }

        public List<Product> getProducts(List<int> IDs)
        {
            using (var con = new  MVTDbContext())
            {
                return con.Products.Where(product => IDs.Contains(product.ID)).ToList();

            }
        }
        public List<Product> searchProducts(int pageNo)
        {
            int pageSize = 3;
            using (var con = new  MVTDbContext())
            {

                return con.Products.OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();

            }
        }
        public List<Product> getProducts(int pageNo, int pageSize)
        {

            using (var con = new  MVTDbContext())
            {

                return con.Products.OrderByDescending(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();

            }
        }
        public List<Product> getLatestProducts(int numberOfProduct)
        {

            using (var con = new  MVTDbContext())
            {

                return con.Products.OrderByDescending(x => x.ID).Take(numberOfProduct).Include(x => x.Category).ToList();

            }
        }
        public List<Product> GetProductsByCategory(int categoryID, int pageSize)
        {
            using (var context = new  MVTDbContext())
            {
                return context.Products.Where(x => x.Category.ID == categoryID).OrderByDescending(x => x.ID).Take(pageSize).Include(x => x.Category).ToList();
            }
        }

        public List<Product> getProducts()
        {

            using (var con = new  MVTDbContext())
            {

                return con.Products.Include(x => x.Category).ToList();

            }
        }


        public void SaveProduct(Product product)
        {
            using (var con = new  MVTDbContext())
            {
                con.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;
                con.Products.Add(product);
                con.SaveChanges();
            }
        }
        public void UpdateProduct(Product product)
        {
            using (var con = new  MVTDbContext())
            {
                con.Entry(product).State = System.Data.Entity.EntityState.Modified;

                con.SaveChanges();
            }
        }
        public void DeleteProduct(int ID)
        {
            using (var con = new  MVTDbContext())
            {
                var product = con.Products.Find(ID);
                con.Products.Remove(product);
                con.SaveChanges();
            }
        }

        public int GetmaximumPrice()
        {
            using (var con = new  MVTDbContext())
            {

                return (int)(con.Products.Max(x => x.Price));

            }
        }

        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int pageNo, int pageSize)
        {
            using (var con = new  MVTDbContext())
            {

                var products = con.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.CategoryID == categoryID.Value).ToList();
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }
                return products.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            }
        }
        public int TotalProductCount()
        {
            using (var con = new MVTDbContext())
            {
                var products = con.Products.ToList();
                return products.Count;   

            }
        }
        public int SearchProductsCount(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
        {
            using (var con = new  MVTDbContext())
            {

                var products = con.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.CategoryID == categoryID.Value).ToList();
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }
                return products.Count;

            }
        }

    }
}
