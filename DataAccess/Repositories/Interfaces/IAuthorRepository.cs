using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<bool> CreateAuthorAsync(Author author, CancellationToken token = default);
        Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken token = default);
        Task<Author?> GetAuthorBySlugAsync(string slug, CancellationToken token = default);
        Task<IEnumerable<Author>> GetAllAuthorsAsync(CancellationToken token = default);
        Task<bool> DeleteAuthorByIdAsync(Guid id, CancellationToken token = default);
    }
}
