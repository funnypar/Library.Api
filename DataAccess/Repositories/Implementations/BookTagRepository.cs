using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class BookTagRepository : IBookTagRepository
    {
        private readonly DataContext.DataContext _context;
        public BookTagRepository(DataContext.DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<IEnumerable<BookTag>> GetAllBookTagsAsync(CancellationToken token = default)
        {
            try
            {
                var bookTags = await _context.BookTags
                .Include(bookTag => bookTag.Books)
                .ThenInclude(book => book.Authors)
                .Include(bookTag => bookTag.Books)
                .ThenInclude(book => book.Publisher)
                .ToListAsync(cancellationToken: token);
                return bookTags;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the booktags.", ex);
            }
            
        }
        public async Task<BookTag?> GetBookTagByIdAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var bookTag = await _context.BookTags
                                .Include(bookTag => bookTag.Books)
                                .ThenInclude(book => book.Authors)
                                .Include(bookTag => bookTag.Books)
                                .ThenInclude(book => book.Publisher)
                                .FirstOrDefaultAsync(bookTag => bookTag.Id == id, cancellationToken: token);

                if (bookTag == null)
                {
                    return null;
                }
                return bookTag;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the booktag.", ex);
            }
            
        }
        public async Task<BookTag?> GetBookTagBySlugAsync(string slug, CancellationToken token = default)
        {
            try
            {
                var bookTag = await _context.BookTags
                .Include(bookTag => bookTag.Books)
                .ThenInclude(book => book.Authors)
                .Include(bookTag => bookTag.Books)
                .ThenInclude(book => book.Publisher)
                .FirstOrDefaultAsync(bookTag => bookTag.Slug == slug, cancellationToken: token);

                if (bookTag == null)
                {
                    return null;
                }
                return bookTag;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the booktag.", ex);
            }
            
        }
        public async Task<bool> CreateBookTagAsync(BookTag bookTag, CancellationToken token = default)
        {
            try
            {
                var result = await _context.AddAsync(bookTag, cancellationToken: token);
                if (result == null)
                {
                    return false;
                }
                await _context.SaveChangesAsync(cancellationToken: token);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while creating the booktag.", ex);
            }
            
        }
        public async Task<bool> DeleteBookTagByIdAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _context.BookTags.Where(bookTag => bookTag.Id == id).ExecuteDeleteAsync(cancellationToken: token);
                if (result == 0)
                {
                    return false;
                }
                await _context.SaveChangesAsync(cancellationToken: token);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while deleting the booktag.", ex);
            }
        }
    }
}
