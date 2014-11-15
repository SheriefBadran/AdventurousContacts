using System;
using System.Collections.Generic;
namespace AdventurousContacts.Models.Repositories
{
    public interface IRepository
    {
        IEnumerable<Contact> GetContacts();
        void InsertContact(Contact contact);
        void Save();
    }
}
