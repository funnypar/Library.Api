using Business.Dtos.Responses;

namespace Business.Dtos.Requests
{
    public class AuthorsRequestDto
    {
        public required string Name { get; set; }
        public required AuthorDetailDto AuthorsDetail { get; set; }
        public required ICollection<Guid> Books { get; set; } = new List<Guid>();

    }
}
