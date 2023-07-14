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
                new Book() { Id = 286, Title = "Karagoz ve Hacivat", Price = 75,CategoryId = 1 },
                new Book() { Id = 287, Title = "Mesnevi", Price = 150, CategoryId = 2 },
                new Book() { Id = 288, Title = "Dede Korkut", Price = 75, CategoryId = 1 }
                );
        }
    }
}
