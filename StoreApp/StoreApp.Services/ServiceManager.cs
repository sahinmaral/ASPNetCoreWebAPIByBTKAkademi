using StoreApp.Services.Abstract;

namespace StoreApp.Services
{
    public class ServiceManager : IServiceManager
    {
        public readonly IBookService _bookService;
        public readonly ICategoryService _categoryService;
        public readonly IAuthenticationService _authenticationService;

        public ServiceManager(IBookService bookService, ICategoryService categoryService, IAuthenticationService authenticationService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _authenticationService = authenticationService;
        }

        public IBookService BookService => _bookService;
        public ICategoryService CategoryService => _categoryService;
        public IAuthenticationService AuthenticationService => _authenticationService;
    }
}
