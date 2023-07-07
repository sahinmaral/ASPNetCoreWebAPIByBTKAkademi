using StoreApp.Entities.Models;
using StoreApp.Repositories.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Repositories.EFCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(StoreAppDbContext context) : base(context)
        {
        }
    }
}
