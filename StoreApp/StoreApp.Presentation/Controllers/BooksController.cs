using Microsoft.AspNetCore.Mvc;

using StoreApp.Entities.Models;
using StoreApp.Services.Abstract;

namespace StoreApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BooksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(_serviceManager.BookService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            try
            {
                var foundProduct = _serviceManager.BookService.GetById(id);
                return Ok(foundProduct);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        [HttpPost]
        public IActionResult InsertBook([FromBody] Book book)
        {
            try
            {
                _serviceManager.BookService.Create(book);
                return Ok($"Book with {book.Title} has been added");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook([FromRoute] int id, [FromBody] Book book)
        {
            try
            {
                _serviceManager.BookService.Update(id, book);
                return Ok(book);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook([FromRoute] int id)
        {
            try
            {
                _serviceManager.BookService.Delete(id);
                return Ok($"Book with {id} id has been deleted");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
