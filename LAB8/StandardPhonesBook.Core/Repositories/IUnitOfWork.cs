using System;

namespace StandardPhonesBook.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository PersonRepository { get; }
        void Commit();
    }
}
