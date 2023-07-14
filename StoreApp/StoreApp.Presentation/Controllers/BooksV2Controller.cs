using Microsoft.AspNetCore.Mvc;

using StoreApp.Entities.DTOs;
using StoreApp.Presentation.ActionFilters;
using StoreApp.Services.Abstract;

namespace StoreApp.Presentation.Controllers
{
    [ApiVersion("2.0",Deprecated = true)]
    [Route("api/books")]
    [ApiController]
    public class BooksV2Controller : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public BooksV2Controller(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet(Name = nameof(GetBooks))]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetBooks()
        {
            List<BookDto> books = await _serviceManager.BookService.GetAll();
            var booksV2 = books.Select(b => new
            {
                Title = b.Title,
                Id = b.Id
            });

            return Ok(booksV2);
        }
    }
}
