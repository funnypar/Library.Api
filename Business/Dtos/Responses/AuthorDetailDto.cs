using DataAccess.Models;

namespace Business.Dtos.Responses
{
    public class AuthorDetailDto
    {
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Website { get; set; }
        public Guid[]? Images { get; set; }

    }
}
