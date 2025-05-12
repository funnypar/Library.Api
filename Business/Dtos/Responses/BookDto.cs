namespace Business.Dtos.Responses
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool IsPublished { get; set; }
        public List<string> Tags { get; set; } = new();
        public List<string> Authors { get; set; } = new();
        public Guid[]? Images { get; set; } 
        public string Publisher { get; set; } = string.Empty;
        public required string Slug { get; set; }
    }
}
