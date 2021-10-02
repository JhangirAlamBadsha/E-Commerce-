using Microsoft.AspNet.Identity.Owin;
using MVT.Services;
using MVT.Web.Areas.Dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVT.Web.Areas.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard/Dashboard
        private MVTSignInManager _signInManager;
        private MVTUserManager _userManager;

        public MVTSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<MVTSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public MVTUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<MVTUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index(string search)
        {
            DashboardViewModel model = new DashboardViewModel();
            model.TotalCategories = CategoriesService.Instance.GetCategoriesCount(search);
            model.TotalOrders = OrdersService.Instance.TotalOrdersCount();
            model.TotalProducts = ProductService.Instance.TotalProductCount(); ;
            model.TotalUsers= UserManager.Users.Count();
            model.TotalContacts = ContactServices.Instance.TotalContactsCount();
            model.TotalRoles = 3;
            return View(model);
        }
    }
}