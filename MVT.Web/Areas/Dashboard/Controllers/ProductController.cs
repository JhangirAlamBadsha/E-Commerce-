using MVT.Entities;
using MVT.Services;
using MVT.Web.Areas.Dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVT.Web.Areas.Dashboard.Controllers
{
    public class ProductController : Controller
    {
        // GET: Dashboard/Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search, int? pageNo)
        {


            ProductSearchViewModel model = new ProductSearchViewModel();
            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.Products = ProductService.Instance.searchProducts(model.PageNo);
            if (string.IsNullOrEmpty(search) == false)
            {
                model.Products = model.Products.Where(p => p.Name != null && p.Name.Contains(search)).ToList();
            }

            return PartialView(model);
        }
        //public ActionResult ProductTable(string search)
        //{

        //    var products = ProductService.Instance.getProducts().ToList();
        //    if (string.IsNullOrEmpty(search) == false)
        //    {
        //        products = products.Where(p => p.Name != null && p.Name.Contains(search)).ToList();
        //    }

        //    return PartialView(products);
        //}
        [HttpGet]
        public ActionResult Create()
        {
            //CategoriesService categoriesService = new CategoriesService();
            var categories = CategoriesService.Instance.GetCategories();
            return PartialView(categories);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            // CategoriesService categoriesService = new CategoriesService();
            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            newProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryID);
            newProduct.ImageURL = model.ImageURL;

            ProductService.Instance.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }
        //[HttpPost]
        //public ActionResult Create(NewCategoryViewModel model)
        //{
        //    CategoriesService categoriesService = new CategoriesService();
        //    var newProduct = new Product();
        //    newProduct.Name = model.Name;
        //    newProduct.Description = model.Description;
        //    newProduct.Price = model.Price;
        //    newProduct.Category = categoriesService.GetCategory(model.CategoryID);
        //    ProductService.Instance.SaveProduct(newProduct);
        //    return RedirectToAction("ProductTable");
        // }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();
            //CategoriesService categoriesService = new CategoriesService();
            var product = ProductService.Instance.getProducts(ID);

            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            //model.CategoryID = product.Category != null ? product.Category.Id : 1;
            model.ImageURL = product.ImageURL;

            model.AvailableCategories = CategoriesService.Instance.GetAllCategories();

            return PartialView(model);
        }
        //public ActionResult  Edit(int Id)
        //{
        //    var product = ProductService.Instance.getProducts(Id);
        //    return PartialView(product);
        //}
        [HttpPost]

        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = ProductService.Instance.getProducts(model.ID);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;

            existingProduct.Category = null; //mark it null. Because the referncy key is changed below
            existingProduct.CategoryID = model.CategoryID;

            //dont update imageURL if its empty
            if (!string.IsNullOrEmpty(model.ImageURL))
            {
                existingProduct.ImageURL = model.ImageURL;
            }

            ProductService.Instance.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }
        //public ActionResult Edit(Product product)
        //{
        //    ProductService.Instance.UpdateProduct(product);
        //    return RedirectToAction("ProductTable");
        //}
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            ProductService.Instance.DeleteProduct(Id);
            return RedirectToAction("ProductTable");
        }
        public ActionResult Details(int ID)
        {
            DetailProductViewModel model = new DetailProductViewModel();
            model.Product = ProductService.Instance.GetProduct(ID);
            return View(model);

        }
    }
}