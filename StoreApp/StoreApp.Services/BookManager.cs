using AutoMapper;

using Microsoft.EntityFrameworkCore;

using StoreApp.Entities.DTOs;
using StoreApp.Entities.Enums;
using StoreApp.Entities.Models;
using StoreApp.Entities.Models.RequestFeatures;
using StoreApp.Repositories.Abstract;
using StoreApp.Services.Abstract;

using System.Linq.Expressions;

using static System.Reflection.Metadata.BlobBuilder;

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

        public async Task<bool> AnyAsync(Expression<Func<Book, bool>> expression)
        {
            return await _repositoryManager.BookRepository.AnyAsync(expression);
        }

        public BookDto Create(BookDtoForCreate dto)
        {
            Book? existedBook = _repositoryManager.BookRepository.GetAllByCondition(x => x.Title.Equals(dto.Title), false).SingleOrDefault();
            if (existedBook is not null)
            {
                string message = $"Book with {dto.Title} title has already added";

                _loggerService.Log(message, LogTypes.Info);
                throw new InvalidOperationException(message);
            }

            Book entity = _mapper.Map<Book>(dto);
            _repositoryManager.BookRepository.Create(entity);
            _repositoryManager.Save();

            return _mapper.Map<BookDto>(entity);
        }

        public async Task<BookDto> CreateAsync(BookDtoForCreate dto)
        {
            Book? existedBook = _repositoryManager.BookRepository.GetAllByCondition(x => x.Title.Equals(dto.Title), false).SingleOrDefault();
            if (existedBook is not null)
            {
                string message = $"Book with {dto.Title} title has already added";
                _loggerService.Log(message, LogTypes.Info);
                throw new InvalidOperationException(message);
            }

            Book entity = _mapper.Map<Book>(dto);
            _repositoryManager.BookRepository.Create(entity);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<BookDto>(entity);
        }

        public void Delete(int id)
        {
            var foundEntity = _repositoryManager.BookRepository.GetById(id, false);
            _repositoryManager.BookRepository.Delete(foundEntity);
            _repositoryManager.Save();
        }

        public async Task DeleteAsync(int id)
        {
            var foundEntity = await _repositoryManager.BookRepository.GetByIdAsync(id, false);
            _repositoryManager.BookRepository.Delete(foundEntity);
            await _repositoryManager.SaveAsync();
        }

        public async Task<(IEnumerable<BookDto> books,MetaData metaData)> GetAllAsync(BookParameters bookParameters, bool trackChanges)
        {
            var books = await _repositoryManager
                .BookRepository
                .GetAll(trackChanges)
                .OrderBy(b => b.Id)
                .ToListAsync();

            var pagedList = PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);

            var bookDto = _mapper.Map<IEnumerable<BookDto>>(pagedList);

            return (bookDto, pagedList.MetaData);
        }

        public async Task<(IEnumerable<BookDto> books, MetaData metaData)> GetAllByConditionAsync(BookParameters bookParameters, Expression<Func<Book, bool>> expression, bool trackChanges = false)
        {
            var books = await _repositoryManager
                .BookRepository
                .GetAllByCondition(expression, trackChanges)
                .OrderBy(b => b.Id)
                .ToListAsync();

            var pagedList = PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);

            var bookDto = _mapper.Map<IEnumerable<BookDto>>(pagedList);

            return (bookDto, pagedList.MetaData);
        }

        public BookDto? GetById(int id, bool trackChanges)
        {
            return _mapper.Map<BookDto>(_repositoryManager.BookRepository.GetById(id, trackChanges));
        }

        public async Task<BookDto?> GetByIdAsync(int id, bool trackChanges = false)
        {
            return _mapper.Map<BookDto>(await _repositoryManager.BookRepository.GetByIdAsync(id, trackChanges));
        }

        public async Task<BookDtoForUpdate> GetByIdForPatchAsync(int id, bool trackChanges = false)
        {
            var book = await _repositoryManager.BookRepository.GetByIdAsync(id, trackChanges);
            var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(book);
            return bookDtoForUpdate;
        }

        public void Update(int id, BookDtoForUpdate dto)
        {
            _repositoryManager.BookRepository.Update(_mapper.Map<Book>(dto));
            _repositoryManager.Save();
        }

        public async Task UpdateAsync(int id, BookDtoForUpdate dto)
        {
            _repositoryManager.BookRepository.Update(_mapper.Map<Book>(dto));
            await _repositoryManager.SaveAsync();
        }
    }
}
