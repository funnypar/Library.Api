using Business.Dtos.Requests;
using Business.Dtos.Responses;

namespace Business.Services.Interfaces
{
    public interface IBookTagServices
    {
        Task<BookTagsResponseDto> GetAllBookTagsAsync(CancellationToken token = default);
        Task<BookTagsResponseDto> GetBookTagAsync(string idOrSlug,CancellationToken token = default);
        Task<Guid> CreateBookTagAsync(BookTagsRequestDto bookTag, CancellationToken token = default);
        Task<Guid> UpdateBookTagAsync(Guid id, BookTagsRequestDto bookTag, CancellationToken token = default);
        Task<Guid> DeleteBookTagAsync(Guid id, CancellationToken token = default);
    }
}
