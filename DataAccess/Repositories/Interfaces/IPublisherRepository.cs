using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetAllPublisherAsync(CancellationToken token = default);
        Task<Publisher?> GetPublisherByIdAsync(Guid id, CancellationToken token = default);
        Task<Publisher?> GetPublisherBySlugAsync(string slug, CancellationToken token = default);
        Task<bool> CreatePublisherAsync(Publisher publisher, CancellationToken token = default);
        Task<bool> DeletePublisherByIdAsync(Guid id, CancellationToken token = default);
    }
}
