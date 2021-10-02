using MVT.Services;
using MVT.Web.Areas.Dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVT.Web.Areas.Dashboard.Controllers
{
    public class ContactController : Controller
    {
        // GET: Dashboard/Contact
        public ActionResult Index()
        {
            ContactViewModel model = new ContactViewModel();
             model.Contacts = ContactServices.Instance.GetAllContacts();
            return View(model);
        }
    }
}