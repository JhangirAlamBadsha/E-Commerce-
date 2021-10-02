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
    public class ContactsController : Controller
    {
        // GET: Contacts
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
             
            return View();
        }
        [HttpPost]
        public ActionResult Create(ContactViewModel model)
        {
            Contact  contact = new Contact();
            contact.ID = model.ID;
            contact.Name = model.Name;
            contact.Gmail = model.Gmail;
            contact.Comment = model.Comment;


            ContactServices.Instance.SaveContact(contact);
            return RedirectToAction("Index","Home");
        }

    }
}