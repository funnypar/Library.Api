using Business.Dtos.Requests;
using Business.Dtos.Responses;
using DataAccess.Models;

namespace Business.Services.Interfaces
{
    public interface IPublisherServices
    {
        Task<PublishersResponseDto> GetAllPublisherAsync(CancellationToken token = default);
        Task<PublishersResponseDto> GetPublisherAsync(string idOrSlug,CancellationToken token = default);
        Task<Guid> CreatePublisherAsync(PublishersRequestDto publishersRequestDto, CancellationToken token = default);
        Task<Guid> DeletePublisherAsync(Guid id,  CancellationToken token = default);
        Task<Guid> UpdatePublisherAsync(Guid id, PublishersRequestDto publishersRequestDto,CancellationToken token= default);
    }
}
