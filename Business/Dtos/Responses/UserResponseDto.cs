namespace Business.Dtos.Responses
{
    public class UserResponseDto
    {
        public int Total { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<UserDto> Users { get; set; } = new List<UserDto>();
    }
}
