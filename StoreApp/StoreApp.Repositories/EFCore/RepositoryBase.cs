using Microsoft.EntityFrameworkCore;

using StoreApp.Entities.Models.Abstract;
using StoreApp.Repositories.Abstract;

using System.Linq.Expressions;

namespace StoreApp.Repositories.EFCore
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T: class,IEntity
    {
        protected readonly StoreAppDbContext _context;

        public RepositoryBase(StoreAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression,bool trackChanges)
        {
            return trackChanges ? await _context.Set<T>().AnyAsync(expression) : await _context.Set<T>().AsNoTracking().AnyAsync(expression);
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll(bool trackChanges)
        {
            return trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges ? _context.Set<T>().Where(expression) : _context.Set<T>().Where(expression).AsNoTracking();
        }

        public T? GetById(int id, bool trackChanges)
        {
            return trackChanges ? _context.Set<T>().SingleOrDefault(x => x.Id == id) : _context.Set<T>().AsNoTracking().SingleOrDefault(x => x.Id == id);
        }

        public async Task<T?> GetByIdAsync(int id, bool trackChanges)
        {
            return trackChanges ? await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id) : await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
