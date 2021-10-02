using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVT.Entities;
using MVT.Services;
using MVT.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVT.Web.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
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
        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            ShopViewModel model = new ShopViewModel();
            var pageSize = ConfigurationsService.Instance.ShopPageSize();
            model.SearchTerm = searchTerm;
            model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();
            model.MaximumPrice = ProductService.Instance.GetmaximumPrice();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.Products = ProductService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);
            model.SortBy = sortBy;
            model.categoryID = categoryID;
            int totalCount = ProductService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Pager = new Pager(totalCount, pageNo, pageSize);
            return View(model);

        }
        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            FilterProductsViewModel model = new FilterProductsViewModel();
            var pageSize = ConfigurationsService.Instance.ShopPageSize();
            model.SearchTerm = searchTerm;
            model.SortBy = sortBy;
            model.categoryID = categoryID;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.Products = ProductService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);
            int totalCount = ProductService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Pager = new Pager(totalCount, pageNo, pageSize);
            return PartialView(model);

        }

        //ProductService productService = new ProductService();
        [Authorize]
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();
            var CartProductsCookie = Request.Cookies["CartProducts"];
            if (CartProductsCookie != null && !string.IsNullOrEmpty(CartProductsCookie.Value))
            {
                model.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = ProductService.Instance.getProducts(model.CartProductIDs);
                model.User = UserManager.FindById(User.Identity.GetUserId());

            }
            return View(model);
        }
        public JsonResult PlaceOrder(string productIDs)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!string.IsNullOrEmpty(productIDs))
            {
                var productQuantities = productIDs.Split('-').Select(x => int.Parse(x)).ToList();

                var boughtProducts = ProductService.Instance.getProducts(productQuantities.Distinct().ToList());
                Order newOrder = new Order();
                newOrder.UserID = User.Identity.GetUserId();
                newOrder.OrderedAt = DateTime.Now;
                newOrder.Status = "Pending";
                newOrder.TotalAmount = boughtProducts.Sum(x => x.Price * productQuantities.Where(productID => productID == x.ID).Count());

                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { ProductID = x.ID, Quantity = productQuantities.Where(productID => productID == x.ID).Count() }));

                var rowsEffected = ShopService.Instance.SaveOrder(newOrder);

                result.Data = new { Success = true, Rows = rowsEffected };
            }
            else
            {
                result.Data = new { Success = false };
            }

            return result;
        }
    }
}