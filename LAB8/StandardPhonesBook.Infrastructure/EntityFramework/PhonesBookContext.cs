using Microsoft.EntityFrameworkCore;
using StandardPhonesBook.Core.Entities;

namespace StandardPhonesBook.Infrastructure.EntityFramework
{
    class PhonesBookContext : DbContext
    {
        private const string Connection =
            "Data Source=(local);Initial Catalog=DBPhonesBook;Integrated Security=True";

        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Person>().ToTable("Person");
        }
    }
}
