using System;
using StandardPhonesBook.Core.Entities;

namespace StandardPhonesBook.Core.Repositories
{
    public interface IPersonRepository : IRepository<Person, Guid>
    {

    }
}
