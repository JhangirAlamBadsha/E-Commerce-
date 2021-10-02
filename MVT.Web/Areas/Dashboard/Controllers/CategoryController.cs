using MVT.Entities;
using MVT.Services;
using MVT.Web.Areas.Dashboard.ViewModels;
using MVT.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVT.Web.Areas.Dashboard.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Dashboard/Category
        public ActionResult Index()
        {
            return View();
        }


        //   [HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}
        //   public ActionResult ProductTable()
        //   {
        //       var categorires = categoriesServices.GetCategories();
        //       return PartialView(categorires);
        //   }

        //   [HttpPost]
        //   public ActionResult Create(Category Category)
        //   {
        //       categoriesServices.SaveCategory(Category);
        //       return RedirectToAction("Index");
        //   }

        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();
            return PartialView(model);
        }


        public ActionResult CategoryTable(string search, int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();
            model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = CategoriesService.Instance.GetCategoriesCount(search);
            model.Categories = CategoriesService.Instance.GetCategories(search, pageNo.Value);

            if (model.Categories != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, 3);

                return PartialView("_CategoryTable", model);
            }
            else
            {
                return HttpNotFound();
            }
        }



        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            var newCategory = new Category();
            newCategory.Name = model.Name;
            newCategory.Description = model.Description;
            newCategory.ImageURL = model.ImageURL;
            newCategory.isFeatured = model.isFeatured;

            CategoriesService.Instance.SaveCategory(newCategory);
            return RedirectToAction("CategoryTable");
        }


        [HttpGet]
        public ActionResult Edit(int ID)
        {
            //    var category = CategoriesService.Instance.GetCategory(Id);
            //    return View(category);
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = CategoriesService.Instance.GetCategory(ID);

            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.isFeatured = category.isFeatured;

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            //CategoriesService.Instance.UpdateCategory(category);
            //return RedirectToAction("Index");
            var existingCategory = CategoriesService.Instance.GetCategory(model.ID);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageURL = model.ImageURL;
            existingCategory.isFeatured = model.isFeatured;

            CategoriesService.Instance.UpdateCategory(existingCategory);
            return RedirectToAction("CategoryTable");
        }
        // [HttpGet]
        //public ActionResult Delete(int Id)
        //{
        //    var category = CategoriesService.Instance.GetCategory(Id);
        //    return View(category);

        //}
        [HttpPost]
        public ActionResult Delete(int Id)
        {

            CategoriesService.Instance.DeleteCategory(Id);
            return RedirectToAction("CategoryTable");
        }

    }
}