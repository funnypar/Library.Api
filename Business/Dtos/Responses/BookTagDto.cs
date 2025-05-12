namespace Business.Dtos.Responses
{
    public class BookTagDto
    {
        public Guid Id { get; init; }
        public required string Name { get; set; }
        public string Slug { get; set; } = string.Empty;
        public required ICollection<BookDto> Books { get; set; }

    }
}
