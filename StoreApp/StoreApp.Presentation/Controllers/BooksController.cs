
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Entities.DTOs;
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

            var foundProduct = _serviceManager.BookService.GetById(id);
            if (foundProduct is null) return NotFound();

            return Ok(foundProduct);
        }

        [HttpPost]
        public IActionResult InsertBook([FromBody] BookDtoForCreate dto)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var book = _serviceManager.BookService.Create(dto);
            return StatusCode(201,book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook([FromRoute] int id, [FromBody] BookDtoForUpdate dto)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _serviceManager.BookService.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook([FromRoute] int id)
        {
            _serviceManager.BookService.Delete(id);
            return Ok($"Book with {id} id has been deleted");
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
        [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            BookDtoForUpdate dto = _serviceManager.BookService.GetByIdForPatch(id);

            bookPatch.ApplyTo(dto);

            TryValidateModel(dto);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _serviceManager.BookService.Update(id, dto);

            return NoContent(); 
        }
    }
}
