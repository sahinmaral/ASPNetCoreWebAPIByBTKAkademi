using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models;

namespace StoreApp.Services.Abstract
{
    public interface IBookService : IServiceBase<Book>
    {
        void Create(BookDtoForCreate dto);
        void Update(int id, BookDtoForUpdate dto);
    }
}
