using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAllImagesAsync(CancellationToken token = default);
        Task<Image?> GetImageByIdAsync(Guid id, CancellationToken token = default);
        Task<bool> CreateImageAsync(Image image, CancellationToken token = default);
        Task<bool> DeleteImageByIdAsync(Guid id, CancellationToken token = default);
        Task<bool> UpdateImageAsync(Guid id, Image image, CancellationToken token = default);
    }
}
