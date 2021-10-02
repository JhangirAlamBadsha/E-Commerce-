using MVC.Database;
using MVT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVT.Services
{
    public class ContactServices
    {
        #region Singleton
        public static ContactServices Instance
        {
            get
            {
                if (instance == null) instance = new ContactServices();

                return instance;
            }
        }
        private static ContactServices instance { get; set; }
        private ContactServices()
        {
        }
        #endregion
        public void SaveContact(Contact contact)
        {
            using (var context = new MVTDbContext())
            {
                context.contacts.Add(contact);
                context.SaveChanges();
            }
        }
        public int TotalContactsCount()
        {
            using (var con = new MVTDbContext())
            {
                var contacts = con.contacts.ToList();
                return contacts.Count;

            }
        }
        public List<Contact> GetAllContacts()
        {
            using (var context = new MVTDbContext())
            {
                return context.contacts
                        .ToList();
            }
        }
    }
   
}
