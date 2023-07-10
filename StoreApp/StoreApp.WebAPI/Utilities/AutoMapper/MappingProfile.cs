using AutoMapper;

using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models;

namespace StoreApp.WebAPI.Utilities.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDtoForCreate, Book>();
            CreateMap<Book, BookDtoForUpdate>().ReverseMap();
        }
    }
}
