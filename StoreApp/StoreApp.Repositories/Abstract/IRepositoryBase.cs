using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models.Abstract;

using System.Linq.Expressions;

namespace StoreApp.Repositories.Abstract
{
    public interface IRepositoryBase<T> where T: class,IEntity
    {
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll(bool trackChanges);
        IQueryable<T> GetAllByCondition(Expression<Func<T,bool>> expression, bool trackChanges);
        T? GetById(int id,bool trackChanges);
        Task<T?> GetByIdAsync(int id, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
