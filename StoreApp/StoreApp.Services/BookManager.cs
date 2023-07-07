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
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;

        public BookManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public void Create(Book entity)
        {
            if (entity is null)
                throw new NullReferenceException(nameof(entity));

            Book? existedBook = _repositoryManager.BookRepository.GetAllByCondition(x => x.Title.Equals(entity.Title),false).SingleOrDefault();
            if (existedBook is not null) throw new InvalidOperationException($"Book with {entity.Title} title has already added");

            _repositoryManager.BookRepository.Create(entity);
            _repositoryManager.Save();
        }

        public void Delete(int id)
        {
            var foundEntity = _repositoryManager.BookRepository.GetById(id,false);
            if (foundEntity is null)
                throw new NullReferenceException($"Book with {id} id could not found");

            _repositoryManager.BookRepository.Delete(foundEntity);
            _repositoryManager.Save();
        }

        public IEnumerable<Book> GetAll(bool trackChanges)
        {
            return _repositoryManager.BookRepository.GetAll(trackChanges);
        }

        public IQueryable<Book> GetAllByCondition(Expression<Func<Book, bool>> expression, bool trackChanges = false)
        {
            return _repositoryManager.BookRepository.GetAllByCondition(expression,trackChanges);
        }

        public Book? GetById(int id,bool trackChanges)
        {
            return _repositoryManager.BookRepository.GetById(id,trackChanges);
        }

        public void Update(int id,Book entity)
        {
            var foundEntity = _repositoryManager.BookRepository.GetById(id, false);
            if (foundEntity is null)
                throw new NullReferenceException($"Book with {id} id could not found");

            if(entity is null)
                throw new NullReferenceException(nameof(entity));

            foundEntity.Title = entity.Title;
            foundEntity.Price = entity.Price;

            _repositoryManager.BookRepository.Update(foundEntity);
            _repositoryManager.Save();
        }
    }
}
