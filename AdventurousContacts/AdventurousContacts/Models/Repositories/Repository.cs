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

        public void Add(Contact contact) 
        {
            //_entities.Contacts.Add(contact);
            _entities.uspAddContact_SELECT(contact.FirstName, contact.LastName, contact.EmailAddress);
        }

        public void Delete(Contact contact)
        {
            _entities.uspRemoveContact(contact.ContactID);
        }

        public Contact GetContactById(int contactId)
        {
            return _entities.Contacts.Find(contactId);
        }

        public void Save()
        {
            _entities.SaveChanges();
        }

        #region IDisposable

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _entities.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}