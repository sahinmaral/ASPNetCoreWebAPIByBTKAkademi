using System.Linq.Expressions;

namespace StoreApp.Services.Abstract
{
    public interface IServiceBase<T>
    {
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression,bool trackChanges = false);
    }
}
