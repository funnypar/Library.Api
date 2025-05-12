using DataAccess.Models;

namespace Business.Dtos.Responses
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<string>? Books { get; set; } 
        public required AuthorDetailDto AuthorDetail { get; set; }
        public required string Slug { get; set; }

    }
}
