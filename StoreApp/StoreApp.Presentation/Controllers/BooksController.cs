using Microsoft.AspNetCore.Mvc;

using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models;
using StoreApp.Entities.Models.Abstract;
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
            _serviceManager.BookService.Create(dto);
            return Ok($"Book with {dto.Title} has been added");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook([FromRoute] int id, [FromBody] BookDtoForUpdate dto)
        {
            _serviceManager.BookService.Update(id, dto);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook([FromRoute] int id)
        {
            _serviceManager.BookService.Delete(id);
            return Ok($"Book with {id} id has been deleted");

        }
    }
}
