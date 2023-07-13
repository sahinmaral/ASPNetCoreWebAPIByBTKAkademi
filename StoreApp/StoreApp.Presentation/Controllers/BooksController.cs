
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models;
using StoreApp.Entities.Models.RequestFeatures;
using StoreApp.Presentation.ActionFilters;
using StoreApp.Services.Abstract;

using System.Text.Json;

namespace StoreApp.WebAPI.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]
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
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [ServiceFilter(typeof(PriceOutOfRangeCheckAttribute))]
        public IActionResult GetBooks([FromQuery]BookParameters parameters)
        {
            var linkParameters = new LinkParameters()
            {
                BookParameters = parameters,
                HttpContext = HttpContext
            };

            var result = _serviceManager.BookService.GetAll(linkParameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks ? 
                Ok(result.linkResponse.LinkedEntities) : 
                Ok(result.linkResponse.ShapedEntities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {

            var foundProduct = await _serviceManager.BookService.GetByIdAsync(id);
            if (foundProduct is null) return NotFound();

            return Ok(foundProduct);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> InsertBook([FromBody] BookDtoForCreate dto)
        {

            var book = await _serviceManager.BookService.CreateAsync(dto);
            return StatusCode(201,book);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(NotFoundFilterAttribute<Book>))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookDtoForUpdate dto)
        {

            await _serviceManager.BookService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(NotFoundFilterAttribute<Book>))]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            await _serviceManager.BookService.DeleteAsync(id);
            return Ok($"Book with {id} id has been deleted");
        }

        [HttpPatch("{id:int}")]
        [ServiceFilter(typeof(NotFoundFilterAttribute<Book>))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
        [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            BookDtoForUpdate dto = await _serviceManager.BookService.GetByIdForPatchAsync(id);

            bookPatch.ApplyTo(dto);

            TryValidateModel(dto);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _serviceManager.BookService.UpdateAsync(id, dto);

            return NoContent(); 
        }
    }
}
