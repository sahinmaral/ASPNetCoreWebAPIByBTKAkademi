using StoreApp.Repositories.Abstract;

namespace StoreApp.Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly StoreAppDbContext _context;
        private readonly Lazy<BookRepository> _bookRepository;

        public RepositoryManager(StoreAppDbContext context)
        {
            _context = context;
            _bookRepository = new Lazy<BookRepository>(() => new BookRepository(_context));
        }

        public IBookRepository BookRepository => _bookRepository.Value;

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
