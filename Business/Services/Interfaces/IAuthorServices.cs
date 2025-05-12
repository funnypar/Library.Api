using Business.Dtos.Requests;
using Business.Dtos.Responses;

namespace Business.Services.Interfaces
{
    public interface IAuthorServices
    {
        Task<AuthorsResponseDto> GetAllAuthorsAsync(CancellationToken token = default);
        Task<AuthorsResponseDto> GetAuthorAsync(string idOrSlug, CancellationToken token = default);
        Task<Guid> CreateAuthorAsync(AuthorsRequestDto authorRequestDto,CancellationToken token = default);
        Task<Guid> DeleteAuthorAsync(Guid id, CancellationToken token = default);
        Task<Guid> UpdateAuthorAsync(Guid id,AuthorsRequestDto authorsRequestDto, CancellationToken token = default);
    }
}
