using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Microsoft.AspNetCore.Http;

namespace Business.Services.Interfaces
{
    public interface IImageServices
    {
        Task<ImageResponseDto> GetAllImagesAsync(CancellationToken token = default);
        Task<ImageResponseDto> GetImageAsync(Guid id, CancellationToken token = default);
        Task<Guid> CreateImageAsync(ImageRequestDto imageRequestDto, CancellationToken token = default);
        Task<Guid> DeleteImageAsync(Guid id, CancellationToken token = default);
        Task<Guid> UpdateImageAsync(Guid id, ImageRequestDto imageRequestDto, CancellationToken token = default);
    }
}
