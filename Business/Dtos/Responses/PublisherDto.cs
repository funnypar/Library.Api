namespace Business.Dtos.Responses
{
    public class PublisherDto
    {
        public Guid Id { get; init; }
        public required string Slug { get; set; }
        public required string Name { get; set; }
        public Guid[]? Images { get; set; }
        public ICollection<BookDto> Books { get; set; } = new List<BookDto>();
    }
}
