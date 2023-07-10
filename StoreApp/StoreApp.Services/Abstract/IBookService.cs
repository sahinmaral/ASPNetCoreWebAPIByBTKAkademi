using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models;

using System.Linq.Expressions;

namespace StoreApp.Services.Abstract
{
    public interface IBookService
    {
        //void UpdateForPatch(BookDtoForUpdate bookDtoForUpdate, Book book);
        BookDto? GetById(int id, bool trackChanges = false);
        BookDtoForUpdate GetByIdForPatch(int id, bool trackChanges = false);
        void Delete(int id);
        IQueryable<BookDto> GetAllByCondition(Expression<Func<Book, bool>> expression, bool trackChanges = false);
        IEnumerable<BookDto> GetAll(bool trackChanges = false);
        BookDto Create(BookDtoForCreate dto);
        void Update(int id, BookDtoForUpdate dto);
    }
}
