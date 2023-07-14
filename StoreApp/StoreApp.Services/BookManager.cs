using AutoMapper;

using Microsoft.EntityFrameworkCore;

using StoreApp.Entities.DTOs;
using StoreApp.Entities.Enums;
using StoreApp.Entities.Models;
using StoreApp.Entities.Models.LinkModels;
using StoreApp.Entities.Models.RequestFeatures;
using StoreApp.Repositories.Abstract;
using StoreApp.Repositories.EFCore.Extensions;
using StoreApp.Services.Abstract;

using System.Linq.Expressions;

namespace StoreApp.Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;
        private readonly IBookLinks<BookDto> _bookLinksForBookDto;
        private readonly IBookLinks<BookWithDetailsDto> _bookLinksForBookWithDetailsDto;

        public BookManager(IRepositoryManager repositoryManager, ILoggerService loggerService, IMapper mapper, IBookLinks<BookDto> bookLinksForBookDto, IBookLinks<BookWithDetailsDto> bookLinksForBookWithDetailsDto)
        {
            _repositoryManager = repositoryManager;
            _loggerService = loggerService;
            _mapper = mapper;
            _bookLinksForBookDto = bookLinksForBookDto;
            _bookLinksForBookWithDetailsDto = bookLinksForBookWithDetailsDto;
        }

        public async Task<bool> AnyAsync(Expression<Func<Book, bool>> expression,bool trackChanges)
        {
            return await _repositoryManager.BookRepository.AnyAsync(expression,trackChanges);
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

        public (LinkResponse linkResponse,MetaData metaData) GetAll(LinkParameters linkParameters, bool trackChanges)
        {
            var books = _repositoryManager
                .BookRepository
                .GetAll(trackChanges)
                .FilterBooksByPrice(linkParameters.BookParameters.MinPrice, linkParameters.BookParameters.MaxPrice)
                .SearchByTitle(linkParameters.BookParameters.SearchTerm)
                .Sort(linkParameters.BookParameters.OrderBy);

            var pagedList = PagedList<Book>.ToPagedList(books, linkParameters.BookParameters.PageNumber, linkParameters.BookParameters.PageSize);

            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(pagedList);

            var links = _bookLinksForBookDto.TryGenerateLinks(bookDtos, linkParameters.BookParameters.Fields, linkParameters.HttpContext);

            return (linkResponse : links, metaData : pagedList.MetaData);
        }
        public async Task<List<BookDto>> GetAll(bool trackChanges = false)
        {
            var books = await _repositoryManager.BookRepository.GetAll(trackChanges).ToListAsync();

            return _mapper.Map <List<BookDto>>(books);
        }

        public (LinkResponse linkResponse, MetaData metaData) GetAllWithDetails(LinkParameters linkParameters, bool trackChanges)
        {
            var books = _repositoryManager
                .BookRepository
                .GetAll(trackChanges)
                .Include(b => b.Category)
                .AsQueryable()
                .FilterBooksByPrice(linkParameters.BookParameters.MinPrice, linkParameters.BookParameters.MaxPrice)
                .SearchByTitle(linkParameters.BookParameters.SearchTerm)
                .Sort(linkParameters.BookParameters.OrderBy);

            var pagedList = PagedList<Book>.ToPagedList(books, linkParameters.BookParameters.PageNumber, linkParameters.BookParameters.PageSize);

            var bookDtos = _mapper.Map<IEnumerable<BookWithDetailsDto>>(pagedList);

            var links = _bookLinksForBookWithDetailsDto.TryGenerateLinks(bookDtos, linkParameters.BookParameters.Fields, linkParameters.HttpContext);

            return (linkResponse: links, metaData: pagedList.MetaData);
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
