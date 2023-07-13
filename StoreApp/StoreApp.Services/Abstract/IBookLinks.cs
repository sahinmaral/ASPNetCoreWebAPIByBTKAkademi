using Microsoft.AspNetCore.Http;

using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models.LinkModels;


namespace StoreApp.Services.Abstract
{
    public interface IBookLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<BookDto> bookDtos, string fields, HttpContext httpContext);
    }
}
