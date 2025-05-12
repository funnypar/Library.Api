using Business.Dtos.Requests;
using Business.Dtos.Responses;
using DataAccess.Models;

namespace Business.Mapping
{
    public static class UsersMapping
    {
        public static UserResponseDto MapToUSerResponseDto(this IEnumerable<UserDto> userResponseDto,int page = 1,int pageSize = 10 )
        {
            return new UserResponseDto
            {
                Page = page,
                PageSize = pageSize,
                Total = userResponseDto.Count(),
                Users = userResponseDto.ToList()
            };
        }

        public static UserDto MapToUserDto (this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                UserName = user.UserName,
                Image = user?.Image?.Id,
                Slug = user.Slug
            };
        }

        public static User MapToUser(this UserRequestDto userRequestDto)
        {
            return new User
            {
                UserName = userRequestDto.UserName,
                Password = userRequestDto.Password,
                Email = userRequestDto.Email,
            };
        }
    }
}
