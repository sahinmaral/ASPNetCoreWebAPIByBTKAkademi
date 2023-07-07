using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StoreApp.Entities.Models;

namespace StoreApp.Repositories.EFCore.Config
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book() { Id = 1, Title = "Karagoz ve Hacivat", Price = 75 },
                new Book() { Id = 2, Title = "Mesnevi", Price = 150 },
                new Book() { Id = 3, Title = "Dede Korkut", Price = 75 }
                );
        }
    }
}
