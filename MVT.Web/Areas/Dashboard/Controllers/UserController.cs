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
    public class UserController : Controller
    {
        private MVTSignInManager _signInManager;
        private MVTUserManager _userManager;

        public UserController()
        {
        }

        public UserController(MVTUserManager userManager, MVTSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

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
        // GET: Dashboard/User
        public ActionResult Index()
        {
            UserListingViewModel model = new UserListingViewModel();
            model.Users = UserManager.Users;

            return View(model);
        }
    }
}