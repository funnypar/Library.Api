using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
        Task<User?> GetUserByIdAsync(Guid id,CancellationToken cancellationToken = default);
        Task<User?> GetUserBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default);
        Task<bool> UpdateUserByIdAsync(Guid id, User user, CancellationToken cancellationToken = default);
        Task<bool> DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
