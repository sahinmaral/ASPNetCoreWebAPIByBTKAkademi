using StoreApp.Entities.Models.Abstract;

namespace StoreApp.Entities.DTOs
{
    public record BookDtoForCreate(string Title,decimal Price) : IDto;
}
