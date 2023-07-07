using Microsoft.EntityFrameworkCore;

using StoreApp.Entities.Models.Abstract;
using StoreApp.Repositories.Abstract;

namespace StoreApp.Repositories.EFCore
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T: class,IEntity
    {
        protected readonly StoreAppDbContext _context;

        public RepositoryBase(StoreAppDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll(bool trackChanges = false)
        {
            return trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetAllByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool trackChanges = false)
        {
            return trackChanges ? _context.Set<T>().Where(expression) : _context.Set<T>().Where(expression).AsNoTracking();
        }

        public T? GetById(int id, bool trackChanges = false)
        {
            return trackChanges ? _context.Set<T>().SingleOrDefault(x => x.Id == id) : _context.Set<T>().AsNoTracking().SingleOrDefault(x => x.Id == id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
