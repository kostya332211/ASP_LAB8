using System;
using System.Collections.Generic;
using System.Linq;
using StandardPhonesBook.Core.Entities;
using StandardPhonesBook.Core.Repositories;

namespace StandardPhonesBook.Infrastructure.EntityFramework.Repositories
{
    class PersonRepository : IPersonRepository
    {
        private readonly PhonesBookContext _context;

        public PersonRepository(PhonesBookContext context)
        {
            _context = context;
        }

        public Person Get(Predicate<Person> predicate)
        {
            return _context.Persons.ToList().Find(predicate);
        }

        public IEnumerable<Person> All()
        {
            return _context.Persons;
        }

        public void Insert(Person entity)
        { 
            _context.Persons.Add(entity);
        }

        public void Delete(Predicate<Person> predicate)
        {
            var person = Get(predicate);
            if (person != null)
            {
                _context.Persons.Remove(Get(predicate));
            }
        }

    }
}
