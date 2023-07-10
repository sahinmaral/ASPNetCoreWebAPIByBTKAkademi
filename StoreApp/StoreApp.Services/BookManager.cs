using AutoMapper;

using StoreApp.Entities.DTOs;
using StoreApp.Entities.Enums;
using StoreApp.Entities.Models;
using StoreApp.Entities.Models.Exceptions;
using StoreApp.Repositories.Abstract;
using StoreApp.Services.Abstract;

using System.Linq.Expressions;

namespace StoreApp.Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;
        public BookManager(IRepositoryManager repositoryManager, ILoggerService loggerService, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public BookDto Create(BookDtoForCreate dto)
        {

            Book? existedBook = _repositoryManager.BookRepository.GetAllByCondition(x => x.Title.Equals(dto.Title),false).SingleOrDefault();
            if (existedBook is not null)
            {
                string message = $"Book with {dto.Title} title has already added";

                _loggerService.Log(message,LogTypes.Info);
                throw new InvalidOperationException(message);
            }

            Book entity = _mapper.Map<Book>(dto);
            _repositoryManager.BookRepository.Create(entity);
            _repositoryManager.Save();

            return _mapper.Map<BookDto>(entity);
        }

        public void Delete(int id)
        {
            var foundEntity = _repositoryManager.BookRepository.GetById(id,false);
            if (foundEntity is null)
            {
                string message = $"Book with {id} id could not found";
                _loggerService.Log(message,LogTypes.Info);
                throw new BookNotFoundException(id);
            }

            _repositoryManager.BookRepository.Delete(foundEntity);
            _repositoryManager.Save();
        }

        public IEnumerable<BookDto> GetAll(bool trackChanges)
        {
            return _mapper.Map<IEnumerable<BookDto>>(_repositoryManager.BookRepository.GetAll(trackChanges));
        }

        public IQueryable<BookDto> GetAllByCondition(Expression<Func<Book, bool>> expression, bool trackChanges = false)
        {
            return _mapper.Map<IQueryable<BookDto>>(_repositoryManager.BookRepository.GetAllByCondition(expression,trackChanges));
        }

        public BookDto? GetById(int id,bool trackChanges)
        {
            return _mapper.Map<BookDto>(_repositoryManager.BookRepository.GetById(id, trackChanges));
        }

        public BookDtoForUpdate GetByIdForPatch(int id, bool trackChanges = false)
        {
            var book = _repositoryManager.BookRepository.GetById(id, trackChanges);

            if (book is null)
                throw new BookNotFoundException(id);

            var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(book);

            return bookDtoForUpdate;
        }

        public void Update(int id, BookDtoForUpdate dto)
        {
            var foundEntity = _repositoryManager.BookRepository.GetById(id, false);
            if (foundEntity is null)
            {
                string message = $"Book with {id} id could not found";
                _loggerService.Log(message, LogTypes.Info);
                throw new BookNotFoundException(id);
            }

            _repositoryManager.BookRepository.Update(_mapper.Map<Book>(dto));
            _repositoryManager.Save();
        }

    }
}
