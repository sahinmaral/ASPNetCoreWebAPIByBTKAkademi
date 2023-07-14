using StoreApp.Entities.Models;
using StoreApp.Repositories.Abstract;

namespace StoreApp.Repositories.EFCore
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreAppDbContext context) : base(context)
        {
        }
    }
}
