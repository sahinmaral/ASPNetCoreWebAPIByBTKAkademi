using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models;
using StoreApp.Entities.Models.LinkModels;
using StoreApp.Entities.Models.RequestFeatures;

namespace StoreApp.Services.Abstract
{
    public interface IBookService : IServiceBase<Book>
    {
        BookDto? GetById(int id, bool trackChanges = false);
        Task<BookDto?> GetByIdAsync(int id, bool trackChanges = false);
        Task<BookDtoForUpdate> GetByIdForPatchAsync(int id, bool trackChanges = false);
        void Delete(int id);
        Task DeleteAsync(int id);
        (LinkResponse linkResponse, MetaData metaData) GetAll(LinkParameters linkParameters, bool trackChanges = false);
        BookDto Create(BookDtoForCreate dto);
        Task<BookDto> CreateAsync(BookDtoForCreate dto);
        void Update(int id, BookDtoForUpdate dto);
        Task UpdateAsync(int id, BookDtoForUpdate dto);
    }
}
