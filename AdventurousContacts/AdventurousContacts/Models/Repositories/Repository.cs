using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventurousContacts.Models.Repositories
{
    public class Repository : IRepository
    {
        Entities _entities = new Entities();

        public IEnumerable<Contact> GetContacts()
        {
            return _entities.Contacts.ToList();
        }

        public void InsertContact(Contact contact) 
        {
            _entities.Contacts.Add(contact);
        }

        public void Save()
        {
            _entities.SaveChanges();
        }
    }
}