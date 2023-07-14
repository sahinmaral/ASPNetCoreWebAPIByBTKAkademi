
using Marvin.Cache.Headers;

using Microsoft.AspNetCore.Authorization;
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
    [ApiVersion("1.0")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/books")]
    [ApiController]
    //[ResponseCache(CacheProfileName = "5MinsCacheProfile")]
    //[HttpCacheExpiration(MaxAge = 80)]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public BooksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [Authorize]
        [HttpHead]
        [HttpGet(Name = nameof(GetBooks))]
        //[ResponseCache(Duration = 60)]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [ServiceFilter(typeof(PriceOutOfRangeCheckAttribute))]
        public IActionResult GetBooks([FromQuery] BookParameters parameters)
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

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {

            var foundProduct = await _serviceManager.BookService.GetByIdAsync(id);
            if (foundProduct is null) return NotFound();

            return Ok(foundProduct);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpPost(Name = nameof(InsertBook))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> InsertBook([FromBody] BookDtoForCreate dto)
        {

            var book = await _serviceManager.BookService.CreateAsync(dto);
            return StatusCode(201, book);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpPut("{id}")]
        [ServiceFilter(typeof(NotFoundFilterAttribute<Book>))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookDtoForUpdate dto)
        {

            await _serviceManager.BookService.UpdateAsync(id, dto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(NotFoundFilterAttribute<Book>))]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            await _serviceManager.BookService.DeleteAsync(id);
            return Ok($"Book with {id} id has been deleted");
        }

        [Authorize(Roles = "Admin, Editor")]
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

        [Authorize]
        [HttpOptions]
        public IActionResult GetBooksOptions()
        {
            Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS, DELETE, PATCH, HEAD" });
            Response.Headers.Add("Access-Control-Allow-Origin", new[] { (string)Request.Headers["Origin"] });
            Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept, X-Pagination" });
            return Ok();
        }
    }
}
