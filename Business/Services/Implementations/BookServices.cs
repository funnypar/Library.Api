using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Business.Mapping;
using Business.Services.Interfaces;
using DataAccess.DataContext;
using DataAccess.Repositories.Interfaces;
using FluentValidation;
using Library.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Implementations
{
    public class BookServices : IBookServices
    {
        private readonly DataContext _dataContext;
        private readonly IBookRepository _bookRepository;
        private readonly IValidator<Book> _bookValidator;
        private readonly IValidator<RequestBooksOptions> _requestValidator;
        public BookServices(IBookRepository bookRepository, DataContext dataContext, IValidator<Book> bookValidator, IValidator<RequestBooksOptions> requestValidator)
        {
            _bookRepository = bookRepository;
            _dataContext = dataContext;
            _bookValidator = bookValidator;
            _requestValidator = requestValidator;
        }
        public async Task<BooksResponseDto> GetAllBooksAsync(RequestBooksOptions options, CancellationToken token = default)
        {
            try
            {
                await _requestValidator.ValidateAndThrowAsync(options, token);

                var books = await _bookRepository.GetAllBooksAsync(options, token);
                var mappedBooks = books.Select(book => book.MapToBookDto()).ToList();


                var pagedBooks = mappedBooks
                    .Skip((options.Page - 1) * options.PageSize)
                    .Take(options.PageSize)
                    .ToList();

                var response = new BooksResponseDto
                {
                    Total = mappedBooks.Count,
                    Page = options.Page,
                    PageSize = options.PageSize,
                    Items = pagedBooks
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An unexpected error occurred while getting the books. --> {ex.Message}", ex);
            }
        }

        public async Task<BooksResponseDto> GetBookAsync(RequestBooksOptions options ,string idOrSlug, CancellationToken token = default)
        {
            try
            {
                var book = Guid.TryParse(idOrSlug, out var id) 
                    ? await _bookRepository.GetBookByIdAsync(id, token)
                    : await _bookRepository.GetBookBySlugAsync(idOrSlug, token);
                if (book == null)
                {
                    return new BooksResponseDto()
                    {
                        Total = 0,
                        Page = 1,
                        PageSize = 10,
                        Items = new List<BookDto>()
                    };
                }
                var mappedBook = new List<BookDto> { book.MapToBookDto() };
                var result = mappedBook.MapToBooksResponseDto(options);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An unexpected error occurred while getting the book --> {ex.Message}", ex);
            }
        }
        public async Task<Guid> CreateBookAsync(BooksRequestDto bookRequestDto, CancellationToken token = default)
        {
            try
            {
                var book = bookRequestDto.MapToBook();

                // Get circular
                var publisher = await _dataContext.Publishers
                        .Where(publisher => bookRequestDto.PublisherId == publisher.Id).FirstAsync(cancellationToken: token);
                var authors = await _dataContext.Authors
                        .Where(author => bookRequestDto.Authors.Contains(author.Id))
                        .ToListAsync(cancellationToken: token);
                var bookTags = await _dataContext.BookTags
                         .Where(bookTag => bookRequestDto.BookTags.Contains(bookTag.Id))
                         .ToListAsync(cancellationToken: token);

                var imageGuids = bookRequestDto.Images.Select(Guid.Parse).ToList();
                var images = await _dataContext.Images
                    .Where(bookImage => imageGuids.Contains(bookImage.Id))
                    .ToArrayAsync(cancellationToken: token);
                // Set them
                book.Title = bookRequestDto.Title;
                book.Authors = authors;
                book.BookTags = bookTags;
                book.Publisher = publisher;
                book.Images = images;
                book.SetSlug();

                //validate
                await _bookValidator.ValidateAndThrowAsync(book, cancellationToken: token);
                // Save
                var result = await _bookRepository.CreateBookAsync(book, token);

                if (!result)
                {
                    return Guid.Empty;
                }
                return book.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while creating the book.", ex);
            }
        }
        public async Task<Guid> UpdateBookByIdAsync(Guid id, BooksRequestDto booksRequestDto, CancellationToken token = default)
        {
            try
            {
                var book = await _dataContext.Books
                .Include(book => book.Authors)
                .Include(book => book.Publisher)
                .Include(book => book.BookTags)
                .Include(book => book.Images)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken: token);

                if (book == null)
                {
                    return Guid.Empty;
                }

                // Get circulars
                var authors = await _dataContext.Authors
                    .Where(author => booksRequestDto.Authors.Contains(author.Id))
                    .ToListAsync(token);

                var publisher = await _dataContext.Publishers
                    .FirstOrDefaultAsync(b => b.Id == booksRequestDto.PublisherId, cancellationToken: token);

                if (publisher == null)
                {
                    throw new Exception("There is no publisher");
                }

                var bookTags = await _dataContext.BookTags
                    .Where(bookTag => booksRequestDto.BookTags.Contains(bookTag.Id))
                    .ToListAsync(token);
                var imageIds = booksRequestDto.Images.Select(Guid.Parse).ToArray(); 
                var images = await _dataContext.Images
                                    .Where(image => imageIds.Contains(image.Id))
                                    .ToArrayAsync(token);


                //  Update the existing book entity instead of creating a new one
                book.Title = booksRequestDto.Title;
                book.Authors.Clear();
                book.BookTags.Clear();
                book.Description = booksRequestDto.Description;
                book.Authors = authors;
                book.Publisher = publisher;
                book.BookTags = bookTags;
                book.Images = images;
                book.PublishedDate = booksRequestDto.PublishedDate;
                book.SetSlug();

                // Save
                await _bookValidator.ValidateAndThrowAsync(book, token);
                await _dataContext.SaveChangesAsync(token);

                return book.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while updating the book.", ex);
            }
        }
        public async Task<Guid> DeleteBookByIdAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _bookRepository.DeleteBookByIdAsync(id, token);
                if (!result)
                {
                    return Guid.Empty;
                }
                return id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while deleting the book.", ex);
            }
        }

  
    }
}
