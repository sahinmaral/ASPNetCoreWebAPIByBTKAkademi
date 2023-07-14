using StoreApp.Entities.Models;
using StoreApp.Repositories.Abstract;

namespace StoreApp.Repositories.EFCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(StoreAppDbContext context) : base(context)
        {
        }
    }
}
