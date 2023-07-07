using StoreApp.Entities.Models.Abstract;

using System.Linq.Expressions;

namespace StoreApp.Services.Abstract
{
    public interface IServiceBase<T> where T: class, IEntity
    {
        IEnumerable<T> GetAll(bool trackChanges = false);
        IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
        T? GetById(int id, bool trackChanges = false);
        void Create(T entity);
        void Update(int id, T entity);
        void Delete(int id);
    }
}
