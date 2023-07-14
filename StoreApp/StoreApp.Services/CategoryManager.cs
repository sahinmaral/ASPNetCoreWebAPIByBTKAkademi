using AutoMapper;

using Microsoft.EntityFrameworkCore;

using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models;
using StoreApp.Repositories.Abstract;
using StoreApp.Services.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CategoryManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<bool> AnyAsync(Expression<Func<Category, bool>> expression,bool trackChanges)
        {
            return await _repositoryManager.CategoryRepository.AnyAsync(expression,trackChanges);
        }

        public async Task<List<CategoryDto>> GetAll(bool trackChanges = false)
        {
            return _mapper.Map<List<CategoryDto>>(await _repositoryManager.CategoryRepository.GetAll(trackChanges).ToListAsync());
        }

        public async Task<CategoryDto?> GetById(int id, bool trackChanges = false)
        {
            return _mapper.Map<CategoryDto>(await _repositoryManager.CategoryRepository.GetByIdAsync(id, trackChanges));
        }
    }
}
