using StoreApp.Repositories.Abstract;

namespace StoreApp.Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly StoreAppDbContext _context;

        public RepositoryManager(StoreAppDbContext context,IBookRepository bookRepository,ICategoryRepository categoryRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public IBookRepository BookRepository => _bookRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
