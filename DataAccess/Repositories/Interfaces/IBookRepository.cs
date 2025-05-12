using Library.Api.Mapping;

namespace DataAccess.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<bool> CreateBookAsync(Book book, CancellationToken token = default);
        Task<Book?> GetBookByIdAsync(Guid id, CancellationToken token = default);
        Task<Book?> GetBookBySlugAsync(string slug, CancellationToken token = default);
        Task<IEnumerable<Book>> GetAllBooksAsync(RequestBooksOptions options,CancellationToken token = default);
        Task<bool> DeleteBookByIdAsync(Guid id, CancellationToken token = default);
    }
}
