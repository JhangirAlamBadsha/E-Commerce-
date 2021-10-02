using MVT.Services;
using MVT.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVT.Web.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Details(int ID)
        {
            DetailProductViewModel model = new DetailProductViewModel();
            model.Product = ProductService.Instance.GetProduct(ID);
            return View(model);

        }

    }
}