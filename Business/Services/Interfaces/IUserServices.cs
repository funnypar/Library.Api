using Business.Dtos.Requests;
using Business.Dtos.Responses;

namespace Business.Services.Interfaces
{
    public interface IUserServices
    {
        Task<UserResponseDto> GetAllUserAsync(CancellationToken token = default);
        Task<UserResponseDto> GetUserAsync(string idOrSlug, CancellationToken token = default);
        Task<Guid> CreateUserAsync(UserRequestDto userRequestDto, CancellationToken token = default);
        Task<Guid> DeleteUserAsync(Guid id, CancellationToken token = default);
        Task<Guid> UpdateUserAsync(Guid id, UserRequestDto publishersRequestDto, CancellationToken token = default);
    }
}
