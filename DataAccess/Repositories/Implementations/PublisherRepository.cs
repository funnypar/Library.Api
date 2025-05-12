using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Repositories.Implementations
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly DataContext.DataContext _context;

        public PublisherRepository(DataContext.DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<IEnumerable<Publisher>> GetAllPublisherAsync(CancellationToken token = default)
        {
            try
            {
                var publishers = await _context.Publishers
                .Include(publisher => publisher.Books)
                .ThenInclude(book => book.Authors)
                .Include(publisher => publisher.Books)
                .ThenInclude(book => book.BookTags)
                .Include(publisher => publisher.Images)
                .ToListAsync(cancellationToken: token);
                return publishers;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the publishers.", ex);
            }
        }
        public async Task<Publisher?> GetPublisherByIdAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var publisher = await _context.Publishers.Include(publisher => publisher.Books)
                .ThenInclude(book => book.Authors)
                .Include(publisher => publisher.Books)
                .ThenInclude(book => book.BookTags)
                .Include(publisher => publisher.Images)
                .FirstOrDefaultAsync(publisher => publisher.Id == id, cancellationToken: token);
                if (publisher == null)
                {
                    return null;
                }
                return publisher;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the publisher.", ex);
            }
            
        }
        public async Task<Publisher?> GetPublisherBySlugAsync(string slug, CancellationToken token = default)
        {
            try
            {
                var publisher = await _context.Publishers
                                .Include(publisher => publisher.Books)
                                .ThenInclude(book => book.Authors)
                                .Include(publisher => publisher.Images)
                                .FirstOrDefaultAsync(publisher => publisher.Slug == slug, cancellationToken: token);
                if (publisher == null)
                {
                    return null;
                }
                return publisher;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the publisher.", ex);
            }
        }
        public async Task<bool> CreatePublisherAsync(Publisher publisher, CancellationToken token = default)
        {
            try
            {
                var result = await _context.Publishers.AddAsync(publisher, cancellationToken: token);
                if (result == null)
                {
                    return false;
                }
                await _context.SaveChangesAsync(cancellationToken: token);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while creating the publisher.", ex);
            }
        }
        public async Task<bool> DeletePublisherByIdAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _context.Publishers
                    .Where(p => p.Id == id)
                    .ExecuteDeleteAsync(cancellationToken: token);

                if (result == 0)
                {
                    return false; 
                }

                return true;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                throw new InvalidOperationException("Cannot delete the publisher because it has associated books.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while deleting the publisher.", ex);
            }
        }
    }
}
