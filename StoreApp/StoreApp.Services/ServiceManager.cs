using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using StoreApp.Entities.Models;
using StoreApp.Repositories.Abstract;
using StoreApp.Services.Abstract;

namespace StoreApp.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;
        public readonly Lazy<IAuthenticationService> _authenticationService;
        public IBookService BookService => _bookService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public ServiceManager(
            IRepositoryManager repositoryManager,
            ILoggerService loggerService,
            IMapper mapper,
            IBookLinks bookLinks,
            UserManager<User> userManager,
            IConfiguration configuration
            )
        {
            _bookService = new Lazy<IBookService>(() =>
            new BookManager(repositoryManager, loggerService, mapper, bookLinks));

            _authenticationService = new Lazy<IAuthenticationService>(() =>
            new AuthenticationManager(loggerService, mapper, configuration, userManager));
        }
    }
}
