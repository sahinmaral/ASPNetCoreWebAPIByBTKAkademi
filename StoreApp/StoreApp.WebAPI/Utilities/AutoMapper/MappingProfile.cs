using AutoMapper;

using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models;

namespace StoreApp.WebAPI.Utilities.Mapping
{
    public class MappingProfile : Profile
    {
        protected MappingProfile()
        {
            CreateMap<BookDtoForCreate, Book>();
            CreateMap<Book, BookDtoForUpdate>().ReverseMap();
        }
    }
}
