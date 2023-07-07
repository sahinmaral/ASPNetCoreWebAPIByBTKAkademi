using BookDemoWebAPI.Data;
using BookDemoWebAPI.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookDemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(InMemoryContext.Books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            var foundProduct = InMemoryContext.Books.SingleOrDefault(x => x.Id == id);
            if (foundProduct is null) return NotFound($"Book with {id} ID could not found");
            return Ok(foundProduct);
        }

        [HttpPost]
        public IActionResult InsertBook([FromBody] Book book)
        {
            if (book.Title.Length == 0) return BadRequest("Title of new book must be filled");

            Book existedBook = InMemoryContext.Books.SingleOrDefault(x => x.Title.Equals(book.Title));
            if (existedBook is not null) return BadRequest($"Book with {book.Title} title has already added");

            InMemoryContext.Books.Add(book);
            return Ok($"Book with {book.Title} has been added");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook([FromRoute]int id,[FromBody]Book book)
        {
            var foundProduct = InMemoryContext.Books.SingleOrDefault(x => x.Id == id);
            if (foundProduct is null) return NotFound($"Book with {id} ID could not found");

            foundProduct.Title = book.Title;
            foundProduct.Price = book.Price;
            return Ok(foundProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook([FromRoute] int id)
        {
            var foundProduct = InMemoryContext.Books.SingleOrDefault(x => x.Id == id);
            if (foundProduct is null) return NotFound($"Book with {id} ID could not found");

            InMemoryContext.Books.Remove(foundProduct);
            return Ok($"Book with {foundProduct.Title} has been deleted");
        }

    }
}
