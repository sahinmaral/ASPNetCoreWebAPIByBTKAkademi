using StoreApp.Entities.Models;

namespace StoreApp.Repositories.EFCore.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> FilterBooksByPrice(this IQueryable<Book> books,uint minPrice,uint maxPrice)
        {
            return books.Where(b => (b.Price >= minPrice) && (b.Price <= maxPrice));
        }

        public static IQueryable<Book> SearchByTitle(this IQueryable<Book> books,string? searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return books;

            return books.Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()));
        }
    }
}
