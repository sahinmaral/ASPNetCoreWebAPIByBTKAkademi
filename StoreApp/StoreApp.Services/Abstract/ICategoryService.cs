using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models;

namespace StoreApp.Services.Abstract
{
    public interface ICategoryService : IServiceBase<Category>
    {
        Task<List<CategoryDto>> GetAll(bool trackChanges = false);
        Task<CategoryDto?> GetById(int id, bool trackChanges = false);
    }
}
