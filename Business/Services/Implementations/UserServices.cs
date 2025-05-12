using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Business.Mapping;
using Business.Services.Interfaces;
using DataAccess.DataContext;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _userValidator;
        private readonly DataContext _dataContext;

        public UserServices(IUserRepository userRepository, IValidator<User> userValidator, DataContext dataContext)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
            _dataContext = dataContext;
        }
        public async Task<UserResponseDto> GetAllUserAsync(CancellationToken token = default)
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync(token);
                var result = users.Select(user => user.MapToUserDto());
                return result.MapToUSerResponseDto();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Something went wrong when we want to get all users.", ex);
            }
        }
        public async Task<UserResponseDto> GetUserAsync(string idOrSlug, CancellationToken token = default)
        {
            try
            {
                var user = Guid.TryParse(idOrSlug, out var id) 
                    ? await _userRepository.GetUserByIdAsync(id,token) 
                    : await _userRepository.GetUserBySlugAsync(idOrSlug,token);
                if (user == null)
                {
                    return new UserResponseDto();
                }
                var mappedUser = new List<UserDto> { user.MapToUserDto() };
                return mappedUser.MapToUSerResponseDto();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Something went wrong when we want to get the user.", ex);
            }
        }
        public async Task<Guid> CreateUserAsync(UserRequestDto userRequestDto, CancellationToken token = default)
        {
            try
            {
                var mappedUser = userRequestDto.MapToUser();
                var image = await _dataContext.Images
                            .FirstOrDefaultAsync(userImage => userImage.Id == userRequestDto.Image);

                mappedUser.Image = image;
                mappedUser.SetSlug();

                await _userValidator.ValidateAndThrowAsync(mappedUser, token);
                var result = await _userRepository.CreateUserAsync(mappedUser, token);

                if (!result)
                {
                    return Guid.Empty;
                }
                return mappedUser.Id;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Something went wrong when we want to create the user.", ex);
            }
        }
        public async Task<Guid> UpdateUserAsync(Guid id, UserRequestDto userRequestDto, CancellationToken token = default)
        {
            try
            {
                var mappedUser = userRequestDto.MapToUser();
                var image = await _dataContext.Images
                           .FirstOrDefaultAsync(userImage => userImage.Id == userRequestDto.Image);

                mappedUser.Image = image;
                mappedUser.SetSlug();

                await _userValidator.ValidateAndThrowAsync(mappedUser, token);
                var user = await _userRepository.UpdateUserByIdAsync(id, mappedUser, token);
                if (!user)
                {
                    return Guid.Empty;
                }
                return id;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Something went wrong when we want to update the user.", ex);
            }
        }
        public async Task<Guid> DeleteUserAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _userRepository.DeleteUserByIdAsync(id, token);
                if (!result)
                {
                    return Guid.Empty;
                }
                return id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Something went wrong when we want to delete the user.", ex);
            }
        }
    }
}
