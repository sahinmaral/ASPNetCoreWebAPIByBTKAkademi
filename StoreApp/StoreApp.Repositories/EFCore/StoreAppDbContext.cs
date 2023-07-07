

using Microsoft.EntityFrameworkCore;

using StoreApp.Entities.Models;
using StoreApp.Repositories.EFCore.Config;

namespace StoreApp.Repositories.EFCore
{
    public class StoreAppDbContext : DbContext
    {
        public StoreAppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookConfiguration).Assembly);
        }

        public DbSet<Book> Books { get; set; }
    }
}
