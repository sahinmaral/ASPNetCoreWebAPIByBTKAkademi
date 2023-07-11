using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models;

using System.Linq.Expressions;

namespace StoreApp.Services.Abstract
{
    public interface IBookService : IServiceBase<Book>
    {
        //void UpdateForPatch(BookDtoForUpdate bookDtoForUpdate, Book book);
        BookDto? GetById(int id, bool trackChanges = false);
        Task<BookDto?> GetByIdAsync(int id, bool trackChanges = false);
        Task<BookDtoForUpdate> GetByIdForPatchAsync(int id, bool trackChanges = false);
        void Delete(int id);
        Task DeleteAsync(int id);
        IQueryable<BookDto> GetAllByCondition(Expression<Func<Book, bool>> expression, bool trackChanges = false);
        IEnumerable<BookDto> GetAll(bool trackChanges = false);
        BookDto Create(BookDtoForCreate dto);
        Task<BookDto> CreateAsync(BookDtoForCreate dto);
        void Update(int id, BookDtoForUpdate dto);
        Task UpdateAsync(int id, BookDtoForUpdate dto);
    }
}
