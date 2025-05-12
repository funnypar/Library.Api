namespace Business.Dtos.Responses
{
    public class UserDto
    {
        public Guid Id { get; init; }
        public  DateTime CreatedOn { get; init; } = DateTime.Now;
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Guid? Image { get; set; }
        public string Slug { get; set; } = string.Empty;
    }
}
