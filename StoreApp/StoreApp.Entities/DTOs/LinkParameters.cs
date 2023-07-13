using Microsoft.AspNetCore.Http;

using StoreApp.Entities.Models.RequestFeatures;

namespace StoreApp.Entities.DTOs
{
    public record LinkParameters
    {
        public BookParameters BookParameters { get; init; }
        public HttpContext HttpContext { get; init; }
    }
}
