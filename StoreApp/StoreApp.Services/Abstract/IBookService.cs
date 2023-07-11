using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models;
using StoreApp.Entities.Models.RequestFeatures;

using System.Linq.Expressions;

namespace StoreApp.Services.Abstract
{
    public interface IBookService : IServiceBase<Book>
    {
        BookDto? GetById(int id, bool trackChanges = false);
        Task<BookDto?> GetByIdAsync(int id, bool trackChanges = false);
        Task<BookDtoForUpdate> GetByIdForPatchAsync(int id, bool trackChanges = false);
        void Delete(int id);
        Task DeleteAsync(int id);
        Task<(IEnumerable<BookDto> books, MetaData metaData)> GetAllByConditionAsync(BookParameters bookParameters,Expression<Func<Book, bool>> expression, bool trackChanges = false);
        Task<(IEnumerable<BookDto> books, MetaData metaData)> GetAllAsync(BookParameters bookParameters, bool trackChanges = false);
        BookDto Create(BookDtoForCreate dto);
        Task<BookDto> CreateAsync(BookDtoForCreate dto);
        void Update(int id, BookDtoForUpdate dto);
        Task UpdateAsync(int id, BookDtoForUpdate dto);
    }
}
