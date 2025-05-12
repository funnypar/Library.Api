using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext.DataContext _context;

        public AuthorRepository(DataContext.DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync(CancellationToken token = default)
        {
            try
            {
                var authors = await _context.Authors
                .Include(author => author.AuthorDetail)
                .ThenInclude(authorDetail => authorDetail.Images)
                .Include(author => author.Books)
                .ToListAsync(cancellationToken: token);
                return authors;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the authors.", ex);
            }
        }
        public async Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var author = await _context.Authors
                                .Include(author => author.AuthorDetail)
                                .ThenInclude(authorDetail => authorDetail.Images)
                                .Include(author => author.Books)
                                .FirstOrDefaultAsync(author => author.Id == id, cancellationToken: token);
                return author;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the author.", ex);
            }
        }
        public async Task<Author?> GetAuthorBySlugAsync(string slug, CancellationToken token = default)
        {
            try
            {
                var author = await _context.Authors
                                .Include(author => author.AuthorDetail)
                                .ThenInclude(authorDetail => authorDetail.Images)
                                .Include(author => author.Books)
                                .FirstOrDefaultAsync(author => author.Slug == slug, cancellationToken: token);
                return author;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the author.", ex);
            }
        }
        public async Task<bool> CreateAuthorAsync(Author author, CancellationToken token = default)
        {
            try
            {
                var result = await _context.Authors.AddAsync(author, cancellationToken: token);
                if (result == null)
                {
                    return false;
                }
                await _context.SaveChangesAsync(cancellationToken: token);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while creating the author.", ex);
            }
        }
        public async Task<bool> DeleteAuthorByIdAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _context.Authors.Where(author => author.Id == id).ExecuteDeleteAsync(cancellationToken: token);
                if (result == 0)
                {
                    return false;
                }
                await _context.SaveChangesAsync(cancellationToken: token);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while deleting the author.", ex);
            }
        }
    }
}
