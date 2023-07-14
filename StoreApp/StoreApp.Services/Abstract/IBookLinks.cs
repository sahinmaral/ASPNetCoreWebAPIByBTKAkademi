using Microsoft.AspNetCore.Http;

using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models.LinkModels;


namespace StoreApp.Services.Abstract
{
    public interface IBookLinks<T> where T : BookDto
    {
        LinkResponse TryGenerateLinks(IEnumerable<T> dtos, string fields, HttpContext httpContext);
        
    }
}
