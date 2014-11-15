using System;
using System.Collections.Generic;
namespace AdventurousContacts.Models.Repositories
{
    public interface IRepository: IDisposable
    {
        IEnumerable<Contact> GetContacts();

        void Add(Contact contact);

        Contact GetContactById(int id);

        void Delete(Contact contact);

        void Save();
    }
}
