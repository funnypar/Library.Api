namespace Business.Dtos.Responses
{
    public class AuthorsResponseDto
    {
        public int Total { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
    }
}
