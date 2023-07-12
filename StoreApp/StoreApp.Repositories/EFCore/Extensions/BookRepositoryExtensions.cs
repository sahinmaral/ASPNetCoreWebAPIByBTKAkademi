using StoreApp.Entities.Models;

using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

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

        /// <summary>
        /// Extension method for order books dynamically
        /// </summary>
        /// <param name="orderByQueryString">Query string to order books dynamically. Example (title,price desc,id asc)</param>
        /// <returns></returns>
        public static IOrderedQueryable<Book> Sort(this IQueryable<Book> books,string? orderByQueryString)
        {
            if (string.IsNullOrEmpty(orderByQueryString))
                return books.OrderBy(b => b.Id);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Book>(orderByQueryString);

            if (orderQuery is null)
                return books.OrderBy(b => b.Id);

            return books.OrderBy(orderQuery);
        }
    }
}
