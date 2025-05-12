using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IBookTagRepository
    {
        Task<IEnumerable<BookTag>> GetAllBookTagsAsync(CancellationToken token = default);
        Task<BookTag?> GetBookTagByIdAsync(Guid id, CancellationToken token = default);
        Task<BookTag?> GetBookTagBySlugAsync(string slug, CancellationToken token = default);
        Task<bool> CreateBookTagAsync(BookTag bookTag, CancellationToken token = default);
        Task<bool> DeleteBookTagByIdAsync(Guid id, CancellationToken token = default);
    }
}
