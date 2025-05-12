using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext.DataContext _dataContext;
        public UserRepository(DataContext.DataContext dataContext) 
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var users = await _dataContext.Users
                    .Include(user => user.Image)
                    .ToListAsync(cancellationToken:  cancellationToken);
                return users;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Something went wrong when we want to get all users.", ex);
            }
        }
        public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _dataContext.Users
                           .Include(user => user.Image)
                           .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
                if (user == null) 
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Something went wrong when we want to get user.", ex);
            }
        }
        public async Task<User?> GetUserBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _dataContext.Users.
                    Include(user => user.Image)
                    .FirstOrDefaultAsync(user => user.Slug == slug, cancellationToken: cancellationToken);
                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Something went wrong when we want to get the user.", ex);
            }
        }
        public async Task<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _dataContext.Users.AddAsync(user, cancellationToken: cancellationToken);
                await _dataContext.SaveChangesAsync(cancellationToken: cancellationToken);
                if(result == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Something went wrong when we want to create the user.", ex);
            }
        }
        public async Task<bool> UpdateUserByIdAsync(Guid id,User user, CancellationToken cancellationToken = default)
        {
            try
            {
                var userFound = await _dataContext.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
                if (userFound is null)
                {
                    return false;
                }

                userFound.UserName = user.UserName;
                userFound.Password = user.Password;
                userFound.Email = user.Email;
                userFound.SetSlug();

                var result = await _dataContext.SaveChangesAsync(cancellationToken);
                if (result <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Something went wrong when we want to update the user.", ex);
            }
        }
        public async Task<bool> DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _dataContext.Users.Where(user => user.Id == id).ExecuteDeleteAsync(cancellationToken);
                if(user <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Something went wrong when we want to delete the user.", ex);
            }
        }
    }
}
