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
            CreateMap<Book, BookWithDetailsDto>()
                .ForMember(b => b.CategoryName, bwd => bwd.MapFrom(map => map.Category.Name));
            CreateMap<BookDtoForCreate, Book>();
            CreateMap<Book, BookDtoForUpdate>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>();

            CreateMap<Category, CategoryDto>();
        }
    }
}
