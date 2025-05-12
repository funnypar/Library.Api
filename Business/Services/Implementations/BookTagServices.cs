using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Business.Mapping;
using Business.Services.Interfaces;
using DataAccess.DataContext;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Implementations
{
    public class BookTagServices : IBookTagServices
    {
        private readonly IBookTagRepository _bookTagRepository;
        private readonly DataContext _dataContext;
        private readonly IValidator<BookTag> _validator;
        public BookTagServices(IBookTagRepository bookTagRepository, DataContext dataContext, IValidator<BookTag> validator)
        {
            _bookTagRepository = bookTagRepository;
            _dataContext = dataContext;
            _validator = validator;
        }
        public async Task<BookTagsResponseDto> GetAllBookTagsAsync(CancellationToken token = default)
        {
            try
            {
                var bookTags = await _bookTagRepository.GetAllBookTagsAsync(token);
                var mappedBookTag = bookTags.Select(bookTag => bookTag.MapToBookTagsDto()).ToList();
                return mappedBookTag.MapToBookTagsResponseDto(page: 1, pageSize: 10);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the booktags.", ex);
            }
        }
        public async Task<BookTagsResponseDto> GetBookTagAsync(string idOrSlug, CancellationToken token = default)
        {
            try
            {
                var bookTag = Guid.TryParse(idOrSlug, out var id) ? await _bookTagRepository.GetBookTagByIdAsync(id) :
                await _bookTagRepository.GetBookTagBySlugAsync(idOrSlug);

                if (bookTag == null)
                {
                    return new BookTagsResponseDto { Page = 1, PageSize = 10, Total = 0, BookTags = [] };
                }
                var mappedBookTag = new List<BookTagDto> { bookTag.MapToBookTagsDto() };
                return mappedBookTag.MapToBookTagsResponseDto(page: 1, pageSize: 10);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the booktag.", ex);
            }
        }
        public async Task<Guid> CreateBookTagAsync(BookTagsRequestDto bookTagsRequestDto,CancellationToken token = default)
        {
            try
            {
                var bookTag = bookTagsRequestDto.MapToBookTag();
                var books = await _dataContext.Books.Where(book => bookTagsRequestDto.Books.Contains(book.Id)).ToListAsync(cancellationToken: token);
                bookTag.Books = books;
                bookTag.SetSlug();

                await _validator.ValidateAndThrowAsync(bookTag, cancellationToken: token);
                var result = await _bookTagRepository.CreateBookTagAsync(bookTag, token);
                if (!result)
                {
                    return Guid.Empty;
                }
                return bookTag.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while creating the booktag.", ex);
            }
        }
        public async Task<Guid> UpdateBookTagAsync(Guid id, BookTagsRequestDto bookTagDto, CancellationToken token = default)
        {
            try
            {
                var bookTag = await _dataContext.BookTags
                .Include(bookTag => bookTag.Books).FirstOrDefaultAsync(bookTag => bookTag.Id == id, cancellationToken: token);
                if (bookTag == null)
                {
                    return Guid.Empty;
                }
                var books = await _dataContext.Books.Where(book => bookTagDto.Books.Contains(book.Id)).ToListAsync(cancellationToken: token);
                bookTag.Name = bookTagDto.Name;
                bookTag.Books.Clear();
                bookTag.Books = books;
                bookTag.SetSlug();

                await _validator.ValidateAndThrowAsync(bookTag, cancellationToken: token);
                await _dataContext.SaveChangesAsync(cancellationToken: token);
                return id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while updating the booktag.", ex);
            }
        }
        public async Task<Guid> DeleteBookTagAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _bookTagRepository.DeleteBookTagByIdAsync(id, token);
                if (!result)
                {
                    return Guid.Empty;
                }
                return id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while deleting the booktag.", ex);
            }
        }
    }
}
