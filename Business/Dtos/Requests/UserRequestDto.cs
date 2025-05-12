namespace Business.Dtos.Requests
{
    public class UserRequestDto
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Guid Image { get; set; }
    }
}
