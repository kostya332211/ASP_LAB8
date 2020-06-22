using System;
using StandardPhonesBook.Core.Repositories;
using StandardPhonesBook.Infrastructure.EntityFramework.Repositories;

namespace StandardPhonesBook.Infrastructure.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {

        private PhonesBookContext _context;

        private IPersonRepository _personRepository;

        private PhonesBookContext Context => _context ?? (_context = new PhonesBookContext());

        public IPersonRepository PersonRepository =>_personRepository ?? (_personRepository = new PersonRepository(_context));

        public UnitOfWork()
        {
            _context = new PhonesBookContext();
        }

        private bool _isDisposed;
        public void Dispose()
        {
            if (Context == null)
            {
                return;
            }

            if (!_isDisposed)
            {
                Context.Dispose();
            }

            _isDisposed = true;
        }

        public void Commit()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("UnitOfWork");
            }

            Context.SaveChanges();
        }
    }
}
