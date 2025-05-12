namespace Business.Dtos.Responses
{
    public class ImageDto
    {
        public required string Name { get; set; }
        public required string ContentType { get; set; }
        public required string Data { get; set; }
        public Guid? Book { get; set; } = new();
        public Guid? Publisher { get; set; } = new();
        public Guid? AuthorDetail { get; set; } = new();
        public Guid? User { get; set; } = new();
    }
}
