using AutoMapper;

using StoreApp.Repositories.Abstract;
using StoreApp.Services.Abstract;

namespace StoreApp.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;
        public IBookService BookService => _bookService.Value;
        public ServiceManager(
            IRepositoryManager repositoryManager,
            ILoggerService loggerService,
            IMapper mapper,
            IBookLinks bookLinks
            )
        {
            _bookService = new Lazy<IBookService>(() => new BookManager(repositoryManager, loggerService, mapper,bookLinks));
        }
    }
}
