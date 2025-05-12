using DataAccess.Repositories.Interfaces;
using Library.Api.Mapping;
using Library.Core.Attributes;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext.DataContext _dataContext;
        public BookRepository(DataContext.DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(RequestBooksOptions options, CancellationToken token = default)
        {
            try
            {
                var query = _dataContext.Books
                    .Include(book => book.BookTags)
                    .Include(book => book.Authors)
                    .Include(book => book.Publisher)
                    .Include(book => book.Images)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(options?.Title))
                {
                    query = query.Where(book => book.Title.Contains(options.Title));
                }

                var result = await query.ToListAsync(cancellationToken: token);

                if (!string.IsNullOrWhiteSpace(options?.SortField) && options.SortOrder != SortOrder.Unsorted)
                {
                    var sortField = options.SortField;

                    var propertyInfo = typeof(RequestBooksOptions).GetProperty(nameof(RequestBooksOptions.SortField));
                    var hasCapitalizeAttr = propertyInfo?
                        .GetCustomAttributes(typeof(CapitalizeFirstAttribute), false)
                        .Any() ?? false;

                    if (hasCapitalizeAttr)
                    {
                        sortField = char.ToUpper(sortField[0]) + sortField.Substring(1).ToLower();
                    }

                    var bookPropertyInfo = typeof(Book).GetProperty(sortField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (bookPropertyInfo != null)
                    {
                        if (options.SortOrder == SortOrder.Descending)
                        {
                            return result.OrderByDescending(book => bookPropertyInfo.GetValue(book)).ToList();
                        }
                        return result.OrderBy(book => bookPropertyInfo.GetValue(book)).ToList();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the books.", ex);
            }
        }


        public async Task<Book?> GetBookByIdAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var book = await _dataContext.Books
                                .Include(book => book.BookTags)
                                .Include(book => book.Authors)
                                .Include(book => book.Publisher)
                                .Include(book => book.Images)
                                .FirstOrDefaultAsync(book => book.Id == id, cancellationToken: token);
                return book;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the book.", ex);
            }
        }
        public async Task<Book?> GetBookBySlugAsync(string slug, CancellationToken token = default)
        {
            try
            {
                var book = await _dataContext.Books
                              .Include(book => book.BookTags)
                              .Include(book => book.Authors)
                              .Include(book => book.Publisher)
                              .FirstOrDefaultAsync(book => book.Slug == slug, cancellationToken: token);
                return book;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the book.", ex);
            }
        }
        public async Task<bool> CreateBookAsync(Book book, CancellationToken token = default)
        {
            try
            {
                var result = await _dataContext.AddAsync(book, cancellationToken: token);
                if (result == null)
                {
                    return false;
                }
                await _dataContext.SaveChangesAsync(cancellationToken: token);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while creating the book.", ex);
            }
        }
        public async Task<bool> DeleteBookByIdAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _dataContext.Books.Where(book => book.Id == id)
                                .ExecuteDeleteAsync(cancellationToken: token);
                if (result == 0)
                {
                    return false;
                }
                await _dataContext.SaveChangesAsync(cancellationToken: token);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while deleting the book.", ex);
            }
        }

    }
}
